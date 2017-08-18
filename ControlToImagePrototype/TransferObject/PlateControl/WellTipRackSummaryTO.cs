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
    /// Creates a object of WellTipRackSummaryTO to be used in Tip Rack plate for data binding.
    /// </summary>
    public class WellTipRackSummaryTO
    {
        #region Member Variables

        private List<WellTipRackTO> wellTipRackSummaryTO;

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WellTipRackSummaryTO"/> class.
        /// </summary>
        public WellTipRackSummaryTO()
        {
            wellTipRackSummaryTO = new List<WellTipRackTO>();
        }

        #endregion Construction

        #region Properties

        /// <summary>
        /// Gets the list of WellTipRackTO.
        /// </summary>
        /// <value>
        /// The WellTipRackTO list.
        /// </value>
        public List<WellTipRackTO> WellTipRackSummary
        {
            get
            {
                return wellTipRackSummaryTO;
            }
        }

        #endregion Properties
    }
}