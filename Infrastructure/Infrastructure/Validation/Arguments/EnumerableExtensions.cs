namespace SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    public static class EnumerableExtensions
    {
        [AssertionMethod]
        public static void ShouldNotBeNullOrEmpty<T>(
            this IEnumerable<T> collection,
            [InvokerParameterName] [NotNull] string parameterName)
        {
            collection.ShouldNotBeNull(parameterName);

            if (!collection.Any())
            {
                throw new ArgumentException("Collection may not be empty", parameterName);
            }
        }
    }
}
