namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using JetBrains.Annotations;

    public static class StringExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNullOrEmpty(
            this string value,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            value.ShouldNotBeNull(parameterName);

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("parameter may not be an empty string", parameterName);
            }
        }
    }
}
