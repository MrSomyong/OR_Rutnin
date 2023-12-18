using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for iDBNull
/// </summary>
public class iDBNull
{
	public iDBNull()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static T GetValue<T>(object obj)
    {
        if (obj == null) return (T)(object)null;
        else if (typeof(T).Equals(typeof(string))) return (T)(object)obj.ToString();
        else return (T)obj;

    }
}