namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System.IO;
    using JetBrains.Annotations;

    public interface IXmlFileSystem<T> : IFileSystemWrapper
        where T : class
    {
        [CanBeNull]
        T GetFileAsObject([NotNull] FileInfo fileInfo);

        void SaveObjectToFile([NotNull] FileInfo fileInfo, [NotNull] T value);
    }
}
