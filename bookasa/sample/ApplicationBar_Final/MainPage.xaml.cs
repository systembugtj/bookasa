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
using Microsoft.Phone.Shell;

namespace ApplicationBar
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void MenuItemClick1(object sender, EventArgs e)
        {
            MessageBox.Show(((ApplicationBarMenuItem)sender).Text, "You selected: ", MessageBoxButton.OK);
        }

        private void MenuItemClick2(object sender, EventArgs e)
        {
            MessageBox.Show(((ApplicationBarMenuItem)sender).Text, "You selected: ", MessageBoxButton.OK);
        }

        private void onAppBarVisible(object sender, RoutedEventArgs e)
        {
            if (this.ApplicationBar != null)
                this.ApplicationBar.IsVisible = (bool)((ToggleControlSwitch)sender).IsChecked;
        }

        private void onMenuEnabled(object sender, RoutedEventArgs e)
        {
            if (this.ApplicationBar != null)
                this.ApplicationBar.IsMenuEnabled = (bool)((ToggleControlSwitch)sender).IsChecked;
        }

        private void onAppBarBtnClick(object sender, EventArgs e)
        {
            MessageBox.Show("You clicked an Icon Button");
        }
    }
}