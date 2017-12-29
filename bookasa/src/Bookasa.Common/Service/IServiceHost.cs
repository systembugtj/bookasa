using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data;

namespace Arcadia.Bookasa.Common.Service
{
    public interface IServiceHost
    {
        List<T> GetFunctionExtensions<T>(ExtensionCategory category);

        T GetFunctionExtension<T>() where T : IServiceExtension, new ();

        void RunInBackground(IBackgroundWorking worker);
    }
}
