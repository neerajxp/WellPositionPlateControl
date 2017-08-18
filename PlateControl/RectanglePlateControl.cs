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

namespace PlateControl
{
    public class RectanglePlateControl : Control
    {
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

        private Canvas canvas;
        private Label lblRowHeader;
        private Label lblColHeader;

        ///private List<WellPositionControl> Children;
        //private WellPositionControl[,] children;
        private WellPositionControl[][] children;

        private bool isDown;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "RV: This has to be done in constructor.")]
        static RectanglePlateControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RectanglePlateControl), new FrameworkPropertyMetadata(typeof(RectanglePlateControl)));
        }

        private int labelWidth;
        private int labelHeight;

        public RectanglePlateControl()
        {
        }

        public override void OnApplyTemplate()
        {
            ////use this for debugging control adjustment
            //IsWellControl = false;
            ////use this for debugging control adjustment
            base.OnApplyTemplate();
            //labelWidth = DiameterEachShape + 10;
            //labelHeight = DiameterEachShape + 10;
            //////Adjust Canvas size as per Well size, row/column spacing and Row/Column Header distance.
            //RectangleWidth = ColShapes * DiameterEachShape + ColShapes * ColSpacing + HeaderSpacingTop + labelWidth;
            //RectangleHeight = RowShapes * DiameterEachShape + RowShapes * RowSpacing + HeaderSpacingLeft + labelHeight;
            //////
            ////canvas.Width = RectangleWidth;
            ////canvas.Height = RectangleHeight;
            //if (IsBorderVisible == true)
            //{
            //    Border b = (Border)GetTemplateChild(PartCanvasBorder);
            //    b.Width = RectangleWidth;
            //    b.Height = RectangleHeight;
            //    b.Margin = canvas.Margin;
            //    b.BorderBrush = new SolidColorBrush(Colors.Red);
            //}
            CreateGrid();
            // this.canvas.PreviewMouseDown += new MouseButtonEventHandler(RectanglePlateControl_MouseDown);
            //this.MouseDown += new MouseButtonEventHandler(RectanglePlateControl_MouseDown);
            //this.MouseUp += new MouseButtonEventHandler(RectanglePlateControl_MouseUp);
            //this.MouseMove += new MouseEventHandler(RectanglePlateControl_MouseMove);
            //this.canvas.Background = new SolidColorBrush(Colors.LightCyan);
        }

        //private void RectanglePlateControl_MouseMove(object sender, MouseEventArgs e)
        //{
        //    if (sender is WellPositionControl)
        //    {
        //        CurrentSelectedWellControl = (WellPositionControl)sender;
        //        if (test.Contains("down"))
        //        {
        //            //CurrentSelectedWellControl = (WellPositionControl)sender;
        //            SelectOnDrag(CurrentSelectedWellControl);
        //        }
        //    }
        //}

        private void RectanglePlateControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //test.Add((WellPositionControl)sender);
            isDown = false;
            //CurrentSelectedWellControl = (WellPositionControl)sender;
            //SelectOnDrag();
        }

        private void RectanglePlateControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;

            //CurrentSelectedWellControl = (WellPositionControl)sender;
            // Clear all selection if ControlKey is NOT press
            //if (!Keyboard.IsKeyDown(Key.LeftCtrl) && !Keyboard.IsKeyDown(Key.RightCtrl))
            //{
            foreach (WellPositionControl[] wp in children)
            {
                foreach (WellPositionControl wp1 in wp)
                {
                    wp1.IsSelected = false;
                }
            }
            //}

            // Find the block created by pressing shift key and set IsSelected = true for all the control in that block.
            //if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            //{
            if (CurrentSelectedWellControl != null && LastSelectedWellControl != null)
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
            //}
            //  CurrentSelectedWellControl.IsSelected = true;
            //  if (CurrentSelectedWellControl != null)
            //  {
            //      LastSelectedWellControl = CurrentSelectedWellControl;
            //  }
        }

        private void CreateGrid()
        {
            canvas = (Canvas)GetTemplateChild(PartCanvas);
            labelWidth = DiameterEachShape + 10;
            labelHeight = DiameterEachShape + 10;
            ////Adjust Canvas size as per Well size, row/column spacing and Row/Column Header distance.
            RectangleWidth = ColShapes * DiameterEachShape + ColShapes * ColSpacing + HeaderSpacingTop + labelWidth;
            RectangleHeight = RowShapes * DiameterEachShape + RowShapes * RowSpacing + HeaderSpacingLeft + labelHeight;
            ////
            canvas.Width = RectangleWidth;
            canvas.Height = RectangleHeight;
            //IsBorderVisible = false;
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
            ///Make the grid readonly if flag is set.
            if (IsReadonly == true)
            {
                Label l1 = new Label();
                l1.Background = new SolidColorBrush(Colors.Aqua);
                l1.Background.Opacity = 0;
                l1.Height = RectangleHeight;
                l1.Width = RectangleWidth;
                canvas.Children.Add(l1);
            }
        }

        private void Label_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ////int row = Grid.GetRow((Label)sender);
            ////int col = Grid.GetColumn((Label)sender);

            Label tempLabel = sender as Label;
            int row = Grid.GetRow(tempLabel);
            int col = Grid.GetColumn(tempLabel);

            ////Check if Row Header or Column Header of the grid is clicked.
            if (row == 0 || col == 0)
            {
                foreach (WellPositionControl[] wp in children)
                {
                    foreach (WellPositionControl wp1 in wp)
                    {
                        wp1.IsSelected = false;
                    }
                    //// wp.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(wp_PreviewMouseDown);
                }
            }

            //Select entire row of selected row header
            if (col == 0)
            {
                ////var ControlRow = Children.FindAll(p => p.WellPosition.PositionX == row);
                ////foreach (WellPositionControl wp in ControlRow)
                ////{
                ////    //On Row Select, select if its non empty well
                ////    if (wp.Legend != PositionContainment.Empty)
                ////    {
                ////        wp.IsSelected = true;
                ////    }
                ////}
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

            //Select entire column of selected column header
            if (row == 0)
            {
                //var ControlColumn = Children.FindAll(p => p.WellPosition.PositionY == col);
                //foreach (WellPositionControl wp in ControlColumn)
                //{
                //    //On column Select, select if its non empty well
                //    if (wp.Legend != PositionContainment.Empty)
                //    {
                //        wp.IsSelected = true;
                //    }
                //}
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

        private Grid CreateRectangles()
        {
            //// PosX  = Column no, PosY = Row No, array will be accessed as Array[Row][Column].

            ////Children = new List<WellPositionControl>();
            children = new WellPositionControl[RowShapes][];
            for (int a1 = 0; a1 < RowShapes; a1++)
            {
                children[a1] = new WellPositionControl[ColShapes];
            }

            // int RowShapes = 5;
            // int ColShapes = 5;
            //65 = A
            int baseChar = 64;

            //bool IsFillHorizontalFirst = false;

            var grid1 = new Grid();
            //grid1.Margin = new Thickness(0, 50, 0, 0);
            if (IsWellControl == false)
            {
                grid1.ShowGridLines = true;
            }

            for (int col = 0; col <= ColShapes; col++)
            {
                grid1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(DiameterEachShape + ColSpacing) });

                //Label l1 = new Label();
                //l1.Content = col.ToString();
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
                    //used to have proper lines. sometimes dark-light line comes in pixels
                    lblColHeader.UseLayoutRounding = true;
                }
                ////sets label's top position from grid first column.
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
                //l1.Content = row.ToString();
                lblRowHeader.Content = (char)temp;

                lblRowHeader.FontFamily = new FontFamily("Arial");
                lblRowHeader.FontWeight = FontWeights.Bold;
                lblRowHeader.FontSize = 10;

                lblRowHeader.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                lblRowHeader.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                lblRowHeader.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                lblRowHeader.SetValue(Grid.RowProperty, row);
                lblRowHeader.SetValue(Grid.ColumnProperty, 0);

                //to see control at the center of shapes
                lblRowHeader.Height = DiameterEachShape + RowSpacing;
                lblRowHeader.Padding = new Thickness(0);

                lblRowHeader.MouseDown += Label_MouseDown;

                if (IsWellControl == false)
                {
                    lblRowHeader.BorderThickness = new Thickness(1);
                    lblRowHeader.BorderBrush = new SolidColorBrush(Colors.Black);
                    //used to have proper lines. sometimes dark-light line comes in pixels
                    lblRowHeader.UseLayoutRounding = true;
                }
                ////sets label's left position from grid first row.
                lblRowHeader.Margin = new Thickness(0, 0, HeaderSpacingLeft, 0);
                if (row > 0)
                {
                    grid1.Children.Add(lblRowHeader);
                }
            }

            int global = 0;

            ////swaps row with column if controls are to be generated in ... order
            //if (IsFillHorizontalFirst == true)
            //{
            //    //swap row and col counts
            //    int temp;
            //    temp = RowShapes;
            //    RowShapes = ColShapes;
            //    ColShapes = temp;
            //}

            for (int row = 1; row <= RowShapes; row++)
            {
                for (int col = 1; col <= ColShapes; col++)
                {
                    global = global + 1;

                    //Button b1 = new Button();
                    //// row.ToString() + col.ToString();
                    //b1.Content = global;
                    //b1.Width = DiameterEachShape;
                    //b1.Height = DiameterEachShape;
                    //b1.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    //b1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                    ////if (IsFillHorizontalFirst == true)
                    ////{
                    ////    b1.SetValue(Grid.RowProperty, col);
                    ////    b1.SetValue(Grid.ColumnProperty, row);

                    ////    int temp = baseChar + row;
                    ////    b1.Content = b1.Content.ToString() + (char)temp + row.ToString();
                    ////}
                    ////else
                    ////{
                    //b1.SetValue(Grid.RowProperty, row);
                    //b1.SetValue(Grid.ColumnProperty, col);
                    //int temp = baseChar + row;
                    //b1.Content = b1.Content.ToString() + (char)temp + col.ToString();
                    ////}

                    //if (IsWellControl == false)
                    //{
                    //    grid1.Children.Add(b1);
                    //}

                    WellPositionControl wp = new WellPositionControl();

                    if (RowShapes == 8)
                    {
                        // wp.WellOrigin = 6;
                        wp.LegendPosition = new WellPosition() { PositionX = 1, PositionY = 1 };
                    }
                    else if (RowShapes == 16)
                    {
                        // wp.WellOrigin = 2;
                        wp.LegendPosition = new WellPosition() { PositionX = 1, PositionY = 1 };
                        //   wp.LegendPosition = new WellPosition() { PositionX = 0.5, PositionY =0.5 };
                    }

                    ////ResourceDictionary myResources = new ResourceDictionary();

                    //  myResources.Source = new Uri(@"\Themes\Generic.xaml", UriKind.RelativeOrAbsolute);
                    // myResources.Source = new Uri(@"pack://application:,,,/Themes/Generic.xaml", UriKind.RelativeOrAbsolute);
                    // myResources.Source = new Uri(@"\Themes\WellPositionControlStyle.xaml", UriKind.Relative);

                    //Style testStyle = FindResource("WellPositionStyle") as Style;
                    // Style testStyle = myResources["WellPositionStyle"] as Style;
                    // wp.Style = testStyle;

                    // wp.Legend = PositionContainmentType;
                    wp.Legend = PositionContainment.Empty;

                    //wp.Legend = PositionContantment.Controls;
                    wp.IsInputPlate = IsWellInputPlate;
                    wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;

                    wp.CanvasHeight = DiameterEachShape + RowSpacing;
                    wp.CanvasWidth = DiameterEachShape + ColSpacing;
                    wp.WellDiameter = DiameterEachShape;
                    wp.WellSelectionSize = RectWellSelectionSize;

                    wp.MouseDown += new System.Windows.Input.MouseButtonEventHandler(wp_MouseDown);
                    wp.MouseUp += new System.Windows.Input.MouseButtonEventHandler(wp_MouseUp);
                    wp.MouseMove += new MouseEventHandler(wp_MouseMove);

                    // wp.ApplyTemplate();

                    //if (IsFillHorizontalFirst == true)
                    //{
                    //    int temp = baseChar + col;
                    //    wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };
                    //}
                    //else
                    //{
                    //    int temp = baseChar + row;
                    //    wp.WellPosition = new WellPosition() { PositionX = row, PositionY = col };
                    //}

                    //if (IsFillHorizontalFirst == true)
                    //{
                    //    wp.SetValue(Grid.RowProperty, col);
                    //    wp.SetValue(Grid.ColumnProperty, row);
                    //}
                    //else
                    //{
                    //    wp.SetValue(Grid.RowProperty, row);
                    //    wp.SetValue(Grid.ColumnProperty, col);
                    //}
                    ////New Code

                    wp.SetValue(Grid.RowProperty, row);
                    wp.SetValue(Grid.ColumnProperty, col);

                    // baseChar = baseChar + 1;
                    //wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };

                    //// PosX  = Column no, PosY = Row No, array will be accessed as Array[Row][Column].
                    wp.WellPosition = new WellPosition() { PositionX = col, PositionY = row };

                    //if (PositionContainmentType == PositionContainment.AnyKind)
                    //{
                    //    wp.AnyKindShapeFillColor = Colors.Tomato;
                    //}

                    //add the control to list for reference
                    children[row - 1][col - 1] = wp;

                    if (IsWellControl == true)
                    {
                        grid1.Children.Add(wp);
                    }
                }
            }

            // c1.ItemsSource = children;
            if (IsWellInputPlate == false)
            {
                AppyOutputPlateItemsOnGrid();
            }
            else
            {
                AppyInputPlateItemsOnGrid();
            }
            return grid1;
        }

        private void wp_MouseMove(object sender, MouseEventArgs e)
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

        public void wp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            SelectOnDrag();
        }

        private void wp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            isDown = true;
            CurrentSelectedWellControl = (WellPositionControl)sender;
            // Clear all selection if ControlKey is NOT press
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

            // Find the block created by pressing shift key and set IsSelected = true for all the control in that block.
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
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
            CurrentSelectedWellControl.IsSelected = true;
            if (CurrentSelectedWellControl != null)
            {
                LastSelectedWellControl = CurrentSelectedWellControl;
            }
        }

        //public void SelectOnDrag(WellPositionControl well)
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

        public void AppyOutputPlateItemsOnGrid()
        {
            int row = 0;
            int col = 0;
            if (AssignmentRect == null)
                return;

            for (int pos = 0; pos < AssignmentRect.OutputPlateItemLists.Count; pos++)
            {
                WellPositionControl test = (WellPositionControl)children[row][col];
                //test.Legend = PositionContainment.Standard;
                test.IsInputPlate = false;
                test.Legend = AssignmentRect.OutputPlateItemLists[pos].OrderType;
                test.Targets = AssignmentRect.OutputPlateItemLists[pos];
                row = row + 1;
                if (row > RowShapes - 1)
                {
                    col = col + 1;
                    row = 0;
                }
            }
        }

        public void AppyInputPlateItemsOnGrid()
        {
            int row = 0;
            int col = 0;
            if (AssignmentRect == null)
                return;

            for (int pos = 0; pos < AssignmentRect.OutputPlateItemLists.Count; pos++)
            {
                WellPositionControl test = (WellPositionControl)children[row][col];
                test.IsInputPlate = true;
                test.Legend = AssignmentRect.OutputPlateItemLists[pos].OrderType;
                //test.IsInputPlate = false;
                //test.Legend = PositionContainment.AnyKind;
                row = row + 1;
                if (row > RowShapes - 1)
                {
                    col = col + 1;
                    row = 0;
                }
            }
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

        ///// <summary>
        ///// Fills the wells in horizontal or vertical first order. default is filled vertically ie column by column
        ///// </summary>
        //public static readonly DependencyProperty IsFillHorizontalFirstProperty = DependencyProperty.Register("IsFillHorizontalFirst", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));

        //public bool IsFillHorizontalFirst
        //{
        //    get
        //    {
        //        return (bool)GetValue(IsFillHorizontalFirstProperty);
        //    }
        //    set
        //    {
        //        SetValue(IsFillHorizontalFirstProperty, value);
        //    }
        //}

        /// <summary>
        /// Dependency property to key to a individual control to be identified uniquely
        /// </summary>
        public static readonly DependencyProperty PositionContainmentTypeProperty = DependencyProperty.Register("PositionContainmentType", typeof(PositionContainment), typeof(WellPositionControl), new FrameworkPropertyMetadata(PositionContainment.Empty));

        public PositionContainment PositionContainmentType
        {
            get
            {
                return (PositionContainment)GetValue(PositionContainmentTypeProperty);
            }
            set
            {
                SetValue(PositionContainmentTypeProperty, value);
            }
        }

        /// <summary>
        /// Dependency property to specify Width of Rectangle where wellposition control is to be drawn
        /// </summary>
        public static readonly DependencyProperty RectangleWidthProperty = DependencyProperty.Register("RectangleWidth", typeof(double), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRectangleWidth));

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
        /// Dependency property to specify height of Rectangle where wellposition control is to be drawn
        /// </summary>
        public static readonly DependencyProperty RectangleHeightProperty = DependencyProperty.Register("RectangleHeight", typeof(double), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRectangleHeight));

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
        /// Dependency property to specify spacing between column headers and well controls.
        /// </summary>
        public static readonly DependencyProperty HeaderSpacingLeftProperty = DependencyProperty.Register("HeaderSpacingLeft", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultHeaderSpacingLeft));

        /// <summary>
        /// Gets or sets spacing between column headers and well controls.
        /// </summary>
        /// <value>
        /// spacing between column header and well controls in int format.
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
        /// Dependency property to specify spacing between row headers and well controls.
        /// </summary>
        public static readonly DependencyProperty HeaderSpacingTopProperty = DependencyProperty.Register("HeaderSpacingTop", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultHeaderSpacingTop));

        /// <summary>
        /// Gets or sets spacing between row headers and well controls.
        /// </summary>
        /// <value>
        /// spacing between row header and well controls in int format.
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
        /// Dependency property to show visible border to rectangle control containing well controls.
        /// </summary>
        public static readonly DependencyProperty IsBorderVisibleProperty = DependencyProperty.Register("IsBorderVisible", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));

        /// <summary>
        /// Gets or sets value if to show border to the rectangle control
        /// </summary>
        /// <value>
        /// boolean flag mentioning border visibility
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
        ///  temporary proeprty just to see control generation order.
        /// </summary>
        public static readonly DependencyProperty IsWellControlProperty = DependencyProperty.Register("IsWellControl", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// Gets or sets value if to show border to the rectangle control
        /// </summary>
        /// <value>
        /// boolean flag mentioning border visibility
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
        /// Dependency property to show size of outer selection circle when the control is clicked.
        /// </summary>
        public static readonly DependencyProperty RectWellSelectionSizeProperty = DependencyProperty.Register("RectWellSelectionSize", typeof(int), typeof(WellPositionControl), new FrameworkPropertyMetadata(DefaultRotorWellSelectionSize));

        /// <summary>
        /// Gets or sets value of outer selection circle when well is clicked.
        /// </summary>
        /// <value>
        /// size of circle when clicked in integer.
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
        /// Dependency property to bind targets on the control.
        /// </summary>
        public static readonly DependencyProperty AssignmentRectProperty = DependencyProperty.Register("AssignmentRect", typeof(Assignment), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackAssignment)) { BindsTwoWayByDefault = true });

        public Assignment AssignmentRect
        {
            get
            {
                return (Assignment)GetValue(AssignmentRectProperty);
            }
            set
            {
                SetValue(AssignmentRectProperty, value);
                // CreateGrid();
            }
        }

        private static void CallbackAssignment(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.AssignmentRect = (Assignment)args.NewValue;
        }

        /// <summary>
        /// Dependency property to indicate no of wells in each row. default rows is 8
        /// </summary>
        public static readonly DependencyProperty RowShapesProperty = DependencyProperty.Register("RowShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRowShapes, new PropertyChangedCallback(CallbackRowshapes)) { BindsTwoWayByDefault = true });

        //public static readonly DependencyProperty RowShapesProperty = DependencyProperty.Register("RowShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(8, CallbackRowshapes));

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

        private static void CallbackRowshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.RowShapes = (int)args.NewValue;
        }

        //private static void CallbackHandler(DependencyObject property, DependencyPropertyChangedEventArgs args)
        //{
        //    RectanglePlateControl rect = (RectanglePlateControl)property;
        //    if (args.Property.Name == "RowShapes")
        //    {
        //        rect.RowShapes = (int)args.NewValue;
        //    }
        //    else if (args.Property.Name == "ColShapes")
        //    {
        //        rect.ColShapes = (int)args.NewValue;
        //    }
        //    else if (args.Property.Name == "AssignmentRect")
        //    {
        //        //  rect.AssignmentRect = (Assignment)args.NewValue;
        //    }
        //    rect.CreateGrid();
        //    //_readout.Readout.Content = _readout.MinutesRemaining;
        //}

        /// <summary>
        /// Dependency Property to indicate no of wells in each Column. default is 12
        /// </summary>
        //public static readonly DependencyProperty ColShapesProperty = DependencyProperty.Register("ColShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(12,CallbackColshapes));
        public static readonly DependencyProperty ColShapesProperty = DependencyProperty.Register("ColShapes", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultColShapes, new PropertyChangedCallback(CallbackColshapes)) { BindsTwoWayByDefault = true });

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

        private static void CallbackColshapes(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.ColShapes = (int)args.NewValue;
        }

        /// <summary>
        /// Dependency property to specify size of well control in diameter.
        /// </summary>
        public static readonly DependencyProperty DiameterEachShapeProperty = DependencyProperty.Register("DiameterEachShape", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultDiameterEachShape, new PropertyChangedCallback(CallbackDiameter)) { BindsTwoWayByDefault = true });

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
                //CreateGrid();
            }
        }

        private static void CallbackDiameter(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.DiameterEachShape = (int)args.NewValue;
        }

        /// <summary>
        /// Dependency property to specify spacing between two rows of well controls.
        /// </summary>
        public static readonly DependencyProperty RowSpacingProperty = DependencyProperty.Register("RowSpacing", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultRowSpacing, new PropertyChangedCallback(CallbackRowspacing)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Gets or sets spacing between adjacent rows.
        /// </summary>
        /// <value>
        /// spacing between adjacent rows in int format.
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

        private static void CallbackRowspacing(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.RowSpacing = (int)args.NewValue;
        }

        /// <summary>
        /// Dependency property to specify spacing between adjacent columns of well controls.
        /// </summary>
        public static readonly DependencyProperty ColSpacingProperty = DependencyProperty.Register("ColSpacing", typeof(int), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(DefaultColSpacing, new PropertyChangedCallback(CallbackColspacing)) { BindsTwoWayByDefault = true });

        /// <summary>
        /// Gets or sets spacing between adjacent columns.
        /// </summary>
        /// <value>
        /// spacing between adjacent columns in int format.
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

        private static void CallbackColspacing(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.ColSpacing = (int)args.NewValue;
        }

        /// <summary>
        /// Dependency property to identify, if the shape to be created is of input or output plate control
        /// </summary>
        public static readonly DependencyProperty IsWellInputPlateProperty = DependencyProperty.Register("IsWellInputPlate", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false));//, new PropertyChangedCallback(CallbackIsWellInputPlate)){ BindsTwoWayByDefault = true });

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

        private static void CallbackIsWellInputPlate(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.IsWellInputPlate = (bool)args.NewValue;
        }

        public static readonly DependencyProperty IsReadonlyProperty = DependencyProperty.Register("IsReadonly", typeof(bool), typeof(RectanglePlateControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(CallbackIsReadonly)) { BindsTwoWayByDefault = true });

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
            RectanglePlateControl rect = (RectanglePlateControl)property;
            rect.IsReadonly = (bool)args.NewValue;
        }
    }
}