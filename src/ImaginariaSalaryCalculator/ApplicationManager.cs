namespace ImaginariaSalaryCalculator
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using BaseTypeExtensions;
    using Microsoft.Extensions.Hosting;
    using NetSalaryContracts;
    using TaxableModels;

    internal class ApplicationManager : BackgroundService
    {
        private readonly INetSalaryCalculator _salaryCalculator;
        private CancellationTokenSource _stoppingCts;

        public ApplicationManager(INetSalaryCalculator salaryCalculator)
        {
            _salaryCalculator = salaryCalculator;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _stoppingCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            Console.CancelKeyPress += ShutDown;
            Console.WriteLine("Starting salary calculator. Type CTRL + C to exit.");

            while (!_stoppingCts.IsCancellationRequested)
            {
                await ReadInput();
            }

            Console.WriteLine("Shutting down application ...");
        }

        private void ShutDown(object sender, ConsoleCancelEventArgs e)
        {
            _stoppingCts.Cancel();
            Environment.Exit(-1);
        }

        private async Task ReadInput()
        {
            Console.WriteLine($"Please input amount for gross salary:");
            var input = Console.ReadLine();

            await Task.Delay(100);

            if (input.IsValidPositiveDecimal(out decimal num))
            {
                var salary = new Salary { GrossAmount = num };
                await _salaryCalculator.CalculateNetAmount(salary);
                Console.WriteLine(salary.ToString());
            }
            else if(!_stoppingCts.IsCancellationRequested)
            {
                Console.WriteLine($"Please input a valid amount for gross salary in the range 1 - {decimal.MaxValue}");
            }
        }
    }
}