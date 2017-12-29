using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Crimson.Catalog;
using System.Xml;
using System.Xml.Linq;


namespace Crimson.Catalog.Tests
{
    [TestFixture]
    public class AmazonCatalogTest
    {
        [Test]
        public void AmazonTest()
        {
            
            foreach (var node in GetBooksByIBN("9780307265739"))
                if (node is XElement)
                    Console.WriteLine("{0}", (node as XElement).Value);
        }
    }
}
