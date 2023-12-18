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
    public partial class Computer : System.Web.UI.Page
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
                LoadComputerList();
                LoadStore();
                LoadClinic();
                InitSetupComputer();
                LoadTreatmentMethod();
            }
        }

        private void LoadStore()
        {
            try
            {
                ListItem litStore = new ListItem();
                litStore.Text = "None";
                litStore.Value = "";
                VT_STORE VT_STORE = new VT_STORE();

                List<VT_STORE> lstStore = new BLVT_STORE(extConnDBInfo).SearchAll();
                ddlDefaultStore.DataSource = lstStore;
                ddlDefaultStore.DataValueField = "StoreCode";
                ddlDefaultStore.DataTextField = "StoreName";
                ddlDefaultStore.DataBind();
                ddlDefaultStore.Items.Insert(0, litStore);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadClinic()
        {
            try
            {
                ListItem litClinic = new ListItem();
                litClinic.Text = "None";
                litClinic.Value = "";
                VT_CLINIC VT_STORE = new VT_CLINIC();

                List<VT_CLINIC> lstClinic = new BLVT_CLINIC(extConnDBInfo).SearchAll();
                ddlDefaultClinic.DataSource = lstClinic;
                ddlDefaultClinic.DataValueField = "CLINIC_CODE";
                ddlDefaultClinic.DataTextField = "CLINIC_NAME_ENG";
                ddlDefaultClinic.DataBind();
                ddlDefaultClinic.Items.Insert(0, litClinic);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadComputerList()
        {
            try
            {
                List<SETUPCOMPUTER> lstComputer = new BLSETUPCOMPUTER(appConnDBInfo , extConnDBInfo).SearchAll();
                gvSetupComputerList.DataSource = lstComputer;
                gvSetupComputerList.DataBind();

            }
            catch { }
        }
        private void InitSetupComputer()
        {
            hfComputerCode.Value = string.Empty;
            btnClear.Visible = true;
            txtComputerCode.Enabled = true;
            txtComputerCode.Text = string.Empty;
            txtComputerName.Text = string.Empty;
            ddlDefaultStore.ClearSelection();
            ddlDefaultClinic.ClearSelection();
            gvTreatmentList.DataSource = new List<SETUPCOMPUTERMETHOD>();
            gvTreatmentList.DataBind();

            foreach (GridViewRow rowx in gvSetupComputerList.Rows)
            {

                    if (rowx.RowState == DataControlRowState.Alternate)
                    {
                        rowx.CssClass = gvSetupComputerList.AlternatingRowStyle.CssClass;
                    }
                    else
                        rowx.CssClass = "";
            }

        }
        protected void gvSetupComputerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteComputer")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupComputerList.Rows[index];
                SETUPCOMPUTER _SETUPCOMPUTER = new SETUPCOMPUTER();
                _SETUPCOMPUTER.ComputerCode = gvSetupComputerList.DataKeys[row.RowIndex].Values["ComputerCode"].ToString();
                _SETUPCOMPUTER.ComputerName = gvSetupComputerList.DataKeys[row.RowIndex].Values["ComputerName"].ToString();
                ReturnValue rv = new BLSETUPCOMPUTER(appConnDBInfo).Delete(_SETUPCOMPUTER);

                if (rv.Value == true)
                {
                    InitSetupComputer();
                    LoadComputerList();
                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           " $.notify('Deleted {0} : {1} successfully.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", _SETUPCOMPUTER.ComputerCode, _SETUPCOMPUTER.ComputerName);
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
            else if (e.CommandName == "EditComputer")
            {
               
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupComputerList.Rows[index];
                SETUPCOMPUTER _SETUPCOMPUTER = new SETUPCOMPUTER();
                _SETUPCOMPUTER.ComputerCode = gvSetupComputerList.DataKeys[row.RowIndex].Values["ComputerCode"].ToString();
                _SETUPCOMPUTER.ComputerName = gvSetupComputerList.DataKeys[row.RowIndex].Values["ComputerName"].ToString();
                _SETUPCOMPUTER.DefaultStoreCode = gvSetupComputerList.DataKeys[row.RowIndex].Values["DefaultStoreCode"].ToString();
                _SETUPCOMPUTER.DefaultClinicCode = gvSetupComputerList.DataKeys[row.RowIndex].Values["DefaultClinicCode"].ToString();


                hfComputerCode.Value = _SETUPCOMPUTER.ComputerCode;
                txtComputerCode.Enabled = false;
                txtComputerCode.Text = _SETUPCOMPUTER.ComputerCode;
                txtComputerName.Text = _SETUPCOMPUTER.ComputerName;
                ddlDefaultStore.ClearSelection();
                ddlDefaultStore.Items.FindByValue(_SETUPCOMPUTER.DefaultStoreCode).Selected = true;
                hfClinic.Value = _SETUPCOMPUTER.DefaultClinicCode;
                //ddlDefaultClinic.ClearSelection();
                //ddlDefaultClinic.Items.FindByValue(_SETUPCOMPUTER.DefaultClinicCode).Selected = true;

                foreach (GridViewRow rowx in gvSetupComputerList.Rows)
                {
                    if (rowx.RowIndex == row.RowIndex)
                    {

                        if (rowx.RowState == DataControlRowState.Selected)
                        {
                            rowx.CssClass = "selected";

                        }
                        else
                            rowx.CssClass = gvSetupComputerList.AlternatingRowStyle.CssClass.ToString() + " selected";
                    }
                    else
                    {

                        if (rowx.RowState == DataControlRowState.Alternate)
                        {
                            rowx.CssClass = gvSetupComputerList.AlternatingRowStyle.CssClass;
                        }
                        else
                            rowx.CssClass = "";
                    }
                }

                LoadTreatmentMethodList(_SETUPCOMPUTER.ComputerCode);
                string strClinicCode = "'" + _SETUPCOMPUTER.DefaultClinicCode.Replace(",", "','") + "'";
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlTreatmentMethod']"").val(null).trigger('change');
                                            $(""[id*='ddlTreatmentMethod']"").prop('disabled', false);
                                            $(""[id*='btnClearTreatmentMethod']"").prop('disabled', false);
                                            $(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change');
                                            }});   
                ", strClinicCode);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

            } 
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                try
                {
                    SETUPCOMPUTER _SETUPCOMPUTER = new SETUPCOMPUTER();
                    _SETUPCOMPUTER.ComputerCode = txtComputerCode.Text.Trim();
                    _SETUPCOMPUTER.ComputerName = txtComputerName.Text.Trim();
                    _SETUPCOMPUTER.DefaultStoreCode = ddlDefaultStore.SelectedItem.Value;
                    _SETUPCOMPUTER.DefaultClinicCode = hfClinic.Value;//ddlDefaultClinic.SelectedItem.Value;
                 

                    if (!string.IsNullOrEmpty(hfComputerCode.Value))
                    {
                        _SETUPCOMPUTER.ComputerCode = hfComputerCode.Value;
                        ReturnValue rv = new BLSETUPCOMPUTER(appConnDBInfo).Update(_SETUPCOMPUTER); 
                        if (rv.Value)
                        {
                            InitSetupComputer();
                            LoadComputerList();
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlDefaultStore']"").val(null).trigger('change');
                                            $(""[id*='ddlDefaultClinic']"").val(null).trigger('change');
                                            $.notify('Update {0} : {1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPCOMPUTER.ComputerCode, _SETUPCOMPUTER.ComputerName);
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
                        ReturnValue dup = new BLSETUPCOMPUTER(appConnDBInfo).CheckDup(_SETUPCOMPUTER);
                        if (!dup.Value)
                        {
                            ReturnValue rv = new BLSETUPCOMPUTER(appConnDBInfo).Insert(_SETUPCOMPUTER);
                            if (rv.Value)
                            {
                                InitSetupComputer();
                                LoadComputerList();
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlDefaultStore']"").val(null).trigger('change');
                                            $(""[id*='ddlDefaultClinic']"").val(null).trigger('change');
                                            $.notify('Insert {0} : {1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPCOMPUTER.ComputerCode, _SETUPCOMPUTER.ComputerName);
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
                                            $.notify('Computer Code is duplicated in data setup',
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

        private bool CheckValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string computerCode = txtComputerCode.Text.Trim();
            string computerName = txtComputerName.Text.Trim();
            string defaultStoreCode = ddlDefaultStore.SelectedItem.Value;
            string defaultClinicCode = hfClinic.Value;//ddlDefaultClinic.SelectedItem.Value;
            if (string.IsNullOrEmpty(computerCode) || string.IsNullOrEmpty(computerName) 
                || string.IsNullOrEmpty(defaultStoreCode) || string.IsNullOrEmpty(defaultClinicCode))
            {
                message = string.IsNullOrEmpty(computerCode) ? "   - Computer Code" : "";
                message += (!string.IsNullOrEmpty(message) ? "\\n" : "");
                message += string.IsNullOrEmpty(computerName) ? "  - Computer Name " : "";
                message += (!string.IsNullOrEmpty(message) ? "\\n" : "");
                message += string.IsNullOrEmpty(defaultStoreCode) ? "  - Default Store " : "";
                message += (!string.IsNullOrEmpty(message) ? "\\n" : "");
                message += string.IsNullOrEmpty(defaultClinicCode) ? "  - Default Clinic " : "";
                message = "Please fill out this field\\n" + message;

                string strClinicCode = "'" + hfClinic.Value.Replace(",", "','") + "'";
                string strClinic = !string.IsNullOrEmpty(hfClinic.Value) ? string.Format(@"$(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change'); ", strClinicCode) : string.Empty;
                string alertClinic = string.Format(@"$(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change'); ", strClinicCode);
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                           {1}
                                          }});   ", message, strClinic);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

                valid = false;
            }
            return valid;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            InitSetupComputer();
        }

        protected void gvSetupComputerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        private void LoadTreatmentMethod()
        {
            try
            {
                ListItem litTreatmentMethod = new ListItem();
                litTreatmentMethod.Text = "None";
                litTreatmentMethod.Value = "";

                List<VT_TREATMENTMETHODCODE> lstTreatmentMethod = new BLVT_TREATMENTMETHODCODE(appConnDBInfo).GetTreatmentMethodAll();

                ddlTreatmentMethod.DataSource = lstTreatmentMethod;
                ddlTreatmentMethod.DataValueField = "MethodCode";
                ddlTreatmentMethod.DataTextField = "TreatmentMethodName";
                ddlTreatmentMethod.DataBind();
                ddlTreatmentMethod.Items.Insert(0, litTreatmentMethod);

                //Edit 15.02 เปลี่ยนมาใช้ Group method
                //List<VT_TREATMENTMETHODCODE> lstTreatmentMethod = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentMethodAll();

                //ddlTreatmentMethod.DataSource = lstTreatmentMethod;
                //ddlTreatmentMethod.DataValueField = "MethodCode";
                //ddlTreatmentMethod.DataTextField = "TreatmentMethodName";
                //ddlTreatmentMethod.DataBind();
                //ddlTreatmentMethod.Items.Insert(0, litTreatmentMethod);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnAddTreatmentMethod_Click(object sender, EventArgs e)
        {
            string treatmentMethod = ddlTreatmentMethod.SelectedItem.Value;
            string computerCode = hfComputerCode.Value;
            AddTreatmentMethod(computerCode, treatmentMethod);
            
        }

        private void AddTreatmentMethod(string computerCode, string treatmentMethod)
        {

            //Edit 15.02 แก้ไขให้อ่านจาก group method
            //VT_TREATMENTMETHODCODE treatment = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentMethodByKey(treatmentMethod);

            VT_TREATMENTMETHODCODE treatment = new BLVT_TREATMENTMETHODCODE(appConnDBInfo).GetTreatmentMethodByKey(treatmentMethod);
            SETUPCOMPUTERMETHOD _SETUPCOMPUTERMETHOD = new SETUPCOMPUTERMETHOD();
            _SETUPCOMPUTERMETHOD.ComputerCode = computerCode;
            _SETUPCOMPUTERMETHOD.MethodCode = treatment.MethodCode;
            _SETUPCOMPUTERMETHOD.MethodName = treatment.MethodName;
            string strClinicCode = "'" + hfClinic.Value.Replace(",", "','") + "'";
            string strClinic = !string.IsNullOrEmpty(hfClinic.Value) ? string.Format(@"$(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change'); ", strClinicCode) : string.Empty;
            ReturnValue dup = new BLSETUPCOMPUTERMETHOD(appConnDBInfo).CheckDup(_SETUPCOMPUTERMETHOD);
            if (!dup.Value)
            {
                ReturnValue rv = new BLSETUPCOMPUTERMETHOD(appConnDBInfo).Insert(_SETUPCOMPUTERMETHOD);
                if (rv.Value == true)
                {
                    ddlTreatmentMethod.SelectedIndex = 0;
                    LoadTreatmentMethodList(computerCode);
                   
                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           "     $(\"[id *= 'ddlTreatmentMethod']\").val(null).trigger('change');" +
                                           "     $(\"[id *= 'ddlTreatmentMethod']\").prop('disabled', false);" +
                                           "     {2}" +
                                           " $.notify('Add {0} : {1} completed.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", treatment.MethodCode, treatment.MethodName, strClinic);
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
                                            {0}
                                            $.notify('This treatment method was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", strClinic);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
            }
        }

        protected void btnClearTreatmentMethod_Click(object sender, EventArgs e)
        {
            string strClinicCode = "'" + hfClinic.Value.Replace(",", "','") + "'";
            string strClinic = !string.IsNullOrEmpty(hfClinic.Value) ? string.Format(@"$(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change'); ", strClinicCode) : string.Empty;
            string alertScript = string.Format(@"javascript: $(document).ready(function(){{

                                           $(""[id*='ddlTreatmentMethod']"").select2({{

                                                placeholder: ""Select Method"",
                                                width: '100%',
                                                allowClear: true,
                                                disabled: false,

                                            }});
                                            $(""[id*='ddlTreatmentMethod']"").val(null).trigger('change');
                                            $(""[id*='ddlTreatmentMethod']"").prop('disabled', false);
                                            $(""[id*='btnClearTreatmentMethod']"").prop('disabled', false);
                                            $(""[id*='btnAddTreatmentMethod']"").prop('disabled', true);
                                            {0}
                                            }});   ", strClinic);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);
        }

        protected void gvTreatmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTreatmentList.Rows[index];
            if (e.CommandName == "DeleteTreatmentMethod")
            {

                SETUPCOMPUTERMETHOD _SETUPCOMPUTERMETHOD = new SETUPCOMPUTERMETHOD();
                _SETUPCOMPUTERMETHOD.ComputerMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["ComputerMethodID"]);
                _SETUPCOMPUTERMETHOD.ComputerCode = gvTreatmentList.DataKeys[row.RowIndex].Values["ComputerCode"].ToString();
                _SETUPCOMPUTERMETHOD.MethodCode = gvTreatmentList.DataKeys[row.RowIndex].Values["MethodCode"].ToString();
                _SETUPCOMPUTERMETHOD.MethodName = gvTreatmentList.DataKeys[row.RowIndex].Values["MethodName"].ToString();
                string strClinicCode = "'" + hfClinic.Value.Replace(",", "','") + "'";
                string strClinic = !string.IsNullOrEmpty(hfClinic.Value) ? string.Format(@"$(""[id*='ddlDefaultClinic']"").val([{0}]).trigger('change'); ", strClinicCode) : string.Empty;
                ReturnValue rv = new BLSETUPCOMPUTERMETHOD(appConnDBInfo).Delete(_SETUPCOMPUTERMETHOD);
                if (rv.Value)
                {
                    LoadTreatmentMethodList(_SETUPCOMPUTERMETHOD.ComputerCode);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            {2}
                                            $.notify('Delete [{0}]:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPCOMPUTERMETHOD.MethodCode, _SETUPCOMPUTERMETHOD.MethodName, strClinic);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);

                }
                else
                {
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            {1}
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", rv.Exception, strClinic);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);
                }

            }





        }

        protected void gvTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void LoadTreatmentMethodList(string ComputerCode)
        {
            try
            {
                List<SETUPCOMPUTERMETHOD> list = new BLSETUPCOMPUTERMETHOD(appConnDBInfo).GetAllByKey(ComputerCode);
                gvTreatmentList.DataSource = list;
                gvTreatmentList.DataBind();

            }
            catch { }

        }
    }
}