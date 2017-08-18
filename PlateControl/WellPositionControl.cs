// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Drawing.Drawing2D;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PlateControl
{
    /// <summary>
    /// Class for  WellPosition custom control
    /// </summary>
    [TemplatePart(Name = PartCanvas, Type = typeof(Canvas))]
    public class WellPositionControl : Control
    {
        private const string PartCanvas = "PART_CANVAS2";
        private const double DefaultCanvasWidth = 30;
        private const double DefaultCanvasHeight = 30;
        private const int DefaultWellDiameter = 50;
        private const int DefaultWellOrigin = 0;
        private double angle;
        private Canvas canvas;
        private int WellOrigin;

        // for calibration
        private bool isAutoCalibration;

        private int radiusCrossHair = 25;

        /// <summary>
        /// Initializes static members of the <see cref="WellPositionControl"/> class.
        /// </summary>
        static WellPositionControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WellPositionControl), new FrameworkPropertyMetadata(typeof(WellPositionControl)));
        }

        /// <summary>
        /// CanvasWidth is
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            canvas = (Canvas)GetTemplateChild(PartCanvas);
            //LegendPosition.PositionX = 3;
            //LegendPosition.PositionY = 3;
            // CanvasHeight = WellDiameter;
            // CanvasWidth = WellDiameter;

            ////setting Canvas.Background supports clicking on outside well to select it. else clicking on control only selects.
            canvas.Background = new SolidColorBrush(Colors.White);
            canvas.MouseDown += WellPosition_MouseDown;

            // this.MouseDown += WellPosition_MouseDown;
            //canvas.MouseUp += WellPosition_MouseUp;

            // target = new PlateItem();

            Legend = PositionContainment.Buffer;
            GenerateShape();
        }

        private void l1_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }

        private void WellPosition_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //IsMouseDown = true;
            IsSelected = !IsSelected;

            // CreateOuterSurface();
        }

        #region Methods

        /// <summary>
        /// Generates appropriate shape. shape parameters are defind using properties
        /// </summary>
        public void GenerateShape()
        {
            canvas.Children.Clear();

            canvas.Width = CanvasWidth;
            canvas.Height = CanvasHeight;

            WellOrigin = (int)CanvasWidth / 2 - WellDiameter / 2;

            CreateOuterSurface();

            // if (Legend == PositionContainment.Blocked && IsInputControl == true)
            if (Legend == PositionContainment.Blocked)
            {
                CreateSamplesOnly();
                CreateBlockedShape();
            }
            else if (Legend == PositionContainment.Buffer)
            {
                CreateSamplesOnly();
                ////CreateBufferShapeTest();
                ////CreateShapeBuffer();
                CreateStrippedTube();
            }
            else if (Legend == PositionContainment.Marked)
            {
                CreateMarked();
            }
            else if (Legend == PositionContainment.StandardsOnly)
            {
                if (IsInputPlate == true)
                    CreateShapeInput();
                else
                    CreateShapeOutput();

                IsDrawTargetLegend = true;
                IsFilledLegend = true;
            }
            else if (Legend == PositionContainment.SamplesOnly)
            {
                CreateSamplesOnly();
                //if (IsInputPlate == true)
                //    CreateShapeInput();
                //else
                //    CreateShapeOutput();
            }
            else if (Legend == PositionContainment.ControlsOnly)
            {
                if (IsInputPlate == true)
                    CreateShapeInput();
                else
                    CreateShapeOutput();
                IsDrawTargetLegend = true;
                IsFilledLegend = false;
            }
            else if (Legend == PositionContainment.AnyKind)
            {
                if (IsInputPlate == true)
                    CreateAnyKind();
                else
                    CreateShapeOutput();

                IsDrawTargetLegend = false;
                IsFilledLegend = false;
                //CreateShapeOutput();
            }
            else if (Legend == PositionContainment.Empty)
            {
                CreateEmpty();
            }
            else if (Legend == PositionContainment.Calibration)
            {
                IsReadOnlyWell = true;
                CreateCalibration();
            }
            //if (Legend == PositionContainment.Ring)
            //{
            //    CreateSamplesOnly();
            //}
            if (IsDrawTargetLegend == true)
            {
                CreateLegend();
            }
        }

        private void CreateMarked()
        {
            int wellOrigin = ((int)CanvasWidth / 2) - (WellDiameter / 2);
            ////Create circle
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

            path.Fill = new SolidColorBrush(Colors.Black);
            path.Stroke = new SolidColorBrush(Colors.White);

            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);
            /////////Create slash/////////

            /////WellDiameter = WellDiameter - WellDiameter * 30 / 100;
            wellOrigin = ((int)CanvasWidth / 2) - (WellDiameter / 2);
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
            ////myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            ////myPath.Stroke = Brushes.DarkBlue;
            ////myPath.StrokeThickness = 2;
            ////myPath.Data = myPathGeometry;

            ////myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            ////canvas.Children.Add(myPath);

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
            myPath.Stroke = Brushes.White;
            myPath.StrokeThickness = 3;
            myPath.Data = myPathGeometry;
            myPathGeometry.Transform = new TranslateTransform(wellOrigin, wellOrigin);
            canvas.Children.Add(myPath);
        }

        /// <summary>
        /// Generates a pie shaped circular control with provided colors.
        /// </summary>
        private void CreateShapeInput()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

            if (Legend == PositionContainment.AnyKind)
            {
                path.Fill = new SolidColorBrush(AnyKindShapeFillColor);
                path.Stroke = new SolidColorBrush(Colors.White);
            }
            else
            {
                path.Fill = new SolidColorBrush(Colors.DarkBlue);
                path.Stroke = new SolidColorBrush(Colors.White);
            }
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a pie shaped circular control with provided colors.
        /// </summary>
        private void CreateShapeOutput()
        {
            if (Targets.OutputPlateTargets.Count > 0)
            {
                if (Targets.OutputPlateTargets.Count < 3)
                {
                    angle = Math.PI / 2;
                }
                else
                {
                    angle = -Math.PI / 2 - Math.PI / 4;
                }
                ////Random rand = new Random(255);
                ////byte[] colorBytes = new byte[3];

                foreach (Target ps in Targets.OutputPlateTargets)
                {
                    double totalps = Targets.OutputPlateTargets.Count;
                    Geometry geometry;
                    Path path = new Path();

                    //if (totalps == 1 && ps != null)
                    //{
                    //    geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
                    //}
                    //else
                    //{
                    geometry = new PathGeometry();
                    double x = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
                    double y = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
                    LineSegment lingeSeg = new LineSegment(new Point(x, y), true);

                    //double angleShare = (ps / totalps) * 360;
                    double angleShare = 360 / totalps;
                    angle += DegreeToRadian(angleShare);
                    x = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
                    y = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
                    ArcSegment arcSeg = new ArcSegment(new Point(x, y), new Size(WellDiameter / 2, WellDiameter / 2), angleShare, angleShare > 180, SweepDirection.Clockwise, false);
                    LineSegment lingeSeg2 = new LineSegment(new Point(WellDiameter / 2, WellDiameter / 2), true);
                    PathFigure fig = new PathFigure(new Point(WellDiameter / 2, WellDiameter / 2), new PathSegment[] { lingeSeg, arcSeg, lingeSeg2 }, false);

                    ((PathGeometry)geometry).Figures.Add(fig);
                    //   }
                    ////rand.NextBytes(colorBytes);
                    ////System.Windows.Media.Color randomColor = System.Windows.Media.Color.FromRgb(colorBytes[0], colorBytes[1], colorBytes[2]);

                    //path.Fill = new SolidColorBrush(randomColor);
                    path.Fill = new SolidColorBrush(ps.Color);
                    path.Stroke = new SolidColorBrush(Colors.White);
                    path.StrokeThickness = 1;
                    path.Data = geometry;
                    geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
                    if (ps.Name.Length > 0)
                    {
                        path.ToolTip = ps.Name;
                    }
                    canvas.Children.Add(path);
                }
            }
            else
            {
                Geometry geometry;
                Path path = new Path();
                geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

                path.Fill = new SolidColorBrush(Colors.DarkBlue);
                path.Stroke = new SolidColorBrush(Colors.White);
                path.StrokeThickness = 1;

                path.Data = geometry;
                geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
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
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates circle with grey fill for input control
        /// </summary>
        private void CreateAnyKind()
        {
            ////Creates Grey filled input control
            //Geometry geometry;
            //Path path = new Path();
            //geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

            //if (AnyKindShapeFillColor != Color.FromArgb(0, 0, 0, 0))
            //{
            //    path.Fill = new SolidColorBrush(Colors.Pink);
            //}
            //else
            //{
            //    path.Fill = new SolidColorBrush(Colors.Gray);
            //}
            //path.Data = geometry;
            //canvas.Children.Add(path);

            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);

            path.Fill = new SolidColorBrush(Colors.DarkBlue);
            path.Stroke = new SolidColorBrush(Colors.White);
            path.StrokeThickness = 1;
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a highlight color to controls indicating control selection
        /// </summary>
        ///
        //private Path pathOuterSurface = new Path();
        private Path pathOuterSurface;

        private void CreateOuterSurface()
        {
            if (IsSelected == true)
            {
                //angle = Math.PI / 2;
                //double totalps = Targets.OutputPlateTargets.Count;
                Geometry geometry;
                pathOuterSurface = new Path();
                geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2 + WellDiameter / 8, WellDiameter / 2 + WellDiameter / 8);

                //pathOuterSurface.StrokeThickness = WellDiameter / 10;
                pathOuterSurface.StrokeThickness = WellSelectionSize / 8;

                pathOuterSurface.Stroke = new SolidColorBrush(Color.FromRgb(170, 202, 240));
                //if (IsInputPlate == true)
                //{
                //    pathOuterSurface.Stroke = new SolidColorBrush(Colors.Cyan);
                //}
                //else
                //{
                //    /////SelectedHighlightColor
                //    pathOuterSurface.Stroke = new SolidColorBrush(Colors.LightGreen);
                //}

                pathOuterSurface.Data = geometry;
                geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
                //canvas.Children.Add(pathOuterSurface);

                canvas.Children.Add(pathOuterSurface);

                //pathOuterSurface.MouseDown += WellPosition_MouseDown;
                //pathOuterSurface.MouseUp += WellPosition_MouseUp;
            }
            else
            {
                canvas.Children.Remove(pathOuterSurface);
                //pathOuterSurface.MouseDown -= WellPosition_MouseDown;
                //pathOuterSurface.MouseUp -= WellPosition_MouseUp;
            }
        }

        ///<summary>
        ///Generates a sample control ie an empty circle with black border
        ///</summary>
        private void CreateSamplesOnly()
        {
            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.White);
            path.Stroke = new SolidColorBrush(Colors.Black);
            path.StrokeThickness = 1;
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);
        }

        /// <summary>
        /// Generates a circle control having a cross sign in-between
        /// </summary>
        private void CreateBlockedShape()
        {
            //angle = -Math.PI / 2;
            //cross line 1

            WellDiameter = 34;
            angle = DegreeToRadian(45);
            double x1 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y1 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
            angle += DegreeToRadian(90);

            double x2 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y2 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
            angle += DegreeToRadian(90);

            double x3 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y3 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
            angle += DegreeToRadian(90);

            double x4 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y4 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
            angle += DegreeToRadian(90);

            PathFigure myPathFigure = new PathFigure();
            //myPathFigure.StartPoint = new Point(x1 - 10, y1 - 10);
            myPathFigure.StartPoint = new Point(x1 - WellDiameter / 10, y1 - WellDiameter / 10);

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x3 + WellDiameter / 10, y3 + WellDiameter / 10);

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

            myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(myPath);

            //cross line 2

            myPathFigure = new PathFigure();
            myPathFigure.StartPoint = new Point(x2 + WellDiameter / 10, y2 - WellDiameter / 10);

            myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(x4 - WellDiameter / 10, y4 + WellDiameter / 10);

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
            myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(myPath);
        }

        /// <summary>
        /// Generates a circle control having a cross sign in-between
        /// </summary>
        private void CreateBufferShapeTest()
        {
            //angle = -Math.PI / 2;
            //cross line 1
            WellDiameter = 34;

            angle = DegreeToRadian(0);
            double x1 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y1 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(95);
            ////angle += DegreeToRadian(100);
            double x2 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y2 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;
            angle = DegreeToRadian(110);
            ////angle += DegreeToRadian(distance);
            double x3 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y3 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(130);
            ////angle += DegreeToRadian(distance);
            double x4 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y4 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(160);
            ////angle += DegreeToRadian(distance);
            double x5 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y5 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(250);
            ////angle += DegreeToRadian(distance);
            double x6 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y6 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(290);
            ////angle += DegreeToRadian(distance);
            double x7 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y7 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            angle = DegreeToRadian(390);
            ////angle += DegreeToRadian(170);
            double x8 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y8 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            DrawLine(x1, y1, x2, y2);

            DrawLine(x3, y3, x8, y8);

            DrawLine(x4, y4, x7, y7);

            DrawLine(x5, y5, x6, y6);

            ////cross line 2
        }

        private void DrawLine(double startPointCosAngel, double startPointSinAngle, double endPointCosAngle, double endPointSinAngle)
        {
            PathFigure myPathFigure = new PathFigure();
            //myPathFigure.StartPoint = new Point(x1 - 10, y1 - 10);
            myPathFigure.StartPoint = new Point(startPointCosAngel - WellDiameter / 10, startPointSinAngle - WellDiameter / 10);

            LineSegment myLineSegment = new LineSegment();
            myLineSegment.Point = new Point(endPointCosAngle + WellDiameter / 10, endPointSinAngle + WellDiameter / 10);

            PathSegmentCollection myPathSegmentCollection = new PathSegmentCollection();
            myPathSegmentCollection.Add(myLineSegment);

            myPathFigure.Segments = myPathSegmentCollection;

            PathFigureCollection myPathFigureCollection = new PathFigureCollection();
            myPathFigureCollection.Add(myPathFigure);

            PathGeometry myPathGeometry = new PathGeometry();
            myPathGeometry.Figures = myPathFigureCollection;

            Path myPath = new Path();
            myPath.Stroke = Brushes.DarkBlue;
            myPath.StrokeThickness = 0.5;
            myPath.Data = myPathGeometry;

            myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(myPath);
        }

        private void CreateShapeBuffer()
        {
            //angle = -Math.PI / 2;
            //cross line 1

            WellDiameter = 34;
            int pointDistance = 25;

            angle = DegreeToRadian(80);
            double x1 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y1 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(95);
            angle += DegreeToRadian(pointDistance);
            double x2 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y2 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(110);
            angle += DegreeToRadian(pointDistance);
            double x3 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y3 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(130);
            angle += DegreeToRadian(pointDistance);
            double x4 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y4 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(160);
            angle = DegreeToRadian(270);
            double x5 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y5 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(250);
            angle += DegreeToRadian(pointDistance);
            double x6 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y6 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(pointDistance);
            angle += DegreeToRadian(pointDistance);
            double x7 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y7 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            ////angle = DegreeToRadian(pointDistance);
            angle += DegreeToRadian(pointDistance - 6);
            double x8 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y8 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            DrawLine(x1, y1, x8, y8);

            DrawLine(x2, y2, x7, y7);

            DrawLine(x3, y3, x6, y6);

            DrawLine(x4, y4, x5, y5);
        }

        ////private int WellDiameter = 50;

        private void CreateStrippedTube()
        {
            //WellDiameter = 50;

            //Start from 45 degrees
            int StartDegree = 45;
            //set no. of strips required
            int NoOfStrips = 7;
            for (int i = 0; i < NoOfStrips; i++)
            {
                //Get the position of initial point
                angle = DegreeToRadian(StartDegree);
                double x1 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
                double y1 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);

                ////double x1 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
                ////double y1 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

                //Get corresponding position on circle which will used to join to first point above.
                angle += DegreeToRadian(90 - StartDegree * 2);
                double x3 = (Math.Cos(angle) * (WellDiameter / 2)) + (WellDiameter / 2);
                double y3 = (Math.Sin(angle) * (WellDiameter / 2)) + (WellDiameter / 2);

                ////double x3 = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
                ////double y3 = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

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
                myPath.Stroke = Brushes.Gray;

                myPath.StrokeThickness = 2;
                myPath.Data = myPathGeometry;
                myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
                canvas.Children.Add(myPath);

                //Move the degree for the next strip starting point
                StartDegree = StartDegree - 180 / NoOfStrips;
            }
        }

        /// <summary>
        /// Path geometry creats angles based on radians. This function converts degrees to radians
        /// </summary>
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        /// <summary>
        /// creates a rectangluar box (filled or unfilled) at the corner of circle. called as legend
        /// Standard is filled rectangle. controls is empty rectangle
        /// </summary>
        private void CreateLegend()
        {
            double angle;
            angle = DegreeToRadian(-135);
            double x = Math.Cos(angle) * WellDiameter / 2 + WellDiameter / 2;
            double y = Math.Sin(angle) * WellDiameter / 2 + WellDiameter / 2;

            Rectangle r1 = new Rectangle();

            r1.Stroke = new SolidColorBrush(Colors.Black);
            //r1.StrokeThickness = 1;
            //r1.Height = WellDiameter / 1.5; // 2.2;
            //r1.Width = WellDiameter / 1.5; // 2.2;
            r1.Height = canvas.Width / 2.3;
            r1.Width = canvas.Width / 2.3;

            //r1.Margin = new Thickness(x - WellDiameter / 2, y - 5, 0, 0);
            //r1.Margin = new Thickness(x - ((int)CanvasWidth / 2 - WellDiameter / 2), y - 5, 0, 0);
            r1.Margin = new Thickness(0, 0, 0, 0);

            //r1.RenderTransform = new TranslateTransform(x - r1.Width / 6, y - r1.Width / 6);

            r1.StrokeThickness = r1.Width / 6;

            //if (r1.StrokeThickness == 1)
            //{
            //    r1.RenderTransform = new TranslateTransform(x - r1.Width / 2, y - r1.Width / 2);
            //}
            //else
            //{
            //    r1.RenderTransform = new TranslateTransform(x - r1.Width / 6, y - r1.Width / 6);
            //}

            if (Legend == PositionContainment.StandardsOnly)
            {
                r1.Stroke = new SolidColorBrush(Colors.White);
                r1.Fill = new SolidColorBrush(Colors.Black);
                //r1.StrokeThickness = 1;
            }
            else if (Legend == PositionContainment.ControlsOnly)
            {
                //r1.Height = WellDiameter / 2; // 2.2;
                //r1.Width = WellDiameter / 2; // 2.2;
                r1.Stroke = new SolidColorBrush(Colors.White);
                r1.Fill = new SolidColorBrush(Colors.Black);

                // r1.StrokeThickness = 1;
                Rectangle r2 = new Rectangle();
                r2.Height = canvas.Width / 7;
                r2.Width = canvas.Width / 7;
                r2.RenderTransform = new TranslateTransform(r1.Width / 2 - r1.Width / 6, r1.Height / 2 - r1.Width / 6);
                //r2.RenderTransform = new TranslateTransform(-1, -1);//(2.4, 2.4);

                //r2.RenderTransform = new TranslateTransform(-4, -4);
                r2.Fill = new SolidColorBrush(Colors.White);
                canvas.Children.Add(r2);
                Canvas.SetZIndex(r2, 999);

                //r2.Fill = new SolidColorBrush(Colors.White);
                //r2.Height = WellDiameter / 5.5; // 2.2;
                //r2.Width = WellDiameter / 5.5; // 2.2;
                //r2.Margin = new Thickness(4, 4, 0, 0);

                //r2.Height = r1.Height / 3;
                //r2.Width = r1.Width / 3;
                //r2.Margin = new Thickness(r1.Margin.Left + 1, r1.Margin.Top + 1, 0, 0);
            }

            //else if (Legend == PositionContantment.Selected)
            //{
            //    r1.StrokeThickness = 2;
            //    r1.Fill = new SolidColorBrush(Colors.White);
            //}

            ///////Generic code for any shape
            //if (IsDrawTargetLegend == true)
            //{
            //    if (IsFilledLegend == true)
            //    {
            //        r1.Stroke = new SolidColorBrush(Colors.White);
            //        r1.StrokeThickness = 2;
            //        r1.Fill = new SolidColorBrush(Colors.Black);
            //    }
            //    else
            //    {
            //        r1.StrokeThickness = 2;
            //        r1.Fill = new SolidColorBrush(Colors.White);
            //        // r1.Fill = new SolidColorBrush(Colors.Black);
            //        // r1.Stroke = new SolidColorBrush(Colors.White);
            //        // canvas.Children.Add(r2);
            //        // Canvas.SetZIndex(r2, 999);
            //    }
            //}

            //r1.RenderTransformOrigin = new Point() { X = 2, Y = 2 };

            canvas.Children.Add(r1);
            Canvas.SetZIndex(r1, 888);

            //if (Legend == PositionContainment.ControlsOnly)
            //{
            //    canvas.Children.Add(r2);
            //    Canvas.SetZIndex(r2, 999);
            //}
        }

        /// <summary>
        /// Generates a calibration shape. cross hair on a filled sample. If Auto then calibration is gray else blue.
        /// </summary>
        private void CreateCalibration()
        {
            //canvas2.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Clear();
            ////create sample
            //WellOrigin = WellDiameter / 2;
            WellOrigin = (int)canvas.Width / 2 - WellDiameter / 2;

            Geometry geometry;
            Path path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), WellDiameter / 2, WellDiameter / 2);
            path.Fill = new SolidColorBrush(Colors.LightGray);
            path.Stroke = new SolidColorBrush(Colors.DarkGray);
            path.StrokeThickness = 0;
            if (radiusCrossHair < WellDiameter)
            {
                path.StrokeThickness = 1;
            }
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);

            ////create outer circle
            path = new Path();
            WellOrigin = (int)canvas.Width / 2 - radiusCrossHair / 2;
            geometry = new EllipseGeometry(new Point(radiusCrossHair / 2, radiusCrossHair / 2), radiusCrossHair / 2, radiusCrossHair / 2);
            //path.Fill = new SolidColorBrush(Colors.White);
            if (isAutoCalibration)
                path.Stroke = new SolidColorBrush(Color.FromRgb(157, 160, 168));
            else
                path.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));

            path.StrokeThickness = 2;
            path.Data = geometry;
            canvas.UseLayoutRounding = true;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            canvas.Children.Add(path);

            //WellOrigin = (int)canvas2.Width / 2 - WellDiameter / 2;
            angle = DegreeToRadian(90);
            double x1 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            double y1 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            double x2 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            double y2 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            double x3 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            double y3 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            double x4 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            double y4 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            PathFigure myPathFigure = new PathFigure();
            // myPathFigure.StartPoint = new Point(x1 - WellDiameter / 10, y1 - WellDiameter / 10);
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

            if (isAutoCalibration)
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(157, 160, 168));
            else
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));
            myPath.StrokeThickness = 0.8;
            myPath.Data = myPathGeometry;
            myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            //  myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);

            ///add second line
            ///
            Canvas.SetZIndex(myPath, 999);
            canvas.Children.Add(myPath);

            ///add second line
            ///
            angle = DegreeToRadian(-180);
            x1 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            y1 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            x2 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            y2 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            x3 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            y3 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            x4 = Math.Cos(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            y4 = Math.Sin(angle) * radiusCrossHair / 2 + radiusCrossHair / 2;
            angle += DegreeToRadian(90);

            myPathFigure = new PathFigure();
            // myPathFigure.StartPoint = new Point(x1 - WellDiameter / 10, y1 - WellDiameter / 10);
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

            if (isAutoCalibration)
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(157, 160, 168));
            else
                myPath.Stroke = new SolidColorBrush(Color.FromRgb(0, 77, 159));

            myPath.StrokeThickness = 0.8;
            myPathGeometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            myPath.Data = myPathGeometry;
            canvas.Children.Add(myPath);

            //add white cirlce
            WellOrigin = (int)canvas.Width / 2 - WellDiameter / 2;
            path = new Path();
            geometry = new EllipseGeometry(new Point(WellDiameter / 2, WellDiameter / 2), 2, 2);
            path.Fill = new SolidColorBrush(Colors.LightGray);
            path.Data = geometry;
            geometry.Transform = new TranslateTransform(WellOrigin, WellOrigin);
            Canvas.SetZIndex(path, 1000);
            canvas.Children.Add(path);
        }

        #endregion Methods

        #region properties

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control
        /// </summary>
        public static readonly DependencyProperty IsInputPlateProperty = DependencyProperty.Register("IsInputPlate", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

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
        /// Dependency property to if the control is currently selected or not
        /// </summary>
        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

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
                        if (IsReadOnlyWell == false)
                        {
                            CreateOuterSurface();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Dependency property to specify Width of canvas where wellposition control is to be drawn
        /// </summary>
        public static readonly DependencyProperty CanvasWidthProperty = DependencyProperty.Register("CanvasWidth", typeof(double), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultCanvasWidth));

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
        /// Dependency property to specify height of canvas where wellposition control is to be drawn
        /// </summary>
        public static readonly DependencyProperty CanvasHeightProperty = DependencyProperty.Register("CanvasHeight", typeof(double), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultCanvasHeight));

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
        /// Dependency property which specifies if Legend is to be drawn.
        /// </summary>
        public static readonly DependencyProperty IsDrawTargetLegendProperty = DependencyProperty.Register("IsDrawTargetLegend", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

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
        /// Dependency property which specifies if Legend background is to be filled.
        /// </summary>
        public static readonly DependencyProperty IsFilledLegendProperty = DependencyProperty.Register("IsFilledLegend", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false));

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

        ///// <summary>
        ///// Dependency property which gets data from the parent, and generates colored pie shaped wellpositioncontrol
        ///// </summary>
        //public static readonly DependencyProperty TargetsProperty = DependencyProperty.Register("Targets", typeof(IList<Target>), typeof(WellPositionControl), new FrameworkPropertyMetadata(null));

        //public PlateItem Targets
        //{
        //    get
        //    {
        //        return (PlateItem)GetValue(TargetsProperty);
        //    }
        //    set
        //    {
        //        SetValue(TargetsProperty, value);
        //    }
        //}

        /// <summary>
        /// Dependency property which gets data from the parent, and generates colored pie shaped wellpositioncontrol
        /// </summary>

        private PlateItem target;

        public PlateItem Targets
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

        ///// <summary>
        ///// Dependency property which gets data from the parent, and generates colored pie shaped wellpositioncontrol.
        ///// </summary>
        //public static readonly DependencyProperty TargetsProperty = DependencyProperty.Register("Targets", typeof(IList<Target>), typeof(WellPositionControl), new FrameworkPropertyMetadata(null));

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "It is required for UI binding.")]
        //public IList<Target> Targets
        //{
        //    get
        //    {
        //        return (IList<Target>)GetValue(TargetsProperty);
        //    }

        //    set
        //    {
        //        SetValue(TargetsProperty, value);
        //    }
        //}

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely.
        /// </summary>
        public static readonly DependencyProperty WellPositionProperty = DependencyProperty.Register("WellPosition", typeof(WellPosition), typeof(WellPositionControl));

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

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely.
        /// </summary>
        public static readonly DependencyProperty LegendProperty = DependencyProperty.Register("Legend", typeof(PositionContainment), typeof(WellPositionControl), new FrameworkPropertyMetadata(PositionContainment.Empty));

        public PositionContainment Legend
        {
            get
            {
                return (PositionContainment)GetValue(LegendProperty);
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
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty WellSelectionSizeProperty = DependencyProperty.Register("WellSelectionSize", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(25));

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
        /// Dependency property which mentions the color of highlight when wellposition control is selected.
        /// </summary>
        private Color selectedHighlightColor;

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
        /// Property which identifies color to be filled when creating circular filled shape with any colors. can be used in worktable module.
        /// </summary>
        private Color anyKindShapeFillColor;

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
        /// Dependency property to specify size of well control in diameter.
        /// </summary>
        public static readonly DependencyProperty WellDiameterProperty = DependencyProperty.Register("WellDiameter", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultWellDiameter, new PropertyChangedCallback(CallbackDiameter)) { BindsTwoWayByDefault = true });

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
                //CreateGrid();
            }
        }

        private static void CallbackDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl rect = (WellPositionControl)property;
            rect.WellDiameter = (int)args.NewValue;
        }

        ///// <summary>
        ///// Dependency property to specify size of well control in diameter.
        ///// </summary>
        //public static readonly DependencyProperty WellOriginProperty = DependencyProperty.Register("WellOrigin", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultWellOrigin, new PropertyChangedCallback(CallbackWellOrigin)) { BindsTwoWayByDefault = true });

        ///// <summary>
        ///// Gets or sets starting point of well
        ///// </summary>
        ///// <value>
        ///// Diameter of each well.
        ///// </value>
        //public int WellOrigin
        //{
        //    get
        //    {
        //        return (int)GetValue(WellOriginProperty);
        //    }
        //    set
        //    {
        //        SetValue(WellOriginProperty, value);
        //    }
        //}

        //private static void CallbackWellOrigin(DependencyObject property, DependencyPropertyChangedEventArgs args)
        //{
        //    WellPositionControl rect = (WellPositionControl)property;
        //    rect.WellOrigin = (int)args.NewValue;
        //}

        /// <summary>
        /// Dependency property to set legend position
        /// </summary>
        public static readonly DependencyProperty LegendPositionProperty = DependencyProperty.Register("LegendPosition", typeof(WellPosition), typeof(WellPositionControl));

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

        public static readonly DependencyProperty IsReadOnlyWellProperty = DependencyProperty.Register("IsReadOnlyWell", typeof(bool), typeof(WellPositionControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsReadOnlyWell)) { BindsTwoWayByDefault = true });

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

        private static void CallbackIsReadOnlyWell(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            WellPositionControl well = (WellPositionControl)property;
            well.IsReadOnlyWell = (bool)args.NewValue;
        }

        #endregion properties
    }
}