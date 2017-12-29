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
using Crimson.Reader.EPub.OpenContainerFormat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace Crimson.Catalog.WebSite.Tests
{
    /// <summary>
    ///This is a test class for ContainerTest and is intended
    ///to contain all ContainerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ContainerTest
    {
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
        ///A test for Version
        ///</summary>
        [TestMethod()]
        public void VersionTest()
        {
            Container target = new Container(); // TODO: Initialize to an appropriate value
            Version expected = new Version(1, 0); // TODO: Initialize to an appropriate value
            Assert.AreEqual(expected.ToString(), target.Version.ToString());
        }

        /// <summary>
        ///A test for RootFiles
        ///</summary>
        [TestMethod()]
        public void RootFilesTest()
        {
            Container target = new Container(); // TODO: Initialize to an appropriate value
            List<RootFile> expected = new List<RootFile>(); // TODO: Initialize to an appropriate value
            Assert.AreEqual(expected.Count, target.RootFiles.Count);
        }

        /// <summary>
        ///A test for NameSpace
        ///</summary>
        [TestMethod()]
        public void NameSpaceTest()
        {
            Container target = new Container(); // TODO: Initialize to an appropriate value
            string expected = "urn:oasis:names:tc:opendocument:xmlns:container"; // TODO: Initialize to an appropriate value
            Assert.AreEqual(expected, target.NameSpace);
        }

        /// <summary>
        ///A test for ToString
        ///</summary>
        [TestMethod()]
        public void ToStringTest()
        {
            Container target = new Container(); // TODO: Initialize to an appropriate value
            string expected = "<container version=\"1.0\" xmlns=\"urn:oasis:names:tc:opendocument:xmlns:container\">\r\n<rootfiles xmlns=\"\">\r\n<rootfile full-path=\"1023/content.opf\" media-type=\"application/oebps-package+xml\" />\r\n</rootfiles>\r\n</container>";
            target.RootFiles.Add(new RootFile() { Name="1023/content.opf", MediaType="application/oebps-package+xml" });
            Assert.AreEqual(expected, target.ToString());
        }

        /// <summary>
        ///A test for Container Constructor
        ///</summary>
        [TestMethod()]
        public void ContainerConstructorTest()
        {
            Container target = new Container();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
