using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class Complication : System.Web.UI.Page
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
                loadgvComplicationEmpty();
                loadComplication();
            }

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = save();
            if (rtv.Value)
            {
                AlertMessage(true, false, "Update Complete.");
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, true);
            //setdefaultvalue();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PostOR/");
        }

        protected void btnAddComplication_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = saveComplication();
            if (rtv.Value)
            {
                loadComplication();
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutComplication();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }
        

        protected void btnClearComplication_Click(object sender, EventArgs e)
        {
            ClearLaoutComplication();
        }

        private void ClearLaoutComplication()
        {
            hdID.Value = string.Empty;
            txtComplicationHeader.Text = string.Empty;
            txtComplicationDetail.Text = string.Empty;
            foreach (GridViewRow r in gvPostORComplication.Rows)
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


        private void loadComplication()
        {
            SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
            List<SETUPCOMPLICATIONVO> lstSETUPCOMPLICATIONVO = new BLSETUPCOMPLICATION(dbInfo).SearchByKey(SETUPCOMPLICATIONVO);
            gvPostORComplication.DataSource = lstSETUPCOMPLICATIONVO;
            gvPostORComplication.DataBind();
        }

        private ReturnValue save()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                if (!string.IsNullOrEmpty(txtComplicationHeader.Text))
                {
                    saveComplication();
                    loadComplication();
                    ClearLaoutComplication();

                }

                loadComplication();
            }
            catch (Exception ex)
            {
                //divError.Visible = true;
                //lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue saveComplication()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                SETUPCOMPLICATIONVO.ID = hdID.Value;
                SETUPCOMPLICATIONVO.ComplicationHeader = txtComplicationHeader.Text;
                SETUPCOMPLICATIONVO.ComplicationDetail = txtComplicationDetail.Text;
                //List<POSTORCOMPLICATIONVO> _lstPOSTORCOMPLICATIONVO = new BLPOSTORCOMPLICATION(dbInfo).SearchByKey(POSTORCOMPLICATIONVO);
                if (!string.IsNullOrEmpty(hdID.Value))
                {
                    rtv = new BLSETUPCOMPLICATION(dbInfo).Update(SETUPCOMPLICATIONVO);
                    hdID.Value = SETUPCOMPLICATIONVO.ID;
                }
                else
                {
                    SETUPCOMPLICATIONVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLSETUPCOMPLICATION(dbInfo).Insert(SETUPCOMPLICATIONVO);
                    hdID.Value = SETUPCOMPLICATIONVO.ID;
                }

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

        private void loadgvComplicationEmpty()
        {
            gvPostORComplication.DataSource = new List<SETUPCOMPLICATIONVO>();
            gvPostORComplication.DataBind();
        }

        private void loadComplicationLaout()
        {
            try
            {
                SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                SETUPCOMPLICATIONVO.ID = hdID.Value;
                List<SETUPCOMPLICATIONVO> lstSETUPCOMPLICATIONVO = new BLSETUPCOMPLICATION(dbInfo).SearchByKey(SETUPCOMPLICATIONVO);
                foreach (SETUPCOMPLICATIONVO xx in lstSETUPCOMPLICATIONVO)
                {
                    txtComplicationHeader.Text = xx.ComplicationHeader;
                    txtComplicationDetail.Text = xx.ComplicationDetail;
                }
            }
            catch { }
        }

        protected void gvPostORComplication_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvPostORComplication.Rows[index];
                HiddenField hdgvID = (HiddenField)row.FindControl("hdgvID");
                ReturnValue rv = new BLSETUPCOMPLICATION(dbInfo).Delete(hdgvID.Value);
                if (rv.Value)
                {
                    loadComplication();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPostORComplication_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                foreach (GridViewRow r in gvPostORComplication.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvPostORComplication.Rows[e.NewEditIndex];

                hdID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadComplicationLaout();
            }
            catch { }
        }

        private List<SETUPCOMPLICATIONVO> GetListValue_gvPostORComplication()
        {
            List<SETUPCOMPLICATIONVO> _lstor = new List<SETUPCOMPLICATIONVO>();
            int i = 1;
            foreach (GridViewRow drow in gvPostORComplication.Rows)
            {
                SETUPCOMPLICATIONVO _or = new SETUPCOMPLICATIONVO();
                _or.ID = hdID.Value;
                _or.ComplicationHeader = (drow.FindControl("lblgvComplicationHeader") as Label).Text;
                _or.ComplicationDetail = (drow.FindControl("lblgvComplicationDetail") as Label).Text;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;
        }

    }
}