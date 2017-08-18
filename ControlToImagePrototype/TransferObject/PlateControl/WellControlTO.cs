// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using QIAgility.Common.ObjectModel.Worktable;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Providing data to WellTargetTO.
    /// </summary>
    public class WellControlTO
    {
        #region Member Variables

        private bool isAssigned;
        private bool isSelected;
        private bool isAutoCalibrated;
        private double wellDiameter;
        private Position wellPosition;
        private OrderType legend;

        private SampleTO sample;
        private string name;

        private string welltoolTip;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WellControlTO" /> class.
        /// </summary>
        public WellControlTO()
        {
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether well is assigned or not not.
        /// </summary>
        /// <value>Boolean flag.</value>
        public bool IsAssigned
        {
            get
            {
                return isAssigned;
            }

            set
            {
                if (isAssigned != value)
                {
                    isAssigned = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well is currently selected.
        /// </summary>
        /// <value>Boolean flag.</value>
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }

            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether well is autocalibrated.
        /// </summary>
        /// <value>Boolean flag.</value>
        public bool IsAutoCalibrated
        {
            get
            {
                return isAutoCalibrated;
            }

            set
            {
                if (isAutoCalibrated != value)
                {
                    isAutoCalibrated = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the diameter of well.
        /// </summary>
        /// <value>Diameter in double format.</value>
        public double WellDiameter
        {
            get
            {
                return wellDiameter;
            }

            set
            {
                if (wellDiameter != value)
                {
                    wellDiameter = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the sample.
        /// </summary>
        /// <value>Sample Information.</value>
        public SampleTO Sample
        {
            get
            {
                return sample;
            }

            set
            {
                if (sample != value)
                {
                    sample = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the order type of well.
        /// </summary>
        /// <value>Order Type.</value>
        public OrderType Legend
        {
            get
            {
                return legend;
            }

            set
            {
                if (legend != value)
                {
                    legend = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the wellposition of each well in row and column.
        /// </summary>
        /// <value>WellPosition in row and col.</value>
        public Position WellPosition
        {
            get
            {
                return wellPosition;
            }

            set
            {
                if (wellPosition != value)
                {
                    wellPosition = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the Name of current sample.
        /// </summary>
        /// <value>Sample Name.</value>
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                if (name != value)
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value which will be shown on mouse tool tip of wells.
        /// </summary>
        /// <value>Tool tip value.</value>
        public string WellToolTip
        {
            get
            {
                return welltoolTip;
            }

            set
            {
                if (welltoolTip != value)
                {
                    welltoolTip = value;
                }
            }
        }

        #endregion Properties
    }
}