using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPLOGONVO
    {
        public string UserID { get; set; }
        public string Password { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }

        public string AccessID { get; set; }
        public string AccessCode { get; set; }
        public string AccessName { get; set; }

        public bool UseReserve { get; set; }
        public bool UseAppointment { get; set; }
        public bool UseOperation { get; set; }
        public bool UseReport { get; set; }
        public bool UseSetup { get; set; }
        public string UseType { get; set; }
        public string Remark { get; set; }
        public bool Active { get; set; }
        public string strActive { get; set; }
        public bool AdminType { get; set; }
        public string UseTypeName { get; set; }
        public string Doctor { get; set; }

        public bool UseViewBooking { get; set; }
        public bool UsePostTreatment { get; set; }
        public bool UseEnquiryPrice { get; set; }
        public bool UseInjectionRoom { get; set; }
        public bool UseReportPostOP { get; set; }
        public bool UseSetupGroupMethod { get; set; }
        public bool UseReserveReadOnly { get; set; }

        public bool UseSetupDoctor { get; set; }
        public bool UseSetupNurse { get; set; }

        public SETUPUSERTYPEVO SETUPUSERTYPEVO { get; set; }
    }
}
