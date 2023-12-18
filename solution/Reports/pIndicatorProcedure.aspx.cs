using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class pIndicatorProcedure : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["gvrptIndicatorProcedure"] != null)
                {

                    gvData.DataSource = Session["gvrptIndicatorProcedure"];
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}