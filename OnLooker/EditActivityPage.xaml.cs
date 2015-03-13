using System;
using System.Linq;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace OnLooker
{
    public partial class EditActivityPage : PhoneApplicationPage
    {
        private int curInput = 0;
        private Image choosedPicture = new Image();

        public EditActivityPage()
        {
            InitializeComponent();
            ChoosePhotoPanel.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void ReleaseEventPage_Loaded(object sender, RoutedEventArgs e)
        {
            ReleaseEventTxtBox.Focus();
        }

        private void ReleaseEventPhoto_Click(object sender, EventArgs e)
        {
            if (curInput == 0)
            {
                curInput = 1;
                (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IconUri = new Uri("Images/keyboard.png", UriKind.Relative);
                this.Focus();
                ChoosePhotoPanel.Visibility = System.Windows.Visibility.Visible;

            }
            else
            {
                curInput = 0;
                (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IconUri = new Uri("Images/camera.png", UriKind.Relative);
                ChoosePhotoPanel.Visibility = System.Windows.Visibility.Collapsed;
                ReleaseEventTxtBox.Focus();
            }
        }
        private void CameraBtn_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureTask aCamera = new CameraCaptureTask();
            aCamera.Completed += ACamera_Completed;
            aCamera.Show();
        }

        private void PictureBtn_Click(object sender, RoutedEventArgs e)
        {

            PhotoChooserTask pictureChooser = new PhotoChooserTask();
            pictureChooser.Completed += new EventHandler<PhotoResult>(PictureChooser_Completed);
            pictureChooser.Show();
        }
        void PictureChooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                Image tmpImage = new Image();
                tmpImage.Source = bmp;
                tmpImage.Height = 80;
                tmpImage.Width = 80;
                tmpImage.Stretch = System.Windows.Media.Stretch.Fill;

                ChosenPictureBox.Items.Add(tmpImage);
            }
            curInput = 0;
            ChoosePhotoPanel.Visibility = System.Windows.Visibility.Collapsed;
            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IconUri = new Uri("Images/camera.png", UriKind.Relative);
        }
        void ACamera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(e.ChosenPhoto);
                Image tmpImage = new Image();
                tmpImage.Source = bmp;
                tmpImage.Height = 80;
                tmpImage.Width = 80;
                tmpImage.Stretch = System.Windows.Media.Stretch.Fill;

                ChosenPictureBox.Items.Add(tmpImage);
            }
            ChoosePhotoPanel.Visibility = System.Windows.Visibility.Collapsed;
            curInput = 0;
            (this.ApplicationBar.Buttons[0] as ApplicationBarIconButton).IconUri = new Uri("Images/camera.png", UriKind.Relative);

        }

        private void EventOk_Click(object sender, EventArgs e)
        {

        }
        private void ReleaseEventTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            InputTextLength.Text = ReleaseEventTxtBox.Text.Length.ToString() + "/80";
        }



    }
}