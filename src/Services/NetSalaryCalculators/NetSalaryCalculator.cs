namespace NetSalaryCalculators
{
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;
    using NetSalaryContracts;
    using TaxableModels;
    using TaxableModels.Taxators;
    using TaxConfigs;

    public class NetSalaryCalculator : INetSalaryCalculator
    {
        private readonly SalaryTaxConfig _taxConfig;
        private readonly Taxator baseTaxator;

        public NetSalaryCalculator(IOptions<SalaryTaxConfig> taxConfigOptions)
        {
            _taxConfig = taxConfigOptions.Value;
            baseTaxator = new BaseTaxator(_taxConfig);
            var incomeTaxator = new IncomeTaxator(_taxConfig);
            var socialTaxator = new SocialContributionTaxator(_taxConfig);
            baseTaxator.SetSupervisor(incomeTaxator).SetSupervisor(socialTaxator);
        }

        public async Task<Salary> CalculateNetAmount(Salary salary)
        {
            await Task.Yield();
            return baseTaxator.TaxSalary(salary);
        }
    }
}
