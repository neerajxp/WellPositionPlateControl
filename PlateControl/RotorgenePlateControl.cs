using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using RotorPlateControl;

namespace PlateControl
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PlateControl"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:PlateControl;assembly=PlateControl"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:RotorgenePlateControl/>
    ///
    /// </summary>
    public class RotorgenePlateControl : Control
    {
        private const string PartCanvas = "PART_ROTORGENE_CANVAS";
        private const string PartScrollviewer = "PART_ROTORGENE_SCROLLVIEWER";

        private Canvas canvas;
        private ScrollViewer scrollViewer;
        private WellPositionControl[] children;

        private const int DefaultWellDiameter = 12;
        private const int DefaultWellsCount = 100;
        private const int DefaultWellPadding = 3;
        private const int DefaultRotorWellSelectionSize = 25;
        private int wellCanvasSize = 20;

        private List<string> LabelList;

        static RotorgenePlateControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(typeof(RotorgenePlateControl)));
        }

        public void CreateLabels()
        {
            LabelList = new List<string>();
            LabelList.Add("A1");
            LabelList.Add("B1");
            LabelList.Add("C1");
            LabelList.Add("D1");
            LabelList.Add("E1");
            LabelList.Add("F1");
            LabelList.Add("G1");
            LabelList.Add("H1");
            LabelList.Add("H12");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            IsReadonly = false;
            CreateCircles();
        }

        private double CanvasWidth;
        private double CanvasHeight;
        private double angle;
        private double RequiredPerimeterCircumferenceLabel;

        public void CreateCircles()
        {
            IsAllowWellSelection = true;
            wellCanvasSize = WellDiameter + WellPadding + 10;

            CreateLabels();
            Geometry geometry;
            Path path;
            // double CanvasWidth;
            //double CanvasHeight;
            angle = 90;

            canvas = (Canvas)GetTemplateChild(PartCanvas);
            canvas.Children.Clear();
            scrollViewer = (ScrollViewer)GetTemplateChild(PartScrollviewer);
            children = new WellPositionControl[(int)WellsCount];

            double templabelWellDiameter = WellDiameter + 1;
            //double RequiredPerimeterCircumference = (WellsCount * WellDiameter / Math.PI);
            double RequiredPerimeterCircumference = (WellsCount * wellCanvasSize / Math.PI);
            RequiredPerimeterCircumferenceLabel = (WellsCount * templabelWellDiameter / Math.PI);
            //accomodate padding specidifed
            //RequiredPerimeterCircumference = RequiredPerimeterCircumference + WellsCount * WellPadding;
            RequiredPerimeterCircumferenceLabel = RequiredPerimeterCircumferenceLabel + WellsCount * WellPadding;

            CanvasWidth = RequiredPerimeterCircumference;
            CanvasHeight = RequiredPerimeterCircumference;

            canvas.Width = CanvasWidth + 80;
            canvas.Height = CanvasHeight + 80;

            scrollViewer.Width = 450;
            scrollViewer.Height = 350;

            //totalps = 20;
            //totalps = noOfShapes;
            //angle = 90;

            angle = DegreeToRadian(angle);
            double angleShare = 360 / (double)WellsCount;
            //for (double i = 0; i < 360; i = i + angleShare)
            int posY = 0;
            for (int i = 0; i < WellsCount; i++)
            {
                path = new Path();
                geometry = new PathGeometry();
                double x = Math.Cos(angle) * CanvasWidth / 2 + CanvasWidth / 2;
                double y = Math.Sin(angle) * CanvasHeight / 2 + CanvasHeight / 2;
                // LineSegment lingeSeg = new LineSegment(new Point(x, y), true);

                //ArcSegment arcSeg = new ArcSegment(new Point(x, y), new Size(CanvasWidth / 2, CanvasHeight / 2), angleShare, angleShare > 180, SweepDirection.Clockwise, false);
                // LineSegment lingeSeg2 = new LineSegment(new Point(CanvasWidth / 2, CanvasHeight / 2), true);
                //PathFigure fig = new PathFigure(new Point(CanvasWidth / 2, CanvasHeight / 2), new PathSegment[] { lingeSeg }, false);

                //((PathGeometry)geometry).Figures.Add(fig);

                ////label
                //System.Windows.Controls.Label l1 = new System.Windows.Controls.Label();
                //l1.Content = i;
                //l1.BorderThickness = new Thickness(1);
                //l1.BorderBrush = new SolidColorBrush(Colors.Red);
                //l1.Margin = new Thickness(x + 25, y, 0, 0);

                WellPositionControl wp = new WellPositionControl();
                wp.Legend = PositionContainment.Empty;
                wp.IsInputPlate = IsWellInputPlate;
                wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                wp.CanvasHeight = WellDiameter + WellPadding;
                wp.CanvasWidth = WellDiameter + WellPadding;
                wp.WellDiameter = WellDiameter;
                wp.WellSelectionSize = RotorWellSelectionSize;
                wp.Margin = new Thickness(x + 30, y + 20, 0, 0);
                wp.WellPosition = new WellPosition() { PositionX = i + 1, PositionY = posY + 1 };
                wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(wp_MouseDown);

                children[i] = wp;
                canvas.Children.Add(wp);

                if (LabelList.Contains(wp.WellPosition.ToString()))
                {
                    ShowLabel(wp);
                }

                //Positioning to be in A1,A2,A3 order.
                if ((i + 1) % 12 == 0)
                {
                    posY = posY + 1;
                }
                angle += DegreeToRadian(angleShare);
            }

            if (IsWellInputPlate == false)
            {
                AppyOutputPlateItemsOnGrid();
            }
            else
            {
                AppyInputPlateItemsOnGrid();
            }

            scrollViewer.ScrollToBottom();

            if (IsReadonly == true)
            {
                Label l1 = new Label();
                l1.Background = new SolidColorBrush(Colors.Aqua);
                l1.Background.Opacity = 0;
                l1.Height = CanvasHeight + 80;
                l1.Width = CanvasWidth + 80;
                canvas.Children.Add(l1);
            }
        }

        public void ShowLabel(WellPositionControl wp)
        {
            Label lblRowHeader = new Label();
            lblRowHeader.Content = wp.WellPosition.ToString();
            lblRowHeader.FontFamily = new FontFamily("Arial");
            lblRowHeader.FontWeight = FontWeights.Bold;
            lblRowHeader.FontSize = 10;
            lblRowHeader.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
            lblRowHeader.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;

            //lblRowHeader.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            //lblRowHeader.SetValue(Grid.RowProperty, row);
            //lblRowHeader.SetValue(Grid.ColumnProperty, 0);
            //lblRowHeader.Margin = new Thickness(wp.Margin.Left, wp.Margin.Top + 20, wp.Margin.Right, wp.Margin.Bottom);

            //to see control at the center of shapes
            lblRowHeader.Height = 20;
            lblRowHeader.Padding = new Thickness(0);
            lblRowHeader.BorderThickness = new Thickness(0);
            lblRowHeader.BorderBrush = new SolidColorBrush(Colors.Black);
            //used to have proper lines. sometimes dark-light line comes in pixels
            lblRowHeader.UseLayoutRounding = true;

            if (wp.WellPosition.ToString() == "A1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left + 5, wp.Margin.Top + 25, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "B1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left - 18, wp.Margin.Top + 7, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "C1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left - 20, wp.Margin.Top, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "D1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left - 18, wp.Margin.Top - 10, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "E1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left, wp.Margin.Top - 20, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "F1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left + 20, wp.Margin.Top - 8, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "G1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left + 27, wp.Margin.Top + 5, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "H1")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left + 30, wp.Margin.Top + 5, wp.Margin.Right, wp.Margin.Bottom);
            }
            else if (wp.WellPosition.ToString() == "H12")
            {
                lblRowHeader.Margin = new Thickness(wp.Margin.Left + 5, wp.Margin.Top + 18, wp.Margin.Right, wp.Margin.Bottom);
            }
            canvas.Children.Add(lblRowHeader);

            double x = Math.Cos(angle) * (RequiredPerimeterCircumferenceLabel) / 2 + RequiredPerimeterCircumferenceLabel / 2;
            double y = Math.Sin(angle) * (RequiredPerimeterCircumferenceLabel) / 2 + RequiredPerimeterCircumferenceLabel / 2;

            //lblRowHeader.Margin = new Thickness(x, y, 0, 0);
            //lblRowHeader.Margin = new Thickness(wp.Margin.Left, wp.Margin.Top, wp.Margin.Right, wp.Margin.Bottom);
            //canvas.Children.Add(lblRowHeader);
        }

        public void AppyOutputPlateItemsOnGrid()
        {
            int row = 0;
            if (AssignmentRotorgene == null)
                return;

            for (int pos = 0; pos < AssignmentRotorgene.RotorPlateItemLists.Count; pos++)
            {
                WellPositionControl test = (WellPositionControl)children[row];
                test.Targets = (PlateItem)AssignmentRotorgene.RotorPlateItemLists[pos];
                test.Legend = PositionContainment.StandardsOnly;
                row = row + 1;
            }
        }

        public void AppyInputPlateItemsOnGrid()
        {
            int row = 0;
            if (AssignmentRotorgene == null)
                return;

            for (int pos = 0; pos < AssignmentRotorgene.RotorPlateItemLists.Count; pos++)
            {
                WellPositionControl test = (WellPositionControl)children[row];
                test.IsInputPlate = false;
                test.Legend = PositionContainment.AnyKind;
                row = row + 1;
            }
        }

        private void wp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsAllowWellSelection == true)
            {
                CurrentSelectedWellControl = (WellPositionControl)sender;
                // Clear all selection if ControlKey is NOT press
                if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    foreach (WellPositionControl wp in children)
                    {
                        wp.IsSelected = false;
                    }
                }

                // Find the block created by pressing shift key and set IsSelected = true for all the control in that block.
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    //int minX = Math.Min(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);
                    //int maxX = Math.Max(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);
                    //for (int x = minX; x <= maxX; x++)
                    //{
                    //    if (children[x].IsSelected == false)
                    //    {
                    //        children[x].IsSelected = true;
                    //    }
                    //}

                    int minX = LastSelectedWellControl.WellPosition.PositionX;
                    int maxX = CurrentSelectedWellControl.WellPosition.PositionX;
                    for (int x = minX; x <= maxX; x++)
                    {
                        if (children[x].IsSelected == false)
                        {
                            children[x].IsSelected = true;
                        }
                    }
                }

                CurrentSelectedWellControl.IsSelected = true;
                if (CurrentSelectedWellControl != null)
                {
                    LastSelectedWellControl = CurrentSelectedWellControl;
                }
            }
            //else
            //{
            //    foreach (WellPositionControl wp in children)
            //    {
            //        wp.IsSelected = false;
            //    }
            //    if (CurrentSelectedWellControl != null)
            //    {
            //        CurrentSelectedWellControl.IsSelected = true;
            //    }
            //}
        }

        private WellPositionControl lastSelectedWellControl;

        private WellPositionControl LastSelectedWellControl
        {
            get
            {
                return lastSelectedWellControl;
            }

            set
            {
                lastSelectedWellControl = value;
            }
        }

        private WellPositionControl currentSelectedWellControl;

        private WellPositionControl CurrentSelectedWellControl
        {
            get
            {
                return currentSelectedWellControl;
            }

            set
            {
                currentSelectedWellControl = value;
            }
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control
        /// </summary>
        public static readonly DependencyProperty IsWellInputPlateProperty = DependencyProperty.Register("IsWellInputPlate", typeof(bool), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(false));//, new PropertyChangedCallback(CallbackIsWellInputPlate)){ BindsTwoWayByDefault = true });

        public bool IsWellInputPlate
        {
            get
            {
                return (bool)GetValue(IsWellInputPlateProperty);
            }
            set
            {
                SetValue(IsWellInputPlateProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to bind targets on the control.
        /// </summary>
        public static readonly DependencyProperty AssignmentRotorgeneProperty = DependencyProperty.Register("AssignmentRotorgene", typeof(RotorAssignment), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackAssignment)) { BindsTwoWayByDefault = true });

        public RotorAssignment AssignmentRotorgene
        {
            get
            {
                return (RotorAssignment)GetValue(AssignmentRotorgeneProperty);
            }
            set
            {
                SetValue(AssignmentRotorgeneProperty, value);
                // CreateGrid();
                CreateCircles();
            }
        }

        private static void CallbackAssignment(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorgenePlateControl rotor = (RotorgenePlateControl)property;
            rotor.AssignmentRotorgene = (RotorAssignment)args.NewValue;
        }

        /// <summary>
        /// Dependency property to indicate no of wells in each row. default rows is 8
        /// </summary>
        public static readonly DependencyProperty RowShapesProperty = DependencyProperty.Register("RowShapes", typeof(int), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(8, new PropertyChangedCallback(CallbackRowshapes)) { BindsTwoWayByDefault = true });

        public int RowShapes
        {
            get
            {
                return (int)GetValue(RowShapesProperty);
            }
            set
            {
                SetValue(RowShapesProperty, value);
                CreateCircles();
            }
        }

        private static void CallbackRowshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorgenePlateControl rect = (RotorgenePlateControl)property;
            rect.RowShapes = (int)args.NewValue;
        }

        public static readonly DependencyProperty WellPaddingProperty = DependencyProperty.Register("WellPadding", typeof(int), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(DefaultWellPadding));

        public int WellPadding
        {
            get
            {
                return (int)GetValue(WellPaddingProperty);
            }
            set
            {
                SetValue(WellPaddingProperty, value);
            }
        }

        public static readonly DependencyProperty WellsCountProperty = DependencyProperty.Register("WellsCount", typeof(int), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(DefaultWellsCount));

        public int WellsCount
        {
            get
            {
                return (int)GetValue(WellsCountProperty);
            }
            set
            {
                SetValue(WellsCountProperty, value);
            }
        }

        public static readonly DependencyProperty WellDiameterProperty = DependencyProperty.Register("WellDiameter", typeof(int), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(DefaultWellDiameter, new PropertyChangedCallback(CallbackWellDiameter)) { BindsTwoWayByDefault = true });

        public int WellDiameter
        {
            get
            {
                return (int)GetValue(WellDiameterProperty);
            }
            set
            {
                SetValue(WellDiameterProperty, value);
            }
        }

        private static void CallbackWellDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorgenePlateControl rot = (RotorgenePlateControl)property;
            rot.WellDiameter = (int)args.NewValue;
        }

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty RotorWellSelectionSizeProperty = DependencyProperty.Register("RotorWellSelectionSize", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultRotorWellSelectionSize));

        /// <summary>
        /// Gets or sets value of outer selection circle when well is clicked.
        /// </summary>
        /// <value>
        /// size of circle when clicked in integer.
        /// </value>
        public int RotorWellSelectionSize
        {
            get
            {
                return (int)GetValue(RotorWellSelectionSizeProperty);
            }
            set
            {
                SetValue(RotorWellSelectionSizeProperty, value);
            }
        }

        public static readonly DependencyProperty IsAllowWellSelectionProperty = DependencyProperty.Register("IsAllowWellSelection", typeof(bool), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(false));

        public bool IsAllowWellSelection
        {
            get
            {
                return (bool)GetValue(IsAllowWellSelectionProperty);
            }
            set
            {
                SetValue(IsAllowWellSelectionProperty, value);
            }
        }

        public static readonly DependencyProperty IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool), typeof(RotorgenePlateControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsReadonly)) { BindsTwoWayByDefault = true });

        public bool IsReadonly
        {
            get
            {
                return (bool)GetValue(IsReadonlyProperty);
            }
            set
            {
                SetValue(IsReadonlyProperty, value);
            }
        }

        private static void CallbackIsReadonly(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorgenePlateControl rect = (RotorgenePlateControl)property;
            rect.IsReadonly = (bool)args.NewValue;
        }
    }
}