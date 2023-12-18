using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Info
{
    public class SETUPCOMPUTER
    {
        public string ComputerCode { get; set; }
        public string ComputerName { get; set; }
        public string DefaultStoreCode { get; set; }
        public string DefaultStoreName { get; set; }
        public string DefaultClinicCode { get; set; }
        public string DefaultClinicName { get; set; }
        public VT_STORE StoreInfo { get; set; }
        public VT_CLINIC ClinicInfo { get; set; }

    }
}
