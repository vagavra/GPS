using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GPS
{
    class DrawMap
    {
        GPSDatabase gpsDatabase = new GPSDatabase();
        public int infoEdgeVariable = 0;
   
        // dodavanje čvora
        public void AddNode(int kX, int kY, string name, List<Characteristic.CharacteristicTypes> list)
        {
            ErrorMsg f8 = new ErrorMsg();
            int kontrol = 0;
            Node n = new Node();
            foreach (var elem in gpsDatabase.GetAllNodes())
            {
                var node = (Node)gpsDatabase.GetElementById(elem.ElementId);
                if (node.X == kX && node.Y == kY)
                {
                   f8.ShowDialog();
                   break;
                }
                else if (elem.Name == name)
                {
                   f8.ShowDialog();
                   break;
                }
                else if (kX <= 0 || kX >= 521 || kY <= 0 || kY >= 351)
                {
                    f8.ShowDialog();
                    break;
                }
                else
                {                                      
                    n = gpsDatabase.AddNode(kX, kY, name);
                    kontrol = 1;
                    break;
                }
            }
            //dodao je cvor -> provjerimo treba li karakteristike dodat
            if(kontrol == 1)
            {
                for(int i = 0; i < list.Count(); i++)
                {
                    gpsDatabase.NodeAddCharacteristic(n, list[i]);
                }  
            }
        }

        //dodavanje brida
        public void AddEdge(string start,string end, string name, List<Characteristic.CharacteristicTypes> list, bool smjer)
        {
            ErrorMsg f8 = new ErrorMsg();
            int kontrol = 0;
            Edge e = new Edge();
            foreach (var elem in gpsDatabase.GetAllEdges())
            {
                if (elem.Name == name)
                {
                    f8.ShowDialog();
                    break;
                }
                else
                {
                    e = gpsDatabase.AddEdge(start, end, name, smjer);
                    kontrol = 1;
                    break;
                }
            }
            if (kontrol == 1)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    gpsDatabase.EdgeAddCharacteristic(e, list[i]);
                }
            }
        }

        

        //funkcija koja iscrtava graf
        public void drawGraph(Graphics g, bool street = false)
        {

            g.Clear(Color.White);

            SolidBrush darkGrayBrush = new SolidBrush(Color.DarkGray);
            SolidBrush blackBrush = new SolidBrush(Color.Black);
            SolidBrush lightGrayBrush = new SolidBrush(Color.LightGray);
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            SolidBrush orangeBrush = new SolidBrush(Color.Orange);

            String drawString;
            Font drawFont = new Font("Arial", 8);
            Font drawStreetsFont = new Font("Arial", 7);



            //crtanje bridova
            foreach (var element in gpsDatabase.GetAllEdges())
            {

                var edge = (Edge)element;
                var start = (Node)gpsDatabase.GetElementById(edge.StartId);
                var end = (Node)gpsDatabase.GetElementById(edge.EndId);

                g.DrawLine(new Pen(Color.DarkGray, 4), start.X + 7, start.Y + 7, end.X + 7, end.Y + 7);
                if (edge.SingleDirection==false)
                {
                    g.DrawLine(new Pen(Color.LightGray, 2), start.X + 7, start.Y + 7, end.X + 7, end.Y + 7);
                }
                else
                {
                    g.DrawLine(new Pen(Color.White, 2), start.X + 7, start.Y + 7, end.X + 7, end.Y + 7);
                }
                //ovo crta imena ulica, ali se ne vide bas

                if(street)
                {
                    drawString = edge.Name;
                    int positionX = (start.X + end.X) / 2;

                    int positionY = (start.Y + end.Y) / 2;
                    if (Math.Abs(start.Y - end.Y) < 10)
                    {
                        positionY -= 14;
                        positionX = Math.Min(start.X, end.X) + 5;
                    }
                    g.DrawString(drawString, drawStreetsFont, System.Drawing.Brushes.DarkOrange, positionX + 14, positionY + 8);
                }
            }

            //crtanje cvorova
            foreach (var element in gpsDatabase.GetAllNodes())
            {
                    var node = (Node)element;

                    g.FillEllipse(
                    darkGrayBrush,
                    new RectangleF(node.X, node.Y, 14, 14)
                    );

                    drawString = node.Name;
                    g.DrawString(drawString, drawFont, blackBrush, node.X + 14, node.Y + 8);
                
            }
        }



        //funkcija koja iscrtava put
        public void colorLines(Graphics g, List<Edge> lista)
        {
            
            SolidBrush blueBrush = new SolidBrush(Color.Blue);
            foreach (Edge pe in lista)
            {
                    var first = (Node)gpsDatabase.GetElementById(pe.StartId);
                    var second = (Node)gpsDatabase.GetElementById(pe.EndId);
                    g.DrawLine(new Pen(Color.Blue, 2), first.X + 7, first.Y + 7, second.X + 7, second.Y + 7);
                    g.FillEllipse(
                               blueBrush,
                               new RectangleF(first.X, first.Y, 14, 14)
                    );
                    g.FillEllipse(
                              blueBrush,
                              new RectangleF(second.X, second.Y, 14, 14)
                   );

            }
               
             
        }

        public void setInfoVariable(int x)
        {
            infoEdgeVariable = x;
        }

        
       //crta "zastavicu" na cvoru
        public void drawNode(Graphics g, Node element, int temp = 1)
        {
            int x, y;
            Node node = element;
            Image newImage;
            if (temp == 0)
            {
                newImage = Image.FromFile(@"../../Pictures/location1.png");
                x = node.X - newImage.Width / 2 + 10;

            }
            else
            {
                newImage = Image.FromFile(@"../../Pictures/location.png");
                x = node.X - newImage.Width / 2 + 7;


            }

            y = node.Y - newImage.Height / 2-4;
            g.DrawImageUnscaled(newImage, x, y);

        }

        //crta "zastavicu" na bridu
        public void drawEdge(Graphics g, Edge element, int temp = 1)
        {
            int x, y;
            Image newImage;

            if (temp == 0)
            {
                newImage = Image.FromFile(@"../../Pictures/location1.png");
            }
            else
            {
                newImage = Image.FromFile(@"../../Pictures/location.png");

            }

            Edge edge = element;
            var start = (Node)gpsDatabase.GetElementById(edge.StartId);
            var end = (Node)gpsDatabase.GetElementById(edge.EndId);

            var positionX = ((start.X + end.X) / 2) + 4;
            var positionY = ((start.Y + end.Y) / 2) + 4;
            x = positionX - newImage.Width / 2;
            y = positionY - newImage.Height / 2;
            g.DrawImageUnscaled(newImage, x, y);

        }


        public void drawNodeOrEdgeLocation(Graphics g, int id, int temp = 1)
        {
          
            Element element=gpsDatabase.GetElementById(id);

            if (element.IsNode)
            {
              
                drawNode(g,(Node)element,temp);
            }
            else
            {
                drawEdge(g, (Edge)element,temp);

            }
        }


        public void drawAllElementsWithCharacteristic(Graphics g, List<Element> list)
        {
            
            drawGraph(g);

            foreach (var element in list)
            {
                if (element.IsNode)
                {
                    Node node = (Node)element;
                    drawNode(g,node);

                }
                else
                {
                    Edge edge = (Edge)element;
                    drawEdge(g, edge);

                }
            }

        }
    }
}
