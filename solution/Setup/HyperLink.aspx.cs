using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Info;
using System.Data;

namespace solution.Setup
{
    public partial class HyperLink : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;


        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");


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
                LoadHyperLinkList();
                InitSetupHyperLink();
                
            }
        }
        

        private void LoadHyperLinkList()
        {
            try
            {
                List<SETUPHYPERLINK> lstHyperLink = new BLSETUPHYPERLINK(appConnDBInfo).SearchAll();
                gvSetupHyperLinkList.DataSource = lstHyperLink;
                gvSetupHyperLinkList.DataBind();
            }
            catch { }
        }
        private void InitSetupHyperLink()
        {

            foreach (GridViewRow rowx in gvSetupHyperLinkList.Rows)
            {

                    if (rowx.RowState == DataControlRowState.Alternate)
                    {
                        rowx.CssClass = gvSetupHyperLinkList.AlternatingRowStyle.CssClass;
                    }
                    else
                        rowx.CssClass = "";
            }

        }
        protected void gvSetupHyperLinkList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteLink")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupHyperLinkList.Rows[index];
                SETUPHYPERLINK _SETUPHYPERLINK = new SETUPHYPERLINK();
                _SETUPHYPERLINK.LinkCode = gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["LinkCode"].ToString();
                _SETUPHYPERLINK.LinkName = gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["LinkName"].ToString();
                ReturnValue rv = new BLSETUPHYPERLINK(appConnDBInfo).Delete(_SETUPHYPERLINK);

                if (rv.Value == true)
                {
                    InitSetupHyperLink();
                    LoadHyperLinkList();
                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           " $.notify('Deleted {0} : {1} successfully.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", _SETUPHYPERLINK.LinkCode, _SETUPHYPERLINK.LinkName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                              alertScript, true);

                }
                else
                {
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", rv.Exception);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);
                }
            }
            else if (e.CommandName == "EditLink")
            {
               
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupHyperLinkList.Rows[index];
                SETUPHYPERLINK _SETUPHYPERLINK = new SETUPHYPERLINK();
                _SETUPHYPERLINK.LinkCode = gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["LinkCode"].ToString();
                _SETUPHYPERLINK.LinkName = gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["LinkName"].ToString();
                _SETUPHYPERLINK.LinkURL = gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["LinkURL"].ToString();
                _SETUPHYPERLINK.IsShow = Convert.ToBoolean(gvSetupHyperLinkList.DataKeys[row.RowIndex].Values["IsShow"]);


                txtLinkCode.Text = _SETUPHYPERLINK.LinkCode;
                txtLinkName.Text = _SETUPHYPERLINK.LinkName;
                txtLinkURL.Text = _SETUPHYPERLINK.LinkURL;
                hdLinkCode.Value = _SETUPHYPERLINK.LinkCode;
                cbIsShow.Checked = _SETUPHYPERLINK.IsShow;
                htmlTitleHyperLink.InnerHtml = "Edit Hyperlink";
                btnUpdateLink.Text = "Update Link";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalHyperLink();", true);
                

            }
        }
      
        private bool CheckValidation()
       {
            bool valid = true;
            string message = string.Empty;
            string linkCode = txtLinkCode.Text.Trim();
            string linkName = txtLinkName.Text.Trim();
            string linkURL = txtLinkURL.Text.Trim();
            bool isShow = Convert.ToBoolean(cbIsShow.Checked);


            bool test = CheckURLValid(linkURL);

            if (linkCode == string.Empty || linkName == string.Empty || linkURL == string.Empty || !CheckURLValid(linkURL))
            {
                message = string.IsNullOrEmpty(linkCode) ? "   - Code " : "";
                message += string.IsNullOrEmpty(linkName) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Name " : "";
                message += string.IsNullOrEmpty(linkURL) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - URL " : "";
                message += !CheckURLValid(linkURL) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Invalid URL format" : "";
                message = "Please fill out this field\\n" + message;
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

                valid = false;
            }
            
            return valid;
       }

        private bool CheckURLValidx(string source)
        {
            Uri uriResult;
            return Uri.TryCreate(source, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;
        }


        public bool CheckURLValid(string webUrl)
        {
            if (webUrl == null) return false;
            return System.Text.RegularExpressions.Regex.IsMatch(webUrl, @"(http|https)://");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            InitSetupHyperLink();
        }

        protected void gvSetupHyperLinkList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                  
                    Label lblShow = (Label)e.Row.FindControl("lblShow");
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsShow")) == true)
                    {
                        lblShow.Text = "<i class='fa fa-eye fa-lg text-info'></i>";
                    }
                    else
                    {
                        lblShow.Text = "<i class='fa fa-eye-slash fa-lg text-danger'></i>";
                    }


                  

                }
            }

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                Button btnAddLink = e.Row.FindControl("btnAddLink") as Button;
                btnAddLink.Enabled = !(gvSetupHyperLinkList.Rows.Count >= 5);
            }

        }
        protected void btnAddLink_Click(object sender, EventArgs e)
        {
            txtLinkCode.Text = string.Empty;
            txtLinkName.Text = string.Empty;
            txtLinkURL.Text = string.Empty;
            cbIsShow.Checked = false;
            htmlTitleHyperLink.InnerHtml = "Add Hyperlink";
            btnUpdateLink.Text = "Add Link";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalHyperLink();", true);
        }
        protected void gvSetupHyperLinkList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < gvSetupHyperLinkList.Columns.Count - 1; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = gvSetupHyperLinkList.Columns.Count;
            }
        }

        protected void btnUpdateLink_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                try
                {

                    string linkCode = txtLinkCode.Text.Trim();
                    string linkName = txtLinkName.Text.Trim();
                    string linkURL = txtLinkURL.Text.Trim();
                    bool isShow = Convert.ToBoolean(cbIsShow.Checked);

                    SETUPHYPERLINK lnk = new SETUPHYPERLINK();
                    lnk.LinkCode = linkCode;
                    lnk.LinkName = linkName;
                    lnk.LinkURL = linkURL;
                    lnk.IsShow = isShow;
                    
                    if (!string.IsNullOrEmpty(hdLinkCode.Value))
                    {
                        if (lnk.LinkCode != hdLinkCode.Value)
                        {
                            ReturnValue dup = new BLSETUPHYPERLINK(appConnDBInfo).CheckDup(lnk);

                            if (dup.Value)
                            {
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Code is duplicated in data setup',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ");
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                    alertScript, true);
                            }
                            else
                            {
                                ReturnValue rv = new BLSETUPHYPERLINK(appConnDBInfo).Update(lnk, hdLinkCode.Value);
                                if (rv.Value)
                                {
                                    InitSetupHyperLink();
                                    LoadHyperLinkList();
                                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                              $('#modalHyperLink').modal('hide'); 
                                              $.notify('Update {0} : {1} completed.',
                                                    {{
                                                        className: 'success',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                              }});   ", lnk.LinkCode, lnk.LinkName);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                        alertScript, true);
                                }
                                else
                                {
                                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                                $.notify('{0}',
                                                    {{
                                                        className: 'error',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                              }});   ", rv.Exception);
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                        alertScript, true);
                                }
                            }

                        }
                        else {

                            ReturnValue rv = new BLSETUPHYPERLINK(appConnDBInfo).Update(lnk, hdLinkCode.Value);
                            if (rv.Value)
                            {
                                InitSetupHyperLink();
                                LoadHyperLinkList();
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                                $('#modalHyperLink').modal('hide'); 
                                                $.notify('Update {0} : {1} completed.',
                                                    {{
                                                        className: 'success',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                              }});   ", lnk.LinkCode, lnk.LinkName);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                    alertScript, true);
                            }
                            else
                            {
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                                $.notify('{0}',
                                                    {{
                                                        className: 'error',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                              }});   ", rv.Exception);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                    alertScript, true);
                            }

                        }




                            
                        }
                    else
                    {
                        ReturnValue dup = new BLSETUPHYPERLINK(appConnDBInfo).CheckDup(lnk);

                        if (!dup.Value)
                        {
                            ReturnValue rv = new BLSETUPHYPERLINK(appConnDBInfo).Insert(lnk);
                            if (rv.Value)
                            {
                                InitSetupHyperLink();
                                LoadHyperLinkList();
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $('#modalHyperLink').modal('hide'); 
                                            $.notify('Insert {0} : {1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", lnk.LinkCode, lnk.LinkName);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                    alertScript, true);

                            }
                            else
                            {
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", rv.Exception);
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                    alertScript, true);
                            }
                        }
                        else
                        {
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Code is duplicated in data setup',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                        }
                    }

                }
                catch { }
            }
        }

        private void setDefaultControlEdit()
        {
            //ddlOrderType.SelectedIndex = 0;
            //txtAmt.Text = string.Empty;
            //txtQty.Text = string.Empty;
            //txtChargeAMT.Text = string.Empty;
        }
    }
}