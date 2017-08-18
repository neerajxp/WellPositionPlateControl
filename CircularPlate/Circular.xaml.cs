using System;
using System.Windows;
using PlateControl;
using RotorPlateControl;

namespace CircularPlate
{
    /// <summary>
    /// Interaction logic for Circular.xaml
    /// </summary>
    public partial class Circular : Window
    {
        private RotorViewModel testVM1;
        private RotorAssignment Assign1;

        public Circular()
        {
            InitializeComponent();

            Assign1 = new RotorAssignment();
            Random rand = new Random(255);
            byte[] colorBytes = new byte[3];
            //System.Windows.Media.Color randomColor;
            GenerateDummyOutputData(12);

            testVM1 = new RotorViewModel();
            testVM1.WellPaddingVM = 8;
            //testVM1.RowShapesVM = 8;
            //testVM1.ColShapesVM = 12;
            //testVM1.IsInputPlateVM = true;

            //testVM1.DiameterEachShapeVM = 25;
            //testVM1.RowSpacingVM = 5;
            //testVM1.ColSpacingVM = 5;
            testVM1.DiameterofWellVM = 17;
            testVM1.IsInputPlateVM = true;
            testVM1.AssignmentRotorVM = Assign1;
            // testVM1.RowShapesRotorVM = 8;
            this.DataContext = testVM1;
        }

        public void GenerateDummyOutputData(int count)
        {
            Assign1.RotorPlateItemLists.Clear();

            PlateItem p1 = new PlateItem();
            p1.OrderType = PlateControl.PositionContainment.StandardsOnly;
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            Assign1.RotorPlateItemLists.Add(p1);

            p1 = new PlateItem();
            p1.OrderType = PlateControl.PositionContainment.ControlsOnly;
            p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
            p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.05, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
            Assign1.RotorPlateItemLists.Add(p1);

            for (int i = 0; i < count; i++)
            {
                p1 = new PlateItem();
                p1.OrderType = PlateControl.PositionContainment.SamplesOnly;
                p1.OutputPlateTargets.Add(new Target { Name = "target1", TargetType = TargetType.IC, Concentration = 1.25, Color = System.Windows.Media.Color.FromRgb(224, 0, 60) });
                p1.OutputPlateTargets.Add(new Target { Name = "target2", TargetType = TargetType.IC, Concentration = 0.35, Color = System.Windows.Media.Color.FromRgb(15, 168, 1) });
                p1.OutputPlateTargets.Add(new Target { Name = "target3", TargetType = TargetType.IC, Concentration = 1.05, Color = System.Windows.Media.Color.FromRgb(0, 0, 255) });
                p1.OutputPlateTargets.Add(new Target { Name = "target4", TargetType = TargetType.IC, Concentration = 0.25, Color = System.Windows.Media.Color.FromRgb(255, 140, 0) });
                Assign1.RotorPlateItemLists.Add(p1);
            }

            //  OnPropertyChanged("RotorPlateItemLists");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GenerateDummyOutputData(5);
            testVM1.IsInputPlateVM = false;
            testVM1.WellPaddingVM = 8;
            testVM1.AssignmentRotorVM = Assign1;

            //testVM1.DiameterEachShapeVM = 17;
            //testVM1.RowSpacingVM = 11;
            //testVM1.ColSpacingVM = 11;

            testVM1.DiameterofWellVM = 20;
            testVM1.RowShapesRotorVM = 8;
            //testVM1.ColShapesVM = 12;
        }
    }
}