using System;
using System.Collections.Generic;
using System.Linq;

namespace OnLooker
{
    class LatLngSets
    {
        private readonly List<Point> pLatLngList = new List<Point>();

        public List<Point> LatLngList
       {
           get { return pLatLngList; }
       }

       public LatLngSets()
        {
           
            pLatLngList.Add(new Point(30.3615,114.2490));
            pLatLngList.Add(new Point(30.3609, 114.2452));
            pLatLngList.Add(new Point(30.3621, 114.2343));
            pLatLngList.Add(new Point(30.3578, 114.2494));
            pLatLngList.Add(new Point(30.3568, 114.2455));
            pLatLngList.Add(new Point(30.3556, 114.2444));
            pLatLngList.Add(new Point(30.3545, 114.2550));
            pLatLngList.Add(new Point(30.3611, 114.2499));
            pLatLngList.Add(new Point(30.3533, 114.24382));
            pLatLngList.Add(new Point(30.3538, 114.2510));
            pLatLngList.Add(new Point(30.3545, 114.2560));
            pLatLngList.Add(new Point(30.3620, 114.2459));
            pLatLngList.Add(new Point(30.3588, 114.2482));
            pLatLngList.Add(new Point(30.3524, 114.210));
            pLatLngList.Add(new Point(30.3605, 114.2580));
            pLatLngList.Add(new Point(30.3620, 114.2559));
            pLatLngList.Add(new Point(30.3638, 114.2422));
            pLatLngList.Add(new Point(30.3524, 114.210));

        }

    }
}
