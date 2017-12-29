using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcadia.Bookasa.Common.Exception
{
    public class InitializeParameterEmtyException : System.Exception
    {
        public InitializeParameterEmtyException(string msg)
            : base(msg)
        {
        }
    }
}
