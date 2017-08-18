// ------Copyright: QIAGEN GmbH, Hilden, Germany------------------------------
// ------2013-----------------------------------------------------------------
//
// $Id: Position.cs 6215 2014-09-02 10:25:19Z patilp $
//
// ---------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace QIAgility.Common.ObjectModel.Worktable
{
    /// <summary>
    /// It contains information of Position.
    /// </summary>

    public class Position
    {
        public Position()
        {
            IsTipAvailable = false;
        }

        /// <summary>
        /// Gets or sets the position label.
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether is tip available.
        /// </summary>
        /// <value>
        /// <c>True</c> if tip is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsTipAvailable
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the position x.
        /// </summary>
        public double PosX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the position y.
        /// </summary>
        public double PosY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets Row of the position.
        /// </summary>
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets Column of the position.
        /// </summary>
        public int Column { get; set; }

        /// <summary>
        /// Gets or sets the Diameter.
        /// </summary>
        public double Diameter
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the well is marked.
        /// </summary>
        public bool IsMarked { get; set; }
    }
}