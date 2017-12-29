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
using Crimson.Catalog.ID3.ID3v1Frame;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crimson.Catalog.ID3;

namespace Crimson.Catalog.WebSite.Tests
{
    /// <summary>
    ///This is a test class for ID3v1Test and is intended
    ///to contain all ID3v1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class ID3v1Test
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
        ///A test for Year
        ///</summary>
        [TestMethod()]
        public void YearTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual(string.Empty, target.Year);
        }

        /// <summary>
        ///A test for TrackNumber
        ///</summary>
        [TestMethod()]
        public void TrackNumberTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual(1, target.TrackNumber);
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        [TestMethod()]
        public void TitleTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual("Perfumed Planet", target.Title);
        }

        /// <summary>
        ///A test for HaveTag
        ///</summary>
        [TestMethod()]
        public void HaveTagTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual(true, target.HaveTag);
        }

        /// <summary>
        ///A test for Genre
        ///</summary>
        [TestMethod()]
        public void GenreTest()
        {
            ID3v1 target = new ID3v1(filePath, false);
            Assert.AreEqual(Genre.Blues, target.Genre);
        }

        /// <summary>
        ///A test for FilePath
        ///</summary>
        [TestMethod()]
        public void FilePathTest()
        {
            ID3v1 target = new ID3v1(filePath, false);
            Assert.AreEqual(filePath, target.FilePath);
        }

        /// <summary>
        ///A test for Comment
        ///</summary>
        [TestMethod()]
        public void CommentTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            string expected = string.Empty;
            string actual = target.Comment;
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for Artist
        ///</summary>
        [TestMethod()]
        public void ArtistTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual("Andre Norton", target.Artist);
        }

        /// <summary>
        ///A test for Album
        ///</summary>
        [TestMethod()]
        public void AlbumTest()
        {
            ID3v1 target = new ID3v1(filePath, true);
            Assert.AreEqual("Plague Ship", target.Album);
        }

        /// <summary>
        ///A test for Save
        ///</summary>
        [TestMethod()]
        public void SaveTest()
        {
            string filePath = string.Empty; // TODO: Initialize to an appropriate value
            bool LoadData = false; // TODO: Initialize to an appropriate value
            ID3v1 target = new ID3v1(filePath, LoadData); // TODO: Initialize to an appropriate value
            target.Save();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            ID3v1 target = new ID3v1(filePath, false);
            target.Load();
            Assert.IsTrue(target.HaveTag);
        }

        /// <summary>
        ///A test for ID3v1 Constructor
        ///</summary>
        [TestMethod()]
        public void ID3v1ConstructorTest()
        {
            ID3v1 target = new ID3v1(filePath, false);
            Assert.IsNotNull(target);
        }
    }
}
