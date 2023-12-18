using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

/// <summary>
/// Summary description for Enum
/// </summary>
public class EnumOR
{

    //LogOn
    public enum UseReserve
    {
        None = 0,
        Use = 1,
    }
    public enum UseAppointment
    {
        None = 0,
        Use = 1,
    }
    public enum UseOperation
    {
        None = 0,
        Use = 1,
    }
    public enum UseReport
    {
        None = 0,
        Use = 1,
    }
    public enum UseSetup
    {
        None = 0,
        Use = 1,
    }
    //Information
    public enum Gender
    {
        None = 0,
        Male = 1,
        Female = 2,
        Unspecify = 3,
    }

    public enum Sex
    {
        None = 0,
        ชาย = 1,
        หญิง = 2,
        Unspecify = 3,
    }
    //Operation
    public enum PatientInfection
    {
        None = 0,
        PatientInfection = 1,
    }
    public enum PatientType1
    {
        None = 0,
        PatientType1 = 1,
    }
    public enum PatientType2
    {
        None = 0,
        PatientType2 = 1,
    }
    public enum PatientUP
    {
        None = 0,
        PatientUP = 1,
    }
    public enum ORTimeFollow
    {
        None = 0,
        ORTimeFollow = 1,
    }
    public enum ORStatCase
    {
        None = 0,
        ORTimeFollow = 1,
    }
    public enum ORSpecificType
    {
        None = 0,
        Specific = 1,
        Refer = 2,
    }
    public enum ORStatus
    {
        None = 0,
        OPD = 1,
        IPD = 2,
        Observe = 3,
        Reserve = 4,
    }
    public enum AdmitTimeType
    {
        เช้า = 1,
        บ่าย = 2,
    }
    public enum ORAnesthesiaSign
    {
        None = 0,
        Plus = 1,
        Both = 2,
    }

    public enum AnesthesiaSign
    {
        [Description("None")] None = 0,
        [Description("+")] Plus = 1,
        [Description("+-")] Both = 2,
    }

    public static IDictionary<int, string> GetAnesthesiaSign<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.AnesthesiaSign)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public enum ORSide
    {
        RE = 1,
        LE = 2,
        BE = 3,
        ยังไม่ระบุตา = 4,
        None = 5,
    }

    //Menu
    //public enum Menu
    //{
    //    Reserve = 1,
    //    Appointment = 2,
    //    Operarion = 3,
    //    Report = 4,
    //    SetupUserID = 5,
    //    SetupPreset = 6,
    //}

    public enum UserType
    {
        Adminstrator = 1,
        IT = 2,
        ReadOnly = 3,
        [Description("Enquire Only")] EnquireOnly = 4,
        [Description("Enquire and Post Charge")] EnquireAndPostCharge = 5,
        [Description("User")] User = 6
    }

    public enum ORCaseType
    {
        [Description("")] None = 0,
        [Description("Elective Case")] ElectiveCase = 1,
        [Description("Urgency Case")] UrgencyCase = 2,
        [Description("Emergency Case")] EmergencyCase = 3
    }

    public enum ORWrongCase
    {
        [Description("")] None = 0,
        [Description("ผิดคน")] ผิดคน = 1,
        [Description("ผิดข้าง")] ผิดข้าง = 2,
        [Description("ผิดชนิดการผ่าตัด")] ผิดชนิดการผ่าตัด = 3
    }

    public enum ORWoundType
    {
        [Description("")] None = 0,
        [Description("ภายใน 48 ชม.")] hr48 = 1,
        [Description("ภายใน 30 วัน หลังการผ่าตัด")] day30 = 2
    }

    public enum ORUnplantType
    {
        [Description("")] None = 0,
        [Description("External Segment")] ExternalSegment = 1,
        [Description("Anterior Segment")] AnteriorSegment = 2,
        [Description("Posterior Segment")] PosteriorSegment = 3
    }

    public enum NurseType
    {
        [Description("")] None = 0,
        [Description("Scrub")] Scrub = 1,
        [Description("Circulate")] Circulate = 2,
        [Description("Anes Nurse")] AnesNurse = 3
    }

    public enum ImplantType
    {
        [Description("")] None = 0,
        [Description("IOL")] IOL = 1,
        [Description("Enucleation Implant")] EnucleationImplant = 2,
        [Description("Gass Ball")] GassBall = 3,
        [Description("GlauComa Implant")] GlauComaImplant = 4,
        [Description("Silicone Band")] SiliconeBand = 5,
        [Description("Plant and Screw")] PlantAndScrew = 6
    }

    public enum ORProcedureType
    {
        [Description("")] None = 0,
        [Description("Major")] Major = 1,
        [Description("Minor")] Minor = 2
    }

    public enum CxlCheckFlag
    {
        None = 0,
        Only = 1,
        Without = 2,
    }

    public static IDictionary<int, string> GetORProcedureType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ORProcedureType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public static object GetNames(Type type)
    {
        throw new NotImplementedException();
    }

    public static string GetEnumDescription(Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        else
            return value.ToString();
    }

    public static IDictionary<int, string> GetAllName<TEnum>() where TEnum : struct
    {
        var enumerationType = typeof(TEnum);

        if (!enumerationType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumerationType))
        {
            var name = Enum.GetName(enumerationType, value);
            dictionary.Add(value, name);
        }

        return dictionary;
    }

    public static IDictionary<int, string> GetORWrongCase<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ORWrongCase)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public static IDictionary<int, string> GetORCaseType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ORCaseType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public static IDictionary<int, string> GetORWoundType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ORWoundType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }


    public static IDictionary<int, string> GetUnplantType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ORUnplantType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public static IDictionary<int, string> GetImplantType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.ImplantType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }

    public static IDictionary<int, string> GetNurseType<TEnum>() where TEnum : struct
    {
        var enumeType = typeof(TEnum);
        if (!enumeType.IsEnum)
            throw new ArgumentException("Enumeration type is expected.");

        var dictionary = new Dictionary<int, string>();

        foreach (int value in Enum.GetValues(enumeType))
        {
            var desc = EnumOR.GetEnumDescription((EnumOR.NurseType)value);
            dictionary.Add(value, desc);
        }

        return dictionary;
    }
}
