using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Grafika_zad7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool IS_DRAWING_ENABLE;

        List<Point> points;
        List<Rectangle> rectangles;
        Collection<CustomShape> shapes;

        CustomShape currentShape;

        public MainWindow()
        {
            InitializeComponent();
            points = new List<Point>();
            rectangles = new List<Rectangle>();
            shapes = new Collection<CustomShape>();
            IS_DRAWING_ENABLE = true;
        }

        private void AddPoint(object sender, MouseButtonEventArgs e)
        {
            if (!IS_DRAWING_ENABLE)
            {
                return;
            }

            Point point = e.GetPosition(canvas);
            points.Add(point);

            Rectangle rectangle = new Rectangle();
            rectangle.SetValue(Canvas.LeftProperty, point.X - 3);
            rectangle.SetValue(Canvas.TopProperty, point.Y - 3);
            rectangle.Width = 7;
            rectangle.Height = 7;
            rectangle.Stroke = Brushes.Black;
            rectangle.Fill = new SolidColorBrush(Colors.White);
            rectangle.StrokeThickness = 2;
            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(CheckShape);
            rectangles.Add(rectangle);

            canvas.Children.Add(rectangle);
        }

        private void CreateCustomShape(object sender, RoutedEventArgs e)
        {
            CustomShape customShape = new CustomShape(rectangles, points);
            customShape.Stroke = Brushes.Red;
            customShape.StrokeThickness = 1;
            canvas.Children.Add(customShape);

            shapes.Add(customShape);
            points.Clear();
            rectangles.Clear();
        }

        private void CheckShape(object sender, MouseButtonEventArgs e)
        {
            Rectangle clickedRectangle = sender as Rectangle;
            // Poprzednia figura.
            if (currentShape != null)
            {
                foreach (Rectangle rectangle in currentShape.GetRectangles())
                {
                    rectangle.Stroke = Brushes.Black;
                }
            }
            // Obecna figura.
            foreach (CustomShape shape in shapes)
            {
                List<Rectangle> shapeRectangles = shape.GetRectangles();
                if (shapeRectangles.Contains(clickedRectangle))
                {
                    foreach (Rectangle rectangle in shapeRectangles)
                    {
                        rectangle.Stroke = Brushes.OrangeRed;
                    }
                    currentShape = shape;
                    moveShapeButton.IsEnabled = true;
                    rotateShapeButton.IsEnabled = true;
                    return;
                }
            }
        }

        

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point pt = e.GetPosition(canvas);
            mousePosition.Content = $"({pt.X:N0}, {pt.Y:N0})";
        }

        private void DisableDrawing(object sender, RoutedEventArgs e)
        {
            IS_DRAWING_ENABLE = false;
        }

        private void EnableDrawing(object sender, RoutedEventArgs e)
        {
            IS_DRAWING_ENABLE = true;
        }

        private void MoveShape(object sender, RoutedEventArgs e)
        {
            // TODO: Walidacja danych.
            int x = Int32.Parse(inputX.Text);
            int y = Int32.Parse(inputY.Text);
            Vector vector = new Vector(x, y);
            currentShape.MoveShape(vector);

            // Aktualizacja listy.
            List<Rectangle> rectangles = currentShape.GetRectangles();
            List<Point> points = currentShape.GetPoints();
            for (int i = 0; i < rectangles.Count; i++)
            {
                rectangles[i].SetValue(Canvas.LeftProperty, points[i].X - 3);
                rectangles[i].SetValue(Canvas.TopProperty, points[i].Y - 3);
            }
        }

        private void RotateShape(object sender, RoutedEventArgs e)
        {
            // TODO: Walidacja danych.
            int x = Int32.Parse(inputRotateX.Text);
            int y = Int32.Parse(inputRotateY.Text);
            int angle = Int32.Parse(inputRotateAngle.Text);
            Point centerPoint = new Point(x, y);
            currentShape.RotateShape(centerPoint, angle);

            // Aktualizacja listy.
            List<Rectangle> rectangles = currentShape.GetRectangles();
            List<Point> points = currentShape.GetPoints();
            for (int i = 0; i < rectangles.Count; i++)
            {
                rectangles[i].SetValue(Canvas.LeftProperty, points[i].X - 3);
                rectangles[i].SetValue(Canvas.TopProperty, points[i].Y - 3);
            }
        }
    }
}
