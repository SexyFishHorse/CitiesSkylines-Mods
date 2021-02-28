namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using JetBrains.Annotations;

    public static class IntExtensions
    {
        [AssertionMethod]
        public static void ShouldBeGreaterThanZero(
            this int value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            if (!(value > 0))
            {
                throw new ArgumentOutOfRangeException(parameterName, value, "The value must be greater than zero");
            }
        }
    }
}
