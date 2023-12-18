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

namespace solution.Print
{
    public partial class O_Print : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvOperation"] != null)
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    gvOperation.DataSource = Session["gvOperation"];                                   

                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>)Session["gvOperation"])
                    {
                        iSum += lst.OROPERATIONVO.QTY;
                    }

                    gvOperation.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvOperation.Columns[0].FooterStyle.Font.Bold = true;
                    gvOperation.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvOperation.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                    gvOperation.DataBind();

                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        protected void gvOperation_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvOperation.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvOperation.Columns.Count;
                }
            }
        }
    }
}