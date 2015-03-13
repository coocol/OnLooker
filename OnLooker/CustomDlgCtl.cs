using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Tasks;

namespace OnLooker
{
    public class CustomDlgCtl : UserControl
    {
        private readonly BitmapImage tarBmp = new BitmapImage();

        public BitmapImage GetImagePhoto()
        {
            return tarBmp;
        }

        public CustomDlgCtl()
        {
            photoBtn.Click += PhotoBtn_Click;
            pictureBtn.Click += PictureBtn_Click;
        }

        readonly Button photoBtn = new Button
        {
            Content = "拍照",
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Width = 170,
            Height = 100,
            RenderTransformOrigin = new System.Windows.Point(0.33,0.453),
            Margin = new Thickness(-10,0,0,0),
        };

        readonly Button pictureBtn = new Button
        {
            Content = "相册",
            HorizontalAlignment = HorizontalAlignment.Left,
            Margin = new Thickness(197,0,-10,0),
            VerticalAlignment = VerticalAlignment.Top,
            Width = 163,
            Height = 100,
        };
      
        private void PhotoBtn_Click(object sender, RoutedEventArgs e)
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
        void ACamera_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                tarBmp.SetSource(e.ChosenPhoto);
        }

        void PictureChooser_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                tarBmp.SetSource(e.ChosenPhoto);
        }

    }
}
