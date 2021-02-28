namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System.IO;
    using System.Xml.Serialization;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    public class XmlFileSystem<T> : FileSystemWrapper, IXmlFileSystem<T> where T : class
    {
        private readonly IFileSystemWrapper fileSystem;

        private readonly XmlSerializer serializer;

        public XmlFileSystem([NotNull] IFileSystemWrapper fileSystem)
        {
            fileSystem.ShouldNotBeNull("fileSystem");
            this.fileSystem = fileSystem;

            serializer = new XmlSerializer(typeof(T));
        }

        public T GetFileAsObject(FileInfo fileInfo)
        {
            fileInfo.ShouldNotBeNull("fileInfo");

            using (var fileStream = fileSystem.OpenRead(fileInfo))
            {
                return serializer.Deserialize(fileStream) as T;
            }
        }

        public void SaveObjectToFile(FileInfo fileInfo, T value)
        {
            fileInfo.ShouldNotBeNull("fileInfo");
            value.ShouldNotBeNull("value");

            using (var fileStream = fileSystem.OpenFile(fileInfo, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fileStream, value);
            }
        }
    }
}
