using TaxConfigs;

namespace TaxableModels.Taxators
{
    public class BaseTaxator : Taxator
    {
        public BaseTaxator(SalaryTaxConfig salaryTaxConfig)
            : base(salaryTaxConfig)
        {
        }

        public override Salary TaxSalary(Salary salary)
        {
            if (salary.GrossAmount <= taxConfig.IncomeTaxableAmount)
            {
                return salary;
            }
            else
            {
                return superVisor?.TaxSalary(salary);
            }
        }
    }
}
