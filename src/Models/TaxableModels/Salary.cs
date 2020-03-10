namespace TaxableModels
{
    using System;
    using BaseTypeExtensions;

    public class Salary
    {
        private decimal _grossAmount;

        /// <summary>
        /// Gross amount 
        /// </summary>
        public decimal GrossAmount { get { return _grossAmount; } set => _grossAmount = value.IsInRange(1, decimal.MaxValue); }

        public decimal NetAmount { get { return GrossAmount - IncomeTax - SocialContribution; } }

        public decimal IncomeTax { get; set; }

        public decimal SocialContribution { get; set; }

        public override string ToString()
        {
            return $"Gross amount: {GrossAmount}{Environment.NewLine}NetAmount: {NetAmount}{Environment.NewLine}" +
                $"Income Tax: {IncomeTax}{Environment.NewLine}Social contribution: {SocialContribution}";
        }
    }
}
