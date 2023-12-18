using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;
using System.Threading;
using System.Drawing;

namespace solution.PatientsSchedule
{
    public partial class Default : System.Web.UI.Page
    {

        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        //Thread.CurrentThread.CurrentCulture = new CultureInfo("th-TH");
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("th-TH");
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
            if (!Page.IsPostBack)
            {
                Main();
            }
        }
        public void Main() {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            //DateTime fromdate = new DateTime(2015, 5, 1);
            //DateTime todate = new DateTime(2015, 5, 3);
            //lblmonth.Text = "เดือน " + fromdate.ToString("MMMM yyyy");
            //hMonth.Value = fromdate.ToString("yyyy-MM-dd hh:mm:ss");
            //loadCalendar(fromdate);

            lblmonth.Text = "เดือน " + DateTime.Today.ToString("MMMM yyyy");
            hMonth.Value = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
            loadCalendar(DateTime.Today);
        }
        protected void btnPrev_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            DateTime localDate = Convert.ToDateTime(hMonth.Value);
            DateTime d2 = localDate.AddMonths(-1);
            lblmonth.Text = "เดือน " + d2.ToString("MMMM yyyy");
            hMonth.Value = d2.ToString("yyyy-MM-dd HH:mm:ss");
            loadCalendar(d2);
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            DateTime localDate = Convert.ToDateTime(hMonth.Value);
            DateTime d2 = localDate.AddMonths(1);
            lblmonth.Text = "เดือน " + d2.ToString("MMMM yyyy");
            hMonth.Value = d2.ToString("yyyy-MM-dd HH:mm:ss");
            loadCalendar(d2);

        }
        public void loadCalendar(DateTime d) {
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
            DateTime startDate = new DateTime(d.Year, d.Month, 1);
            DateTime endDate1 = new DateTime(d.Year, d.Month, 3);
            //DateTime endDate = startDate;
            DateTime endDate = startDate.AddMonths(1).AddDays(-1);
            VT_APPOINTMENTMASTERVO v = new VT_APPOINTMENTMASTERVO();
            v.AppointmentDateFrom = startDate;
            v.AppointmentDateTo = endDate;
            //v.AppointmentDateTo = endDate;
            List<VT_APPOINTMENTMASTERVO> lst = new BLVT_APPOINTMENTMASTER(dbInfo).SearchByKey(v);
            Session["lstAPPOINTMENTMASTERVO"] = lst;
            DataTable dt = new DataTable();
            dt.Columns.Add("อาทิตย์");
            dt.Columns.Add("จันทร์");
            dt.Columns.Add("อังคาร");
            dt.Columns.Add("พุธ");
            dt.Columns.Add("พฤหัสบดี");
            dt.Columns.Add("ศุกร์");
            dt.Columns.Add("เสาร์");
            DataRow dr = dt.NewRow();

            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                List<VT_APPOINTMENTMASTERVO> lst_doc = new List<VT_APPOINTMENTMASTERVO>();
                List<VT_APPOINTMENTMASTERVO> lst_doc1 = new List<VT_APPOINTMENTMASTERVO>(); // แพทย์ เวร
                List<VT_APPOINTMENTMASTERVO> lst_doc2 = new List<VT_APPOINTMENTMASTERVO>(); // แพทย์ เสริม
                foreach (VT_APPOINTMENTMASTERVO v1 in lst) {
                    if (v1.AppointmentDateTime.Value.ToString("yyyyMMdd") == date.ToString("yyyyMMdd")) {
                        lst_doc.Add(v1);
                        if (v1.DOCTORCODE != "")
                        {
                            lst_doc1.Add(v1);
                        }
                        else {
                            lst_doc2.Add(v1);
                        }
                    }

                }
                List<VT_APPOINTMENTMASTERVO> lstsort_doc1 = lst_doc1.OrderBy(o => o.Doctor).ToList();
                List<VT_APPOINTMENTMASTERVO> lstsort_doc2 = lst_doc2.OrderBy(o => o.Doctor).ToList();

                int i = (int)date.DayOfWeek;
                StringBuilder sb = new StringBuilder();
                if (lst_doc.Count > 0)
                {
                    sb.Append("<div data-toggle = 'modal' data-target = '#ModalDetail"+ date.Day + "' style='cursor: pointer;background:white'");
                    sb.Append(" onmouseover = 'this.style.background='gray';' onmouseout = 'this.style.background='white';'>");
                }
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("th-TH");
                DateTime localDate = Convert.ToDateTime(date.Date);

                sb.Append("<div style='text-align: center'>" + date.Day + "</div>");
                
                if (lst_doc.Count > 0)
                {
                    int cnt1 = lst_doc.Count(s => s.DOCTORCODE != "");
                    int cnt2 = lst_doc.Count(s => s.DOCTORCODE == "");

                    int iPatient = (17 - lst_doc.Count);
                    if (iPatient < 0)
                    { iPatient = iPatient * (-1); }

                    if ((17 - lst_doc.Count) == 0)
                    {
                        sb.Append("<div style='text-align: center;color: green'><b>17/" + (17 - lst_doc.Count) + " Full </b></div>");
                    }
                    else if ((17 - lst_doc.Count) < 1)
                    {
                        sb.Append("<div style='text-align: center;color: red'><b>17/+" + iPatient + " Over </b></div>");
                    }
                    else
                    {
                        sb.Append("<div style='text-align: center;color: Orange'><b>17/" + (17 - lst_doc.Count) + " Wait </b></div>");
                    }                    
                    sb.Append("<div style='text-align: center;color: #111315'>แพทย์เวร " + cnt1 + "</div>");
                    sb.Append("<div style='text-align: center;color: #111315'>แพทย์เสริม " + cnt2 + "</div>");
                    sb.Append("<div style='text-align: center;color: #111315'>Emergency - </div>");

                    sb.Append("<div class='modal fade bd-example-modal-lg' id='ModalDetail" + date.Day + "' tabindex='-1'>");
                    sb.Append("<div class='modal-lg modal-dialog modal-dialog-centered' style='max-width: 90vw'> <div class='modal-content'> <div class='modal-header'>");
                    sb.Append("<h5 class='modal-title'>"+ localDate.ToString("dddd d MMMM yyyy") + "</h5>");
                    
                    sb.Append("<button type = 'button' class='close' aria-label='Close'>");
                    sb.Append("<span aria-hidden='true'>&times;</span>");
                    sb.Append("</button>");
                    sb.Append("</div>");
                    sb.Append("<div class='modal-body' style='color: #111315'>");
                    sb.Append("<table class='table table-bordered'>");
                    sb.Append("<tr>");
                    int CountCol = 0;
                    string docid = "";
                    foreach (VT_APPOINTMENTMASTERVO col in lstsort_doc1)
                    {
                        if (docid == "")
                        {
                            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + col.DoctorName + " (เวรผ่าตัด)");
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc1)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    if (col_p.ORROOMNAME != "")
                                    { 
                                    sb.Append("<div style='background-color:#ffffff;border:0.8px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + " , Bed#" + col_p.ORROOM + "</div>");
                                    }
                                    else
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:0.8px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + "</div>");
                                    }
                                }
                            }
                            sb.Append("</th>");
                            CountCol++;
                        }
                        else if (docid != col.Doctor)
                        {
                            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + col.DoctorName + " (เวรผ่าตัด)");
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc1)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    if (col_p.ORROOMNAME != "")
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + " , Bed#" + col_p.ORROOM + "</div>");
                                    }
                                    else
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + "</div>");
                                    }
                                        
                                }
                            }
                            sb.Append("</th>");
                            CountCol++;
                        }
                        docid = col.Doctor;
                    }
                    docid = "";
                    foreach (VT_APPOINTMENTMASTERVO col in lstsort_doc2)
                    {
                        if (docid == "")
                        {
                            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + col.DoctorName + " (แพทย์เสริม)");
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc2)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    if (col_p.ORROOMNAME != "")
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + " , Bed#" + col_p.ORROOM + "</div>");
                                    }
                                    else
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + "</div>");
                                    }
                                }
                            }
                            sb.Append("</th>");
                            CountCol++;
                        }
                        else if (docid != col.Doctor)
                        {
                            sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + col.DoctorName + " (แพทย์เสริม)");
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc2)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    if (col_p.ORROOMNAME != "")
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + " , Bed#" + col_p.ORROOM + "</div>");
                                    }
                                    else
                                    {
                                        sb.Append("<div style='background-color:#ffffff;border:1px solid #ccc;font-size:0.8em;padding:4px;border-radius:4px;margin-bottom:2px;'> " + col_p.PatientName + " , " + col_p.HN + " , " + col_p.PRCEDURENAME + "</div>");
                                    }                                        
                                }
                            }
                            sb.Append("</th>");
                            CountCol++;
                        }
                        docid = col.Doctor;
                    }
                    sb.Append("</tr>");

                    int cunt_col_all = 0;
                    sb.Append("<tr>");
                    docid = "";
                    foreach (VT_APPOINTMENTMASTERVO col in lstsort_doc1)
                    {
                        if (docid == "")
                        {
                            sb.Append("<td>");
                            int cunt_col_p = 0;
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc1)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    cunt_col_p++;
                                }
                            }
                            sb.Append("<div> รวม " + cunt_col_p + "</div>");
                            sb.Append("</td>");
                            cunt_col_all = cunt_col_all + cunt_col_p;
                            CountCol++;
                        }
                        else if (docid != col.Doctor)
                        {
                            sb.Append("<td>");
                            int cunt_col_p = 0;
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc1)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    cunt_col_p++;
                                }
                            }
                            sb.Append("<div> รวม " + cunt_col_p + "</div>");
                            sb.Append("</td>");
                            cunt_col_all = cunt_col_all + cunt_col_p;
                            CountCol++;
                        }
                        docid = col.Doctor;
                    }
                    docid = "";
                    foreach (VT_APPOINTMENTMASTERVO col in lstsort_doc2)
                    {
                        if (docid == "")
                        {
                            sb.Append("<td>");
                            int cunt_col_p = 0;
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc2)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    cunt_col_p++;
                                }
                            }
                            sb.Append("<div> รวม " + cunt_col_p + "</div>");
                            sb.Append("</td>");
                            cunt_col_all = cunt_col_all + cunt_col_p;
                            CountCol++;
                        }
                        else if (docid != col.Doctor)
                        {
                            sb.Append("<td>");
                            int cunt_col_p = 0;
                            foreach (VT_APPOINTMENTMASTERVO col_p in lstsort_doc2)
                            {
                                if (col_p.Doctor == col.Doctor)
                                {
                                    cunt_col_p++;
                                }
                            }
                            sb.Append("<div> รวม " + cunt_col_p + "</div>");
                            sb.Append("</td>");
                            cunt_col_all = cunt_col_all + cunt_col_p;
                            CountCol++;
                        }
                        
                        docid = col.Doctor;
                    }
                    sb.Append("</tr>");

                    sb.Append("<tr>");
                    sb.Append("<td colspan='" + CountCol + "'>");
                    sb.Append("รวมสุทธิ "+ cunt_col_all);
                    sb.Append("</td>");
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                    sb.Append("</div>");
                }
                if (lst_doc.Count > 0)
                {
                    sb.Append("</div>");
                }
                dr[i] = sb;
                if (i == 6)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
                else if (date == endDate)
                {
                    dt.Rows.Add(dr);
                    dr = dt.NewRow();
                }
            }

            this.gvWeeklyCalender.DataSource = dt;
            this.gvWeeklyCalender.DataBind();  
            foreach (GridViewRow row in this.gvWeeklyCalender.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvWeeklyCalender, "Select$" + row.RowIndex);
                    //row.Attributes["style"] = "cursor:pointer";
                    //row.Attributes["data-toggle"] = "modal";
                    //row.Attributes["data-target"] = "#PatientDetail";
                }

                foreach (TableCell cell in row.Cells)
                {
                    if (cell.Text != "&nsbp;")
                    {
                        cell.Text = Server.HtmlDecode(cell.Text);
                    }
                }
                row.Cells[0].Width = 100;
                row.Cells[1].Width = 100;
                row.Cells[2].Width = 100;
                row.Cells[3].Width = 100;
                row.Cells[4].Width = 100;
                row.Cells[5].Width = 100;
                row.Cells[6].Width = 100;

                row.Cells[0].Height = 50;
                row.Cells[1].Height = 50;
                row.Cells[2].Height = 50;
                row.Cells[3].Height = 50;
                row.Cells[4].Height = 50;
                row.Cells[5].Height = 50;
                row.Cells[6].Height = 50;

                row.Cells[0].ForeColor = Color.Red;
                row.Cells[6].ForeColor = Color.Red;
            }            
        }
        protected void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            int index = gvWeeklyCalender.SelectedRow.RowIndex;
            string name = gvWeeklyCalender.SelectedRow.Cells[index].Text;
            //string country = gvWeeklyCalender.SelectedRow.Cells[1].Text;
            //string message = "Row Index: " + index + "\\nName: " + name + "\\nCountry: " + country;
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            loadPatient();
        }
        public void loadPatient()
        {
            List<VT_APPOINTMENTMASTERVO> lst = Session["lstAPPOINTMENTMASTERVO"] as List<VT_APPOINTMENTMASTERVO>;


            StringBuilder sb = new StringBuilder();
            ////Table start.
            //sb.Append("<table class='table table-bordered'>");

            ////Adding HeaderRow.
            //sb.Append("<tr>");
            //foreach (DataColumn column in dt.Columns)
            //{
            //    sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
            //}
            //sb.Append("</tr>");
            //int a = lstAll.Count / 7;
            //int b = lstAll.Count % 7;
            //if (b > 0)
            //{
            //    a = a + 1;
            //}
            //for (int i = 0; i < a; i++)
            //{
            //    sb.Append("<tr>");
            //    string su, mo, tu, we, th, fr, sa;
            //    su = lstSunday.Count > i ? lstSunday[i].ToString() : "";
            //    mo = lstMonday.Count > i ? lstMonday[i].ToString() : "";
            //    tu = lstTuesday.Count > i ? lstTuesday[i].ToString() : "";
            //    we = lstWednesday.Count > i ? lstWednesday[i].ToString() : "";
            //    th = lstThursday.Count > i ? lstThursday[i].ToString() : "";
            //    fr = lstFriday.Count > i ? lstFriday[i].ToString() : "";
            //    sa = lstSaturday.Count > i ? lstSaturday[i].ToString() : "";
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + su + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + mo + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + tu + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + we + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + tu + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + fr + "</td>");
            //    sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + sa + "</td>");
            //    sb.Append("</tr>");
            //}
            ////Table end.
            //sb.Append("</table>");
            //ltTable.Text = sb.ToString();
        }
    }
}