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
    public partial class rptSurgeryProcedrue : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected double sumFooterValue = 0;
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

                List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
                List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchrptSurgeryProcedrue(DateFrom, DateTo, ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
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
                        lstORHEADERVO.Add(ORHEADERVO);
                    }
                }

                gvData.DataSource = lstORHEADERVO;
                gvData.DataBind();

                //gvData.FooterRow.Cells[3].Text = sumFooterValue.ToString("N2");

                Session["gvrptSurgeryProcedrue"] = lstORHEADERVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reports/pSurgeryProcedrue/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานรายละเอียดการผ่าตัดแยก Procedrue.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvData.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvData.HeaderRow.Cells)
                {
                    cell.BackColor = gvData.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvData.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvData.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvData.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvData.RenderControl(hw);

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