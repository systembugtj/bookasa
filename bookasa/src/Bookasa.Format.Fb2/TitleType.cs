using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Arcadia.Bookasa.Format.Fb2
{
    /// <summary>
    /// A title used in sections, poems and body elements.
    /// </summary>
    public class TitleType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;

        internal TitleType(XmlNode node)
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
                    case "p": this.Content.Add(new ParagraphType(childNode));
                        break;
                    case "empty-line": this.Content.Add(new EmptyLineType());
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
