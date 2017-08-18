using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using QIAgility.Common.ObjectModel.Worktable;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace ControlToImagePrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            inputSampleList = new WellControlSummaryTO();
        }

        private WellControlSummaryTO inputSampleList;

        public WellControlSummaryTO InputSampleList
        {
            get
            {
                return inputSampleList;
            }

            set
            {
                if (value != null)
                {
                    inputSampleList = value;
                }
            }
        }

        public void ConvertInputData()
        {
            inputSampleList = new WellControlSummaryTO();
            inputSampleList.WellControlSummary.Clear();
            for (int i = 0; i < 10; i++)
            {
                WellControlTO wellControl = new WellControlTO();

                wellControl.WellPosition = new Position() { Label = "A1", Row = 1, Column = 1, PosX = 1, PosY = 1 };
                wellControl.IsAssigned = true;
                wellControl.IsSelected = false;
                wellControl.Legend = OrderType.Empty;
                wellControl.Sample = new SampleTO();
                wellControl.WellDiameter = 10;
                inputSampleList.WellControlSummary.Add(wellControl);
            }

            OnPropertyChanged("InputSampleList");
        }

        private void btnCreateRectangle_Click(object sender, RoutedEventArgs e)
        {
            ConvertInputData();
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}