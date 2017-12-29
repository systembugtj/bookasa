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

namespace SIPInputScope
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void setSIPType(InputScopeNameValue isnvSIP)
        {
            if (this.textBox1 != null)
            {
                InputScope inps = new InputScope();
                InputScopeName isN = new InputScopeName();
                isN.NameValue = isnvSIP;
                inps.Names.Add(isN);
                this.textBox1.InputScope = inps;
            }
        }

        private void setDefaultSIP(object sender, RoutedEventArgs e)
        {
            if (textBlock2 != null)
            {
                textBlock2.Text = "Enter some text:";
                setSIPType(InputScopeNameValue.Default);
            }
        }

        private void setEmailSIP(object sender, RoutedEventArgs e)
        {
            textBlock2.Text = "Enter an email address:";
            setSIPType(InputScopeNameValue.EmailNameOrAddress);
        }

        private void setURLSIP(object sender, RoutedEventArgs e)
        {
            textBlock2.Text = "Enter a URL:";
            setSIPType(InputScopeNameValue.Url);
        }

        private void setPhoneSIP(object sender, RoutedEventArgs e)
        {
            textBlock2.Text = "Enter a Phone Number:";
            setSIPType(InputScopeNameValue.TelephoneNumber);
        }
    }
}