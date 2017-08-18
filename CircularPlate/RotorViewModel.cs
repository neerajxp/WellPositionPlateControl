// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.ComponentModel;
using PlateControl;
using RotorPlateControl;

namespace CircularPlate
{
    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class RotorViewModel : INotifyPropertyChanged
    {
        #region Member Variables

        #endregion Member Variables

        #region Construction

        /// <summary>
        /// TODO add meaningful comment
        /// </summary>
        public RotorViewModel()
        {
        }

        #endregion Construction

        #region Properties

        private RotorAssignment assignmentRotorVM;

        public RotorAssignment AssignmentRotorVM
        {
            get
            {
                return assignmentRotorVM;
            }
            set
            {
                assignmentRotorVM = value;
                OnPropertyChanged("AssignmentRotorVM");
            }
        }

        private int rowShapesRotorVM;

        public int RowShapesRotorVM
        {
            get
            {
                return rowShapesRotorVM;
            }
            set
            {
                rowShapesRotorVM = value;
                OnPropertyChanged("RowShapesRotorVM");
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

        private int diameterofWellVM;

        public int DiameterofWellVM
        {
            get
            {
                return diameterofWellVM;
            }
            set
            {
                diameterofWellVM = value;
                OnPropertyChanged("DiameterofWellVM");
            }
        }

        private int wellPaddingVM;

        public int WellPaddingVM
        {
            get
            {
                return wellPaddingVM;
            }
            set
            {
                wellPaddingVM = value;
                OnPropertyChanged("WellPaddingVM");
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

        #endregion Properties

        #region Methods

        #endregion Methods
    }
}