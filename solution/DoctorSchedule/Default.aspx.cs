using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
using System.Text;
namespace solution.DoctorSchedule
{
    public partial class Default : System.Web.UI.Page
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
            if (!this.IsPostBack)
            {
                loadvalue();
            }
        }
        public void loadvalue() {
            List<V_DOCTOR_ORDAYWEEKVO> lst = new BLV_DOCTOR_ORDAYWEEK(dbInfo).SearchAll();
            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[7] {
                    new DataColumn("อาทิตย์", typeof(string)),
                    new DataColumn("จันทร์", typeof(string)),
                    new DataColumn("อังคาร",typeof(string)),
                    new DataColumn("พุธ",typeof(string)),
                    new DataColumn("พฤหัสบดี",typeof(string)),
                    new DataColumn("ศุกร์",typeof(string)),
                    new DataColumn("เสาร์",typeof(string))
            });
            List<string> lstSunday = new List<string>();
            List<string> lstMonday = new List<string>();
            List<string> lstTuesday = new List<string>();
            List<string> lstWednesday = new List<string>();
            List<string> lstThursday = new List<string>();
            List<string> lstFriday = new List<string>();
            List<string> lstSaturday = new List<string>();
            List<string> lstAll = new List<string>();
            foreach (V_DOCTOR_ORDAYWEEKVO v in lst)
            {
                if (!string.IsNullOrEmpty(v.DoctorName))
                {
                    if (v.ORDAYWEEK == "Sunday")
                    {
                        lstSunday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Monday")
                    {
                        lstMonday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Tuesday")
                    {
                        lstTuesday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Wednesday")
                    {
                        lstWednesday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Thursday")
                    {
                        lstThursday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Friday")
                    {
                        lstFriday.Add(v.DoctorName);
                    }
                    if (v.ORDAYWEEK == "Saturday")
                    {
                        lstSaturday.Add(v.DoctorName);
                    }
                    lstAll.Add(v.DoctorName);
                }
            }
            
            StringBuilder sb = new StringBuilder();
            //Table start.
            sb.Append("<table class='table table-bordered'>");

            //Adding HeaderRow.
            sb.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                sb.Append("<th style='background-color: #B8DBFD;border: 1px solid #ccc'>" + column.ColumnName + "</th>");
            }
            sb.Append("</tr>");
            int a = lstAll.Count / 7;
            int b = lstAll.Count % 7;
            if (b > 0) {
                a = a + 1;
            }
            if (lstSunday.Count > a)
            {
                a = lstSunday.Count;
            }
            if(lstMonday.Count > a)
            {
                a = lstMonday.Count;
            }
            if (lstTuesday.Count > a)
            {
                a = lstTuesday.Count;
            }
            if (lstWednesday.Count > a)
            {
                a = lstWednesday.Count;
            }
            if (lstThursday.Count > a)
            {
                a = lstThursday.Count;
            }
            if (lstFriday.Count > a)
            {
                a = lstFriday.Count;
            }
            if (lstSaturday.Count > a)
            {
                a = lstSaturday.Count;
            }
            for (int i = 0; i < a; i++) {
                sb.Append("<tr>");
                string su, mo, tu, we, th, fr, sa;
                su = lstSunday.Count > i ? lstSunday[i].ToString() : "";
                mo = lstMonday.Count > i ? lstMonday[i].ToString() : "";
                tu = lstTuesday.Count > i ? lstTuesday[i].ToString() : "";
                we = lstWednesday.Count > i ? lstWednesday[i].ToString() : "";
                th = lstThursday.Count > i ? lstThursday[i].ToString() : "";
                fr = lstFriday.Count > i ? lstFriday[i].ToString() : "";
                sa = lstSaturday.Count > i ? lstSaturday[i].ToString() : "";
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + su + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + mo + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + tu + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + we + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + th + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + fr + "</td>");
                sb.Append("<td style='width:100px;border: 1px solid #ccc'>" + sa + "</td>");
                sb.Append("</tr>");
            }
            //Table end.
            sb.Append("</table>");
            ltTable.Text = sb.ToString();
        }
    }
}