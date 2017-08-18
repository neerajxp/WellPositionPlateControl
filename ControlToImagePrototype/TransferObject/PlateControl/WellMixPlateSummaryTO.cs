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
    /// Creates object of WellMixPlateSummaryTO to be used for data binding of mix plate.
    /// </summary>
    public class WellMixPlateSummaryTO
    {
        #region Member Variables

        private List<WellMixPlateTO> wellMixPlateSummaryTO;

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WellMixPlateSummaryTO"/> class.
        /// </summary>
        public WellMixPlateSummaryTO()
        {
            wellMixPlateSummaryTO = new List<WellMixPlateTO>();
        }

        #endregion Construction

        #region Properties

        /// <summary>
        /// Gets the assignment data for mix plate.
        /// </summary>
        /// <value>
        /// List of WellMixPlateTO objects.
        /// </value>
        public List<WellMixPlateTO> WellMixPlateList
        {
            get
            {
                return wellMixPlateSummaryTO;
            }
        }

        #endregion Properties
    }
}