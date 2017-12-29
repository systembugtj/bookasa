using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arcadia.Bookasa.Data.Watching;
using Arcadia.Bookasa.Common;
using Arcadia.Bookasa.Common.Exception;
using Arcadia.Bookasa.Common.Watch;
using Arcadia.Bookasa.Common.Scan;
using Arcadia.Bookasa.Common.Service;
using Arcadia.Bookasa.Common.Storage;

namespace Arcadia.Bookasa.Service.SourceMonitor
{
    public class SourceMonitor : IServiceExtension, IBackgroundWorking
    {
        private IServiceHost _serviceHost;
        private IConfiguration _configSetting;
        private object _gate = new object();
        private Queue<WatchSource> _changedSourceQueue = new Queue<WatchSource>();

        /// <summary>
        /// return the configuration setting.
        /// </summary>
        public IConfiguration ConfigurationSetting 
        { 
            get
            {
                return this._configSetting;
            }        
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config"></param>
        public SourceMonitor()
        {

        }

        /// <summary>
        /// Initialize the Source Monitor as a Service Extension.
        /// </summary>
        /// <param name="config">configuration </param>
        /// <param name="serviceHost">service host which init the extension</param>
        public void Initialize(IConfiguration config, IServiceHost serviceHost)
        {
            if (config == null || serviceHost == null)
            {
                throw new InitializeParameterEmtyException("parameter can't be null for initialize");
            }

            _configSetting = config;
            _serviceHost = serviceHost;

            WatchSourceCollection watchSourcese = this.ConfigurationSetting.GetConfiguationData<WatchSourceCollection>("");                       

            List <ISourceWatcher> watchers = _serviceHost.GetFunctionExtensions<ISourceWatcher>(ExtensionCategory.WatchingExtension);

            foreach (WatchSource source in watchSourcese)
            {
                watchers.ForEach(delegate(ISourceWatcher watcher)
                {
                    if (watcher.IsAcceptedSource(source))
                    {
                        watcher.Watching(source);

                        watcher.RegisterEvent(this.MonitorSourceChangedEventHandler);
                    }
                });
            }

            _serviceHost.RunInBackground(this);
             
        }
        public void MonitorSourceChangedEventHandler(object sender, SourceChangedEventArgs e)
        {
            lock (_gate)
            {
                _changedSourceQueue.Enqueue(e.ChangedUri);
            }
        }

        public void Running()
        {
            lock (_gate)
            {
                WatchSource source = _changedSourceQueue.Dequeue();

                List<IBookScanner> scanners = _serviceHost.GetFunctionExtensions<IBookScanner>(ExtensionCategory.ScanningExtension);

                foreach (IBookScanner scanner in scanners)
                {
                    if (scanner.IsAcceptedSource(source))
                    {
                        scanner.Scanning(source, this.MonitorFindBookEventHandler);
                        break;
                    }
                };
                
            }
        }

        public void MonitorFindBookEventHandler (Object sender, Object args)
        {
        }
    }
}
