using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data.Watching;

namespace Arcadia.Bookasa.Common.Scan
{

    public delegate void FindBookEventHandler (Object sender, Object args);


    public interface IBookScanner
    {
        bool IsAcceptedSource(WatchSource source);

        void Scanning(WatchSource target, FindBookEventHandler handler);
    }
}
