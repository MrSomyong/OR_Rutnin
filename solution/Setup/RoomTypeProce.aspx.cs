using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class RoomTypeProce : System.Web.UI.Page
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
                AlertMessage(false, false, "");

            ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
            ROOMTYPEPROCEVO.RoomType = hdRoomType.Value;
            ROOMTYPEPROCEVO.ProcedureCode = txtProcedureCode.Text;
            ReturnValue rtv = new BLROOMTYPEPROCE(dbInfo).Insert(ROOMTYPEPROCEVO);
            if (rtv.Value)
            {
                LoadgvRoomType(hdRoomType.Value);
                txtProcedureCode.Text = string.Empty;
            }
        }

        private bool TestValue()
        {
            bool xbool = true;
            
            string RoomType = hdRoomType.Value;
            string ProcedureCode = txtProcedureCode.Text;
            if (RoomType == string.Empty || ProcedureCode == string.Empty)
            {
                AlertMessage(true, true, "ProcedureCode ห้ามว่าง!!");
                xbool = false;
            }
            else
            {
                string xError = string.Empty;
                ROOMTYPEPROCEVO _xSearch = new ROOMTYPEPROCEVO();
                _xSearch.RoomType = RoomType;
                _xSearch.ProcedureCode = ProcedureCode;
                List<ROOMTYPEPROCEVO> lstROOMTYPEPROCEVO = new BLROOMTYPEPROCE(dbInfo).CheckDup(_xSearch);
                if (lstROOMTYPEPROCEVO.Count > 0)
                {
                    xError += "ProcedureCode ห้ามซ้ำ\n";
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

        private void Delete(string ProcedureCode)
        {
            ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
            ROOMTYPEPROCEVO.RoomType = hdRoomType.Value;
            ROOMTYPEPROCEVO.ProcedureCode = ProcedureCode;
            ReturnValue rtvsub = new BLROOMTYPEPROCE(dbInfo).Delete(ROOMTYPEPROCEVO);
        }

        private void LoadgvSetup()
        {
            SETUPORROOMTYPEVO _SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
           // _SETUPLOGONVO.AdminType = false;
            List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(_SETUPORROOMTYPEVO);

            gvSetup.DataSource = lstSETUPORROOMTYPEVO;
            gvSetup.DataBind();
        }


        private void LoadgvRoomType(string RoomType)
        {
            ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
            ROOMTYPEPROCEVO.RoomType = RoomType; 
            List<ROOMTYPEPROCEVO> lstROOMTYPEPROCEVO = new BLROOMTYPEPROCE(dbInfo).SearchByKey(ROOMTYPEPROCEVO);

            gvRoomType.DataSource = lstROOMTYPEPROCEVO;
            gvRoomType.DataBind();
            Enable(true);
            Clearvalue();
        }

        private void Clearvalue()
        {
            txtProcedureCode.Text = string.Empty;
            //hdRoomType.Value = string.Empty;
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
            txtProcedureCode.Enabled = rv;
        }

        private void MapDDL()
        {
            //SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
            //List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(SETUPORROOMTYPEVO);
            //ddlRoomType.DataSource = lstSETUPORROOMTYPEVO;
            //ddlRoomType.DataValueField = "ID";
            //ddlRoomType.DataTextField = "Name";
            //ddlRoomType.DataBind();
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
            hdRoomType.Value = ((HiddenField)row.FindControl("hdRoomTypeID")).Value;
            lblRoomType.Text = ((Label)row.FindControl("lblRoomTypeName")).Text;

            LoadgvRoomType(hdRoomType.Value);
        }

        protected void gvRoomType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvRoomType.Rows[rowIndex];

                Label xProcedureCode = (row.FindControl("lblProcedureCode") as Label);
                Delete(xProcedureCode.Text);
                LoadgvRoomType(hdRoomType.Value);
            }
        }

        protected void gvRoomType_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        #endregion GridView [...]

        #endregion Event [...]


    }
}