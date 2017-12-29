using System;
using System.IO;
using System.IO.IsolatedStorage;
using ICSharpCode.SharpZipLib.Tests.TestSupport;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    public class ZipBase
    {
        static protected string GetTempFilePath()
        {
            string result;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                result = Environment.TickCount.ToString();
                store.CreateDirectory(result);
            }
            return result;
        }

        protected byte[] MakeInMemoryZip(bool withSeek, params object[] createSpecs)
        {
            MemoryStream ms;

            if (withSeek)
            {
                ms = new MemoryStream();
            }
            else
            {
                ms = new MemoryStreamWithoutSeek();
            }

            using (ZipOutputStream outStream = new ZipOutputStream(ms))
            {
                for (int counter = 0; counter < createSpecs.Length; ++counter)
                {
                    RuntimeInfo info = createSpecs[counter] as RuntimeInfo;
                    outStream.Password = info.Password;

                    if (info.Method != CompressionMethod.Stored)
                    {
                        outStream.SetLevel(info.CompressionLevel); // 0 - store only to 9 - means best compression
                    }

                    string entryName;

                    if (info.IsDirectory)
                    {
                        entryName = "dir" + counter + "/";
                    }
                    else
                    {
                        entryName = "entry" + counter + ".tst";
                    }

                    ZipEntry entry = new ZipEntry(entryName);
                    entry.CompressionMethod = info.Method;
                    if (info.Crc >= 0)
                    {
                        entry.Crc = info.Crc;
                    }

                    outStream.PutNextEntry(entry);

                    if (info.Size > 0)
                    {
                        outStream.Write(info.Original, 0, info.Original.Length);
                    }
                }
            }
            return ms.ToArray();
        }

        protected byte[] MakeInMemoryZip(ref byte[] original, CompressionMethod method,
                                         int compressionLevel, int size, string password, bool withSeek)
        {
            MemoryStream ms;

            if (withSeek)
            {
                ms = new MemoryStream();
            }
            else
            {
                ms = new MemoryStreamWithoutSeek();
            }

            using (ZipOutputStream outStream = new ZipOutputStream(ms))
            {
                outStream.Password = password;

                if (method != CompressionMethod.Stored)
                {
                    outStream.SetLevel(compressionLevel); // 0 - store only to 9 - means best compression
                }

                ZipEntry entry = new ZipEntry("dummyfile.tst");
                entry.CompressionMethod = method;

                outStream.PutNextEntry(entry);

                if (size > 0)
                {
                    System.Random rnd = new Random();
                    original = new byte[size];
                    rnd.NextBytes(original);

                    // Although this could be written in one chunk doing it in lumps
                    // throws up buffering problems including with encryption the original
                    // source for this change.
                    int index = 0;
                    while (size > 0)
                    {
                        int count = (size > 0x200) ? 0x200 : size;
                        outStream.Write(original, index, count);
                        size -= 0x200;
                        index += count;
                    }
                }
            }
            return ms.ToArray();
        }

        protected static void MakeTempFile(string name, int size)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = store.CreateFile(name))
                {
                    byte[] buffer = new byte[4096];
                    while (size > 0)
                    {
                        fs.Write(buffer, 0, Math.Min(size, buffer.Length));
                        size -= buffer.Length;
                    }
                }
            }
        }

        protected static byte ScatterValue(byte rhs)
        {
            return (byte)((rhs * 253 + 7) & 0xff);
        }

        static void AddKnownDataToEntry(ZipOutputStream zipStream, int size)
        {
            if (size > 0)
            {
                byte nextValue = 0;
                int bufferSize = Math.Min(size, 65536);
                byte[] data = new byte[bufferSize];
                int currentIndex = 0;
                for (int i = 0; i < size; ++i)
                {
                    data[currentIndex] = nextValue;
                    nextValue = ScatterValue(nextValue);

                    currentIndex += 1;
                    if ((currentIndex >= data.Length) || (i + 1 == size))
                    {
                        zipStream.Write(data, 0, currentIndex);
                        currentIndex = 0;
                    }
                }
            }
        }

        public void WriteToFile(string fileName, byte[] data)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = store.OpenFile(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                {
                    fs.Write(data, 0, data.Length);
                }
            }
        }

        #region MakeZipFile
        protected void MakeZipFile(Stream storage, bool isOwner, string[] names, int size, string comment)
        {
            using (ZipOutputStream zOut = new ZipOutputStream(storage))
            {
                zOut.IsStreamOwner = isOwner;
                zOut.SetComment(comment);
                for (int i = 0; i < names.Length; ++i)
                {
                    zOut.PutNextEntry(new ZipEntry(names[i]));
                    AddKnownDataToEntry(zOut, size);
                }
                zOut.Close();
            }
        }

        protected void MakeZipFile(string name, string[] names, int size, string comment)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = store.CreateFile(name))
                {
                    using (ZipOutputStream zOut = new ZipOutputStream(fs))
                    {
                        zOut.SetComment(comment);
                        for (int i = 0; i < names.Length; ++i)
                        {
                            zOut.PutNextEntry(new ZipEntry(names[i]));
                            AddKnownDataToEntry(zOut, size);
                        }
                        zOut.Close();
                    }
                    fs.Close();
                }
            }
        }
        #endregion

        #region MakeZipFile Entries
        protected void MakeZipFile(string name, string entryNamePrefix, int entries, int size, string comment)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream fs = store.CreateFile(name))
                {
                    using (ZipOutputStream zOut = new ZipOutputStream(fs))
                    {
                        zOut.SetComment(comment);
                        for (int i = 0; i < entries; ++i)
                        {
                            zOut.PutNextEntry(new ZipEntry(entryNamePrefix + (i + 1).ToString()));
                            AddKnownDataToEntry(zOut, size);
                        }
                    }
                }
            }
        }

        protected void MakeZipFile(Stream storage, bool isOwner,
                                   string entryNamePrefix, int entries, int size, string comment)
        {
            using (ZipOutputStream zOut = new ZipOutputStream(storage))
            {
                zOut.IsStreamOwner = isOwner;
                zOut.SetComment(comment);
                for (int i = 0; i < entries; ++i)
                {
                    zOut.PutNextEntry(new ZipEntry(entryNamePrefix + (i + 1).ToString()));
                    AddKnownDataToEntry(zOut, size);
                }
            }
        }

        #endregion

        protected static void CheckKnownEntry(Stream inStream, int expectedCount)
        {
            byte[] buffer = new byte[1024];

            int bytesRead;
            int total = 0;
            byte nextValue = 0;
            while ((bytesRead = inStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                total += bytesRead;
                for (int i = 0; i < bytesRead; ++i)
                {
                    Assert.AreEqual(nextValue, buffer[i], "Wrong value read from entry");
                    nextValue = ScatterValue(nextValue);
                }
            }
            Assert.AreEqual(expectedCount, total, "Wrong number of bytes read from entry");
        }

        protected byte ReadByteChecked(Stream stream)
        {
            int rawValue = stream.ReadByte();
            Assert.IsTrue(rawValue >= 0);
            return (byte)rawValue;
        }

        protected int ReadInt(Stream stream)
        {
            return ReadByteChecked(stream) |
                   (ReadByteChecked(stream) << 8) |
                   (ReadByteChecked(stream) << 16) |
                   (ReadByteChecked(stream) << 24);
        }

        protected long ReadLong(Stream stream)
        {
            long result = ReadInt(stream) & 0xffffffff;
            return result | (((long)ReadInt(stream)) << 32);
        }

    }
}