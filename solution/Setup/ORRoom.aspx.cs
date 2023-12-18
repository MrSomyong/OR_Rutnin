using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class ORRoom : System.Web.UI.Page
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
                MapDDL();
                LoadValue();
            }
            divError.Visible = false;
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
            txtCODE.Text = string.Empty;
            txtCODE.Enabled = true;
            txtName.Text = string.Empty;
            ddlCodeType.ClearSelection();
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
            SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
            List<SETUPORROOMVO> lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
            gvSetup.DataSource = lstSETUPORROOMVO;
            gvSetup.DataBind();
        }

        private ReturnValue Save()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                rtv = SaveValue();
                LoadValue();
                ClearLaout();
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
                SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                SETUPORROOMVO.ID = hdID.Value;
                SETUPORROOMVO.CODE = txtCODE.Text;
                SETUPORROOMVO.Name = txtName.Text;
                SETUPORROOMVO.CodeType = ddlCodeType.SelectedValue;

                if (!string.IsNullOrEmpty(hdID.Value))
                {
                    rtv = new BLSETUPORROOM(dbInfo).Update(SETUPORROOMVO);
                }
                else
                {
                    SETUPORROOMVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLSETUPORROOM(dbInfo).Insert(SETUPORROOMVO);
                }
                hdID.Value = SETUPORROOMVO.ID;
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
            gvSetup.DataSource = new List<SETUPORROOMVO>();
            gvSetup.DataBind();
        }

        private void LoadLaout()
        {
            try
            {
                SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                SETUPORROOMVO.ID = hdID.Value;
                List<SETUPORROOMVO> lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                foreach (SETUPORROOMVO xx in lstSETUPORROOMVO)
                {
                    hdID.Value = xx.ID;
                    txtCODE.Text = xx.CODE;
                    txtName.Text = xx.Name;
                    ddlCodeType.SelectedValue = xx.CodeType;

                    txtCODE.Enabled = false;
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
                SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                SETUPORROOMVO.ID = ID.Value;
                ReturnValue rv = new BLSETUPORROOM(dbInfo).Delete(SETUPORROOMVO);
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

        private List<SETUPORROOMVO> GetListValue_gvSetup()
        {
            List<SETUPORROOMVO> _lstor = new List<SETUPORROOMVO>();
            int i = 1;
            foreach (GridViewRow drow in gvSetup.Rows)
            {
                SETUPORROOMVO _or = new SETUPORROOMVO();
                _or.ID = hdID.Value;
                _or.CODE = (drow.FindControl("lblCODE") as Label).Text;
                _or.Name = (drow.FindControl("lblName") as Label).Text;
                _or.CodeType = (drow.FindControl("hdCodeType") as HiddenField).Value;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;
        }

        private void MapDDL()
        {
            SETUPORROOMTYPEVO SETUPORROOMTYPEVO = new SETUPORROOMTYPEVO();
            // Room Type
            //ListItem litRoomType = new ListItem();
            //litRoomType.Text = "None";
            //litRoomType.Value = "";
            List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByKey(SETUPORROOMTYPEVO);
            ddlCodeType.DataSource = lstSETUPORROOMTYPEVO;
            ddlCodeType.DataValueField = "ID";
            ddlCodeType.DataTextField = "Name";
            ddlCodeType.DataBind();
            //ddlCodeType.Items.Insert(0, litRoomType);
        }

        private bool TestValue()
        {
            bool xbool = true;
            string CODE = txtCODE.Text.Trim();
            string Name = txtName.Text.Trim();
            string CodeType = ddlCodeType.SelectedValue;
            if (CodeType == string.Empty || Name == string.Empty || CodeType == string.Empty)
            {
                AlertMessage(true, true, "Code, Name, CodeType ห้ามว่าง!!");
                xbool = false;
            }
            else
            {
                string xError = string.Empty;
                SETUPORROOMVO _xSearch = new SETUPORROOMVO();
                List<SETUPORROOMVO> lstSETUPORROOMVO = new List<SETUPORROOMVO>();
                if (hdID.Value == string.Empty) //New
                {
                    _xSearch.CODE = CODE;
                    lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(_xSearch);
                    if (lstSETUPORROOMVO.Count > 0)
                    {
                        xError += "Code ห้ามซ้ำ\n";
                    }

                    _xSearch = new SETUPORROOMVO();
                    _xSearch.Name = Name;
                    lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(_xSearch);
                    if (lstSETUPORROOMVO.Count > 0)
                    {
                        xError += "Name ห้ามซ้ำ\n";
                    }
                }               

                if (xError != string.Empty)
                {
                    AlertMessage(true, true, xError);
                    xbool = false;
                }
            }
            return xbool;
        }
    }
}