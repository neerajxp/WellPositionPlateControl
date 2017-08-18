// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.ComponentModel;

namespace PlateControl
{
    //public enum TargetType : int
    //{
    //    /// <summary>
    //    /// Test Target type.
    //    /// </summary>
    //    Test,

    //    /// <summary>
    //    /// IC Target type.
    //    /// </summary>
    //    IC
    //}

    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class InputTarget : INotifyPropertyChanged
    {
        //private string function;
        /// <summary>
        /// The is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is selected.
        /// </summary>
        /// <value>
        /// <c>True</c> if this instance is selected; otherwise, <c>false</c>.
        /// </value>
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
                    OnPropertyChanged("IsSelected");
                }
            }
        }

        ///// <summary>
        ///// The name of target.
        ///// </summary>
        //private string name;

        ///// <summary>
        ///// The concentration with unit.
        ///// </summary>
        //private string concentrationUnit;

        ///// <summary>
        ///// The color.
        ///// </summary>
        //[NonSerialized]
        //private Color color;

        ///// <summary>
        ///// The concentration.
        ///// </summary>
        //private double? concentration;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputPlateTargetTO"/> class.
        /// </summary>
        public InputTarget()
        {
        }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        //public Color Color
        //{
        //    get
        //    {
        //        return color;
        //    }

        //    set
        //    {
        //        if (color != value)
        //        {
        //            color = value;
        //            OnPropertyChanged("Color");
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the ConcentrationUnit.
        /// </summary>
        /// <value>
        /// The ConcentrationUnit.
        /// </value>
        //public string ConcentrationUnit
        //{
        //    get
        //    {
        //        return concentrationUnit;
        //    }

        //    set
        //    {
        //        if (concentrationUnit != value)
        //        {
        //            concentrationUnit = value;
        //            OnPropertyChanged("ConcentrationUnit");
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name of target.
        /// </value>
        //public string Name
        //{
        //    get
        //    {
        //        return name;
        //    }

        //    set
        //    {
        //        if (name != value)
        //        {
        //            name = value;
        //            OnPropertyChanged("Name");
        //        }
        //    }
        //}

        //public string Function
        //{
        //    get
        //    {
        //        return function;
        //    }

        //    set
        //    {
        //        if (function != value)
        //        {
        //            function = value;
        //            OnPropertyChanged("Function");
        //        }
        //    }
        //}

        //public string TargetInformation
        //{
        //    get
        //    {
        //        return string.Concat(Name, " , ", TargetType, " , ", function).TrimEnd(" , ".ToCharArray());
        //    }
        //}

        /// <summary>
        /// Gets or sets the concentration.
        /// </summary>
        /// <value>
        /// The concentration.
        /// </value>
        //public double? Concentration
        //{
        //    get
        //    {
        //        return concentration;
        //    }

        //    set
        //    {
        //        if (concentration != value)
        //        {
        //            concentration = value;
        //            OnPropertyChanged("ConcentrationWithUnit");
        //            OnPropertyChanged("Concentration");
        //        }
        //    }
        //}

        //public string ConcentrationWithUnit
        //{
        //    get
        //    {
        //        if (concentration != 0)
        //        {
        //            return string.Concat(concentration, " ", concentrationUnit);
        //        }
        //        else
        //        {
        //            return concentrationUnit;
        //        }
        //    }
        //}

        /// <summary>
        /// Gets or sets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        //public TargetType TargetType
        //{
        //    get
        //    {
        //        return targetType;
        //    }

        //    set
        //    {
        //        if (targetType != value)
        //        {
        //            targetType = value;
        //            OnPropertyChanged("TargetType");
        //        }
        //    }
        //}

        /// <summary>
        /// The target type.
        /// </summary>
        //private TargetType targetType;

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