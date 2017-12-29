using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class FictionBook2
    {
        private const string xmlns = "http://www.gribuser.ru/xml/fictionbook/2.0";

        public FictionBook2(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("fb2", xmlns);
            nsmgr.AddNamespace("xlink=", "http://www.w3.org/1999/xlink");

            this.Bodies = new List<BodyType>();
            this.Binaries = new List<Binary64Type>();

            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                switch (node.Name)
                {
                    case "stylesheet":
                        break;
                    case "description": this.Description = new Description(node);
                        break;
                    case "body": this.Bodies.Add(new BodyType(node));
                        break;
                    case "binary": this.Binaries.Add(new Binary64Type(node));
                        break;
                }
            }
        }

        public Description Description { get; private set; }

        public IList<BodyType> Bodies { get; private set; }

        public IList<Binary64Type> Binaries {get; private set;}

        public static bool IsValid(string filename)
        {
            using (XmlReader xmlreader = XmlReader.Create(filename))
            {
                return xmlreader.ReadToFollowing("FictionBook", xmlns);
            }
        }
    }
}
