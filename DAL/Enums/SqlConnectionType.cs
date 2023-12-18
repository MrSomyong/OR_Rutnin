using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum SqlConnectionType
    {
        /// <summary>
        /// Stardard Connection type
        /// </summary>
        Standard = 1,
        /// <summary>
        /// Trusted Connection type
        /// </summary>
        Trusted = 2
    }
}