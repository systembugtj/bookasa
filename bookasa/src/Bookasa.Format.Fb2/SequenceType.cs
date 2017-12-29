using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class SequenceType
    {
        private XmlNode theNode;
        private XmlAttribute attrName;
        private XmlAttribute attrNumber;
        private XmlAttribute attrLang;

        public SequenceType(XmlNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException("node");
            }

            theNode = node;

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "name": this.attrName = attr;
                        break;
                    case "number": this.attrNumber = attr;
                        break;
                    case "lang": this.attrLang = attr;
                        break;
                    default:
                        break;
                }
            }
        }

        public string Name
        {
            get
            {
                if (attrName != null) return attrName.InnerText;
                return null;
            }
            set
            {
                if (attrName != null)
                {
                    attrName.InnerText = value;
                }
            }
        }

        public int Number
        {
            get
            {
                if (attrNumber != null) return int.Parse(attrNumber.InnerText);
                return 0;
            }
            set
            {
                if (attrNumber != null)
                {
                    attrNumber.InnerText = value.ToString();
                }
            }
        }
    }
}
