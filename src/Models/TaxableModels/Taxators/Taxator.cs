namespace TaxableModels.Taxators
{
    using TaxConfigs;

    public abstract class Taxator
    {
        protected Taxator superVisor;
        protected SalaryTaxConfig taxConfig;

        protected Taxator(SalaryTaxConfig salaryTaxConfig)
        {
            taxConfig = salaryTaxConfig;
        }

        public Taxator SetSupervisor(Taxator taxator)
        {
            superVisor = taxator;
            return taxator;
        }

        public abstract Salary TaxSalary(Salary salary);

        public decimal CalculateTaxAmount(decimal amount, decimal percentage)
        {
            return amount * (percentage / 100);
        }
    }
}
