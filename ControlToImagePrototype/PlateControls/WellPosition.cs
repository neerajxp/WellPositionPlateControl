// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Well position control.
    /// </summary>
    public class WellPosition
    {
        #region Member Variables

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="WellPosition"/> class.
        /// </summary>
        public WellPosition()
        {
        }

        #endregion Construction

        #region Properties

        /// <summary>
        /// Gets or sets the position label.
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Well position on X axis.
        /// </summary>
        public int PositionX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Well position on Y axis.
        /// </summary>
        public int PositionY
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}