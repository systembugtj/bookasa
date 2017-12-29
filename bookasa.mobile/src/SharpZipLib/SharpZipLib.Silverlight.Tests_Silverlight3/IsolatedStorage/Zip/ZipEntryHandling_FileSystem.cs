using System;
using System.Reflection;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ICSharpCode.SharpZipLib.Tests.FileSystem.Zip
{
    /// <summary>
    /// Need to implement IsolatedStorage aware ZipFile
    /// </summary>
    //[TestClass]
    public class ZipEntryHandling_IsolatedStorage : ZipBase
    {
        /// <summary>
        /// Test that obsolete copy constructor works correctly.
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        public void Copying()
        {
            long testCrc = 3456;
            long testSize = 99874276;
            long testCompressedSize = 72347;
            byte[] testExtraData = new byte[] { 0x00, 0x01, 0x00, 0x02, 0x0EF, 0xFE };
            string testName = "Namu";
            int testFlags = 4567;
            long testDosTime = 23434536;
            CompressionMethod testMethod = CompressionMethod.Deflated;

            string testComment = "A comment";

            ZipEntry source = new ZipEntry(testName);
            source.Crc = testCrc;
            source.Comment = testComment;
            source.Size = testSize;
            source.CompressedSize = testCompressedSize;
            source.ExtraData = testExtraData;
            source.Flags = testFlags;
            source.DosTime = testDosTime;
            source.CompressionMethod = testMethod;

#pragma warning disable 0618
            ZipEntry clone = new ZipEntry(source);
#pragma warning restore

            PiecewiseCompare(source, clone);
        }
        void PiecewiseCompare(ZipEntry lhs, ZipEntry rhs)
        {
            Type entryType = typeof(ZipEntry);
            BindingFlags binding = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

            FieldInfo[] fields = entryType.GetFields(binding);

            //Assert.Greater(fields.Length, 8, "Failed to find fields");
            Assert.IsTrue(fields.Length > 8, "Failed to find fields");

            foreach (FieldInfo info in fields)
            {
                object lValue = info.GetValue(lhs);
                object rValue = info.GetValue(rhs);

                Assert.AreEqual(lValue, rValue);
            }
        }
        /// <summary>
        /// Check that cloned entries are correct.
        /// </summary>
        [TestMethod]
        [Tag("Zip")]
        public void Cloning()
        {
            long testCrc = 3456;
            long testSize = 99874276;
            long testCompressedSize = 72347;
            byte[] testExtraData = new byte[] { 0x00, 0x01, 0x00, 0x02, 0x0EF, 0xFE };
            string testName = "Namu";
            int testFlags = 4567;
            long testDosTime = 23434536;
            CompressionMethod testMethod = CompressionMethod.Deflated;

            string testComment = "A comment";

            ZipEntry source = new ZipEntry(testName);
            source.Crc = testCrc;
            source.Comment = testComment;
            source.Size = testSize;
            source.CompressedSize = testCompressedSize;
            source.ExtraData = testExtraData;
            source.Flags = testFlags;
            source.DosTime = testDosTime;
            source.CompressionMethod = testMethod;

            ZipEntry clone = (ZipEntry)source.Clone();

            // Check values against originals
            Assert.AreEqual(testName, clone.Name, "Cloned name mismatch");
            Assert.AreEqual(testCrc, clone.Crc, "Cloned crc mismatch");
            Assert.AreEqual(testComment, clone.Comment, "Cloned comment mismatch");
            Assert.AreEqual(testExtraData, clone.ExtraData, "Cloned Extra data mismatch");
            Assert.AreEqual(testSize, clone.Size, "Cloned size mismatch");
            Assert.AreEqual(testCompressedSize, clone.CompressedSize, "Cloned compressed size mismatch");
            Assert.AreEqual(testFlags, clone.Flags, "Cloned flags mismatch");
            Assert.AreEqual(testDosTime, clone.DosTime, "Cloned DOSTime mismatch");
            Assert.AreEqual(testMethod, clone.CompressionMethod, "Cloned Compression method mismatch");

            // Check against source
            PiecewiseCompare(source, clone);
        }
    }
}