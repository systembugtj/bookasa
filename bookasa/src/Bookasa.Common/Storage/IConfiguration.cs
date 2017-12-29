using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arcadia.Bookasa.Common.Storage
{
    public interface IConfiguration
    {
        T GetConfiguationData <T>(string name);
    }
}
