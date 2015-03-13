using System.Windows.Controls;
using System;
using System.Linq;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
namespace OnLooker
{
    public partial class EventDetailsPage : PhoneApplicationPage
    {
        public EventDetailsPage()
        {
            InitializeComponent();
            Image img = new Image();
            BitmapImage bmp = new BitmapImage(new Uri("Images/花.jpg", UriKind.Relative));
            img.Source = bmp;
            Grid.SetRow(img, 2);
            ContentPanel.Children.Add(img);
        }

        private void CommentBtn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ChatMsgPage.xaml", UriKind.Relative));
        }
    }
}
