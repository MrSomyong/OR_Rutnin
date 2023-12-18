using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class POSTORDETAILVO
    {
        public string ORID { get; set; }
        public Nullable<DateTime> StartORDateTime { get; set; }
        public Nullable<DateTime> FinishORDateTime { get; set; }
        public Nullable<DateTime> StartAnesDateTime { get; set; }
        public Nullable<DateTime> FinishAnesDateTime { get; set; }
        public Nullable<DateTime> StartBlockDateTime { get; set; }
        public Nullable<DateTime> FinishBlockDateTime { get; set; }
        public Nullable<DateTime> StartRecoveryDateTime { get; set; }
        public Nullable<DateTime> FinishRecoveryDateTime { get; set; }
        public int ORCaseType { get; set; }
        public int ORWrongCase { get; set; }
        public int ORWoundType { get; set; }
        public int ORUnplantType { get; set; }

        public Nullable<Boolean> ORWoundType1 { get; set; }
        public Nullable<Boolean> ORWoundType2 { get; set; }
        public Nullable<Boolean> ORWoundType3 { get; set; }
        public Nullable<Boolean> ORWoundType4 { get; set; }

        public Nullable<Boolean> External { get; set; }
        public Nullable<Boolean> Anterior { get; set; }
        public Nullable<Boolean> Posterior { get; set; }

        public Nullable<Boolean> ChangOperation { get; set; }
        public Nullable<Boolean> HR48 { get; set; }
        public Nullable<Boolean> Day30 { get; set; }

        public string Indicator { get; set; }
    }
}
