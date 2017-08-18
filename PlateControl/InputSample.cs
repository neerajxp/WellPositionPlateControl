// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.ComponentModel;

namespace PlateControl
{
    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class InputSample
    {
        /// <summary>
        /// The concentration.
        /// </summary>
        private double? concentration;

        /// <summary>
        /// The description.
        /// </summary>
        private string description;

        /// <summary>
        /// The name of sample.
        /// </summary>
        private string name;

        /// <summary>
        /// The position of sample.
        /// </summary>
        private string position;

        public InputSample()
        {
        }

        /// <summary>
        /// Gets or sets the concentration.
        /// </summary>
        /// <value>
        /// The concentration.
        /// </value>
        public double? Concentration
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
                    OnPropertyChanged("Concentration");
                    OnPropertyChanged("ConcentrationWithUnit");
                }
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
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
                    OnPropertyChanged("Description");
                }
            }
        }

        /// <summary>
        /// Gets or sets the name.
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
                    OnPropertyChanged("Name");
                }
            }
        }

        ///// <summary>
        ///// Gets the Composite Control TargetTO list.
        ///// </summary>
        ///// <value>
        ///// The List of CompositeControlTargetTO.
        ///// </value>
        ///// /// <summary>
        ///// The composite control target tos.
        ///// </summary>
        //private IList<Target> inputPlateTargets;

        //public IList<Target> InputPlateTargets
        //{
        //    get
        //    {
        //        return inputPlateTargets;
        //    }
        //}

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public string WellPosition
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
                    OnPropertyChanged("WellPosition");
                }
            }
        }

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
                    OnPropertyChanged("IsWorklistSampleSelected");
                }
            }
        }

        private bool isWorklistSampleSelected;

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
                    OnPropertyChanged("ConcentrationUnit");
                }
            }
        }

        private string concentrationUnit;

        public string ConcentrationWithUnit
        {
            get
            {
                return concentration + " " + ConcentrationUnit;
            }
        }

        /// <summary>
        /// The target type.
        /// </summary>
        private TargetType targetType;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}