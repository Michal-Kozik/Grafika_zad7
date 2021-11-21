﻿using System;
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
        Collection<List<Rectangle>> shapes;

        List<Rectangle> currentShape;
        List<Rectangle> previousShape;

        public MainWindow()
        {
            InitializeComponent();
            points = new List<Point>();
            rectangles = new List<Rectangle>();
            shapes = new Collection<List<Rectangle>>();
            currentShape = new List<Rectangle>();
            previousShape = new List<Rectangle>();
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
            rectangle.SetValue(Canvas.LeftProperty, point.X - 2);
            rectangle.SetValue(Canvas.TopProperty, point.Y - 2);
            rectangle.Width = 5;
            rectangle.Height = 5;
            rectangle.Stroke = Brushes.Black;
            rectangle.Fill = new SolidColorBrush(Colors.White);
            rectangle.StrokeThickness = 2;
            rectangle.MouseLeftButtonDown += new MouseButtonEventHandler(CheckShape);
            rectangles.Add(rectangle);

            canvas.Children.Add(rectangle);
        }

        private void CheckShape(object sender, MouseButtonEventArgs e)
        {
            Rectangle clickedRectangle = sender as Rectangle;
            // Poprzednia figura.
            foreach (Rectangle rectangle in previousShape)
            {
                rectangle.Stroke = Brushes.Black;
            }
            // Obecna figura.
            foreach (List<Rectangle> shape in shapes)
            {
                if (shape.Contains(clickedRectangle))
                {
                    foreach (Rectangle rectangle in shape)
                    {
                        rectangle.Stroke = Brushes.LightGreen;
                    }
                    return;
                }
            }
        }

        private void CreateCustomShape(object sender, RoutedEventArgs e)
        {
            CustomShape customShape = new CustomShape();
            customShape.MakeShape(points);
            customShape.Stroke = Brushes.Red;
            customShape.StrokeThickness = 1;
            canvas.Children.Add(customShape);

            previousShape = new List<Rectangle>(currentShape);
            currentShape = new List<Rectangle>(rectangles);

            points.Clear();
            
            shapes.Add(new List<Rectangle>(rectangles));
            rectangles.Clear();
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
    }
}
