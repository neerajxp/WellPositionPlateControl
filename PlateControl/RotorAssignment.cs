// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.ObjectModel;
using PlateControl;

namespace RotorPlateControl
{
    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class RotorAssignment
    {
        public RotorAssignment()
        {
            // rotorPlateItemLists = new List<PlateItem>();
            rotorPlateItemLists = new ObservableCollection<PlateItem>();
        }

        // private IList<PlateItem> rotorPlateItemLists;
        private ObservableCollection<PlateItem> rotorPlateItemLists;

        // public IList<PlateItem> RotorPlateItemLists
        public ObservableCollection<PlateItem> RotorPlateItemLists
        {
            get
            {
                return rotorPlateItemLists;
            }
        }
    }
}