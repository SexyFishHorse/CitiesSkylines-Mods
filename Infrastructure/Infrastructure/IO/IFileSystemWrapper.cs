namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System.IO;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IFileSystemWrapper
    {
        void CreateDirectory([NotNull] string folderPath);

        bool FileExists([NotNull] FileInfo fileInfo);

        Stream OpenFile([NotNull] FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess);

        Stream OpenRead([NotNull] FileInfo fileInfo);
    }
}
