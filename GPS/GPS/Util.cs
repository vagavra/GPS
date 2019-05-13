using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS
{
    class Util
    {
        public static double Distance(int x1, int y1, int x2, int y2)
        {
            return Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
        }

        public static double Distance(Node n1, Node n2)

        {

            return Distance(n1.X, n1.Y, n2.X, n2.Y);

       }
}
}
