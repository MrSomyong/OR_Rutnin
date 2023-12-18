using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class ORMIGRATIONVO
    {
        public string ID { get; set; }
        public string HN { get; set; }
        public Nullable<DateTime> ORDate { get; set; }
        public string strORDate { get; set; }
        public int Side { get; set; }
        public string strSide { get; set; }
        public string ProcedureMemo { get; set; }
        public string Note { get; set; }
        public string Surgeon { get; set; }
        public string SurgeonName { get; set; }
        public string ORRoom { get; set; }
        public string ORRoomName { get; set; }

    }
}
