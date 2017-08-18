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
    /// Providing data to WellTargetTO.
    /// </summary>
    public class WellControlSummaryTO
    {
        #region Member Variables

        /// <summary>
        /// The list of samples.
        /// </summary>
        private List<WellControlTO> wellControlSummary;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WellControlSummaryTO" /> class.
        /// </summary>
        public WellControlSummaryTO()
        {
            wellControlSummary = new List<WellControlTO>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the list of samples for input plate.
        /// </summary>
        public List<WellControlTO> WellControlSummary
        {
            get
            {
                return wellControlSummary;
            }
        }

        #endregion Properties
    }
}