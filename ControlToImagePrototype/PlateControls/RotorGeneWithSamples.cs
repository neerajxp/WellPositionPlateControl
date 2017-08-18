// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates circular plate control for input plate type and does not requires info about x and y positions.
    /// Control is generated based on wellscount and wellsrestartat parameter.
    /// </summary>
    public class RotorGeneWithSamples : RotorGenePlateControl
    {
        #region Member Variables

        private const string PartCanvas = "PART_ROTORGENE_CANVAS";
        private const string PartScrollviewer = "PART_ROTORGENE_SCROLLVIEWER";

        /// <summary>
        /// Dependency property to bind samples on the control.
        /// </summary>
        public static readonly DependencyProperty WellInputAssignmentProperty = DependencyProperty.Register("WellInputAssignment", typeof(WellControlSummaryTO), typeof(RotorGeneWithSamples), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackWellInputAssignment)));

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="RotorGeneWithSamples"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Required for the project")]
        static RotorGeneWithSamples()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RotorGeneWithSamples), new FrameworkPropertyMetadata(typeof(RotorGeneWithSamples)));
        }

        #endregion Construction

        #region Control Overrides

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

        private void SetRestartWellsCounter()
        {
            if (WellInputAssignment.WellControlSummary.Count > 0)
            {
                string temp = WellInputAssignment.WellControlSummary[0].WellPosition.Label.Substring(0, 1);
                for (int pos = 0; pos < WellInputAssignment.WellControlSummary.Count; pos++)
                {
                    string strCurItem = WellInputAssignment.WellControlSummary[pos].WellPosition.Label.Substring(0, 1);
                    if (strCurItem != temp)
                    {
                        RestartWellsAt = pos;
                        return;
                    }
                }
            }
        }

        private void ApplyInputPlateItemsOnGrid()
        {
            base.OnApplyTemplate();

            if (WellInputAssignment != null)
            {
                SetRestartWellsCounter();
                InputWellPositions = new List<string>();
                InputWellPositions.AddRange(WellInputAssignment.WellControlSummary.Select(x => x.WellPosition.Label).ToList());
                for (int pos = 0; pos < WellInputAssignment.WellControlSummary.Count; pos++)
                {
                    string strCurItem = WellInputAssignment.WellControlSummary[pos].WellPosition.Label;
                    WellPositionControl test = GetWellPositionControl(strCurItem);
                    if (test != null)
                    {
                        SampleTO sample = WellInputAssignment.WellControlSummary[pos].Sample;
                        if (sample != null)
                        {
                            string tooltipSampleName = string.Empty;
                            if (string.IsNullOrEmpty(sample.Name) == false)
                            {
                                tooltipSampleName = sample.Name + ", ";
                            }

                            test.WellToolTip = string.Concat(tooltipSampleName, strCurItem, ", Conc:", sample.ConcentrationWithUnit, ", Desc:", sample.Description);
                        }

                        test.IsInputPlate = true;
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
                            else
                            {
                                test.Legend = WellInputAssignment.WellControlSummary[pos].Legend;
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
            WellPositionControl test = new WellPositionControl();
            for (int pos = 0; pos < WellsCount; pos++)
            {
                test = (WellPositionControl)Children[pos];
                if (test != null)
                {
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
            RotorGeneWithSamples rect = (RotorGeneWithSamples)property;
            rect.WellInputAssignment = (WellControlSummaryTO)args.NewValue;
        }

        #endregion Methods
    }
}