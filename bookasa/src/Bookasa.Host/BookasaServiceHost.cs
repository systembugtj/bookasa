using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Common;
using Arcadia.Bookasa.Common.Service;

namespace Arcadia.Bookasa.Host
{
    public class BookasaServiceHost : IServiceHost
    {
        public BookasaServiceHost()
        {
        }

        public List<T> GetFunctionExtensions<T>(ExtensionCategory category)
        {
            throw new NotImplementedException("GetFunctionExtension<T> is not implemented");
        }

        public T GetFunctionExtension<T>() where T : IServiceExtension, new ()
        {
            throw new NotImplementedException("GetFunctionExtension<T> is not implemented");
        }

        public void RunInBackground(IBackgroundWorking worker)
        {

        }
    }
}
