using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Crimson.Reader.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            // Process unhandled exception
            bool shutdown = false;

            // Process exception
            if (e.Exception is DivideByZeroException)
            {
                // Recoverable - continue processing
                shutdown = false;
            }
            else if (e.Exception is ArgumentNullException)
            {
                // Unrecoverable - end processing
                shutdown = true;
            }

            if (shutdown)
            {
                // If unrecoverable, attempt to save data
                MessageBoxResult result = MessageBox.Show("Application must exit:\n\n" + e.Exception.Message + "\n\nSave before exit?", "app", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes)
                {
                    // Save data
                }

                // Add entry to event log
                //EventLog.WriteEntry("app", "Unrecoverable Exception: " + e.Exception.Message, EventLogEntryType.Error);

                // Return exit code
                this.Shutdown(-1);
            }

            e.Handled = true;
        }
    }
}
