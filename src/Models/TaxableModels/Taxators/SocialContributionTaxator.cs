using TaxConfigs;

namespace TaxableModels.Taxators
{
    public class SocialContributionTaxator : Taxator
    {
        public SocialContributionTaxator(SalaryTaxConfig salaryTaxConfig)
            : base(salaryTaxConfig)
        {
        }

        public override Salary TaxSalary(Salary salary)
        {
            var socialTaxableAmount = taxConfig.SocialBenefitsTaxableAmount - taxConfig.IncomeTaxableAmount;
            salary.SocialContribution = CalculateTaxAmount(socialTaxableAmount, taxConfig.SocialBenefitsPercentage);

            return salary;
        }
    }
}
