using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

using CrystalDecisions.Web;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.ReportSource;

using DAL;
namespace solution.Print
{
    public partial class prtReportStickerWard : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        private DateTime Ordate;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["d"] != null)
                {
                    string _date = Request.QueryString["d"];
                    Ordate = DateTime.Parse(_date);
                    //DateTime ordate = DateTime.Parse(_date);
                    //loadvalue(ordate);
                }

                ReportDocument crystalReport = new ReportDocument();
                string pathorrom = Server.MapPath("/") + "/Report/Report/ORStickerWard.rpt";
                crystalReport.Load(pathorrom);
                DataSet dsCustomers = GetData();
                string pathSchemaorrom = Server.MapPath("/") + "/Report/Schema/ORSTICKER.xsd";
                dsCustomers.WriteXmlSchema(pathSchemaorrom);
                crystalReport.SetDataSource(dsCustomers);
                CRReportView.ReportSource = crystalReport;

                //ORHEADERVO ORHEADERVO = new ORHEADERVO();
                //List<ORHEADERVO> lstORHEADERVO = new BLORHEADER(dbInfo).SearchByKey(ORHEADERVO);
                //crystalReport.SetDataSource(lstORHEADERVO);

            }
            catch (Exception ex)
            { throw ex; }
        }

        private DataSet GetDataReport(DateTime dt)
        {
            DataSet ds = new DataSet();
            DataSet dsheader = new BLORHEADER(dbInfo).SearchByORDate_DS(dt);
            //DataSet dsOperation = new BLOROPERATION(dbInfo).SearchByORID_DS(dt);

            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;

                    using (DataSet dsORData = new DataSet())
                    {
                        //Get OR HEADER
                        string strSQLComm = "SELECT	ORID" +
                                            ", HN" +
                                            ", PatientName" +
                                            ", PatientInfection" +
                                            ", PatientType1" +
                                            ", PatientType2" +
                                            ", PatientUP" +
                                            ", ORDate" +
                                            ", ORTime" +
                                            ", ORTimeFollow" +
                                            ", ORStatCase" +
                                            ", ORCase" +
                                            ", ORSpecificType" +
                                            ", ORStatus" +
                                            ", RoomType" +
                                            ", AdmitTimeType" +
                                            ", ORRoom" +
                                            ", AnesthesiaType1" +
                                            ", AnesthesiaType2" +
                                            ", AnesthesiaSign" +
                                            ", Surgeon1" +
                                            ", Surgeon2" +
                                            ", Surgeon3" +
                                            ", Remark" +
                                            ", CreateDate" +
                                            ", CreateBy" +
                                            ", UpdateDate" +
                                            ", UpdateBy " +
                                            "FROM ORHEADER " +
                                            "WHERE ORDate = '2017-11-20'";
                        //"WHERE ORDate = '" + Ordate + "'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORHEADER");

                        //Get OR OROPERATION
                        strSQLComm = "SELECT	ORID" +
                                        ",MainCode" +
                                        ",SubCode" +
                                        ",SubName" +
                                        ",SubMark" +
                                        ",Side " +
                                        "FROM dbo.OROPERATION";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "OROPERATION");

                        //Get OR VT_PATIENTMASTER
                        strSQLComm = "SELECT	HN" +
                                        ",BirthDateTime" +
                                        ",Sex" +
                                        ",Ref" +
                                        ",Nationality" +
                                        ",'' As Age " + //Age Get แล้วส่งเป็นจำนวนปีมาให้เลยนะครับ
                                        "FROM dbo.VT_PATIENTMASTER WHERE HN = 'XXX'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_PATIENTMASTER");

                        //Get OR VT_OPERATIONROOM
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_OPERATIONROOM";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_OPERATIONROOM");

                        //Get OR VT_DOCTORMASTER
                        strSQLComm = "SELECT DOCTOR,DoctorName FROM dbo.VT_DOCTORMASTER";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_DOCTORMASTER");

                        //Get OR VT_ROOMTYPE
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_ROOMTYPE";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_ROOMTYPE");

                        //Get OR VT_ANESTHESIA
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_ANESTHESIA";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_ANESTHESIA");

                        //Get OR SETUPOPERATIONMAIN
                        strSQLComm = "SELECT MainCode,Name FROM dbo.SETUPOPERATIONMAIN";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "SETUPOPERATIONMAIN");

                        //Get OR ORANESTHESIADOCTORSCHEDULE
                        strSQLComm = "SELECT A.Doctor,B.DoctorName,A.StartAnesthesiaDateTime,A.Reamrk FROM dbo.ORANESTHESIADOCTORSCHEDULE A " +
                                        "LEFT JOIN VT_DOCTORMASTER B ON(A.Doctor = B.DOCTOR) " +
                                        "WHERE A.StartAnesthesiaDateTime = '2017-11-16'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORANESTHESIADOCTORSCHEDULE");

                        //Get OR ORANESTHESIANURSESCHEDULE
                        strSQLComm = "SELECT A.NURSE,B.Name,A.StartAnesthesiaDateTime,A.Reamrk FROM dbo.ORANESTHESIANURSESCHEDULE A " +
                                        "LEFT JOIN VT_NURSEMASTER B ON(A.NURSE = B.Code) " +
                                        "WHERE A.StartAnesthesiaDateTime = '2017-11-16'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORANESTHESIANURSESCHEDULE");

                        //Add Table REPORT HEADER
                        DataTable sdt = new DataTable("REPORT_HEADER");
                        sdt.Columns.Add("REPORTNAME", typeof(String));
                        sdt.Columns.Add("ORDate", typeof(DateTime));

                        DataRow row = sdt.NewRow();
                        sdt.Rows.Add("ORROOM.rpt", DateTime.Now);

                        dsORData.Tables.Add(sdt);

                        return dsORData;
                    }
                }
            }
        }

        private DataSet GetData()
        {
            string conString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    cmd.Connection = con;
                    
                    using (DataSet dsORData = new DataSet())
                    {
                        //Get OR HEADER
                        string strSQLComm = "SELECT	ORID" +
                                            ", HN" +
                                            ", PatientName" +
                                            ", PatientInfection"+
                                            ", PatientType1"+
                                            ", PatientType2"+
                                            ", PatientUP" +
                                            ", ORDate" +
                                            ", ORTime" +
                                            ", ORTimeFollow" +
                                            ", ORStatCase" +
                                            ", ORCase" +
                                            ", ORSpecificType" +
                                            ", ORStatus" +
                                            ", RoomType" +
                                            ", AdmitTimeType" +
                                            ", ORRoom" +
                                            ", AnesthesiaType1" +
                                            ", AnesthesiaType2" +
                                            ", AnesthesiaSign" +
                                            ", Surgeon1" +
                                            ", Surgeon2" +
                                            ", Surgeon3" +
                                            ", Remark" +
                                            ", CreateDate" +
                                            ", CreateBy" +
                                            ", UpdateDate" +
                                            ", UpdateBy " +
                                            "FROM ORHEADER " +
                                            "WHERE ORDate = '2017-11-28'";
                        //"WHERE ORDate = '" + Ordate + "'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORHEADER");

                        //Get OR OROPERATION
                        strSQLComm =    "SELECT	ORID"+
		                                ",MainCode"+
		                                ",SubCode"+
                                        ",SubName" +
		                                ",SubMark"+
		                                ",Side "+
                                        "FROM dbo.OROPERATION";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "OROPERATION");

                        //Get OR VT_PATIENTMASTER
                        strSQLComm =    "SELECT	HN"+
		                                ",BirthDateTime"+
		                                ",Sex"+
		                                ",Ref"+
		                                ",Nationality"+
		                                ",'' As Age "+ //Age Get แล้วส่งเป็นจำนวนปีมาให้เลยนะครับ
                                        "FROM dbo.VT_PATIENTMASTER WHERE HN = 'XXX'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_PATIENTMASTER");

                        //Get OR VT_OPERATIONROOM
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_OPERATIONROOM";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_OPERATIONROOM");

                        //Get OR VT_DOCTORMASTER
                        strSQLComm = "SELECT DOCTOR,DoctorName FROM dbo.VT_DOCTORMASTER";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_DOCTORMASTER");

                        //Get OR VT_ROOMTYPE
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_ROOMTYPE";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_ROOMTYPE");

                        //Get OR VT_ANESTHESIA
                        strSQLComm = "SELECT CODE,NAME FROM dbo.VT_ANESTHESIA";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "VT_ANESTHESIA");

                        //Get OR SETUPOPERATIONMAIN
                        strSQLComm = "SELECT MainCode,Name FROM dbo.SETUPOPERATIONMAIN";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "SETUPOPERATIONMAIN");

                        //Get OR ORANESTHESIADOCTORSCHEDULE
                        strSQLComm =    "SELECT A.Doctor,B.DoctorName,A.StartAnesthesiaDateTime,A.Reamrk FROM dbo.ORANESTHESIADOCTORSCHEDULE A "+
                                        "LEFT JOIN VT_DOCTORMASTER B ON(A.Doctor = B.DOCTOR) "+
                                        "WHERE A.StartAnesthesiaDateTime = '2017-11-16'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORANESTHESIADOCTORSCHEDULE");

                        //Get OR ORANESTHESIANURSESCHEDULE
                        strSQLComm = "SELECT A.NURSE,B.Name,A.StartAnesthesiaDateTime,A.Reamrk FROM dbo.ORANESTHESIANURSESCHEDULE A " +
                                        "LEFT JOIN VT_NURSEMASTER B ON(A.NURSE = B.Code) " +
                                        "WHERE A.StartAnesthesiaDateTime = '2017-11-16'";
                        cmd = new SqlCommand(strSQLComm);
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dsORData, "ORANESTHESIANURSESCHEDULE");

                        //Add Table REPORT HEADER
                        DataTable sdt = new DataTable("REPORT_HEADER");
                        sdt.Columns.Add("REPORTNAME", typeof(String));
                        sdt.Columns.Add("ORDate", typeof(DateTime));

                        DataRow row = sdt.NewRow();
                        sdt.Rows.Add("ORROOM.rpt", DateTime.Now);

                        dsORData.Tables.Add(sdt);

                        return dsORData;
                    }
                }
            }
        }

    }


}