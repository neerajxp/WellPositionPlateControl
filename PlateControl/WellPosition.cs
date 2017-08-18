// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

namespace PlateControl
{
    /// <summary>
    /// Data structure to store x and y axis position of well controls
    /// </summary>
    public class WellPosition
    {
        #region Member Variables

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// Constructor of Data structure to store x and y axis position of well controls
        /// </summary>
        public WellPosition()
        {
        }

        #endregion Construction

        #region Properties

        /// <summary>
        /// Well position on X axis
        /// </summary>
        private int positionX;

        public int PositionX
        {
            get
            {
                return positionX;
            }
            set
            {
                positionX = value;
            }
        }

        /// <summary>
        /// Well position on Y axis
        /// </summary>
        private int positionY;

        public int PositionY
        {
            get
            {
                return positionY;
            }
            set
            {
                positionY = value;
            }
        }

        public override string ToString()
        {
            int tempx = 64 + positionY;
            int tempy = (positionX % 12);

            return (char)tempx + tempy.ToString();
            // return (char)tempx + positionY.ToString();
        }

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}