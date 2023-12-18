using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class UserRoomType : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                MapDDL();
                LoadgvSetup();
                Clearvalue();                
            }
        }

        #region Method [...]

        private void Save()
        {
            if (!TestValue())
                return;
            else
                AlertMessage(false, false, string.Empty);

            SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
            SETUPUSERROOMTYPEVO.UserID = hdUserID.Value;
            SETUPUSERROOMTYPEVO.RoomType = ddlRoomType.SelectedValue;
            ReturnValue rtv = new BLSETUPUSERROOMTYPE(dbInfo).Insert(SETUPUSERROOMTYPEVO);
            if (rtv.Value)
            {
                LoadgvRoomType(hdUserID.Value);
            }
        }

        private bool TestValue()
        {
            bool xbool = true;
            string UserID = hdUserID.Value;
            string RoomType = ddlRoomType.SelectedValue;
            if (UserID == string.Empty || RoomType == string.Empty)
            {
                AlertMessage(true, true, "RoomType ห้ามว่าง!!");
                xbool = false;
            }
            else
            {
                string xError = string.Empty;
                SETUPUSERROOMTYPEVO _xSearch = new SETUPUSERROOMTYPEVO();
                _xSearch.UserID = UserID;
                _xSearch.RoomType = RoomType;
                List<SETUPUSERROOMTYPEVO> lstSETUPUSERROOMTYPEVO = new BLSETUPUSERROOMTYPE(dbInfo).CheckDup(_xSearch);
                if (lstSETUPUSERROOMTYPEVO.Count > 0)
                {
                    xError += "RoomType ห้ามซ้ำ\n";
                }

                if (xError != string.Empty)
                {
                    AlertMessage(true, true, xError);
                    xbool = false;
                }
            }
            return xbool;
        }

        private void AlertMessage(bool hidmsg, bool iserror, string msg)
        {
            if (iserror)
            {
                divError.Attributes["class"] = "alert alert-warning alert - dismissible fade show";
            }
            else
            {
                divError.Attributes["class"] = "alert alert-success alert - dismissible fade show";
            }
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }

        private void Delete(string RoomType)
        {
            SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
            SETUPUSERROOMTYPEVO.UserID = hdUserID.Value;
            SETUPUSERROOMTYPEVO.RoomType = RoomType;
            ReturnValue rtvsub = new BLSETUPUSERROOMTYPE(dbInfo).Delete(SETUPUSERROOMTYPEVO);
        }

        private void LoadgvSetup()
        {
            SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
           // _SETUPLOGONVO.AdminType = false;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(_SETUPLOGONVO);

            gvSetup.DataSource = lstSETUPLOGONVO;
            gvSetup.DataBind();
        }


        private void LoadgvRoomTypeEmty()
        {
            List<SETUPORGANSUBVO> lstSETUPORGANSUBVO = new List<SETUPORGANSUBVO>();
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            //SETUPORGANSUBVO.MainCode = txtMainCode.Text.Trim();
            lstSETUPORGANSUBVO.Add(SETUPORGANSUBVO);
            //gvORGANSub.DataSource = lstSETUPORGANSUBVO;
            //gvORGANSub.DataBind();
        }

        private void LoadgvRoomType(string UserID)
        {
            SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
            SETUPUSERROOMTYPEVO.UserID = UserID; 
            List<SETUPUSERROOMTYPEVO> lstSETUPUSERROOMTYPEVO = new BLSETUPUSERROOMTYPE(dbInfo).SearchByKey(SETUPUSERROOMTYPEVO);

            gvRoomType.DataSource = lstSETUPUSERROOMTYPEVO;
            gvRoomType.DataBind();
            Enable(true);

        }

        private void Clearvalue()
        {
            //Enable(false);
            ddlRoomType.ClearSelection();
            //hdUserID.Value = string.Empty;
        }

        private void Enable(bool rv)
        {
            if (rv == false)
            {
                btnAdd.Enabled = rv;
                btnAdd.CssClass = "btn btn-secondary btn-sm pull-right";
                btnClear.Enabled = rv;
                btnClear.CssClass = "btn btn-secondary btn-sm pull-right";
            }
            else
            {
                btnAdd.Enabled = rv;
                btnAdd.CssClass = "btn btn-info btn-sm pull-right";
                btnClear.Enabled = rv;
                btnClear.CssClass = "btn btn-warning btn-sm pull-right";
            }
            ddlRoomType.Enabled = rv;
        }

        private void MapDDL()
        {
            SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
            List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(SETUPORROOMTYPEVO);
            ddlRoomType.DataSource = lstSETUPORROOMTYPEVO;
            ddlRoomType.DataValueField = "ID";
            ddlRoomType.DataTextField = "Name";
            ddlRoomType.DataBind();
        }

        #endregion Method [...]

        #region Event [...]


        #region ButTon [...]

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clearvalue();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //saveMain();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //deleteMain();
            Clearvalue();
        }

        #endregion ButTon [...]

        #region GridView [...].

        protected void gvSetup_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);

            GridViewRow row = gvSetup.Rows[index];
            hdUserID.Value = ((HiddenField)row.FindControl("hdUserID")).Value;
            lblUserName.Text = ((Label)row.FindControl("lblUsername")).Text;

            LoadgvRoomType(hdUserID.Value);
        }

        protected void gvRoomType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvRoomType.Rows[rowIndex];

                HiddenField xRoomType = (row.FindControl("hdRoomType") as HiddenField);
                Delete(xRoomType.Value);
                LoadgvRoomType(hdUserID.Value);
            }
        }

        protected void gvRoomType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        #endregion GridView [...]

        #endregion Event [...]


    }
}