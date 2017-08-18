using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

//using PieControl;
namespace PlateControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private IList<Target> data1;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            data1 = new List<Target>();
            for (int i = 0; i < slider1.Value; i++)
            {
                data1.Add(new Target { Color = System.Windows.Media.Color.FromRgb(10, 10, 10) });
            }

            canvas1.Children.Clear();
            x1 = 25;
            y1 = 5;

            List<bool> ctrltype = new List<bool>() { true, false };

            foreach (bool t in ctrltype)
            {
                testInputControl = t;
                for (int i = 0; i < comboBox1.Items.Count; i++)
                {
                    comboBox1.SelectedIndex = i;
                    //CreateSampleShapes();
                    iterator = iterator + 1;
                    x1 = x1 + 120;
                    if (iterator > 3)
                    {
                        y1 = y1 + 120;
                        x1 = 25;
                        iterator = 0;
                    }
                }
            }
        }

        private int x1, y1;
        private int iterator;
        private bool testInputControl;

        //public void CreateSampleShapes()
        //{
        //    data1 = new List<Target>();
        //    for (int i = 0; i < slider1.Value; i++)
        //    {
        //        data1.Add(new Target { Id = 1, Color = System.Drawing.Color.Red, Description = "test1" });
        //    }

        //    WellPositionControl wp = new WellPositionControl();
        //    ResourceDictionary myResources = new ResourceDictionary();
        //    myResources.Source = new Uri(@"\Themes\Generic.xaml", UriKind.RelativeOrAbsolute);

        //    Style testStyle = myResources["WellPositionStyle"] as Style;
        //    wp.Style = testStyle;
        //    wp.Legend = (PositionContantment)Enum.Parse(typeof(PositionContantment), comboBox1.Text);
        //    wp.IsInputControl = testInputControl;
        //    wp.SelectedHighlightColor = Colors.Cyan;
        //    if (radioButton2.IsChecked == true)
        //    {
        //        wp.IsInputControl = false;
        //        wp.SelectedHighlightColor = Colors.LightGreen;
        //    }

        //    wp.CanvasHeight = double.Parse(lblHeight.Content.ToString());
        //    wp.CanvasWidth = double.Parse(lblWidth.Content.ToString());
        //    wp.ApplyTemplate();
        //    wp.Targets = data1;
        //    wp.IsSelected = false;
        //    if (chkIsSelected.IsChecked == true)
        //    {
        //        wp.IsSelected = true;
        //    }

        //    wp.IsDrawTargetLegend = false;
        //    if (chkDrawLegend.IsChecked == true)
        //    {
        //        wp.IsDrawTargetLegend = true;
        //    }
        //    wp.IsFilledLegend = false;
        //    if (chkFilled.IsChecked == true)
        //    {
        //        wp.IsFilledLegend = true;
        //    }

        //    wp.GenerateShape();

        //    //            canvas1.Children.Clear();
        //    wp.Margin = new Thickness(x1, y1, 0, 0);
        //    canvas1.Children.Add(wp);
        //}

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CreateSampleShapeSingle();
        }

        public WellPositionControl wp;

        public void CreateSampleShapeSingle()
        {
            data1 = new List<Target>();
            for (int i = 0; i < slider1.Value; i++)
            {
                data1.Add(new Target { Id = 1, Color = Colors.Red, Description = "test1" });
            }

            canvas1.Children.Clear();

            wp = new WellPositionControl();
            ResourceDictionary myResources = new ResourceDictionary();
            myResources.Source = new Uri(@"\Themes\Generic.xaml", UriKind.RelativeOrAbsolute);

            Style testStyle = myResources["WellPositionStyle"] as Style;
            wp.Style = testStyle;
            wp.Legend = (PositionContainment)Enum.Parse(typeof(PositionContainment), comboBox1.Text);
            ////wp.IsInputControl = true;
            wp.SelectedHighlightColor = Colors.Cyan;
            if (radioButton2.IsChecked == true)
            {
                ////wp.IsInputControl = false;
                wp.SelectedHighlightColor = Colors.LightGreen;
            }

            wp.CanvasHeight = double.Parse(lblHeight.Content.ToString());
            wp.CanvasWidth = double.Parse(lblWidth.Content.ToString());
            wp.ApplyTemplate();
            ////wp.Targets = data1;
            wp.IsSelected = false;
            if (chkIsSelected.IsChecked == true)
            {
                wp.IsSelected = true;
            }
            wp.IsDrawTargetLegend = false;
            if (chkDrawLegend.IsChecked == true)
            {
                wp.IsDrawTargetLegend = true;
            }
            wp.IsFilledLegend = false;
            if (chkFilled.IsChecked == true)
            {
                wp.IsFilledLegend = true;
            }
            wp.OnApplyTemplate();
            wp.Margin = new Thickness(50, 50, 0, 0);
            canvas1.Children.Add(wp);
        }

        //private void button3_Click(object sender, RoutedEventArgs e)
        //{
        //    RectanglePlateControl r1 = new RectanglePlateControl();

        //    ResourceDictionary myResources1 = new ResourceDictionary();
        //    myResources1.Source = new Uri(@"\Themes\RectanglePlateControlStyle.xaml", UriKind.RelativeOrAbsolute);
        //    Style testStyle1 = myResources1["RectanglePlateControl"] as Style;
        //    //Style testStyle1 = FindResource("RectanglePlateControl") as Style;
        //    r1.Style = testStyle1;
        //    r1.ApplyTemplate();
        //    r1.CreateGrid();
        //    canvas1.Children.Add(r1);

        //    //start
        //    IList<Target> data1 = new List<Target>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        data1.Add(new Target { Id = 1, Color = System.Drawing.Color.Red, Description = "test1" });
        //    }
        //    WellPositionControl wp = new WellPositionControl();
        //    ResourceDictionary myResources = new ResourceDictionary();
        //    myResources.Source = new Uri(@"\Themes\WellPositionControlStyle.xaml", UriKind.RelativeOrAbsolute);
        //    //Style testStyle = FindResource("WellPositionStyle") as Style;
        //    Style testStyle = myResources["WellPositionStyle"] as Style;
        //    wp.Style = testStyle;
        //    wp.Legend = PositionContantment.Controls;
        //    wp.IsInputControl = false;
        //    wp.VerticalAlignment = System.Windows.VerticalAlignment.Top;
        //    wp.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
        //    wp.SelectedHighlightColor = Colors.Cyan;
        //    wp.SelectedHighlightColor = Colors.LightGreen;
        //    wp.CanvasHeight = 50;
        //    wp.CanvasWidth = 50;
        //    wp.ApplyTemplate();
        //    wp.Targets = data1;
        //    wp.IsSelected = false;
        //    wp.IsDrawTargetLegend = true;
        //    wp.IsFilledLegend = false;
        //    wp.GenerateShape();
        //    canvas1.Children.Add(wp);
        //}
    }
}