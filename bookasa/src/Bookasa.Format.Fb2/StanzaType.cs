using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class StanzaType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;

        internal StanzaType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Lines = new List<ParagraphType>();

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "lang": attrLang = attr;
                        break;
                    default:
                        break;
                }
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                switch (childNode.Name)
                {
                    case "title": this.Title = new TitleType(childNode);
                        break;
                    case "subtitle": this.Subtitle = new ParagraphType(childNode);
                        break;
                    case "v": this.Lines.Add(new ParagraphType(childNode));
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

        /// <summary>
        /// Poem title
        /// </summary>
        public TitleType Title { get; private set; }

        public ParagraphType Subtitle { get; private set; }

        public IList<ParagraphType> Lines { get; private set; }

    }
}
