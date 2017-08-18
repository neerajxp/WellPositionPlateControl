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
    public class InputAssignment
    {
        /// <summary>
        /// The control types.
        /// </summary>
        private IList<InputSample> samples;

        public InputAssignment()
        {
            samples = new List<InputSample>();
        }

        public IList<InputSample> Samples
        {
            get
            {
                return samples;
            }
        }
    }
}