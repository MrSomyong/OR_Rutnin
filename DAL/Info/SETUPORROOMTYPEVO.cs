using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPORROOMTYPEVO
    {
        public string ID { get; set; }        
        public string CodeType { get; set; }
        public string Name { get; set; }
        public string ProcedureCode { get; set; }
    }
}
