namespace TaxableModels.Taxators
{
    using TaxConfigs;

    public class IncomeTaxator : Taxator
    {
        public IncomeTaxator(SalaryTaxConfig salaryTaxConfig)
            : base(salaryTaxConfig)
        {
        }

        public override Salary TaxSalary(Salary salary)
        {
            var taxableAmount = salary.GrossAmount - taxConfig.IncomeTaxableAmount;
            salary.IncomeTax = CalculateTaxAmount(taxableAmount, taxConfig.IncomeTaxPercentage); 

            if (salary.GrossAmount > taxConfig.SocialBenefitsTaxableAmount)
            {
                return superVisor?.TaxSalary(salary);
            }
            else
            {
                var socialTaxableAmount = salary.GrossAmount - taxConfig.IncomeTaxableAmount;
                salary.SocialContribution = CalculateTaxAmount(socialTaxableAmount, taxConfig.SocialBenefitsPercentage);
            }

            return salary;
        }
    }
}
