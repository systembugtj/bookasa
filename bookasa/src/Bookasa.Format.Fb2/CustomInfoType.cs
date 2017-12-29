using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Arcadia.Bookasa.Format.Fb2
{
    public class CustomInfoType : TextFieldType
    {
        internal CustomInfoType(XmlNode node) : base(node)
        {
        }

        public string InfoType 
        {
            get
            {
                if (theNode.Attributes.Count > 1)
                {
                    return theNode.Attributes[1].Value;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                if (theNode.Attributes.Count > 1)
                {
                    theNode.Attributes[1].Value = value;
                }
                else
                {
                    //theNode.Attributes.Append(new XmlAttribute() { Value = value.ToString() });
                }
            }
        }
    }
}
