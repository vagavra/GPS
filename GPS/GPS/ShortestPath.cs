using System;

using System.Collections;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;


namespace GPS

{
    class ShortestPath
    {

        public GPSNavigation gps;
        public Node start;
        public Node end;
        public List<Node> pathNodes = new List<Node>();
        public List<Edge> pathEdges = new List<Edge>();
        public double distance;
        public bool exists = true;
        public Element stop;

        public ShortestPath(GPSNavigation gps, Node start, Node end)
        {
            this.gps = gps;
            this.start = start;
            this.end = end;

            FindShortestPath();
            SetPathInfo();
        }

        public ShortestPath(GPSNavigation gps, Node start, Node end, Characteristic.CharacteristicTypes type)
        {
            this.gps = gps;
            this.start = start;
            this.end = end;

            FindShortestPath(type);
            SetPathInfoCharacteristic();
        }

        public void SetPathInfo()
        {
            Node last = null;

            foreach(Node n in gps.nodes)
            {
                if(n.ElementId == end.ElementId)
                {
                    last = n;
                    break;
                }
            }

            distance = last.distanceFromStart;

            if (distance == Double.MaxValue)
            {
                exists = false;

                return;
            }

            Node c = last;

            while (c != null)
            {
                if (c.prevEdge != null)
                    pathEdges.Add(c.prevEdge);

                pathNodes.Add(c);
                c = c.prevNode;
            }

            pathNodes.Reverse();
            pathEdges.Reverse();
        }

        public void SetPathInfoCharacteristic()
        {   
            if(stop == null)
            {
                distance = Double.MaxValue;
                exists = false;
                return;
            }

            if (stop.IsNode)
            {
                ShortestPath firstPart = new ShortestPath(gps, start, (Node)stop);
                pathNodes.AddRange(firstPart.pathNodes);
                pathEdges.AddRange(firstPart.pathEdges);

                pathNodes.RemoveAt(pathNodes.Count - 1);

                ShortestPath secondPart = new ShortestPath(gps, (Node)stop, end);
                pathNodes.AddRange(secondPart.pathNodes);
                pathEdges.AddRange(secondPart.pathEdges);

                distance = firstPart.distance + secondPart.distance;
            } else
            {
                Edge eStop = (Edge)stop;

                double currentDistance = new ShortestPath(gps, start, eStop.Start()).distance + eStop.Distance + new ShortestPath(gps, eStop.End(), end).distance;
                double reverseDistance = Double.MaxValue;
                Node eStart = eStop.Start();
                Node eEnd = eStop.End();

                if (eStop.SingleDirection == false)
                {
                    reverseDistance = new ShortestPath(gps, start, eStop.End()).distance + eStop.Distance + new ShortestPath(gps, eStop.Start(), end).distance;
                }

                if (reverseDistance < currentDistance)
                {
                    Node tmp = eStart;
                    eStart = eEnd;
                    eEnd = tmp;
                }

                ShortestPath firstPart = new ShortestPath(gps, start, eStart);
                pathNodes.AddRange(firstPart.pathNodes);
                pathEdges.AddRange(firstPart.pathEdges);

                pathEdges.Add(eStop);

                ShortestPath secondPart = new ShortestPath(gps, eEnd, end);
                pathNodes.AddRange(secondPart.pathNodes);
                pathEdges.AddRange(secondPart.pathEdges);

                distance = firstPart.distance + eStop.Distance + secondPart.distance;
            }
        }

        public void FindShortestPath(Characteristic.CharacteristicTypes type)
        {
            double minDistance = Double.MaxValue;
            stop = null;

            var typeNodes = gps.NodesByChType(type);

            foreach (Node n in typeNodes)
            {
                double currentDistance = new ShortestPath(gps, start, n).distance + new ShortestPath(gps, n, end).distance;

                if (currentDistance < minDistance)
                {
                    minDistance = currentDistance;
                    stop = n;
                }
            }

            var typeEdges = gps.EdgesByChType(type);

            foreach (Edge e in typeEdges)
            {
                double currentDistance = new ShortestPath(gps, start, e.Start()).distance + e.Distance + new ShortestPath(gps, e.End(), end).distance;
                double reverseDistance = Double.MaxValue;

                if(e.SingleDirection == false)
                {
                    reverseDistance = new ShortestPath(gps, start, e.End()).distance + e.Distance + new ShortestPath(gps, e.Start(), end).distance;
                }

                if (currentDistance < minDistance || reverseDistance < minDistance)
                {
                    minDistance = Math.Min(currentDistance, reverseDistance);
                    stop = e;
                }
            }
        }

        public void FindShortestPath()
        {
            SortedSet<Node> rNodes = new SortedSet<Node>();

            foreach (Node n in gps.nodes)
            {
                n.distanceFromStart = Double.MaxValue;
                n.prevEdge = null;
                n.prevNode = null;

                if (n.ElementId == start.ElementId)
                    n.distanceFromStart = 0;

                rNodes.Add(n);
            }

            while (rNodes.Count != 0)
            {
                Node current = rNodes.First();

                if (current.ElementId == end.ElementId)
                {
                    break;
                }

                List<Node> nNodes = gps.NNodes(current);

                foreach (Node nNode in nNodes)
                {
                    double dist = current.distanceFromStart + gps.Distance(current, nNode);

                    if (dist < nNode.distanceFromStart)
                    {
                        rNodes.Remove(nNode);
                        nNode.distanceFromStart = dist;
                        nNode.prevEdge = gps.EdgeBetween(current, nNode);                 
                        nNode.prevNode = current;
                        rNodes.Add(nNode);
                    }
                }

                rNodes.Remove(current);
            }

        }
    }

}