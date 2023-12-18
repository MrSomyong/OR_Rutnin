using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Auth
{

    public partial class ChangePass : System.Web.UI.Page
    {

        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            string strUser = Session["USERID"].ToString();
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

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    string userid = Request.QueryString["id"];
                    loadvalue(userid);
                }
                else
                {
                    loadvalue(strUser);
                }
            }
        }

        private void loadvalue(string userid)
        {
            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = userid;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(SETUPLOGONVO);
            txtName.Text = lstSETUPLOGONVO[0].FirstName + " " + lstSETUPLOGONVO[0].LastName;
            hdUserID.Value = lstSETUPLOGONVO[0].UserID;

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;

            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = hdUserID.Value;
            SETUPLOGONVO.Password = txtpassword.Text.Trim();


            ReturnValue rtv = new BLSETUPLOGON(dbInfo).ChangePassword(SETUPLOGONVO);

            if (rtv.Value)
            {
                AlertMessage(true, "Change Password Success.");
                divError.Attributes["class"] = "alert alert-success alert - dismissible fade show";

            }
            else
            {
                AlertMessage(true, rtv.Exception.Message);
            }

        }
        private void AlertMessage(bool hidmsg, string msg)
        {
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }
    }
}