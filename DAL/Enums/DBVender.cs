using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum DBVender : int
    {
        /// <summary>
        /// Sql Server 2000,2005,2008 and Express Vender
        /// </summary>
        SQLServer = 1,
        /// <summary>
        /// Oracle Database Vender
        /// </summary>
        Oracle = 2,
        /// <summary>
        /// IBM DB2 Database Vender
        /// </summary>
        DB2 = 3,
        /// <summary>
        /// My SQL Database Vender
        /// </summary>
        MySQL = 4,
        /// <summary>
        /// Sybase Advantage Database Server Vender
        /// </summary>
        SyBase = 5,
        /// <summary>
        /// Infomix Database Vender
        /// </summary>
        Infomix = 6,
        /// <summary>
        /// Postgre SQL Database Vender
        /// </summary>
        PostgreSQL = 7,
        /// <summary>
        /// Paradox Database Vender
        /// </summary>
        Paradox = 8,
        /// <summary>
        /// FireBird Database Vender
        /// </summary>
        Firebird = 9,
        /// <summary>
        /// AS/400 (IBM iSeries) Database Vender
        /// </summary>
        AS400 = 10,
        /// <summary>
        /// Progress Database Vender
        /// </summary>
        Progress = 11,
        /// <summary>
        /// Unkown Database
        /// </summary>
        Unkhown = -1
    }
}