using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class ORRoomType : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;
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
                LoadgvSetupEmpty();
                LoadValue();
            }
            divError.Visible = false;
        }

        private bool TestValue()
        {
            bool xbool = true;
            string CodeType = txtCodeType.Text.Trim();
            string Name = txtName.Text.Trim();
            //string ProcedureCode = txtProcedureCode.Text.Trim();
            if (CodeType == string.Empty || Name == string.Empty)
            {
                AlertMessage(true, true, "CodeType, Name ห้ามว่าง");
                xbool = false;
            }
            else
            {
                string xError = string.Empty;
                SETUPORROOMTYPEVO _xSearch = new SETUPORROOMTYPEVO();
                List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new List<SETUPORROOMTYPEVO>();
                if (hdID.Value == string.Empty) //New
                {
                    _xSearch = new SETUPORROOMTYPEVO();
                    _xSearch.CodeType = CodeType;
                    lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(_xSearch);
                    if (lstSETUPORROOMTYPEVO.Count > 0)
                    {
                        xError += "CodeType ห้ามซ้ำ\n";
                    }
                    _xSearch = new SETUPORROOMTYPEVO();
                    _xSearch.Name = Name;
                    lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(_xSearch);
                    if (lstSETUPORROOMTYPEVO.Count > 0)
                    {
                        xError += "Name ห้ามซ้ำ\n";
                    }
                    //_xSearch = new SETUPORROOMTYPEVO();
                    //_xSearch.ProcedureCode = ProcedureCode;
                    //lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(_xSearch);
                    //if (lstSETUPORROOMTYPEVO.Count > 0)
                    //{
                    //    xError += "ProcedureCode ห้ามซ้ำ";
                    //}
                }
                else
                {
                    //_xSearch = new SETUPORROOMTYPEVO();
                    //_xSearch.ID = hdID.Value;
                    //_xSearch.ProcedureCode = ProcedureCode;

                    //lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).CheckDupProcedureCode(_xSearch);
                    //if (lstSETUPORROOMTYPEVO.Count > 0)
                    //{
                    //    xError += "ProcedureCode ห้ามซ้ำ";
                    //}
                }

                if (xError != string.Empty)
                {
                    AlertMessage(true, true, xError);
                    xbool = false;
                }
            }
            return xbool;
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, true);
            //setdefaultvalue();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ORRoom/");
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!TestValue())
            {
                return;
            }
            ReturnValue rtv = SaveValue();
            if (rtv.Value)
            {
                LoadValue();
                AlertMessage(true, false, "Update Complete.");
                ClearLaout();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearLaout();
        }

        private void ClearLaout()
        {
            hdID.Value = string.Empty;
            txtCodeType.Text = string.Empty;
            txtCodeType.Enabled = true;
            txtName.Text = string.Empty;
            //txtProcedureCode.Text = string.Empty;
            foreach (GridViewRow r in gvSetup.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
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


        private void LoadValue()
        {
            SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
            List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(SETUPORROOMTYPEVO);
            gvSetup.DataSource = lstSETUPORROOMTYPEVO;
            gvSetup.DataBind();
        }

        private ReturnValue Save()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                if (!string.IsNullOrEmpty(txtCodeType.Text))
                {
                    SaveValue();
                    LoadValue();
                    ClearLaout();

                }

                LoadValue();
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue SaveValue()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                SETUPORROOMTYPEVO.ID = hdID.Value;
                SETUPORROOMTYPEVO.CodeType = txtCodeType.Text;
                SETUPORROOMTYPEVO.Name = txtName.Text;
                //SETUPORROOMTYPEVO.ProcedureCode = txtProcedureCode.Text;

                if (!string.IsNullOrEmpty(hdID.Value))
                {
                    rtv = new BLSETUPORROOMTYPE(dbInfo).Update(SETUPORROOMTYPEVO);
                }
                else
                {
                    SETUPORROOMTYPEVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLSETUPORROOMTYPE(dbInfo).Insert(SETUPORROOMTYPEVO);
                }
                hdID.Value = SETUPORROOMTYPEVO.ID;
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private void setbtnDisable()
        {
            //btnSave.Attributes["disabled"] = "disabled";
            //btnClear.Attributes["disabled"] = "disabled";

            //btnSave.CssClass = "btn btn-secondary pull-right";
            //btnClear.CssClass = "btn btn-secondary pull-right";
        }

        private void setbtnEnable()
        {
            //btnSave.Attributes.Remove("disabled");
            //btnClear.Attributes.Remove("disabled");

            //btnSave.CssClass = "btn btn-success pull-right";
            //btnClear.CssClass = "btn btn-info pull-right";
        }

        private void LoadgvSetupEmpty()
        {
            gvSetup.DataSource = new List<SETUPORROOMTYPEVO>();
            gvSetup.DataBind();
        }

        private void LoadLaout()
        {
            try
            {
                SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                SETUPORROOMTYPEVO.ID = hdID.Value;
                List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(SETUPORROOMTYPEVO);
                foreach (SETUPORROOMTYPEVO xx in lstSETUPORROOMTYPEVO)
                {
                    hdID.Value = xx.ID;
                    txtCodeType.Text = xx.CodeType;
                    txtName.Text = xx.Name;
                    //txtProcedureCode.Text = xx.ProcedureCode;
                    txtCodeType.Enabled = false;
                }
            }
            catch { }
        }

        protected void gvSetup_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvSetup.Rows[index];
                HiddenField ID = (HiddenField)row.FindControl("hdID");
                SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
                SETUPORROOMTYPEVO.ID = ID.Value;
                ReturnValue rv = new BLSETUPORROOMTYPE(dbInfo).Delete(SETUPORROOMTYPEVO);
                if (rv.Value)
                {
                    LoadValue();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvSetup_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                foreach (GridViewRow r in gvSetup.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvSetup.Rows[e.NewEditIndex];

                hdID.Value = ((HiddenField)row.FindControl("hdID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                LoadLaout();
            }
            catch { }
        }

        private List<SETUPORROOMTYPEVO> GetListValue_gvSetup()
        {
            List<SETUPORROOMTYPEVO> _lstor = new List<SETUPORROOMTYPEVO>();
            int i = 1;
            foreach (GridViewRow drow in gvSetup.Rows)
            {
                SETUPORROOMTYPEVO _or = new SETUPORROOMTYPEVO();
                _or.ID = hdID.Value;
                _or.CodeType = (drow.FindControl("lblCodeType") as Label).Text;
                _or.Name = (drow.FindControl("lblName") as Label).Text;
                _or.ProcedureCode = (drow.FindControl("lblProcedureCode") as Label).Text;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;
        }

    }
}