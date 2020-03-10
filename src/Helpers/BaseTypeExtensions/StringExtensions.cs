namespace BaseTypeExtensions
{
    using System.Globalization;

    public static class StringExtensions
    {
        public static bool IsValidPositiveDecimal(this string input, out decimal num)
        {
            num = 0;
            var parsedInput = input.Replace(',', '.');

            if (decimal.TryParse(parsedInput, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal output))
            {
                num = output;
                return output > 0;
            }

            return false;
        }
    }
}
