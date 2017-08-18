// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Windows;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates plate control of input plate type.
    /// </summary>
    public class PlateControlWithSamples : RectanglePlateControl
    {
        #region Member Variables

        /// <summary>
        /// Dependency property to bind targets on the control.
        /// </summary>
        public static readonly DependencyProperty WellInputAssignmentProperty = DependencyProperty.Register("WellInputAssignment", typeof(WellControlSummaryTO), typeof(PlateControlWithSamples), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackWellInputAssignment)));

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="PlateControlWithSamples"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "RV: This has to be done in constructor.")]
        static PlateControlWithSamples()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PlateControlWithSamples), new FrameworkPropertyMetadata(typeof(PlateControlWithSamples)));
        }

        #endregion Construction

        #region Control Overrides

        /// <summary>
        /// Called when the Template is applied to the control. Template is defined in Style file.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ApplyInputPlateItemsOnGrid();
        }

        #endregion Control Overrides

        #region Properties

        /// <summary>
        /// Gets or sets the assignment data for input plate.
        /// </summary>
        /// <value>
        /// Object of WellControlSummaryTO.
        /// </value>
        public WellControlSummaryTO WellInputAssignment
        {
            get
            {
                return (WellControlSummaryTO)GetValue(WellInputAssignmentProperty);
            }

            set
            {
                SetValue(WellInputAssignmentProperty, value);
                if (Children != null)
                {
                    ApplyInputPlateItemsOnGrid();
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Gets or sets the input plate assignment data for input plate.
        /// </summary>
        /// <value>
        /// Object of WellControlSummaryTO.
        /// </value>
        public void ApplyInputPlateItemsOnGrid()
        {
            base.OnApplyTemplate();

            if (WellInputAssignment != null)
            {
                for (int pos = 0; pos < WellInputAssignment.WellControlSummary.Count; pos++)
                {
                    string strCurItem = WellInputAssignment.WellControlSummary[pos].WellPosition.Label.Replace(":", string.Empty);
                    WellPositionControl test = GetWellPositionControl(strCurItem);
                    if (test != null)
                    {
                        SampleTO sample = WellInputAssignment.WellControlSummary[pos].Sample;
                        if (sample != null)
                        {
                            test.WellToolTip = string.Concat(sample.Name, ", ", strCurItem, ", Conc:", sample.ConcentrationWithUnit, ", Desc:", sample.Description);
                        }

                        test.IsInputPlate = true;
                        test.Legend = OrderType.Empty;
                        if (WellInputAssignment.WellControlSummary[pos].IsAssigned == true)
                        {
                            test.IsInputAssigned = true;
                            test.Legend = OrderType.AnyKind;
                            test.IsSelected = false;
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(sample.Name) == false)
                            {
                                test.Legend = OrderType.InputSample;
                            }
                        }

                        if (WellInputAssignment.WellControlSummary[pos].IsSelected == true)
                        {
                            test.IsSelected = true;
                        }
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

        private static void CallbackWellInputAssignment(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            PlateControlWithSamples rect = (PlateControlWithSamples)property;
            rect.WellInputAssignment = (WellControlSummaryTO)args.NewValue;
        }

        #endregion Methods
    }
}