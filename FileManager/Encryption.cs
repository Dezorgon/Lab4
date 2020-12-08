namespace FileManager
{
    public class Encryption
    {
        public byte[] Crypt(byte[] bytes)
        {
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] ^= 1;
            return bytes;
        }
        public string GetCryptedFilePath(string FileName)
        {
            return FileName.EndsWith(".crypt") ? FileName.Remove(FileName.LastIndexOf(".crypt")) : FileName + ".crypt";
        }
    }
}