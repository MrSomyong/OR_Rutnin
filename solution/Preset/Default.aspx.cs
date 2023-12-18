using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Preset
{
    public partial class Default : System.Web.UI.Page
    {

        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
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
                MapDDL();
                Clearvalue();
            }
        }

        #region Method [...]

        private void saveMain()
        {
            SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new SETUPOPERATIONMAINVO();
            SETUPOPERATIONMAINVO.MainCode = txtMainCode.Text.Trim().ToUpper();
            SETUPOPERATIONMAINVO.Name = txtName.Text.Trim();

            if (btnSave.CommandName == "new")
            {
                ReturnValue rtv = new BLSETUPOPERATIONMAIN(dbInfo).Insert(SETUPOPERATIONMAINVO);
                if (rtv.Value)
                {
                    loadgvmain();

                }
            }
            else if(btnSave.CommandName == "edit")
            {
                ReturnValue rtv = new BLSETUPOPERATIONMAIN(dbInfo).Update(SETUPOPERATIONMAINVO);
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
                SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPOPERATIONSUBVO.SubCode = hdSubCode.Value;
                SETUPOPERATIONSUBVO.SubName = txtSubName.Text.Trim();
                SETUPOPERATIONSUBVO.ICDCM = ddlicd.SelectedValue;
                SETUPOPERATIONSUBVO.ORProcedureType = int.Parse(ddlORProcedureType.SelectedValue);
                SETUPOPERATIONSUBVO.ORGANMAIN = ddlORGANMAIN.SelectedValue;
                SETUPOPERATIONSUBVO.ORGANSUB = ddlORGANSUB.SelectedValue;
                ReturnValue rtvsub = new BLSETUPOPERATIONSUB(dbInfo).Update(SETUPOPERATIONSUBVO);
            }
            else
            {                

                SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text.Trim().ToUpper();
                SETUPOPERATIONSUBVO.SubCode = Guid.NewGuid().ToString();
                SETUPOPERATIONSUBVO.SubName = txtSubName.Text.Trim();
                SETUPOPERATIONSUBVO.ICDCM = ddlicd.SelectedValue;
                SETUPOPERATIONSUBVO.ORProcedureType = int.Parse(ddlORProcedureType.SelectedValue);
                SETUPOPERATIONSUBVO.ORGANMAIN = ddlORGANMAIN.SelectedValue;
                SETUPOPERATIONSUBVO.ORGANSUB = ddlORGANSUB.SelectedValue;
                ReturnValue rtvsub = new BLSETUPOPERATIONSUB(dbInfo).Insert(SETUPOPERATIONSUBVO);
            }
            SETUPOPERATIONSUBVO _SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            _SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text;


            ClearSubvalue();
            loadgvsub(_SETUPOPERATIONSUBVO);
        }

        private void deleteMain()
        {
            ReturnValue rtv = new BLSETUPOPERATIONMAIN(dbInfo).Delete(txtMainCode.Text);
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
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text;
            SETUPOPERATIONSUBVO.SubCode = hdSubCode.Value;
            ReturnValue rtvsub = new BLSETUPOPERATIONSUB(dbInfo).DeleteByMain(SETUPOPERATIONSUBVO);
        }

        private void deleteSub(string subcode)
        {
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text;
            SETUPOPERATIONSUBVO.SubCode = subcode;
            ReturnValue rtvsub = new BLSETUPOPERATIONSUB(dbInfo).Delete(SETUPOPERATIONSUBVO);
        }

        private void loadgvmain()
        {
            List<SETUPOPERATIONMAINVO> lstSETUPOPERATIONMAINVO = new BLSETUPOPERATIONMAIN(dbInfo).SearchAll();

            gvOROperation.DataSource = lstSETUPOPERATIONMAINVO;
            gvOROperation.DataBind();
        }

        private void loadgvsub(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(SETUPOPERATIONSUBVO);

            gvOROperationSub.DataSource = lstSETUPOPERATIONSUBVO;
            gvOROperationSub.DataBind();
        }

        private void loadgvsubemty()
        {
            List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new List<SETUPOPERATIONSUBVO>();
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text.Trim();
            lstSETUPOPERATIONSUBVO.Add(SETUPOPERATIONSUBVO);
            gvOROperationSub.DataSource = lstSETUPOPERATIONSUBVO;
            gvOROperationSub.DataBind();
        }

        private void loadMainvalue()
        {

            SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new BLSETUPOPERATIONMAIN(dbInfo).SearchByCode(txtMainCode.Text.Trim());
            if (!string.IsNullOrEmpty(SETUPOPERATIONMAINVO.MainCode))
            {

                SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                SETUPOPERATIONSUBVO.MainCode = SETUPOPERATIONMAINVO.MainCode;
                loadgvsub(SETUPOPERATIONSUBVO);
                btnSave.CommandName = "edit";
            }
            else
            {
                loadgvsubemty();
                btnSave.CommandName = "new";
            }

            txtName.Text = SETUPOPERATIONMAINVO.Name;
            txtSubName.Text = SETUPOPERATIONMAINVO.Name;
            Enable(true);

        }

        private void Clearvalue()
        {
            Enable(false);
            txtMainCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSubName.Text = string.Empty;
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = Guid.NewGuid().ToString();
            loadgvsub(SETUPOPERATIONSUBVO);
            btnAdd.Text = "Add";
            btnAdd.CommandName = "Add";
        }

        private void ClearSubvalue()
        {
            btnAdd.CommandName = "new";
            txtSubName.Text = txtName.Text;
            hdSubCode.Value = string.Empty;
            ddlicd.SelectedValue = string.Empty;
            ddlORProcedureType.ClearSelection();
            ddlORGANMAIN.ClearSelection();
            ddlORGANSUB.ClearSelection();
            btnAdd.Text = "Add";
            btnAdd.CommandName = "Add";
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

        private void MapDDL()
        {
            //ListItem lsticd = new ListItem();
            //lsticd.Text = "None";
            //lsticd.Value = "";
            //SETUPICDCMVO icdvo = new SETUPICDCMVO();
            //List<SETUPICDCMVO> lstSETUPICDVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdvo);
            //ddlicd.DataSource = lstSETUPICDVO;
            //ddlicd.DataValueField = "Code";
            //ddlicd.DataTextField = "CodeName";
            //ddlicd.DataBind();
            //ddlicd.Items.Insert(0, lsticd);

            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            MapDDLICDCM1_Search(SETUPICDCMVO);

            // ORProcedureType
            ddlORProcedureType.DataSource = EnumOR.GetORProcedureType<EnumOR.ORProcedureType>();
            ddlORProcedureType.DataTextField = "Value";
            ddlORProcedureType.DataValueField = "Key";
            ddlORProcedureType.DataBind();

            SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
            List<SETUPORGANMAINVO> lstBLSETUPORGANMAINVO = new BLSETUPORGANMAIN(dbInfo).SearchByKey(SETUPORGANMAINVO);
            ddlORGANMAIN.DataSource = lstBLSETUPORGANMAINVO;
            ddlORGANMAIN.DataValueField = "MainCode";
            ddlORGANMAIN.DataTextField = "Name";
            ddlORGANMAIN.DataBind();
            ListItem litOrgan = new ListItem();
            litOrgan.Text = "None";
            litOrgan.Value = "";
            ddlORGANMAIN.Items.Insert(0, litOrgan);
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
            ClearSubvalue();
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

        protected void txtICDCM1Search_TextChanged(object sender, EventArgs e)
        {
            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM1Search.Text;
            SETUPICDCMVO.Name = txtICDCM1Search.Text;
            MapDDLICDCM1_Search(SETUPICDCMVO);
        }

        #endregion ButTon [...]

        #region GridView [...].

        protected void gvOROperation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            txtMainCode.Text = gvOROperation.Rows[rowIndex].Cells[0].Text;

            loadMainvalue();
        }

        protected void gvOROperationSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvOROperationSub.Rows[rowIndex];

                hdSubCode.Value = (row.FindControl("hdgvSubCode") as HiddenField).Value;
                txtSubName.Text = (row.FindControl("lblgvSubName") as Label).Text;
                deleteSub(hdSubCode.Value);
                SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                SETUPOPERATIONSUBVO.MainCode = txtMainCode.Text;
                hdSubCode.Value = string.Empty;
                txtSubName.Text = txtName.Text;
                loadgvsub(SETUPOPERATIONSUBVO);
            }
            else if (e.CommandName == "edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);

                GridViewRow row = gvOROperationSub.Rows[rowIndex];

                string MainCode = (row.FindControl("hdMainCode") as HiddenField).Value;
                string SubCode = (row.FindControl("hdgvSubCode") as HiddenField).Value;
                SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                SETUPOPERATIONSUBVO.MainCode = MainCode;
                SETUPOPERATIONSUBVO.SubCode = SubCode;

                List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(SETUPOPERATIONSUBVO);
                if (lstSETUPOPERATIONSUBVO.Count > 0)
                {
                    hdSubCode.Value = lstSETUPOPERATIONSUBVO[0].SubCode;
                    txtSubName.Text = lstSETUPOPERATIONSUBVO[0].SubName;
                    //if (!string.IsNullOrEmpty(lstSETUPOPERATIONSUBVO[0].ICDCM))
                    //{
                        try
                        {
                            ddlicd.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;
                        }
                        catch (Exception)
                        {
                            ddlicd.SelectedIndex = 0;
                        }
                        
                    //}                        
                    ddlORProcedureType.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORProcedureType.Value.ToString();
                    ddlORGANMAIN.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORGANMAIN;
                    MapDllORGANSUB(ddlORGANMAIN.SelectedValue);
                    ddlORGANSUB.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORGANSUB;
                }
                btnAdd.CommandName = "edit";
                btnAdd.Text = "Edit";
            }
        }

        protected void btnClearSub_Click(object sender, EventArgs e)
        {
            ClearSubvalue();
        }

        protected void gvOROperationSub_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }
        protected void gvOROperationSub_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        #endregion GridView [...]

        protected void ddlORGANMAIN_SelectedIndexChanged(object sender, EventArgs e)
        {
            MapDllORGANSUB(ddlORGANMAIN.SelectedValue);
        }

        private void MapDllORGANSUB(string MainCode) {
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = MainCode;
            List<SETUPORGANSUBVO> lstSETUPORGANSUBVO = new BLSETUPORGANSUB(dbInfo).SearchByKey(SETUPORGANSUBVO);
            ddlORGANSUB.DataSource = lstSETUPORGANSUBVO;
            ddlORGANSUB.DataValueField = "SubCode";
            ddlORGANSUB.DataTextField = "SubName";
            ddlORGANSUB.DataBind();
            ListItem litOrgan = new ListItem();
            litOrgan.Text = "None";
            litOrgan.Value = "";
            ddlORGANSUB.Items.Insert(0, litOrgan);
        }

        private void MapDDLICDCM1_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlicd.DataSource = lstSETUPICDCMVO;
                ddlicd.DataValueField = "Code";
                ddlicd.DataTextField = "CodeName";
                ddlicd.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "None";
                litICDCM.Value = "";
                ddlicd.Items.Insert(0, litICDCM);
                ddlicd.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        #endregion Event [...]
    }
}