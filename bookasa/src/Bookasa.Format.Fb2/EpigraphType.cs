using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class EpigraphType
    {
        protected XmlNode theNode;
        private XmlAttribute attrId;

        internal EpigraphType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Content = new ArrayList();
            this.TextAuthors = new List<TextFieldType>();

            foreach (XmlAttribute attr in node.Attributes)
            {
                if (attr.Name == "id")
                {
                    attrId = attr;
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
                    case "text-author": this.TextAuthors.Add(new TextFieldType(childNode));
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

        public IList Content { get; private set; }

        public IList<TextFieldType> TextAuthors { get; private set; }
    }
}
