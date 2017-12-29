using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Common.Format;
using Arcadia.Bookasa.Data;

namespace Arcadia.Bookasa.Format.TXT
{
    public class BookFormatTxt : BookFormatBase
    {
        protected override string ExtensionName
        {
            get
            {
                return "TXT";
            }
        }
    }
}
