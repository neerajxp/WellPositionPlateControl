// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id:  $
//
// ---------------------------------------------------------------------------

namespace PlateControl
{
    /// <summary>
    /// This enum contains various well controls to be generated
    /// </summary>
    public enum PositionContainment : int
    {
        /// <summary>
        /// Indiates a blue filled cirlce if input type control else a PI circle with filling
        /// </summary>
        SamplesOnly,

        /// <summary>
        /// Indiates a cirlce having a cross mark
        /// </summary>
        Blocked,

        /// <summary>
        /// Indiates a black filled cirlce having black filled rectangle legend
        /// </summary>
        StandardsOnly,

        /// <summary>
        /// Indiates a black filled cirlce with a black having empty legend with black border
        /// </summary>
        ControlsOnly,

        /// <summary>
        /// Indiates a filled grey circle
        /// </summary>
        AnyKind,

        /// <summary>
        /// Indicates a empty circle  with grey border
        /// </summary>
        Empty,

        /// <summary>
        /// Indicates a calibration shape
        /// </summary>
        Calibration,

        Marked,

        Buffer
    }
}