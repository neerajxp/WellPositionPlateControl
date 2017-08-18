// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2009-----------------------------------------------------------------
//
// $Id: TargetTO.cs 5132 2014-07-01 05:22:47Z mauryan $
//
// ---------------------------------------------------------------------------
using System;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Target TO used to save target data.
    /// </summary>

    public class TargetTO
    {
        #region Member Variables

        private Regex leadingTrailing = new Regex(@"^([\s]+)|([\s]+)$");

        /// <summary>
        /// The color.
        /// </summary>
        [NonSerialized]
        private Color color;

        /// <summary>
        /// The name of target.
        /// </summary>
        private string name;

        /// <summary>
        /// The reporter.
        /// </summary>
        private string reporter;

        /// <summary>
        /// The concentration.
        /// </summary>
        private decimal? concentration;

        /// <summary>
        /// The concentration with unit.
        /// </summary>
        private string concentrationUnit;

        private bool isDeleted;

        #endregion Member Variables

        #region Properties

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                if (color != value)
                {
                    color = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of target.
        /// </value>
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
        /// Gets or sets the reporter.
        /// </summary>
        /// <value>
        /// The reporter.
        /// </value>
        public string Reporter
        {
            get
            {
                return reporter;
            }

            set
            {
                if (reporter != value)
                {
                    reporter = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>True</c> If this instance is selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted
        {
            get
            {
                return isDeleted;
            }

            set
            {
                if (isDeleted != value)
                {
                    isDeleted = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the ConcentrationUnit.
        /// </summary>
        /// <value>
        /// The ConcentrationUnit.
        /// </value>
        public string ConcentrationUnit
        {
            get
            {
                return concentrationUnit;
            }

            set
            {
                if (concentrationUnit != value)
                {
                    concentrationUnit = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the concentration.
        /// </summary>
        /// <value>
        /// The concentration.
        /// </value>
        public decimal? Concentration
        {
            get
            {
                return concentration;
            }

            set
            {
                if (concentration != value)
                {
                    concentration = value;
                }
            }
        }

        public string ConcentrationWithUnit
        {
            get
            {
                if (concentration != 0)
                {
                    return string.Concat(concentration, " ", concentrationUnit);
                }
                else
                {
                    return concentrationUnit;
                }
            }
        }

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetTO"/> class.
        /// </summary>
        public TargetTO()
        {
            Name = string.Empty;
            Reporter = string.Empty;
        }

        #endregion Constructor
    }
}