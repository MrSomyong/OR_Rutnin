using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum PM
    {
        Unkhown = 0,
        UserGroup = 1,
        User = 2,
        Divission = 3,
        Agent = 4,
        Customer_Supplier = 5,
        Airline = 6,
        Commodity = 7,
        Country = 8,
        FreightUnit = 9,
        Liner = 10,
        Port = 11,
        OverseaShipperConsignee = 12,
        ThaiShipperConsignee = 13,
        Services = 14,
        CommodityGroup = 15,
        ContainerTypeSeaFreight = 16,
        ContainerTypeAirFreight = 17,
        ContainerSizeSeaFreight = 18,
        ContainerSizeAirFreight = 19,
        FreightCurrency = 20,
        Package = 21,
        TypeofPayment = 22,
        FeederVessel = 23,
        Trade = 24,
        Bank = 25,
        AccountCode = 26,
        Truck=27
    }

    //public class Permission
    //{
    //    public static int GetPM(PM _PM)
    //    {
    //        int _PM_ID = 0;
    //        try
    //        {
    //            _PM_ID = (int)Enum.Parse(typeof(PM), Enum.GetName(typeof(PM), _PM));
    //        }
    //        catch { }
    //        return _PM_ID;
    //    }
    //}
}