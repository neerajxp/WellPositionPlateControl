// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates circular plate control and requires no info about x and y positions.
    /// Control is generated based on wellscount and wellsrestartat parameter.
    /// </summary>
    public class RotorGenePlateControl : Control
    {
        #region Member Variables

        private const string PartCanvas = "PART_ROTORGENE_CANVAS";
        private const int DefaultWellDiameter = 12;
        private const int DefaultWellsCount = 100;
        private const int DefaultWellPadding = 3;
        private const int DefaultRotorWellSelectionSize = 25;
        private const int DefaultLabelSpacing = 10;
        private const int DefaultRestartWellsAt = 12;
        private int wellCanvasSize = 20;
        private double canvasWidth;
        private double canvasHeight;
        private double angle;
        private Canvas canvas;
        private WellPositionControl[] children;
        private List<string> labelList;
        private WellPositionControl lastSelectedWellControl;
        private WellPositionControl currentSelectedWellControl;

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control.
        /// </summary>
        public static readonly DependencyProperty IsWellInputPlateProperty = DependencyProperty.Register("IsWellInputPlate", typeof(bool), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to indicate no of wells in each row. default rows is 8.
        /// </summary>
        public static readonly DependencyProperty RowShapesProperty = DependencyProperty.Register("RowShapes", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(8, new PropertyChangedCallback(CallbackRowshapes)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property indicating spacing between any two wells.
        /// </summary>
        public static readonly DependencyProperty WellPaddingProperty = DependencyProperty.Register("WellPadding", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(DefaultWellPadding));

        /// <summary>
        /// Dependency property indicating total number of well count.
        /// </summary>
        public static readonly DependencyProperty WellsCountProperty = DependencyProperty.Register("WellsCount", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(DefaultWellsCount, new PropertyChangedCallback(CallbackWellsCount)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property indicating the diameter of well.
        /// </summary>
        public static readonly DependencyProperty WellDiameterProperty = DependencyProperty.Register("WellDiameter", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(DefaultWellDiameter, new PropertyChangedCallback(CallbackWellDiameter)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty RotorWellSelectionSizeProperty = DependencyProperty.Register("RotorWellSelectionSize", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultRotorWellSelectionSize));

        /// <summary>
        /// Dependency property indicating whether selection is to be allowed.
        /// </summary>
        public static readonly DependencyProperty IsAllowWellSelectionProperty = DependencyProperty.Register("IsAllowWellSelection", typeof(bool), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty ReadOnlyProperty = DependencyProperty.Register("ReadOnly", typeof(bool), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsReadonly)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to indicate if labels of wells are to be binded from input data.
        /// </summary>
        public static readonly DependencyProperty IsAssignLabelsProperty = DependencyProperty.Register("IsAssignLabels", typeof(bool), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsAssignLabels)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to for list of well positions.
        /// </summary>
        public static readonly DependencyProperty InputWellPositionsProperty = DependencyProperty.Register("InputWellPositions", typeof(List<string>), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackInputWellPositions)));

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty IsDisplayCenterWellProperty = DependencyProperty.Register("IsDisplayCenterWell", typeof(bool), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsDisplayCenterWell)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to set Assigned Well.
        /// </summary>
        public static readonly DependencyProperty DefaultWellTypeProperty = DependencyProperty.Register("DefaultWellType", typeof(OrderType), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(OrderType.Empty));

        /// <summary>
        /// Dependency property to welltype of center well.
        /// </summary>
        public static readonly DependencyProperty CenterWellTypeProperty = DependencyProperty.Register("CenterWellType", typeof(OrderType), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(OrderType.Empty));

        /// <summary>
        /// Dependency property to set spacing between labels.
        /// </summary>
        public static readonly DependencyProperty LabelSpacingProperty = DependencyProperty.Register("LabelSpacing", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(DefaultLabelSpacing, new PropertyChangedCallback(CallbackLabelSpacing)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property indicating number where wells count will restart.
        /// </summary>
        public static readonly DependencyProperty RestartWellsAtProperty = DependencyProperty.Register("RestartWellsAt", typeof(int), typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(DefaultRestartWellsAt, new PropertyChangedCallback(CallbackRestartWells)) { BindsTwoWayByDefault = true });

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="RotorGenePlateControl"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Required for the project")]
        static RotorGenePlateControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RotorGenePlateControl), new FrameworkPropertyMetadata(typeof(RotorGenePlateControl)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RotorGenePlateControl" /> class.
        /// </summary>
        public RotorGenePlateControl()
        {
            this.Unloaded += new RoutedEventHandler(RotorGenePlateControl_Unloaded);
            this.Loaded += new RoutedEventHandler(RotorGenePlateControl_Loaded);
        }

        private void RotorGenePlateControl_Loaded(object sender, RoutedEventArgs e)
        {
            UnSubscribeEvent(true);
            UnSubscribeEvent(false);
        }

        private void RotorGenePlateControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnSubscribeEvent(true);
        }

        #endregion Construction

        #region Control Overrides

        /// <summary>
        /// Called when the Template is applied to the control. Template is defined in Style file.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ReadOnly = false;
            CreateCircles();
        }

        #endregion Control Overrides

        #region Properties

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

        /// <summary>
        /// Gets or sets a value indicating whether this control is input plate.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is of input plate type; otherwise, <c>false</c>.
        /// </value>
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
        /// Gets or sets a value indicating number of rows in plate.
        /// </summary>
        /// <value>
        /// Number of rows.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating spacing between any two wells.
        /// </summary>
        /// <value>
        /// Integer value spacifing space between two wells.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating total number of well count.
        /// </summary>
        /// <value>
        /// Integer value spacifing totals wells.
        /// </value>
        public int WellsCount
        {
            get
            {
                return (int)GetValue(WellsCountProperty);
            }

            set
            {
                SetValue(WellsCountProperty, value);
                CreateCircles();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating the diameter of well.
        /// </summary>
        /// <value>
        /// Integer value spacifing well diameter.
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
        /// Size of outer selection circle when clicked, in integer format.
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

        /// <summary>
        /// Gets or sets a value indicating whether selection is to be allowed.
        /// </summary>
        /// <value>
        /// <c>True</c> if selection is allowed; otherwise, <c>false</c>.
        /// </value>
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

        /// <summary>
        /// Gets or sets a value indicating whether this control is Read Only/Not Editable.
        /// </summary>
        /// <value>
        /// <c>True</c> if this control is read only; otherwise, <c>false</c>.
        /// </value>
        public bool ReadOnly
        {
            get
            {
                return (bool)GetValue(ReadOnlyProperty);
            }

            set
            {
                SetValue(ReadOnlyProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Label are to be assigned from input data.
        /// </summary>
        /// <value>
        /// <c>True</c> if label are to be binded from input data; otherwise, <c>false</c>.
        /// </value>
        public bool IsAssignLabels
        {
            get
            {
                return (bool)GetValue(IsAssignLabelsProperty);
            }

            set
            {
                SetValue(IsAssignLabelsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the binding data to assign the labels.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "NM: Required to assign dependency property from child classes. Not allowing to initialize in constructor if setter omitted.")]
        public List<string> InputWellPositions
        {
            get
            {
                return (List<string>)GetValue(InputWellPositionsProperty);
            }

            set
            {
                SetValue(InputWellPositionsProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well at center of canvas to be displayed.
        /// </summary>
        /// <value>
        /// <c>True</c> if well at center to display; otherwise, <c>false</c>.
        /// </value>
        public bool IsDisplayCenterWell
        {
            get
            {
                return (bool)GetValue(IsDisplayCenterWellProperty);
            }

            set
            {
                SetValue(IsDisplayCenterWellProperty, value);
                CreateCircles();
            }
        }

        /// <summary>
        /// Gets or sets the well controls which has been populated on canvas to be accessed from child classes.
        /// </summary>
        /// <value>
        /// The collection of all WellPositionControls.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NM: Plate control code was done on the basis of row and column and searching and assignment of well and array was used for easy searching and assignment of well.")]
        public WellPositionControl[] Children
        {
            get
            {
                return children;
            }

            set
            {
                children = value;
            }
        }

        /// <summary>
        /// Gets or sets the type of well which will be shown by default.
        /// </summary>
        /// <value>
        /// The Legend of default wells.
        /// </value>
        public OrderType DefaultWellType
        {
            get
            {
                return (OrderType)GetValue(DefaultWellTypeProperty);
            }

            set
            {
                if (DefaultWellType != value)
                {
                    SetValue(DefaultWellTypeProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of center well.
        /// </summary>
        /// <value>
        /// The Legend of default well at center.
        /// </value>
        public OrderType CenterWellType
        {
            get
            {
                return (OrderType)GetValue(CenterWellTypeProperty);
            }

            set
            {
                if (CenterWellType != value)
                {
                    SetValue(CenterWellTypeProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating spacing between labels.
        /// </summary>
        /// <value>
        /// The spacing between labels.
        /// </value>
        public int LabelSpacing
        {
            get
            {
                return (int)GetValue(LabelSpacingProperty);
            }

            set
            {
                if (LabelSpacing != value)
                {
                    SetValue(LabelSpacingProperty, value);
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating number where wells count will restart.
        /// </summary>
        /// <value>
        /// Integer number where well names will be restarted.
        /// </value>
        public int RestartWellsAt
        {
            get
            {
                return (int)GetValue(RestartWellsAtProperty);
            }

            set
            {
                SetValue(RestartWellsAtProperty, value);
                CreateCircles();
            }
        }

        #endregion Properties

        #region Methods

        private void UnSubscribeEvent(bool flag)
        {
            if (Children != null)
            {
                for (int i = 0; i < WellsCount; i++)
                {
                    WellPositionControl wp = (WellPositionControl)Children[i];

                    if (flag == true)
                    {
                        wp.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                    }
                    else
                    {
                        wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the canvas with all well controls in circular order.
        /// </summary>
        public void CreateCircles()
        {
            wellCanvasSize = WellDiameter + WellPadding;
            angle = 90;

            canvas = (Canvas)GetTemplateChild(PartCanvas);
            if (canvas != null)
            {
                canvas.Children.Clear();
                children = new WellPositionControl[(int)WellsCount];
                if (IsDisplayCenterWell == true)
                {
                    children = new WellPositionControl[(int)(WellsCount + 1)];
                }

                double requiredPerimeterCircumference = (WellsCount * wellCanvasSize) / Math.PI;
                canvasWidth = requiredPerimeterCircumference;
                canvasHeight = requiredPerimeterCircumference;
                canvas.Width = canvasWidth;
                canvas.Height = canvasHeight;
                angle = DegreeToRadian(angle);
                double angleShare = 360 / (double)WellsCount;
                int posY = 0;
                labelList = new List<string>();
                bool isSeriesChanged = true;
                for (int i = 0; i < WellsCount; i++)
                {
                    double x = (Math.Cos(angle) * canvasWidth / 2) + (canvas.Width / 2);
                    double y = (Math.Sin(angle) * canvasHeight / 2) + (canvas.Height / 2);
                    WellPositionControl wp = new WellPositionControl();
                    wp.Legend = DefaultWellType;
                    wp.IsInputPlate = IsWellInputPlate;
                    wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    wp.CanvasHeight = wellCanvasSize;
                    wp.CanvasWidth = wellCanvasSize;
                    wp.WellDiameter = WellDiameter;
                    wp.IsWellTransparent = true;
                    wp.WellSelectionSize = RotorWellSelectionSize;
                    wp.Margin = new Thickness(x, y, 0, 0);
                    wp.WellPosition = new WellPosition() { PositionX = i + 1, PositionY = posY + 1 };
                    ////wp.WellName = GetWellName(i + 1, posY + 1);
                    wp.WellName = AssignLabelsFromInputData(i);
                    wp.WellPosition.Label = wp.WellName;
                    wp.WellToolTip = wp.WellName;
                    wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                    children[i] = wp;
                    canvas.Children.Add(wp);
                    if (isSeriesChanged == true)
                    {
                        labelList.Add(wp.WellName);
                        isSeriesChanged = false;
                    }

                    if ((i + 1) % RestartWellsAt == 0)
                    {
                        posY = posY + 1;

                        isSeriesChanged = true;
                    }

                    angle += DegreeToRadian(angleShare);
                }

                CreatePlateLabels();
                CreateCenterWell();
            }
        }

        private string AssignLabelsFromInputData(int pos)
        {
            string returnText = string.Empty;
            if (IsAssignLabels == true && InputWellPositions != null)
            {
                if (InputWellPositions.Count == WellsCount)
                {
                    returnText = InputWellPositions[pos];
                }
            }

            return returnText;
        }

        private void CreateCenterWell()
        {
            if (IsDisplayCenterWell == true)
            {
                WellPositionControl wp = new WellPositionControl();
                wp.Legend = CenterWellType;
                wp.IsInputPlate = IsWellInputPlate;
                wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                wp.CanvasHeight = wellCanvasSize;
                wp.CanvasWidth = wellCanvasSize;
                wp.WellDiameter = WellDiameter;
                wp.IsWellTransparent = true;
                wp.WellSelectionSize = RotorWellSelectionSize;
                wp.Margin = new Thickness(canvas.Width / 2, canvas.Height / 2, 0, 0);
                wp.WellPosition = new WellPosition() { PositionX = 0, PositionY = 0, Label = "X0" };
                wp.WellName = "X0";
                wp.WellToolTip = wp.WellName;
                wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                children[WellsCount] = wp;
                canvas.Children.Add(wp);
            }
        }

        /// <summary>
        /// Gets the name of well based on x and y positions.
        /// </summary>
        /// <returns>
        /// Returns the name of the well.
        /// </returns>
        /// <param name="positionX">The x position of well.</param>
        /// <param name="positionY">The y position of well.</param>
        public string GetWellName(double positionX, double positionY)
        {
            int tempRowName;
            int tempColno;
            checked
            {
                tempRowName = 64 + Convert.ToInt32(positionY);
                tempColno = Convert.ToInt32(positionX) % RestartWellsAt;
                if (tempColno == 0)
                {
                    tempColno = RestartWellsAt;
                }
            }

            return (char)tempRowName + tempColno.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Ceates labels on circular plate.
        /// </summary>
        public void CreatePlateLabels()
        {
            wellCanvasSize = WellDiameter + WellPadding + LabelSpacing;
            angle = 89;
            double requiredPerimeterCircumference = (WellsCount * wellCanvasSize) / Math.PI;
            canvasWidth = requiredPerimeterCircumference;
            canvasHeight = requiredPerimeterCircumference;
            angle = DegreeToRadian(angle);
            double angleShare = 360 / (double)WellsCount;
            int posY = 0;
            for (int i = 0; i < WellsCount; i++)
            {
                double x = (Math.Cos(angle) * canvasWidth / 2) + (canvas.Width / 2);
                double y = (Math.Sin(angle) * canvasHeight / 2) + (canvas.Height / 2);
                string temp = GetWellName(i + 1, posY + 1);
                if (labelList.Contains(temp))
                {
                    Label lblRowHeader = new Label();
                    lblRowHeader.Content = temp;
                    lblRowHeader.FontFamily = new FontFamily("Arial");
                    lblRowHeader.FontWeight = FontWeights.Bold;
                    lblRowHeader.FontSize = 10;
                    lblRowHeader.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Right;
                    lblRowHeader.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    lblRowHeader.Height = 20;
                    lblRowHeader.Padding = new Thickness(0);
                    lblRowHeader.BorderThickness = new Thickness(0);
                    lblRowHeader.BorderBrush = new SolidColorBrush(Colors.Black);
                    lblRowHeader.UseLayoutRounding = true;
                    lblRowHeader.Margin = new Thickness(x + 1, y - 1, 0, 0);
                    canvas.Children.Add(lblRowHeader);
                }

                if ((i + 1) % RestartWellsAt == 0)
                {
                    posY = posY + 1;
                }

                angle += DegreeToRadian(angleShare);
            }
        }

        private void WP_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            CurrentSelectedWellControl = (WellPositionControl)sender;

            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
            {
                foreach (WellPositionControl wp in children)
                {
                    wp.IsSelected = false;
                }
            }

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (LastSelectedWellControl != null && CurrentSelectedWellControl != null)
                {
                    int minX = LastSelectedWellControl.WellPosition.PositionX;
                    int maxX = CurrentSelectedWellControl.WellPosition.PositionX;
                    for (int x = minX - 1; x < maxX; x++)
                    {
                        if (children[x].IsSelected == false)
                        {
                            children[x].IsSelected = true;
                        }
                    }
                }
            }

            CurrentSelectedWellControl.IsSelected = true;
            if (CurrentSelectedWellControl != null)
            {
                LastSelectedWellControl = CurrentSelectedWellControl;
            }
        }

        private double DegreeToRadian(double inputAngle)
        {
            return Math.PI * inputAngle / 180.0;
        }

        private static void CallbackRowshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.RowShapes = (int)args.NewValue;
        }

        private static void CallbackWellsCount(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rot = (RotorGenePlateControl)property;
            rot.WellsCount = (int)args.NewValue;
        }

        private static void CallbackWellDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rot = (RotorGenePlateControl)property;
            rot.WellDiameter = (int)args.NewValue;
        }

        private static void CallbackIsReadonly(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.ReadOnly = (bool)args.NewValue;
        }

        private static void CallbackIsAssignLabels(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.IsAssignLabels = (bool)args.NewValue;
        }

        private static void CallbackLabelSpacing(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.LabelSpacing = (int)args.NewValue;
        }

        private static void CallbackRestartWells(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rot = (RotorGenePlateControl)property;
            rot.RestartWellsAt = (int)args.NewValue;
        }

        private static void CallbackInputWellPositions(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.InputWellPositions = (List<string>)args.NewValue;
        }

        private static void CallbackIsDisplayCenterWell(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGenePlateControl rect = (RotorGenePlateControl)property;
            rect.IsDisplayCenterWell = (bool)args.NewValue;
        }

        #endregion Methods
    }
}