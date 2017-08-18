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
    /// Providing data to WellTargetSummaryTO.
    /// </summary>
    public class WellTargetSummaryTO
    {
        #region Member Variables

        /// <summary>
        /// The list of Targets.
        /// </summary>
        private List<WellTargetTO> wellTargetSummary;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="WellTargetSummaryTO" /> class.
        /// </summary>
        public WellTargetSummaryTO()
        {
            wellTargetSummary = new List<WellTargetTO>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the list of targets for output plate.
        /// </summary>
        public List<WellTargetTO> WellTargetSummary
        {
            get
            {
                return wellTargetSummary;
            }
        }

        #endregion Properties
    }
}