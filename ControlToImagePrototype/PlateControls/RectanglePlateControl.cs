// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates rectangle plate control and requires no info about x and y positions.
    /// Control is generated based on row shapes and col shapes.
    /// </summary>
    public class RectanglePlateControl : Control
    {
        #region Member Variables

        private const string PartCanvas = "PART_CANVAS1";
        private const string PartCanvasBorder = "PART_CANVAS1_BORDER";
        private const double DefaultRectangleWidth = 250;
        private const double DefaultRectangleHeight = 300;
        private const int DefaultDiameterEachShape = 17;
        private const int DefaultRowSpacing = 5;
        private const int DefaultColSpacing = 5;
        private const int DefaultHeaderSpacingLeft = 10;
        private const int DefaultHeaderSpacingTop = 5;
        private const int DefaultRotorWellSelectionSize = 25;
        private const int DefaultRowShapes = 8;
        private const int DefaultColShapes = 12;
        private bool isDown;
        private int labelWidth;
        private int labelHeight;
        private Canvas canvas;
        private Label lblRowHeader;
        private Label lblColHeader;
        private WellPositionControl[][] children;
        private WellPositionControl lastSelectedWellControl;
        private WellPositionControl currentSelectedWellControl;

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely.
        /// </summary>
        public static readonly DependencyProperty PositionContainmentTypeProperty = DependencyProperty.Register("PositionContainmentType", typeof(OrderType), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(OrderType.Empty));

        /// <summary>
        /// Dependency property to specify Width of Rectangle where wellposition control is to be drawn.
        /// </summary>
        public static readonly DependencyProperty RectangleWidthProperty = DependencyProperty.Register("RectangleWidth", typeof(double), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRectangleWidth));

        /// <summary>
        /// Dependency property to specify height of Rectangle where wellposition control is to be drawn.
        /// </summary>
        public static readonly DependencyProperty RectangleHeightProperty = DependencyProperty.Register("RectangleHeight", typeof(double), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRectangleHeight));

        /// <summary>
        /// Dependency property to specify spacing between column headers and well controls.
        /// </summary>
        public static readonly DependencyProperty HeaderSpacingLeftProperty = DependencyProperty.Register("HeaderSpacingLeft", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultHeaderSpacingLeft));

        /// <summary>
        /// Dependency property to specify spacing between row headers and well controls.
        /// </summary>
        public static readonly DependencyProperty HeaderSpacingTopProperty = DependencyProperty.Register("HeaderSpacingTop", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultHeaderSpacingTop));

        /// <summary>
        /// Dependency property to show visible border to rectangle control containing well controls.
        /// </summary>
        public static readonly DependencyProperty IsBorderVisibleProperty = DependencyProperty.Register("IsBorderVisible", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        ///  Temporary proeprty just to see control generation order.
        /// </summary>
        public static readonly DependencyProperty IsWellControlProperty = DependencyProperty.Register("IsWellControl", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty RectWellSelectionSizeProperty = DependencyProperty.Register("RectWellSelectionSize", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRotorWellSelectionSize));

        /// <summary>
        /// Dependency property to indicate no of wells in each row. default rows is 8.
        /// </summary>
        public static readonly DependencyProperty RowShapesProperty = DependencyProperty.Register("RowShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRowShapes, new PropertyChangedCallback(CallbackRowshapes)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency Property to indicate no of wells in each Column. default is 12.
        /// </summary>
        public static readonly DependencyProperty ColShapesProperty = DependencyProperty.Register("ColShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultColShapes, new PropertyChangedCallback(CallbackColshapes)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to specify size of well control in diameter.
        /// </summary>
        public static readonly DependencyProperty DiameterEachShapeProperty = DependencyProperty.Register("DiameterEachShape", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultDiameterEachShape, new PropertyChangedCallback(CallbackDiameter)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to specify spacing between two rows of well controls.
        /// </summary>
        public static readonly DependencyProperty RowSpacingProperty = DependencyProperty.Register("RowSpacing", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRowSpacing, new PropertyChangedCallback(CallbackRowspacing)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to specify spacing between adjacent columns of well controls.
        /// </summary>
        public static readonly DependencyProperty ColSpacingProperty = DependencyProperty.Register("ColSpacing", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultColSpacing, new PropertyChangedCallback(CallbackColspacing)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control.
        /// </summary>
        public static readonly DependencyProperty IsWellInputPlateProperty = DependencyProperty.Register("IsWellInputPlate", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to set Assigned Well.
        /// </summary>
        public static readonly DependencyProperty IsAssignedProperty = DependencyProperty.Register("IsAssigned", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Dependency property to set Assigned Well.
        /// </summary>
        public static readonly DependencyProperty DefaultWellTypeProperty = DependencyProperty.Register("DefaultWellType", typeof(OrderType), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(OrderType.Empty));

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="RectanglePlateControl"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "RV: This has to be done in constructor.")]
        static RectanglePlateControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RectanglePlateControl), new FrameworkPropertyMetadata(typeof(RectanglePlateControl)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectanglePlateControl" /> class.
        /// </summary>
        public RectanglePlateControl()
        {
            this.Unloaded += new RoutedEventHandler(RectanglePlateControl_Unloaded);
            this.Loaded += new RoutedEventHandler(RectanglePlateControl_Loaded);
        }

        private void RectanglePlateControl_Loaded(object sender, RoutedEventArgs e)
        {
            UnSubscribeEvent(true);
            UnSubscribeEvent(false);
        }

        private void RectanglePlateControl_Unloaded(object sender, RoutedEventArgs e)
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
            CreateGrid();
        }

        #endregion Control Overrides

        #region Properties

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
        /// Gets or sets a value indicating whether well is assigned or not not.
        /// </summary>
        /// <value>
        /// <c>True</c> if this well is assigned; otherwise, <c>false</c>.
        /// </value>
        public bool IsAssigned
        {
            get
            {
                return (bool)GetValue(IsAssignedProperty);
            }

            set
            {
                if (IsAssigned != value)
                {
                    SetValue(IsAssignedProperty, value);
                    if (canvas != null)
                    {
                        SetAssigned();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to show border to the rectangle control.
        /// </summary>
        /// <value>
        /// Boolean flag mentioning border visibility.
        /// </value>
        public bool IsBorderVisible
        {
            get
            {
                return (bool)GetValue(IsBorderVisibleProperty);
            }

            set
            {
                SetValue(IsBorderVisibleProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether if to show border to the rectangle control.
        /// </summary>
        /// <value>
        /// Boolean flag mentioning border visibility.
        /// </value>
        public bool IsWellControl
        {
            get
            {
                return (bool)GetValue(IsWellControlProperty);
            }

            set
            {
                SetValue(IsWellControlProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets spacing between column headers and well controls.
        /// </summary>
        /// <value>
        /// Spacing between column header and well controls in int format.
        /// </value>
        public int HeaderSpacingLeft
        {
            get
            {
                return (int)GetValue(HeaderSpacingLeftProperty);
            }

            set
            {
                SetValue(HeaderSpacingLeftProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets spacing between row headers and well controls.
        /// </summary>
        /// <value>
        /// Spacing between row header and well controls in int format.
        /// </value>
        public int HeaderSpacingTop
        {
            get
            {
                return (int)GetValue(HeaderSpacingTopProperty);
            }

            set
            {
                SetValue(HeaderSpacingTopProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets value of outer selection circle when well is clicked.
        /// </summary>
        /// <value>
        /// Size of circle when clicked in integer.
        /// </value>
        public int RectWellSelectionSize
        {
            get
            {
                return (int)GetValue(RectWellSelectionSizeProperty);
            }

            set
            {
                SetValue(RectWellSelectionSizeProperty, value);
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
                CreateGrid();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating number of columns in plate.
        /// </summary>
        /// <value>
        /// Number of columns.
        /// </value>
        public int ColShapes
        {
            get
            {
                return (int)GetValue(ColShapesProperty);
            }

            set
            {
                SetValue(ColShapesProperty, value);
                CreateGrid();
            }
        }

        /// <summary>
        /// Gets or sets size of each well control.
        /// </summary>
        /// <value>
        /// Diameter of each well.
        /// </value>
        public int DiameterEachShape
        {
            get
            {
                return (int)GetValue(DiameterEachShapeProperty);
            }

            set
            {
                SetValue(DiameterEachShapeProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets spacing between adjacent rows.
        /// </summary>
        /// <value>
        /// Spacing between adjacent rows in int format.
        /// </value>
        public int RowSpacing
        {
            get
            {
                return (int)GetValue(RowSpacingProperty);
            }

            set
            {
                SetValue(RowSpacingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets spacing between adjacent columns.
        /// </summary>
        /// <value>
        /// Spacing between adjacent columns in int format.
        /// </value>
        public int ColSpacing
        {
            get
            {
                return (int)GetValue(ColSpacingProperty);
            }

            set
            {
                SetValue(ColSpacingProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the Rectangle.
        /// </summary>
        /// <value>
        /// The width of the Rectangle.
        /// </value>
        public double RectangleWidth
        {
            get
            {
                return (double)GetValue(RectangleWidthProperty);
            }

            set
            {
                SetValue(RectangleWidthProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the height of the Rectangle.
        /// </summary>
        /// <value>
        /// The height of the Rectangle.
        /// </value>
        public double RectangleHeight
        {
            get
            {
                return (double)GetValue(RectangleHeightProperty);
            }

            set
            {
                SetValue(RectangleHeightProperty, value);
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
        /// Gets or sets the well controls which has been populated on canvas to be accessed from child classes.
        /// </summary>
        /// <value>
        /// The collection of all WellPositionControls.
        /// </value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "NM: Plate control code was done on the basis of row and column and searching and assignment of well and array was used for easy searching and assignment of well.")]
        public WellPositionControl[][] Children
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
        /// Gets or sets the Legend types defined.
        /// </summary>
        /// <value>
        /// The Legend/OrderType.
        /// </value>
        public OrderType PositionContainmentType
        {
            get
            {
                return (OrderType)GetValue(PositionContainmentTypeProperty);
            }

            set
            {
                SetValue(PositionContainmentTypeProperty, value);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Subscribe/Unsubscribe all events for cleanup.
        /// </summary>
        /// <param name="flag">Boolean Flag.</param>
        private void UnSubscribeEvent(bool flag)
        {
            if (Children != null)
            {
                for (int row = 0; row < RowShapes; row++)
                {
                    for (int col = 0; col < ColShapes; col++)
                    {
                        WellPositionControl wp = (WellPositionControl)Children[row][col];
                        if (wp != null)
                        {
                            if (flag == true)
                            {
                                wp.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                                wp.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(WP_MouseUp);
                                wp.MouseMove -= new MouseEventHandler(WP_MouseMove);
                            }
                            else
                            {
                                wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                                wp.MouseUp += new System.Windows.Input.MouseButtonEventHandler(WP_MouseUp);
                                wp.MouseMove += new MouseEventHandler(WP_MouseMove);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Creates the canvas with all controls.
        /// </summary>
        private void CreateGrid()
        {
            canvas = (Canvas)GetTemplateChild(PartCanvas);
            if (canvas != null)
            {
                labelWidth = DiameterEachShape + 10;
                labelHeight = DiameterEachShape + 10;
                RectangleWidth = (ColShapes * DiameterEachShape) + (ColShapes * ColSpacing) + HeaderSpacingTop + labelWidth;
                RectangleHeight = (RowShapes * DiameterEachShape) + (RowShapes * RowSpacing) + HeaderSpacingLeft + labelHeight;
                canvas.Width = RectangleWidth;
                canvas.Height = RectangleHeight;
                if (IsBorderVisible == true)
                {
                    Border b = (Border)GetTemplateChild(PartCanvasBorder);
                    b.Width = RectangleWidth;
                    b.Height = RectangleHeight;
                    b.Margin = canvas.Margin;
                    b.BorderBrush = new SolidColorBrush(Colors.Red);
                }

                Grid g1 = CreateRectangles();
                canvas.Children.Clear();
                canvas.Children.Add(g1);
            }
        }

        private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Label tempLabel = sender as Label;
            int row = Grid.GetRow(tempLabel);
            int col = Grid.GetColumn(tempLabel);

            if (row == 0 || col == 0)
            {
                foreach (WellPositionControl[] wp in children)
                {
                    foreach (WellPositionControl wp1 in wp)
                    {
                        wp1.IsSelected = false;
                    }
                }
            }

            if (col == 0)
            {
                foreach (WellPositionControl[] wp in children)
                {
                    foreach (WellPositionControl wp1 in wp)
                    {
                        if (wp1.WellPosition.PositionY == row)
                        {
                            wp1.IsSelected = true;
                        }
                    }
                }
            }

            if (row == 0)
            {
                foreach (WellPositionControl[] wp in children)
                {
                    foreach (WellPositionControl wp1 in wp)
                    {
                        if (wp1.WellPosition.PositionX == col)
                        {
                            wp1.IsSelected = true;
                        }
                    }
                }
            }
        }

        private void SetAssigned()
        {
            if (IsAssigned == true)
            {
                CurrentSelectedWellControl.IsInputAssigned = true;
            }
            else
            {
                CurrentSelectedWellControl.IsInputAssigned = false;
            }
        }

        private Grid CreateRectangles()
        {
            children = new WellPositionControl[RowShapes][];
            for (int a1 = 0; a1 < RowShapes; a1++)
            {
                children[a1] = new WellPositionControl[ColShapes];
            }

            int baseChar = 64;
            var grid1 = new Grid();

            if (IsWellControl == false)
            {
                grid1.ShowGridLines = true;
            }

            for (int col = 0; col <= ColShapes; col++)
            {
                grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(DiameterEachShape + ColSpacing) });

                lblColHeader = new Label();
                string colHeader = col.ToString(CultureInfo.CurrentCulture);
                lblColHeader.Content = (string)colHeader;
                lblColHeader.FontFamily = new FontFamily("Arial");
                lblColHeader.FontWeight = FontWeights.Bold;
                lblColHeader.FontSize = 10;
                lblColHeader.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lblColHeader.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lblColHeader.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                lblColHeader.SetValue(Grid.RowProperty, 0);
                lblColHeader.SetValue(Grid.ColumnProperty, col);
                lblColHeader.Width = DiameterEachShape + ColSpacing;
                lblColHeader.Padding = new Thickness(-5);
                lblColHeader.MouseDown += Label_MouseDown;
                if (IsWellControl == false)
                {
                    lblColHeader.BorderThickness = new Thickness(1);
                    lblColHeader.BorderBrush = new SolidColorBrush(Colors.Black);
                    lblColHeader.UseLayoutRounding = true;
                }

                lblColHeader.Margin = new Thickness(0, 0, 0, HeaderSpacingTop);
                if (col > 0)
                {
                    grid1.Children.Add(lblColHeader);
                }
            }

            for (int row = 0; row <= RowShapes; row++)
            {
                grid1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(DiameterEachShape + RowSpacing) });
                int temp;
                temp = baseChar + row;
                lblRowHeader = new Label();
                lblRowHeader.Content = (char)temp;
                lblRowHeader.FontFamily = new FontFamily("Arial");
                lblRowHeader.FontWeight = FontWeights.Bold;
                lblRowHeader.FontSize = 10;
                lblRowHeader.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lblRowHeader.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lblRowHeader.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                lblRowHeader.SetValue(Grid.RowProperty, row);
                lblRowHeader.SetValue(Grid.ColumnProperty, 0);
                lblRowHeader.Height = DiameterEachShape + RowSpacing;
                lblRowHeader.Padding = new Thickness(0);
                lblRowHeader.MouseDown += Label_MouseDown;
                if (IsWellControl == false)
                {
                    lblRowHeader.BorderThickness = new Thickness(1);
                    lblRowHeader.BorderBrush = new SolidColorBrush(Colors.Black);

                    lblRowHeader.UseLayoutRounding = true;
                }

                lblRowHeader.Margin = new Thickness(0, 0, HeaderSpacingLeft, 0);
                if (row > 0)
                {
                    grid1.Children.Add(lblRowHeader);
                }
            }

            int global = 0;
            for (int row = 1; row <= RowShapes; row++)
            {
                for (int col = 1; col <= ColShapes; col++)
                {
                    global = global + 1;
                    WellPositionControl wp = new WellPositionControl();
                    if (RowShapes == 8)
                    {
                        wp.LegendPosition = new WellPosition() { PositionX = 1, PositionY = 1 };
                    }
                    else if (RowShapes == 16)
                    {
                        wp.LegendPosition = new WellPosition() { PositionX = 1, PositionY = 1 };
                    }

                    wp.Legend = DefaultWellType;
                    wp.IsInputPlate = IsWellInputPlate;
                    wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    wp.CanvasHeight = DiameterEachShape + RowSpacing;
                    wp.CanvasWidth = DiameterEachShape + ColSpacing;
                    wp.WellDiameter = DiameterEachShape;
                    wp.WellSelectionSize = RectWellSelectionSize;
                    wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(WP_MouseDown);
                    wp.MouseUp += new System.Windows.Input.MouseButtonEventHandler(WP_MouseUp);
                    wp.MouseMove += new MouseEventHandler(WP_MouseMove);
                    wp.SetValue(Grid.RowProperty, row);
                    wp.SetValue(Grid.ColumnProperty, col);
                    wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };
                    wp.WellName = GetWellName(col, row);
                    wp.WellPosition.Label = wp.WellName;
                    children[row - 1][col - 1] = wp;
                    if (IsWellControl == true)
                    {
                        grid1.Children.Add(wp);
                    }
                }
            }

            return grid1;
        }

        private void WP_MouseMove(object sender, MouseEventArgs e)
        {
            CurrentSelectedWellControl = (WellPositionControl)sender;
            if (isDown == true)
            {
                SelectOnDrag();
            }

            if (Mouse.LeftButton != MouseButtonState.Pressed)
            {
                isDown = false;
            }
        }

        public void WP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            SelectOnDrag();
        }

        private void WP_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDown = true;
            CurrentSelectedWellControl = (WellPositionControl)sender;
            if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
            {
                foreach (WellPositionControl[] wp in children)
                {
                    foreach (WellPositionControl wp1 in wp)
                    {
                        wp1.IsSelected = false;
                    }
                }
            }

            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (LastSelectedWellControl != null && CurrentSelectedWellControl != null)
                {
                    int minX = Math.Min(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);
                    int maxX = Math.Max(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);

                    int minY = Math.Min(CurrentSelectedWellControl.WellPosition.PositionY, LastSelectedWellControl.WellPosition.PositionY);
                    int maxY = Math.Max(CurrentSelectedWellControl.WellPosition.PositionY, LastSelectedWellControl.WellPosition.PositionY);

                    for (int x = minX - 1; x < maxX; x++)
                    {
                        for (int y = minY - 1; y < maxY; y++)
                        {
                            children[y][x].IsSelected = true;
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

        /// <summary>
        /// Gets the name of well based on x and y positions.
        /// </summary>
        /// <returns>
        /// Returns the name of the well.
        /// </returns>
        /// <param name="positionY">The x position of well.</param>
        /// <param name="positionX">The y position of well.</param>
        public string GetWellName(double positionY, double positionX)
        {
            int tempRowName;
            int tempColno;
            int resetcol;
            checked
            {
                tempRowName = (int)'A' + Convert.ToInt32(positionX) - 1;
                resetcol = Convert.ToInt32(ColShapes) + 1;
                tempColno = Convert.ToInt32(positionY) % resetcol;
            }

            return (char)tempRowName + tempColno.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Sets current and last selected well control to support multiple well selection on mouse drag.
        /// </summary>
        public void SelectOnDrag()
        {
            if (isDown == true)
            {
                if (CurrentSelectedWellControl != null && LastSelectedWellControl != null)
                {
                    if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
                    {
                        foreach (WellPositionControl[] wp in children)
                        {
                            foreach (WellPositionControl wp1 in wp)
                            {
                                wp1.IsSelected = false;
                            }
                        }
                    }

                    int minX = Math.Min(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);
                    int maxX = Math.Max(CurrentSelectedWellControl.WellPosition.PositionX, LastSelectedWellControl.WellPosition.PositionX);

                    int minY = Math.Min(CurrentSelectedWellControl.WellPosition.PositionY, LastSelectedWellControl.WellPosition.PositionY);
                    int maxY = Math.Max(CurrentSelectedWellControl.WellPosition.PositionY, LastSelectedWellControl.WellPosition.PositionY);

                    for (int x = minX - 1; x < maxX; x++)
                    {
                        for (int y = minY - 1; y < maxY; y++)
                        {
                            children[y][x].IsSelected = true;
                        }
                    }
                }
            }
        }

        private static void CallbackIsWellInputPlate(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.IsWellInputPlate = (bool)args.NewValue;
        }

        private static void CallbackColspacing(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.ColSpacing = (int)args.NewValue;
        }

        private static void CallbackRowspacing(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.RowSpacing = (int)args.NewValue;
        }

        private static void CallbackDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.DiameterEachShape = (int)args.NewValue;
        }

        private static void CallbackColshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.ColShapes = (int)args.NewValue;
        }

        private static void CallbackRowshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.RowShapes = (int)args.NewValue;
        }

        #endregion Methods
    }
}