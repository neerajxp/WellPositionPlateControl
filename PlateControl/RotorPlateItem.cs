// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using PlateControl;

namespace RotorPlateControl
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
        Samples,

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
    public class RotorPlateItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputPlateItemTO"/> class.
        /// </summary>
        private IList<Target> rotorPlateTargets;

        public RotorPlateItem()
        {
            rotorPlateTargets = new List<Target>();
        }

        public IList<Target> RotorPlateTargets
        {
            get
            {
                return rotorPlateTargets;
            }
        }

        private OrderType orderType;

        public OrderType OrderType
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