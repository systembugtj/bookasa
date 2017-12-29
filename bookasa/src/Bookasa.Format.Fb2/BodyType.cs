using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class BodyType
    {
        protected XmlNode theNode;
        private XmlAttribute attrLang;
        private XmlAttribute attrId;

        internal BodyType(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            theNode = node;
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
                    case "section": this.Sections.Add(new SectionType(childNode));
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

        public ImageType Image { get; private set; }

        public TitleType Title { get; private set; }

        public IList<EpigraphType> Epigraphs { get; private set; }

        public IList<SectionType> Sections { get; private set; }

    }
}
