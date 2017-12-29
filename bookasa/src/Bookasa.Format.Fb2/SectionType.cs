using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections;

namespace Arcadia.Bookasa.Format.Fb2
{
    /// <summary>
    /// A basic block of a book. Can contain more child sections or textual content.
    /// </summary>
    public class SectionType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;
        private XmlAttribute attrId;

        internal SectionType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
            this.Content = new ArrayList();
            this.Epigraphs = new List<EpigraphType>();
            this.Sections = new List<SectionType>();

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
                    case "image": this.Image = new ImageType(childNode);
                        break;
                    case "annotation": this.Annotation = new AnnotationType(childNode);
                        break;
                    case "section": this.Sections.Add(new SectionType(childNode));
                        break;
                    case "p": this.Content.Add(new ParagraphType(childNode));
                        break;
                    case "poem": this.Content.Add(new PoemType(childNode));
                        break;
                    case "empty-line": this.Content.Add(new EmptyLineType());
                        break;
                    case "subtitle": this.Content.Add(new ParagraphType(childNode));
                        break;
                    case "cite": this.Content.Add(new CiteType(childNode));
                        break;
                    //case "table": this.Content.Add(new TableType(childNode));
                    //    break;
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

        public TitleType Title { get; private set; }

        public IList<EpigraphType> Epigraphs { get; private set; }

        public ImageType Image { get; private set; }

        public AnnotationType Annotation { get; private set; }

        public IList<SectionType> Sections { get; private set; }

        public IList Content { get; private set; }

    }
}
