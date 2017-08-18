// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.ComponentModel;
using System.Windows.Media;

namespace QIAgility.CoreApp.Controls.PlateControls
{
    /// <summary>
    /// Temporary class to minic target need to remove.
    /// </summary>
    public class Target : INotifyPropertyChanged
    {
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
                OnPropertyChanged(this, "Id");
            }
        }

        private Color color;

        public Color Color
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
                OnPropertyChanged(this, "Color");
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
                OnPropertyChanged(this, "Description");
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