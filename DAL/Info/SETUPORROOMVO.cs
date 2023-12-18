using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPORROOMVO
    {
        public string ID { get; set; }
        public string CODE { get; set; }
        public string CodeType { get; set; }
        public string Name { get; set; }
        public string CodeTypeName { get; set; }
        public string ArCodeType { get; set; }
    }
}
