// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Class for  WellPosition custom control.
    /// </summary>
    [TemplatePart(Name = PartCanvas, Type = typeof(Canvas))]
    public class WellPositionControl : Control
    {
        #region Member Variables

        private const string PartCanvas = "PART_CANVAS2";
        private const double DefaultCanvasWidth = 30;
        private const double DefaultCanvasHeight = 30;
        private const int DefaultWellDiameter = 17;
        private const int DefaultWellOrigin = 0;
        private double angle;
        private int wellOrigin;
        private Canvas canvas;
        private Path pathOuterSurface = new Path();
        private WellTargetTO target;
        private Color selectedHighlightColor;
        private Color anyKindShapeFillColor;

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control.
        /// </summary>
        public static readonly DependencyProperty IsInputPlateProperty = DependencyProperty.Register("IsInputPlate", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to if the control is currently selected or not.
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to specify Width of canvas where wellposition control is to be drawn.
        /// </summary>
        public static readonly DependencyProperty CanvasWidthProperty = DependencyProperty.Register("CanvasWidth", typeof(double), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultCanvasWidth));

        /// <summary>
        /// Dependency property to specify height of canvas where wellposition control is to be drawn.
        /// </summary>
        public static readonly DependencyProperty CanvasHeightProperty = DependencyProperty.Register("CanvasHeight", typeof(double), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultCanvasHeight));

        /// <summary>
        /// Dependency property which specifies if Legend is to be drawn.
        /// </summary>
        public static readonly DependencyProperty IsDrawTargetLegendProperty = DependencyProperty.Register("IsDrawTargetLegend", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property which specifies if Legend background is to be filled.
        /// </summary>
        public static readonly DependencyProperty IsFilledLegendProperty = DependencyProperty.Register("IsFilledLegend", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely.
        /// </summary>
        public static readonly DependencyProperty WellPositionProperty = DependencyProperty.Register("WellPosition", typeof(WellPosition), typeof(WellPositionControl));

        /// <summary>
        /// Dependency property for name of the well like A1, A2 etc.
        /// </summary>
        public static readonly DependencyProperty WellNameProperty = DependencyProperty.Register("WellName", typeof(string), typeof(WellPositionControl));

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely.
        /// </summary>
        public static readonly DependencyProperty LegendProperty = DependencyProperty.Register("Legend", typeof(OrderType), typeof(WellPositionControl), new FrameworkPropertyMetadata(OrderType.Empty));

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty WellSelectionSizeProperty = DependencyProperty.Register("WellSelectionSize", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(25));

        /// <summary>
        /// Dependency property to show size of outer selection cross hair circle of calibrated well.
        /// </summary>
        public static readonly DependencyProperty DiameterCrosshairProperty = DependencyProperty.Register("DiameterCrosshair", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(25));

        /// <summary>
        /// Dependency property to set color of sample.
        /// </summary>
        public static readonly DependencyProperty SampleFillColorProperty = DependencyProperty.Register("SampleFillColor", typeof(Color), typeof(WellPositionControl), new FrameworkPropertyMetadata(Color.FromRgb(192, 197, 211)));

        /// <summary>
        /// Dependency property to specify size of well control in diameter.
        /// </summary>
        public static readonly DependencyProperty WellDiameterProperty = DependencyProperty.Register("WellDiameter", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultWellDiameter, new PropertyChangedCallback(CallbackDiameter)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to set legend position.
        /// </summary>
        public static readonly DependencyProperty LegendPositionProperty = DependencyProperty.Register("LegendPosition", typeof(WellPosition), typeof(WellPositionControl));

        /// <summary>
        /// Dependency property to indicate if well is tobe auto calibrated.
        /// </summary>
        public static readonly DependencyProperty IsAutoCalibratedProperty = DependencyProperty.Register("IsAutoCalibrated", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to indicate if border to the well is to be visible.
        /// </summary>
        public static readonly DependencyProperty IsWellBorderVisibleProperty = DependencyProperty.Register("IsWellBorderVisible", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to indicate if well is in readonly mode.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyWellProperty = DependencyProperty.Register("IsReadOnlyWell", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsReadOnlyWell)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to set Assigned Well.
        /// </summary>
        public static readonly DependencyProperty IsInputAssignedProperty = DependencyProperty.Register("IsInputAssigned", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to show tool tip on mouse hover.
        /// </summary>
        public static readonly DependencyProperty WellToolTipProperty = DependencyProperty.Register("WellToolTip", typeof(string), typeof(WellPositionControl), new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(CallbackWellToolTip)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to indiated whether the well is transparant.
        /// </summary>
        public static readonly DependencyProperty IsWellTransparentProperty = DependencyProperty.Register("IsWellTransparent", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsWellTransparent)) { BindsTwoWayByDefault = true });

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="WellPositionControl"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Required for project to have method call from static DP.")]
        static WellPositionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WellPositionControl), new FrameworkPropertyMetadata(typeof(WellPositionControl)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WellPositionControl" /> class.
        /// </summary>
        public WellPositionControl()
        {
            this.Loaded += new RoutedEventHandler(WellPositionControl_Loaded);
            this.Unloaded += new RoutedEventHandler(WellPositionControl_Unloaded);
        }

        private void WellPositionControl_Unloaded(object sender, RoutedEventArgs e)
        {
            pathOuterSurface.MouseDown -= SelectionSurface_MouseDown;
        }

        private void WellPositionControl_Loaded(object sender, RoutedEventArgs e)
        {
            pathOuterSurface.MouseDown -= SelectionSurface_MouseDown;

            pathOuterSurface.MouseDown += SelectionSurface_MouseDown;
        }

        #endregion Construction

        #region Control Overrides

        /// <summary>
        /// Called when the Template is applied to the control. Template is defined in Style file.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            canvas = (Canvas)GetTemplateChild(PartCanvas);
            if (IsWellTransparent == false)
            {
                canvas.Background = new SolidColorBrush(Colors.White);
            }

            canvas.MouseDown += WellPosition_MouseDown;
            GenerateShape();
        }

        #endregion Control Overrides

        #region properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is input control.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if this instance is input control; otherwise, <c>false</c>.
        /// </value>
        public bool IsInputPlate
        {
            get
            {
                return (bool)GetValue(IsInputPlateProperty);
            }

            set
            {
                SetValue(IsInputPlateProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsSelected
        {
            get
            {
                return (bool)GetValue(IsSelectedProperty);
            }

            set
            {
                if (IsSelected != value)
                {
                    SetValue(IsSelectedProperty, value);
                    if (canvas != null)
                    {
                        CreateOuterSurface();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well is auto calibrated shape.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is of auto calibrated type; otherwise, <c>false</c>.
        /// </value>
        public bool IsAutoCalibrated
        {
            get
            {
                return (bool)GetValue(IsAutoCalibratedProperty);
            }

            set
            {
                SetValue(IsAutoCalibratedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well border is visible.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsWellBorderVisible
        {
            get
            {
                return (bool)GetValue(IsWellBorderVisibleProperty);
            }

            set
            {
                SetValue(IsWellBorderVisibleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well is readonly and selection is not allowed.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is read only; otherwise, <c>false</c>.
        /// </value>
        public bool IsReadOnlyWell
        {
            get
            {
                return (bool)GetValue(IsReadOnlyWellProperty);
            }

            set
            {
                SetValue(IsReadOnlyWellProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well for input type is assigned.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsInputAssigned
        {
            get
            {
                return (bool)GetValue(IsInputAssignedProperty);
            }

            set
            {
                SetValue(IsInputAssignedProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well is transparent.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is transparent; otherwise, <c>false</c>.
        /// </value>
        public bool IsWellTransparent
        {
            get
            {
                return (bool)GetValue(IsWellTransparentProperty);
            }

            set
            {
                SetValue(IsWellTransparentProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether legend is to drawn.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if legend is to be drawn; otherwise, <c>false</c>.
        /// </value>
        public bool IsDrawTargetLegend
        {
            get
            {
                return (bool)GetValue(IsDrawTargetLegendProperty);
            }

            set
            {
                SetValue(IsDrawTargetLegendProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether legend is of filled type.
        /// </summary>
        /// <value>
        /// Returns <c>true</c> if this instance is filled legend; otherwise, <c>false</c>.
        /// </value>
        public bool IsFilledLegend
        {
            get
            {
                return (bool)GetValue(IsFilledLegendProperty);
            }

            set
            {
                SetValue(IsFilledLegendProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets size of each well control.
        /// </summary>
        /// <value>
        /// Diameter of each well.
        /// </value>
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

        /// <summary>
        /// Gets or sets value of outer selection circle when well is clicked.
        /// </summary>
        /// <value>
        /// Size of circle when clicked in integer.
        /// </value>
        public int WellSelectionSize
        {
            get
            {
                return (int)GetValue(WellSelectionSizeProperty);
            }

            set
            {
                SetValue(WellSelectionSizeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets value of  outer selection cross hair circle when well is clicked.
        /// </summary>
        /// <value>
        /// Size of circle when clicked in integer.
        /// </value>
        public int DiameterCrosshair
        {
            get
            {
                return (int)GetValue(DiameterCrosshairProperty);
            }

            set
            {
                SetValue(DiameterCrosshairProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the canvas.
        /// </summary>
        /// <value>
        /// The width of the canvas.
        /// </value>
        public double CanvasWidth
        {
            get
            {
                return (double)GetValue(CanvasWidthProperty);
            }

            set
            {
                SetValue(CanvasWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the height of the canvas.
        /// </summary>
        /// <value>
        /// The height of the canvas.
        /// </value>
        public double CanvasHeight
        {
            get
            {
                return (double)GetValue(CanvasHeightProperty);
            }

            set
            {
                SetValue(CanvasHeightProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets tool tip for the well.
        /// </summary>
        /// <value>
        /// Tool tip message.
        /// </value>
        public string WellToolTip
        {
            get
            {
                return (string)GetValue(WellToolTipProperty);
            }

            set
            {
                SetValue(WellToolTipProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the well.
        /// </summary>
        /// <value>
        /// The name of well.
        /// </value>
        public string WellName
        {
            get
            {
                return (string)GetValue(WellNameProperty);
            }

            set
            {
                SetValue(WellNameProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of highlight when wellposition control is selected.
        /// </summary>
        /// <value>
        /// The color object.
        /// </value>
        public Color SelectedHighlightColor
        {
            get
            {
                return selectedHighlightColor;
            }

            set
            {
                selectedHighlightColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color to be filled when creating circular filled shape with any colors.
        /// </summary>
        /// <value>
        /// The color object.
        /// </value>
        public Color AnyKindShapeFillColor
        {
            get
            {
                return anyKindShapeFillColor;
            }

            set
            {
                anyKindShapeFillColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the color to be filled for the well.
        /// </summary>
        /// <value>
        /// The color object.
        /// </value>
        public Color SampleFillColor
        {
            get
            {
                return (Color)GetValue(SampleFillColorProperty);
            }

            set
            {
                SetValue(SampleFillColorProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the Legend types defined.
        /// </summary>
        /// <value>
        /// The Legend/OrderType.
        /// </value>
        public OrderType Legend
        {
            get
            {
                return (OrderType)GetValue(LegendProperty);
            }

            set
            {
                SetValue(LegendProperty, value);
                if (canvas != null)
                {
                    GenerateShape();
                }
            }
        }

        /// <summary>
        /// Gets or sets the wellposition object for the well.
        /// </summary>
        /// <value>
        /// The position object with x, y, row and column details.
        /// </value>
        public WellPosition LegendPosition
        {
            get
            {
                return (WellPosition)GetValue(LegendPositionProperty);
            }

            set
            {
                SetValue(LegendPositionProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the targets to be binded for the well.
        /// </summary>
        /// <value>
        /// The object of WellTargetTO.
        /// </value>
        public WellTargetTO Targets
        {
            get
            {
                return target;
            }

            set
            {
                target = value;
            }
        }

        /// <summary>
        /// Gets or sets the wellposition object for the well.
        /// </summary>
        /// <value>
        /// The position object with x, y, row and column details.
        /// </value>
        public WellPosition WellPosition
        {
            get
            {
                return (WellPosition)GetValue(WellPositionProperty);
            }

            set
            {
                SetValue(WellPositionProperty, value);
            }
        }

        #endregion properties

        #region Methods

        /// <summary>
        /// Generates appropriate shape. shape parameters are defind using properties.
        /// </summary>
        private void GenerateShape()
        {
            canvas.Children.Clear();

            canvas.Width = CanvasWidth;
            canvas.Height = CanvasHeight;

            wellOrigin = ((int)CanvasWidth / 2) - (WellDiameter / 2);

            CreateOuterSurface();
            canvas.Children.Add(pathOuterSurface);

            if (Legend == OrderType.Blocked)
            {
                CreateSamplesOnly();
                CreateBlockedShape();
                IsDrawTargetLegend = false;
            }
            else if (Legend == OrderType.Standard)
            {
                if (IsInputPlate == true)
                {
                    CreateShapeInput();
                }
                else
                {
                    CreateShapeOutput();
                }

                IsDrawTargetLegend = true;
                IsFilledLegend = true;
            }
            else if (Legend == OrderType.Sample)
            {
                if (IsInputPlate == true)
                {
                    CreateSamplesOnly();
                    IsDrawTargetLegend = false;
                }
                else
                {
                    CreateShapeOutput();
                }
            }
            else if (Legend == OrderType.Control)
            {
                if (IsInputPlate == true)
                {
                    CreateShapeInput();
                }
                else
                {
                    CreateShapeOutput();
                }

                IsDrawTargetLegend = true;
                IsFilledLegend = false;
            }
            else if (Legend == OrderType.AnyKind)
            {
                if (IsInputPlate == true)
                {
                    CreateAnyKind();
                }
                else
                {
                    CreateShapeOutput();
                }

                IsDrawTargetLegend = false;
                IsFilledLegend = false;
            }
            else if (Legend == OrderType.Empty)
            {
                CreateEmpty();
                IsDrawTargetLegend = false;
            }
            else if (Legend == OrderType.Calibration || Legend == OrderType.AutoCalibration)
            {
                IsReadOnlyWell = true;
                CreateCalibration();
                IsDrawTargetLegend = false;
            }
            else if (Legend == OrderType.InputSample)
            {
                IsDrawTargetLegend = false;
                CreateInputSample();
            }
            else if (Legend == OrderType.Marked)
            {
                CreateMarked();
            }

            if (IsDrawTargetLegend == true)
            {
                CreateLegend();
            }
        }

        /// <summary>
        /// Generates a gray filled well with.
        /// </summary>
        private void CreateInputSample()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(SampleFillColor);
            path.Stroke = new SolidColorBrush(Colors.White);
            path.StrokeThickness = 1;
            path.Data = geometry;
            if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
            {
                path.ToolTip = WellToolTip;
            }

            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a pie shaped circular control with provided colors.
        /// </summary>
        private void CreateShapeInput()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

            if (Legend == OrderType.AnyKind)
            {
                path.Fill = new SolidColorBrush(AnyKindShapeFillColor);
                path.Stroke = new SolidColorBrush(Colors.White);
            }
            else
            {
                path.Fill = new SolidColorBrush(Colors.DarkBlue);
                path.Stroke = new SolidColorBrush(Colors.White);
            }

            if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
            {
                path.ToolTip = WellToolTip;
            }

            path.Data = geometry;
            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a pie shaped circular control with provided colors.
        /// </summary>
        private void CreateShapeOutput()
        {
            string tooltipText = string.Empty;
            string legendText = string.Empty;
            string concentrationText = string.Empty;
            if (Targets != null)
            {
                //if (Targets.TargetList.Count > 0)
                //{
                //    if (Targets.TargetList.Count < 3)
                //    {
                //        angle = Math.PI / 2;
                //    }
                //    else
                //    {
                //        angle = (-Math.PI / 2) - (Math.PI / 4);
                //    }

                //    if (Targets.TargetList.Count > 1)
                //    {
                //        foreach (TargetTO ps in Targets.TargetList)
                //        {
                //            double totalps = Targets.TargetList.Count;
                //            Geometry geometry;
                //            Path path = new Path();
                //            geometry = new PathGeometry();
                //            double x = (Math.Cos(angle) * WellDiameter / 2) + (WellDiameter / 2);
                //            double y = (Math.Sin(angle) * WellDiameter / 2) + (WellDiameter / 2);
                //            LineSegment lingeSeg = new LineSegment(new Point(x, y), true);
                //            double angleShare = 360 / totalps;
                //            angle += DegreeToRadian(angleShare);
                //            x = (Math.Cos(angle) * WellDiameter / 2) + (WellDiameter / 2);
                //            y = (Math.Sin(angle) * WellDiameter / 2) + (WellDiameter / 2);
                //            ArcSegment arcSeg = new ArcSegment(new Point(x, y), new Size(WellDiameter / 2, WellDiameter / 2), angleShare, angleShare > 180, SweepDirection.Clockwise, false);
                //            LineSegment lingeSeg2 = new LineSegment(new Point(WellDiameter / 2, WellDiameter / 2), true);
                //            PathFigure fig = new PathFigure(new Point(WellDiameter / 2, WellDiameter / 2), new PathSegment[] { lingeSeg, arcSeg, lingeSeg2 }, false);
                //            ((PathGeometry)geometry).Figures.Add(fig);
                //            path.Fill = new SolidColorBrush(ps.Color);
                //            path.Stroke = new SolidColorBrush(Colors.White);
                //            path.StrokeThickness = 1;
                //            path.Data = geometry;
                //            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);

                //            if (Legend != OrderType.Sample)
                //            {
                //                legendText = ", " + Legend.ToString();
                //            }

                //            if (Legend == OrderType.Standard)
                //            {
                //                concentrationText = ", Conc:" + ps.ConcentrationWithUnit;
                //            }

                //            tooltipText = String.Concat(WellName, ", ", ps.Name, ":", ps.TargetType.ToString(), legendText, concentrationText);
                //            if (ps.Name.Length > 0)
                //            {
                //                path.ToolTip = tooltipText;
                //            }

                //            canvas.Children.Add(path);
                //        }
                //    }
                //    else
                //    {
                //        //TargetTO ps = Targets.TargetList[0];
                //        //Geometry geometry;
                //        //Path path = new Path();
                //        //geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
                //        //path.Fill = new SolidColorBrush(ps.Color);
                //        //path.Stroke = new SolidColorBrush(Colors.White);
                //        //path.StrokeThickness = 1;
                //        //if (IsWellBorderVisible == true)
                //        //{
                //        //    path.Stroke = new SolidColorBrush(Colors.DarkGray);
                //        //}

                //        //path.Data = geometry;
                //        //geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
                //        //if (Legend != OrderType.Sample)
                //        //{
                //        //    legendText = ", " + Legend.ToString();
                //        //}

                //        //if (Legend == OrderType.Standard)
                //        //{
                //        //    concentrationText = ", Conc:" + ps.ConcentrationWithUnit;
                //        //}

                //        //tooltipText = String.Concat(WellName, ", ", ps.Name, ":", ps.TargetType.ToString(), legendText, concentrationText);
                //        //if (ps.Name.Length > 0)
                //        //{
                //        //    path.ToolTip = tooltipText;
                //        //}

                //        //canvas.Children.Add(path);
                //    }
                //}
            }
            else
            {
                Geometry geometry;
                Path path = new Path();
                geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

                if (Legend == OrderType.AnyKind)
                {
                    path.Fill = new SolidColorBrush(AnyKindShapeFillColor);
                    path.Stroke = new SolidColorBrush(Colors.White);
                    if (IsWellBorderVisible == true)
                    {
                        path.Stroke = new SolidColorBrush(Colors.DarkGray);
                    }
                }

                if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
                {
                    path.ToolTip = WellToolTip;
                }

                path.Data = geometry;
                geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
                canvas.Children.Add(path);
            }
        }

        /// <summary>
        /// Generates a empty circle control with no fill and grey border.
        /// </summary>
        private void CreateEmpty()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.White);
            path.Stroke = new SolidColorBrush(Colors.Gray);
            path.StrokeThickness = 1;
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates circle with grey fill for input control.
        /// </summary>
        private void CreateAnyKind()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.Black);
            path.StrokeThickness = 0;
            path.Stroke = new SolidColorBrush(Colors.DarkGray);
            if (IsWellBorderVisible == true)
            {
                path.StrokeThickness = 1;
            }

            path.Data = geometry;
            if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
            {
                path.ToolTip = WellToolTip;
            }

            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
        }

        private void CreateOuterSurface()
        {
            if (IsSelected == true)
            {
                Geometry geometry;
                geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), (WellDiameter / 2) + (WellDiameter / 8), (WellDiameter / 2) + (WellDiameter / 8));
                pathOuterSurface.StrokeThickness = WellSelectionSize / 8;
                pathOuterSurface.Stroke = new SolidColorBrush(Color.FromRgb(170, 202, 240));
                pathOuterSurface.Data = geometry;
                geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
                pathOuterSurface.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                if (pathOuterSurface != null)
                {
                    pathOuterSurface.Visibility = System.Windows.Visibility.Hidden;
                }
            }
        }

        /// <summary>
        /// Generates a sample control ie an empty circle with black border.
        /// </summary>
        private void CreateSamplesOnly()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.White);
            path.Stroke = new SolidColorBrush(Colors.DarkBlue);
            path.StrokeThickness = 2;
            path.Data = geometry;
            if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
            {
                path.ToolTip = WellToolTip;
            }

            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a circle control having a cross sign in-between.
        /// </summary>
        private void CreateBlockedShape()
        {
            angle = DegreeToRadian(45);
            double x1 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            double y1 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            angle += DegreeToRadian(90);

            double x2 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            double y2 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            angle += DegreeToRadian(90);

            double x3 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            double y3 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            angle += DegreeToRadian(90);

            double x4 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            double y4 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
            angle += DegreeToRadian(90);

            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x1 - (WellDiameter / 10), y1 - (WellDiameter / 10));

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x3 + (WellDiameter / 10), y3 + (WellDiameter / 10));

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.DarkBlue;
            myPath.StrokeThickness = 2;
            myPath.Data = myPathGeometry;

            myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(myPath);

            myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x2 + (WellDiameter / 10), y2 - (WellDiameter / 10));

            myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x4 - (WellDiameter / 10), y4 + (WellDiameter / 10));

            myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            myPath = new Path();
            myPath.Stroke = Brushes.DarkBlue;
            myPath.StrokeThickness = 2;
            myPath.Data = myPathGeometry;
            myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(myPath);
        }

        private void CreateMarked()
        {
            /////Creation of circle.
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.Black);
            path.Stroke = new SolidColorBrush(Colors.White);
            path.StrokeThickness = 1;
            path.Data = geometry;
            if (string.IsNullOrEmpty(WellToolTip) == false && WellToolTip.Length > 0)
            {
                path.ToolTip = WellToolTip;
            }

            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);

            /////Creation of slash within cirlce
            ////WellDiameter = WellDiameter - WellDiameter * 30 / 100;
            ////wellOrigin = ((int)CanvasWidth / 2) - (WellDiameter / 2);
            int markedLineDiameter = WellDiameter - (WellDiameter * 30 / 100);
            int markedwellOrigin = ((int)CanvasWidth / 2) - (markedLineDiameter / 2);
            angle = DegreeToRadian(45);
            double x1 = (Math.Cos(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            double y1 = (Math.Sin(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            angle += DegreeToRadian(90);

            double x2 = (Math.Cos(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            double y2 = (Math.Sin(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            angle += DegreeToRadian(90);

            double x3 = (Math.Cos(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            double y3 = (Math.Sin(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            angle += DegreeToRadian(90);

            double x4 = (Math.Cos(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            double y4 = (Math.Sin(angle) * (markedLineDiameter / 2)) + (markedLineDiameter / 2);
            angle += DegreeToRadian(90);

            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x1 - (markedLineDiameter / 10), y1 - (markedLineDiameter / 10));

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x3 + (markedLineDiameter / 10), y3 + (markedLineDiameter / 10));

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            ///// myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            ////myPath.Stroke = Brushes.DarkBlue;
            ////myPath.StrokeThickness = 2;
            ////myPath.Data = myPathGeometry;

            /////myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            ////canvas.Children.Add(myPath);

            myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x2 + (markedLineDiameter / 10), y2 - (markedLineDiameter / 10));

            myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x4 - (markedLineDiameter / 10), y4 + (markedLineDiameter / 10));

            myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            myPath = new Path();
            myPath.Stroke = Brushes.White;
            myPath.StrokeThickness = 3;
            myPath.Data = myPathGeometry;
            myPathGeometry.Transform = new TranslateTransform(markedwellOrigin, markedwellOrigin);
            canvas.Children.Add(myPath);
        }

        /// <summary>
        /// Path geometry creats angles based on radians. This function converts degrees to radians.
        /// </summary>
        /// <param name="inputAngle">Input Angle in Degrees.</param>
        /// <returns>
        /// Angle in radians.
        /// </returns>
        private double DegreeToRadian(double inputAngle)
        {
            return Math.PI * inputAngle / 180.0;
        }

        /// <summary>
        /// Creates a rectangluar box (filled or unfilled) at the corner of circle. called as legend.
        /// Standard is filled rectangle. controls is empty rectangle.
        /// </summary>
        private void CreateLegend()
        {
            Rectangle r1 = new Rectangle();
            r1.Stroke = new SolidColorBrush(Colors.Black);
            r1.Height = canvas.Width / 2.3;
            r1.Width = canvas.Width / 2.3;
            r1.Margin = new Thickness(0, 0, 0, 0);
            r1.StrokeThickness = r1.Width / 6;
            if (Legend == OrderType.Standard)
            {
                r1.Stroke = new SolidColorBrush(Colors.White);
                r1.Fill = new SolidColorBrush(Colors.Black);
            }
            else if (Legend == OrderType.Control)
            {
                r1.Stroke = new SolidColorBrush(Colors.White);
                r1.Fill = new SolidColorBrush(Colors.Black);
                Rectangle r2 = new Rectangle();
                r2.Height = canvas.Width / 7;
                r2.Width = canvas.Width / 7;
                r2.RenderTransform = new TranslateTransform((r1.Width / 2) - (r1.Width / 6), (r1.Height / 2) - (r1.Width / 6));
                r2.Fill = new SolidColorBrush(Colors.White);
                canvas.Children.Add(r2);
                Canvas.SetZIndex(r2, 999);
            }

            canvas.Children.Add(r1);
            Canvas.SetZIndex(r1, 888);
        }

        /// <summary>
        /// Generates a calibration shape. cross hair on a filled sample. If Auto then calibration is gray else blue.
        /// </summary>
        private void CreateCalibration()
        {
            canvas.Children.Clear();
            wellOrigin = (int)(canvas.Width / 2) - (WellDiameter / 2);

            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), (WellDiameter / 2), (WellDiameter / 2));
            path.Fill = new SolidColorBrush(SampleFillColor);
            path.Stroke = new SolidColorBrush(Colors.DarkGray);
            path.StrokeThickness = 0;
            if (IsWellBorderVisible == true)
            {
                path.StrokeThickness = 1;
            }

            if (DiameterCrosshair < WellDiameter)
            {
                path.StrokeThickness = 1;
            }
            else
            {
                DiameterCrosshair = Convert.ToInt32(WellDiameter * 1.4);
            }

            path.Data = geometry;
            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);
            path = new Path();
            wellOrigin = (int)(canvas.Width / 2) - (DiameterCrosshair / 2);
            geometry = new EllipseGeometry(new Point(DiameterCrosshair / 2, DiameterCrosshair / 2), (DiameterCrosshair / 2), (DiameterCrosshair / 2));
            if (IsAutoCalibrated == true || Legend == OrderType.AutoCalibration)
            {
                path.Stroke = new SolidColorBrush(Color.FromRgb(157, 160, 168));
            }
            else
            {
                path.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));
            }

            path.StrokeThickness = 2;
            path.Data = geometry;
            canvas.UseLayoutRounding = true;
            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(path);

            angle = DegreeToRadian(90);
            double x1 = (Math.Cos(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            double y1 = (Math.Sin(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            angle += DegreeToRadian(90);

            angle += DegreeToRadian(90);

            double x3 = (Math.Cos(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            double y3 = (Math.Sin(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            angle += DegreeToRadian(90);

            angle += DegreeToRadian(90);

            PathFigure myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x1, y1);

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x3, y3);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();

            if (IsAutoCalibrated == true || Legend == OrderType.AutoCalibration)
            {
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(156, 159, 167));
            }
            else
            {
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));
            }

            myPath.StrokeThickness = 0.8;
            myPath.Data = myPathGeometry;
            myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);

            Canvas.SetZIndex(myPath, 999);
            canvas.Children.Add(myPath);
            angle = DegreeToRadian(-180);
            x1 = (Math.Cos(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            y1 = (Math.Sin(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            angle += DegreeToRadian(90);

            angle += DegreeToRadian(90);

            x3 = (Math.Cos(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            y3 = (Math.Sin(angle) * (DiameterCrosshair / 2)) + (DiameterCrosshair / 2);
            angle += DegreeToRadian(90);

            angle += DegreeToRadian(90);

            myPathFigure = new PathFigure();

            myPathFigure.StartPoint = new Point(x1, y1);

            myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x3, y3);

            myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            myPath = new Path();

            if (IsAutoCalibrated == true || Legend == OrderType.AutoCalibration)
            {
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(156, 159, 167));
            }
            else
            {
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));
            }

            myPath.StrokeThickness = 0.8;
            myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            myPath.Data = myPathGeometry;
            canvas.Children.Add(myPath);

            wellOrigin = ((int)canvas.Width / 2) - (WellDiameter / 2);
            path = new Path();
            geometry = new EllipseGeometry(new Point((WellDiameter / 2), (WellDiameter / 2)), 2, 2);
            path.Fill = new SolidColorBrush(SampleFillColor);
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            Canvas.SetZIndex(path, 1000);
            canvas.Children.Add(path);
        }

        private void WellPosition_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsReadOnlyWell == false)
            {
                IsSelected = !IsSelected;
            }
        }

        private void SelectionSurface_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (IsReadOnlyWell == false)
            {
                IsSelected = !IsSelected;
            }
        }

        private static void CallbackDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl rect = (WellPositionControl)property;
            rect.WellDiameter = (int)args.NewValue;
        }

        private static void CallbackIsReadOnlyWell(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl well = (WellPositionControl)property;
            well.IsReadOnlyWell = (bool)args.NewValue;
        }

        private static void CallbackWellToolTip(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl well = (WellPositionControl)property;
            well.WellToolTip = (string)args.NewValue;
        }

        private static void CallbackIsWellTransparent(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl well = (WellPositionControl)property;
            well.IsWellTransparent = (bool)args.NewValue;
        }

        #endregion Methods
    }
}