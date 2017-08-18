using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using PlateControl;

namespace CircularPlate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Assignment A1;

        public void GenerateDummyOutputData(int count)
        {
            A1.OutputPlateItemLists.Clear();

            //p1.OrderType = PositionContainment.Sample;
            //p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            //p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            //A1.OutputPlateItemLists.Add(p1);

            //p1 = new PlateItem();
            //p1.OrderType = PositionContainment.Standard;
            //p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            //p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.35, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            //p1.OutputPlateTargets.Add(new Target { Name = "target3", TargetType = TargetType.IC, Concentration = 1.05, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
            //A1.OutputPlateItemLists.Add(p1);

            //p1 = new PlateItem();
            //p1.OrderType = PositionContainment.Control;
            //p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 2.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) }); //original(224, 0, 60)  test(125, 80, 120)
            //p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.225, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) }); //original(15, 168, 1)  test(150, 18, 11)
            //p1.OutputPlateTargets.Add(new Target { Name = "target3", TargetType = TargetType.IC, Concentration = 1.11, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });//original(0, 0, 255) test(0, 80, 25)
            //p1.OutputPlateTargets.Add(new Target { Name = "target4", TargetType = TargetType.IC, Concentration = 0.25, Color = System.Windows.Media.Color.FromRgb(255, 140, 0) });//original(255, 140, 0) test(125, 100, 50)
            //A1.OutputPlateItemLists.Add(p1);
            //for (int i = 0; i < slidersample.Value; i++)
            //{
            //    p1 = new PlateItem();
            //    p1.OrderType = PositionContainment.Control;
            //    p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            //    p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.35, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            //    p1.OutputPlateTargets.Add(new Target { Name = "target3", TargetType = TargetType.IC, Concentration = 1.05, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
            //    A1.OutputPlateItemLists.Add(p1);
            //}
            // p1 = new PlateItem();
            PlateItem p1;
            p1 = new PlateItem();
            // p1.OrderType = PositionContainment.Blocked;
            //A1.OutputPlateItemLists.Add(p1);

            //p1 = new PlateItem();
            //p1.OrderType = PositionContainment.Calibration;
            //A1.OutputPlateItemLists.Add(p1);

            p1 = new PlateItem();
            p1.OrderType = PositionContainment.AnyKind;
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
            A1.OutputPlateItemLists.Add(p1);

            p1 = new PlateItem();
            p1.OrderType = PositionContainment.StandardsOnly;
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
            A1.OutputPlateItemLists.Add(p1);

            for (int i = 0; i < 10; i++)
            {
                p1 = new PlateItem();
                p1.OrderType = PositionContainment.ControlsOnly;
                p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
                p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
                p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
                p1.OutputPlateTargets.Add(new Target { Name = "target4", TargetType = TargetType.IC, Concentration = 0.25, Color = System.Windows.Media.Color.FromRgb(255, 140, 0) });//original(255, 140, 0) test(125, 100, 50)
                A1.OutputPlateItemLists.Add(p1);
            }
        }

        private void rad96_Click(object sender, RoutedEventArgs e)
        {
            GenerateDummyOutputData(5);
            t1.IsInputPlateVM = (bool)ChkInputcontrol.IsChecked;
            t1.IsReadOnlyVM = (bool)chkIsReadonly.IsChecked;
            t1.AssignmentVM = A1;
            t1.DiameterEachShapeVM = 17;
            t1.RowSpacingVM = 11;
            t1.ColSpacingVM = 11;
            t1.RowShapesVM = 8;
            t1.ColShapesVM = 12;
        }

        private void rad384_Click(object sender, RoutedEventArgs e)
        {
            GenerateDummyOutputData(15);
            t1.IsInputPlateVM = (bool)ChkInputcontrol.IsChecked;
            t1.IsReadOnlyVM = (bool)chkIsReadonly.IsChecked;
            t1.AssignmentVM = A1;
            t1.DiameterEachShapeVM = 12;
            t1.RowSpacingVM = 7;
            t1.ColSpacingVM = 7;
            t1.RowShapesVM = 16;
            t1.ColShapesVM = 24;
        }

        private TestViewModel t1;

        public MainWindow()
        {
            InitializeComponent();

            A1 = new Assignment();
            Random rand = new Random(255);
            byte[] colorBytes = new byte[3];
            //System.Windows.Media.Color randomColor;
            GenerateDummyOutputData(12);

            t1 = new TestViewModel();
            t1.RowShapesVM = 8;
            t1.ColShapesVM = 12;
            t1.IsInputPlateVM = false;
            t1.ControlType = PositionContainment.SamplesOnly;
            t1.DiameterEachShapeVM = 25;
            t1.RowSpacingVM = 5;
            t1.ColSpacingVM = 5;
            t1.AssignmentVM = A1;
            t1.IsReadOnlyVM = (bool)chkIsReadonly.IsChecked;
            this.DataContext = t1;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            GenerateDummyOutputData(5);
            t1.RowShapesVM = 16;
            t1.ColShapesVM = 24;

            t1.AssignmentVM = A1;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            CreateCordinates();
            //CreateCircles();
        }

        public void GenerateInputData()
        {
            InputAssignment inputAssign = new InputAssignment();
            InputSample s1 = new InputSample();
            //s1.InputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(125, 80, 120) });

            inputAssign.Samples.Add(s1);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public void CreateCordinates()
        {
            Geometry geometry;
            Path path;
            double angle;
            double CanvasWidth;
            double CanvasHeight;
            double totalps;
            CanvasWidth = 200;
            CanvasHeight = 200;
            totalps = 4;
            angle = 45;
            angle = DegreeToRadian(angle);
            double angleShare = 360 / totalps;
            for (double i = 0; i < 360; i = i + angleShare)
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

                path.Fill = new SolidColorBrush(Colors.Black);
                path.Stroke = new SolidColorBrush(Colors.Black);
                path.StrokeThickness = 5;

                path.Data = geometry;
                canvas1.Children.Add(path);
                canvas1.Children.Add(l1);
                //canvas1.Children.Add(b1);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int a = 5;
            int b = 6;
            int c = a >= b ? a : b;
            int d = a <= b ? a : b;
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            IList<Target> data1 = new List<Target>();
            for (int i = 0; i < 3; i++)
            {
                data1.Add(new Target { Color = System.Windows.Media.Color.FromRgb(10, 10, 10) });
            }

            WellPositionControl wp = new WellPositionControl();
            canvas1.Children.Add(wp);

            //ResourceDictionary myResources = new ResourceDictionary();
            //myResources.Source = new Uri(@"\Themes\Generic.xaml", UriKind.RelativeOrAbsolute);

            //Style testStyle = FindResource("WellPositionStyle") as Style;
            //wp.Style = testStyle;

            //////wp.Legend = PositionContantment.Blocked;
            //////wp.IsInputControl = false;
            //////wp.SelectedHighlightColor = Colors.Cyan;
            //////wp.SelectedHighlightColor = Colors.LightGreen;
            //////wp.CanvasHeight = 100;
            //////wp.CanvasWidth = 100;
            ////////wp.ApplyTemplate();
            //////wp.Targets = data1;
            //////wp.IsSelected = false;
            //////wp.IsDrawTargetLegend = true;
            //////wp.IsFilledLegend = false;
            ////////wp.GenerateShape();

            //////canvas1.Children.Add(wp);
            ////////grid1.Children.Add(b1);
        }

        public void eventhandler1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked");
        }

        private RectanglePlateControl r1;

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<WellPositionControl> mytypes = ControlFinder<WellPositionControl>.FindControl(r1);
            //   IEnumerator test = mytypes.GetEnumerator();

            double posx = 1;
            double posy = 1;
            //var ControlRow = AllControls.FindAll(p => p.WellPosition.PositionX == row);
            foreach (WellPositionControl wp in mytypes)
            {
                if (wp.WellPosition.PositionX == posx && wp.WellPosition.PositionY == posy)
                {
                    wp.Legend = PositionContainment.AnyKind;
                    wp.IsSelected = false;
                    posx = posx + 1;
                    if (posx == 4)
                    {
                        break;
                    }
                }
            }

            //var ControlRow = r1. AllControls.FindAll(p => p.WellPosition.PositionX == row);
            //foreach (WellPositionControl wp in ControlRow)
            //{
            //    wp.IsSelected = true;
            //}

            // WellPositionControl wp = (WellPositionControl)r1.AllControls["A1"];
            //wp.IsSelected = true;

            //wp = (WellPositionControl)r1.AllControls["A2"];
            //wp.IsSelected = true;

            //wp = (WellPositionControl)r1.AllControls["A3"];
            //wp.IsSelected = true;
        }

        private void button1_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void rad96_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void rad384_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void button1_Click_2(object sender, RoutedEventArgs e)
        {
            Circular w1 = new Circular();
            // Window1 w1 = new Window1();
            w1.Show();
        }

        private void chkIsReadonly_Click(object sender, RoutedEventArgs e)
        {
            rad96_Click(null, null);
        }
    }

    public static class ControlFinder<T>
    {
        public static IEnumerable<T> FindControl(DependencyObject root)
        {
            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; ++i)
            {
                dynamic child = VisualTreeHelper.GetChild(root, i);
                if (child is T)
                {
                    yield return (T)child;
                }
                else
                {
                    foreach (T found in FindControl(child))
                    {
                        yield return found;
                    }
                }
            }
        }
    }
}