using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WebBrowser
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void onClickGo(object sender, RoutedEventArgs e)
        {
            string site;
            site = textBox1.Text;
            webBrowser1.Navigate(new Uri(site, UriKind.Absolute));
        }

        private void onPageLoaded(object sender, RoutedEventArgs e)
        {
            InputScope inputScope = new InputScope();
            InputScopeName inputScopeName = new InputScopeName();

            inputScopeName.NameValue = InputScopeNameValue.Url;

            inputScope.Names.Add(inputScopeName);
            textBox1.InputScope = inputScope;
        }

        private void onLoadComplete(object sender, NavigationEventArgs e)
        {
            MessageBox.Show("WebBrowser has loaded the content", "Content Loaded", MessageBoxButton.OK);
        }
    }
}