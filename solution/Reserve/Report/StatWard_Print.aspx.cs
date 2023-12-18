using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Print
{
    public partial class StatWard_Print : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvStatWard"] != null)
                {

                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    gvStatWard.DataSource = Session["gvStatWard"];

                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>)Session["gvStatWard"])
                    {
                        iSum += lst.QTY;
                    }

                    gvStatWard.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvStatWard.Columns[0].FooterStyle.Font.Bold = true;
                    gvStatWard.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvStatWard.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                    gvStatWard.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void gvStaWard_RowCreated(object sender, GridViewRowEventArgs e)
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