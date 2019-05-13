using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GPS
{
    class GPSDatabase
    {
        GPSContext gpsContext = new GPSContext();

        // cvoru dodaje karakteristiku
        public void NodeAddCharacteristic(Node n, Characteristic.CharacteristicTypes x)
        {          
            Characteristic newChar = new Characteristic();
            newChar.ElementId = n.ElementId;

            newChar.CharacteristicType = x;

            this.gpsContext.Characteristics.Add(newChar);
            this.gpsContext.SaveChanges();
        }

        // bridu dodaje karakteristiku
        public void EdgeAddCharacteristic(Edge n, Characteristic.CharacteristicTypes x)
        {

            Characteristic newChar = new Characteristic();
            newChar.ElementId = n.ElementId;

            newChar.CharacteristicType = x;

            this.gpsContext.Characteristics.Add(newChar);
            this.gpsContext.SaveChanges();
        }

        // vraca imena svih cvorova
        public List<string> GetAllNamesNodes()
        {
            List<string> names = new List<string>();
            foreach(var e in GetAllNodes())
            {
                names.Add(e.Name);
            }
            return names;
        }

        //vraca listu elemenata
        public IQueryable<Element> GetAllElements()
        {
            return gpsContext.Elements;
        }

        // vraca samo cvorove
        public List<Element> GetAllNodes()
        {
            List<Element> nodes = new List<Element>();
            var q = gpsContext.Elements;
            foreach (var p in q)
            {
                if(p.IsNode == true)
                {
                    nodes.Add(p);
                }
            }
            return nodes;
        }

        // vraca samo bridove
        public List<Element> GetAllEdges()
        {
            List<Element> edges = new List<Element>();
            var q = gpsContext.Elements;
            foreach (var p in q)
            {
                if (p.IsNode == false)
                {
                    edges.Add(p);
                }
            }
            return edges;
        }

        // dodaje cvor u bazu
        public Node AddNode(int x, int y, string name)
        {
            Node newNode = new Node();
            newNode.IsNode = true;
            newNode.Name = name;
            newNode.X = x;
            newNode.Y = y;

            this.gpsContext.Elements.Add(newNode);
            this.gpsContext.SaveChanges();
            return newNode;
        }

        // trazi id elementa po imenu
        public int FindIdByName(string name)
        {
            foreach(var e in GetAllNodes())
            {
                if (e.Name == name)
                    return e.ElementId;
            }
            return -1;
        }

        // trazi id elementa po imenu
        public int FindElementIdByName(string name)
        {
            var el = FindElementByName(name);

            return el == null ? -1 : el.ElementId;
        }

        public Element FindElementByName(string name)
        {
            var el = gpsContext.Elements.Where(e => e.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            return el;
        }

        public Node FindNodeByName(string name)
        {
            var el = gpsContext.Elements.OfType<Node>().Where(e => e.Name.ToLower().Equals(name.ToLower())).FirstOrDefault();

            return el;
        }

        public int FindIdEdgeByName(string name)
        {
            foreach (var e in GetAllEdges())
            {
                if (e.Name == name)
                    return e.ElementId;
            }
            return -1;
        }

        // vraca cvor po id-u
        public Node GetNodeById(int id)
        {
            Node n = new Node();
            foreach(var e in GetAllNodes())
            {
                if(e.ElementId == id)
                {
                    n = (Node)e;
                    return n;
                }
            }
            return n;
        }

        // dodaje brid u bazu
        public Edge AddEdge(string s, string e, string name, bool smjer)
        {
            Edge newEdge = new Edge();
            newEdge.IsNode = false;
            newEdge.Name = name;
            int e1 = newEdge.EndId = FindIdByName(e);
            int s1 = newEdge.StartId = FindIdByName(s);
            Node n1 = GetNodeById(s1);
            Node n2 = GetNodeById(e1);
            double d = Util.Distance(n1, n2);
            newEdge.Distance = d;
            newEdge.SingleDirection = smjer;

            this.gpsContext.Elements.Add(newEdge);
            this.gpsContext.SaveChanges();
            return newEdge;
        }

        //vraca element po id
        public Element GetElementById(int id)
        {
            return gpsContext.Elements.FirstOrDefault(c => c.ElementId == id);
        }

        public IQueryable<Characteristic> GetAllCharacteristic()
        {
            return gpsContext.Characteristics;
        }

        //vraca listu karakteristika po id
        public List<Characteristic.CharacteristicTypes> GetCharacteristicsByElementId(int id)
        {
            var q= gpsContext.Characteristics.Where(c => c.ElementId == id);
            List<Characteristic.CharacteristicTypes> list = new List<Characteristic.CharacteristicTypes>();

            foreach (var element in q)
            {
                list.Add(element.CharacteristicType);
            }
            
            return list;
        }


        //vraca sve elemente s danom karakteristikom
        public List<Element> GetAllElementsWithCharacteristics(Characteristic.CharacteristicTypes type)
        {
            List<Element> list = new List<Element>();
            var q = gpsContext.Characteristics.Where(c => c.CharacteristicType==type);

            foreach(var element in q)
            {
                list.Add(GetElementById(element.ElementId));
            }

            return list;
        }

    }
}
