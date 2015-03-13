using System.Text;
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
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace bubble_0._1
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        delegate void myDelegte(string s);
        delegate void EnableBtnDeletegte();
        EnableBtnDeletegte aEnableBtnDelegte;
        void EnableBtn()
        {
            button1.Background = new SolidColorBrush(Colors.Transparent);
            button1.IsEnabled = true;
        }
        myDelegte adelete;
        void SetText(string s)
        {
            inputbox.Text = s;
        }


        public class msg_class
        {
            public String content { get; set; }
            public Visibility showleft { get; set; }
            public Visibility showright { get; set; }

            public msg_class(String content, Visibility showleft, Visibility showright)
            {
                this.content = content;
                this.showleft = showleft;
                this.showright = showright;
            }
        }

        void showmsg(String msg, bool isself)
        {
            if (isself)
            {
                list.Items.Add(new msg_class(msg, Visibility.Collapsed, Visibility.Visible));
            }
            else
            {
                list.Items.Add(new msg_class(msg, Visibility.Visible, Visibility.Collapsed));
            };
        }

        void readmsg(int index)
        {
            string s = ((msg_class)(list.Items[index])).content;
            list.Items[index] = new msg_class(s, Visibility.Visible, Visibility.Collapsed);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //showmsg(inputbox.Text, true);
            //list.ScrollIntoView(list.Items[list.Items.Count - 1]);
            button1.IsEnabled = false;
            button1.Background = new SolidColorBrush(Colors.Gray);
            adelete = SetText;
            aEnableBtnDelegte = EnableBtn;
            HttpRequestPost();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            showmsg(inputbox.Text, false);
            list.ScrollIntoView(list.Items[list.Items.Count - 1]);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedIndex != -1)
            {
                readmsg(list.SelectedIndex);
            };
        }

        public void HttpRequestPost()
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://onlooker.ziqiang.net/requestget/");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                req.BeginGetRequestStream(new AsyncCallback(RequestCallBack), req);
            }
            catch(Exception e)
            { }
        }
        public void RequestCallBack(IAsyncResult result)
        {
            try
            {
                 string parms = "UserImeiCode=123&PosLatitude=10&PosLongitude=100&RequestType=0&RequestStatus=0";
                byte[] bb = Encoding.UTF8.GetBytes(parms);
                HttpWebRequest req = (HttpWebRequest)result.AsyncState;
                System.IO.Stream postStream = req.EndGetRequestStream(result);
                byte[] daya = System.Text.Encoding.UTF8.GetBytes(parms);
                postStream.Write(daya, 0, daya.Length);
                postStream.Close();
                req.BeginGetResponse(new AsyncCallback(ResponseCallBack), req);
            }
            catch(Exception e)
            { }
        }
       
        public void ResponseCallBack(IAsyncResult result)
        {
            try
            {
                HttpWebRequest req = (HttpWebRequest)result.AsyncState;
                HttpWebResponse response = (HttpWebResponse)req.EndGetResponse(result);
                System.IO.Stream str= response.GetResponseStream();
                System.IO.StreamReader reader= new System.IO.StreamReader(str);
               string tmp = reader.ReadToEnd();
                str.Close();
                reader.Close();
                response.Close();
                Dispatcher.BeginInvoke(adelete, tmp);
                Dispatcher.BeginInvoke(aEnableBtnDelegte);
                //inputbox.Text = tmp;
                //Action<string> act = new Action<string>(DisplayRes);
                //this.Dispatcher.BeginInvoke(act);
            }
            catch(Exception E)
            {}
        }
        public void DisplayRes(string s)
        {
            inputbox.Text = s;
        }

    }
}