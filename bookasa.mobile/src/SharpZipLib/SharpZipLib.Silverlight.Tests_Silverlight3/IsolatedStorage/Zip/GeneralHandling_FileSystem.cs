using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    //[TestClass]
    public class GeneralHandling_IsolatedStorage : ZipBase
    {
        void TestLargeZip(string tempFile, int targetFiles)
        {
            const int BlockSize = 4096;

            byte[] data = new byte[BlockSize];
            byte nextValue = 0;
            for (int i = 0; i < BlockSize; ++i)
            {
                nextValue = ScatterValue(nextValue);
                data[i] = nextValue;
            }

            using (ZipFile zFile = new ZipFile(tempFile))
            {
                Assert.AreEqual(targetFiles, zFile.Count);
                byte[] readData = new byte[BlockSize];
                int readIndex;
                foreach (ZipEntry ze in zFile)
                {
                    Stream s = zFile.GetInputStream(ze);
                    readIndex = 0;
                    while (readIndex < readData.Length)
                    {
                        readIndex += s.Read(readData, readIndex, data.Length - readIndex);
                    }

                    for (int ii = 0; ii < BlockSize; ++ii)
                    {
                        Assert.AreEqual(data[ii], readData[ii]);
                    }
                }
                zFile.Close();
            }
        }

        /// <summary>
        /// Need to implement IsolatedStorage aware ZipFile
        /// </summary>
        //[TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void MakeLargeZipFile()
        {
            string tempFile = null;
            try
            {
                //            tempFile = Path.GetTempPath();
                tempFile = @"g:\\tmp";
            }
            catch (SecurityException)
            {
            }

            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            const int blockSize = 4096;

            byte[] data = new byte[blockSize];
            byte nextValue = 0;
            for (int i = 0; i < blockSize; ++i)
            {
                nextValue = ScatterValue(nextValue);
                data[i] = nextValue;
            }

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
            Console.WriteLine("Starting at {0}", DateTime.Now);
            try
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedStorageFileStream fs = store.CreateFile(tempFile))
                    {
                        ZipOutputStream zOut = new ZipOutputStream(fs);
                        zOut.SetLevel(4);
                        const int TargetFiles = 8100;
                        for (int i = 0; i < TargetFiles; ++i)
                        {
                            ZipEntry e = new ZipEntry(i.ToString());
                            e.CompressionMethod = CompressionMethod.Stored;

                            zOut.PutNextEntry(e);
                            for (int block = 0; block < 128; ++block)
                            {
                                zOut.Write(data, 0, blockSize);
                            }
                        }
                        zOut.Close();
                        fs.Close();

                        TestLargeZip(tempFile, TargetFiles);
                    }
                }
            }
            finally
            {
                Console.WriteLine("Starting at {0}", DateTime.Now);
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.CreateFile(tempFile);
                }
            }
        }


        /// <summary>
        /// Need to implement IsolatedStorage aware ZipFile
        /// </summary>
        //[TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void PartialStreamClosing()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            if (tempFile != null)
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                MakeZipFile(tempFile, new String[] { "Farriera", "Champagne", "Urban myth" }, 10, "Aha");

                using (ZipFile zipFile = new ZipFile(tempFile))
                {
                    Stream stream = zipFile.GetInputStream(0);
                    stream.Close();

                    stream = zipFile.GetInputStream(1);
                    zipFile.Close();
                }
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(tempFile);
                }
            }
        }
    }
}