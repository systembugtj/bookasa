using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class PoemType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;
        private XmlAttribute attrId;

        internal PoemType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Epigraphs = new List<EpigraphType>();
            this.Stanzas = new ArrayList();
            this.TextAuthors = new List<TextFieldType>();

            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "id": attrId = attr;
                        break;
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
                    case "epigraph": this.Epigraphs.Add(new EpigraphType(childNode));
                        break;
                    case "stanza": this.Stanzas.Add(new StanzaType(childNode));
                        break;
                    case "text-author": this.TextAuthors.Add(new TextFieldType(childNode));
                        break;
                    case "date": this.Date = new DateType(childNode);
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

        /// <summary>
        /// Poem title
        /// </summary>
        public TitleType Title { get; set; }

        /// <summary>
        /// Poem epigraphs if any.
        /// </summary>
        public IList<EpigraphType> Epigraphs { get; private set; }

        /// <summary>
        /// Each poem should have at leasst one stanza.
        /// Stanzas are usually separate with empty lines by user agents.
        /// </summary>
        public IList Stanzas { get; private set; }

        public IList<TextFieldType> TextAuthors { get; private set; }

        public DateType Date { get; set; }

    }
}
