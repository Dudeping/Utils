using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Codeping.Utils.Mvc
{
    public class UtilPage<TContext, TEntry> : UtilPage
        where TContext : DbContext
        where TEntry : UtilEntry
    {
        protected readonly TContext _context;

        public UtilPage(TContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TEntry Entry { get; set; }

        protected async Task<IActionResult> OnGetAsync(
            int? id, string notFoundText, string notFoundUrl, Action callback = null)
        {
            if (id == null)
            {
                return this.Message(notFoundText, notFoundUrl);
            }

            this.Entry = await _context.Set<TEntry>().FindAsync(id);

            if (this.Entry == null)
            {
                return this.Message(notFoundText, notFoundUrl);
            }

            callback?.Invoke();

            return this.Page();
        }

        protected IActionResult InvokeUpdate(string url, string notFoundText)
        {
            try
            {
                _context.Attach(this.Entry).State = EntityState.Modified;

                _context.SaveChanges();

                return this.Message("修改成功!", url);
            }
            catch (DbUpdateConcurrencyException) when (!this.Exists(this.Entry.Id))
            {
                return this.Message(notFoundText, url);
            }
            catch (Exception ex)
            {
                return this.Message($"保存失败! {ex.Message}", url);
            }
        }

        protected async Task<IActionResult> InvokeUpdateAsync(string url, string notFoundText)
        {
            try
            {
                _context.Attach(this.Entry).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return this.Message("修改成功!", url);
            }
            catch (DbUpdateConcurrencyException) when (!this.Exists(this.Entry.Id))
            {
                return this.Message(notFoundText, url);
            }
            catch (Exception ex)
            {
                return this.Message($"保存失败! {ex.Message}", url);
            }
        }

        protected bool Exists(int id)
        {
            return _context.Set<TEntry>().Any(x => x.Id == id);
        }
    }
}
