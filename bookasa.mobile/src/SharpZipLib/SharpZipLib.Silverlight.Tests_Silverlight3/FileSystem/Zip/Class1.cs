using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Reflection;
//using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Text;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Tests.TestSupport;




using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Salient.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip2
{

    

    [TestClass]
    public class GeneralHandling_FileSystem : ZipBase
    {
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void PartialStreamClosing()
        {
            string tempFile = GetTempFilePath();

            if (tempFile != null)
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                MakeZipFile(tempFile, new String[] { "Farriera", "Champagne", "Urban myth" }, 10, "Aha");

                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
                    {
                        Stream stream = zipFile.GetInputStream(0);
                        stream.Close();

                        stream = zipFile.GetInputStream(1);
                        zipFile.Close();
                    }
                    store.DeleteFile(tempFile);
                }
            }
        }
    }
    [TestClass]
    public class ZipFileHandling_FileSystem : ZipBase
    {
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void UpdateCommentOnlyOnDisk()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                

                string tempFile = GetTempFilePath();
                Assert.IsNotNull(tempFile, "No permission to execute this test?");

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                if (store.FileExists(tempFile))
                {
                    store.DeleteFile(tempFile);
                }

                using (IsolatedZipFile testFile = IsolatedZipFile.Create(tempFile))
                {
                    testFile.BeginUpdate();
                    testFile.Add(new StringMemoryDataSource("Aha"), "No1", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("And so it goes"), "No2", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("No3"), "No3", CompressionMethod.Stored);
                    testFile.CommitUpdate();

                    // TODO: Testing is not yet ported -  Assert.IsTrue(testFile.TestArchive(true));
                }

                using (IsolatedZipFile testFile = new IsolatedZipFile(tempFile))
                {
                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("", testFile.ZipFileComment);

                    testFile.BeginUpdate(new IsolatedDiskArchiveStorage(testFile, FileUpdateMode.Direct));
                    testFile.SetComment("Here is my comment");
                    testFile.CommitUpdate();

                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                }

                using (IsolatedZipFile testFile = new IsolatedZipFile(tempFile))
                {
                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("Here is my comment", testFile.ZipFileComment);
                }
                store.DeleteFile(tempFile);

                // Variant using indirect updating.
                using (IsolatedZipFile testFile = IsolatedZipFile.Create(tempFile))
                {
                    testFile.BeginUpdate();
                    testFile.Add(new StringMemoryDataSource("Aha"), "No1", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("And so it goes"), "No2", CompressionMethod.Stored);
                    testFile.Add(new StringMemoryDataSource("No3"), "No3", CompressionMethod.Stored);
                    testFile.CommitUpdate();

                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                }

                using (IsolatedZipFile testFile = new IsolatedZipFile(tempFile))
                {
                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("", testFile.ZipFileComment);

                    testFile.BeginUpdate();
                    testFile.SetComment("Here is my comment");
                    testFile.CommitUpdate();

                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                }

                using (IsolatedZipFile testFile = new IsolatedZipFile(tempFile))
                {
                    // TODO: Testing is not yet ported - Assert.IsTrue(testFile.TestArchive(true));
                    Assert.AreEqual("Here is my comment", testFile.ZipFileComment);
                }

                store.DeleteFile(tempFile);
                store.DeleteDirectory(Path.GetDirectoryName(tempFile));

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

            using (IsolatedZipFile zipFile = IsolatedZipFile.Create(Path.GetTempFileName()))
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

                // TODO: Testing is not yet ported - Assert.IsTrue(zipFile.TestArchive(true));
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
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedZipFile f = IsolatedZipFile.Create(tempFile))
                {
                    f.Password = "Hello";

                    StringMemoryDataSource m = new StringMemoryDataSource(TestValue);
                    f.BeginUpdate();
                    f.Add(m, "a.dat");
                    f.CommitUpdate();
                }

                using (IsolatedZipFile f = new IsolatedZipFile(tempFile))
                {
                    f.Password = "Hello";
                    // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true), "Archive test should pass");
                }

                using (IsolatedZipFile g = new IsolatedZipFile(tempFile))
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

                store.DeleteFile(tempFile);
            }
        }

        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void AddAndDeleteEntries()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string tempFile = GetTempFilePath();
                Assert.IsNotNull(tempFile, "No permission to execute this test?");

                string addFile = Path.Combine(tempFile, "a.dat");
                MakeTempFile(addFile, 1);

                string addFile2 = Path.Combine(tempFile, "b.dat");
                MakeTempFile(addFile2, 259);

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                using (IsolatedZipFile f = IsolatedZipFile.Create(tempFile))
                {
                    f.BeginUpdate();
                    f.Add(addFile);
                    f.Add(addFile2);
                    f.CommitUpdate();
                    // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
                }

                using (IsolatedZipFile f = new IsolatedZipFile(tempFile))
                {
                    Assert.AreEqual(2, f.Count);
                    // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
                    f.BeginUpdate();
                    f.Delete(f[0]);
                    f.CommitUpdate();
                    Assert.AreEqual(1, f.Count);
                    // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
                }

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
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    MakeZipFile(tempFile, "", 10, 1024, "");

                    using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
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

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                MakeTempFile(addFile, 1);

                try
                {
                    tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                    using (IsolatedZipFile f = IsolatedZipFile.Create(tempFile))
                    {
                        f.BeginUpdate();
                        f.Add(addFile);
                        f.CommitUpdate();
                        Assert.AreEqual(1, f.Count);
                        // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
                    }

                    using (IsolatedZipFile f = new IsolatedZipFile(tempFile))
                    {
                        Assert.AreEqual(1, f.Count);
                        f.BeginUpdate();
                        f.Delete(f[0]); // failing here in 4 and 3 - fixed - one-off in zipfile
                        f.CommitUpdate();
                        Assert.AreEqual(0, f.Count);
                        // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
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

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");

                using (IsolatedZipFile f = IsolatedZipFile.Create(tempFile))
                {
                    f.BeginUpdate();
                    f.CommitUpdate();
                    // TODO: Testing is not yet ported - Assert.IsTrue(f.TestArchive(true));
                    f.Close();
                }

                using (IsolatedZipFile f = new IsolatedZipFile(tempFile))
                {
                    Assert.AreEqual(0, f.Count);
                }

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
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                try
                {
                    using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
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
                    using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
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
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                MakeZipFile(tempFile, new string[] { "Farriera", "Champagne", "Urban myth" }, 10, "Aha");

                using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
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
                store.DeleteFile(tempFile);
            }
        }
        /// <summary>
        /// Check that ZipFile class handles no entries in zip file
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void HandlesNoEntries()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                string tempFile = GetTempFilePath();
                Assert.IsNotNull(tempFile, "No permission to execute this test?");

                tempFile = Path.Combine(tempFile, "SharpZipTest.Zip");
                MakeZipFile(tempFile, "", 0, 1, "Aha");

                using (IsolatedZipFile zipFile = new IsolatedZipFile(tempFile))
                {
                    Assert.AreEqual(0, zipFile.Count);
                    zipFile.Close();
                }

                store.DeleteFile(tempFile);
            }
        }


    }

    [TestClass]
    public class WindowsNameTransformHandling_FileSystem : TransformBase
    {
        [TestMethod]
        public void BasicDirectories()
        {
            WindowsNameTransform wnt = new WindowsNameTransform();
            wnt.TrimIncomingPaths = false;

            string tutu = Path.GetDirectoryName("\\bogan\\ping.txt");
            TestDirectory(wnt, "d/", "d");
            TestDirectory(wnt, "d", "d");
            TestDirectory(wnt, "absolute/file2", @"absolute\file2");

            const string BaseDir1 = @"Dir";
            wnt.BaseDirectory = BaseDir1;

            TestDirectory(wnt, "talofa", Path.Combine(BaseDir1, "talofa"));

            const string BaseDir2 = @"Dir\";
            wnt.BaseDirectory = BaseDir2;

            TestDirectory(wnt, "talofa", Path.Combine(BaseDir2, "talofa"));
        }
    }

    [TestClass]
    public class IsolatedFastZipHandling_FileSystem : ZipBase
    {
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void Basics()
        {
            const string tempName1 = "a(1).dat";
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                MemoryStream target = new MemoryStream();

                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                string addFile = Path.Combine(tempFilePath, tempName1);
                MakeTempFile(addFile, 1);

                try
                {
                    IsolatedFastZip fastZip = new IsolatedFastZip();
                    fastZip.CreateZip(target, tempFilePath, false, @"a\(1\)\.dat", null); // failing here in 4

                    MemoryStream archive = new MemoryStream(target.ToArray());
                    using (IsolatedZipFile zf = new IsolatedZipFile(archive))
                    {
                        Assert.AreEqual(1, zf.Count);
                        ZipEntry entry = zf[0];
                        Assert.AreEqual(tempName1, entry.Name);
                        Assert.AreEqual(1, entry.Size);
                        // TODO: Testing is not yet ported - Assert.IsTrue(zf.TestArchive(true));

                        zf.Close();
                    }
                }
                finally
                {
                    store.DeleteFile(tempName1);
                }
            }
        }

        const string ZipTempDir = "SharpZipLibTest";

        void EnsureTestDirectoryIsEmpty(string baseDir)
        {
            string name = Path.Combine(baseDir, ZipTempDir);

            if (Directory.Exists(name))
            {
                Directory.Delete(name, true);
            }

            Directory.CreateDirectory(name);
        }

        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void ExtractEmptyDirectories()
        {
            string tempFilePath = GetTempFilePath();
            Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

            string name = Path.Combine(tempFilePath, "x.zip");

            EnsureTestDirectoryIsEmpty(tempFilePath);

            string targetDir = Path.Combine(tempFilePath, ZipTempDir + @"\floyd");
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var fs = store.CreateFile(name))
                {
                    using (ZipOutputStream zOut = new ZipOutputStream(fs))
                    {
                        zOut.PutNextEntry(new ZipEntry("floyd/"));
                    }
                }

                IsolatedFastZip fastZip = new IsolatedFastZip();
                fastZip.CreateEmptyDirectories = true;
                fastZip.ExtractZip(name, targetDir, "zz");

                store.DeleteFile(name);
                Assert.IsTrue(Directory.Exists(targetDir), "Empty directory should be created");

            }
        }

        [TestMethod]
        [Tag("Zip")]
        public void Encryption()
        {
            const string tempName1 = "a.dat";
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                MemoryStream target = new MemoryStream();

                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                string addFile = Path.Combine(tempFilePath, tempName1);
                MakeTempFile(addFile, 1);

                try
                {
                    IsolatedFastZip fastZip = new IsolatedFastZip();
                    fastZip.Password = "Ahoy";

                    fastZip.CreateZip(target, tempFilePath, false, @"a\.dat", null); // failing here in 4

                    MemoryStream archive = new MemoryStream(target.ToArray());
                    using (IsolatedZipFile zf = new IsolatedZipFile(archive))
                    {
                        zf.Password = "Ahoy";
                        Assert.AreEqual(1, zf.Count);
                        ZipEntry entry = zf[0];
                        Assert.AreEqual(tempName1, entry.Name);
                        Assert.AreEqual(1, entry.Size);
                        // TODO: Testing is not yet ported - Assert.IsTrue(zf.TestArchive(true));
                        Assert.IsTrue(entry.IsCrypted);
                    }
                }
                finally
                {
                    store.DeleteFile(tempName1);
                }
            }
        }

        [TestMethod]
        [Tag("Zip")]
        [ExpectedException(typeof(DirectoryNotFoundException))]
        public void CreateExceptions()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedFastZip fastZip = new IsolatedFastZip();
                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                string addFile = Path.Combine(tempFilePath, "test.zip");
                try
                {
                    fastZip.CreateZip(addFile, @"z:\doesnt exist", false, null);
                }
                finally
                {
                    store.DeleteFile(addFile);
                }
            }
        }

        [TestMethod]
        [Tag("Zip")]
        public void UnicodeText()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedFastZip zippy = new IsolatedFastZip();
                ZipEntryFactory factory = new ZipEntryFactory();
                factory.IsUnicodeText = true;
                zippy.EntryFactory = factory;

                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                const string tempName1 = "a.dat";
                string addFile = Path.Combine(tempFilePath, tempName1);
                MakeTempFile(addFile, 1);

                try
                {
                    MemoryStream target = new MemoryStream();
                    zippy.CreateZip(target, tempFilePath, false, tempName1, null); // failing here in 4

                    MemoryStream archive = new MemoryStream(target.ToArray());

                    using (IsolatedZipFile z = new IsolatedZipFile(archive))
                    {
                        Assert.AreEqual(1, z.Count);
                        Assert.IsTrue(z[0].IsUnicodeText);
                    }
                }
                finally
                {
                    store.DeleteFile(addFile);
                }
            }
        }

        [TestMethod]
        [Tag("Zip")]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ExtractExceptions()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                IsolatedFastZip fastZip = new IsolatedFastZip();
                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                string addFile = Path.Combine(tempFilePath, "test.zip");
                try
                {
                    fastZip.ExtractZip(addFile, @"z:\doesnt exist", null);
                }
                finally
                {
                    store.DeleteFile(addFile);
                }
            }
        }




        [TestMethod]
        [Tag("Zip")]
        public void NonAsciiPasswords()
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                const string tempName1 = "a.dat";

                MemoryStream target = new MemoryStream();

                string tempFilePath = GetTempFilePath();
                Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

                string addFile = Path.Combine(tempFilePath, tempName1);
                MakeTempFile(addFile, 1);

                string password = "abc\u0066\u0393";
                try
                {
                    IsolatedFastZip fastZip = new IsolatedFastZip();
                    fastZip.Password = password;

                    fastZip.CreateZip(target, tempFilePath, false, @"a\.dat", null); // failing here in 4

                    MemoryStream archive = new MemoryStream(target.ToArray());
                    using (IsolatedZipFile zf = new IsolatedZipFile(archive))
                    {
                        zf.Password = password;
                        Assert.AreEqual(1, zf.Count);
                        ZipEntry entry = zf[0];
                        Assert.AreEqual(tempName1, entry.Name);
                        Assert.AreEqual(1, entry.Size);
                        // TODO: Testing is not yet ported - Assert.IsTrue(zf.TestArchive(true));
                        Assert.IsTrue(entry.IsCrypted);
                    }
                }
                finally
                {
                    store.DeleteFile(tempName1);
                }
            }
        }
    }

    public class TransformBase : ZipBase
    {
        protected void TestFile(INameTransform t, string original, string expected)
        {
            string transformed = t.TransformFile(original);
            Assert.AreEqual(expected, transformed, "Should be equal");
        }

        protected void TestDirectory(INameTransform t, string original, string expected)
        {
            string transformed = t.TransformDirectory(original);
            Assert.AreEqual(expected, transformed, "Should be equal");
        }
    }

    #region ZipBase
    public class ZipBase
    {
        static protected string GetTempFilePath()
        {

            string result = Guid.NewGuid().ToString("N");
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
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
            using (var fs = store.CreateFile(name))
            {
                byte[] buffer = new byte[4096];
                while (size > 0)
                {
                    fs.Write(buffer, 0, Math.Min(size, buffer.Length));
                    size -= buffer.Length;
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
            using (var fs = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
            {
                fs.Write(data, 0, data.Length);
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
                using (var fs = store.CreateFile(name))
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
            using (var fs = store.CreateFile(name))
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

    #endregion

    #region Local Support Classes
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

    class MemoryDataSource : IStaticDataSource
    {
        #region Constructors
        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        /// <param name="data">The data to provide.</param>
        public MemoryDataSource(byte[] data)
        {
            data_ = data;
        }
        #endregion

        #region IDataSource Members

        /// <summary>
        /// Get a Stream for this <see cref="IStaticDataSource"/>
        /// </summary>
        /// <returns>Returns a <see cref="Stream"/></returns>
        public Stream GetSource()
        {
            return new MemoryStream(data_);
        }
        #endregion

        #region Instance Fields
        readonly byte[] data_;
        #endregion
    }

    class StringMemoryDataSource : MemoryDataSource
    {
        public StringMemoryDataSource(string data)
            : base(Encoding.UTF8.GetBytes(data))
        {
        }
    }
    #endregion
}
