// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using QIAgility.CoreApp.Controls.TransferObject.PlateControl;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Creates circular plate control for output plate type and does not requires info about x and y positions.
    /// Control is generated based on wellscount and wellsrestartat parameter.
    /// </summary>
    public class RotorGeneWithTargets : RotorGenePlateControl
    {
        #region Member Variables

        private const string PartCanvas = "PART_ROTORGENE_CANVAS";
        private const string PartScrollviewer = "PART_ROTORGENE_SCROLLVIEWER";

        /// <summary>
        /// Dependency property to bind targets on the control.
        /// </summary>
        public static readonly DependencyProperty WellOutputAssignmentProperty = DependencyProperty.Register("WellOutputAssignment", typeof(WellTargetSummaryTO), typeof(RotorGeneWithTargets), new FrameworkPropertyMetadata(new PropertyChangedCallback(CallbackWellOutputAssignment)));

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes static members of the <see cref="RotorGeneWithTargets"/> class.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Required for the project")]
        static RotorGeneWithTargets()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RotorGeneWithTargets), new FrameworkPropertyMetadata(typeof(RotorGeneWithTargets)));
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

        /// <summary>
        /// Gets or sets the assignment data for output plate.
        /// </summary>
        /// <value>
        /// Object of WellTargetSummaryTO.
        /// </value>
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

        private void SetRestartWellsCounter()
        {
            if (WellOutputAssignment.WellTargetSummary.Count > 0)
            {
                string temp = WellOutputAssignment.WellTargetSummary[0].WellPosition.Label.Substring(0, 1);
                for (int pos = 0; pos < WellOutputAssignment.WellTargetSummary.Count; pos++)
                {
                    string strCurItem = WellOutputAssignment.WellTargetSummary[pos].WellPosition.Label.Substring(0, 1);
                    if (strCurItem != temp)
                    {
                        RestartWellsAt = pos;
                        return;
                    }
                }
            }
        }

        private void ApplyOutputPlateItemsOnGrid()
        {
            base.OnApplyTemplate();

            if (WellOutputAssignment != null)
            {
                InputWellPositions = new List<string>();
                InputWellPositions.AddRange(WellOutputAssignment.WellTargetSummary.Select(x => x.WellPosition.Label).ToList());
                SetRestartWellsCounter();

                for (int pos = 0; pos < WellOutputAssignment.WellTargetSummary.Count; pos++)
                {
                    string strCurItem = WellOutputAssignment.WellTargetSummary[pos].WellPosition.Label;
                    WellPositionControl test = GetWellPositionControl(strCurItem);
                    if (test != null)
                    {
                        test.Targets = WellOutputAssignment.WellTargetSummary[pos];
                        test.IsInputPlate = false;
                        test.Legend = WellOutputAssignment.WellTargetSummary[pos].Legend;
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

        private static void CallbackWellOutputAssignment(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            RotorGeneWithTargets rect = (RotorGeneWithTargets)property;
            rect.WellOutputAssignment = (WellTargetSummaryTO)args.NewValue;
        }

        #endregion Methods
    }
}