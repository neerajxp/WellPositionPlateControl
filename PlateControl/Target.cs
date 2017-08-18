// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Windows.Media;

//using System.Drawing;

namespace PlateControl
{
    public enum TargetType : int
    {
        /// <summary>
        /// Test Target type.
        /// </summary>
        Test,

        /// <summary>
        /// IC Target type.
        /// </summary>
        IC
    }

    /// <summary>
    /// Temporary class to minic target need to remove.
    /// merge
    /// </summary>
    public class Target : INotifyPropertyChanged
    {
        #region Member Variables

        private string function;

        /// <summary>
        /// The name of target.
        /// </summary>
        private string name;

        /// <summary>
        /// The concentration with unit.
        /// </summary>
        private string concentrationUnit;

        /// <summary>
        /// The color.
        /// </summary>
        [NonSerialized]
        private Color color;

        /// <summary>
        /// The concentration.
        /// </summary>
        private double? concentration;

        /// <summary>
        /// The target type.
        /// </summary>
        private TargetType targetType;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputPlateTargetTO"/> class.
        /// </summary>
        public Target()
        {
        }

        #endregion Constructor

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
                    OnPropertyChanged("Color");
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
                    OnPropertyChanged("ConcentrationUnit");
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
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Function
        {
            get
            {
                return function;
            }

            set
            {
                if (function != value)
                {
                    function = value;
                    OnPropertyChanged("Function");
                }
            }
        }

        public string TargetInformation
        {
            get
            {
                return string.Concat(Name, " , ", TargetType, " , ", function).TrimEnd(" , ".ToCharArray());
            }
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
                    OnPropertyChanged("ConcentrationWithUnit");
                    OnPropertyChanged("Concentration");
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

        /// <summary>
        /// Gets or sets the type of the target.
        /// </summary>
        /// <value>
        /// The type of the target.
        /// </value>
        public TargetType TargetType
        {
            get
            {
                return targetType;
            }

            set
            {
                if (targetType != value)
                {
                    targetType = value;
                    OnPropertyChanged("TargetType");
                }
            }
        }

        #endregion Properties

        #region Events

        public event PropertyChangedEventHandler PropertyChangedd;

        private void OnPropertyChanged(string propertyname)
        {
            if (this.PropertyChangedd != null)
            {
                PropertyChangedd(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        #endregion Events

        #region Properties

        private int id;

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                OnPropertyChanged("Id");
            }
        }

        private string description;

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
                OnPropertyChanged("Description");
            }
        }

        #endregion Properties

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, string propertyname)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(sender, new PropertyChangedEventArgs(propertyname));
            }
        }

        #endregion Events
    }
}