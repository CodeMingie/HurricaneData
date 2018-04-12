using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HurDatForms
{
    public class Point
    {
        public decimal x;
        public decimal y;
    }
    
    public class StateLookup
    {
        private Dictionary<string, List<Point>> stateInfo;

        public StateLookup()
        {
            stateInfo = LoadStateInfo();
        }

        public List<string> GetStates()
        {
            return stateInfo.Keys.ToList();
        }

        //Load state info from xml file into dictionary of state name and list of verticies
        private Dictionary<string, List<Point>> LoadStateInfo()
        {
            XElement xelement = XElement.Load("Files//states.xml");
            IEnumerable<XElement> states = xelement.Elements();

            Dictionary<string, List<Point>> stateInfoTemp = new Dictionary<string, List<Point>>();
            foreach (var state in states)
            {
                List<Point> vertices = new List<Point>();
                string stateName = state.Attribute("name").Value;
                foreach (var point in state.Elements())
                {
                    string lat = point.Attribute("lat").Value;
                    string lng = point.Attribute("lng").Value;

                    Point p = new Point();
                    p.x = Convert.ToDecimal(lng);
                    p.y = Convert.ToDecimal(lat);
                    vertices.Add(p);
                }

                stateInfoTemp.Add(stateName, vertices);
            }

            return stateInfoTemp;
        }

        //Given a polygon with vertices, check if point is inside the polygon
        //by drawing a ray from point to the right parallel of the x axis.
        //If number of intersections is odd then point is inside polygon
        private bool PointInPolygon(Point point, List<Point> vertices)
        {
            int i, j, nvert = vertices.Count;
            bool c = false;

            //Loop through the verticies using i. j trails i.
            for (i = 0, j = nvert - 1; i < nvert; j = i++)
            {
                //Check if the point is between the two verticies
                if (((vertices[i].y >= point.y) != (vertices[j].y >= point.y)) &&
                    //Check whether the intersection is to the right of point
                    (point.x <= (vertices[j].x - vertices[i].x) * (point.y - vertices[i].y) / (vertices[j].y - vertices[i].y) + vertices[i].x)
                  ) 
                    c = !c;
            }

            return c;
        }

        public string GetStateFromCor(decimal lng, decimal lat)
        {
            Point point = new Point { x = lng, y = lat };
            string inState = "";

            foreach (string state in stateInfo.Keys)
            {
                if (this.PointInPolygon(point, stateInfo[state]))
                {
                    inState = state;
                    break;
                }
            }

            return inState;
        }


    }




}
