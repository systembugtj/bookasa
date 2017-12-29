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
    public class CreateCatalogTests
    {
        private Library LocalCatalog { get; set; }

        [TestFixtureSetUp]
        public void CreateDataBase()
        {
            System.IO.File.Delete("MyLibrary.sdf");
            LocalCatalog = new Library("MyLibrary.sdf");
            //local = new MyLibrary.Library(@"Data Source=M90\SQLEXPRESS;Initial Catalog=MyLibrary;Integrated Security=True");
            //LocalCatalog.DeleteDatabase();
            LocalCatalog.CreateDatabase();
        }

        [Test]
        public void CreateCatalogTest()
        {
            CreateCatalog cat = new CreateCatalog() { LocalCatalog = LocalCatalog };
            cat.LoadCatalogs("Applications");

            var catalogs = (from c in LocalCatalog.Catalogs
                       where c.Name.Contains("Europe")
                       select c);

            Assert.AreEqual(catalogs.First().CatalogId, 3);
        }

        [TestFixtureTearDown]
        public void CloseDatabase()
        {
            LocalCatalog.Dispose();
            LocalCatalog = null;
        }
    }
}
