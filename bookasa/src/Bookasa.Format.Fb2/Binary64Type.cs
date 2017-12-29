using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class Binary64Type
    {
        private XmlNode theNode;
        private XmlAttribute attrType;
        private XmlAttribute attrId;

        internal Binary64Type(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            
            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "id": attrId = attr;
                        break;
                    case "content-type": attrType = attr;
                        break;
                    default:
                        break;
                }
            }
        }

        public string ContentType
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

        public string Id
        {
            get
            {
                if (attrId != null)
                    return attrId.Value;
                return null;
            }
            set
            {
                if (attrId != null)
                    attrId.Value = value;
            }
        }

        public string Value
        {
            get { return theNode.InnerText; }
        }
    }
}
