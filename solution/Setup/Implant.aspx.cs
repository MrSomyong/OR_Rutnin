using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class Implant : System.Web.UI.Page
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
            SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO = new SETUPIMPLANTMAINVO();
            SETUPIMPLANTMAINVO.MainCode = txtMainCode.Text.Trim().ToUpper();
            SETUPIMPLANTMAINVO.Name = txtName.Text.Trim();

            if (btnSave.CommandName == "new")
            {
                ReturnValue rtv = new BLSETUPIMPLANTMAIN(dbInfo).Insert(SETUPIMPLANTMAINVO);
                if (rtv.Value)
                {
                    loadgvmain();

                }
            }
            else if(btnSave.CommandName == "edit")
            {
                ReturnValue rtv = new BLSETUPIMPLANTMAIN(dbInfo).Update(SETUPIMPLANTMAINVO);
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
                SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
                SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPIMPLANTSUBVO.SubCode = hdSubCode.Value;
                SETUPIMPLANTSUBVO.SubName = txtSubName.Text.Trim();
                ReturnValue rtvsub = new BLSETUPIMPLANTSUB(dbInfo).Update(SETUPIMPLANTSUBVO);
            }
            else
            {                

                SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
                SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPIMPLANTSUBVO.SubCode = Guid.NewGuid().ToString();
                SETUPIMPLANTSUBVO.SubName = txtSubName.Text.Trim();
                ReturnValue rtvsub = new BLSETUPIMPLANTSUB(dbInfo).Insert(SETUPIMPLANTSUBVO);
            }
            SETUPIMPLANTSUBVO _SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            _SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text;


            ClearSubvalue();
            loadgvsub(_SETUPIMPLANTSUBVO);
        }

        private void deleteMain()
        {
            ReturnValue rtv = new BLSETUPIMPLANTMAIN(dbInfo).Delete(txtMainCode.Text);
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
            SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text;
            SETUPIMPLANTSUBVO.SubCode = hdSubCode.Value;
            ReturnValue rtvsub = new BLSETUPIMPLANTSUB(dbInfo).DeleteByMain(SETUPIMPLANTSUBVO);
        }

        private void deleteSub(string subcode)
        {
            SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text;
            SETUPIMPLANTSUBVO.SubCode = subcode;
            ReturnValue rtvsub = new BLSETUPIMPLANTSUB(dbInfo).Delete(SETUPIMPLANTSUBVO);
        }

        private void loadgvmain()
        {
            List<SETUPIMPLANTMAINVO> lstSETUPIMPLANTMAINVO = new BLSETUPIMPLANTMAIN(dbInfo).SearchAll();

            gvImplant.DataSource = lstSETUPIMPLANTMAINVO;
            gvImplant.DataBind();
        }

        private void loadgvsub(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            List<SETUPIMPLANTSUBVO> lstSETUPIMPLANTSUBVO = new BLSETUPIMPLANTSUB(dbInfo).SearchByKey(SETUPIMPLANTSUBVO);

            gvImplantSub.DataSource = lstSETUPIMPLANTSUBVO;
            gvImplantSub.DataBind();
        }

        private void loadgvsubemty()
        {
            List<SETUPIMPLANTSUBVO> lstSETUPIMPLANTSUBVO = new List<SETUPIMPLANTSUBVO>();
            SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text.Trim();
            lstSETUPIMPLANTSUBVO.Add(SETUPIMPLANTSUBVO);
            gvImplantSub.DataSource = lstSETUPIMPLANTSUBVO;
            gvImplantSub.DataBind();
        }

        private void loadMainvalue()
        {

            SETUPIMPLANTMAINVO SETUPIMPLANTMAINVO = new BLSETUPIMPLANTMAIN(dbInfo).SearchByCode(txtMainCode.Text.Trim());
            if (!string.IsNullOrEmpty(SETUPIMPLANTMAINVO.MainCode))
            {

                SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
                SETUPIMPLANTSUBVO.MainCode = SETUPIMPLANTMAINVO.MainCode;
                loadgvsub(SETUPIMPLANTSUBVO);
                btnSave.CommandName = "edit";
            }
            else
            {
                loadgvsubemty();
                btnSave.CommandName = "new";
            }

            txtName.Text = SETUPIMPLANTMAINVO.Name;
            txtSubName.Text = SETUPIMPLANTMAINVO.Name;
            Enable(true);

        }

        private void Clearvalue()
        {
            Enable(false);
            txtMainCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSubName.Text = string.Empty;
            SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            SETUPIMPLANTSUBVO.MainCode = Guid.NewGuid().ToString();
            loadgvsub(SETUPIMPLANTSUBVO);
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

        protected void gvImplant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            txtMainCode.Text = gvImplant.Rows[rowIndex].Cells[0].Text;

            loadMainvalue();
        }

        protected void gvImplantSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvImplantSub.Rows[rowIndex];

                hdSubCode.Value = (row.FindControl("hdgvSubCode") as HiddenField).Value;
                txtSubName.Text = (row.FindControl("lblgvSubName") as Label).Text;
                deleteSub(hdSubCode.Value);
                SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
                SETUPIMPLANTSUBVO.MainCode = txtMainCode.Text;
                hdSubCode.Value = string.Empty;
                txtSubName.Text = txtName.Text;
                loadgvsub(SETUPIMPLANTSUBVO);
            }
        }

        protected void btnClearSub_Click(object sender, EventArgs e)
        {
            ClearSubvalue();
        }

        protected void gvImplantSub_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        #endregion GridView [...]

        #endregion Event [...]


    }
}