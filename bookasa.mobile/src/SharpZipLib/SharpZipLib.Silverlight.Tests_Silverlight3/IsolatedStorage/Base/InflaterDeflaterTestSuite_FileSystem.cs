using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Security;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Base
{
    [TestClass]
    public class InflaterDeflaterTestSuite_IsolatedStorage
    {

        [TestMethod]
        [Tag("Base")]
        public void CloseInflatorWithNestedUsing()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string tempFile = Environment.TickCount.ToString();
                store.CreateDirectory(tempFile);

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                using (IsolatedStorageFileStream diskFile = store.CreateFile(tempFile))
                using (DeflaterOutputStream deflator = new DeflaterOutputStream(diskFile))
                using (StreamWriter textWriter = new StreamWriter(deflator))
                {
                    textWriter.Write("Hello");
                    textWriter.Flush();
                }

                using (IsolatedStorageFileStream diskFile = store.OpenFile(tempFile, FileMode.Open))
                using (InflaterInputStream deflator = new InflaterInputStream(diskFile))
                using (StreamReader textReader = new StreamReader(deflator))
                {
                    char[] buffer = new char[5];
                    int readCount = textReader.Read(buffer, 0, 5);
                    Assert.AreEqual(5, readCount);

                    StringBuilder b = new StringBuilder();
                    b.Append(buffer);
                    Assert.AreEqual("Hello", b.ToString());

                }

                store.CreateFile(tempFile);
            }
        }

        [TestMethod]
        [Tag("Base")]
        public void CloseDeflatorWithNestedUsing()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string tempFile = Environment.TickCount.ToString();
                store.CreateDirectory(tempFile);
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                using (IsolatedStorageFileStream diskFile = store.CreateFile(tempFile))
                using (DeflaterOutputStream deflator = new DeflaterOutputStream(diskFile))
                using (StreamWriter txtFile = new StreamWriter(deflator))
                {
                    txtFile.Write("Hello");
                    txtFile.Flush();
                }

                store.CreateFile(tempFile);

            }
        }


        [TestMethod]
        [Tag("Base")]
        public void CloseInflatorWithNestedUsing2()
        {

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {


                string tempFile = Guid.NewGuid().ToString("N");
                store.CreateDirectory(tempFile);

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                using (var diskFile = store.CreateFile(tempFile))
                using (DeflaterOutputStream deflator = new DeflaterOutputStream(diskFile))
                using (StreamWriter textWriter = new StreamWriter(deflator))
                {
                    textWriter.Write("Hello");
                    textWriter.Flush();
                }

                using (var diskFile = store.OpenFile(tempFile, FileMode.Open, FileAccess.Read))
                using (InflaterInputStream deflator = new InflaterInputStream(diskFile))
                using (StreamReader textReader = new StreamReader(deflator))
                {
                    char[] buffer = new char[5];
                    int readCount = textReader.Read(buffer, 0, 5);
                    Assert.AreEqual(5, readCount);

                    StringBuilder b = new StringBuilder();
                    b.Append(buffer);
                    Assert.AreEqual("Hello", b.ToString());
                }

                store.DeleteFile(tempFile);
                store.DeleteDirectory(Path.GetDirectoryName(tempFile));
            }

        }

        [TestMethod]
        [Tag("Base")]
        public void CloseDeflatorWithNestedUsing2()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {


                string tempFile = Guid.NewGuid().ToString("N");

                store.CreateDirectory(tempFile);

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                using (var diskFile = store.CreateFile(tempFile))
                using (DeflaterOutputStream deflator = new DeflaterOutputStream(diskFile))
                using (StreamWriter txtFile = new StreamWriter(deflator))
                {
                    txtFile.Write("Hello");
                    txtFile.Flush();


                }
                store.DeleteFile(tempFile);
                store.DeleteDirectory(Path.GetDirectoryName(tempFile));
            }
        }
    }
}
