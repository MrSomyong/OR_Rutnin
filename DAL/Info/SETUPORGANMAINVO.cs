﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPORGANMAINVO
    {

        public string MainCode { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }

        public List<SETUPORGANSUBVO> lstSETUPIMPLANTSUBVO { get; set; }
    }
}
