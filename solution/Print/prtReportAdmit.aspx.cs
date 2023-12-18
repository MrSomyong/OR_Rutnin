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
    public partial class prtReportAdmit : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        private DateTime Ordate;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["USERID"] == null)
                {
                    Response.Redirect("/Auth/Login");

                    Response.End();

                }
                if (Request.QueryString["d"] != null)
                {
                    if (!IsPostBack)
                    {
                        MapDll();
                    }
                    string _date = Request.QueryString["d"];
                    Ordate = DateTime.Parse(_date);
                    string roomid = Request.QueryString["r"];
                    //
                    ReportDocument crystalReport = new ReportDocument();
                    string pathorrom = Server.MapPath("/") + "/Report/Report/ORADMIT.rpt";
                    crystalReport.Load(pathorrom);

                    DataSet dsCustomers = GetData(roomid, Ordate);

                    string pathSchemaorrom = Server.MapPath("/") + "/Report/Schema/ORROOM.xsd";
                    dsCustomers.WriteXmlSchema(pathSchemaorrom);
                    crystalReport.SetDataSource(dsCustomers);
                    CRReportView.ReportSource = crystalReport;
                    //crystalReport.Close();
                    //crystalReport.Dispose();
                    //
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private void MapDll()
        {
            if (System.Drawing.Printing.PrinterSettings.InstalledPrinters.Count > 0)
            {
                foreach (String myPrinter in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    cboCurrentPrinters.Items.Add(myPrinter);
                }
                cboCurrentPrinters.SelectedIndex = 0;
            }

            //cboCurrentPrinters.DataSource = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            //cboCurrentPrinters.DataBind();

        }

        private DataSet GetData(string roomid, DateTime Ordate)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dtheader = new BLORHEADER(dbInfo).SearchByRoomDate_DS(roomid, Ordate);

                List<string> lst_ORID = new List<string>();
                List<string> lst_ORRoom = new List<string>();
                List<string> lst_Surgeon = new List<string>();
                List<string> lst_RoomType = new List<string>();
                List<string> lst_AnesthesiaType = new List<string>();

                foreach (DataRow dr in dtheader.Rows)
                {
                    lst_ORID.Add(dr["ORID"].ToString());
                    lst_ORRoom.Add(dr["ORRoom"].ToString());
                    lst_Surgeon.Add(dr["Surgeon1"].ToString());
                    lst_Surgeon.Add(dr["Surgeon2"].ToString());
                    lst_Surgeon.Add(dr["Surgeon3"].ToString());
                    lst_RoomType.Add(dr["RoomType"].ToString());
                    lst_AnesthesiaType.Add(dr["AnesthesiaType1"].ToString());
                    lst_AnesthesiaType.Add(dr["AnesthesiaType2"].ToString());

                }
                string[] ORIDs = lst_ORID.ToArray();
                string[] ORRooms = lst_ORRoom.ToArray();
                string[] Surgeons = lst_Surgeon.ToArray();
                string[] RoomTypes = lst_RoomType.ToArray();
                string[] AnesthesiaTypes = lst_AnesthesiaType.ToArray();

                DataTable dtOROPERATION = new BLOROPERATION(dbInfo).SearchByORID_DS(ORIDs);
                DataTable dtORRoom = new BLOPERATIONROOM(dbInfo).SearchByCode_DS(ORRooms);
                DataTable dtSurgeon = new BLDOCTORMASTER(dbInfo).SearchByCode_DS(Surgeons);
                DataTable dtRoomType = new BLROOMTYPE(dbInfo).SearchByCode_DS(RoomTypes);
                DataTable dtAnesthesiaTypes = new BLANESTHESIA(dbInfo).SearchByCode_DS(AnesthesiaTypes);
                DataTable dtORANESTHESIADOCTORSCHEDULE = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByDate_DS(Ordate);
                DataTable dtORANESTHESIANURSESCHEDULE = new BLORANESTHESIANURSESCHEDULE(dbInfo).SearchByDate_DS(Ordate);

                dtheader.TableName = "ORHEADER";
                dtOROPERATION.TableName = "OROPERATION";
                dtORRoom.TableName = "VT_OPERATIONROOM";
                dtSurgeon.TableName = "VT_DOCTORMASTER";
                dtRoomType.TableName = "VT_ROOMTYPE";
                dtAnesthesiaTypes.TableName = "VT_ANESTHESIA";
                dtORANESTHESIADOCTORSCHEDULE.TableName = "ORANESTHESIADOCTORSCHEDULE";
                dtORANESTHESIANURSESCHEDULE.TableName = "ORANESTHESIANURSESCHEDULE";

                ds.Tables.Add(dtheader);
                ds.Tables.Add(dtOROPERATION);
                ds.Tables.Add(dtSurgeon);
                ds.Tables.Add(dtRoomType);
                ds.Tables.Add(dtAnesthesiaTypes);
                ds.Tables.Add(dtORRoom);
                ds.Tables.Add(dtORANESTHESIADOCTORSCHEDULE);
                ds.Tables.Add(dtORANESTHESIANURSESCHEDULE);

                //Add Table REPORT HEADER
                DataTable sdt = new DataTable("REPORT_HEADER");
                sdt.Columns.Add("REPORTNAME", typeof(String));
                sdt.Columns.Add("ORDate", typeof(DateTime));

                DataRow row = sdt.NewRow();
                sdt.Rows.Add("ORROOM.rpt", DateTime.Now);

                ds.Tables.Add(sdt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        protected void pntPrint_Click(object sender, EventArgs e)
        {
            string _date = Request.QueryString["d"];
            Ordate = DateTime.Parse(_date);
            string roomid = Request.QueryString["r"];
            //
            ReportDocument crystalReport = new ReportDocument();
            string pathorrom = Server.MapPath("/") + "/Report/Report/ORADMIT.rpt";
            crystalReport.Load(pathorrom);

            DataSet dsCustomers = GetData(roomid, Ordate);

            string pathSchemaorrom = Server.MapPath("/") + "/Report/Schema/ORROOM.xsd";
            dsCustomers.WriteXmlSchema(pathSchemaorrom);
            crystalReport.SetDataSource(dsCustomers);
            crystalReport.PrintOptions.PrinterName = cboCurrentPrinters.SelectedValue;
            crystalReport.PrintToPrinter(1, false, 0, 0);
            crystalReport.Close();
            crystalReport.Dispose();
        }

    }
}