using System;
using System.Collections.Generic;
using System.Text;
using ADOUtils;
using System.Data;

namespace DAL
{
    class DAPOSTORIMPLANT : DataAccess
    {
        private static string _tblPOSTORIMPLANT = "POSTORIMPLANT";
        private static string _tblSETUPIMPLANTMAIN = "SETUPIMPLANTMAIN";
        private static string _tblSETUPIMPLANTSUB = "SETUPIMPLANTSUB";

        private static string _ID = "ID";
        private static string _PostOperation_ID = "PostOperation_ID";
        private static string _MainCode = "MainCode";
        private static string _Name = "Name";
        private static string _SubCode = "SubCode";
        private static string _SubName = "SubName";
        private static string _Seq = "Seq";
        private static string _Remark = "Remark";
        private static string _Img1 = "Img1";
        private static string _Img2 = "Img2";
        private static string _Img3 = "Img3";
        private static string _Img4 = "Img4";
        private static string _Img5 = "Img5";
        private static string _Used = "Used";

        System.Globalization.CultureInfo cultureinfo = new System.Globalization.CultureInfo("en-US");

        public DAPOSTORIMPLANT() { }
        public DAPOSTORIMPLANT(DatabaseInfo dbInfo) { this.DbInfo = dbInfo; }
        public DAPOSTORIMPLANT(System.Data.IDbTransaction useTransaction)
        {
            this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }
        public DAPOSTORIMPLANT(DatabaseInfo dbInfo, System.Data.IDbTransaction useTransaction)
        {
            this.DbInfo = dbInfo; this.useTransaction = true; this.transaction = useTransaction;
            this.connection = useTransaction.Connection;
        }

        internal List<POSTORIMPLANTVO> SearchByKey(POSTORIMPLANTVO POSTORIMPLANTVO)
        {
            List<POSTORIMPLANTVO> retValue = new List<POSTORIMPLANTVO>();
            try
            {
                StringBuilder strQuery = new StringBuilder();

                strQuery.Append(" select a." + _ID + ", a." + _PostOperation_ID + ", a." + _MainCode + ", a." + _SubCode + ", a." + _SubName
                                    + ",a." + _Img1 + ",a." + _Img2 + ",a." + _Img3 + ",a." + _Img4 + ",a." + _Img5 + ",a." + _Used + ",a." + _Remark
                                    + ", a." + _Seq + ", c." + _Name);
                strQuery.Append(" from " + _tblPOSTORIMPLANT + " as a");
                strQuery.Append(" left join " + _tblSETUPIMPLANTSUB + " as b on a." + _SubCode + " = b." + _SubCode);
                strQuery.Append(" left join " + _tblSETUPIMPLANTMAIN + " as c on a." + _MainCode + " = c." + _MainCode);
                strQuery.Append(" where 1=1");
                if (!string.IsNullOrEmpty(POSTORIMPLANTVO.ID))
                {
                    strQuery.Append(" and a." + _ID + " = @" + _ID);
                }
                if (!string.IsNullOrEmpty(POSTORIMPLANTVO.PostOperation_ID))
                {
                    strQuery.Append(" and a." + _PostOperation_ID + " = @" + _PostOperation_ID);
                }

                strQuery.Append(" Order By a." + _Seq);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(POSTORIMPLANTVO.ID)));
                parameter.Add(new IParameter(_PostOperation_ID, IDbType.VarChar, DBNullConvert.From(POSTORIMPLANTVO.PostOperation_ID)));
                command = GetCommand(strQuery.ToString(), parameter);
                query = GetExecuteReader(command);
                while (query.Read())
                {
                    POSTORIMPLANTVO _POSTORIMPLANTVO = new POSTORIMPLANTVO();
                    _POSTORIMPLANTVO.ID = query[_ID].ToString();
                    _POSTORIMPLANTVO.PostOperation_ID = query[_PostOperation_ID].ToString();
                    _POSTORIMPLANTVO.MainCode = query[_MainCode].ToString();
                    _POSTORIMPLANTVO.SubCode = query[_SubCode].ToString();
                    _POSTORIMPLANTVO.SubName = query[_SubName].ToString();
                    _POSTORIMPLANTVO.Name = query[_Name].ToString();
                    _POSTORIMPLANTVO.Seq = ADOUtil.GetIntFromQuery(query[_Seq].ToString());
                    if (query[_Img1] != DBNull.Value)
                    {
                        _POSTORIMPLANTVO.Img1 = (byte[])(query[_Img1]);
                    }
                    if (query[_Img2] != DBNull.Value)
                    {
                        _POSTORIMPLANTVO.Img2 = (byte[])(query[_Img2]);
                    }
                    if (query[_Img3] != DBNull.Value)
                    {
                        _POSTORIMPLANTVO.Img3 = (byte[])(query[_Img3]);
                    }
                    if (query[_Img4] != DBNull.Value)
                    {
                        _POSTORIMPLANTVO.Img4 = (byte[])(query[_Img4]);
                    }
                    if (query[_Img5] != DBNull.Value)
                    {
                        _POSTORIMPLANTVO.Img5 = (byte[])(query[_Img5]);
                    }
                    _POSTORIMPLANTVO.Used = query[_Used].ToString() == "True" ? true : false;
                    _POSTORIMPLANTVO.Remark = query[_Remark].ToString();
                    retValue.Add(_POSTORIMPLANTVO);
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

        internal ReturnValue Insert(POSTORIMPLANTVO _POSTORIMPLANTVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                #region SQL Insert
                StringBuilder sbInsert = new StringBuilder();
                StringBuilder sbValue = new StringBuilder();

                sbInsert.Append("INSERT INTO " + _tblPOSTORIMPLANT + " (");
                sbValue.Append(" VALUES(");

                sbInsert.Append(_ID);
                sbValue.Append("@" + _ID);

                sbInsert.Append("," + _PostOperation_ID);
                sbValue.Append(",@" + _PostOperation_ID);

                sbInsert.Append("," + _MainCode);
                sbValue.Append(",@" + _MainCode);

                sbInsert.Append("," + _SubCode);
                sbValue.Append(",@" + _SubCode);

                sbInsert.Append("," + _SubName);
                sbValue.Append(",@" + _SubName);

                sbInsert.Append("," + _Seq);
                sbValue.Append(",@" + _Seq);

                sbInsert.Append("," + _Remark);
                sbValue.Append(",@" + _Remark);

                sbInsert.Append(")");
                sbValue.Append(")");

                sbInsert.Append(sbValue.ToString());
                #endregion

                ConnectDB();

                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.ID)));
                parameter.Add(new IParameter(_PostOperation_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.PostOperation_ID)));
                parameter.Add(new IParameter(_MainCode, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.MainCode)));
                parameter.Add(new IParameter(_SubCode, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.SubCode)));
                parameter.Add(new IParameter(_SubName, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.SubName)));
                parameter.Add(new IParameter(_Seq, IDbType.Int, DBNullConvert.From(_POSTORIMPLANTVO.Seq, false)));
                parameter.Add(new IParameter(_Remark, IDbType.Text, DBNullConvert.From(_POSTORIMPLANTVO.Remark)));
                command = GetCommand(sbInsert.ToString(), parameter);

                effected = GetExecuteNonQuery(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }

            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue UpdateImage(POSTORIMPLANTVO _POSTORIMPLANTVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORIMPLANT + " SET ");
                sbQuery.Append("" + _ID + " = @" + _ID);
                if (_POSTORIMPLANTVO.Img1 != null || _POSTORIMPLANTVO.delimg1)
                    sbQuery.Append("," + _Img1 + " = @" + _Img1);

                if (_POSTORIMPLANTVO.Img2 != null || _POSTORIMPLANTVO.delimg2)
                    sbQuery.Append("," + _Img2 + " = @" + _Img2);

                if (_POSTORIMPLANTVO.Img3 != null || _POSTORIMPLANTVO.delimg3)
                    sbQuery.Append("," + _Img3 + " = @" + _Img3);

                if (_POSTORIMPLANTVO.Img4 != null || _POSTORIMPLANTVO.delimg4)
                    sbQuery.Append("," + _Img4 + " = @" + _Img4);

                if (_POSTORIMPLANTVO.Img5 != null || _POSTORIMPLANTVO.delimg5)
                    sbQuery.Append("," + _Img5 + " = @" + _Img5);

                sbQuery.Append(" where " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.ID)));

                if (_POSTORIMPLANTVO.delimg1)
                {
                    parameter.Add(new IParameter(_Img1, IDbType.Image, new byte[100]));
                }
                else if (_POSTORIMPLANTVO.Img1 != null)
                {
                    parameter.Add(new IParameter(_Img1, IDbType.Image, _POSTORIMPLANTVO.Img1));
                }

                if (_POSTORIMPLANTVO.delimg2)
                {
                    parameter.Add(new IParameter(_Img2, IDbType.Image, new byte[100]));
                }
                else if (_POSTORIMPLANTVO.Img2 != null)
                {
                    parameter.Add(new IParameter(_Img2, IDbType.Image, _POSTORIMPLANTVO.Img2));
                }

                if (_POSTORIMPLANTVO.delimg3)
                {
                    parameter.Add(new IParameter(_Img3, IDbType.Image, new byte[100]));
                }
                else if (_POSTORIMPLANTVO.Img3 != null)
                {
                    parameter.Add(new IParameter(_Img3, IDbType.Image, _POSTORIMPLANTVO.Img3));
                }

                if (_POSTORIMPLANTVO.delimg4)
                {
                    parameter.Add(new IParameter(_Img4, IDbType.Image, new byte[100]));
                }
                else if (_POSTORIMPLANTVO.Img4 != null)
                {
                    parameter.Add(new IParameter(_Img4, IDbType.Image, _POSTORIMPLANTVO.Img4));
                }

                if (_POSTORIMPLANTVO.delimg5)
                {
                    parameter.Add(new IParameter(_Img5, IDbType.Image, new byte[100]));
                }
                else if (_POSTORIMPLANTVO.Img5 != null)
                {
                    parameter.Add(new IParameter(_Img5, IDbType.Image, _POSTORIMPLANTVO.Img5));
                }

                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue UpdateUsed(POSTORIMPLANTVO _POSTORIMPLANTVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORIMPLANT + " SET ");
                sbQuery.Append("" + _Used + " = @" + _Used);
                sbQuery.Append(" where " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.ID)));
                parameter.Add(new IParameter(_Used, IDbType.Bit, DBNullConvert.From(_POSTORIMPLANTVO.Used)));

                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue UpdateRemark(POSTORIMPLANTVO _POSTORIMPLANTVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("UPDATE " + _tblPOSTORIMPLANT + " SET ");
                sbQuery.Append("" + _Remark + " = @" + _Remark);
                sbQuery.Append(" where " + _ID + " = @" + _ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.ID)));
                parameter.Add(new IParameter(_Remark, IDbType.Text, DBNullConvert.From(_POSTORIMPLANTVO.Remark)));

                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);

                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue Delete(string ID)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORIMPLANT);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);

                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(ID)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }

        internal ReturnValue Delete(POSTORIMPLANTVO _POSTORIMPLANTVO)
        {
            ReturnValue retVal = new ReturnValue();
            try
            {
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("DELETE FROM " + _tblPOSTORIMPLANT);
                sbQuery.Append(" WHERE " + _ID + " = @" + _ID);
                sbQuery.Append(" AND " + _PostOperation_ID + " = @" + _PostOperation_ID);
                ConnectDB();
                List<IParameter> parameter = new List<IParameter>();
                parameter.Add(new IParameter(_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.ID)));
                parameter.Add(new IParameter(_PostOperation_ID, IDbType.VarChar, DBNullConvert.From(_POSTORIMPLANTVO.PostOperation_ID)));
                command = GetCommand(sbQuery.ToString(), parameter);
                effected = GetExecuteNonQuery(command);
                retVal.Value = (effected > 0 ? true : false);
                command.Cancel();
                DisconnectDB();
            }
            catch (Exception exc)
            {
                retVal.Value = false;
                retVal.Exception = exc;
            }
            return retVal;
        }
    }
}
