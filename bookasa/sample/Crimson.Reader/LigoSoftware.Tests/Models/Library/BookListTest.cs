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
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Crimson.Catalog.WebSite.Tests.Library
{
    /// <summary>
    ///This is a test class for BookListTest and is intended
    ///to contain all BookListTest Unit Tests
    ///</summary>
    [TestClass()]
    public class BookListTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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
        /// How many books in the 1000 Novels everyone must read were written by Mark Twain? (5)
        /// </summary>
        [TestMethod()]
        public void CountMarkTwainBooksTest()
        {
            Services.Library.BookList target = new Data.Library.GlobalBookLists().OneThousandNovelsEveryMustRead();
            var twainBooks = (from book in target.Books
                              join bookcreator in target.BookCreators on
                                book.BookId equals bookcreator.BookId
                              join creator in target.Creators on
                                bookcreator.CreatorId equals creator.CreatorId
                              where creator.Fullname.ToUpper().Contains("Twain".ToUpper())
                              select new { book.Title, creator.Fullname } ).ToList();
                            
            Assert.AreEqual(twainBooks.Count(), 5);
        }

        /// <summary>
        /// How many authors in the 1000 Novels everyone must read?\
        /// </summary>
        [TestMethod()]
        public void CountCreatorsTest()
        {
            Services.Library.BookList target = new Data.Library.GlobalBookLists().OneThousandNovelsEveryMustRead();
            Assert.AreEqual(target.Creators.Count(), 684);
        }

        /// <summary>
        ///A test for If we get 1000 Novels
        ///</summary>
        [TestMethod()]
        public void OneThousandNovelsEveryMustReadTest()
        {
            Data.Library.GlobalBookLists target = new Data.Library.GlobalBookLists();
            Assert.AreEqual(1000, target.OneThousandNovelsEveryMustRead().Books.Count, "Thre are not 1000 books in the list");
        }

        /// <summary>
        ///A test for GlobalBookLists Constructor
        ///</summary>
        [TestMethod()]
        public void GlobalBookListsConstructorTest()
        {
            Data.Library.GlobalBookLists target = new Data.Library.GlobalBookLists();
            Assert.IsNotNull(target);
        }
    }
}
