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
    public partial class rptSurgeryOrgan : System.Web.UI.Page
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
                List<ORHEADERVO> lstORHEADERVO = new BLORHEADER(dbInfo).SearchrptSurgeryOrgan(DateFrom, DateTo, ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
                
                gvData.DataSource = lstORHEADERVO;
                gvData.DataBind();
                Session["gvrptSurgeryOrgan"] = lstORHEADERVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reports/pSurgeryOrgan/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string Amount = ((Label)e.Row.FindControl("lblAmount")).Text;
            //    double totalvalue = Convert.ToDouble(Amount);
            //    //e.Row.Cells[6].Text = totalvalue.ToString();
            //    sumFooterValue += totalvalue;
            //}

            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    Label lbl = (Label)e.Row.FindControl("lblTotalAmount");
            //    lbl.Text = sumFooterValue.ToString();
            //}
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานการผ่าตัดแยกตาม-Organ.xls");
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