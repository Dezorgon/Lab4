using FileManager.OptionModels;
using System;
using System.IO;

namespace FileManager
{
    class Archivator
    {
        public void UnZip(string oldPath, string path)
        {
            try
            {
                byte[] file = new byte[0];
                string newPath = GetFilePathInTargetDirectoryByTime(new FileInfo(oldPath));

                new FileStream(newPath, FileMode.OpenOrCreate)
                    .DecompressFromFile(path)
                    .ReadToByteArray(out file)
                    .Dispose();

                var encryption = new Encryption();
                byte[] decryptedFile = encryption.Crypt(file);

                File.WriteAllBytes(newPath, decryptedFile);
            }
            catch (Exception e)
            {
                using (StreamWriter writer = new StreamWriter(Options.PathOptions.Templog, true))
                {
                    writer.WriteLine(e.Message);
                    writer.Flush();
                }
            }
        }
        public string GetFilePathInTargetDirectoryByTime(FileInfo fileInfo, bool archive = false)
        {
            DateTime creationTime = fileInfo.CreationTime;

            string path = Path.Combine(
                creationTime.ToString("yyyy"),
                creationTime.ToString("MM"),
                creationTime.ToString("dd"));

            if (archive)
                path = Path.Combine("Archive", path);

            path = Path.Combine(GetTargetDirectoryPath(), path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return Path.Combine(path,
                Path.GetFileNameWithoutExtension(fileInfo.FullName)
                + creationTime.ToString("_yyyy_MM_dd_HH_mm_ss")
                + fileInfo.Extension);
        }
        private string GetTargetDirectoryPath(string path = "")
        {
            return Path.Combine(Options.PathOptions.TargetDirectory, Path.GetFileName(path));
        }
    }
}
