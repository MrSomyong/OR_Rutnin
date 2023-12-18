using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    /// <summary>
    /// Hash Algorithms
    /// </summary>
    public enum HashAlgorithms
    {
        /// <summary>
        /// MD5 Algorithms
        /// </summary>
        MD5 = 0,
        /// <summary>
        /// SHA1 Algorithms
        /// </summary>
        SHA1 = 1,
        /// <summary>
        /// SHA256 Algorithms
        /// </summary>
        SHA256 = 2,
        /// <summary>
        /// SHA384 Algorithms
        /// </summary>
        SHA384 = 3,
        /// <summary>
        /// SHA512 Algorithms
        /// </summary>
        SHA512 = 4
    }
}