using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;
using System.Net;

namespace Crimson.Catalog.Tests
{
    [TestFixture]
    public class RssFeedTests
    {
        [Test]
        public void WallStreeJournalRssTest()
        {
            using (StringReader reader = new StringReader(new WebClient().DownloadString("http://online.wsj.com/xml/rss/3_7011.xml")))
            {
                // setup the SgmlReader and load it into a XDocument
                Sgml.SgmlReader sgmlReader = new Sgml.SgmlReader();
                sgmlReader.DocType = "HTML";
                sgmlReader.WhitespaceHandling = WhitespaceHandling.All;
                sgmlReader.CaseFolding = Sgml.CaseFolding.ToLower;
                sgmlReader.InputStream = reader;

                string rawrssfeed = sgmlReader.ReadOuterXml();
                Console.WriteLine(rawrssfeed);
                XDocument rss = XDocument.Load(rawrssfeed);

                // Read the rss feed using linq
                var rssfeed = (from nodes in rss.Descendants()
                               select nodes);

                foreach (XElement p in rssfeed)
                {
                    switch (p.Name.LocalName)
                    {
                        default:
                            ParseItem(p);
                            break;
                    }
                }
            }
        }

        public void ParseItem(XElement rssitem)
        {
            // Read the html header using linq
            var itemdetails = (from nodes in rssitem.Descendants()
                              select nodes);

            foreach (XElement item in itemdetails)
            {
                switch (item.Name.LocalName)
                {
                    default:
                        Console.Error.Write(item.Name.LocalName + " ");
                        foreach (var att in item.Attributes())
                            Console.Error.Write(att.Name + "=" + att.Value.Trim() + " ");
                        Console.Error.WriteLine();
                        break;
                }
            }
            Console.Error.Write("*********************************************");
        }
    }
}
//case "p":
//    Console.WriteLine("<paragraph>");
//    Console.WriteLine(p.Value.Trim());
//    Console.WriteLine("</paragraph>");
//    break;
//case "h1":
//    Console.WriteLine("<h1>{0}</h1>", p.Value.Trim());
//    break;
//case "h2":
//    Console.WriteLine("<h2>{0}</h2>", p.Value.Trim());
//    break;
