using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Arcadia.Bookasa.Format.Fb2
{
    public abstract class StyleType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;

        internal StyleType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Content = new ArrayList();

            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name == "lang")
                {
                    attrLang = attr;
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
                    case "a": this.Content.Add(new LinkType(childNode));
                        break;
                    case "image": this.Content.Add(new ImageType(childNode));
                        break;
                    case "#text": this.Content.Add(childNode.InnerText);
                        break;
                    default:
                        break;
                }
            }
        }

        public string Language
        {
            get
            {
                if (attrLang != null) 
                    return attrLang.Value;
                return null;
            }
            set
            {
                if (attrLang != null) 
                    attrLang.Value = value;
            }
        }

        public IList Content { get; private set; }
        
    }
}
