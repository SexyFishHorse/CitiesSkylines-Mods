namespace SexyFishHorse.CitiesSkylines.Infrastructure.IO
{
    using System.IO;
    using System.Xml.Serialization;
    using JetBrains.Annotations;
    using SexyFishHorse.CitiesSkylines.Infrastructure.Validation.Arguments;

    public class XmlFileSystem<T> : FileSystemWrapper, IXmlFileSystem<T> where T : class
    {
        private readonly IFileSystemWrapper _fileSystem;

        private readonly XmlSerializer _serializer;

        public XmlFileSystem([NotNull] IFileSystemWrapper fileSystem)
        {
            fileSystem.ShouldNotBeNull("fileSystem");
            _fileSystem = fileSystem;

            _serializer = new XmlSerializer(typeof(T));
        }

        public T GetFileAsObject(FileInfo fileInfo)
        {
            fileInfo.ShouldNotBeNull("fileInfo");

            using (var fileStream = _fileSystem.OpenRead(fileInfo))
            {
                return _serializer.Deserialize(fileStream) as T;
            }
        }

        public void SaveObjectToFile(FileInfo fileInfo, T value)
        {
            fileInfo.ShouldNotBeNull("fileInfo");
            value.ShouldNotBeNull("value");

            using (var fileStream = _fileSystem.OpenFile(fileInfo, FileMode.Create, FileAccess.Write))
            {
                _serializer.Serialize(fileStream, value);
            }
        }
    }
}
