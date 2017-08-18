// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlateControl
{
    /// <summary>
    /// Control type.
    /// </summary>
    [Serializable]
    public enum OrderType : int
    {
        /// <summary>
        ///  Sample Type.
        /// </summary>
        Sample,

        /// <summary>
        ///  Control Type.
        /// </summary>
        Control,

        /// <summary>
        ///  Standard Type.
        /// </summary>
        Standard
    }

    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class PlateItem : INotifyPropertyChanged
    {
        #region Member Variables

        /// <summary>
        /// The position of sample.
        /// </summary>
        private WellPosition position;

        /// <summary>
        /// The name of sample.
        /// </summary>
        private string name;

        /// <summary>
        /// The composite control target tos.
        /// </summary>
        private IList<Target> outputPlateTargets;

        #endregion Member Variables

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputPlateItemTO"/> class.
        /// </summary>
        public PlateItem()
        {
            outputPlateTargets = new List<Target>();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets the Composite Control TargetTO list.
        /// </summary>
        /// <value>
        /// The List of CompositeControlTargetTO.
        /// </value>
        public IList<Target> OutputPlateTargets
        {
            get
            {
                return outputPlateTargets;
            }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public WellPosition WellPosition
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

        public PositionContainment OrderType
        {
            get
            {
                return orderType;
            }

            set
            {
                if (orderType != value)
                {
                    orderType = value;
                    OnPropertyChanged("OrderType");
                }
            }
        }

        private PositionContainment orderType;

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyname)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }

        #endregion Properties

        //public PlateItem()
        //{
        //    outputPlateTargets = new List<Target>();
        //}

        //private string name;

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
        //            OnPropertyChanged(this, "Name");
        //        }
        //    }
        //}

        //private string position;

        //public string WellPosition
        //{
        //    get
        //    {
        //        return position;
        //    }

        //    set
        //    {
        //        if (position != value)
        //        {
        //            position = value;
        //            OnPropertyChanged(this, "WellPosition");
        //        }
        //    }
        //}

        //private IList<Target> outputPlateTargets;

        //public IList<Target> OutputPlateTargets
        //{
        //    get
        //    {
        //        return outputPlateTargets;
        //    }
        //}

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(object sender, string propertyname)
        //{
        //    if (this.PropertyChanged != null)
        //    {
        //        PropertyChanged(sender, new PropertyChangedEventArgs(propertyname));
        //    }
        //}
    }
}