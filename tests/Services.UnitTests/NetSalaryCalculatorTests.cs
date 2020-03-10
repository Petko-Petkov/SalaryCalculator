namespace Services.UnitTests
{
    using System.IO;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Options;
    using NetSalaryCalculators;
    using NUnit.Framework;
    using TaxableModels;
    using TaxConfigs;

    [TestFixture]
    public class NetSalaryCalculatorTests
    {
        private IOptions<SalaryTaxConfig> _salaryTaxOptions;
        private IConfigurationRoot _configuration;
        private NetSalaryCalculator _netSalaryCalculator;

        [SetUp]
        public void Setup()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            _configuration = configuration.Build();

            _salaryTaxOptions = Options.Create(_configuration.GetSection("salaryTaxConfig").Get<SalaryTaxConfig>());
            _netSalaryCalculator = new NetSalaryCalculator(_salaryTaxOptions);
        }

        [TestCase(980, 0, 0)]
        [TestCase(1200, 20, 30)]
        [TestCase(2550, 155, 232.5)]
        [TestCase(1873, 87.3, 130.95)]
        [TestCase(3000, 200, 300)]
        [TestCase(3400, 240, 300)]
        [TestCase(6000, 500, 300)]
        public async Task CalculateNetAmount_WithValidInput_ReturnsExpectedAmount(decimal gross, decimal taxExpense, decimal socialExpense)
        {
            // Arrange
            var salary = new Salary { GrossAmount = gross };

            // Act
            salary = await _netSalaryCalculator.CalculateNetAmount(salary);

            // Assert
            Assert.AreEqual(taxExpense, salary.IncomeTax);
            Assert.AreEqual(socialExpense, salary.SocialContribution);
            Assert.AreEqual(gross - taxExpense - socialExpense, salary.NetAmount);
        }

        [Test]
        public async Task CalculateNetAmount_WithNegativeInput_ThrowsException()
        {
            // Arrange
            var salary = new Salary { GrossAmount = -1000 };

            // Act
            salary = await _netSalaryCalculator.CalculateNetAmount(salary);

            // Assert
            Assert.AreEqual(1, salary.GrossAmount);
            Assert.AreEqual(1, salary.NetAmount);
            Assert.AreEqual(0, salary.IncomeTax);
            Assert.AreEqual(0, salary.SocialContribution);
        }
    }
}