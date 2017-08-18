// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

using System.Collections.Generic;

namespace PlateControl
{
    /// <summary>
    /// TODO add meaningful comment
    /// </summary>
    public class Assignment
    {
        public Assignment()
        {
            outputPlateItemLists = new List<PlateItem>();
        }

        private IList<PlateItem> outputPlateItemLists;

        public IList<PlateItem> OutputPlateItemLists
        {
            get
            {
                return outputPlateItemLists;
            }
        }
    }
}