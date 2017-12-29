using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using Spring.Context;
using Spring.Context.Support;

namespace Arcadia.Bookasa.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IApplicationContext context = ContextRegistry.GetContext();

            Window myXamlWindow = context.GetObject("MainWindow") as Window; //new MainWindow();
            myXamlWindow.Show();
        }

    }
}
