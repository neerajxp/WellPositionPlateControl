using System.ComponentModel;

// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------
using PlateControl;

namespace CircularPlate
{
    /// <summary>
    /// TODO add meaningful comment.
    /// </summary>
    public class TestViewModel : INotifyPropertyChanged
    {
        #region Member Variables

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// TODO add meaningful comment
        /// </summary>
        public TestViewModel()
        {
        }

        private PositionContainment controlType;

        public PositionContainment ControlType
        {
            get
            {
                return controlType;
            }
            set
            {
                controlType = value;
                OnPropertyChanged("ControlType");
            }
        }

        private bool isEnabledVMe;

        public bool IsEnabledVM
        {
            get
            {
                return isEnabledVMe;
            }
            set
            {
                isEnabledVMe = value;
                OnPropertyChanged("IsEnabledVM");
            }
        }

        private int rowShapesVM;

        public int RowShapesVM
        {
            get
            {
                return rowShapesVM;
            }
            set
            {
                rowShapesVM = value;
                OnPropertyChanged("RowShapesVM");
            }
        }

        private int colShapesVM;

        public int ColShapesVM
        {
            get
            {
                return colShapesVM;
            }
            set
            {
                colShapesVM = value;
                OnPropertyChanged("ColShapesVM");
            }
        }

        private int diameterEachShapeVM;

        public int DiameterEachShapeVM
        {
            get
            {
                return diameterEachShapeVM;
            }
            set
            {
                diameterEachShapeVM = value;
                OnPropertyChanged("DiameterEachShapeVM");
            }
        }

        private int rowSpacingVM;

        public int RowSpacingVM
        {
            get
            {
                return rowSpacingVM;
            }
            set
            {
                rowSpacingVM = value;
                OnPropertyChanged("RowSpacingVM");
            }
        }

        private int colSpacingVM;

        public int ColSpacingVM
        {
            get
            {
                return colSpacingVM;
            }
            set
            {
                colSpacingVM = value;
                OnPropertyChanged("ColSpacingVM");
            }
        }

        private int isBorderVisibleVM;

        public int IsBorderVisibleVM
        {
            get
            {
                return isBorderVisibleVM;
            }
            set
            {
                isBorderVisibleVM = value;
                OnPropertyChanged("IsBorderVisibleVM");
            }
        }

        private bool isInputPlateVM;

        public bool IsInputPlateVM
        {
            get
            {
                return isInputPlateVM;
            }
            set
            {
                isInputPlateVM = value;
                OnPropertyChanged("IsInputPlateVM");
            }
        }

        private Assignment assignmentVM;

        public Assignment AssignmentVM
        {
            get
            {
                return assignmentVM;
            }
            set
            {
                assignmentVM = value;
                OnPropertyChanged("AssignmentVM");
            }
        }

        private bool isReadOnlyVM;

        public bool IsReadOnlyVM
        {
            get
            {
                return isReadOnlyVM;
            }
            set
            {
                isReadOnlyVM = value;
                OnPropertyChanged("IsReadOnlyVM");
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

        #endregion Construction

        #region Properties

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}