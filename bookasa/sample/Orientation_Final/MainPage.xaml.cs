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
using System.Windows.Navigation;

namespace Orientation
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            SupportedOrientations = SupportedPageOrientation.Portrait | SupportedPageOrientation.Landscape;
        }

        private void onPortraitPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PortraitPage.xaml", UriKind.Relative));
        }

        private void onLandscapePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/LandscapePage.xaml", UriKind.Relative));
        }

        private void onNeutralPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NeutralPage.xaml", UriKind.Relative));
        }

        private void onOrientationChange(object sender, OrientationChangedEventArgs e)
        {
            MessageBox.Show("You changed the Orientation to: " + this.Orientation,"Orientation Change",MessageBoxButton.OK);
        }
    }
}