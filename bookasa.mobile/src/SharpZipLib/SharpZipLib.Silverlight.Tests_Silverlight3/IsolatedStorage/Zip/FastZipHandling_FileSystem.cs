using System.IO;
using System.IO.IsolatedStorage;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    
    /// <summary>
    /// TODO: need to implement an IsolatedStorage aware ZipFile/FastZip/EntryFactory etc etc
    /// </summary>
    //[TestClass]
    public class FastZipHandling_IsolatedStorage : ZipBase
    {
        [TestMethod]
        [Tag("Zip")]
        [Tag("CreatesTempFile")]
        public void Basics()
        {
            const string tempName1 = "a(1).dat";

            MemoryStream target = new MemoryStream();

            string tempFilePath = GetTempFilePath();
            Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

            string addFile = Path.Combine(tempFilePath, tempName1);
            MakeTempFile(addFile, 1);

            try
            {
                FastZip fastZip = new FastZip();
                fastZip.CreateZip(target, tempFilePath, false, @"a\(1\)\.dat", null); // failing here in 4

                MemoryStream archive = new MemoryStream(target.ToArray());
                using (ZipFile zf = new ZipFile(archive))
                {
                    Assert.AreEqual(1, zf.Count);
                    ZipEntry entry = zf[0];
                    Assert.AreEqual(tempName1, entry.Name);
                    Assert.AreEqual(1, entry.Size);
                    Assert.IsTrue(zf.TestArchive(true));

                    zf.Close();
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
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
            string targetDir;
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                targetDir = Path.Combine(tempFilePath, ZipTempDir + @"\floyd");
                using (IsolatedStorageFileStream fs = store.CreateFile(name))
                {
                    using (ZipOutputStream zOut = new ZipOutputStream(fs))
                    {
                        zOut.PutNextEntry(new ZipEntry("floyd/"));
                    }
                }

                FastZip fastZip = new FastZip();
                fastZip.CreateEmptyDirectories = true;
                fastZip.ExtractZip(name, targetDir, "zz");

                store.DeleteFile(name);
            }
            Assert.IsTrue(Directory.Exists(targetDir), "Empty directory should be created");
        }

        [TestMethod]
        [Tag("Zip")]
        public void Encryption()
        {
            const string tempName1 = "a.dat";

            MemoryStream target = new MemoryStream();

            string tempFilePath = GetTempFilePath();
            Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

            string addFile = Path.Combine(tempFilePath, tempName1);
            MakeTempFile(addFile, 1);

            try
            {
                FastZip fastZip = new FastZip();
                fastZip.Password = "Ahoy";

                fastZip.CreateZip(target, tempFilePath, false, @"a\.dat", null); // failing here in 4

                MemoryStream archive = new MemoryStream(target.ToArray());
                using (ZipFile zf = new ZipFile(archive))
                {
                    zf.Password = "Ahoy";
                    Assert.AreEqual(1, zf.Count);
                    ZipEntry entry = zf[0];
                    Assert.AreEqual(tempName1, entry.Name);
                    Assert.AreEqual(1, entry.Size);
                    Assert.IsTrue(zf.TestArchive(true));
                    Assert.IsTrue(entry.IsCrypted);
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
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
            FastZip fastZip = new FastZip();
            string tempFilePath = GetTempFilePath();
            Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

            string addFile = Path.Combine(tempFilePath, "test.zip");
            try
            {
                fastZip.CreateZip(addFile, @"z:\doesnt exist", false, null);
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(addFile);
                }
            }
        }

        [TestMethod]
        [Tag("Zip")]
        public void UnicodeText()
        {
            FastZip zippy = new FastZip();
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

                using (ZipFile z = new ZipFile(archive))
                {
                    Assert.AreEqual(1, z.Count);
                    Assert.IsTrue(z[0].IsUnicodeText);
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
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
            FastZip fastZip = new FastZip();
            string tempFilePath = GetTempFilePath();
            Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

            string addFile = Path.Combine(tempFilePath, "test.zip");
            try
            {
                fastZip.ExtractZip(addFile, @"z:\doesnt exist", null);
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(addFile);
                }
            }
        }


        ///// <summary>
        ///// This test is ignored as it fails in the origin tests as well
        ///// </summary>
        //[Test,Ignore] 
        //[Tag("Zip")]
        //public void ReadingOfLockedDataFiles()
        //{
        //    const string tempName1 = "a.dat";

        //    MemoryStream target = new MemoryStream();

        //    string tempFilePath = GetTempFilePath();
        //    Assert.IsNotNull(tempFilePath, "No permission to execute this test?");

        //    string addFile = Path.Combine(tempFilePath, tempName1);
        //    MakeTempFile(addFile, 1);

        //    try
        //    {
        //        FastZip fastZip = new FastZip();

        //        using (File.Open(addFile, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
        //        {
        //            fastZip.CreateZip(target, tempFilePath, false, @"a\.dat", null);

        //            MemoryStream archive = new MemoryStream(target.ToArray());
        //            using (ZipFile zf = new ZipFile(archive))
        //            {
        //                Assert.AreEqual(1, zf.Count);
        //                ZipEntry entry = zf[0];
        //                Assert.AreEqual(tempName1, entry.Name);
        //                Assert.AreEqual(1, entry.Size);
        //                Assert.IsTrue(zf.TestArchive(true));

        //                zf.Close();
        //            }
        //        }
        //    }
        //    finally
        //    {
        //        store.Delete(tempName1);
        //    }
        //}

        [TestMethod]
        [Tag("Zip")]
        public void NonAsciiPasswords()
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
                FastZip fastZip = new FastZip();
                fastZip.Password = password;

                fastZip.CreateZip(target, tempFilePath, false, @"a\.dat", null); // failing here in 4

                MemoryStream archive = new MemoryStream(target.ToArray());
                using (ZipFile zf = new ZipFile(archive))
                {
                    zf.Password = password;
                    Assert.AreEqual(1, zf.Count);
                    ZipEntry entry = zf[0];
                    Assert.AreEqual(tempName1, entry.Name);
                    Assert.AreEqual(1, entry.Size);
                    Assert.IsTrue(zf.TestArchive(true));
                    Assert.IsTrue(entry.IsCrypted);
                }
            }
            finally
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(tempName1);
                }
            }
        }
    }
}