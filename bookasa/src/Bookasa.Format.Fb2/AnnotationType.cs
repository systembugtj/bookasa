using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Arcadia.Bookasa.Format.Fb2
{
    /// <summary>
    /// A cut-down version of Section used in annotations.
    /// </summary>
    public class AnnotationType
    {
        private XmlNode theNode;
        private XmlAttribute attrId;
        private XmlAttribute attrLang;

        public AnnotationType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            this.theNode = node;
            this.Content = new ArrayList();

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "lang": attrLang = attr;
                        break;
                    case "id": attrId = attr;
                        break;
                    default:
                        break;
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "p": this.Content.Add(new ParagraphType(childNode));
                        break;
                    case "poem": this.Content.Add(new PoemType(childNode));
                        break;
                    case "cite": this.Content.Add(new CiteType(childNode));
                        break;
                    case "empty-line": this.Content.Add(new EmptyLineType());
                        break;
                    default:
                        break;
                }
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
