namespace Helpers.UnitTests
{
    using BaseTypeExtensions;
    using NUnit.Framework;

    [TestFixture]
    public class NumericExtensionsTests
    {
        [TestCase(1, 0, 10)]
        [TestCase(1, 1, 2)]
        [TestCase(10, 1, 10)]
        public void IsInRange_WithValuesWithinRange_ReturnsInput(decimal input, decimal min, decimal max)
        {
            Assert.AreEqual(input, input.IsInRange(min, max));
        }

        [TestCase(0, 1, 10)]
        [TestCase(1, 1.00000000001, 2)]
        [TestCase(10.000000000001, 1, 10)]
        public void IsInRange_WithValuesOutsideRange_ReturnsOne(decimal input, decimal min, decimal max)
        {
            Assert.AreEqual(1, input.IsInRange(min, max));
        }
    }
}
