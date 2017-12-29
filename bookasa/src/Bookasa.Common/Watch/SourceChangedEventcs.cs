using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data.Watching;

namespace Arcadia.Bookasa.Common.Watch
{

    public class SourceChangedEventArgs : System.EventArgs
    {
        public WatchSource ChangedUri { set; get; }
        public SourceChangedEventArgs(WatchSource changedUri)
        {
            this.ChangedUri = changedUri;
        }
    }

    public delegate void SourceChangedEventHandler(object sender, SourceChangedEventArgs e);
}
