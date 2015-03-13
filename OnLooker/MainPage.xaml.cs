using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Com.AMap.Api.Maps;
using Com.AMap.Api.Maps.Model;
using System.Windows.Media;
using AMapInfoWindow;
using System.Threading;

namespace OnLooker
{
    public partial class MainPage : PhoneApplicationPage
    {
        AMapCircle mycircle;//范围圆
        AMapGeolocator mylocation;//定位，我的
        LatLng locationLatLng;//经纬度变量
        readonly List<AMapMarker> markersList = new List<AMapMarker>();//标记物列表
        int loadCount = 1;//页面第一次打开，导航到这里
        AMap amap;//地图
        private AMapMarker mymarker;//
        int exitPress = 1;
        public MainPage()
        {
            InitializeComponent();
            this.MapPanel.Children.Add(amap = new AMap());
            amap.GetUiSettings().ZoomControlsEnabled = false;
            this.Loaded += MyLocation_Loaded;
            this.Unloaded += MyLocation_Unloaded;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            GeoCoordinateWatcher SysGeoCoorWatcher = new GeoCoordinateWatcher();
            //new Thread(() =>
            //{
                if (!SysGeoCoorWatcher.TryStart(false, TimeSpan.FromSeconds(30)))
                {
                    new Coding4Fun.Toolkit.Controls.ToastPrompt()
                    {
                        FontSize = 26,
                        Message = "GPS设备已经关闭",
                        Title = null,
                        Background = new SolidColorBrush(Colors.Orange),
                        Foreground = new SolidColorBrush(Colors.Blue),
                        IsTimerEnabled = true,
                      
                        TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                        Height = 100
                    }.Show();
                    //Dispatcher.BeginInvoke(delegate
                    //{
                    //    MessageBox.Show("您的GPS设备已经关闭，需要使用程序的话，需要您到设置->定位 页打开定位功能",
                    //        "定位服务已关闭",
                    //        MessageBoxButton.OK);
                    //});
                }
            //}).Start();
            if (!System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                new Coding4Fun.Toolkit.Controls.ToastPrompt()
                {
                    FontSize = 26,
                    Message = "网络异常。请检查网络设置",
                    Title = null,
                    Background = new SolidColorBrush(Colors.Orange),
                    Foreground = new SolidColorBrush(Colors.Blue),

                    TextOrientation = System.Windows.Controls.Orientation.Horizontal,
                    Height = 100
                }.Show();

            }

            if (loadCount != 1)
            {
                this.MapPanel.Children.Add(amap = new AMap());
                amap.GetUiSettings().ZoomControlsEnabled = false;
                this.Loaded += MyLocation_Loaded;
                this.Unloaded += MyLocation_Unloaded;
            }
            loadCount++;
            base.OnNavigatedTo(e);
        }
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LatLngSets datas = new LatLngSets();
            for (int i = 0; i < datas.LatLngList.Count; i++)
            {
                AMapMarker marker = amap.AddMarker(new AMapMarkerOptions()
                {
                    Position = new LatLng(datas.LatLngList[i].X + 0.168, datas.LatLngList[i].Y + 0.11),
                    Title = "EventMarker",
                    Snippet = "Snippet",
                    IconUri = new Uri("Resources/images/AZURE.png", UriKind.Relative),
                });
                AMapMarker marker1 = amap.AddMarker(new AMapMarkerOptions()
                {
                    Position = new LatLng(datas.LatLngList[i].X + 0.168, datas.LatLngList[i].Y + 0.11),
                    Title = "ActivityMarker",
                    Snippet = "Snippet",
                    IconUri = new Uri("Resources/images/GREEN.png", UriKind.Relative),
                });
                markersList.Add(i % 2 == 0 ? marker : marker1);
            }
            amap.MarkerClickListener += AmapMarkerClickListener;
        }
        void MyLocation_Loaded(object sender, RoutedEventArgs e)
        {
            mylocation = new AMapGeolocator();
            mylocation.Start();
            //触发位置改变事件
            mylocation.PositionChanged += MylocationPositionChanged;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            amap = null;
            mycircle = null;
            mymarker = null;
            if(mylocation!=null)
            {
                mylocation.PositionChanged -= MylocationPositionChanged;
            }
           
            mylocation = null;
            this.Loaded -= MyLocation_Loaded;
            this.Unloaded -= MyLocation_Unloaded;
            this.MapPanel.Children.Clear();
            base.OnNavigatedFrom(e);
        }
        void MyLocation_Unloaded(object sender, RoutedEventArgs e)
        {
            if (mylocation != null)
            {
                mylocation.PositionChanged -= MylocationPositionChanged;
                mylocation.Stop();
                return;
            }
        }
        void MylocationPositionChanged(AMapGeolocator sender, AMapPositionChangedEventArgs args)
        {
            locationLatLng = args.LngLat;
            this.Dispatcher.BeginInvoke(() =>
            {
                if (mymarker == null)
                {
                    //添加圆
                    mycircle = amap.AddCircle(new AMapCircleOptions()
                     {
                         Center = args.LngLat,//圆点位置
                         Radius = (float)780.0,//(float)args.Accuracy,//半径
                         FillColor = Color.FromArgb(80, 100, 150, 255),
                         StrokeWidth = 2,//边框粗细
                         StrokeColor = Color.FromArgb(80, 0, 0, 255),//边框颜色
                      });

                    //添加点标注，用于标注地图上的点
                    mymarker = amap.AddMarker(
                   new AMapMarkerOptions()
                   {
                       Position = args.LngLat,//图标的位置
                       Title = "我的位置",
                       Snippet=args.LngLat.ToString(),
                       IconUri = new Uri("Images/location_on.png", UriKind.Relative),//图标的URL
                       Anchor = new System.Windows.Point(0.5, 0.5),//图标中心点
                    });
                }
                else
                {
                    //点标注和圆的位置在当前经纬度
                    mymarker.Position = args.LngLat;
                    //mycircle.Center = args.LngLat;
                    //mycircle.Radius = (float)780.0;//(float)args.Accuracy;//圆半径
                }

                //设置当前地图的经纬度和缩放级别
                amap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(args.LngLat, 15));
            });
        }
        void AmapMarkerClickListener(AMapMarker sender, AMapEventArgs args)
        {
            
            AInfoWindow info = new AInfoWindow();
            info.Title = "樱花开了耶！！";
            info.ContentText = "一大片一大片的:)！！";
            if ((sender as AMapMarker).Title == "EventMarker")
            {
                // info.Background = new SolidColorBrush(Color.FromArgb(180, 0, 0, 200));
                info.TitleBackgroundColor = Color.FromArgb(180, 0, 180, 80);
                info.ContentBackgroundColor = Color.FromArgb(180, 0, 180, 80);
            }
            else
            {
                info.TitleBackgroundColor = Color.FromArgb(180, 0, 0, 180);
                info.ContentBackgroundColor = Color.FromArgb(180, 0, 0, 180);
            }
               
            info.TitleTextBlock.FontSize = 24;
            info.ContentTextBlock.FontSize = 26;
            sender.ShowInfoWindow(info, new System.Windows.Point(0, 0)); 
        }

        private void EventBtn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ReleaseEventPage.xaml", UriKind.Relative));
        }
        private void ListModeBtn_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ListModePage.xaml", UriKind.Relative));
        }

        private void SettingItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/SettingPage.xaml", UriKind.Relative));
        }

        private void ActivityItem_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/ReleaseActivityPage.xaml", UriKind.Relative));
        }

        private void MainPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                var result = MessageBox.Show("您确定要退出OnLooker吗？", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
        }

        private void RefreshBtn_Click(object sender, EventArgs e)
        {

        }
    
    }
}

       