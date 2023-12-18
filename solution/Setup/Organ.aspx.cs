using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class Organ : System.Web.UI.Page
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
                loadgvmain();
                Clearvalue();
            }
        }

        #region Method [...]

        private void saveMain()
        {
            SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
            SETUPORGANMAINVO.MainCode = txtMainCode.Text.Trim().ToUpper();
            SETUPORGANMAINVO.Name = txtName.Text.Trim();

            if (btnSave.CommandName == "new")
            {
                ReturnValue rtv = new BLSETUPORGANMAIN(dbInfo).Insert(SETUPORGANMAINVO);
                if (rtv.Value)
                {
                    loadgvmain();

                }
            }
            else if(btnSave.CommandName == "edit")
            {
                ReturnValue rtv = new BLSETUPORGANMAIN(dbInfo).Update(SETUPORGANMAINVO);
                if (rtv.Value)
                {
                    loadgvmain();

                }
            }
        }

        private void saveSub()
        {
            if (btnAdd.CommandName == "edit")
            {
                SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                SETUPORGANSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPORGANSUBVO.SubCode = hdSubCode.Value;
                SETUPORGANSUBVO.SubName = txtSubName.Text.Trim();
                ReturnValue rtvsub = new BLSETUPORGANSUB(dbInfo).Update(SETUPORGANSUBVO);
            }
            else
            {                

                SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                SETUPORGANSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPORGANSUBVO.SubCode = Guid.NewGuid().ToString();
                SETUPORGANSUBVO.SubName = txtSubName.Text.Trim();
                ReturnValue rtvsub = new BLSETUPORGANSUB(dbInfo).Insert(SETUPORGANSUBVO);
            }
            SETUPORGANSUBVO _SETUPORGANSUBVO = new SETUPORGANSUBVO();
            _SETUPORGANSUBVO.MainCode = txtMainCode.Text;


            ClearSubvalue();
            loadgvsub(_SETUPORGANSUBVO);
        }

        private void deleteMain()
        {
            ReturnValue rtv = new BLSETUPORGANMAIN(dbInfo).Delete(txtMainCode.Text);
            if (rtv.Value)
            {
                deleteSubbyMain();
            }
            Clearvalue();
            ClearSubvalue();
            loadgvmain();
        }

        private void deleteSubbyMain()
        {
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = txtMainCode.Text;
            SETUPORGANSUBVO.SubCode = hdSubCode.Value;
            ReturnValue rtvsub = new BLSETUPORGANSUB(dbInfo).DeleteByMain(SETUPORGANSUBVO);
        }

        private void deleteSub(string subcode)
        {
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = txtMainCode.Text;
            SETUPORGANSUBVO.SubCode = subcode;
            ReturnValue rtvsub = new BLSETUPORGANSUB(dbInfo).Delete(SETUPORGANSUBVO);
        }

        private void loadgvmain()
        {
            List<SETUPORGANMAINVO> lstSETUPORGANMAINVO = new BLSETUPORGANMAIN(dbInfo).SearchAll();

            gvORGAN.DataSource = lstSETUPORGANMAINVO;
            gvORGAN.DataBind();
        }

        private void loadgvsub(SETUPORGANSUBVO SETUPORGANSUBVO)
        {
            List<SETUPORGANSUBVO> lstSETUPORGANSUBVO = new BLSETUPORGANSUB(dbInfo).SearchByKey(SETUPORGANSUBVO);

            gvORGANSub.DataSource = lstSETUPORGANSUBVO;
            gvORGANSub.DataBind();
        }

        private void loadgvsubemty()
        {
            List<SETUPORGANSUBVO> lstSETUPORGANSUBVO = new List<SETUPORGANSUBVO>();
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = txtMainCode.Text.Trim();
            lstSETUPORGANSUBVO.Add(SETUPORGANSUBVO);
            gvORGANSub.DataSource = lstSETUPORGANSUBVO;
            gvORGANSub.DataBind();
        }

        private void loadMainvalue()
        {

            SETUPORGANMAINVO SETUPORGANMAINVO = new BLSETUPORGANMAIN(dbInfo).SearchByCode(txtMainCode.Text.Trim());
            if (!string.IsNullOrEmpty(SETUPORGANMAINVO.MainCode))
            {

                SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                SETUPORGANSUBVO.MainCode = SETUPORGANMAINVO.MainCode;
                loadgvsub(SETUPORGANSUBVO);
                btnSave.CommandName = "edit";
            }
            else
            {
                loadgvsubemty();
                btnSave.CommandName = "new";
            }

            txtName.Text = SETUPORGANMAINVO.Name;
            txtSubName.Text = SETUPORGANMAINVO.Name;
            Enable(true);

        }

        private void Clearvalue()
        {
            Enable(false);
            txtMainCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSubName.Text = string.Empty;
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = Guid.NewGuid().ToString();
            loadgvsub(SETUPORGANSUBVO);
        }

        private void ClearSubvalue()
        {
            btnAdd.CommandName = "new";
            txtSubName.Text = txtName.Text;
            hdSubCode.Value = string.Empty;

        }

        private void Enable(bool rv)
        {
            if (rv == false)
            {
                btnSave.Enabled = rv;
                btnSave.CssClass = "btn btn-secondary pull-right";
                btnClear.Enabled = rv;
                btnClear.CssClass = "btn btn-secondary pull-right";
                btnDelete.Enabled = rv;
                btnDelete.CssClass = "btn btn-secondary pull-right";

                btnAdd.Enabled = rv;
                btnAdd.CssClass = "btn btn-secondary btn-sm pull-right";
                btnClearSub.Enabled = rv;
                btnClearSub.CssClass = "btn btn-secondary btn-sm pull-right";
            }
            else
            {
                btnSave.Enabled = rv;
                btnSave.CssClass = "btn btn-info pull-right";
                btnClear.Enabled = rv;
                btnClear.CssClass = "btn btn-warning pull-right";
                btnDelete.Enabled = rv;
                btnDelete.CssClass = "btn btn-danger pull-right";

                btnAdd.Enabled = rv;
                btnAdd.CssClass = "btn btn-info btn-sm pull-right";
                btnClearSub.Enabled = rv;
                btnClearSub.CssClass = "btn btn-warning btn-sm pull-right";
            }
            txtSubName.Enabled = rv;
            txtName.Enabled = rv;
            txtMainCode.Enabled = !rv;
        }

        #endregion Method [...]

        #region Event [...]

        #region TextBox [...]

        protected void txtMainCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                loadMainvalue();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {
            txtSubName.Text = txtName.Text.Trim();
        }

        protected void txtHSubName_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion TextBox [...]

        #region ButTon [...]

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            saveSub();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clearvalue();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            saveMain();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            deleteMain();
            Clearvalue();
        }

        #endregion ButTon [...]

        #region GridView [...].

        protected void gvORGAN_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            txtMainCode.Text = gvORGAN.Rows[rowIndex].Cells[0].Text;

            loadMainvalue();
        }

        protected void gvORGANSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvORGANSub.Rows[rowIndex];

                hdSubCode.Value = (row.FindControl("hdgvSubCode") as HiddenField).Value;
                txtSubName.Text = (row.FindControl("lblgvSubName") as Label).Text;
                deleteSub(hdSubCode.Value);
                SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
                SETUPORGANSUBVO.MainCode = txtMainCode.Text;
                hdSubCode.Value = string.Empty;
                txtSubName.Text = txtName.Text;
                loadgvsub(SETUPORGANSUBVO);
            }
            else if (e.CommandName == "edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvORGANSub.Rows[rowIndex];

                hdSubCode.Value = (row.FindControl("hdgvSubCode") as HiddenField).Value;
                txtSubName.Text = (row.FindControl("lblgvSubName") as Label).Text;

                btnAdd.CommandName = "edit";
            }
        }

        protected void btnClearSub_Click(object sender, EventArgs e)
        {
            ClearSubvalue();
        }

        protected void gvORGANSub_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }


        #endregion GridView [...]

        #endregion Event [...]

        protected void gvORGANSub_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
    }
}