using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Auth
{
    public partial class UserList : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
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
            loadvalue();

        }

        private void loadvalue()
        {
            SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
            _SETUPLOGONVO.AdminType = false;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(_SETUPLOGONVO);

            gvSetupLogon.DataSource = lstSETUPLOGONVO;
            gvSetupLogon.DataBind();


        }

        protected void gvSetupLogon_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvSetupLogon.Rows[e.NewEditIndex];
            string userid = (row.FindControl("hdgvUserID") as HiddenField).Value;
            Response.Redirect("/Auth/Edit/?id=" + userid, false);
        }

        protected void gvSetupLogon_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvSetupLogon.Rows[e.RowIndex];
            ReturnValue rv = new BLSETUPLOGON(dbInfo).Delete((row.FindControl("hdgvUserID") as HiddenField).Value);
            if (!rv.Value)
            {
                divError.Visible = true;
                lblMessageError.Text = rv.Exception.Message;
            }
        }

        protected void gvSetupLogon_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChangePass")
            {
                int rowindex = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = gvSetupLogon.Rows[rowindex];
                string userid = (row.FindControl("hdgvUserID") as HiddenField).Value;
                Response.Redirect("/Auth/ChangePass/?id=" + userid, false);
            }
        }
    }
}