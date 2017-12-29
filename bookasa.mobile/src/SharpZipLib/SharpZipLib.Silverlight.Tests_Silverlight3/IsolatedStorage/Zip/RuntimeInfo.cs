using System;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    class RuntimeInfo
    {
        public RuntimeInfo(CompressionMethod method, int compressionLevel,
                           int size, string password, bool getCrc)
        {
            this.method = method;
            this.compressionLevel = compressionLevel;
            this.password = password;
            this.size = size;
            this.random = false;

            original = new byte[Size];
            if (random)
            {
                System.Random rnd = new Random();
                rnd.NextBytes(original);
            }
            else
            {
                for (int i = 0; i < size; ++i)
                {
                    original[i] = (byte)'A';
                }
            }

            if (getCrc)
            {
                Crc32 crc32 = new Crc32();
                crc32.Update(original, 0, size);
                crc = crc32.Value;
            }
        }


        public RuntimeInfo(string password, bool isDirectory)
        {
            this.method = CompressionMethod.Stored;
            this.compressionLevel = 1;
            this.password = password;
            this.size = 0;
            this.random = false;
            isDirectory_ = isDirectory;
            original = new byte[0];
        }

        public byte[] Original
        {
            get { return original; }
        }

        public CompressionMethod Method
        {
            get { return method; }
        }

        public int CompressionLevel
        {
            get { return compressionLevel; }
        }

        public int Size
        {
            get { return size; }
        }

        public string Password
        {
            get { return password; }
        }

        bool Random
        {
            get { return random; }
        }

        public long Crc
        {
            get { return crc; }
        }

        public bool IsDirectory
        {
            get { return isDirectory_; }
        }

        #region Instance Fields
        readonly byte[] original;
        readonly CompressionMethod method;
        int compressionLevel;
        int size;
        string password;
        bool random;
        bool isDirectory_;
        long crc = -1;
        #endregion
    }
}