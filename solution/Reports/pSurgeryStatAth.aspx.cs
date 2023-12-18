using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class pSurgeryStatAth : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvSurgeryStatAth1"] != null)
                {

                    gvData1.DataSource = Session["gvSurgeryStatAth1"];
                    gvData1.DataBind();
                }
                if (Session["gvSurgeryStatAth2"] != null)
                {

                    gvData2.DataSource = Session["gvSurgeryStatAth2"];
                    gvData2.DataBind();
                }
                if (Session["gvSurgeryStatAth3"] != null)
                {

                    gvData3.DataSource = Session["gvSurgeryStatAth3"];
                    gvData3.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}