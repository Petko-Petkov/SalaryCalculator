namespace TaxConfigs
{
    public class SalaryTaxConfig
    {
        public decimal IncomeTaxPercentage { get; set; }

        public decimal SocialBenefitsPercentage { get; set; }

        public decimal IncomeTaxableAmount { get; set; }

        public decimal SocialBenefitsTaxableAmount { get; set; }
    }
}
