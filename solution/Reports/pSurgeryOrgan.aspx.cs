using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class pSurgeryOrgan : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["gvrptSurgeryOrgan"] != null)
                {

                    gvData.DataSource = Session["gvrptSurgeryOrgan"];
                    gvData.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}