using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class TextFieldType
    {
        protected XmlNode theNode;
        protected XmlAttribute langAttr;

        internal TextFieldType(XmlNode node)
        {
            theNode = node;
            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name == "lang")
                {
                    langAttr = attr;
                }
            }
        }

        public string Value 
        {
            get { return theNode.InnerText; }
            set { theNode.InnerText = value; }
        }

        public string Language 
        {
            get
            {
                if (langAttr != null) return langAttr.InnerText;
                return null;
            }
            set
            {
                if (langAttr != null)
                {
                    langAttr.InnerText = value;
                }
            }
        }

        public override string ToString()
        {
            return this.Value;
        }
    }
}
