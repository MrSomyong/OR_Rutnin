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
    public partial class Anesthesia_Print : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                ORHEADERVO ORHEADERVO = new ORHEADERVO();

                if (Session["gvAnesthesia"] != null)
                {

                    gvAnesthesia.DataSource = Session["gvAnesthesia"];
                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>) Session["gvAnesthesia"])
                    {
                        iSum += lst.AnesthesiaTypeQTY;
                    }

                    gvAnesthesia.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvAnesthesia.Columns[0].FooterStyle.Font.Bold = true;
                    gvAnesthesia.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvAnesthesia.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;
                    gvAnesthesia.DataBind();
                }
                if (Session["gvAnesthesia1"] != null)
                {

                    gvAnesthesia1.DataSource = Session["gvAnesthesia1"];
                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>)Session["gvAnesthesia1"])
                    {
                        iSum += lst.AnesthesiaTypeQTY;
                    }

                    gvAnesthesia1.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvAnesthesia1.Columns[0].FooterStyle.Font.Bold = true;
                    gvAnesthesia1.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvAnesthesia1.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;
                    gvAnesthesia1.DataBind();
                }
                if (Session["gvAnesthesia2"] != null)
                {

                    gvAnesthesia2.DataSource = Session["gvAnesthesia2"];
                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>)Session["gvAnesthesia2"])
                    {
                        iSum += lst.AnesthesiaTypeQTY;
                    }

                    gvAnesthesia2.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvAnesthesia2.Columns[0].FooterStyle.Font.Bold = true;
                    gvAnesthesia2.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvAnesthesia2.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;
                    gvAnesthesia2.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void gvAnesthesia_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia.Columns.Count;
                }
            }
        }

        protected void gvAnesthesia1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia1.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia1.Columns.Count;
                }
            }
        }

        protected void gvAnesthesia2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia2.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia2.Columns.Count;
                }
            }
        }
    }
}