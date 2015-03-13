using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using OnLooker.Resources;
using Com.AMap.Api.Maps;
using System.Windows.Media;
using System.IO;
using System.Windows.Threading;

namespace OnLooker
{
    class RequestMsg
    {
        private Uri ServerUri = new System.Uri("http://www.onlooker.ziqiang.net/requestget/");
        private int requestType;
        private int requestStatus;
        private string eventContent;
        private string activityTitle;
        private string activityContent;
        private double latitude;
        private double longtitude;
        private string userName;
        private DateTime happenTime;
        private enum Requesttype { };
        private enum Requeststatus { };
        private string resultString;

        public void startRequest()
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://onlooker.ziqiang.net/requestget/"));
            req.Method = "GET";
            try
            {
                req.BeginGetResponse(ReqCallBack, req);
            }
            catch(Exception e)
            {

            }
            
        }
        public void ReqCallBack(IAsyncResult result)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)result.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
                long conlen = response.ContentLength;
                int status = (int)response.StatusCode;
                string statusTxt = response.StatusDescription;
                using (Stream str = response.GetResponseStream())
                {
                    StreamReader reader = new System.IO.StreamReader(str);
                    resultString = reader.ReadToEnd();
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {

                    });
                }
            }
            catch(Exception e)
            {

            }
            
        }


        public RequestMsg(int trequestType, int trequestStatus)
        {
            requestType = trequestType;
            requestStatus = trequestStatus;
        }
        public void SetActivityTitle(string title)
        {
            activityTitle = title;
        }
        public void SetActivityContent(string con)
        {
            activityContent = con;
        }
        public void SetEventContent(string con)
        {
            eventContent = con;
        }
        public void SetLatLng(double lat, double lng)
        {
            latitude = lat;
            longtitude = lng;
        }
        public void SetUserName(string name)
        {
            userName = name;
        }
        public void SetHappenTime(int year, int month, int day, int hour, int minute)
        {
            happenTime = new System.DateTime(year, month, day, hour, minute, 0);
        }
        public string GetResult()
        {
            return resultString;
        }
    }
}

