// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.Generic;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Creates object of WellTipRackTO to be used to create a well on TipRack plate.
    /// </summary>
    public class WellTipRackTO
    {
        #region Member Variables

        private bool isAvailable;
        private bool isSelected;

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WellTipRackTO"/> class.
        /// </summary>
        public WellTipRackTO()
        {
        }

        #endregion Construction

        #region Properties

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

        #endregion Properties
    }
}