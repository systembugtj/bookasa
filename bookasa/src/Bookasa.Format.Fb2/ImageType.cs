using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class ImageType
    {
        XmlNode theNode;

        public ImageType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.LocalName)
                {
                    case "href": this.Reference = attr.Value;
                        break;
                    case "type": this.LinkType = attr.Value;
                        break;
                    case "alt": this.Text = attr.Value;
                        break;
                    default:
                        break;
                }
            }
        }

        public string Reference { get; private set; }

        public string LinkType { get; private set; }

        public string Text { get; private set; }

    }
}
