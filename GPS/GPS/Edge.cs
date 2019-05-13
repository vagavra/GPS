using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace GPS
{

    class Edge : Element
    {
        private int startId;
        private int endId;
        private bool singleDirection;

        // svojstva
        public int StartId { get => startId; set => startId = value; }
        public int EndId { get => endId; set => endId = value; }
        public bool SingleDirection { get => singleDirection; set => singleDirection = value; }
        public double Distance { get; set; }

        public Node Start()
        {
            return new GPSNavigation().qNodes.Where(n => n.ElementId == StartId).FirstOrDefault();
        }

        public Node End()
        {
            return new GPSNavigation().qNodes.Where(n => n.ElementId == EndId).FirstOrDefault();
        }

        //// konstruktor
        //public Edge( int startId, int endId, bool singleDirection)
        //{
        //    this.startId = startId;
        //    this.endId = endId;
        //    this.singleDirection = singleDirection;
        //}
    }
}
