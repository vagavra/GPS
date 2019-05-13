using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS
{
    class Node : Element, IComparable<Node>
    {
        private int x;
        private int y;

        // svojstva
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        //// konstruktor
        //public Node( int x, int y)
        //{
        //    this.x = x;
        //    this.y = y;
        //}
        public double distanceFromStart;

       public Edge prevEdge;

        public Node prevNode;

 

        

        public int CompareTo(Node other)

       {

            return distanceFromStart.CompareTo(other.distanceFromStart);

        }
}
}
