using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PlateControl;

namespace CircularPlate
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            //IList<Target> data1 = new List<Target>();

            //data1.Add(new Target { Id = 1, Color = System.Windows.Media.Color.FromRgb(125, 80, 125), Description = "test1" });
            //data1.Add(new Target { Id = 1, Color = System.Windows.Media.Color.FromRgb(125, 50, 125), Description = "test1" });
            //data1.Add(new Target { Id = 1, Color = System.Windows.Media.Color.FromRgb(125, 90, 125), Description = "test1" });

            //TestViewModel t1 = new TestViewModel();
            //t1.TargetDataVM = data1;

            //this.DataContext = t1;

            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            canvas1.Children.Clear();
            CreateCircles();
        }

        public void CreateCircles()
        {
            Geometry geometry;
            Path path;
            double angle;
            double CanvasWidth;
            double CanvasHeight;
            // double totalps;
            CanvasWidth = 200;
            CanvasHeight = 200;
            double DiameterofShapes = 20;
            double perimeter = (2 * Math.PI * CanvasWidth) / 2;
            double noOfShapes = perimeter / DiameterofShapes;
            double Padding = 1;

            //Logic to calculate radius of Circumference based on number of shapes
            //noOfShapes = 40;
            noOfShapes = (double)lblNoofshapes.Content;
            double RequiredPerimeterCircumference = (noOfShapes * DiameterofShapes / Math.PI);
            //accomodate padding specidifed
            RequiredPerimeterCircumference = RequiredPerimeterCircumference + noOfShapes * Padding;

            CanvasWidth = RequiredPerimeterCircumference;
            CanvasHeight = RequiredPerimeterCircumference;
            //end

            // totalps = 20;
            //totalps = noOfShapes;
            angle = 90;

            angle = DegreeToRadian(angle);
            double angleShare = 360 / noOfShapes;
            // for (double i = 0; i < 360; i = i + angleShare)
            for (double i = 0; i < noOfShapes; i++)
            {
                path = new Path();
                geometry = new PathGeometry();
                double x = Math.Cos(angle) * CanvasWidth / 2 + CanvasWidth / 2;
                double y = Math.Sin(angle) * CanvasHeight / 2 + CanvasHeight / 2;
                LineSegment lingeSeg = new LineSegment(new Point(x, y), true);

                ArcSegment arcSeg = new ArcSegment(new Point(x, y), new Size(CanvasWidth / 2, CanvasHeight / 2), angleShare, angleShare > 180, SweepDirection.Clockwise, false);
                LineSegment lingeSeg2 = new LineSegment(new Point(CanvasWidth / 2, CanvasHeight / 2), true);
                PathFigure fig = new PathFigure(new Point(CanvasWidth / 2, CanvasHeight / 2), new PathSegment[] { lingeSeg }, false);

                ((PathGeometry)geometry).Figures.Add(fig);
                angle += DegreeToRadian(angleShare);

                //label
                System.Windows.Controls.Label l1 = new System.Windows.Controls.Label();
                l1.Content = i;
                l1.Margin = new Thickness(x, y, 0, 0);

                //button
                System.Windows.Controls.Button b1 = new System.Windows.Controls.Button();
                b1.Content = i;
                b1.Width = 20;
                b1.Height = 20;
                b1.Margin = new Thickness(x, y, 0, 0);

                ////circle
                Ellipse c1 = new Ellipse();
                c1.StrokeThickness = 2;
                c1.Stroke = Brushes.Blue;
                c1.Width = 20;
                c1.Height = 20;
                c1.Margin = new Thickness(x, y, 0, 0);

                path.Fill = new SolidColorBrush(Colors.Black);
                path.Stroke = new SolidColorBrush(Colors.Black);
                path.StrokeThickness = 5;

                path.Data = geometry;
                //canvas1.Children.Add(path);
                // canvas1.Children.Add(l1);
                canvas1.Children.Add(b1);
                //canvas1.Children.Add(c1);
            }
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private void rowsslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            /////
            Assignment A1 = new Assignment();
            PlateItem p1 = new PlateItem();
            p1.OrderType = PositionContainment.ControlsOnly;
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.35, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            p1.OutputPlateTargets.Add(new Target { Name = "target3", TargetType = TargetType.IC, Concentration = 1.05, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
            A1.OutputPlateItemLists.Add(p1);

            //////////
            canvas1.Children.Clear();
            WellPositionControl wp = new WellPositionControl();
            wp.Legend = PositionContainment.Empty;
            wp.CanvasWidth = 35;
            wp.CanvasHeight = 35;
            wp.WellDiameter = 15;

            //wp.Legend = PositionContantment.Controls;
            wp.IsInputPlate = false;
            wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            // wp.CanvasHeight = 50;
            //wp.CanvasWidth = 50;
            wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(wp_MouseDown);
            wp.Legend = PositionContainment.StandardsOnly;
            wp.Targets = A1.OutputPlateItemLists[0];
            // wp.Targets
            //wp.CanvasHeight = DiameterEachShape;
            //wp.CanvasWidth = DiameterEachShape;
            //wp.WellSelectionSize = RectWellSelectionSize;

            //wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(wp_MouseDown);

            //wp.SetValue(Grid.RowProperty, row);
            //wp.SetValue(Grid.ColumnProperty, col);

            // baseChar = baseChar + 1;
            //wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };

            //// PosX  = Column no, PosY = Row No, array will be accessed as Array[Row][Column].
            // wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };

            //if (PositionContainmentType == PositionContainment.AnyKind)
            //{
            //    wp.AnyKindShapeFillColor = Colors.Tomato;
            //}
            canvas1.Children.Add(wp);
        }

        private void wp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string s = "ets";
        }
    }
}