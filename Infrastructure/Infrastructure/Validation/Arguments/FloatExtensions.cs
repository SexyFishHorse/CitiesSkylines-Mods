namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using JetBrains.Annotations;

    public static class FloatExtensions
    {
        [AssertionMethod]
        public static void ShouldBeGreaterThanOrEqualTo(
            this float value,
            float otherValue,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!(value >= otherValue))
            {
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    value,
                    "The value must be greater than or equal to " + otherValue);
            }
        }

        [AssertionMethod]
        public static void ShouldBeLessThan(
            this float value,
            float otherValue,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!(value < otherValue))
            {
                throw new ArgumentOutOfRangeException(parameterName, value, "The value must be less than " + otherValue);
            }
        }

        [AssertionMethod]
        public static void ShouldBeLessThanOrEqualTo(
            this float value,
            float otherValue,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!(value <= otherValue))
            {
                throw new ArgumentOutOfRangeException(
                    parameterName,
                    value,
                    "The value must be less than or equal to " + otherValue);
            }
        }
    }
}
