#region Microsoft Public License (Ms-PL)
//  Microsoft Public License (Ms-PL)
// 
// This license governs use of the accompanying software. If you use the 
// software, you accept this license. If you do not accept the license, do not 
// use the software.
// 
// 1. Definitions
// 
// The terms "reproduce," "reproduction," "derivative works," and "distribution" 
// have the same meaning here as under U.S. copyright law.
// 
// A "contribution" is the original software, or any additions or changes to 
// the software.
// 
// A "contributor" is any person that distributes its contribution under this 
// license.
// 
// "Licensed patents" are a contributor's patent claims that read directly on 
// its contribution.
// 
// 2. Grant of Rights
// 
// (A) Copyright Grant- Subject to the terms of this license, including the 
// license conditions and limitations in section 3, each contributor grants you 
// a non-exclusive, worldwide, royalty-free copyright license to reproduce its 
// contribution, prepare derivative works of its contribution, and distribute its 
// contribution or any derivative works that you create.
// 
// (B) Patent Grant- Subject to the terms of this license, including the 
// license conditions and limitations in section 3, each contributor grants 
// you a non-exclusive, worldwide, royalty-free license under its licensed 
// patents to make, have made, use, sell, offer for sale, import, and/or 
// otherwise dispose of its contribution in the software or derivative works of 
// the contribution in the software.
// 
// 3. Conditions and Limitations
// 
// (A) No Trademark License- This license does not grant you rights to use 
// any contributors' name, logo, or trademarks.
// 
// (B) If you bring a patent claim against any contributor over patents that 
// you claim are infringed by the software, your patent license from such 
// contributor to the software ends automatically.
// 
// (C) If you distribute any portion of the software, you must retain all 
// copyright, patent, trademark, and attribution notices that are present in 
// the software.
// 
// (D) If you distribute any portion of the software in source code form, 
// you may do so only under this license by including a complete copy of 
// this license with your distribution. If you distribute any portion of the 
// software in compiled or object code form, you may only do so under a license
// that complies with this license.
// 
// (E) The software is licensed "as-is." You bear the risk of using it. The 
// contributors give no express warranties, guarantees or conditions. You may 
// have additional consumer rights under your local laws which this license 
// cannot change. To the extent permitted under your local laws, the 
// contributors exclude the implied warranties of merchantability, fitness 
// for a particular purpose and non-infringement.  
// 
// This Software is Copyright (c)2009 by LigoSoftware.com
//
#endregion
using System;
using System.IO;
using Crimson.Catalog.ID3;
using Crimson.Catalog.ID3.ID3v2Frames;
using Crimson.Catalog.ID3.ID3v2Frames.ArrayFrames;
using Crimson.Catalog.ID3.ID3v2Frames.BinaryFrames;
using Crimson.Catalog.ID3.ID3v2Frames.OtherFrames;
using Crimson.Catalog.ID3.ID3v2Frames.StreamFrames;
using Crimson.Catalog.ID3.ID3v2Frames.TextFrames;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crimson.Catalog.WebSite.Tests
{
    /// <summary>
    ///This is a test class for ID3v2Test and is intended
    ///to contain all ID3v2Test Unit Tests
    ///</summary>
    [TestClass()]
    public class ID3v2Test
    {
        // The filename to test.  This should change to whereever you have it mapped
        public const string filePath = @"C:\Users\drcarver\Documents\Visual Studio 2008\Projects\crimsonreader\EBookReader\trunk\LigoSoftware.Tests\Data\AudioBooks\AndreNorton\01 Perfumed Planet.mp3";

        public TestContext testContextInstance { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for VersionInfo
        ///</summary>
        [TestMethod()]
        public void VersionInfoTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(new Version("2.3.0"), target.VersionInfo);
        }

        /// <summary>
        ///A test for TextWithLanguageFrames
        ///</summary>
        [TestMethod()]
        public void TextWithLanguageFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(4, target.TextWithLanguageFrames.Count);
        }

        /// <summary>
        ///A test for TextFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void TextFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(12, target.TextFrames.Count);
        }

        /// <summary>
        ///A test for TermOfUseFrames
        ///</summary>
        [TestMethod()]
        public void TermOfUseFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(0, target.TermOfUseFrames.Count);
        }

        /// <summary>
        ///A test for SynchronisedTextFrames
        ///</summary>
        [TestMethod()]
        public void SynchronisedTextFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(0, target.SynchronisedTextFrames.Count);
        }

        /// <summary>
        ///A test for SynchronisedTempoCodes
        ///</summary>
        [TestMethod()]
        public void SynchronisedTempoCodesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.SynchronisedTempoCodes);
        }

        /// <summary>
        ///A test for Reverb
        ///</summary>
        [TestMethod()]
        public void ReverbTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.Reverb);
        }

        /// <summary>
        ///A test for RelativeVolume
        ///</summary>
        [TestMethod()]
        public void RelativeVolumeTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.RelativeVolume);
        }

        /// <summary>
        ///A test for RecomendedBuffer
        ///</summary>
        [TestMethod()]
        public void RecomendedBufferTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            RecomendedBufferSizeFrame expected = null; // TODO: Initialize to an appropriate value
            RecomendedBufferSizeFrame actual;
            actual = target.RecomendedBuffer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PrivateFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void PrivateFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<PrivateFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<PrivateFrame> actual;
            actual = target.PrivateFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PositionSynchronised
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void PositionSynchronisedTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            PositionSynchronisedFrame expected = null; // TODO: Initialize to an appropriate value
            PositionSynchronisedFrame actual;
            actual = target.PositionSynchronised;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PopularimeterFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void PopularimeterFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<PopularimeterFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<PopularimeterFrame> actual;
            actual = target.PopularimeterFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for PlayCounter
        ///</summary>
        [TestMethod()]
        public void PlayCounterTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.PlayCounter);
        }

        /// <summary>
        ///A test for OwnerShip
        ///</summary>
        [TestMethod()]
        public void OwnerShipTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.OwnerShip);
        }

        /// <summary>
        ///A test for MusicCDIdentifier
        ///</summary>
        [TestMethod()]
        public void MusicCDIdentifierTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsNull(target.MusicCDIdentifier);
        }

        /// <summary>
        ///A test for LoadLinkedFrames
        ///</summary>
        [TestMethod()]
        public void DoNotLoadLinkedFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            bool expected = false;
            target.LoadLinkedFrames = expected;
            Assert.AreEqual(expected, target.LoadLinkedFrames);
        }

        /// <summary>
        ///A test for LoadLinkedFrames
        ///</summary>
        [TestMethod()]
        public void LoadLinkedFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            bool expected = true;
            target.LoadLinkedFrames = expected;
            Assert.AreEqual(expected, target.LoadLinkedFrames);
        }

        /// <summary>
        ///A test for LinkFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void LinkFramesTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            FramesCollection<LinkFrame> expected = new FramesCollection<LinkFrame>();
            FramesCollection<LinkFrame> actual;
            actual = target.LinkFrames;
            Assert.AreEqual(expected.Count, actual.Count);
        }

        /// <summary>
        ///A test for Length
        ///</summary>
        [TestMethod()]
        public void LengthTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.AreEqual(1077, target.Length);
        }

        /// <summary>
        ///A test for HaveTag
        ///</summary>
        [TestMethod()]
        public void HaveTagTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsTrue(target.HaveTag);
        }

        /// <summary>
        ///A test for HadError
        ///</summary>
        [TestMethod()]
        public void HadErrorTest()
        {
            ID3v2 target = new ID3v2(filePath, true);
            Assert.IsFalse(target.HadError);
        }

        /// <summary>
        ///A test for Flags
        ///</summary>
        [TestMethod()]
        public void FlagsTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            ID3v2Flags expected = new ID3v2Flags(); // TODO: Initialize to an appropriate value
            ID3v2Flags actual;
            target.Flags = expected;
            actual = target.Flags;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FilterType
        ///</summary>
        [TestMethod()]
        public void FilterTypeTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            FilterTypes expected = new FilterTypes(); // TODO: Initialize to an appropriate value
            FilterTypes actual;
            target.FilterType = expected;
            actual = target.FilterType;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Filter
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void FilterTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FilterCollection expected = null; // TODO: Initialize to an appropriate value
            FilterCollection actual;
            actual = target.Filter;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FilePath
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void FilePathTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FilePath;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FileName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void FileNameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FileName;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EventTimingCode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void EventTimingCodeTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            EventTimingCodeFrame expected = null; // TODO: Initialize to an appropriate value
            EventTimingCodeFrame actual;
            actual = target.EventTimingCode;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Errors
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void ErrorsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            ErrorCollection expected = null; // TODO: Initialize to an appropriate value
            ErrorCollection actual;
            actual = target.Errors;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Equalisations
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void EqualisationsTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            Equalisation expected = null; // TODO: Initialize to an appropriate value
            Equalisation actual;
            actual = target.Equalisations;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EncapsulatedObjectFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void EncapsulatedObjectFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<GeneralFileFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<GeneralFileFrame> actual;
            actual = target.EncapsulatedObjectFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DropUnknowFrames
        ///</summary>
        [TestMethod()]
        public void DropUnknowFramesTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            target.DropUnknowFrames = expected;
            actual = target.DropUnknowFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DefaultUnicodeEncoding
        ///</summary>
        [TestMethod()]
        public void DefaultUnicodeEncodingTest()
        {
            TextEncodings expected = new TextEncodings(); // TODO: Initialize to an appropriate value
            TextEncodings actual;
            ID3v2.DefaultUnicodeEncoding = expected;
            actual = ID3v2.DefaultUnicodeEncoding;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for DataWithSymbolFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void DataWithSymbolFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<DataWithSymbolFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<DataWithSymbolFrame> actual;
            actual = target.DataWithSymbolFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Commercial
        ///</summary>
        [TestMethod()]
        public void CommercialTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            CommercialFrame expected = null; // TODO: Initialize to an appropriate value
            CommercialFrame actual;
            target.Commercial = expected;
            actual = target.Commercial;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AutoTextEncoding
        ///</summary>
        [TestMethod()]
        public void AutoTextEncodingTest()
        {
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            ID3v2.AutoTextEncoding = expected;
            actual = ID3v2.AutoTextEncoding;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AudioEncryptionFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void AudioEncryptionFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<AudioEncryptionFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<AudioEncryptionFrame> actual;
            actual = target.AudioEncryptionFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AttachedPictureFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void AttachedPictureFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FramesCollection<AttachedPictureFrame> expected = null; // TODO: Initialize to an appropriate value
            FramesCollection<AttachedPictureFrame> actual;
            actual = target.AttachedPictureFrames;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WriteID3Header
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void WriteID3HeaderTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FileStream Data = null; // TODO: Initialize to an appropriate value
            int Ver = 0; // TODO: Initialize to an appropriate value
            target.WriteID3Header(Data, Ver);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetTextFrame
        ///</summary>
        [TestMethod()]
        public void SetTextFrameTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            string FrameID = string.Empty; // TODO: Initialize to an appropriate value
            string Text = string.Empty; // TODO: Initialize to an appropriate value
            target.SetTextFrame(FrameID, Text);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SetMinorVersion
        ///</summary>
        [TestMethod()]
        public void SetMinorVersionTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            int ver = 0; // TODO: Initialize to an appropriate value
            target.SetMinorVersion(ver);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SaveRestOfFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void SaveRestOfFileTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            int StartIndex = 0; // TODO: Initialize to an appropriate value
            FileStreamEx_Accessor Orgin = null; // TODO: Initialize to an appropriate value
            FileStreamEx_Accessor Temp = null; // TODO: Initialize to an appropriate value
            int Ver = 0; // TODO: Initialize to an appropriate value
            target.SaveRestOfFile(StartIndex, Orgin, Temp, Ver);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest2()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            int Ver = 0; // TODO: Initialize to an appropriate value
            string Formula = string.Empty; // TODO: Initialize to an appropriate value
            target.Save(Ver, Formula);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest1()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            int Ver = 0; // TODO: Initialize to an appropriate value
            target.Save(Ver);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            target.Save();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ReadFrames
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void ReadFramesTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FileStreamEx_Accessor Data = null; // TODO: Initialize to an appropriate value
            int Length = 0; // TODO: Initialize to an appropriate value
            target.ReadFrames(Data, Length);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for LoadFrameFromFile
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void LoadFrameFromFileTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            string FrameID = string.Empty; // TODO: Initialize to an appropriate value
            string FileAddress = string.Empty; // TODO: Initialize to an appropriate value
            target.LoadFrameFromFile(FrameID, FileAddress);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for LoadAllLinkedFrames
        ///</summary>
        [TestMethod()]
        public void LoadAllLinkedFramesTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            target.LoadAllLinkedFrames();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            target.Load();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for IsAddable
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void IsAddableTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            string FrameID = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsAddable(FrameID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Initializer
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void InitializerTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            target.Initializer();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetTextFrame
        ///</summary>
        [TestMethod()]
        public void GetTextFrameTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            string FrameID = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetTextFrame(FrameID);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for FormulaFileName
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void FormulaFileNameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            string Formula = string.Empty; // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.FormulaFileName(Formula);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ClearAll
        ///</summary>
        [TestMethod()]
        public void ClearAllTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData); // TODO: Initialize to an appropriate value
            target.ClearAll();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for AddFrame
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void AddFrameTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            FileStreamEx_Accessor Data = null; // TODO: Initialize to an appropriate value
            string FrameID = string.Empty; // TODO: Initialize to an appropriate value
            int Length = 0; // TODO: Initialize to an appropriate value
            FrameFlags Flags = new FrameFlags(); // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddFrame(Data, FrameID, Length, Flags);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddError
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Crimson.Catalog.ID3.dll")]
        public void AddErrorTest()
        {
            PrivateObject param0 = null; // TODO: Initialize to an appropriate value
            ID3v2_Accessor target = new ID3v2_Accessor(param0); // TODO: Initialize to an appropriate value
            ID3Error Error = null; // TODO: Initialize to an appropriate value
            target.AddError(Error);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ID3v2 Constructor
        ///</summary>
        [TestMethod()]
        public void ID3v2ConstructorTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v2 target = new ID3v2(filePath, LoadData);
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
