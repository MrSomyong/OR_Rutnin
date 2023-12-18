using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class pSurgery : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvrptSurgery"] != null)
                {

                    gvData.DataSource = Session["gvrptSurgery"];
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}