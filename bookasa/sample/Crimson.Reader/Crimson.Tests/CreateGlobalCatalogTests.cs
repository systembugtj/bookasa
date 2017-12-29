using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Linq;
using NUnit.Framework;
using Crimson.Catalog.CrimsonLibrary;

namespace Crimson.Catalog.Tests
{
    [TestFixture]
    public class CreateGlobalCatalogTests
    {
        private Library GlobalCatalog { get; set; }

        [TestFixtureSetUp]
        public void CreateDataBase()
        {
            GlobalCatalog = new Library("Data Source=sql399.mysite4now.com;Persist Security Info=True;User ID=\"SilverReader_42237\";Password=\"SilverReader\"");
        }

        [Test]
        public void CreateCatalogTest()
        {
            CreateCatalog cat = new CreateCatalog() { LocalCatalog = GlobalCatalog };
            cat.LoadCatalogs("Applications");

            var catalogs = (from c in GlobalCatalog.Catalogs
                       where c.Name.Contains("Europe")
                       select c);

            Assert.AreEqual(catalogs.First().CatalogId, 9);
        }

        [TestFixtureTearDown]
        public void CloseDatabase()
        {
            GlobalCatalog.Dispose();
            GlobalCatalog = null;
        }
    }
}
