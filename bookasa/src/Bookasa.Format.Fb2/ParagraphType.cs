using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    /// <summary>
    /// A basic paragraph. It may include simple formatting inside.
    /// </summary>
    public class ParagraphType : StyleType
    {
        private XmlAttribute attrId;
        private XmlAttribute attrStyle;

        internal ParagraphType(XmlNode node) : base(node)
        {
            foreach (XmlAttribute attr in node.Attributes)
            {
                switch (attr.Name)
                {
                    case "id": attrId = attr;
                        break;
                    case "style": attrStyle = attr;
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

        public string Style
        {
            get
            {
                if (attrStyle != null)
                    return attrStyle.Value;
                return null;
            }
            set
            {
                if (attrStyle != null)
                    attrStyle.Value = value;
            }
        }
    }
}
