using System.IO;
using System.IO.Compression;

namespace FileManager
{
    public static class FileStreamExtensions
    {
        public static FileStream WriteTo(this FileStream fileStream, byte[] array)
        {
            fileStream.Write(array, 0, array.Length);
            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }
        public static FileStream ReadToByteArray(this FileStream fileStream, out byte[] array)
        {
            array = new byte[fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }
        public static FileStream CompressFromFile(this FileStream fileStream, string path)
        {
            using (FileStream targetStream = File.Create(path))
            {
                using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                {
                    fileStream.CopyTo(compressionStream);
                }
            }
            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }
        public static FileStream DecompressFromFile(this FileStream fileStream, string path)
        {
            using (FileStream sourceStream = File.Open(path, FileMode.Open))
            {
                using (GZipStream compressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                {
                    compressionStream.CopyTo(fileStream);
                }
            }
            fileStream.Seek(0, SeekOrigin.Begin);
            return fileStream;
        }
    }
}