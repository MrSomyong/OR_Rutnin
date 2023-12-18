using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;
using System.Drawing;
using System.Web;

namespace DAL
{
    class DADOCUMENT_ITEM : DataAccess
    {
        private static string _tblDOCUMENT_ITEM= "DOCUMENT_ITEM";
        private static string _Documentdata = "Documentdata";
        private static string _Documentno = "Documentno";
        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");
        public DADOCUMENT_ITEM() { }
        public DADOCUMENT_ITEM(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DADOCUMENT_ITEM(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DADOCUMENT_ITEM(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<DOCUMENT_ITEMVO> SearchByKey(DOCUMENT_ITEMVO _DOCUMENT_ITEMVO)
        {
            List<DOCUMENT_ITEMVO> retValue = new List<DOCUMENT_ITEMVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select * from " + _tblDOCUMENT_ITEM + " where 1=1 ");
                if (!string.IsNullOrEmpty(_DOCUMENT_ITEMVO.Documentno))
                {
                    strQuery.Append(" and " + _Documentno + " = @" + _Documentno);
                }
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_Documentno, IDbType.VarChar, DBNullConvert.From(_DOCUMENT_ITEMVO.Documentno)));

                command = GetCommand(strQuery.ToString(),parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    DOCUMENT_ITEMVO DOCUMENT_ITEMVO = new DOCUMENT_ITEMVO();
                    DOCUMENT_ITEMVO.Documentno = query[_Documentno].ToString();
                    DOCUMENT_ITEMVO.Documentdata = query[_Documentdata].ToString();
                    retValue.Add(DOCUMENT_ITEMVO);
                }
                query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return retValue;
        }
        internal byte[] SearchByHN(string HN)
        { 
            byte[] img_p;
            List<DOCUMENT_ITEMVO> retValue = new List<DOCUMENT_ITEMVO>();
            try
            {

                StringBuilder strQuery = new StringBuilder();
                strQuery.Append(" select top 0 Documentdata from " + _tblDOCUMENT_ITEM + " where " + _Documentno + " = @HN");

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter("HN", IDbType.VarChar, DBNullConvert.From(HN)));

                command = GetCommand(strQuery.ToString(), parameter);
                //query = GetExecuteReader(command);
                img_p = (byte[])command.ExecuteScalar();
                //while (query.Read())
                //{
                //    DOCUMENT_ITEMVO DOCUMENT_ITEMVO = new DOCUMENT_ITEMVO();
                //    DOCUMENT_ITEMVO.Documentno = query[_Documentno].ToString();
                //    DOCUMENT_ITEMVO.Documentdata = query[_Documentdata].ToString();
                //    retValue.Add(DOCUMENT_ITEMVO);
                //}
                //query.Close();
                command.Dispose();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                throw exc;
            }
            return img_p;
        }

        internal Boolean SearchByURL(string strURL)
        {
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(strURL);
            webRequest.Method = "HEAD";
            webRequest.Timeout = 10;
            try
            {
                using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse())
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
