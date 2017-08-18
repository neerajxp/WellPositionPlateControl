// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.Generic;
using QIAgility.Common.ObjectModel.Worktable;
using QIAgility.CoreApp.Controls.ObjectModel.PlateControl;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Providing data to WellTargetTO.
    /// </summary>
    public class WellTargetTO
    {
        #region Member Variables

        private Position wellPosition;
        private OrderType legend;
        private bool isSelected;
        private double wellDiameter;
        private List<TargetTO> targetList;
        private string welltoolTip;
        private int? originColumn;
        private bool isAssigned;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WellTargetTO" /> class.
        /// </summary>
        public WellTargetTO()
        {
            targetList = new List<TargetTO>();
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

        ///// <summary>
        ///// Gets the target list.
        ///// </summary>
        ///// <value>
        ///// The target list.
        ///// </value>
        //public List<TargetTO> TargetList
        //{
        //    get
        //    {
        //        return targetList;
        //    }
        //}

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
        /// Gets or sets the diameter of well.
        /// </summary>
        /// <value>Well diameter in double format.</value>
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

        /// <summary>
        /// Gets or sets the column number of originating sample.
        /// </summary>
        /// <value>Column number.</value>
        public int? OriginColumn
        {
            get
            {
                return originColumn;
            }

            set
            {
                if (originColumn != value)
                {
                    originColumn = value;
                }
            }
        }

        #endregion Properties
    }
}