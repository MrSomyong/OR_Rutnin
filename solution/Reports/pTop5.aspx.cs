using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class pTop5 : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected double sumFooterValue = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvrptTop5"] != null)
                {

                    gvData.DataSource = Session["gvrptTop5"];
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
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
    }
}