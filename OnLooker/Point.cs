using System;
using System.Linq;

namespace OnLooker
{
    
    class Point
    {
        private  double pX;
        private  double pY;

        public double X
        {
            get { return pX; }
        }
        public double Y
        {
            get { return pY; }
        }
           public Point(double tx,double ty)
            {
                pX = tx;
                pY = ty;
            }

       
    }
}
