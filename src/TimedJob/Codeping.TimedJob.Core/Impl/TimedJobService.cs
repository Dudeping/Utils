using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Codeping.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Codeping.TimedJob.Core
{
    internal class TimedJobService : BackgroundService
    {
        private readonly IHostEnvironment _host;
        private readonly IServiceProvider _services;
        private readonly ICollection<Timer> _timers;
        private readonly IDictionary<string, bool> _status;

        public TimedJobService(IServiceProvider services)
        {
            _services = services;
            _timers = new List<Timer>();
            _status = new Dictionary<string, bool>();
            _host = services.GetService<IHostEnvironment>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var assembly = this.GetType().Assembly;

            var assemblies = TypeEx.GetDependencyAssemblies(_host.ApplicationName)
                .Where(x => x.IsDependency(assembly));

            var types = TypeEx.FindTypes<IJob>(assemblies.ToArray()).Select(x => x.GetTypeInfo());

            var jobs = types.SelectMany(x => x.DeclaredMethods.Where(d => d.GetCustomAttribute<InvokeAttribute>() != null));

            foreach (var method in jobs)
            {
                _status.Add(method.GetFullName(), false);

                var invoke = method.GetCustomAttribute<InvokeAttribute>();

                if (invoke == null || !invoke.IsEnabled)
                {
                    continue;
                }

                var delay = invoke.Begin - DateTime.Now;

                delay = delay > TimeSpan.Zero ? delay : TimeSpan.Zero;

                var interval = TimeSpan.FromMilliseconds(invoke.Interval);

                await Task.Run(delegate
                {
                    var timer = new Timer(
                     x => this.Process(method, invoke), null, delay, interval);

                    _timers.Add(timer);
                });
            }
        }

        private void Process(MethodInfo method, InvokeAttribute invoke)
        {
            var identifier = method.GetFullName();

            if (invoke.SkipWhileExecuting && _status[identifier])
            {
                return;
            }

            using var scope = _services.CreateScope();
            var logger = scope.ServiceProvider.GetService<ILogger<TimedJobService>>();

            var args = method.DeclaringType
                .GetConstructors().First().GetParameters()
                .Select(x => this.MatchParameter(x, scope))
                .ToArray();

            var job = Activator.CreateInstance(method.DeclaringType, args);

            try
            {
                logger?.LogInformation($"Invoking {identifier}...");

                _status[identifier] = true;

                args = method.GetParameters()
                    .Select(x => this.MatchParameter(x, scope))
                    .ToArray();

                var result = method.Invoke(job, args);

                if (result is Task task)
                {
                    task.Wait();
                }

                logger?.LogInformation($"Invoked {identifier}");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
            }
            finally
            {
                _status[identifier] = false;
            }
        }

        private object MatchParameter(ParameterInfo parameter, IServiceScope scope)
        {
            return parameter.ParameterType != typeof(IServiceProvider)
                ? scope.ServiceProvider.GetService(parameter.ParameterType)
                : scope.ServiceProvider;
        }
    }
}
