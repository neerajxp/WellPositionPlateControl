// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2009-----------------------------------------------------------------
//
// $Id: OrderType.cs 6359 2014-09-11 04:20:10Z mauryan $
//
// ---------------------------------------------------------------------------
using System;

namespace QIAgility.CoreApp.Controls.ObjectModel.PlateControl
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
        Standard,

        /// <summary>
        /// Indiates a filled grey circle.
        /// </summary>
        AnyKind,

        /// <summary>
        /// Indicates a empty circle  with grey border.
        /// </summary>
        Empty,

        /// <summary>
        /// Indicates a calibration shape.
        /// </summary>
        Calibration,

        /// <summary>
        /// Indicates a Auto Calibration shape.
        /// </summary>
        AutoCalibration,

        /// <summary>
        /// Indiates a cirlce having a cross mark.
        /// </summary>
        Blocked,

        /// <summary>
        /// Indiates gray filled input well.
        /// </summary>
        InputSample,

        /// <summary>
        /// Indiates marked well, having a slash within circle.
        /// </summary>
        Marked
    }
}