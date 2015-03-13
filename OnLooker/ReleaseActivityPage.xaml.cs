using System;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace OnLooker
{
    public partial class ActivityPage : PhoneApplicationPage
    {
        public ActivityPage()
        {
            InitializeComponent();
        }

        private void RegisterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            WebBrowserTask onlookerWeb = new WebBrowserTask();
            onlookerWeb.Uri = new Uri("http://www.baidu.com");
            onlookerWeb.Show();
        }

        private void UserReadingBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/UserReadingPage.xaml", UriKind.Relative));
        }
    }
}