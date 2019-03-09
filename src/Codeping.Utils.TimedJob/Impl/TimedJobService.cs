using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Codeping.Utils.TimedJob
{
    internal class TimedJobService : BackgroundService
    {
        private readonly IAssemblyLocator _locator;
        private readonly IServiceProvider _services;
        private readonly ICollection<Timer> _timers;
        private readonly IDictionary<string, bool> _status;

        public TimedJobService(IAssemblyLocator locator, IServiceProvider services)
        {
            _locator = locator;
            _services = services;
            _timers = new List<Timer>();
            _status = new Dictionary<string, bool>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var jobs = _locator.GetAssemblies()
                .SelectMany(asm => asm.DefinedTypes.Where(x => typeof(IJob).IsAssignableFrom(x))
                    .SelectMany(x => x.DeclaredMethods.Where(y => y.GetCustomAttribute<InvokeAttribute>() != null)));

            foreach (MethodInfo method in jobs)
            {
                _status.Add(method.GetFullName(), false);

                InvokeAttribute invoke = method.GetCustomAttribute<InvokeAttribute>();

                if (invoke == null || !invoke.IsEnabled) continue;

                TimeSpan delay = invoke.Begin - DateTime.Now;

                delay = delay > TimeSpan.Zero ? delay : TimeSpan.Zero;

                var interval = TimeSpan.FromMilliseconds(invoke.Interval);

                await Task.Run(delegate
                {
                    Timer timer = new Timer(
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

            using (IServiceScope scope = _services.CreateScope())
            {
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

                    method.Invoke(job, args);
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
        }

        private object MatchParameter(ParameterInfo parameter, IServiceScope scope)
        {
            return parameter.ParameterType != typeof(IServiceProvider)
                ? scope.ServiceProvider.GetService(parameter.ParameterType)
                : scope.ServiceProvider;
        }
    }
}
