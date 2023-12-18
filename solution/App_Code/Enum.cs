using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Enum
/// </summary>
public class Enum
{
    public enum UserType
    {
        None = 0,
        User = 1,
        Manager = 2,
        Admin = 3,
        Dev = 99,
    }

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
        None_Specific = 0,
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
    public enum ORAnesthesiaSign
    {
        None = 0,
        Plus = 1,
        Both = 2,
    }
    public enum ORSide
    {
        None = 0,
        Right = 1,
        Left = 2,
        Both = 3,
    }

    //Menu
    public enum Menu
    {
        Reserve = 1,
        Appointment = 2,
        Operarion = 3,
        Report = 4,
        SetupUserID = 5,
        SetupPreset = 6,
    }
}