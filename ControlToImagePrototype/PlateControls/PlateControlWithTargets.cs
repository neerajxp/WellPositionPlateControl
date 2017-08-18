// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Windows;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates plate control of output plate type.
    /// </summary>
    public class PlateControlWithTargets : RectanglePlateControl
    {
        #region Member Variables

        /// <summary>
        /// Dependency property to bind targets on the control.
        /// </summary>
        public static readonly DependencyProperty WellOutputAssignmentProperty = DependencyProperty.Register("WellOutputAssignment", typeof(WellTargetSummaryTO), typeof(PlateControlWithTargets), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackWellOutputAssignment)));

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="PlateControlWithTargets"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "RV: This has to be done in constructor.")]
        static PlateControlWithTargets()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlateControlWithTargets), new FrameworkPropertyMetadata(typeof(PlateControlWithTargets)));
        }

        #endregion Construction

        #region Control Overrides

        /// <summary>
        /// Called when the Template is applied to the control. Template is defined in Style file.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyOutputPlateItemsOnGrid();
        }

        #endregion Control Overrides

        #region Properties

        public WellTargetSummaryTO WellOutputAssignment
        {
            get
            {
                return (WellTargetSummaryTO)GetValue(WellOutputAssignmentProperty);
            }

            set
            {
                SetValue(WellOutputAssignmentProperty, value);
                if (Children != null)
                {
                    ApplyOutputPlateItemsOnGrid();
                }
            }
        }

        #endregion Properties

        #region Methods

        public void ApplyOutputPlateItemsOnGrid()
        {
            base.OnApplyTemplate();

            if (WellOutputAssignment != null)
            {
                for (int pos = 0; pos < WellOutputAssignment.WellTargetSummary.Count; pos++)
                {
                    string strCurItem = WellOutputAssignment.WellTargetSummary[pos].WellPosition.Label.Replace(":", string.Empty);
                    WellPositionControl test = GetWellPositionControl(strCurItem);
                    if (test != null)
                    {
                        test.Targets = WellOutputAssignment.WellTargetSummary[pos];
                        test.IsInputPlate = false;
                        test.Legend = WellOutputAssignment.WellTargetSummary[pos].Legend;
                        test.IsSelected = WellOutputAssignment.WellTargetSummary[pos].IsSelected;
                    }
                }
            }
        }

        private WellPositionControl GetWellPositionControl(string wellName)
        {
            int row = 0;
            int col = 0;
            int rowCount = RowShapes;
            int colCount = ColShapes;
            WellPositionControl test = new WellPositionControl();
            for (row = 0; row < rowCount; row++)
            {
                for (col = 0; col < colCount; col++)
                {
                    test = (WellPositionControl)Children[row][col];
                    if (test.WellName == wellName)
                    {
                        return test;
                    }
                }
            }

            return null;
        }

        private static void CallbackWellOutputAssignment(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            PlateControlWithTargets rect = (PlateControlWithTargets)property;
            rect.WellOutputAssignment = (WellTargetSummaryTO)args.NewValue;
        }

        #endregion Methods
    }
}