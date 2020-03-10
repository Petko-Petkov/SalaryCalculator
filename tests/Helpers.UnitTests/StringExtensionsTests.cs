namespace Helpers.UnitTests
{
    using BaseTypeExtensions;
    using NUnit.Framework;

    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("invalid")]
        [TestCase("-1")]
        [TestCase("792281625142643359354395033600000")]
        public void IsValidDecimal_WithInvalidInput_ReturnsFalse(string input)
        {
            Assert.IsFalse(input.IsValidPositiveDecimal(out decimal num));
        }

        [TestCase("0,1", 0.1)]
        [TestCase("20.1", 20.1)]
        [TestCase("1", 1)]
        [TestCase("7,92281625", 7.92281625)]
        public void IsValidPositiveDecimal_WithValidInput_ReturnsTrue(string input, decimal expected)
        {
            decimal num;
            Assert.IsTrue(input.IsValidPositiveDecimal(out num));
            Assert.AreEqual(expected, num);
        }
    }
}