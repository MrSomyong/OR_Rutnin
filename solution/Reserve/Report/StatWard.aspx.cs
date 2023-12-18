using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Text;
using System.Data;

namespace solution.Reserve
{
    public partial class StatWard : System.Web.UI.Page
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
                if (Session["ORDateFrom"] != null)
                {
                    hdDateFrom.Value = Session["ORDateFrom"].ToString();
                    hdDateTo.Value = Session["ORDateTo"].ToString();
                }
                else
                {
                    hdDateFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }

                loadvalue();
            }
        }

        private void loadvalue()
        {
            try
            {
                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORDateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                ORHEADERVO.ORDateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));

                List<ORHEADERVO> lstORHEADERVO = new BLORHEADER(dbInfo).SearchStatWard(ORHEADERVO);
                gvStatWard.DataSource = lstORHEADERVO;

                int iSum = 0;
                foreach (ORHEADERVO lst in lstORHEADERVO)
                {
                    iSum += lst.QTY;
                }

                gvStatWard.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                gvStatWard.Columns[0].FooterStyle.Font.Bold = true;
                gvStatWard.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                gvStatWard.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                gvStatWard.DataBind();
                Session["gvStatWard"] = lstORHEADERVO;
                Session["ORDateFrom"] = hdDateFrom.Value;
                Session["ORDateTo"] = hdDateTo.Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reserve/Report/StatWard_Print/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void gvStatWard_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvStatWard.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvStatWard.Columns.Count;
                }
            }
        }
    }
}