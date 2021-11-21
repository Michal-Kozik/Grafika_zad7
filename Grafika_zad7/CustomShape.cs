using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Grafika_zad7
{
    class CustomShape : Shape
    {
        private LineGeometry line = new LineGeometry();
        private GeometryGroup geometryGroup;
        private List<Point> points;

        public CustomShape()
        {
            geometryGroup = new GeometryGroup();
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                return geometryGroup;
            }
        }

        public void MakeShape(List<Point> points)
        {
            this.points = points;
            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1)
                {
                    geometryGroup.Children.Add(new LineGeometry(points[i], points[0]));
                }
                else
                {
                    geometryGroup.Children.Add(new LineGeometry(points[i], points[i + 1]));
                }
            }
        }

        //public void MakeShape()
        //{
        //    geometryGroup.Children.Add(new LineGeometry(new Point(10, 10), new Point(50, 50)));
        //    geometryGroup.Children.Add(new LineGeometry(new Point(50, 50), new Point(50, 100)));
        //}
    }
}
