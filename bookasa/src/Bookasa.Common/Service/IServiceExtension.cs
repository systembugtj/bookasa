using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Common.Storage;

namespace Arcadia.Bookasa.Common.Service
{
    public interface IServiceExtension
    {
        void Initialize(IConfiguration config, IServiceHost serviceHost);
    }
}
