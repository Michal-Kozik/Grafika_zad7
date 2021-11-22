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
        //private LineGeometry line = new LineGeometry();
        private GeometryGroup geometryGroup;
        private List<Point> points;
        private List<Rectangle> rectangles;

        public CustomShape(List<Rectangle> rectangles, List<Point> points)
        {
            geometryGroup = new GeometryGroup();
            this.rectangles = new List<Rectangle>(rectangles);
            this.points = new List<Point>(points);
            MakeShape();
        }

        public List<Rectangle> GetRectangles()
        {
            return rectangles;
        }

        public void SetRectangles(List<Rectangle> rectangles)
        {
            this.rectangles = rectangles;
        }

        public List<Point> GetPoints()
        {
            return points;
        }

        public void SetPoints(List<Point> points)
        {
            this.points = points;
        }

        private void Refresh()
        {
            StrokeThickness = 2;
            StrokeThickness = 1;
        }



        protected override Geometry DefiningGeometry
        {
            get
            {
                return geometryGroup;
            }
        }

        public void MakeShape()
        {
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

        public void MoveShape(Vector vector)
        {
            GeometryGroup updatedGeometryGroup = new GeometryGroup();
            for (int i = 0; i < points.Count; i++)
            {
                if (i == points.Count - 1)
                {
                    updatedGeometryGroup.Children.Add(new LineGeometry(Point.Add(points[i], vector), Point.Add(points[0], vector)));
                }
                else
                {
                    updatedGeometryGroup.Children.Add(new LineGeometry(Point.Add(points[i], vector), Point.Add(points[i + 1], vector)));
                }
            }
            // Zaktualizowanie kolekcji.
            for (int i = 0; i < points.Count; i++)
            {
                points[i] = Point.Add(points[i], vector);
            }
            geometryGroup = updatedGeometryGroup;
            Refresh();
        }

        
    }
}
