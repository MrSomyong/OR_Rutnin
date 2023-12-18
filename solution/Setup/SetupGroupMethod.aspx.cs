using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Info;
using System.Data;
using Newtonsoft.Json;
namespace solution.Setup
{
    public partial class SetupGroupMethod : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected static DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

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
                LoadSetupGroupMethodList();
                LoadTreatmentCode();
                //LoadMedicineItem();
                LoadDoseUnit();
                LoadDoseQTY();
                LoadDoseType();
                LoadDoseCode();
                LoadUnit();
                LoadAuxLabel();
                LoadDoctor();
                LoadComputer();
                LoadClinic();
                InitSetupGroupMethod();
                InitScriptSetupGroupMethod();

            }



        }

        private void LoadDoctor()
        {
            try
            {
                ListItem lstDoctor = new ListItem();
                lstDoctor.Text = "Select All";
                lstDoctor.Value = "";
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(extConnDBInfo).SearchByKey(DOCTORMASTERVO);
                ddlDoctor.DataSource = lstDOCTORMASTERVO;
                ddlDoctor.DataValueField = "DOCTOR";
                ddlDoctor.DataTextField = "DoctorName";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, lstDoctor);

               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadComputer()
        {
            try
            {
                ListItem lstComputer = new ListItem();
                lstComputer.Text = "Select All";
                lstComputer.Value = "";
                List<SETUPCOMPUTER> lstComputers = new BLSETUPCOMPUTER(appConnDBInfo, extConnDBInfo).SearchAll();
                ddlComputer.DataSource = lstComputers;
                ddlComputer.DataValueField = "ComputerCode";
                ddlComputer.DataTextField = "ComputerName";
                ddlComputer.DataBind();
                ddlComputer.Items.Insert(0, lstComputer);


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
                ListItem lstClinic = new ListItem();
                lstClinic.Text = "Select All";
                lstClinic.Value = "";
                List<VT_CLINIC> lstClinices = new BLVT_CLINIC(extConnDBInfo).SearchAll();
                ddlClinic.DataSource = lstClinices;
                ddlClinic.DataValueField = "CLINIC_CODE";
                ddlClinic.DataTextField = "CLINIC_NAMEDESC";
                ddlClinic.DataBind();
                ddlClinic.Items.Insert(0, lstClinic);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadAuxLabel()
        {
            try
            {
                ListItem litAuxLabel = new ListItem();
                litAuxLabel.Text = "None";
                litAuxLabel.Value = "";

                List<DOSEAUX> lstAuxLabel = new BLVT_DOSEAUX(extConnDBInfo).SearchAll();
                ddlAuxLabel1.DataSource = lstAuxLabel;
                ddlAuxLabel1.DataValueField = "CODE";
                ddlAuxLabel1.DataTextField = "NameInfo";
                ddlAuxLabel1.DataBind();
                ddlAuxLabel1.Items.Insert(0, litAuxLabel);

                ddlAuxLabel2.DataSource = lstAuxLabel;
                ddlAuxLabel2.DataValueField = "CODE";
                ddlAuxLabel2.DataTextField = "NameInfo";
                ddlAuxLabel2.DataBind();
                ddlAuxLabel2.Items.Insert(0, litAuxLabel);

                ddlAuxLabel3.DataSource = lstAuxLabel;
                ddlAuxLabel3.DataValueField = "CODE";
                ddlAuxLabel3.DataTextField = "NameInfo";
                ddlAuxLabel3.DataBind();
                ddlAuxLabel3.Items.Insert(0, litAuxLabel);

                ddlEditAuxLabel1.DataSource = lstAuxLabel;
                ddlEditAuxLabel1.DataValueField = "CODE";
                ddlEditAuxLabel1.DataTextField = "NameInfo";
                ddlEditAuxLabel1.DataBind();
                ddlEditAuxLabel1.Items.Insert(0, litAuxLabel);

                ddlEditAuxLabel2.DataSource = lstAuxLabel;
                ddlEditAuxLabel2.DataValueField = "CODE";
                ddlEditAuxLabel2.DataTextField = "NameInfo";
                ddlEditAuxLabel2.DataBind();
                ddlEditAuxLabel2.Items.Insert(0, litAuxLabel);

                ddlEditAuxLabel3.DataSource = lstAuxLabel;
                ddlEditAuxLabel3.DataValueField = "CODE";
                ddlEditAuxLabel3.DataTextField = "NameInfo";
                ddlEditAuxLabel3.DataBind();
                ddlEditAuxLabel3.Items.Insert(0, litAuxLabel);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InitSetupGroupMethod()
        {
            btnClear.Visible = false;
            txtGroupMethodCode.Enabled = true;
            txtGroupMethodCode.Text = string.Empty;
            txtGroupMethodName.Text = string.Empty;
            hfGroupMethodID.Value = string.Empty;
            hdGroupMethodID.Value = string.Empty;
            hdChargeCode.Value = string.Empty;
            hdTreatmentCode.Value = string.Empty;

            gvTreatmentList.DataSource = new List<SETUPGROUPMETHODTREATMENT>();
            gvTreatmentList.DataBind();

            gvMedicineList.DataSource = new List<SETUPGROUPMETHODTREATMENT>();
            gvMedicineList.DataBind();

            gvDoctorList.DataSource = new List<SETUPGROUPMETHODDOCTOR>();
            gvDoctorList.DataBind();

            gvComputerList.DataSource = new List<SETUPGROUPMETHODCOMPUTER>();
            gvComputerList.DataBind();

            gvClinicList.DataSource = new List<SETUPGROUPMETHODCLINIC>();
            gvClinicList.DataBind();

            //string alertScript = string.Format(@"javascript: $(document).ready(function(){{
            //                                $(""[id*='ddlTreatment']"").val(null).trigger('change');
            //                                $(""[id*='ddlTreatment']"").prop('disabled', true);

            //                                $(""[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode']"").val(null).trigger('change');
            //                                $(""[id*='txtMedQTY']"").val('');
            //                                $(""[id*='ddlMedicine']"").prop('disabled', true);

            //                              }});   ");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
            //                    alertScript, true);

        }

        private void InitScriptSetupGroupMethod()
        {
            //string alertScript = string.Format(@"javascript: $(document).ready(function(){{
            //                                $(""[id*='ddlTreatment']"").val(null).trigger('change');
            //                                $(""[id*='ddlTreatment']"").prop('disabled', true);

            //                                $(""[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode']"").val(null).trigger('change');
            //                                $(""[id*='txtMedQTY']"").val('');
            //                                $(""[id*='ddlMedicine']"").prop('disabled', true);

            //                              }});   ");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
            //                    alertScript, true);

            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlDoctor'],[id*='ddlComputer'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlDoctor'],[id*='ddlComputer'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', true);
                                            $(""[id*='txtMedUnitPrice'],[id*='txtMedQTY'],[id*='txtMedAMT'],[id*='txtMedRemark']"").val('').prop('disabled', true);
                                            $(""[id*='btnAddTreatment'],[id*='btnClearTreatment']"").prop('disabled', true);                                            
                                            $(""[id*='btnAddMedicine'],[id*='btnClearMedicine']"").prop('disabled', true);     
                                            $(""[id*='btnAddDoctor'],[id*='btnClearDoctor']"").prop('disabled', true);
                                            $(""[id*='btnAddComputer'],[id*='btnClearComputer']"").prop('disabled', true);
                                            $(""[id*='btnAddClinic'],[id*='btnClearClinic']"").prop('disabled', true);

                                          }});   ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);

        }

        private void LoadTreatmentCode()
        {
            try
            {
                ListItem litTreatment = new ListItem();
                litTreatment.Text = "None";
                litTreatment.Value = "";
                VT_TREATMENTCODE VT_TREATMENTCODE = new VT_TREATMENTCODE();

                List<VT_TREATMENTCODE> lstTreatment = new BLVT_TREATMENTCODE(extConnDBInfo).SearchAllByKey(0);
                List<VT_TREATMENTCODE> lstTreatment01 = new BLVT_TREATMENTCODE(extConnDBInfo).SearchAllByKey(1);
                lstTreatment.AddRange(lstTreatment01);
                ddlTreatment.DataSource = lstTreatment;
                ddlTreatment.DataValueField = "CODE";
                ddlTreatment.DataTextField = "TREATMENTNAME";
                ddlTreatment.DataBind();
                ddlTreatment.Items.Insert(0, litTreatment);

               

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void LoadMedicineItem()
        //{
        //    try
        //    {
        //        ListItem litMedicine = new ListItem();
        //        litMedicine.Text = "-Please Select-";
        //        litMedicine.Value = "";
        //        VT_STOCK_MASTER VT_STOCKMASTER = new VT_STOCK_MASTER();

        //        List<VT_STOCK_MASTER> listMedicine = new BLVT_STOCK_MASTER(extConnDBInfo).SearchAll();
        //        ddlMedicine.DataSource = listMedicine;
        //        ddlMedicine.DataValueField = "StockCode";
        //        ddlMedicine.DataTextField = "MedicineName";
        //        ddlMedicine.DataBind();
        //        ddlMedicine.Items.Insert(0, litMedicine);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        private void LoadUnit()
        {
            try
            {
                ListItem litUnit = new ListItem();
                litUnit.Text = "-Please Select-";
                litUnit.Value = "";

                List<UNIT> lstUnit = new BLVT_UNIT(extConnDBInfo).SearchAll();
                ddlUnit.DataSource = lstUnit;
                ddlUnit.DataValueField = "CODE";
                ddlUnit.DataTextField = "NameInfo";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, litUnit);

                ddlEditUnit.DataSource = lstUnit;
                ddlEditUnit.DataValueField = "CODE";
                ddlEditUnit.DataTextField = "NameInfo";
                ddlEditUnit.DataBind();
                ddlEditUnit.Items.Insert(0, litUnit);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadDoseUnit()
        {
            try
            {
                ListItem litDoseUnit = new ListItem();
                litDoseUnit.Text = "None";
                litDoseUnit.Value = "";

                List<DOSEUNIT> lstUnit = new BLVT_DOSEUNIT(extConnDBInfo).SearchAll();
                ddlDoseUnit.DataSource = lstUnit;
                ddlDoseUnit.DataValueField = "CODE";
                ddlDoseUnit.DataTextField = "NameInfo";
                ddlDoseUnit.DataBind();
                ddlDoseUnit.Items.Insert(0, litDoseUnit);

                ddlEditDoseUnit.DataSource = lstUnit;
                ddlEditDoseUnit.DataValueField = "CODE";
                ddlEditDoseUnit.DataTextField = "NameInfo";
                ddlEditDoseUnit.DataBind();
                ddlEditDoseUnit.Items.Insert(0, litDoseUnit);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadDoseQTY()
        {
            try
            {
                ListItem litDoseQTY = new ListItem();
                litDoseQTY.Text = "None";
                litDoseQTY.Value = "";

                List<DOSEQTY> lstDoseQTY = new BLVT_DOSEQTY(extConnDBInfo).SearchAll();
                ddlDoseQTY.DataSource = lstDoseQTY;
                ddlDoseQTY.DataValueField = "CODE";
                ddlDoseQTY.DataTextField = "NameInfo";
                ddlDoseQTY.DataBind();
                ddlDoseQTY.Items.Insert(0, litDoseQTY);

                ddlEditDoseQTY.DataSource = lstDoseQTY;
                ddlEditDoseQTY.DataValueField = "CODE";
                ddlEditDoseQTY.DataTextField = "NameInfo";
                ddlEditDoseQTY.DataBind();
                ddlEditDoseQTY.Items.Insert(0, litDoseQTY);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void LoadDoseType()
        {
            try
            {
                ListItem litDoseType = new ListItem();
                litDoseType.Text = "None";
                litDoseType.Value = "";

                List<DOSETYPE> lstDoseType = new BLVT_DOSETYPE(extConnDBInfo).SearchAll();
                ddlDoseType.DataSource = lstDoseType;
                ddlDoseType.DataValueField = "CODE";
                ddlDoseType.DataTextField = "NameInfo";
                ddlDoseType.DataBind();
                ddlDoseType.Items.Insert(0, litDoseType);

                ddlEditDoseType.DataSource = lstDoseType;
                ddlEditDoseType.DataValueField = "CODE";
                ddlEditDoseType.DataTextField = "NameInfo";
                ddlEditDoseType.DataBind();
                ddlEditDoseType.Items.Insert(0, litDoseType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadDoseCode()
        {
            try
            {
                ListItem litDoseCode = new ListItem();
                litDoseCode.Text = "None";
                litDoseCode.Value = "";

                List<DOSECODE> lstDoseCode = new BLVT_DOSECODE(extConnDBInfo).SearchAll();
                ddlDoseCode.DataSource = lstDoseCode;
                ddlDoseCode.DataValueField = "CODE";
                ddlDoseCode.DataTextField = "NameInfo";
                ddlDoseCode.DataBind();
                ddlDoseCode.Items.Insert(0, litDoseCode);

                ddlEditDoseCode.DataSource = lstDoseCode;
                ddlEditDoseCode.DataValueField = "CODE";
                ddlEditDoseCode.DataTextField = "NameInfo";
                ddlEditDoseCode.DataBind();
                ddlEditDoseCode.Items.Insert(0, litDoseCode);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnAddTreatment_Click(object sender, EventArgs e)
        {
            string treatmentCode = ddlTreatment.SelectedItem.Value;
            int groupMethodID = Convert.ToInt32(hfGroupMethodID.Value);
            AddTreatment(groupMethodID, treatmentCode);
        }

        private void AddTreatment(int groupMethodID, string treatmentCode)
        {

            VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode);
            SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
            _SETUPGROUPMETHODTREATMENT.GroupMethodID = groupMethodID;
            _SETUPGROUPMETHODTREATMENT.TreatmentCode = treatment.CODE;
            _SETUPGROUPMETHODTREATMENT.TreatmentName = treatment.Name;

            _SETUPGROUPMETHODTREATMENT.CHARGECODE = treatment.Activity;
            _SETUPGROUPMETHODTREATMENT.AMT = treatment.StdPrice1;
            _SETUPGROUPMETHODTREATMENT.QTY = 1;
            _SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE = treatment.TreatmentStyle;
            _SETUPGROUPMETHODTREATMENT.AutoTick = cbTMAutoTick.Checked;



            if (new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).CheckDup(_SETUPGROUPMETHODTREATMENT) == false)
            {
                if ((_SETUPGROUPMETHODTREATMENT.AMT == 0 && treatment.ZeroPrice != 1) )
                {
                    //_SETUPGROUPMETHODTREATMENT.TreatmentCodeInfo = treatment;
                    EditTreatment(_SETUPGROUPMETHODTREATMENT);
                }
                else
                {
                    ReturnValue rv = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).Insert(_SETUPGROUPMETHODTREATMENT);
                    if (rv.Value == true)
                    {
                        ddlTreatment.SelectedIndex = 0;
                        LoadTreatmentList(groupMethodID);

                        string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                               "     $(\"[id *= 'ddlTreatment']\").val(null).trigger('change');" +
                                               "     $(\"[id *= 'ddlTreatment']\").prop('disabled', false);" +
                                               "     $(\"[id*='cbTMAutoTick']\").prop('checked', false); " +
                                               " $.notify('Add {0} : {1} completed.', " +
                                               "      {{ " +
                                               "          className: 'success', " +
                                               "          position: 'bottom right', " +
                                               "          clickToHide: true " +
                                               "      }} " +
                                               "  ); " +
                                               " }}); ", treatment.CODE, treatment.Name);
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
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('This treatment item was already entered. Please try another item.',
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

        private void EditTreatment(SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT)
        {

            hdTreatmentCode.Value = _SETUPGROUPMETHODTREATMENT.TreatmentCode;
            hdChargeCode.Value = _SETUPGROUPMETHODTREATMENT.CHARGECODE;
            hdGroupMethodID.Value = _SETUPGROUPMETHODTREATMENT.GroupMethodID.ToString();
            hdTreatmentEntryStyle.Value = _SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE.ToString();
            lblTreatmentName.Text = string.Format("[{0}] {1}", _SETUPGROUPMETHODTREATMENT.TreatmentCode, _SETUPGROUPMETHODTREATMENT.TreatmentName);
            txtAmt.Text = string.Format("{0:0.##}", _SETUPGROUPMETHODTREATMENT.AMT);
            txtQty.Text = string.Format("{0:0.##}", _SETUPGROUPMETHODTREATMENT.QTY);
            txtRemark.Text = _SETUPGROUPMETHODTREATMENT.REMARKS;
            cbTMEditAutoTick.Checked = _SETUPGROUPMETHODTREATMENT.AutoTick;

            switch (_SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE)
            {
                case 1:
                    txtAmt.Enabled = false;
                    txtQty.Enabled = false;
                    break;
                case 2:
                    txtAmt.Enabled = true;
                    txtQty.Enabled = false;
                    break;
                case 3:
                    txtAmt.Enabled = false;
                    txtQty.Enabled = true;
                    break;
                default:
                    txtAmt.Enabled = true;
                    txtQty.Enabled = true;
                    break;
            }
            htmlTitleUpdateTreatment.InnerHtml = "Add Treatment Item";
            btnUpdateTreatment.Text = "Add";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalTreatment();", true);
           
        }



        public void LoadTreatmentList(int groupMethodID)
        {
            try
            {
                List<SETUPGROUPMETHODTREATMENT> listTreatment = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByKey(groupMethodID);
                gvTreatmentList.DataSource = listTreatment;
                gvTreatmentList.DataBind();

            }
            catch { }

        }

        protected void btnAddDoctor_Click(object sender, EventArgs e)
        {
            string doctorCode = ddlDoctor.SelectedItem.Value;
            SETUPGROUPMETHODDOCTOR doctor = new SETUPGROUPMETHODDOCTOR();
            doctor.DoctorCode = ddlDoctor.SelectedItem.Value;
            doctor.DoctorName = ddlDoctor.SelectedItem.Text;
            int groupMethodID = Convert.ToInt32(hfGroupMethodID.Value);
            AddDoctor(groupMethodID, doctor);
        }

        private void AddDoctor(int groupMethodID, SETUPGROUPMETHODDOCTOR doctor)
        {

            SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR = new SETUPGROUPMETHODDOCTOR();
            _SETUPGROUPMETHODDOCTOR.GroupMethodID = groupMethodID;
            _SETUPGROUPMETHODDOCTOR.DoctorCode = doctor.DoctorCode;
            _SETUPGROUPMETHODDOCTOR.DoctorName = doctor.DoctorName;
            if (new BLSETUPGROUPMETHODDOCTOR(appConnDBInfo).CheckDup(_SETUPGROUPMETHODDOCTOR) == false)
            {
                ReturnValue rv = new BLSETUPGROUPMETHODDOCTOR(appConnDBInfo).Insert(_SETUPGROUPMETHODDOCTOR);
                if (rv.Value == true)
                {
                    ddlDoctor.SelectedIndex = 0;
                    LoadDoctorList(groupMethodID);

                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           "     $(\"[id *= 'ddlDoctor']\").val(null).trigger('change');" +
                                           "     $(\"[id *= 'ddlDoctor']\").prop('disabled', false);" +
                                           "     $(\"[id *= 'btnAddDoctor']\").attr('disabled', 'disabled'); " +
                                           " $.notify('Add {0} : {1} completed.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", doctor.DoctorCode, doctor.DoctorName);
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
                                            $.notify('This doctor item was already entered. Please try another item.',
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
        protected void btnAddComputer_Click(object sender, EventArgs e)
        {
            string computerCode = ddlComputer.SelectedItem.Value;
            SETUPGROUPMETHODCOMPUTER computer = new SETUPGROUPMETHODCOMPUTER();
            computer.ComputerCode = ddlComputer.SelectedItem.Value;
            computer.ComputerName = ddlComputer.SelectedItem.Text;
            int groupMethodID = Convert.ToInt32(hfGroupMethodID.Value);
            AddComputer(groupMethodID, computer);
        }
        private void AddComputer(int groupMethodID, SETUPGROUPMETHODCOMPUTER computer)
        {

            SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER = new SETUPGROUPMETHODCOMPUTER();
            _SETUPGROUPMETHODCOMPUTER.GroupMethodID = groupMethodID;
            _SETUPGROUPMETHODCOMPUTER.ComputerCode = computer.ComputerCode;
            _SETUPGROUPMETHODCOMPUTER.ComputerName = computer.ComputerName;
            if (new BLSETUPGROUPMETHODCOMPUTER(appConnDBInfo).CheckDup(_SETUPGROUPMETHODCOMPUTER) == false)
            {
                ReturnValue rv = new BLSETUPGROUPMETHODCOMPUTER(appConnDBInfo).Insert(_SETUPGROUPMETHODCOMPUTER);
                if (rv.Value == true)
                {
                    ddlComputer.SelectedIndex = 0;
                    LoadComputerList(groupMethodID);

                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           "     $(\"[id *= 'ddlComputer']\").val(null).trigger('change');" +
                                           "     $(\"[id *= 'ddlComputer']\").prop('disabled', false);" +
                                           "     $(\"[id *= 'btnAddComputer']\").attr('disabled', 'disabled'); " +  
                                           " $.notify('Add {0} : {1} completed.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", computer.ComputerCode, computer.ComputerName);
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
                                            $.notify('This computer item was already entered. Please try another item.',
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

        protected void btnAddClinic_Click(object sender, EventArgs e)
        {
            string clinicCode = ddlClinic.SelectedItem.Value;
            SETUPGROUPMETHODCLINIC clinic = new SETUPGROUPMETHODCLINIC();
            clinic.ClinicCode = ddlClinic.SelectedItem.Value;
            clinic.ClinicName = ddlClinic.SelectedItem.Text;
            int groupMethodID = Convert.ToInt32(hfGroupMethodID.Value);
            AddClinic(groupMethodID, clinic);
        }
        private void AddClinic(int groupMethodID, SETUPGROUPMETHODCLINIC clinic)
        {

            SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC = new SETUPGROUPMETHODCLINIC();
            _SETUPGROUPMETHODCLINIC.GroupMethodID = groupMethodID;
            _SETUPGROUPMETHODCLINIC.ClinicCode = clinic.ClinicCode;
            _SETUPGROUPMETHODCLINIC.ClinicName = clinic.ClinicName;
            if (new BLSETUPGROUPMETHODCLINIC(appConnDBInfo).CheckDup(_SETUPGROUPMETHODCLINIC) == false)
            {
                ReturnValue rv = new BLSETUPGROUPMETHODCLINIC(appConnDBInfo).Insert(_SETUPGROUPMETHODCLINIC);
                if (rv.Value == true)
                {
                    ddlClinic.SelectedIndex = 0;
                    LoadClinicList(groupMethodID);

                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           "     $(\"[id *= 'ddlClinic']\").val(null).trigger('change');" +
                                           "     $(\"[id *= 'ddlClinic']\").prop('disabled', false);" +
                                           "     $(\"[id *= 'btnAddClinic']\").attr('disabled', 'disabled'); " +
                                           " $.notify('Add {0} : {1} completed.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", clinic.ClinicCode, clinic.ClinicName);
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
                                            $.notify('This clinic item was already entered. Please try another item.',
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

        public void LoadDoctorList(int groupMethodID)
        {
            try
            {
                List<SETUPGROUPMETHODDOCTOR> lstGroupMethodDoctor = new BLSETUPGROUPMETHODDOCTOR(appConnDBInfo).GetSETUPGROUPMETHODDOCTORByKey(groupMethodID);
                gvDoctorList.DataSource = lstGroupMethodDoctor;
                gvDoctorList.DataBind();

                try
                {
                    ListItem lstDoctor = new ListItem();
                    lstDoctor.Text = "Select All";
                    lstDoctor.Value = "";
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                    List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(extConnDBInfo).SearchByKey(DOCTORMASTERVO);
                    var result = lstDOCTORMASTERVO.Where(p => lstGroupMethodDoctor.All(p2 => p2.DoctorCode != p.DOCTOR));

                    ddlDoctor.Items.Clear();
                    ddlDoctor.DataSource = result;
                    ddlDoctor.DataValueField = "DOCTOR";
                    ddlDoctor.DataTextField = "DoctorName";
                    ddlDoctor.DataBind();
                    ddlDoctor.Items.Insert(0, lstDoctor);


                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch { }

        }
        public void LoadMedicineList(int groupMethodID)
        {
            try
            {
                List<SETUPGROUPMETHODMEDICINE> listMedicine = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(groupMethodID);
                gvMedicineList.DataSource = listMedicine;
                gvMedicineList.DataBind();

            }
            catch { }

        }
        public void LoadComputerList(int groupMethodID)
        {
            try
            {
                List<SETUPGROUPMETHODCOMPUTER> lstGroupMethodComputer = new BLSETUPGROUPMETHODCOMPUTER(appConnDBInfo).GetSETUPGROUPMETHODCOMPUTERByKey(groupMethodID);
                gvComputerList.DataSource = lstGroupMethodComputer;
                gvComputerList.DataBind();

                try
                {
                    ListItem lstComputer = new ListItem();
                    lstComputer.Text = "Select All";
                    lstComputer.Value = "";
                    List<SETUPCOMPUTER> lstComputers = new BLSETUPCOMPUTER(appConnDBInfo, extConnDBInfo).SearchAll();

                    var result = lstComputers.Where(p => lstGroupMethodComputer.All(p2 => p2.ComputerCode != p.ComputerCode));

                    ddlComputer.Items.Clear();
                    ddlComputer.DataSource = result;
                    ddlComputer.DataValueField = "ComputerCode";
                    ddlComputer.DataTextField = "ComputerName";
                    ddlComputer.DataBind();
                    ddlComputer.Items.Insert(0, lstComputer);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch { }

        }

        public void LoadClinicList(int groupMethodID)
        {
            try
            {
                List<SETUPGROUPMETHODCLINIC> lstGroupMethodClinic = new BLSETUPGROUPMETHODCLINIC(appConnDBInfo).GetSETUPGROUPMETHODCLINICByKey(groupMethodID);
                gvClinicList.DataSource = lstGroupMethodClinic;
                gvClinicList.DataBind();

                try
                {
                    ListItem lstClinic = new ListItem();
                    lstClinic.Text = "Select All";
                    lstClinic.Value = "";
                    List<VT_CLINIC> lstClinics = new BLVT_CLINIC(extConnDBInfo).SearchAll();

                    var result = lstClinics.Where(p => lstGroupMethodClinic.All(p2 => p2.ClinicCode != p.CLINIC_CODE));

                    ddlClinic.Items.Clear();
                    ddlClinic.DataSource = result;
                    ddlClinic.DataValueField = "CLINIC_CODE";
                    ddlClinic.DataTextField = "CLINIC_NAMEDESC";
                    ddlClinic.DataBind();
                    ddlClinic.Items.Insert(0, lstClinic);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            catch { }

        }


        protected void gvSetupGroupMethodList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteGroupMethod")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupGroupMethodList.Rows[index];
                SETUPGROUPMETHOD _SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                _SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHOD.GroupMethodCode = gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodCode"].ToString();
                _SETUPGROUPMETHOD.GroupMethodName = gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHOD(appConnDBInfo).InActive(_SETUPGROUPMETHOD);
                if (rv.Value == true)
                {
                    InitSetupGroupMethod();
                    LoadSetupGroupMethodList();
                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                            "    $(\"[id *= 'ddlTreatment']\").val(null).trigger('change'); " +
                                            "    $(\"[id *= 'ddlTreatment']\").prop('disabled', true); " +
                                            "    $(\"[id *= 'ddlMedicine'],[id *= 'ddlUnit'],[id *= 'ddlDoseType'],[id *= 'ddlDoseQTY'],[id *= 'ddlDoseUnit'],[id *= 'ddlDoseCode'],[id *= 'ddlAuxLabel1'],[id *= 'ddlAuxLabel2'],[id *= 'ddlAuxLabel3']\").val(null).trigger('change'); " +
                                            "    $(\"[id *= 'txtMedQTY'],[id *= 'txtMedAMT'],[id *= 'txtMedUnitPrice'],[id *= 'txtMedRemark']\").val(''); " +
                                            "    $(\"[id *= 'ddlMedicine']\").prop('disabled', true); " +
                                            "    $(\"[id*='cbTMAutoTick'],[id*='cbMedAutoTick']\").prop('checked', false); " +
                                            "    $(\"[id*='cbTMAutoTick'],[id*='cbMedAutoTick']\").prop('disabled', true); " +
                    " $.notify('Deleted {0} : {1} successfully.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", _SETUPGROUPMETHOD.GroupMethodCode, _SETUPGROUPMETHOD.GroupMethodName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                              alertScript, true);
                    //InitScriptSetupGroupMethod();
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
            else if (e.CommandName == "EditGroupMethod")
            {
                //ddlTreatment.Items[0].Attributes.Remove("disabled");

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvSetupGroupMethodList.Rows[index];
                SETUPGROUPMETHOD _SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                _SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHOD.GroupMethodCode = gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodCode"].ToString();
                _SETUPGROUPMETHOD.GroupMethodName = gvSetupGroupMethodList.DataKeys[row.RowIndex].Values["GroupMethodName"].ToString();
                hfGroupMethodID.Value = _SETUPGROUPMETHOD.GroupMethodID.ToString();
                List<SETUPGROUPMETHODTREATMENT> listTreatment = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByKey(_SETUPGROUPMETHOD.GroupMethodID);
                gvTreatmentList.DataSource = listTreatment;
                gvTreatmentList.DataBind();

                btnClear.Visible = true;
                txtGroupMethodCode.Enabled = false;
                txtGroupMethodCode.Text = _SETUPGROUPMETHOD.GroupMethodCode;
                txtGroupMethodName.Text = _SETUPGROUPMETHOD.GroupMethodName;
                








                List<SETUPGROUPMETHODMEDICINE> listMedicine = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(_SETUPGROUPMETHOD.GroupMethodID);
                gvMedicineList.DataSource = listMedicine;
                gvMedicineList.DataBind();



                //List<SETUPGROUPMETHODDOCTOR> lstDoctor = new BLSETUPGROUPMETHODDOCTOR(appConnDBInfo).GetSETUPGROUPMETHODDOCTORByKey(_SETUPGROUPMETHOD.GroupMethodID);
                //gvDoctorList.DataSource = lstDoctor;
                //gvDoctorList.DataBind();
                LoadDoctorList(_SETUPGROUPMETHOD.GroupMethodID);

                LoadComputerList(_SETUPGROUPMETHOD.GroupMethodID);

                LoadClinicList(_SETUPGROUPMETHOD.GroupMethodID);

                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3'],[id*='ddlDoctor'],[id*='ddlComputer'],[id*='ddlClinic']"").val(null).trigger('change');
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3'],[id*='ddlDoctor'],[id*='ddlComputer'],[id*='ddlClinic']"").prop('disabled', false);
                                            $(""[id*='txtMedQTY'],[id*='txtMedAMT'],[id*='txtMedUnitPrice'],[id*='txtMedRemark']"").val('').prop('disabled', false); 
                                            }});   ");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);


                foreach (GridViewRow rowx in gvSetupGroupMethodList.Rows)
                {
                    if (rowx.RowIndex == row.RowIndex)
                    {

                        if (rowx.RowState == DataControlRowState.Selected)
                        {
                            rowx.CssClass = "selected";

                        }
                        else
                            rowx.CssClass = gvSetupGroupMethodList.AlternatingRowStyle.CssClass.ToString() + " selected";
                    }
                    else
                    {

                        if (rowx.RowState == DataControlRowState.Alternate)
                        {
                            rowx.CssClass = gvSetupGroupMethodList.AlternatingRowStyle.CssClass;
                        }
                        else
                            rowx.CssClass = "";


                    }
                }

            }



        }

        public void LoadSetupGroupMethodList()
        {
            try
            {
                List<SETUPGROUPMETHOD> lstSetupGroupMethod = new BLSETUPGROUPMETHOD(appConnDBInfo).SearchAll();
                gvSetupGroupMethodList.DataSource = lstSetupGroupMethod;
                gvSetupGroupMethodList.DataBind();

            }
            catch { }

        }

        protected void gvTreatmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvTreatmentList.Rows[index];
            if (e.CommandName == "DeleteTreatmentItem")
            {

                SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
                _SETUPGROUPMETHODTREATMENT.GroupMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODTREATMENT.TreatmentCode = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();
                _SETUPGROUPMETHODTREATMENT.TreatmentName = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).InActive(_SETUPGROUPMETHODTREATMENT);
                if (rv.Value)
                {
                    LoadTreatmentList(_SETUPGROUPMETHODTREATMENT.GroupMethodID);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete {0}:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHODTREATMENT.TreatmentCode, _SETUPGROUPMETHODTREATMENT.TreatmentName);
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
            else if (e.CommandName == "EditTreatmentItem")
            {
                SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
                _SETUPGROUPMETHODTREATMENT.GroupMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODTREATMENT.TreatmentCode = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();

                SETUPGROUPMETHODTREATMENT SETUPGROUPMETHODTREATMENT = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByKey(_SETUPGROUPMETHODTREATMENT);
                hdTreatmentCode.Value = SETUPGROUPMETHODTREATMENT.TreatmentCode;
                hdChargeCode.Value = SETUPGROUPMETHODTREATMENT.CHARGECODE;
                hdGroupMethodID.Value = SETUPGROUPMETHODTREATMENT.GroupMethodID.ToString();
                hdTreatmentEntryStyle.Value = SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE.ToString();
                lblTreatmentName.Text = string.Format("[{0}] {1}", SETUPGROUPMETHODTREATMENT.TreatmentCode, SETUPGROUPMETHODTREATMENT.TreatmentName);
                txtAmt.Text = string.Format("{0:0.##}", SETUPGROUPMETHODTREATMENT.AMT);
                txtQty.Text = string.Format("{0:0.##}", SETUPGROUPMETHODTREATMENT.QTY);
                txtRemark.Text = SETUPGROUPMETHODTREATMENT.REMARKS;
                cbTMEditAutoTick.Checked = SETUPGROUPMETHODTREATMENT.AutoTick;
                htmlTitleUpdateTreatment.InnerHtml = "Edit Treatment Item";
                btnUpdateTreatment.Text = "Update";

                switch (SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE)
                {
                    case 1:
                        txtAmt.Enabled = false;
                        txtQty.Enabled = false;
                        break;
                    case 2:
                        txtAmt.Enabled = true;
                        txtQty.Enabled = false;
                        break;
                    case 3:
                        txtAmt.Enabled = false;
                        txtQty.Enabled = true;
                        break;
                    default:
                        txtAmt.Enabled = true;
                        txtQty.Enabled = true;
                        break;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalTreatment();", true);
                foreach (GridViewRow gvrow in gvTreatmentList.Rows)
                {
                    if (gvrow.RowIndex == row.RowIndex)
                    {

                        if (gvrow.RowState == DataControlRowState.Selected)
                        {
                            gvrow.CssClass = "selected";

                        }
                        else
                            gvrow.CssClass = gvTreatmentList.AlternatingRowStyle.CssClass.ToString() + " selected";
                    }
                    else
                    {

                        if (gvrow.RowState == DataControlRowState.Alternate)
                        {
                            gvrow.CssClass = gvTreatmentList.AlternatingRowStyle.CssClass;
                        }
                        else
                            gvrow.CssClass = "";


                    }
                }

            }




        }

        protected void gvTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvDoctorList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvDoctorList.Rows[index];
            if (e.CommandName == "DeleteDoctorItem")
            {

                SETUPGROUPMETHODDOCTOR _SETUPGROUPMETHODDOCTOR = new SETUPGROUPMETHODDOCTOR();
                _SETUPGROUPMETHODDOCTOR.GroupMethodID = Convert.ToInt32(gvDoctorList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODDOCTOR.DoctorCode = gvDoctorList.DataKeys[row.RowIndex].Values["DoctorCode"].ToString();
                _SETUPGROUPMETHODDOCTOR.DoctorName = gvDoctorList.DataKeys[row.RowIndex].Values["DoctorName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHODDOCTOR(appConnDBInfo).InActive(_SETUPGROUPMETHODDOCTOR);
                if (rv.Value)
                {
                    LoadDoctorList(_SETUPGROUPMETHODDOCTOR.GroupMethodID);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete {0}:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHODDOCTOR.DoctorCode, _SETUPGROUPMETHODDOCTOR.DoctorName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);

                }
                

            }
            

        }
        protected void gvComputerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvComputerList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvComputerList.Rows[index];
            if (e.CommandName == "DeleteComputerItem")
            {

                SETUPGROUPMETHODCOMPUTER _SETUPGROUPMETHODCOMPUTER = new SETUPGROUPMETHODCOMPUTER();
                _SETUPGROUPMETHODCOMPUTER.GroupMethodID = Convert.ToInt32(gvComputerList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODCOMPUTER.ComputerCode = gvComputerList.DataKeys[row.RowIndex].Values["ComputerCode"].ToString();
                _SETUPGROUPMETHODCOMPUTER.ComputerName = gvComputerList.DataKeys[row.RowIndex].Values["ComputerName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHODCOMPUTER(appConnDBInfo).InActive(_SETUPGROUPMETHODCOMPUTER);
                if (rv.Value)
                {
                    LoadComputerList(_SETUPGROUPMETHODCOMPUTER.GroupMethodID);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete {0}:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHODCOMPUTER.ComputerCode, _SETUPGROUPMETHODCOMPUTER.ComputerName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);

                }


            }
        }

        protected void gvClinicList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        protected void gvClinicList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvClinicList.Rows[index];
            if (e.CommandName == "DeleteClinicItem")
            {

                SETUPGROUPMETHODCLINIC _SETUPGROUPMETHODCLINIC = new SETUPGROUPMETHODCLINIC();
                _SETUPGROUPMETHODCLINIC.GroupMethodID = Convert.ToInt32(gvClinicList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODCLINIC.ClinicCode = gvClinicList.DataKeys[row.RowIndex].Values["ClinicCode"].ToString();
                _SETUPGROUPMETHODCLINIC.ClinicName = gvClinicList.DataKeys[row.RowIndex].Values["ClinicName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHODCLINIC(appConnDBInfo).InActive(_SETUPGROUPMETHODCLINIC);
                if (rv.Value)
                {
                    LoadClinicList(_SETUPGROUPMETHODCLINIC.GroupMethodID);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete {0}:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHODCLINIC.ClinicCode, _SETUPGROUPMETHODCLINIC.ClinicName);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);

                }


            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            if (CheckValidation())
            {
                try
                {
                    string groupMethodCode = txtGroupMethodCode.Text.Trim();
                    string groupMethodName = txtGroupMethodName.Text.Trim();
                    SETUPGROUPMETHOD _SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                    _SETUPGROUPMETHOD.GroupMethodCode = txtGroupMethodCode.Text.Trim();
                    _SETUPGROUPMETHOD.GroupMethodName = txtGroupMethodName.Text.Trim();

                    //string alertScript01 = string.Format(@"javascript: $(document).ready(function(){{
                    //                        $.notify('test {0} : {1} initial data.',
                    //                            {{
                    //                                className: 'success',
                    //                                position: 'bottom right',
                    //                                clickToHide: true
                    //                            }}
                    //                        );
                    //                      }});   ", _SETUPGROUPMETHOD.GroupMethodCode, _SETUPGROUPMETHOD.GroupMethodName);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                    //                    alertScript01, true);

                    if (!string.IsNullOrEmpty(hfGroupMethodID.Value))
                    {

                        _SETUPGROUPMETHOD.GroupMethodID = Convert.ToInt32(hfGroupMethodID.Value) > 0 ? Convert.ToInt32(hfGroupMethodID.Value) : 0;
                        ReturnValue rv = new BLSETUPGROUPMETHOD(appConnDBInfo).Update(_SETUPGROUPMETHOD);

                        if (rv.Value)
                        {
                            InitSetupGroupMethod();
                            LoadSetupGroupMethodList();
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Update {0} : {1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHOD.GroupMethodCode, _SETUPGROUPMETHOD.GroupMethodName);
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
                        //string alertScript01 = string.Format(@"javascript: $(document).ready(function(){{
                        //                    $.notify('Insert 01.',
                        //                        {{
                        //                            className: 'success',
                        //                            position: 'bottom right',
                        //                            clickToHide: true
                        //                        }}
                        //                    );
                        //                  }});   ", _SETUPGROUPMETHOD.GroupMethodCode, _SETUPGROUPMETHOD.GroupMethodName);
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                        //                    alertScript01, true);

                        ReturnValue dup = new BLSETUPGROUPMETHOD(appConnDBInfo).CheckDup(_SETUPGROUPMETHOD);

                        //string alertScript02 = string.Format(@"javascript: $(document).ready(function(){{
                        //                    $.notify('Insert 01  {0}.',
                        //                        {{
                        //                            className: 'success',
                        //                            position: 'bottom right',
                        //                            clickToHide: true
                        //                        }}
                        //                    );
                        //                  }});   ", dup.Value.ToString());
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                        //                    alertScript02, true);

                        if (!dup.Value)
                        {
                            

                            ReturnValue rv = new BLSETUPGROUPMETHOD(appConnDBInfo).Insert(_SETUPGROUPMETHOD);

                            //string alertScript03 = string.Format(@"javascript: $(document).ready(function(){{
                            //                $.notify('Insert 02  {0}.',
                            //                    {{
                            //                        className: 'success',
                            //                        position: 'bottom right',
                            //                        clickToHide: true
                            //                    }}
                            //                );
                            //              }});   ", rv.Exception.ToString());
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                            //                    alertScript03, true);

                            if (rv.Value)
                            {
                                InitSetupGroupMethod();
                                LoadSetupGroupMethodList();
                                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Insert {0} : {1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHOD.GroupMethodCode, _SETUPGROUPMETHOD.GroupMethodName);
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
                                            $.notify('Group Method Code is duplicated in data setup',
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

        protected void btnAddMedicine_Click(object sender, EventArgs e)
        {

                   if (CheckMedicineValidation())
            {
                VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(hfMedicine.Value);//ddlMedicine.SelectedItem.Value);
                SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                SETUPGROUPMETHODMEDICINE.MedicineCode = hfMedicine.Value; //ddlMedicine.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(hfGroupMethodID.Value);
                SETUPGROUPMETHODMEDICINE.MedicineName_EN = VT_STOCK_MASTER.EngName;
                SETUPGROUPMETHODMEDICINE.MedicineName_TH = VT_STOCK_MASTER.ThaiName;
                SETUPGROUPMETHODMEDICINE.QTY = Convert.ToDouble(txtMedQTY.Text);
                SETUPGROUPMETHODMEDICINE.AMT = Convert.ToDouble(txtMedAMT.Text);
                SETUPGROUPMETHODMEDICINE.UnitPrice = Convert.ToDouble(txtMedUnitPrice.Text);
                SETUPGROUPMETHODMEDICINE.UnitCode = ddlUnit.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.DoseTypeCode = ddlDoseType.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.DoseQTY = ddlDoseQTY.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.DoseUnitCode = ddlDoseUnit.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.DoseCode = ddlDoseCode.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.AUXLABEL1 = ddlAuxLabel1.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.AUXLABEL2 = ddlAuxLabel2.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.AUXLABEL3 = ddlAuxLabel3.SelectedItem.Value;
                SETUPGROUPMETHODMEDICINE.Remark = txtMedRemark.Text.Trim();
                SETUPGROUPMETHODMEDICINE.AutoTick = cbMedAutoTick.Checked;

                AddMedicine(SETUPGROUPMETHODMEDICINE);


                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('This medicine item was already entered. Please try another item.',
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

        private void AddMedicine(SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE)
        {

            if (new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).CheckDup(SETUPGROUPMETHODMEDICINE) == false)
            {
                ReturnValue rv = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).Insert(SETUPGROUPMETHODMEDICINE);
                if (rv.Value == true)
                {
                    //ddlMedicine.SelectedIndex = 0;
                    LoadMedicineList(SETUPGROUPMETHODMEDICINE.GroupMethodID);


                    string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                           "     $(\"[id *= 'txtMedQTY'],[id *= 'txtMedAMT'],[id *= 'txtMedUnitPrice'],[id *= 'txtMedRemark'],[id*='hfMedicine']\").val('');" +
                                           "     $(\"[id *= 'ddlMedicine'],[id *= 'ddlUnit'],[id *= 'ddlDoseType'],[id *= 'ddlDoseQTY'],[id *= 'ddlDoseUnit'],[id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']\").val(null).trigger('change');" +

                                           "     $(\"[id *= 'ddlMedicine']\").prop('disabled', false);" +
                                           "     $(\"[id*='cbMedAutoTick']\").prop('checked', false); " +
                                           " $.notify('Add {0} : {1} completed.', " +
                                           "      {{ " +
                                           "          className: 'success', " +
                                           "          position: 'bottom right', " +
                                           "          clickToHide: true " +
                                           "      }} " +
                                           "  ); " +
                                           " }}); ", SETUPGROUPMETHODMEDICINE.MedicineCode, SETUPGROUPMETHODMEDICINE.MedicineName);
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
                                            $.notify('This medicine item was already entered. Please try another item.',
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


        private bool CheckValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string groupMethodCode = txtGroupMethodCode.Text.Trim();
            string groupMethodName = txtGroupMethodName.Text.Trim();
            if (groupMethodCode == string.Empty || groupMethodName == string.Empty)
            {
                message = string.IsNullOrEmpty(groupMethodCode) ? "   - Group Method Code " : "";
                message += (!string.IsNullOrEmpty(message) ? "\\n" : "");
                message += string.IsNullOrEmpty(groupMethodName) ? "  - Group Method Name " : "";
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

        private bool CheckMedicineValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string medicineCode = string.IsNullOrEmpty(hfMedicine.Value) ? string.Empty : hfMedicine.Value;  //string.IsNullOrEmpty(ddlMedicine.SelectedItem.Value)?string.Empty: ddlMedicine.SelectedItem.Value;
            string unit = ddlUnit.SelectedItem.Value;
            double QTY;
            bool isQTY = Double.TryParse(txtMedQTY.Text.Trim(), out QTY);
            double AMT;
            bool isAMT = Double.TryParse(txtMedAMT.Text.Trim(), out AMT);
            double unitPrice;
            bool isUnitPrice = Double.TryParse(txtMedUnitPrice.Text.Trim(), out unitPrice);
            

            if (medicineCode == string.Empty || unit == string.Empty || isUnitPrice == false ||
                isQTY == false || QTY <= 0 ||
                isAMT == false || AMT <= 0)
            {
                message = string.IsNullOrEmpty(medicineCode) ? "   - Medicine Name " : "";
                message += string.IsNullOrEmpty(unit) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Unit " : "";
                message += QTY <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - QTY " : "";
                message += AMT <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - AMT " : "";
                message += unitPrice <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Unit Price " : "";
                message = "Please fill out this field\\n" + message;
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }}); ", message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

                valid = false;
            }
            return valid;
        }


        private bool CheckTreatmentValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string treatmentCode = string.IsNullOrEmpty(hdTreatmentCode.Value) ? string.Empty : hdTreatmentCode.Value;
            double AMT;
            bool isAMT = Double.TryParse(txtAmt.Text.Trim(), out AMT);
            double QTY;
            bool isQTY = Double.TryParse(txtQty.Text.Trim(), out QTY);
            

            if (treatmentCode == string.Empty ||
                isQTY == false || QTY <= 0 ||
                isAMT == false || AMT <= 0)
            {
                message = string.IsNullOrEmpty(treatmentCode) ? "   - Treatment Name " : "";
                message += QTY <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - QTY " : "";
                message += AMT <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - AMT " : "";
                message = "Please fill out this field\\n" + message;
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }}); ", message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

                valid = false;
            }
            return valid;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            InitSetupGroupMethod();
            //InitScriptSetupGroupMethod();
            LoadSetupGroupMethodList();
        }

        protected void btnUpdateTreatment_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckTreatmentValidation())
                {
                    VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(hdTreatmentCode.Value);

                    SETUPGROUPMETHODTREATMENT _SETUPGROUPMETHODTREATMENT = new SETUPGROUPMETHODTREATMENT();
                    _SETUPGROUPMETHODTREATMENT.GroupMethodID = Convert.ToInt32(hdGroupMethodID.Value);
                    _SETUPGROUPMETHODTREATMENT.TreatmentCode = treatment.CODE;
                    _SETUPGROUPMETHODTREATMENT.TreatmentName = treatment.Name;
                    _SETUPGROUPMETHODTREATMENT.CHARGECODE = hdChargeCode.Value;
                    _SETUPGROUPMETHODTREATMENT.TREATMENTENTRYSTYLE = Convert.ToInt32(hdTreatmentEntryStyle.Value);
                    _SETUPGROUPMETHODTREATMENT.AMT = Convert.ToDouble(txtAmt.Text);
                    _SETUPGROUPMETHODTREATMENT.QTY = Convert.ToDouble(txtQty.Text);
                    _SETUPGROUPMETHODTREATMENT.REMARKS = txtRemark.Text.Trim();
                    _SETUPGROUPMETHODTREATMENT.AutoTick = cbTMEditAutoTick.Checked;

                    if (btnUpdateTreatment.Text == "Add")
                    {
                        if (new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).CheckDup(_SETUPGROUPMETHODTREATMENT) == false)
                        {

                            ReturnValue rv = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).Insert(_SETUPGROUPMETHODTREATMENT);
                            if (rv.Value == true)
                            {
                                ddlTreatment.SelectedIndex = 0;
                                LoadTreatmentList(_SETUPGROUPMETHODTREATMENT.GroupMethodID);

                                string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                        "     $(\"[id *= 'ddlTreatment']\").val(null).trigger('change');" +
                                                        "     $(\"[id *= 'ddlTreatment']\").prop('disabled', false);" +
                                                        "     $(\"[id*='cbTMAutoTick']\").prop('checked', false); " +
                                                        " $('#modalEditTreatment').modal('hide'); " +
                                                        " $.notify('Add {0} : {1} completed.', " +
                                                        "      {{ " +
                                                        "          className: 'success', " +
                                                        "          position: 'bottom right', " +
                                                        "          clickToHide: true " +
                                                        "      }} " +
                                                        "  ); " +
                                                        " }}); ", _SETUPGROUPMETHODTREATMENT.TreatmentCode, _SETUPGROUPMETHODTREATMENT.TreatmentName);
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
                                            $.notify('This treatment item was already entered. Please try another item.',
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
                    else
                    {
                        ReturnValue rv = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).Update(_SETUPGROUPMETHODTREATMENT);
                        if (rv.Value == true)
                        {
                            LoadTreatmentList(_SETUPGROUPMETHODTREATMENT.GroupMethodID);
                            string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                   " $('#modalEditTreatment').modal('hide'); " +
                                                   " $.notify('Update completed.', " +
                                                   "      {{ " +
                                                   "          className: 'success', " +
                                                   "          position: 'bottom right', " +
                                                   "          clickToHide: true " +
                                                   "      }} " +
                                                   "  ); " +
                                                   " }}); ");
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                      alertScript, true);
                        }
                        else
                        {

                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $('#modalEditTreatment').modal('hide'); 
                                            $.notify('Error : {0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", rv.Exception.ToString());
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Error : {0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
            }
        }

        protected void gvMedicineList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvMedicineList.Rows[index];
            if (e.CommandName == "DeleteMedicineItem")
            {

                SETUPGROUPMETHODMEDICINE _SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                _SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODMEDICINE.MedicineCode = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineCode"].ToString();
                _SETUPGROUPMETHODMEDICINE.MedicineName_TH = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineName"].ToString();
                ReturnValue rv = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).InActive(_SETUPGROUPMETHODMEDICINE);
                if (rv.Value)
                {
                    LoadMedicineList(_SETUPGROUPMETHODMEDICINE.GroupMethodID);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete {0}:{1} completed.',
                                                {{
                                                    className: 'success',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", _SETUPGROUPMETHODMEDICINE.MedicineCode, _SETUPGROUPMETHODMEDICINE.MedicineName_TH);
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
            else if (e.CommandName == "EditMedicineItem")
            {
                SETUPGROUPMETHODMEDICINE _SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                _SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                _SETUPGROUPMETHODMEDICINE.MedicineCode = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineCode"].ToString();

                SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(_SETUPGROUPMETHODMEDICINE);
                hdMedicineCode.Value = SETUPGROUPMETHODMEDICINE.MedicineCode;
                hdMedicineGroupMethodID.Value = SETUPGROUPMETHODMEDICINE.GroupMethodID.ToString();
                lblMedicineName.Text = string.Format("[{0}] {1}", SETUPGROUPMETHODMEDICINE.MedicineCode, SETUPGROUPMETHODMEDICINE.MedicineName);
                txtEditMedQTY.Text = string.Format("{0:0.##}", SETUPGROUPMETHODMEDICINE.QTY);
                txtEditMedAMT.Text = string.Format("{0:0.##}", SETUPGROUPMETHODMEDICINE.AMT);
                txtEditUnitPrice.Text = string.Format("{0:0.##}", SETUPGROUPMETHODMEDICINE.UnitPrice);
                ddlEditUnit.ClearSelection();
                ddlEditUnit.Items.FindByValue(SETUPGROUPMETHODMEDICINE.UnitCode).Selected = true;
                ddlEditDoseType.ClearSelection();
                ddlEditDoseType.Items.FindByValue(SETUPGROUPMETHODMEDICINE.DoseTypeCode).Selected = true;
                ddlEditDoseQTY.ClearSelection();
                ddlEditDoseQTY.Items.FindByValue(SETUPGROUPMETHODMEDICINE.DoseQTY).Selected = true;
                ddlEditDoseUnit.ClearSelection();
                ddlEditDoseUnit.Items.FindByValue(SETUPGROUPMETHODMEDICINE.DoseUnitCode).Selected = true;
                ddlEditDoseCode.ClearSelection();
                ddlEditDoseCode.Items.FindByValue(SETUPGROUPMETHODMEDICINE.DoseCode).Selected = true;
                ddlEditAuxLabel1.ClearSelection();
                ddlEditAuxLabel1.Items.FindByValue(SETUPGROUPMETHODMEDICINE.AUXLABEL1).Selected = true;
                ddlEditAuxLabel2.ClearSelection();
                ddlEditAuxLabel2.Items.FindByValue(SETUPGROUPMETHODMEDICINE.AUXLABEL2).Selected = true;
                ddlEditAuxLabel3.ClearSelection();
                ddlEditAuxLabel3.Items.FindByValue(SETUPGROUPMETHODMEDICINE.AUXLABEL3).Selected = true;
                txtEditMedRemark.Text = SETUPGROUPMETHODMEDICINE.Remark;
                cbMedEditAutoTick.Checked = SETUPGROUPMETHODMEDICINE.AutoTick;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalMedicine();", true);

                
                foreach (GridViewRow gvrow in gvMedicineList.Rows)
                {
                    if (gvrow.RowIndex == row.RowIndex)
                    {

                        if (gvrow.RowState == DataControlRowState.Selected)
                        {
                            gvrow.CssClass = "selected";

                        }
                        else
                            gvrow.CssClass = gvMedicineList.AlternatingRowStyle.CssClass.ToString() + " selected";
                    }
                    else
                    {

                        if (gvrow.RowState == DataControlRowState.Alternate)
                        {
                            gvrow.CssClass = gvMedicineList.AlternatingRowStyle.CssClass;
                        }
                        else
                            gvrow.CssClass = "";


                    }
                }

            }

        }

        protected void gvMedicineList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void ddlMedicine_SelectedIndexChanged(object sender, EventArgs e)
        {

            string stockCode = hfMedicine.Value; // ddlMedicine.SelectedValue;
            //if (!string.IsNullOrEmpty(stockCode))
            //{
            //    ddlUnit.ClearSelection();
            //    VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(stockCode);
            //    //ddlUnit.Items.FindByValue(VT_STOCK_MASTER.UnitCode01).Selected = true;

            //    if (ddlUnit.Items.Contains(ddlUnit.Items.FindByValue(VT_STOCK_MASTER.UnitCode01.Trim())) == true)
            //    {
            //        ddlUnit.SelectedValue = VT_STOCK_MASTER.UnitCode01;
            //    }
            //    else
            //    {
            //        ddlUnit.SelectedIndex = -1;
            //    }

            //    txtMedQTY.Text = "1";
            //    txtMedAMT.Text = string.Format("{0:0.##}", VT_STOCK_MASTER.Price);
            //}
        }





        private bool CheckUpdateMedicineValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string medicineCode = string.IsNullOrEmpty(hdMedicineCode.Value) ? string.Empty : hdMedicineCode.Value;  //string.IsNullOrEmpty(ddlMedicine.SelectedItem.Value)?string.Empty: ddlMedicine.SelectedItem.Value;
            string unit = ddlEditUnit.SelectedItem.Value;
            double QTY;
            bool isQTY = Double.TryParse(txtEditMedQTY.Text.Trim(), out QTY);
            double AMT;
            bool isAMT = Double.TryParse(txtEditMedAMT.Text.Trim(), out AMT);
            double unitPrice;
            bool isUnitPrice = Double.TryParse(txtEditUnitPrice.Text.Trim(), out unitPrice);


            if (medicineCode == string.Empty || unit == string.Empty || isUnitPrice == false ||
                isQTY == false || QTY <= 0 ||
                isAMT == false || AMT <= 0)
            {
                message = string.IsNullOrEmpty(medicineCode) ? "   - Medicine Name " : "";
                message += string.IsNullOrEmpty(unit) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Unit " : "";
                message += QTY <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - QTY " : "";
                message += AMT <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - AMT " : "";
                message += unitPrice <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Unit Price " : "";
                message = "Please fill out this field\\n" + message;
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }}); ", message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);

                valid = false;
            }
            return valid;
        }


        protected void btnUpdateMedicine_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUpdateMedicineValidation())
                {
                    VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(hdMedicineCode.Value);
                    SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                    SETUPGROUPMETHODMEDICINE _SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                    _SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(hdMedicineGroupMethodID.Value);
                    _SETUPGROUPMETHODMEDICINE.MedicineCode = hdMedicineCode.Value;
                    _SETUPGROUPMETHODMEDICINE.MedicineName_EN = VT_STOCK_MASTER.EngName;
                    _SETUPGROUPMETHODMEDICINE.MedicineName_TH = VT_STOCK_MASTER.ThaiName;
                    _SETUPGROUPMETHODMEDICINE.AMT = Convert.ToDouble(txtEditMedAMT.Text);
                    _SETUPGROUPMETHODMEDICINE.QTY = Convert.ToDouble(txtEditMedQTY.Text);
                    _SETUPGROUPMETHODMEDICINE.UnitPrice = Convert.ToDouble(txtEditUnitPrice.Text);
                    _SETUPGROUPMETHODMEDICINE.UnitCode = ddlEditUnit.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.DoseTypeCode = ddlEditDoseType.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.DoseQTY = ddlEditDoseQTY.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.DoseUnitCode = ddlEditDoseUnit.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.DoseCode = ddlEditDoseCode.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.AUXLABEL1 = ddlEditAuxLabel1.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.AUXLABEL2 = ddlEditAuxLabel2.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.AUXLABEL3 = ddlEditAuxLabel3.SelectedItem.Value;
                    _SETUPGROUPMETHODMEDICINE.Remark = txtEditMedRemark.Text.Trim();
                    _SETUPGROUPMETHODMEDICINE.AutoTick = cbMedEditAutoTick.Checked;



                    ReturnValue rv = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).Update(_SETUPGROUPMETHODMEDICINE);
                    if (rv.Value == true)
                    {
                        LoadMedicineList(_SETUPGROUPMETHODMEDICINE.GroupMethodID);
                        string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                               " $('#modalEditMedicine').modal('hide'); " +
                                               " $.notify('Update completed.', " +
                                               "      {{ " +
                                               "          className: 'success', " +
                                               "          position: 'bottom right', " +
                                               "          clickToHide: true " +
                                               "      }} " +
                                               "  ); " +
                                               " }}); ");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                  alertScript, true);
                    }
                    else
                    {

                        string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $('#modalEditMedicine').modal('hide'); 
                                            $.notify('Error : {0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", rv.Exception.ToString());
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                            alertScript, true);
                    }
                }
            }
            catch (Exception ex)
            {
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Error : {0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", ex.Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
            }
        }



        [System.Web.Services.WebMethod]
        public static string SearchMedicine(string textSearch , int startPage , int per_page)
        {

         VT_STOCK_MASTER VT_STOCKMASTER = new VT_STOCK_MASTER();
            var result = new BLVT_STOCK_MASTER(extConnDBInfo).SearchStockMasterSetup(textSearch, startPage, per_page);
            var json = JsonConvert.SerializeObject(result);
            return json;

        }

        [System.Web.Services.WebMethod]
        public static string GetStockMasterByKey(string stockCode)
        {

            SETUPGROUPMETHODMEDICINE methodMed = new SETUPGROUPMETHODMEDICINE();
            if (!string.IsNullOrEmpty(stockCode))
            {
                
                VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(stockCode);
                methodMed.MedicineCode = VT_STOCK_MASTER.StockCode;
                methodMed.MedicineName_TH = VT_STOCK_MASTER.ThaiName;
                methodMed.MedicineName_EN = VT_STOCK_MASTER.EngName;
                methodMed.UnitCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.UnitCode; //VT_STOCK_MASTER.UnitCode01.Trim();
                methodMed.QTY = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.Qty; //1
                methodMed.AMT = VT_STOCK_MASTER.Price;
                methodMed.UnitPrice = VT_STOCK_MASTER.Price;

                methodMed.DoseTypeCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseType;
                methodMed.DoseQTY = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseQtyCode;
                methodMed.DoseUnitCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseUnitCode;
                methodMed.DoseCode = VT_STOCK_MASTER.STOCK_LEARNDOSEINFO.DoseCode;

                methodMed.AUXLABEL1 = VT_STOCK_MASTER.AUXLABEL1;
                methodMed.AUXLABEL2 = VT_STOCK_MASTER.AUXLABEL2;
                methodMed.AUXLABEL3 = VT_STOCK_MASTER.AUXLABEL3;
                methodMed.Remark = string.Empty;

            }
            var json = JsonConvert.SerializeObject(methodMed);
            return json;
        }

        protected void btnClearTreatment_Click(object sender, EventArgs e)
        {
            string alertScript = string.Format(@"javascript: $(document).ready(function(){{

                                           $(""[id*='ddlTreatment']"").select2({{

                                                placeholder: ""Select Treatment"",
                                                width: '100%',
                                                allowClear: true,
                                                disabled: false,

                                            }});
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
                                            $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', false);
                                            $(""[id*='txtMedQTY'],[id*='txtMedAMT'],[id*='txtMedUnitPrice'],[id*='txtMedRemark']"").val('').prop('disabled', false); 
                                            $(""[id*='btnClearTreatment'],[id*='btnClearMedicine']"").prop('disabled', false);
                                            $(""[id*='btnAddTreatment']"").prop('disabled', true);
                                            $(""[id*='cbTMAutoTick']"").prop('checked', false);
                                            }});   ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);
        }

        protected void btnClearDoctor_Click(object sender, EventArgs e)
        {
            string alertScript = string.Format(@"javascript: $(document).ready(function(){{

                                           $(""[id*='ddlDoctor']"").select2({{

                                                placeholder: ""Select Dotor"",
                                                width: '100%',
                                                allowClear: true,
                                                disabled: false,

                                            }});
                                            $(""[id*='ddlDoctor'],[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
                                            $(""[id*='ddlDoctor'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', false);
                                            $(""[id*='btnClearDoctor'],[id*='btnClearTreatment'],[id*='btnClearMedicine']"").prop('disabled', false);
                                            $(""[id*='btnAddDoctor']"").prop('disabled', true);
                                       
                                            }});   ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);
        }

        //protected void btnClearMedicine_Click(object sender, EventArgs e)
        //{
        //    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
        //                                    $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
        //                                    $(""[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', false);
        //                                    $(""[id*='txtMedQTY'],[id*='txtMedAMT'],[id*='txtMedUnitPrice'],[id*='txtMedRemark']"").val('').prop('disabled', false); 
        //                                    $(""[id*='btnClearTreatment'],[id*='btnClearMedicine']"").prop('disabled', false);
        //                                    }});   ");
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
        //                        alertScript, true);
        //}

        protected void btnClearComputer_Click(object sender, EventArgs e)
        {
            string alertScript = string.Format(@"javascript: $(document).ready(function(){{

                                           $(""[id*='ddlComputer']"").select2({{

                                                placeholder: ""Select Computer"",
                                                width: '100%',
                                                allowClear: true,
                                                disabled: false,

                                            }});
                                            $(""[id*='ddlComputer'],[id*='ddlDoctor'],[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
                                            $(""[id*='ddlComputer'],[id*='ddlDoctor'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', false);
                                            $(""[id*='btnClearComputer'],[id*='btnClearDoctor'],[id*='btnClearTreatment'],[id*='btnClearMedicine']"").prop('disabled', false);
                                            $(""[id*='btnAddComputer']"").prop('disabled', true);
                                       
                                            }});   ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);
        }

        protected void btnClearClinic_Click(object sender, EventArgs e)
        {
            string alertScript = string.Format(@"javascript: $(document).ready(function(){{

                                           $(""[id*='ddlClinic']"").select2({{

                                                placeholder: ""Select Clinic"",
                                                width: '100%',
                                                allowClear: true,
                                                disabled: false,

                                            }});
                                            $(""[id*='ddlClinic'],[id*='ddlComputer'],[id*='ddlDoctor'],[id*='ddlTreatment'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").val(null).trigger('change');
                                            $(""[id*='ddlClinic'],[id*='ddlComputer'],[id*='ddlDoctor'],[id*='ddlMedicine'],[id*='ddlUnit'],[id*='ddlDoseType'],[id*='ddlDoseQTY'],[id*='ddlDoseUnit'],[id*='ddlDoseCode'],[id*='ddlAuxLabel1'],[id*='ddlAuxLabel2'],[id*='ddlAuxLabel3']"").prop('disabled', false);
                                            $(""[id*='btnClearClinic'],[id*='btnClearComputer'],[id*='btnClearDoctor'],[id*='btnClearTreatment'],[id*='btnClearMedicine']"").prop('disabled', false);
                                            $(""[id*='btnAddClinic']"").prop('disabled', true);
                                       
                                            }});   ");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                alertScript, true);
        }
    }
}