namespace BaseTypeExtensions
{
    public static class NumericExtensions
    {
        public static decimal IsInRange(this decimal input, decimal min, decimal max)
        {
            if (min <= input && input <= max)
            {
                return input;
            }

            return 1;
        }
    }
}
