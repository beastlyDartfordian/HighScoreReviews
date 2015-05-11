using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace HighScoreReviews
{
    public partial class Help : PhoneApplicationPage
    {
        public Help()
        {
            InitializeComponent();
        }

        private void NavArticles_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void NavTwitter_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Twitter.xaml", UriKind.Relative));
        }

        private void NavHelp_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Help.xaml", UriKind.Relative));
        }
    }
}