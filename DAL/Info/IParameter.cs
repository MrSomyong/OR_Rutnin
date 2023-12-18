using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    [Serializable]
    internal class IParameter
    {
        public IParameter() { }
        public IParameter(string ParamName, IDbType idbType, object Value)
        {
            this._paramname = ParamName;
            this._idbtype = idbType;
            this._value = Value;
        }
        //public IParameter(string ParamName, object Value, IDbType idbType)
        //{
        //    this._paramname = ParamName;
        //    this._value = Value;
        //    this._idbtype = idbType;
        //}
        public string ParamName
        {
            get { return _paramname; }
            set { _paramname = value; }
        }
        
        public IDbType IDbType
        {
            get { return _idbtype; }
            set { _idbtype = value; }
        }
        
        public object Value
        {
            get { return _value; }
            set { _value = value; }
        }
        string _paramname;
        IDbType _idbtype;
        object _value;
    }

    public enum IDbType
    {
        BigInt = 0,
        Binary = 1,
        Bit = 2,
        Char = 3,
        Date = 0x1f,
        DateTime = 4,
        DateTime2 = 0x21,
        DateTimeOffset = 0x22,
        Decimal = 5,
        Float = 6,
        Image = 7,
        Int = 8,
        Money = 9,
        NChar = 10,
        NText = 11,
        NVarChar = 12,
        Real = 13,
        SmallDateTime = 15,
        SmallInt = 0x10,
        SmallMoney = 0x11,
        Structured = 30,
        Text = 0x12,
        Time = 0x20,
        Timestamp = 0x13,
        TinyInt = 20,
        Udt = 0x1d,
        UniqueIdentifier = 14,
        VarBinary = 0x15,
        VarChar = 0x16,
        Variant = 0x17,
        Xml = 0x19
    }
}