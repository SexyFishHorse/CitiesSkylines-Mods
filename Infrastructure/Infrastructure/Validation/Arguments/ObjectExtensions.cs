namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using JetBrains.Annotations;

    public static class ObjectExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNull(
            this object value,
            [NotNull] [InvokerParameterName] string parameterName,
            [CanBeNull] string errorMessage = null)
        {
            if (value != null)
            {
                return;
            }

            if (errorMessage == null)
            {
                throw new ArgumentNullException(parameterName);
            }

            throw new ArgumentNullException(parameterName, errorMessage);
        }
    }
}
