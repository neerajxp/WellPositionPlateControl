// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Windows.Media;
using QIAgility.Common.ObjectModel.Worktable;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Create object of WellMixPlateTO to be used in mix plate.
    /// </summary>
    public class WellMixPlateTO
    {
        #region Member Variables

        private bool isAvailable;
        private bool isSelected;
        private bool isAutoCalibrated;
        private double wellDiameter;
        private string label;
        private Position wellPosition;
        private OrderType legend;

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WellMixPlateTO"/> class.
        /// </summary>
        public WellMixPlateTO()
        {
        }

        #endregion Construction

        #region Properties

        /// <summary>
        /// Gets or sets the Tube.
        /// </summary>
        /// <value>
        /// The current Tube.
        /// </value>
        public string Label
        {
            get
            {
                return label;
            }

            set
            {
                if (label != value)
                {
                    label = value;
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
        /// Gets or sets the well position of each well in row and column.
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
        /// Gets or sets a value indicating whether well is currently selected.
        /// </summary>
        /// <value>Boolean flag.</value>
        public bool IsAvailable
        {
            get
            {
                return isAvailable;
            }

            set
            {
                if (isAvailable != value)
                {
                    isAvailable = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating diameter of well.
        /// </summary>
        /// <value>Double value for diameter.</value>
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

        #endregion Properties
    }
}