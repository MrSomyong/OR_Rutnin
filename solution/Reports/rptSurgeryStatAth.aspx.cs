using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class rptSurgeryStatAth : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            if (Session["USERID"] == null)
            {
                Response.Redirect("/Auth/Login");

                Response.End();

            }
            else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.ReadOnly).ToString())
            {
                Response.Redirect("/Reserve/Views");

                Response.End();
            }
            if (!IsPostBack)
            {
                if (Session["ORDate"] != null)
                {
                    hdDateFrom.Value = Session["ORDate"].ToString();
                    hdDateTo.Value = Session["ORDate"].ToString();
                }
                else
                {
                    hdDateFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }

                // Cxl Check Flag Status
                ddlCxlCheck.DataSource = EnumOR.GetAllName<EnumOR.CxlCheckFlag>();
                ddlCxlCheck.DataTextField = "Value";
                ddlCxlCheck.DataValueField = "Key";
                ddlCxlCheck.DataBind();
                ddlCxlCheck.SelectedIndex = (int)EnumOR.CxlCheckFlag.Without;

                // Cxl Check Flag Status
                ddlCxlConfirm.DataSource = EnumOR.GetAllName<EnumOR.CxlCheckFlag>();
                ddlCxlConfirm.DataTextField = "Value";
                ddlCxlConfirm.DataValueField = "Key";
                ddlCxlConfirm.DataBind();
                ddlCxlConfirm.SelectedIndex = (int)EnumOR.CxlCheckFlag.Without;

                loadvalue();
            }
        }
        private void loadvalue()
        {
            try
            {
                DateTime DateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                DateTime DateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                List<ORHEADERVO> lstORHEADERVO1 = new List<ORHEADERVO>();
                List<ORHEADERVO> templstORHEADERVO1 = new BLORHEADER(dbInfo).SearchrptSurgeryStatAth(DateFrom, DateTo, "1", ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO1)
                {
                    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                    List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                    if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                    {
                        _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                        if (_lstAPPOINTMENTVO.Count == 0)
                        {
                            APPOINTMENTVO.ConfirmStatusType = 6;
                            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                        }
                    }
                    else
                    {
                        APPOINTMENTVO.ConfirmStatusType = 0;
                        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    }
                    if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                    {
                        lstORHEADERVO1.Add(ORHEADERVO);
                    }
                }

                gvData1.DataSource = lstORHEADERVO1;
                gvData1.DataBind();

                List<ORHEADERVO> lstORHEADERVO2 = new List<ORHEADERVO>();
                List<ORHEADERVO> templstORHEADERVO2 = new BLORHEADER(dbInfo).SearchrptSurgeryStatAth(DateFrom, DateTo, "2", ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO2)
                {
                    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                    List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                    if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                    {
                        _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                        if (_lstAPPOINTMENTVO.Count == 0)
                        {
                            APPOINTMENTVO.ConfirmStatusType = 6;
                            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                        }
                    }
                    else
                    {
                        APPOINTMENTVO.ConfirmStatusType = 0;
                        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    }
                    if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                    {
                        lstORHEADERVO2.Add(ORHEADERVO);
                    }
                }
                gvData2.DataSource = lstORHEADERVO2;
                gvData2.DataBind();

                List<ORHEADERVO> lstORHEADERVO3 = new List<ORHEADERVO>();
                List<ORHEADERVO> templstORHEADERVO3 = new BLORHEADER(dbInfo).SearchrptSurgeryStatAth(DateFrom, DateTo, "3", ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO3)
                {
                    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                    List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                    if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                    {
                        _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                        if (_lstAPPOINTMENTVO.Count == 0)
                        {
                            APPOINTMENTVO.ConfirmStatusType = 6;
                            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                        }
                    }
                    else
                    {
                        APPOINTMENTVO.ConfirmStatusType = 0;
                        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    }
                    if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                    {
                        lstORHEADERVO3.Add(ORHEADERVO);
                    }
                }
                gvData3.DataSource = lstORHEADERVO3;
                gvData3.DataBind();

                Session["gvSurgeryStatAth1"] = lstORHEADERVO1;
                Session["gvSurgeryStatAth2"] = lstORHEADERVO2;
                Session["gvSurgeryStatAth3"] = lstORHEADERVO3;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reports/pSurgeryStatAth/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void btnExportExcelScrub_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานการผ่าตัดแยกประเภทเจ้าหน้าที่(Scrub).xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvData1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvData1.HeaderRow.Cells)
                {
                    cell.BackColor = gvData1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvData1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvData1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvData1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvData1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExportExcelCirculate_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานการผ่าตัดแยกประเภทเจ้าหน้าที่(Circulate).xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvData2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvData2.HeaderRow.Cells)
                {
                    cell.BackColor = gvData2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvData2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvData2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvData2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvData2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        protected void btnExportExcelAnesNurse_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานการผ่าตัดแยกประเภทเจ้าหน้าที่(AnesNurse).xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvData3.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvData3.HeaderRow.Cells)
                {
                    cell.BackColor = gvData3.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvData3.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvData3.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvData3.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvData3.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
    }
}