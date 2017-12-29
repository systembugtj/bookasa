//using Crimson.Catalog;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Linq;
//using System.IO;

//namespace Crimson.Catalog.WebSite.Tests
//{
//    /// <summary>
//    ///This is a test class for DublinCoreRepositoryTest and is intended
//    ///to contain all DublinCoreRepositoryTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class DublinCoreRepositoryTest
//    {
//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext testContextInstance { get; set; }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion

//        /// <summary>
//        ///A test for GetAgent
//        ///</summary>
//        [TestMethod()]
//        public void GetAgentTest()
//        {
//            string connection = string.Empty; // TODO: Initialize to an appropriate value
//            DublinCoreRepository target = new DublinCoreRepository(connection); // TODO: Initialize to an appropriate value
//            IQueryable<Agent> expected = null; // TODO: Initialize to an appropriate value
//            IQueryable<Agent> actual;
//            actual = target.GetAgent();
//            Assert.AreEqual(expected, actual);
//            Assert.Inconclusive("Verify the correctness of this test method.");
//        }

//        /// <summary>
//        ///A test for CreateAgent
//        ///</summary>
//        [TestMethod()]
//        public void CreateAgentTest()
//        {
//            string connection = string.Empty; // TODO: Initialize to an appropriate value
//            DublinCoreRepository target = new DublinCoreRepository(connection); // TODO: Initialize to an appropriate value
//            Guid id = new Guid(); // TODO: Initialize to an appropriate value
//            string name = string.Empty; // TODO: Initialize to an appropriate value
//            int expected = 0; // TODO: Initialize to an appropriate value
//            int actual;
//            actual = target.CreateAgent(id, name);
//            Assert.AreEqual(expected, actual);
//            Assert.Inconclusive("Verify the correctness of this test method.");
//        }

//        /// <summary>
//        ///A test for the Local DublinCoreRepository Constructor
//        ///</summary>
//        [TestMethod()]
//        public void DublinCoreLocalRepositoryConstructorTest()
//        {
//            string connection = @"C:\Users\drcarver\Documents\Visual Studio 2008\Projects\crimsonreader\EBookReader\trunk\LigoSoftware.Tests\MyCollection.sdf"; 
//            DublinCoreRepository target = new DublinCoreRepository(connection);
//            if (File.Exists(connection))
//                target.DeleteDatabase();
//            target.CreateDatabase();
//            Assert.IsTrue(File.Exists(connection));
//        }

//        /// <summary>
//        ///A test for DublinCoreRepository Constructor
//        ///</summary>
//        [TestMethod()]
//        public void DublinCoreRepositoryConstructorTest()
//        {
//            DublinCoreRepository target = new DublinCoreRepository("Data Source=sql399.mysite4now.com;Persist Security Info=True;User ID=CrimsonEditor_42237;Password=crimsoneditor");
//            Assert.IsTrue(target.Resources.Count() == 0);
//        }
//    }
//}
