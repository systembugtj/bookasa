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
using System.Xml.Linq;
using Crimson.Reader.EPub;
using Crimson.Reader.EPub.OpenContainerFormat;
using Crimson.Reader.EPub.OpenPackagingFormat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;
using System.Xml;
using System.IO;

namespace Crimson.Catalog.WebSite.Tests
{
    /// <summary>
    ///This is a test class for OPFTest and is intended
    ///to contain all OPFTest Unit Tests
    ///</summary>
    [TestClass()]
    public class OPFTest
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
        ///A test for UniqueIdentifier
        ///</summary>
        [TestMethod()]
        public void UniqueIdentifierTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.UniqueIdentifier = expected;
            actual = target.UniqueIdentifier;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Spine
        ///</summary>
        [TestMethod()]
        public void SpineTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            Spine expected = null; // TODO: Initialize to an appropriate value
            Spine actual;
            target.Spine = expected;
            actual = target.Spine;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for NavigationCenterExtended
        ///</summary>
        [TestMethod()]
        public void NavigationCenterExtendedTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            NCX expected = null; // TODO: Initialize to an appropriate value
            NCX actual;
            target.NavigationCenterExtended = expected;
            actual = target.NavigationCenterExtended;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for MetaData
        ///</summary>
        [TestMethod()]
        public void MetaDataTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            MetaData expected = null; // TODO: Initialize to an appropriate value
            MetaData actual;
            target.MetaData = expected;
            actual = target.MetaData;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Manifest
        ///</summary>
        [TestMethod()]
        public void ManifestTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            Reader.EPub.OpenPackagingFormat.Manifest expected = null; // TODO: Initialize to an appropriate val
            Reader.EPub.OpenPackagingFormat.Manifest actual;
            target.Manifest = expected;
            actual = target.Manifest;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Guide
        ///</summary>
        [TestMethod()]
        public void GuideTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            Guide expected = null; // TODO: Initialize to an appropriate value
            Guide actual;
            target.Guide = expected;
            actual = target.Guide;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Parse
        ///</summary>
        [TestMethod()]
        public void ParseTest()
        {
            System.Reflection.Assembly asm = Assembly.GetExecutingAssembly();    
            XDocument xd = XDocument.Load(new StreamReader(
                asm.GetManifestResourceStream("Crimson.Catalog.WebSite.Tests.Data.EPub._1023.epub._1023.content.opf")));
            OPF target = new OPF();
            target.Parse(xd.ToString());
            Assert.IsNull(target.Manifest);
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest2()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            XDocument xd = null; // TODO: Initialize to an appropriate value
            target.Load(xd);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest1()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            string uri = string.Empty; // TODO: Initialize to an appropriate value
            target.Load(uri);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Load
        ///</summary>
        [TestMethod()]
        public void LoadTest()
        {
            OPF target = new OPF(); // TODO: Initialize to an appropriate value
            Container con = null; // TODO: Initialize to an appropriate value
            target.Load(con);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for OPF Constructor
        ///</summary>
        [TestMethod()]
        public void OPFConstructorTest()
        {
            OPF target = new OPF();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }
    }
}
