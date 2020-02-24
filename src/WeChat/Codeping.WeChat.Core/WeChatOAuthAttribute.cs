using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Senparc.Weixin;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Codeping.WeChat.Core
{
    /// <summary>
    /// 微信授权属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class WeChatOAuthAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private const string STATUS = "Codeping.Utils.Wechat";
        private const string COOKIE_NAME = "Codeping.Utils.Wechat.RedirectUrl";

        private readonly string _appId = Config.SenparcWeixinSetting.WeixinAppId;
        private readonly string _secret = Config.SenparcWeixinSetting.WeixinAppSecret;

        public bool IsOnlyGetOpenId { get; set; }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                return;
            }

            var code = context.HttpContext.Request.Query["code"];
            var state = context.HttpContext.Request.Query["state"];
            var userAgent = context.HttpContext.Request.Headers["User-Agent"];

            if (!userAgent.ToString().Contains("micromessenger", StringComparison.OrdinalIgnoreCase))
            {
                context.Result = new ContentResult()
                {
                    Content = "不支持除微信浏览器以外的请求!",
                };

                return;
            }

            if (!String.IsNullOrEmpty(code) && !String.IsNullOrEmpty(state))
            {
                var redirectUrl = context.HttpContext.Request.Cookies[COOKIE_NAME];

                if (String.IsNullOrWhiteSpace(redirectUrl))
                {
                    return;
                }

                if (this.IsOnlyGetOpenId)
                {
                    var result = OAuthApi.GetAccessToken(_appId, _secret, code);

                    if (result.errcode != 0)
                    {
                        if (result.errcode == ReturnCode.oauth_code超时 ||
                            result.errcode == ReturnCode.form_id已被使用)
                        {
                            await this.CreateRedirectUrlAsync(context, context.HttpContext);

                            return;
                        }

                        throw new Exception("授权出错, 获取 access_token 失败！");
                    }

                    redirectUrl += (redirectUrl.Contains("?") ? "&" : "?") + "openId=" + result.openid;
                }
                else
                {
                    if (!redirectUrl.Contains("code="))
                    {
                        redirectUrl += (redirectUrl.Contains("?") ? "&" : "?") + "code=" + code;
                    }

                    if (!redirectUrl.Contains("state="))
                    {
                        redirectUrl += (redirectUrl.Contains("?") ? "&" : "?") + "state=" + state;
                    }
                }

                context.HttpContext.Response.Cookies.Delete(COOKIE_NAME);

                context.Result = new RedirectResult(redirectUrl);
            }
            else
            {
                await this.CreateRedirectUrlAsync(context, context.HttpContext);
            }
        }

        private async Task CreateRedirectUrlAsync(AuthorizationFilterContext context, HttpContext httpContext)
        {
            string state = httpContext.Request.Query["state"];
            string redirectUrl = httpContext.Request.Query["redirectUrl"];

            if (String.IsNullOrWhiteSpace(redirectUrl))
            {
                redirectUrl = httpContext.Request.GetDisplayUrl();
            }

            if (!redirectUrl.ToLower().Contains("openid="))
            {
                httpContext.Response.Cookies.Append(COOKIE_NAME, redirectUrl);

                var url = OAuthApi.GetAuthorizeUrl(
                    _appId,
                    redirectUrl,
                    state ?? STATUS,
                    this.IsOnlyGetOpenId ? OAuthScope.snsapi_base : OAuthScope.snsapi_userinfo);

                context.Result = new RedirectResult(url);
            }
            else
            {
                var openid = redirectUrl.Split(new[] { '?', '&' })
                    .FirstOrDefault(x => x.StartsWith("openid=", StringComparison.OrdinalIgnoreCase))
                    .Substring("openid=".Length);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, openid),
                    new Claim(ClaimTypes.Role, "WeChat"),
                };

                var identity = new ClaimsIdentity(claims, WeChatAuthenticationDefaults.AuthenticationScheme);

                await context.HttpContext.SignInAsync(new ClaimsPrincipal(identity));

                context.Result = new RedirectResult(redirectUrl);
            }
        }
    }
}
