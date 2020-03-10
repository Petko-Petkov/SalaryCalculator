namespace ImaginariaSalaryCalculator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NetSalaryCalculators;
    using NetSalaryContracts;
    using TaxConfigs;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            await BuildHost().RunAsync();
        }

        private static IHost BuildHost()
        {
            var host = new HostBuilder()
                   .ConfigureHostConfiguration(configHost =>
                   {
                       configHost.SetBasePath(Directory.GetCurrentDirectory());
                       configHost.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                       configHost.AddEnvironmentVariables();
                   })
                   .ConfigureServices((hostContext, services) =>
                   {
                       services.Configure<HostOptions>(opts => opts.ShutdownTimeout = TimeSpan.FromSeconds(1));
                       services.Configure<SalaryTaxConfig>(opts => hostContext.Configuration.GetSection("salaryTaxConfig").Bind(opts));
                       services.AddSingleton<INetSalaryCalculator, NetSalaryCalculator>();
                       services.AddHostedService<ApplicationManager>();
                       services.AddOptions();
                   })
                   .Build();

            return host;
        }
    }
}
