using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class NamedStyleType : StyleType
    {
        private XmlAttribute attrName;

        internal NamedStyleType(XmlNode node) : base(node)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name == "name")
                {
                    attrName = attr;
                }
            }
        }

        public string Name
        {
            get
            {
                if (attrName != null)
                    return attrName.Value;
                return null;
            }
            set
            {
                if (attrName != null)
                    attrName.Value = value;
            }
        }
    }
}
