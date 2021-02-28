namespace SexyFishHorse.CitiesSkylines.Infrastructure.Utils
{
    using System.Linq;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;
    using UnityObject = UnityEngine.Object;

    public static class UnityObjectUtils
    {
        [PublicAPI]
        [CanBeNull]
        public static T FindObject<T>([NotNull] string name) where T : UnityObject
        {
            name.ShouldNotBeNullOrEmpty("name");

            return UnityObject.FindObjectsOfType<T>().FirstOrDefault(x => x.name == name);
        }
    }
}
