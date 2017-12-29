using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcadia.Bookasa.Common.Exception
{
    public class FindingBookException : System.Exception
    {
        public FindingBookException(string msg) : base (msg)
        {
        }
    }
}
