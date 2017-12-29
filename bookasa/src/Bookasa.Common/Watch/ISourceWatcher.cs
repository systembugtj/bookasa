using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data.Watching;

namespace Arcadia.Bookasa.Common.Watch
{
    public interface ISourceWatcher
    {
        bool IsAcceptedSource(WatchSource source);
        int Watching(WatchSource source);
        void RegisterEvent(SourceChangedEventHandler handler);
        void UnegisterEvent(SourceChangedEventHandler handler);
    }
}
