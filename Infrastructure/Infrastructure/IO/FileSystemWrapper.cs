namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System.IO;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    [PublicAPI]
    public class FileSystemWrapper : IFileSystemWrapper
    {
        public void CreateDirectory(string folderPath)
        {
            folderPath.ShouldNotBeNullOrEmpty("folderPath");

            Directory.CreateDirectory(folderPath);
        }

        public bool FileExists(FileInfo fileInfo)
        {
            fileInfo.ShouldNotBeNull("fileInfo");

            return fileInfo.Exists;
        }

        public Stream OpenFile(FileInfo fileInfo, FileMode fileMode, FileAccess fileAccess)
        {
            fileInfo.ShouldNotBeNull("fileInfo");

            return fileInfo.Open(fileMode, fileAccess);
        }

        public Stream OpenRead(FileInfo fileInfo)
        {
            fileInfo.ShouldNotBeNull("fileInfo");

            return fileInfo.OpenRead();
        }
    }
}
