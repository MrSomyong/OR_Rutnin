using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Text;

/// <summary>
/// Summary description for DBConect
/// </summary>
public class DBConect
{
    string StrConn = "";
    private SqlCommand sqlComm;
    private SqlConnection conn;
    private static DBConect _dbConn;
    public DBConect()
    {
        try
        {
            StrConn = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            conn = new SqlConnection(StrConn);
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public DBConect(bool autoConnect)
    {
        try
        {
            StrConn = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            conn = new SqlConnection(StrConn);
            if (autoConnect)
            {
                if (conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
                conn.Open();
            }
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static DBConect getInstance(bool autoconnect)
    {
        if (_dbConn == null) _dbConn = new DBConect(autoconnect);
        return _dbConn;
    }
    public static DBConect getInstance()
    {
        if (_dbConn == null) _dbConn = new DBConect(false);
        return _dbConn;
    }

    public void openConnect()
    {
        try
        {
            StrConn = WebConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            conn = new SqlConnection(StrConn);
            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
            }
            conn.Open();
        }
        catch (SqlException e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    #region ReqStat [...]
    public int RequestRunning()
    {
        try
        {
            string sql = "SELECT RequestRunning FROM tblParameter";
            sqlComm = new SqlCommand(sql, conn);
            SqlDataReader dr = sqlComm.ExecuteReader();
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            //return ds;
            int i = 0;
            return i;
        }
        catch
        {
            // return null;
            int i = 0;
            return i;
        }
        finally
        {
            conn.Close();

        }
    }
    #endregion ReqStat [...]
    public DataSet GetStationary(string ProductNo)
    {
        try
        {
            string sql = "SELECT * FROM tblProduct,tblProductQuantity where tblProduct.ProductNo='" + ProductNo + "'" +
                "tblProductQuantity.ProductNo = '" + ProductNo + "';";
            sqlComm = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    public DataSet GetProduct()
    {
        try
        {
            string sql = "SELECT * FROM tblProduct";
            sqlComm = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    public DataSet GetReqProduct()
    {
        try
        {
            string sql = "SELECT * FROM RequestDetail";
            sqlComm = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    public DataSet GetApproveMaster()
    {
        try
        {
            string sql = "SELECT * FROM tblApproveMaster";
            sqlComm = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }
    public DataSet GetRequestMaster()
    {
        try
        {
            string sql = "SELECT * FROM RequestMaster";
            sqlComm = new SqlCommand(sql, conn);
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            conn.Close();
        }
    }

    public DataSet Get_WB_Specialty()
    {
        try
        {
            string sql = "select * from [wb_specialty]";
            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }
    public DataSet GetDoctorList(string WBSpecialty)
    {
        try
        {
            //string sql = "select * from [wb_person]";
            string sql = "select * from wb_person left outer join wb_specialty on wb_person.specialtyid = wb_specialty.specialtyid";
            if (string.IsNullOrEmpty(WBSpecialty) || !WBSpecialty.ToUpper().Equals("ALL"))
                sql += " where SpecialtyID like '" + WBSpecialty + "%'";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }
    public bool DeleteSpeciality(string specialityid)
    {
        bool retval = false;
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete wb_specialty");
            sb.Append(" where SpecialtyID = @SpecialtyID");

            SqlCommand comm = new SqlCommand(sb.ToString(), this.conn);
            comm.Parameters.Add(new SqlParameter("@SpecialtyID", specialityid));
            if (comm.ExecuteNonQuery() > 0)
                retval = true;
            else

                retval = false;
            comm.Cancel();
        }
        catch
        {
            return false;
        }
        return retval;
    }
    public bool DeleteDocter(string persionid)
    {
        bool retval = false;
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete wb_person");
            sb.Append(" where PersonID = @PersonID");

            SqlCommand comm = new SqlCommand(sb.ToString(), this.conn);
            comm.Parameters.Add(new SqlParameter("@PersonID", persionid));
            if (comm.ExecuteNonQuery() > 0)
                retval = true;
            else

                retval = false;
            comm.Cancel();
        }
        catch
        {
            return false;
        }
        return retval;
    }
    public bool UpdateDoctoer(string persionid)
    {
        bool retval = false;
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update wb_person set ");
            sb.Append(" where PersonID = @PersonID");

            SqlCommand comm = new SqlCommand(sb.ToString(), this.conn);
            comm.Parameters.Add(new SqlParameter("@PersionID", persionid));
            if (comm.ExecuteNonQuery() > 0)
                retval = true;
            else

                retval = false;
            comm.Cancel();
        }
        catch
        {
            return false;
        }
        return retval;
    }
    public DataSet GetDoctorList(string WBSpecialty, string docid)
    {
        try
        {
            string sql = "select * from [wb_person]";
            if (string.IsNullOrEmpty(WBSpecialty) || !WBSpecialty.ToUpper().Equals("ALL"))
                sql += " where SpecialtyID like '" + WBSpecialty + "%'";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }
    public DataSet GetDoctorDetail(string docid)
    {
        try
        {
            string sql = "select * from [wb_person]";

            sql += " where PersonID = '" + docid + "'";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }

    public DataSet GetDoctorDetailList(string WBSpecialty, string docid, System.Collections.Generic.List<int> datelist
        , int dayType, string sfirstname, string slastname)
    {
        try
        {
            string sql = "select * from wb_person";//,wd_specialty";// left outer join wb_person_diagtime on wb_person.personid = wb_person_diagtime.doctorid";
            sql += " where 1=1";
            //sql += " and wb_person.specialtyid = wb_specialty.specialtyid";
            if (!string.IsNullOrEmpty(WBSpecialty))
                sql += " and wb_person.specialtyid like '" + WBSpecialty + "%'";
            if (!string.IsNullOrEmpty(docid))
                sql += " and docid like '" + docid + "%'";
            if (datelist.Count > 0)
            {
                sql += " and WB_PERSON.PersonID in (select pd.DoctorID from WB_PERSON_DIAGTIME pd";
                sql += " where pd.DoctorID = WB_PERSON.PersonID";
                sql += " and pd.SuffixInfo in (";
                for (int i = 0; i < datelist.Count; i++)
                {
                    if (i > 0)
                        sql += ",";
                    sql += datelist[i];
                }
                sql += "))";
            }
            if (dayType > -1)
            {

            }
            if (!string.IsNullOrEmpty(sfirstname))
                sql += " and NameInfoTH1 like '" + sfirstname + "%'";
            if (!string.IsNullOrEmpty(slastname))
                sql += " and NameInfoTH1 like '%" + slastname + "%'";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }

    public DataSet GetPersonDiagTime(string docid)
    {
        try
        {
            string sql = "select * from wb_person_diagtime";//,wd_specialty";// left outer join wb_person_diagtime on wb_person.personid = wb_person_diagtime.doctorid";
            sql += " where 1=1";
            sql += " and DoctorID = '" + docid + "'";
            sql += " order by SuffixInfo";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }

    public void InsertAppointment(string strName,string persionid,string memo,DateTime appDateTime,string email,string telphone, out string strError)
    {
        strError = string.Empty;
        try
        {
            StringBuilder sbInsert = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();

            sbInsert.Append("insert into WB_APPOINTMENT(");
            sbValue.Append(" values(");

            sbInsert.Append("Subject");
            sbValue.Append("@Subject");

            sbInsert.Append(",PersonID");
            sbValue.Append(",@PersonID");
            
            sbInsert.Append(",Memo");
            sbValue.Append(",@Memo");
            
            sbInsert.Append(",AppointmentDateTime");
            sbValue.Append(",@AppointmentDateTime");
            
            sbInsert.Append(",Email");
            sbValue.Append(",@Email");

            sbInsert.Append(",TelephoneNO");
            sbValue.Append(",@TelephoneNO");

            sbInsert.Append(")");
            sbValue.Append(")");

            sbInsert.Append(sbValue.ToString());

            SqlCommand command = new SqlCommand(sbInsert.ToString(), this.conn);
            command.Parameters.Add(new SqlParameter("@Subject", strName));
            command.Parameters.Add(new SqlParameter("@PersonID", persionid));
            command.Parameters.Add(new SqlParameter("@Memo", memo));
            command.Parameters.Add(new SqlParameter("@AppointmentDateTime", appDateTime));
            command.Parameters.Add(new SqlParameter("@Email", email));
            command.Parameters.Add(new SqlParameter("@TelephoneNO", telphone));
            int effected = command.ExecuteNonQuery();
            command.Cancel();
        }
        catch (Exception ex)
        {
            strError = ex.Message;
        }
        finally
        {
            this.conn.Close();
        }
    }

    public DataSet GetSpecialty()
    {
        try
        {
            string sql = "select * from WB_SPECIALTY";//,wd_specialty";// left outer join wb_person_diagtime on wb_person.personid = wb_person_diagtime.doctorid";
            sql += " where 1=1";
            //sql += " and DoctorID = '" + docid + "'";
            sql += " order by SpecialtyID";

            DataSet ds = new DataSet();
            SqlDataAdapter adap = new SqlDataAdapter(sql, this.conn);
            adap.Fill(ds);
            return ds;
        }
        catch
        {
            return null;
        }
        finally
        {
            this.conn.Close();
        }
    }
    //var spersionid = this.txtSpecialityID.Text.Trim();
    //var spersionnameth = this.txtSpecialityNameTH.Text.Trim();
    //var spersionnameen = this.txtSpecialityNameEN.Text.Trim();
    //var spersionmemoth = this.txtMemoTH.Text.Trim();
    //var spersionmemoen = this.txtMemoEN.Text.Trim();
    //var spersionclinic = this.txtClinic1.Text.Trim();
    //var spersionclinic2 = this.txtClinic2.Text.Trim();
    public bool InsertPersion(string spersionid, string spersionnameth, string spersionnameen, string spersionmemoth, string spersionmemoen,
        string sex,string sSpecialty,string sPicturename, out string strError)
    {
        strError = string.Empty;
        try
        {
            StringBuilder sbInsert = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();

            sbInsert.Append("insert into WB_PERSON(");
            sbValue.Append(" values(");

            sbInsert.Append("PersonID");
            sbValue.Append("@PersonID");

            sbInsert.Append(",PersonType");
            sbValue.Append(",@PersonType");

            sbInsert.Append(",NameInfoTH1");
            sbValue.Append(",@NameInfoTH1");

            sbInsert.Append(",NameInfoEN1");
            sbValue.Append(",@NameInfoEN1");

            sbInsert.Append(",MemoInfoTH1");
            sbValue.Append(",@MemoInfoTH1");

            sbInsert.Append(",MemoInfoEN1");
            sbValue.Append(",@MemoInfoEN1");

            sbInsert.Append(",SpecialtyID");
            sbValue.Append(",@SpecialtyID");

            sbInsert.Append(",Creator");
            sbValue.Append(",@Creator");

            sbInsert.Append(",CreateDateTime");
            sbValue.Append(",@CreateDateTime");

            sbInsert.Append(",ImageFileNameTH1");
            sbValue.Append(",@ImageFileNameTH1"); 

            sbInsert.Append(",Sex");
            sbValue.Append(",@Sex");

            sbInsert.Append(")");
            sbValue.Append(")");

            sbInsert.Append(sbValue.ToString());

            SqlCommand command = new SqlCommand(sbInsert.ToString(), this.conn);
            command.Parameters.Add(new SqlParameter("@PersonID", spersionid));
            command.Parameters.Add(new SqlParameter("@PersonType", 2));
            command.Parameters.Add(new SqlParameter("@NameInfoTH1", spersionnameth));
            command.Parameters.Add(new SqlParameter("@NameInfoEN1", spersionnameen));
            command.Parameters.Add(new SqlParameter("@MemoInfoTH1", spersionmemoth));
            command.Parameters.Add(new SqlParameter("@MemoInfoEN1", spersionmemoen));
            command.Parameters.Add(new SqlParameter("@SpecialtyID", sSpecialty));
            command.Parameters.Add(new SqlParameter("@Creator", "admin"));
            command.Parameters.Add(new SqlParameter("@CreateDateTime", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@Sex", sex));
            command.Parameters.Add(new SqlParameter("@ImageFileNameTH1", sPicturename));
            
            int effected = command.ExecuteNonQuery();
            command.Cancel();
            return true;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return false;
        }
        finally
        {
            this.conn.Close();
        }

        return true;
    }
    public bool InsertSpecialty(string txtSpecialityID, string txtSpecialityNameTH, string txtSpecialityNameEN
        , string txtMemoTH, string txtMemoEN, string txtClinic1, string txtClinic2,string userentry,out string strError)
    {
        strError = string.Empty;
        try
        {
            StringBuilder sbInsert = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();

            sbInsert.Append("insert into WB_SPECIALTY(");
            sbValue.Append(" values(");

            sbInsert.Append("SpecialtyID");
            sbValue.Append("@SpecialtyID");

            sbInsert.Append(",NameInfoTH");
            sbValue.Append(",@NameInfoTH");

            sbInsert.Append(",NameInfoEN");
            sbValue.Append(",@NameInfoEN");

            sbInsert.Append(",MemoInfoTH");
            sbValue.Append(",@MemoInfoTH");

            sbInsert.Append(",MemoInfoEN");
            sbValue.Append(",@MemoInfoEN");

            sbInsert.Append(",ClinicID1");
            sbValue.Append(",@ClinicID1");

            sbInsert.Append(",ClinicID2");
            sbValue.Append(",@ClinicID2");

            sbInsert.Append(",EntryDateTime");
            sbValue.Append(",@EntryDateTime");

            sbInsert.Append(",EntryByUserID");
            sbValue.Append(",@EntryByUserID");

            sbInsert.Append(")");
            sbValue.Append(")");

            sbInsert.Append(sbValue.ToString());

            SqlCommand command = new SqlCommand(sbInsert.ToString(), this.conn);
            command.Parameters.Add(new SqlParameter("@SpecialtyID", txtSpecialityID));
            command.Parameters.Add(new SqlParameter("@NameInfoTH", txtSpecialityNameTH));
            command.Parameters.Add(new SqlParameter("@NameInfoEN", txtSpecialityNameEN));
            command.Parameters.Add(new SqlParameter("@MemoInfoTH", txtMemoTH));
            command.Parameters.Add(new SqlParameter("@MemoInfoEN", txtMemoEN));
            command.Parameters.Add(new SqlParameter("@ClinicID1", txtClinic1));
            command.Parameters.Add(new SqlParameter("@ClinicID2", txtClinic2));
            command.Parameters.Add(new SqlParameter("@EntryDateTime", DateTime.Now));
            command.Parameters.Add(new SqlParameter("@EntryByUserID", userentry));
            int effected = command.ExecuteNonQuery();
            command.Cancel();
            return true;
        }
        catch (Exception ex)
        {
            strError = ex.Message;
            return false;
        }
        finally
        {
            this.conn.Close();
        }
    }

    public bool UpdateSpeciality(string NameInfoTH, string NameInfoEN, string keyspecialityid)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("update wb_specialty set ");
            sb.Append(" nameinfoth = '" + NameInfoTH + "'");
            sb.Append(",nameinfoen = '" + NameInfoEN + "'");

            sb.Append("where specialtyid = '" + keyspecialityid + "'");
            sqlComm = new SqlCommand(sb.ToString(), this.conn);
            int effected= sqlComm.ExecuteNonQuery();
            sqlComm.Cancel();

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
        finally
        {
            this.conn.Close();
        }
    }
}