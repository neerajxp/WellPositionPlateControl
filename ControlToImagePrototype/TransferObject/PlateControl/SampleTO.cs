// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2009-----------------------------------------------------------------
//
// $Id: SampleTO.cs 5826 2014-08-11 08:49:12Z mauryan $
//
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using QIAgility.Common.ObjectModel.Worktable;

namespace QIAgility.CoreApp.Controls.TransferObject.PlateControl
{
    /// <summary>
    /// Sample details.
    /// </summary>

    public class SampleTO
    {
        #region Member Variables

        private bool isWorklistSampleSelected;
        private bool isAssigned;
        private decimal? concentration;
        private string description;
        private string name = string.Empty;
        private string concentrationUnit;
        private Position position;

        /// <summary>
        /// The add IC.
        /// </summary>
        private bool addIC;

        /// <summary>
        /// The replicate count.
        /// </summary>
        private int replicateCount;

        /// <summary>
        /// The exclude.
        /// </summary>
        private bool exclude;

        /// <summary>
        /// The target list.
        /// </summary>
        private List<string> targetListNames;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleTO"/> class.
        /// </summary>
        public SampleTO()
        {
            targetListNames = new List<string>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the concentration of sample.
        /// </summary>
        /// <value>
        /// The concentration of sample.
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

        /// <summary>
        /// Gets or sets a value indicating whether samples can have IC.
        /// </summary>
        /// <value>
        ///   <c>True</c> if added IC in samples; otherwise, <c>false</c>.
        /// </value>
        public bool AddIC
        {
            get
            {
                return addIC;
            }

            set
            {
                if (addIC != value)
                {
                    addIC = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the replicate count.
        /// </summary>
        /// <value>
        /// The replicate count.
        /// </value>
        public int ReplicateCount
        {
            get
            {
                return replicateCount;
            }

            set
            {
                if (replicateCount != value)
                {
                    replicateCount = value;
                }
            }
        }

        /// <summary>
        /// Gets the target list.
        /// </summary>
        /// <value>
        /// The target list.
        /// </value>
        public List<string> TargetListNames
        {
            get
            {
                return targetListNames;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Sample"/> is exclude.
        /// </summary>
        /// <value>
        ///   <c>True</c> if exclude; otherwise, <c>false</c>.
        /// </value>
        public bool Exclude
        {
            get
            {
                return exclude;
            }

            set
            {
                if (exclude != value)
                {
                    exclude = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the description of sample.
        /// </summary>
        /// <value>
        /// The description of sample.
        /// </value>
        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                if (description != value)
                {
                    description = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the name of sample.
        /// </summary>
        /// <value>
        /// The name of sample.
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
                    name = name.ToString().Trim();
                }
            }
        }

        /// <summary>
        /// Gets or sets the position of well.
        /// </summary>
        /// <value>
        /// The position of plate's well where in sample is placed.
        /// </value>
        public Position WellPosition
        {
            get
            {
                return position;
            }

            set
            {
                if (position != value)
                {
                    position = value;
                }
            }
        }

        /// <summary>
        /// Gets the label of plate's well position.
        /// </summary>
        public string Label
        {
            get
            {
                return WellPosition.Label;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is worklist sample selected.
        /// </summary>
        /// <value>
        /// <c>True</c> if this instance is worklist sample selected; otherwise, <c>false</c>.
        /// </value>
        public bool IsWorklistSampleSelected
        {
            get
            {
                return isWorklistSampleSelected;
            }

            set
            {
                if (isWorklistSampleSelected != value)
                {
                    isWorklistSampleSelected = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the concentration unit of sample.
        /// </summary>
        /// <value>
        /// The concentration unit of sample.
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
        /// Gets the concentration with unit.
        /// </summary>
        public string ConcentrationWithUnit
        {
            get
            {
                return concentration + " " + ConcentrationUnit;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this sample is assigned.
        /// </summary>
        /// <value>
        /// <c>True</c> If this sample is assigned; otherwise, <c>false</c>.
        /// </value>
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

        #endregion Properties
    }
}