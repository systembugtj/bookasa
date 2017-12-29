using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Arcadia.Bookasa.Format.Fb2
{
    /// <summary>
    /// Generic hyperlinks. Cannot be nested. 
    /// Footnotes should be implemented by links refering to 
    /// additional bodies in the same document.
    /// </summary>
    public class LinkType
    {
        private XmlNode theNode;
        private XmlAttribute attrLinkType;
        private XmlAttribute attrHref;
        private XmlAttribute attrType;

        internal LinkType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Content = new ArrayList();

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "xlink:type": attrLinkType = attr;
                        break;
                    case "xlink:href": attrHref = attr;
                        break;
                    case "type": attrType = attr;
                        break;
                    default:
                        break;
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "strong": this.Content.Add(new StrongStyleType(childNode));
                        break;
                    case "emphasis": this.Content.Add(new EmphasisStyleType(childNode));
                        break;
                    case "style": this.Content.Add(new NamedStyleType(childNode));
                        break;
                    default:
                        break;
                }
            }
        }

        public string TypeOfLink
        {
            get
            {
                if (attrLinkType != null)
                    return attrLinkType.Value;
                return null;
            }
            set
            {
                if (attrLinkType != null)
                    attrLinkType.Value = value;
            }
        }

        public string LinkTarget
        {
            get
            {
                if (attrHref != null)
                    return attrHref.Value;
                return null;
            }
            set
            {
                if (attrHref != null)
                    attrHref.Value = value;
            }
        }

        public string Type
        {
            get
            {
                if (attrType != null)
                    return attrType.Value;
                return null;
            }
            set
            {
                if (attrType != null)
                    attrType.Value = value;
            }
        }

        public IList Content { get; private set; }
    }
}
