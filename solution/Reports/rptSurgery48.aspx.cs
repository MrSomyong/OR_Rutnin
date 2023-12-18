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
    public partial class rptSurgery48 : System.Web.UI.Page
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
                List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
                DateTime DateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                DateTime DateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchrptSurgery48(DateFrom, DateTo, ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue);
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

                    string r = string.Empty;
                    string l = string.Empty;
                    string b = string.Empty;
                    string None = string.Empty;
                    string NA = string.Empty;
                    List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                    foreach (OROPERATIONVO op1 in lstOROPERATIONVO)
                    {
                        if (op1.Side == (int)EnumOR.ORSide.RE)
                        {
                            if (op1.SubMark == "1")
                                r += " +" + op1.SubName;
                            else if (op1.SubMark == "2")
                                r += " +-" + op1.SubName;
                            else if (op1.SubMark == "3")
                                r += " /" + op1.SubName;
                            else
                            {
                                if (r == "")
                                    r += op1.SubName;
                                else
                                    r += "," + op1.SubName;
                            }
                        }
                        else if (op1.Side == (int)EnumOR.ORSide.LE)
                        {
                            if (op1.SubMark == "1")
                                l += " +" + op1.SubName;
                            else if (op1.SubMark == "2")
                                l += " +-" + op1.SubName;
                            else if (op1.SubMark == "3")
                                l += " /" + op1.SubName;
                            else
                            {
                                if (l == "")
                                    l += op1.SubName;
                                else
                                    l += "," + op1.SubName;
                            }
                        }
                        else if (op1.Side == (int)EnumOR.ORSide.BE)
                        {
                            if (op1.SubMark == "1")
                                b += " +" + op1.SubName;
                            else if (op1.SubMark == "2")
                                b += " +-" + op1.SubName;
                            else if (op1.SubMark == "3")
                                b += " /" + op1.SubName;
                            else
                            {
                                if (b == "")
                                    b += op1.SubName;
                                else
                                    b += "," + op1.SubName;
                            }
                        }
                        else if (op1.Side == (int)EnumOR.ORSide.None)
                        {
                            if (op1.SubMark == "1")
                                None += " +" + op1.SubName;
                            else if (op1.SubMark == "2")
                                None += " +-" + op1.SubName;
                            else if (op1.SubMark == "3")
                                None += " /" + op1.SubName;
                            else if (op1.SubMark == "4")
                                None += " /" + op1.SubName;
                            else if (op1.SubMark == "5")
                                None += " /" + op1.SubName;
                            else
                            {
                                if (None == "")
                                    None += op1.SubName;
                                else
                                    None += "," + op1.SubName;
                            }
                        }
                        else if (op1.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                        {
                            if (op1.SubMark == "1")
                                NA += " +" + op1.SubName;
                            else if (op1.SubMark == "2")
                                NA += " +-" + op1.SubName;
                            else if (op1.SubMark == "3")
                                NA += " /" + op1.SubName;
                            else if (op1.SubMark == "4")
                                NA += " /" + op1.SubName;
                            else if (op1.SubMark == "5")
                                NA += " /" + op1.SubName;
                            else
                            {
                                if (NA == "")
                                    NA += op1.SubName;
                                else
                                    NA += "," + op1.SubName;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(r))
                    {
                        r = r + " : <code>" + EnumOR.ORSide.RE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(l))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        l = _br + l + " : <code>" + EnumOR.ORSide.LE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(b))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        b = _br + b + " : <code>" + EnumOR.ORSide.BE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(None))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                            _br = "<br/>";
                        None = _br + None + " : <code>" + EnumOR.ORSide.None.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                            _br = "<br/>";
                        NA = _br + NA + " : <code>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</code>";
                    }
                    OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                    OROPERATIONVO.strSide = r + l + b + None + NA;
                    ORHEADERVO.OROPERATIONVO = OROPERATIONVO;

                    lstORHEADERVO.Add(ORHEADERVO);
                }
                }
                gvData.DataSource = lstORHEADERVO;
                gvData.DataBind();
                Session["gvrptSurgery48"] = lstORHEADERVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reports/pSurgery48/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }
        protected void gvData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string Amount = ((Label)e.Row.FindControl("lblAmount")).Text;
                double totalvalue = Convert.ToDouble(Amount);
                //e.Row.Cells[6].Text = totalvalue.ToString();
                sumFooterValue += totalvalue;
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Label lbl = (Label)e.Row.FindControl("lblTotalAmount");
                lbl.Text = sumFooterValue.ToString();
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานผ่าตัดซ้ำภายใน 48ชม.xls");
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