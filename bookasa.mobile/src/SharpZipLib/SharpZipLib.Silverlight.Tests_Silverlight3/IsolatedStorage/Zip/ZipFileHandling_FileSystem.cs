using System;
using System.IO;
using System.IO.IsolatedStorage;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    /// <summary>
    /// Need to implement IsolatedStorage aware ZipFile
    /// </summary>
    //[TestClass]
    public class ZipFileHandling_IsolatedStorage : ZipBase
    {
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void UpdateCommentOnlyOnDisk()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                if (File.Exists(tempFile))
                {

                    store.DeleteFile(tempFile);
                }

                using (ZipFile testFile = ZipFile.Create(tempFile))
                {
                    testFile.BeginUpdate();
                    testFile.Add(new StringMemoryDataSource("Aha"), "No1", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("And so it goes"), "No2", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("No3"), "No3", CompressionMethod.Stored);
                    testFile.CommitUpdate();

                    Assert.IsTrue(testFile.TestArchive(true));
                }

                using (ZipFile testFile = new ZipFile(tempFile))
                {
                    Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("", testFile.ZipFileComment);

                    testFile.BeginUpdate(new DiskArchiveStorage(testFile, FileUpdateMode.Direct));
                    testFile.SetComment("Here is my comment");
                    testFile.CommitUpdate();

                    Assert.IsTrue(testFile.TestArchive(true));
                }

                using (ZipFile testFile = new ZipFile(tempFile))
                {
                    Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("Here is my comment", testFile.ZipFileComment);
                }
                store.DeleteFile(tempFile);

                // Variant using indirect updating.
                using (ZipFile testFile = ZipFile.Create(tempFile))
                {
                    testFile.BeginUpdate();
                    testFile.Add(new StringMemoryDataSource("Aha"), "No1", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("And so it goes"), "No2", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("No3"), "No3", CompressionMethod.Stored);
                    testFile.CommitUpdate();

                    Assert.IsTrue(testFile.TestArchive(true));
                }

                using (ZipFile testFile = new ZipFile(tempFile))
                {
                    Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("", testFile.ZipFileComment);

                    testFile.BeginUpdate();
                    testFile.SetComment("Here is my comment");
                    testFile.CommitUpdate();

                    Assert.IsTrue(testFile.TestArchive(true));
                }

                using (ZipFile testFile = new ZipFile(tempFile))
                {
                    Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("Here is my comment", testFile.ZipFileComment);
                }
                store.DeleteFile(tempFile);
            }
        }

        /// <summary>
        /// Check that adding too many entries is detected and handled
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void Zip64Entries()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            const int target = 65537;

            using (ZipFile zipFile = ZipFile.Create(Path.GetTempFileName()))
            {
                zipFile.BeginUpdate();

                for (int i = 0; i < target; ++i)
                {
                    ZipEntry ze = new ZipEntry(i.ToString());
                    ze.CompressedSize = 0;
                    ze.Size = 0;
                    zipFile.Add(ze);
                }
                zipFile.CommitUpdate();

                Assert.IsTrue(zipFile.TestArchive(true));
                Assert.AreEqual(target, zipFile.Count, "Incorrect number of entries stored");
            }
        }

        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void BasicEncryptionToDisk()
        {
            const string TestValue = "0001000";
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

            using (ZipFile f = ZipFile.Create(tempFile))
            {
                f.Password = "Hello";

                StringMemoryDataSource m = new StringMemoryDataSource(TestValue);
                f.BeginUpdate();
                f.Add(m, "a.dat");
                f.CommitUpdate();
            }

            using (ZipFile f = new ZipFile(tempFile))
            {
                f.Password = "Hello";
                Assert.IsTrue(f.TestArchive(true), "Archive test should pass");
            }

            using (ZipFile g = new ZipFile(tempFile))
            {
                g.Password = "Hello";
                ZipEntry ze = g[0];

                Assert.IsTrue(ze.IsCrypted, "Entry should be encrypted");
                using (StreamReader r = new StreamReader(g.GetInputStream(0)))
                {
                    string data = r.ReadToEnd();
                    Assert.AreEqual(TestValue, data);
                }
            }

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(tempFile);
            }
        }

        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void AddAndDeleteEntries()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            string addFile = Path.Combine(tempFile, "a.dat");
            MakeTempFile(addFile, 1);

            string addFile2 = Path.Combine(tempFile, "b.dat");
            MakeTempFile(addFile2, 259);

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

            using (ZipFile f = ZipFile.Create(tempFile))
            {
                f.BeginUpdate();
                f.Add(addFile);
                f.Add(addFile2);
                f.CommitUpdate();
                Assert.IsTrue(f.TestArchive(true));
            }

            using (ZipFile f = new ZipFile(tempFile))
            {
                Assert.AreEqual(2, f.Count);
                Assert.IsTrue(f.TestArchive(true));
                f.BeginUpdate();
                f.Delete(f[0]);
                f.CommitUpdate();
                Assert.AreEqual(1, f.Count);
                Assert.IsTrue(f.TestArchive(true));
            }
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(addFile);
                store.DeleteFile(addFile2);
                store.DeleteFile(tempFile);
            }
        }


        /// <summary>
        /// Simple round trip test for ZipFile class
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void RoundTrip()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

            try
            {
                MakeZipFile(tempFile, "", 10, 1024, "");

                using (ZipFile zipFile = new ZipFile(tempFile))
                {
                    foreach (ZipEntry e in zipFile)
                    {
                        Stream instream = zipFile.GetInputStream(e);
                        CheckKnownEntry(instream, 1024);
                    }
                    zipFile.Close();
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(tempFile);
                }
            }
        }

        [TestMethod]
        [Tag("Zip")]
        public void AddToEmptyArchive()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            string addFile = Path.Combine(tempFile, "a.dat");

            MakeTempFile(addFile, 1);
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                    using (ZipFile f = ZipFile.Create(tempFile))
                    {
                        f.BeginUpdate();
                        f.Add(addFile);
                        f.CommitUpdate();
                        Assert.AreEqual(1, f.Count);
                        Assert.IsTrue(f.TestArchive(true));
                    }

                    using (ZipFile f = new ZipFile(tempFile))
                    {
                        Assert.AreEqual(1, f.Count);
                        f.BeginUpdate();
                        f.Delete(f[0]); // failing here in 4 and 3 - fixed - one-off in zipfile
                        f.CommitUpdate();
                        Assert.AreEqual(0, f.Count);
                        Assert.IsTrue(f.TestArchive(true));
                        f.Close();
                    }

                    store.DeleteFile(tempFile);
                }
                finally
                {
                    store.DeleteFile(addFile);
                }
            }
        }



        [TestMethod]
        [Tag("Zip")]
        public void CreateEmptyArchive()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

            using (ZipFile f = ZipFile.Create(tempFile))
            {
                f.BeginUpdate();
                f.CommitUpdate();
                Assert.IsTrue(f.TestArchive(true));
                f.Close();
            }

            using (ZipFile f = new ZipFile(tempFile))
            {
                Assert.AreEqual(0, f.Count);
            }
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(tempFile);
            }
        }

        /// <summary>
        /// Check that ZipFile finds entries when its got a long comment
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void FindEntriesInArchiveWithLongComment()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
            string longComment = new String('A', 65535);
            MakeZipFile(tempFile, "", 1, 1, longComment);

            try
            {
                using (ZipFile zipFile = new ZipFile(tempFile))
                {
                    foreach (ZipEntry e in zipFile)
                    {
                        Stream instream = zipFile.GetInputStream(e);
                        CheckKnownEntry(instream, 1);
                    }
                    zipFile.Close();
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(tempFile);
                }
            }
        }


        /// <summary>
        /// Check that ZipFile doesnt find entries when there is more than 64K of data at the end.
        /// </summary>
        /// <remarks>
        /// This may well be flawed but is the current behaviour.
        /// </remarks>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void FindEntriesInArchiveExtraData()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");
            bool fails;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                string longComment = new String('A', 65535);
                FileStream tempStream = store.CreateFile(tempFile);
                MakeZipFile(tempStream, false, "", 1, 1, longComment);

                tempStream.WriteByte(85);
                tempStream.Close();

                fails = false;
                try
                {
                    using (ZipFile zipFile = new ZipFile(tempFile))
                    {
                        foreach (ZipEntry e in zipFile)
                        {
                            Stream instream = zipFile.GetInputStream(e);
                            CheckKnownEntry(instream, 1);
                        }
                        zipFile.Close();
                    }
                }
                catch
                {
                    fails = true;
                }

                store.DeleteFile(tempFile);
            }
            Assert.IsTrue(fails, "Currently zip file wont be found");
        }

        /// <summary>
        /// Test ZipFile Find method operation
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void FindEntry()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
            MakeZipFile(tempFile, new string[] { "Farriera", "Champagne", "Urban myth" }, 10, "Aha");

            using (ZipFile zipFile = new ZipFile(tempFile))
            {
                Assert.AreEqual(3, zipFile.Count, "Expected 1 entry");

                int testIndex = zipFile.FindEntry("Farriera", false);
                Assert.AreEqual(0, testIndex, "Case sensitive find failure");
                Assert.IsTrue(string.Compare(zipFile[testIndex].Name, "Farriera", StringComparison.InvariantCulture) == 0);


                testIndex = zipFile.FindEntry("farriera", false);
                Assert.AreEqual(-1, testIndex, "Case sensitive find failure");


                testIndex = zipFile.FindEntry("farriera", true);
                Assert.AreEqual(0, testIndex, "Case insensitive find failure");
                Assert.IsTrue(string.Compare(zipFile[testIndex].Name, "Farriera", StringComparison.InvariantCultureIgnoreCase) == 0);

                testIndex = zipFile.FindEntry("urban mYTH", false);
                Assert.AreEqual(-1, testIndex, "Case sensitive find failure");

                testIndex = zipFile.FindEntry("urban mYTH", true);
                Assert.AreEqual(2, testIndex, "Case insensitive find failure");
                Assert.IsTrue(string.Compare(zipFile[testIndex].Name, "urban mYTH", StringComparison.InvariantCultureIgnoreCase) == 0);

                testIndex = zipFile.FindEntry("Champane.", false);
                Assert.AreEqual(-1, testIndex, "Case sensitive find failure");

                testIndex = zipFile.FindEntry("Champane.", true);
                Assert.AreEqual(-1, testIndex, "Case insensitive find failure");

                zipFile.Close();
            }
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(tempFile);
            }
        }
        /// <summary>
        /// Check that ZipFile class handles no entries in zip file
        /// Need to implement IsolatedStorage aware ZipFile
        /// </summary>
        //[TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void HandlesNoEntries()
        {
            string tempFile = GetTempFilePath();
            Assert.IsNotNull(tempFile, "No permission to execute this test?");

            tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
            MakeZipFile(tempFile, "", 0, 1, "Aha");

            using (ZipFile zipFile = new ZipFile(tempFile))
            {
                Assert.AreEqual(0, zipFile.Count);
                zipFile.Close();
            }
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                store.DeleteFile(tempFile);
            }
        }


    }
}