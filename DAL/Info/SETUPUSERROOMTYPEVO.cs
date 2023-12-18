using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    public class SETUPUSERROOMTYPEVO
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string RoomType { get; set; }
        public string RoomTypeName { get; set; }
    }
}
