using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.AccessMenu
{
    public partial class ListAccessMenu : System.Web.UI.Page
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
            //_SETUPLOGONVO.AdminType = false;
            List<SETUPLOGONVO> lstSETUPACCESSMENUVO = new BLSETUPLOGON(dbInfo).SearchByKeyAccessMenuCode(_SETUPLOGONVO);

            gvSetupAccessMenu.DataSource = lstSETUPACCESSMENUVO;
            gvSetupAccessMenu.DataBind();


        }

        protected void gvSetupAccessMenu_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = gvSetupAccessMenu.Rows[e.NewEditIndex];
            string accessid = (row.FindControl("hdgvAccessID") as HiddenField).Value;
            Response.Redirect("/AccessMenu/EditAccessMenu/?id=" + accessid, false);
        }

        protected void gvSetupAccessMenu_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvSetupAccessMenu.Rows[e.RowIndex];
            ReturnValue rv = new BLSETUPLOGON(dbInfo).DeleteAccessMenuCode((row.FindControl("hdgvAccessID") as HiddenField).Value);
            if (!rv.Value)
            {
                divError.Visible = true;
                lblMessageError.Text = rv.Exception.Message;
            }
            else
            {
                Response.Redirect("/AccessMenu/ListAccessMenu", false);
            }
        }

        protected void gvSetupAccessMenu_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ChangePass")
            {
                int rowindex = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = gvSetupAccessMenu.Rows[rowindex];
                string userid = (row.FindControl("hdgvAccessID") as HiddenField).Value;
                Response.Redirect("/Auth/ChangePass/?id=" + userid, false);
            }
        }
    }
}