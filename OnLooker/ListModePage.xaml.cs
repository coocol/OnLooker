using System.Threading;
using System.Device.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using System.Windows.Media;
using Microsoft.Phone.Shell;

namespace OnLooker
{
    public partial class ListPage : PhoneApplicationPage
    {
        public ListPage()
        {
            InitializeComponent();
            ListModeBox.Margin = new Thickness(20, 20, 10, 0);
            this.Loaded += ListPage_Loaded;
            TextBlock t1 = new TextBlock();
            t1.Text = "sfsergrgrt乳房greg如果日后惹桃花微风如果任何个人体会给他如果突然回头";
            t1.TextWrapping = TextWrapping.Wrap;
            t1.FontSize = 30;
            ListModeBox.Items.Add(t1);
            TextBlock t2 = new TextBlock();
            t2.Text = "   pos       time";
            t2.FontSize = 26;
            ListModeBox.Items.Add(t2);
            TextBlock t3 = new TextBlock();
            t3.Text = "sfsergrgrt乳房greg如果日后惹桃花微风如果任何个人体会给他如果突然回头";
            t3.TextWrapping = TextWrapping.Wrap;
            t3.FontSize = 30;
            ListModeBox.Items.Add(t3);
            TextBlock t4 = new TextBlock();
            t4.Text = "   pos       time";
            t2.FontSize = 26;
            ListModeBox.Items.Add(t4);
            
        }

        void ListPage_Loaded(object sender, RoutedEventArgs e)
        {
            GeoCoordinateWatcher ss = new GeoCoordinateWatcher();
            new Thread(() =>
            {
                if (!ss.TryStart(false, TimeSpan.FromSeconds(30)))
                {
                    Dispatcher.BeginInvoke(delegate
                    {
                        MessageBox.Show("无法使用GPS服务，请手动开启服务");
                    });

                }
            }
            ).Start();
           
        }

        private void ListModeBtn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void EventBtn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ReleaseEventPage.xaml", UriKind.Relative));
        }

        private void ActivityItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ReleaseActivityPage.xaml", UriKind.Relative));
        }

        private void SettingItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("SettingPage.xaml", UriKind.Relative));
        }

        private void ListModeBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void EventBtn_Click_1(object sender, EventArgs e)
        {

        }

        private void ActivityItem_Click_1(object sender, EventArgs e)
        {

        }

        private void SettingItem_Click_1(object sender, EventArgs e)
        {

        }
    }
}