using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GPS

{

    class GPSNavigation

    {

        public GPSContext context;
        public List<Node> nodes;
        public List<Edge> edges;
        public IQueryable<Node> qNodes;
        public IQueryable<Edge> qEdges;

        public GPSNavigation()
        {
            context = new GPSContext();

            qNodes = context.Elements.OfType<Node>();
            qEdges = context.Elements.OfType<Edge>();
            nodes = qNodes.ToList();
            edges = qEdges.ToList();
        }

        public double Distance(Node start, Node end)
        {
            Edge edge = EdgeBetween(start, end);

            return edge == null ? Double.MaxValue : edge.Distance; 
        }

        public List<Node> NodesByChType(Characteristic.CharacteristicTypes type)
        {
            var typeCharacteristics = CharacteristicsByType(type);

            var typeNodes = from characteristic in typeCharacteristics
                            join node in qNodes
                            on characteristic.ElementId equals node.ElementId
                            select node;

            return typeNodes.ToList();
        }

        public List<Edge> EdgesByChType(Characteristic.CharacteristicTypes type)
        {
            var typeCharacteristics = CharacteristicsByType(type);

            var typeEdges = from characteristic in typeCharacteristics
                            join edge in qEdges
                            on characteristic.ElementId equals edge.ElementId
                            select edge;

            return typeEdges.ToList();
        }

        public IQueryable<Characteristic> CharacteristicsByType(Characteristic.CharacteristicTypes type)
        {
            return context.Characteristics.Where(c => c.CharacteristicType == type);
        }

        public Edge EdgeBetween(Node start, Node end)
        {
            return qEdges.Where(
                e => ((e.StartId == start.ElementId && e.EndId == end.ElementId)
                   || (e.EndId == start.ElementId && e.StartId == end.ElementId && e.SingleDirection == false))
                ).FirstOrDefault();
        }

        public Node GetNodeById(int id)
        {
            foreach(var el in nodes)
            {
                if (el.ElementId == id)
                {
                    return el;
                }
            }
            return new Node();
        }

        public List<Node> NNodes(Node n1)

        {
            List<Node> nNodes = new List<Node>();

            foreach(Node n2 in nodes)
            {
                if(n2.ElementId != n1.ElementId && Distance(n1, n2) != Double.MaxValue)
                {
                    nNodes.Add(n2);
                }
            }

            return nNodes;
        }
    }
}