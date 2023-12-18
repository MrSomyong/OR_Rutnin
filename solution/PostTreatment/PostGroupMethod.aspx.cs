using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Info;
using System.Data;
using System.Net;

namespace solution.PostTreatment
{
    public partial class PostGroupMethod : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;

        public string HN { get; set; }
        public string VN { get; set; }
        public int suffix { get; set; }
        public DateTime visitDate { get; set; }
        public string CloseVN { get; set; }
        public string DoctorCode { get; set; }
        public string ClinicCode { get; set; }
        public string HostName { get; set; }

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
            HN = Request.QueryString["hn"];
            VN = Request.QueryString["vn"];
            visitDate = DateTime.ParseExact(Request.QueryString["vndate"], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            suffix = Convert.ToInt32(Request.QueryString["suffix"]);

            if (!IsPostBack)
            {
                InitPostMenu();
                setdefaultvalue();
                if (Request.QueryString["hn"] != null)
                {


                    VT_VNMASTER _VT_VNMASTER = new VT_VNMASTER();
                    _VT_VNMASTER.HN = HN;
                    _VT_VNMASTER.VN = VN;
                    _VT_VNMASTER.VISITDATE = visitDate;
                    _VT_VNMASTER.SUFFIX = suffix;
                    LoadHyperLinkControl();
                    LoadOPDVisitInfo(_VT_VNMASTER);
                    LoadPatientInfo(HN);

                    TREATMENT treatment = new TREATMENT();
                    treatment.VN = VN;
                    treatment.VISITDATE = visitDate;
                    treatment.SUFFIX = suffix;
                    LoadOPDTreatmentList(treatment);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }

                
                LoadStore();
                LoadGroupMethod();
                LoadOrderType();
                LoadDoseUnit();
                LoadDoseQTY();
                LoadDoseType();
                LoadDoseCode();
                LoadUnit();
                LoadAuxLabel();
                InitSetupGroupMethod();
                LoadDoctorList();
                //LoadTreatmentMethod();
                //LoadTreatmentItem();
            }
        }
        private void LoadHyperLinkControl()
        {

            List<SETUPHYPERLINK> lstLink = (List<SETUPHYPERLINK>)new BLSETUPHYPERLINK(appConnDBInfo).SearchAll().Where(l => l.IsShow == true).ToList();
            Int32 i; //create a integer variable
            for (i = 0; i < lstLink.Count(); i++) // will generate 10 LinkButton
            {
                lstLink[i].LinkURL = System.Text.RegularExpressions.Regex.Replace(lstLink[i].LinkURL, @"\[HN\]", HN, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                lstLink[i].LinkURL = System.Text.RegularExpressions.Regex.Replace(lstLink[i].LinkURL, @"\[VN\]", VN, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                lstLink[i].LinkURL = System.Text.RegularExpressions.Regex.Replace(lstLink[i].LinkURL, @"\[Visitdate\]", visitDate.ToString("yyyyMMdd"), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                lstLink[i].LinkURL = System.Text.RegularExpressions.Regex.Replace(lstLink[i].LinkURL, @"\[UserID\]", Session["USERID"].ToString(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                HyperLink hl = new HyperLink();
                hl = new HyperLink();
                hl.Text = string.Format("<span class=\"text\">{0}</span>", lstLink[i].LinkName);
                hl.NavigateUrl = lstLink[i].LinkURL;
                hl.Target = "_blank";
                hl.CssClass = "btn btn-outline-info btn-sm px-3 mr-2";
                phHyperLink.Controls.Add(hl);
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
        private void LoadStore()
        {
            try
            {


                ListItem litStore = new ListItem();
                litStore.Text = "-Please Select-";
                litStore.Value = "";
                VT_STORE VT_STORE = new VT_STORE();

                List<VT_STORE> lstStore = new BLVT_STORE(extConnDBInfo).SearchAll();
                ddlStore.DataSource = lstStore;
                ddlStore.DataValueField = "StoreCode";
                ddlStore.DataTextField = "StoreName";
                ddlStore.DataBind();
                ddlStore.Items.Insert(0, litStore);

                //HostName = Dns.GetHostName();
                SETUPCOMPUTER SetupComputer = new BLSETUPCOMPUTER(appConnDBInfo).SearchByKey(HostName);

                ddlStore.ClearSelection();
                if (!string.IsNullOrEmpty(SetupComputer.DefaultStoreCode))
                    ddlStore.Items.FindByValue(SetupComputer.DefaultStoreCode).Selected = true;

                ddlEditStore.DataSource = lstStore;
                ddlEditStore.DataValueField = "StoreCode";
                ddlEditStore.DataTextField = "StoreName";
                ddlEditStore.DataBind();
                ddlEditStore.Items.Insert(0, litStore);

                ddlEditStore.ClearSelection();
                if (!string.IsNullOrEmpty(SetupComputer.DefaultStoreCode))
                    ddlEditStore.Items.FindByValue(SetupComputer.DefaultStoreCode).Selected = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        private void LoadOrderType()
        {
            try
            {
                List<TYPEOFCHARGE> typeOfCharges = new List<TYPEOFCHARGE>();
                typeOfCharges.Add(new TYPEOFCHARGE { Code = "0", Name = "None" });
                typeOfCharges.Add(new TYPEOFCHARGE { Code = "1", Name = "Free" });
                typeOfCharges.Add(new TYPEOFCHARGE { Code = "2", Name = "Refund" });

                ListItem litOrderType = new ListItem();
                litOrderType.Text = "-Please Select-";
                litOrderType.Value = "";

                ddlOrderType.DataSource = typeOfCharges;
                ddlOrderType.DataValueField = "Code";
                ddlOrderType.DataTextField = "Name";
                ddlOrderType.DataBind();
                ddlOrderType.Items.Insert(0, litOrderType);

                ddlEditOrderType.DataSource = typeOfCharges;
                ddlEditOrderType.DataValueField = "Code";
                ddlEditOrderType.DataTextField = "Name";
                ddlEditOrderType.DataBind();
                ddlEditOrderType.Items.Insert(0, litOrderType);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void LoadUnit()
        {
            try
            {
                ListItem litUnit = new ListItem();
                litUnit.Text = "-Please Select-";
                litUnit.Value = "";

                List<UNIT> lstUnit = new BLVT_UNIT(extConnDBInfo).SearchAll();
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

        private void InitPostMenu()
        {
            lnkMain.NavigateUrl = string.Format("/PostTreatment/Main{0}", HttpContext.Current.Request.Url.Query);
            lnkTreatment.NavigateUrl = string.Format("/PostTreatment/PostTreatment{0}", HttpContext.Current.Request.Url.Query);
            lnkDF.NavigateUrl = string.Format("/PostTreatment/PostDF{0}", HttpContext.Current.Request.Url.Query);
            lnkMedicine.NavigateUrl = string.Format("/PostTreatment/PostMedicine{0}", HttpContext.Current.Request.Url.Query);
            lnkGroup.NavigateUrl = string.Format("/PostTreatment/PostGroupMethod{0}", HttpContext.Current.Request.Url.Query);
        }

        private void setdefaultvalue()
        {
            //pnORHEADER.Enabled = false;
            lblHN.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblDateOfBirth.Text = string.Empty;

            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblIDCard.Text = string.Empty;
            lblNationality.Text = string.Empty;
            lblPatientType.Text = string.Empty;
            divError.Visible = false;

        }

        public void LoadOPDVisitInfo(VT_VNMASTER _VT_VNMASTER)
        {
            try
            {
                VT_VNMASTER VT_VNMASTER = new BLVT_VNMASTER(extConnDBInfo).GetVNDetailByKey(_VT_VNMASTER);
                lblVN.Text = string.Format("{0}/{1}", VT_VNMASTER.VN, VT_VNMASTER.SUFFIX);
                lblVisitDate.Text = CultureInfo.GetDatetime(VT_VNMASTER.VISITINDATETIME.Value, YearType.Thai).ToString("dd MMM yyyy");
                lblClinic.Text = VT_VNMASTER.CLINICNAME;
                lblDoctor.Text = VT_VNMASTER.DoctorName;
                hdDoctorCode.Value = VT_VNMASTER.DOCTOR;
                lblRight.Text = VT_VNMASTER.RIGHTNAME;
                DoctorCode = VT_VNMASTER.DOCTOR;
                ClinicCode = VT_VNMASTER.CLINIC;
                hdMedicinePriceType.Value = VT_VNMASTER.MedicinePriceType;
                hdTreatmentPriceType.Value = VT_VNMASTER.TreatmentPriceType;
                HostName = Dns.GetHostName();

                if (VT_VNMASTER.Close == "Y" || VT_VNMASTER.HoldBill == true)
                {
                   
                    ddlGroupMethod.Enabled = false;
                    CloseVN = "Y";
                }


            }
            catch { }

        }
        public void LoadOPDTreatmentList(TREATMENT _TREATMENT)
        {
            try
            {
                List<TREATMENT> lstTREATMENT = new BLTREATMENT(extConnDBInfo,appConnDBInfo).GetTREATMENTByGroupMethodCode(_TREATMENT);
                gvOPDTreatmentList.DataSource = lstTREATMENT;
                gvOPDTreatmentList.DataBind();

            }
            catch { }

        }
        
        private void LoadGroupMethod()
        {
            try
            {
                ListItem litGroupMethod = new ListItem();
                litGroupMethod.Text = "None";
                litGroupMethod.Value = "";


                //List<SETUPGROUPMETHOD> lstGroupMethod = new BLSETUPGROUPMETHOD(appConnDBInfo).SearchAll();
                
                List<SETUPGROUPMETHOD> lstGroupMethod = new BLSETUPGROUPMETHOD(appConnDBInfo).SearchAllFilterBy(DoctorCode,ClinicCode,HostName);
                ddlGroupMethod.DataSource = lstGroupMethod;
                ddlGroupMethod.DataValueField = "GroupMethodCode";
                ddlGroupMethod.DataTextField = "GroupMethodName";
                ddlGroupMethod.DataBind();
                ddlGroupMethod.Items.Insert(0, litGroupMethod);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void LoadTreatmentMethod()
        {
            try
            {
                ListItem litTreatmentMethod = new ListItem();
                litTreatmentMethod.Text = "None";
                litTreatmentMethod.Value = "";


                List<VT_TREATMENTMETHODCODE> lstTreatmentMethod = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentMethodAll();

                ddlGroupMethod.DataSource = lstTreatmentMethod;
                ddlGroupMethod.DataValueField = "MethodCode";
                ddlGroupMethod.DataTextField = "TreatmentMethodName";
                ddlGroupMethod.DataBind();
                ddlGroupMethod.Items.Insert(0, litTreatmentMethod);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void LoadPatientInfo(string hn)
        {
            try
            {
                ORPATIENTVO ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(hn);
                if (!string.IsNullOrEmpty(ORPATIENTVO.HN))
                {
                    byte[] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(hn);
                    if (bytes != null)
                    {
                        string base64String = Convert.ToBase64String(bytes);
                        imgPatient.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    else
                    {
                        string strURL = "http://172.25.41.30/pdp/upload/hn/" + ORPATIENTVO.HN + ".jpg";
                        bool fileExist = new BLDOCUMENT_ITEM(dbInfo).SearchByURL(strURL);

                        if (fileExist)
                        {
                            imgPatient.ImageUrl = strURL;
                        }
                        else
                        {
                            imgPatient.ImageUrl = "../Images/17241-200.png";
                        }
                    }

                    //pnORHEADER.Enabled = true;
                    //setbtnEnable();
                    lblHN.Text = ORPATIENTVO.HN;
                    lblPatientName.Text = ORPATIENTVO.PatientName;
                    lblGender.Text = ORPATIENTVO.Sex;
                    lblDateOfBirth.Text = CultureInfo.GetDatetime(ORPATIENTVO.BirthDateTime.Value, YearType.Thai).ToString("dd MMM yyyy");
                    lblAge.Text = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    lblIDCard.Text = ORPATIENTVO.Ref;
                    lblNationality.Text = ORPATIENTVO.Nationality;
                    lblPatientType.Text = ORPATIENTVO.PatientType;
                    PictureFileName = ORPATIENTVO.PictureFileName;
                }
            }
            catch { }
        }
        private void LoadDoctorList()
        {
            try
            {
                ListItem litSurgeon = new ListItem();
                litSurgeon.Text = "-Please Doctor-";
                litSurgeon.Value = "";
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                //DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                //List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(extConnDBInfo).SearchByKey(DOCTORMASTERVO);
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(extConnDBInfo).SearchByKeyTreatment(DOCTORMASTERVO);

                ddlEditDoctorList.DataSource = lstDOCTORMASTERVO;
                ddlEditDoctorList.DataValueField = "DOCTOR";
                ddlEditDoctorList.DataTextField = "DoctorName";
                ddlEditDoctorList.DataBind();
                ddlEditDoctorList.Items.Insert(0, litSurgeon);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gvOPDTreatmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandName == "delTreatment")
            {


                VT_VNTREAT _VT_VNTREAT;
                VT_VNMEDICINE _VT_VNMEDINE;
                ReturnValue rv = new ReturnValue();
                rv.Value = false;
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOPDTreatmentList.Rows[index];
                string groupType = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["GroupType"].ToString();

                if (groupType == "Treatment")
                {
                    _VT_VNTREAT = new VT_VNTREAT();
                    _VT_VNTREAT.TREATMENTCODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["ITEMCODE"].ToString();
                    _VT_VNTREAT.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                    _VT_VNTREAT.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                    _VT_VNTREAT.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                    _VT_VNTREAT.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                    _VT_VNTREAT.CXLBYUSERCODE = Session["USERNANME"].ToString();
                    rv = new BLVT_VNTREAT(extConnDBInfo).CXLVNTREAT(_VT_VNTREAT);
                }
                else if (groupType == "Medicine")
                {
                    _VT_VNMEDINE = new VT_VNMEDICINE();
                    _VT_VNMEDINE.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                    _VT_VNMEDINE.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                    _VT_VNMEDINE.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                    _VT_VNMEDINE.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                    _VT_VNMEDINE.CXLBYUSERCODE = Session["USERNANME"].ToString();
                    rv = new BLVT_VNMEDICINE(extConnDBInfo).CXLVNMEDICINE(_VT_VNMEDINE);
                }

                    if (rv.Value)
                {
                    TREATMENT TREATMENT = new TREATMENT();
                    TREATMENT.VN = VN;
                    TREATMENT.VISITDATE = visitDate;
                    TREATMENT.SUFFIX = suffix;
                    LoadOPDTreatmentList(TREATMENT);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete item completed.',
                                                {{
                                                    className: 'success',
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
            else if (e.CommandName == "editTreatment")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOPDTreatmentList.Rows[index];
                string groupType = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["GroupType"].ToString();

                if (groupType == "Treatment")
                {
                    SetDefaultTreatmentControl();

                    string itemCode = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["ITEMCODE"].ToString();
                    VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(itemCode);

                    VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                    _VT_VNTREAT.TREATMENTCODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["ITEMCODE"].ToString();
                    _VT_VNTREAT.CHARGECODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["CHARGECODE"].ToString();
                    _VT_VNTREAT.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                    _VT_VNTREAT.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                    _VT_VNTREAT.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                    _VT_VNTREAT.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                    _VT_VNTREAT.TREATMENTENTRYSTYLE = Convert.ToInt32(treatment.TreatmentStyle);
                    _VT_VNTREAT.CHARGEAMT = treatment.StdPrice;//treatment.StdPrice1;
                    VT_VNTREAT VT_VNTREAT = new BLVT_VNTREAT(extConnDBInfo).GetVT_VNTREATByKey(_VT_VNTREAT);
                    hdTreatmentCode.Value = VT_VNTREAT.TREATMENTCODE;
                    hdSubSuffix.Value = VT_VNTREAT.SUBSUFFIX.ToString();
                    ddlOrderType.ClearSelection();
                    ddlOrderType.Items.FindByValue(VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
                    lblTreatmentName.Text = string.Format("[{0}] {1}", VT_VNTREAT.TREATMENTCODE, VT_VNTREAT.TREATMENTNAME);
                    txtAmt.Text = string.Format("{0:0.##}", VT_VNTREAT.AMT);
                    txtQty.Text = string.Format("{0:0.##}", VT_VNTREAT.QTY);
                    hdChargeCode.Value = string.Format("{0:0.##}", VT_VNTREAT.CHARGECODE);
                    txtChargeAMT.Text = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT);
                    hdChargeAMT.Value = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT);
                    hdTreatmentStyle.Value = VT_VNTREAT.TREATMENTENTRYSTYLE.ToString();
                    txtRemark.Text = VT_VNTREAT.REMARKS;
                    ddlOrderType.ClearSelection();
                    ddlOrderType.Items.FindByValue(VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
                    hdZeroPrice.Value = _VT_VNTREAT.ZeroPrice.ToString();
                    ddlEditDoctorList.ClearSelection();
                    ddlEditDoctorList.Items.FindByValue(VT_VNTREAT.DOCTOR.ToString()).Selected = true;
                    hdGroupRequestCode.Value = VT_VNTREAT.GROUPREQUESTCODE;
                    htmlTitleUpdateTreatment.InnerHtml = "Edit Treatment";
                    btnUpdateTreatment.Text = "Update";

                    divOrderType.Style["display"] = "none";
                    divChargeAMT.Style["display"] = "none";
                    divTimeBetween.Style["display"] = "none";
                    txtStartDateTime.Enabled = false;
                    txtEndDateTime.Enabled = false;
                    txtStartTime.Enabled = false;
                    txtEndTime.Enabled = false;
                    txtStartDateTime.Text = string.Empty;
                    txtEndDateTime.Text = string.Empty;
                    txtStartTime.Text = string.Empty;
                    txtEndTime.Text = string.Empty;
                    hfTimeType.Value = string.Empty;
                    hdTime01.Value = string.Empty;
                    hdTime02.Value = string.Empty;
                    hdTime03.Value = string.Empty;
                    hdTime04.Value = string.Empty;
                    hdTime05.Value = string.Empty;
                    hdTime06.Value = string.Empty;
                    switch (_VT_VNTREAT.TREATMENTENTRYSTYLE)
                    {
                        case 1:
                            txtAmt.Enabled = false;
                            divAMT.Style.Remove("display");
                            txtQty.Enabled = false;
                            divQTY.Style.Remove("display");
                            break;
                        case 2: //Std.Adj Amt. type
                            txtAmt.Enabled = true;
                            divAMT.Style.Remove("display");
                            txtQty.Enabled = false;
                            divQTY.Style["display"] = "none";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtAmt.ClientID + "').focus();}, 500);", true);
                            break;
                        case 3:  //Qty type
                            txtAmt.Enabled = false;
                            divAMT.Style.Remove("display");
                            txtQty.Enabled = true;
                            divQTY.Style.Remove("display");
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtQty.ClientID + "').focus();}, 500);", true);
                            break;
                        case 5: //Time between 
                            txtQty.Enabled = false;
                            divQTY.Style["display"] = "none";
                            divTimeBetween.Style.Remove("display");
                            txtStartDateTime.Enabled = true;
                            txtEndDateTime.Enabled = true;
                            txtStartTime.Enabled = true;
                            txtEndTime.Enabled = true;
                            txtStartDateTime.Text = Convert.ToDateTime(VT_VNTREAT.TREATMENTDATETIMEFROM).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                            txtEndDateTime.Text = Convert.ToDateTime(VT_VNTREAT.TREATMENTDATETIMETO).ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                            txtStartTime.Text = Convert.ToDateTime(VT_VNTREAT.TREATMENTDATETIMEFROM).ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                            txtEndTime.Text = Convert.ToDateTime(VT_VNTREAT.TREATMENTDATETIMETO).ToString("HH:mm", new System.Globalization.CultureInfo("en-US"));
                            hfTimeType.Value = VT_VNTREAT.TIMETYPE.ToString();
                            hdTime01.Value = string.Format("{0:0.##}", treatment.Time01);
                            hdTime02.Value = string.Format("{0:0.##}", treatment.Time02);
                            hdTime03.Value = string.Format("{0:0.##}", treatment.Time03);
                            hdTime04.Value = string.Format("{0:0.##}", treatment.Time04);
                            hdTime05.Value = string.Format("{0:0.##}", treatment.Time05);
                            hdTime06.Value = string.Format("{0:0.##}", treatment.Time06);
                            break;
                        default:
                            txtAmt.Enabled = true;
                            divAMT.Style.Remove("display");
                            txtQty.Enabled = true;
                            divQTY.Style.Remove("display");
                            divTimeBetween.Style["display"] = "none";
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtQty.ClientID + "').focus();}, 500);", true);
                            break;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalTreatment();", true);
                }
                else if (groupType == "Medicine")
                {

                    VT_VNMEDICINE _VT_VNMEDINE = new VT_VNMEDICINE();
                    _VT_VNMEDINE.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                    _VT_VNMEDINE.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                    _VT_VNMEDINE.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                    _VT_VNMEDINE.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);

                    SetDefaultMedicineControl();

                    VT_VNMEDICINE VT_VNMEDICINE = new BLVT_VNMEDICINE(extConnDBInfo).GetVT_VNMEDICINEByKey(_VT_VNMEDINE);
                    hdMedicineCode.Value = VT_VNMEDICINE.MEDICINECODE;
                    hdSubSuffix.Value = VT_VNMEDICINE.SUBSUFFIX.ToString();
                    ddlEditStore.ClearSelection();
                    ddlEditStore.Items.FindByValue(VT_VNMEDICINE.STORE).Selected = true;
                    lblMedicineName.Text = string.Format("[{0}] {1}", VT_VNMEDICINE.MEDICINECODE, VT_VNMEDICINE.MEDICINENAME);
                    ddlEditOrderType.ClearSelection();
                    ddlEditOrderType.Items.FindByValue(VT_VNMEDICINE.TYPEOFCHARGE.ToString()).Selected = true;
                    txtEditMedAMT.Text = string.Format("{0:0.##}", VT_VNMEDICINE.AMT);
                    txtEditMedQTY.Text = string.Format("{0:0.##}", VT_VNMEDICINE.QTY);
                    txtEditUnitPrice.Text = string.Format("{0:0.##}", VT_VNMEDICINE.UNITPRICE);
                    ddlEditUnit.ClearSelection();
                    ddlEditUnit.Items.FindByValue(VT_VNMEDICINE.UNITCODE).Selected = true;
                    ddlEditDoseType.ClearSelection();
                    ddlEditDoseType.Items.FindByValue(VT_VNMEDICINE.DOSETYPE).Selected = true;
                    ddlEditDoseQTY.ClearSelection();
                    ddlEditDoseQTY.Items.FindByValue(VT_VNMEDICINE.DOSEQTYCODE).Selected = true;
                    ddlEditDoseUnit.ClearSelection();
                    ddlEditDoseUnit.Items.FindByValue(VT_VNMEDICINE.DOSEUNITCODE).Selected = true;
                    ddlEditDoseCode.ClearSelection();
                    ddlEditDoseCode.Items.FindByValue(VT_VNMEDICINE.DOSECODE).Selected = true;

                    ddlEditAuxLabel1.ClearSelection();
                    ddlEditAuxLabel1.Items.FindByValue(VT_VNMEDICINE.AUXLABEL1).Selected = true;
                    ddlEditAuxLabel2.ClearSelection();
                    ddlEditAuxLabel2.Items.FindByValue(VT_VNMEDICINE.AUXLABEL2).Selected = true;
                    ddlEditAuxLabel3.ClearSelection();
                    ddlEditAuxLabel3.Items.FindByValue(VT_VNMEDICINE.AUXLABEL3).Selected = true;
                    txtEditRemark.Text = VT_VNMEDICINE.DOSEMEMO;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalMedicine();", true);

                }




            }





        }



        private bool CheckValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string orderType = ddlOrderType.SelectedItem.Value;
            string doctor = ddlEditDoctorList.SelectedItem.Value;
            double AMT;
            bool isAMT = Double.TryParse(txtAmt.Text.Trim(), out AMT);
            double QTY;
            bool isQTY = Double.TryParse(txtQty.Text.Trim(), out QTY);
            int zeroPrice = Convert.ToInt32(hdZeroPrice.Value);
            bool isZeroPrice = Int32.TryParse(hdZeroPrice.Value, out zeroPrice);


            //DateTime dateTimeStart = DateTime.ParseExact(txtStartDateTime.Text.Trim(), "dd/MM/yyyy HH:mm",new  System.Globalization.CultureInfo("en-US"));
            //bool isDateTimeStart = CheckDate(dateTimeStart.ToString());
            //DateTime dateTimeEnd = DateTime.ParseExact(txtEndDateTime.Text.Trim(), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"));
            //bool isDateTimeEnd = CheckDate(dateTimeEnd.ToString());
            DateTime? startDateTime = null;
            bool isStartDateTime = true;
            DateTime? endDateTime = null;
            bool isEndDateTime = true;
            if (Convert.ToInt32(hdTreatmentStyle.Value) == 5)
            {
                DateTime tmpStartDateTime = DateTime.MinValue;
                isStartDateTime = Convert.ToInt32(hdTreatmentStyle.Value) != 5 ? true : DateTime.TryParseExact(string.Format("{0} {1}", txtStartDateTime.Text.Trim(), txtStartTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out tmpStartDateTime);
                startDateTime = isStartDateTime ? tmpStartDateTime : startDateTime = null;

                DateTime tmpEndDateTime = DateTime.MinValue;
                isEndDateTime = Convert.ToInt32(hdTreatmentStyle.Value) != 5 ? true : DateTime.TryParseExact(string.Format("{0} {1}", txtEndDateTime.Text.Trim(), txtEndTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out tmpEndDateTime);
                endDateTime = isEndDateTime ? tmpEndDateTime : endDateTime = null;
            }





            if (orderType == string.Empty || doctor == string.Empty ||
                isAMT == false || (AMT <= 0 && zeroPrice == 0) ||
                isQTY == false || QTY <= 0 ||
                isStartDateTime == false || isEndDateTime == false)
            {
                message += string.IsNullOrEmpty(orderType) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Order Type " : "";
                message += string.IsNullOrEmpty(doctor) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Doctor " : "";
                message += AMT <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - AMT " : "";
                message += QTY <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - QTY " : "";
                message += isStartDateTime == false ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Time between" : "";
                message += isEndDateTime == false ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Time between" : "";
                message += startDateTime > endDateTime ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Time between - start date is after end date" : "";
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
                if (isQTY == false || QTY <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtQty);
                }
                else if (isAMT == false || AMT <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtAmt);
                }
                else if (string.IsNullOrEmpty(doctor))
                {
                    alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                                                            setTimeout(function() {{
                                                                                $('[id*=ddlEditDoctorList]').select2('open');
                                                                            }}, 1000);
                                                              }});   ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                   alertScript, true);
                }
                valid = false;
            }
            return valid;
        }


        protected void btnUpdateTreatment_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValidation())
                {
                    VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                    _VT_VNTREAT.VISITDATE = visitDate;
                    _VT_VNTREAT.VN = VN;
                    _VT_VNTREAT.SUFFIX = suffix;
                    _VT_VNTREAT.SUBSUFFIX = Convert.ToInt32(hdSubSuffix.Value);
                    _VT_VNTREAT.TREATMENTCODE = hdTreatmentCode.Value;
                    _VT_VNTREAT.CHARGECODE = hdChargeCode.Value;
                    _VT_VNTREAT.AMT = Convert.ToDouble(txtAmt.Text);
                    _VT_VNTREAT.QTY = Convert.ToDouble(txtQty.Text);
                    _VT_VNTREAT.REMARKS = txtRemark.Text;
                    _VT_VNTREAT.TYPEOFCHARGE = string.IsNullOrEmpty(ddlOrderType.SelectedItem.Value) ? 0 : Convert.ToInt32(ddlOrderType.SelectedItem.Value);
                    _VT_VNTREAT.REVERSE = string.IsNullOrEmpty(ddlOrderType.SelectedItem.Value) ? 0 : (Convert.ToInt32(ddlOrderType.SelectedItem.Value) == 2 ? 1 : 0);
                    _VT_VNTREAT.GROUPREQUESTCODE = hdGroupRequestCode.Value;
                    _VT_VNTREAT.PAIDFLAG = 0;

                    _VT_VNTREAT.ZeroPrice = Convert.ToInt32(hdZeroPrice.Value);


                    DateTime dateTimeStart;
                    bool isDateTimeStart = DateTime.TryParseExact(string.Format("{0} {1}", txtStartDateTime.Text.Trim(), txtStartTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dateTimeStart);
                    _VT_VNTREAT.TREATMENTDATETIMEFROM = isDateTimeStart ? dateTimeStart : _VT_VNTREAT.TREATMENTDATETIMEFROM = null;

                    DateTime dateTimeEnd;
                    bool isDateTimeEnd = DateTime.TryParseExact(string.Format("{0} {1}", txtEndDateTime.Text.Trim(), txtEndTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dateTimeEnd);
                    _VT_VNTREAT.TREATMENTDATETIMETO = isDateTimeEnd ? dateTimeEnd : _VT_VNTREAT.TREATMENTDATETIMETO = null;

                    if (btnUpdateTreatment.Text == "Add")
                    {
                        _VT_VNTREAT.DOCTOR = !string.IsNullOrEmpty(ddlEditDoctorList.SelectedItem.Value) ? ddlEditDoctorList.SelectedItem.Value : (string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value);
                        _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
                        _VT_VNTREAT.TREATMENTENTRYSTYLE = !string.IsNullOrEmpty(hdTreatmentStyle.Value) ? Convert.ToInt32(hdTreatmentStyle.Value) : _VT_VNTREAT.TREATMENTENTRYSTYLE = null;
                        _VT_VNTREAT.TIMETYPE = !string.IsNullOrEmpty(hfTimeType.Value) ? Convert.ToInt32(hfTimeType.Value) : _VT_VNTREAT.TIMETYPE = null;
                        _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();


                        if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT) == false)
                        {
                            ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).Insert(_VT_VNTREAT);
                            if (rv.Value == true)
                            {

                                TREATMENT TREATMENT = new TREATMENT();
                                TREATMENT.VN = VN;
                                TREATMENT.VISITDATE = visitDate;
                                TREATMENT.SUFFIX = suffix;
                                LoadOPDTreatmentList(TREATMENT);
                                VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(_VT_VNTREAT.TREATMENTCODE);
                                string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                  " $('#modalEditTreatment').modal('hide'); " +
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
                        _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(ddlEditDoctorList.SelectedItem.Value) ? null : ddlEditDoctorList.SelectedItem.Value;
                        _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                        _VT_VNTREAT.TREATMENTENTRYSTYLE = !string.IsNullOrEmpty(hdTreatmentStyle.Value) ? Convert.ToInt32(hdTreatmentStyle.Value) : _VT_VNTREAT.TREATMENTENTRYSTYLE = null;
                        _VT_VNTREAT.TIMETYPE = !string.IsNullOrEmpty(hfTimeType.Value) ? Convert.ToInt32(hfTimeType.Value) : _VT_VNTREAT.TIMETYPE = null;
                        ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).UpdateVNTREAT(_VT_VNTREAT);
                        if (rv.Value == true)
                        {
                            TREATMENT TREATMENT = new TREATMENT();
                            TREATMENT.VN = VN;
                            TREATMENT.VISITDATE = visitDate;
                            TREATMENT.SUFFIX = suffix;
                            LoadOPDTreatmentList(TREATMENT);

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

        protected void gvOPDTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (CloseVN == "Y")
                    {
                        Button btnDel = (Button)e.Row.FindControl("btnDeleteTreatment");
                        btnDel.Enabled = false;
                        Button btnEdit = (Button)e.Row.FindControl("btnEditTreatment");
                        btnEdit.Enabled = false;
                    }
                }
            }

           

        }

        private void SetDefaultTreatmentControl()
        {
           
            ddlOrderType.SelectedIndex = 0;
            txtAmt.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtChargeAMT.Text = string.Empty;
        }

        private void SetDefaultMedicineControl()
        {
            ddlEditStore.SelectedIndex = 0;
            ddlEditOrderType.SelectedIndex = 0;
            txtEditMedAMT.Text = string.Empty;
            txtEditMedQTY.Text = string.Empty;
            txtEditUnitPrice.Text = string.Empty;
            ddlEditUnit.SelectedIndex = 0;
            ddlEditDoseType.SelectedIndex = 0;
            ddlEditDoseQTY.SelectedIndex = 0;
            ddlEditDoseUnit.SelectedIndex = 0;
            ddlEditDoseCode.SelectedIndex = 0;
        }



        protected void ddlGroupMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string groupMethodCode = ddlGroupMethod.SelectedItem.Value;
                List<SETUPGROUPMETHODTREATMENT> listTreatment = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByGroupMethodCode(groupMethodCode);
                gvTreatmentList.DataSource = listTreatment;
                gvTreatmentList.DataBind();

                List<SETUPGROUPMETHODMEDICINE> listMedicine = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByGroupMethodCode(groupMethodCode);
                gvMedicineList.DataSource = listMedicine;
                gvMedicineList.DataBind();

               
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        protected void gvTreatmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
           
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvTreatmentList.Rows[index];
                int groupMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                string groupMethodCode = gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodCode"].ToString();
                string itemCode = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();
                string store = ddlStore.SelectedItem.Value;


                InsertTreatment(groupMethodID,groupMethodCode, itemCode);

            }
            
        }

        private bool CheckMedicineValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string store = ddlStore.SelectedItem.Value;
            if (store == string.Empty)
            {
                
                message += string.IsNullOrEmpty(store) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Store " : "";
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

        private void InsertTreatment(int groupMethodID , string groupMethodCode, string itemCode, bool showNotify = true)
        {
            VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(itemCode);
            SETUPGROUPMETHODTREATMENT _setupGroupMethodTreatment = new SETUPGROUPMETHODTREATMENT() { GroupMethodID = groupMethodID , TreatmentCode = itemCode };
            SETUPGROUPMETHODTREATMENT setupGroupMethodTreatment = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByKey(_setupGroupMethodTreatment);

            VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
            _VT_VNTREAT.VISITDATE = visitDate;
            _VT_VNTREAT.VN = VN;
            _VT_VNTREAT.SUFFIX = suffix;
            _VT_VNTREAT.TREATMENTCODE = itemCode;
            _VT_VNTREAT.TREATMENTNAME = treatment.Name;
            //_VT_VNTREAT.CHARGEAMT = setupGroupMethodTreatment.AMT;
            _VT_VNTREAT.CHARGECODE = setupGroupMethodTreatment.CHARGECODE;
            //_VT_VNTREAT.AMT = setupGroupMethodTreatment.AMT;
            //_VT_VNTREAT.QTY = setupGroupMethodTreatment.QTY;

            _VT_VNTREAT.CHARGECODE = setupGroupMethodTreatment.CHARGECODE;
            if (hdTreatmentPriceType.Value == "3")
            {
                _VT_VNTREAT.AMT = treatment.StdPrice2;
                _VT_VNTREAT.QTY = setupGroupMethodTreatment.QTY;
                _VT_VNTREAT.CHARGEAMT = treatment.StdPrice2 * setupGroupMethodTreatment.QTY;
            }
            else
            {
                _VT_VNTREAT.AMT = treatment.StdPrice1;
                _VT_VNTREAT.QTY = setupGroupMethodTreatment.QTY;
                _VT_VNTREAT.CHARGEAMT = treatment.StdPrice1 * setupGroupMethodTreatment.QTY;
            }

            _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value;
            _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
            _VT_VNTREAT.TREATMENTENTRYSTYLE = setupGroupMethodTreatment.TREATMENTENTRYSTYLE;
            _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
            _VT_VNTREAT.TYPEOFCHARGE = 0;
            _VT_VNTREAT.REVERSE = 0;
            _VT_VNTREAT.PAIDFLAG = 0;
            _VT_VNTREAT.GROUPREQUESTCODE = !string.IsNullOrEmpty(groupMethodCode) ? groupMethodCode : string.Empty;

            if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT) == false)
            {

                if ((_VT_VNTREAT.AMT == 0 && _VT_VNTREAT.ZeroPrice != 1) || _VT_VNTREAT.TREATMENTENTRYSTYLE == 5)
                {
                    _VT_VNTREAT.TreatmentCodeInfo = treatment;
                    EditTreatment(_VT_VNTREAT);
                }
                else
                {

                    ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).Insert(_VT_VNTREAT);
                    if (rv.Value == true)
                    {

                        TREATMENT TREATMENT = new TREATMENT();
                        TREATMENT.VN = VN;
                        TREATMENT.VISITDATE = visitDate;
                        TREATMENT.SUFFIX = suffix;
                        LoadOPDTreatmentList(TREATMENT);

                        if (showNotify)
                        {
                            string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
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
                                            $.notify('[{0}] : {1} This treatment item was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", setupGroupMethodTreatment.TreatmentCode, setupGroupMethodTreatment.TreatmentName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
            }
        }



        private void setDefaultControlEdit()
        {

            ddlOrderType.SelectedIndex = 0;
            txtAmt.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtChargeAMT.Text = string.Empty;
        }

        private void EditTreatment(VT_VNTREAT _VT_VNTREAT)
        {
            setDefaultControlEdit();
            hdTreatmentCode.Value = _VT_VNTREAT.TREATMENTCODE;
            hdTreatmentStyle.Value = _VT_VNTREAT.TREATMENTENTRYSTYLE.ToString();
            hdSubSuffix.Value = _VT_VNTREAT.SUBSUFFIX.ToString();
            hdGroupRequestCode.Value = _VT_VNTREAT.GROUPREQUESTCODE;
            ddlOrderType.ClearSelection();
            ddlOrderType.Items.FindByValue(_VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
            lblTreatmentName.Text = string.Format("[{0}] {1}", _VT_VNTREAT.TREATMENTCODE, _VT_VNTREAT.TREATMENTNAME);
            txtAmt.Text = string.Format("{0:0.##}", _VT_VNTREAT.AMT);
            txtQty.Text = string.Format("{0:0.##}", _VT_VNTREAT.QTY);
            hdChargeCode.Value = string.Format("{0:0.##}", _VT_VNTREAT.CHARGECODE);
            txtChargeAMT.Text = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT);
            hdChargeAMT.Value = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT);
            txtRemark.Text = _VT_VNTREAT.REMARKS;
            ddlOrderType.ClearSelection();
            ddlOrderType.Items.FindByValue(_VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
            VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(hdTreatmentCode.Value);
            //hdZeroPrice.Value = _VT_VNTREAT.ZeroPrice.ToString();
            hdZeroPrice.Value = treatment.ZeroPrice.ToString();
            ddlEditDoctorList.ClearSelection();
            ddlEditDoctorList.Items.FindByValue(_VT_VNTREAT.DOCTOR.ToString()).Selected = true;
            divOrderType.Style["display"] = "none";
            divChargeAMT.Style["display"] = "none";
            divTimeBetween.Style["display"] = "none";
            txtStartDateTime.Enabled = false;
            txtEndDateTime.Enabled = false;
            txtStartTime.Enabled = false;
            txtEndTime.Enabled = false;
            txtStartDateTime.Text = string.Empty;
            txtEndDateTime.Text = string.Empty;
            txtStartTime.Text = string.Empty;
            txtEndTime.Text = string.Empty;
            hfTimeType.Value = string.Empty;
            hdTime01.Value = string.Empty;
            hdTime02.Value = string.Empty;
            hdTime03.Value = string.Empty;
            hdTime04.Value = string.Empty;
            hdTime05.Value = string.Empty;
            hdTime06.Value = string.Empty;
            switch (_VT_VNTREAT.TREATMENTENTRYSTYLE)
            {
                case 1:
                    txtAmt.Enabled = false;
                    divAMT.Style.Remove("display");
                    txtQty.Enabled = false;
                    divQTY.Style.Remove("display");
                    break;
                case 2: //Std.Adj Amt. type
                    txtAmt.Enabled = true;
                    divAMT.Style.Remove("display");
                    txtQty.Enabled = false;
                    divQTY.Style["display"] = "none";
                    //ScriptManager.GetCurrent(this.Page).SetFocus(this.txtAmt);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtAmt.ClientID + "').focus();}, 500);", true);
                    break;
                case 3:  //Qty type
                    txtAmt.Enabled = false;
                    divAMT.Style.Remove("display");
                    txtQty.Enabled = true;
                    divQTY.Style.Remove("display");
                    //ScriptManager.GetCurrent(this.Page).SetFocus(this.txtQty);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtQty.ClientID + "').focus();}, 500);", true);
                    break;
                case 5: //Time between 
                    txtQty.Enabled = false;
                    divQTY.Style["display"] = "none";
                    divTimeBetween.Style.Remove("display");
                    txtStartDateTime.Enabled = true;
                    txtEndDateTime.Enabled = true;
                    txtStartTime.Enabled = true;
                    txtEndTime.Enabled = true;
                    txtStartDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    txtEndDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy", new System.Globalization.CultureInfo("en-US"));
                    txtStartTime.Text = DateTime.Now.ToString("00:00", new System.Globalization.CultureInfo("en-US"));
                    txtEndTime.Text = DateTime.Now.ToString("00:00", new System.Globalization.CultureInfo("en-US"));
                    hfTimeType.Value = _VT_VNTREAT.TreatmentCodeInfo.TimeType.ToString();
                    txtAmt.Text = string.Format("{0:0.##}", 0);
                    hdTime01.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time01);
                    hdTime02.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time02);
                    hdTime03.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time03);
                    hdTime04.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time04);
                    hdTime05.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time05);
                    hdTime06.Value = string.Format("{0:0.##}", _VT_VNTREAT.TreatmentCodeInfo.Time06);
                    break;
                default:
                    txtAmt.Enabled = true;
                    divAMT.Style.Remove("display");
                    txtQty.Enabled = true;
                    divQTY.Style.Remove("display");
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "setTimeout", "setTimeout(function() {$('#" + this.txtQty.ClientID + "').focus();}, 500);", true);
                    //ScriptManager.GetCurrent(this.Page).SetFocus(this.txtQty);
                    break;
            }
            htmlTitleUpdateTreatment.InnerHtml = "Add Treatment";
            btnUpdateTreatment.Text = "Add";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupModal", "showModalTreatment();", true);


        }



            //protected void gvTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
            //{

            //}



            protected void gvMedicineList_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Add")
            {
                if (CheckMedicineValidation())
                {
                    int index = Convert.ToInt32(e.CommandArgument);
                    GridViewRow row = gvMedicineList.Rows[index];
                    
                    int groupMethodID = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                    string itemCode = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineCode"].ToString();
                    string store = ddlStore.SelectedItem.Value;

                    SETUPGROUPMETHODMEDICINE obj = new SETUPGROUPMETHODMEDICINE();
                    obj.GroupMethodID = groupMethodID;
                    obj.MedicineCode = itemCode;

                    SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(obj);
                    VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(itemCode,store, hdMedicinePriceType.Value);

                    VT_VNMEDICINE VT_VNMEDICINE = new VT_VNMEDICINE();
                    VT_VNMEDICINE.VISITDATE = visitDate;
                    VT_VNMEDICINE.VN = VN;
                    VT_VNMEDICINE.SUFFIX = suffix;
                    VT_VNMEDICINE.MEDICINECODE = itemCode;
                    VT_VNMEDICINE.MEDICINE_THAINAME = SETUPGROUPMETHODMEDICINE.MedicineName_TH;
                    //VT_VNMEDICINE.AMT = SETUPGROUPMETHODMEDICINE.AMT;
                    VT_VNMEDICINE.AMT = Convert.ToDouble(VT_STOCK_MASTER.Price)*(SETUPGROUPMETHODMEDICINE.QTY);
                    VT_VNMEDICINE.QTY = SETUPGROUPMETHODMEDICINE.QTY;
                    VT_VNMEDICINE.UNITPRICE = Convert.ToDouble(VT_STOCK_MASTER.Price);
                    VT_VNMEDICINE.UNITCODE = SETUPGROUPMETHODMEDICINE.UnitCode;
                    VT_VNMEDICINE.DOSETYPE = SETUPGROUPMETHODMEDICINE.DoseTypeCode;
                    VT_VNMEDICINE.DOSEQTYCODE = SETUPGROUPMETHODMEDICINE.DoseQTY;
                    VT_VNMEDICINE.DOSEUNITCODE = SETUPGROUPMETHODMEDICINE.DoseUnitCode;
                    VT_VNMEDICINE.DOSECODE = SETUPGROUPMETHODMEDICINE.DoseCode;
                    VT_VNMEDICINE.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                    VT_VNMEDICINE.REVERSE = 0;
                    VT_VNMEDICINE.STORE = store;
                    VT_VNMEDICINE.TYPEOFCHARGE = 0;
                    VT_VNMEDICINE.CHARGECODE = VT_STOCK_MASTER.ChargeCode;
                    VT_VNMEDICINE.PAIDFLAG = 0;
                   
                    string alertScript = string.Empty;
                    if (new BLVT_VNMEDICINE(extConnDBInfo).CheckDup(VT_VNMEDICINE) == false)
                    {
                        ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).Insert(VT_VNMEDICINE);
                        if (rv.Value == true)
                        {
                            TREATMENT treatment = new TREATMENT();
                            treatment.VN = VN;
                            treatment.VISITDATE = visitDate;
                            treatment.SUFFIX = suffix;
                            LoadOPDTreatmentList(treatment);


                            alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                   "     $(\"[id *= 'ddlMedicine'],[id *= 'ddlUnit'],[id *= 'ddlDoseType'],[id *= 'ddlDoseQTY'],[id *= 'ddlDoseUnit'],[id *= 'ddlDoseCode']\").val(null).trigger('change');" +
                                                   "     $(\"[id *= 'txtMedQTY'] , [id *= 'txtMedAMT']\").val('');" +
                                                   "     $(\"[id *= 'ddlMedicine']\").prop('disabled', false);" +
                                                   " $.notify('Add {0} : {1} completed.', " +
                                                   "      {{ " +
                                                   "          className: 'success', " +
                                                   "          position: 'bottom right', " +
                                                   "          clickToHide: true " +
                                                   "      }} " +
                                                   "  ); " +
                                                   " }}); ", VT_VNMEDICINE.MEDICINECODE, VT_VNMEDICINE.MEDICINE_THAINAME);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                      alertScript, true);

                        }
                        else
                        {
                            alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify({0},
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
                    else
                    {
                        alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('[{0}] : {1} This medicine item was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", SETUPGROUPMETHODMEDICINE.MedicineCode, SETUPGROUPMETHODMEDICINE.MedicineName);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                            alertScript, true);
                    }
                }
            }

        }

        protected void gvMedicineList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    CheckBox chkMedSelect = (CheckBox)e.Row.FindControl("chkMedSelect");


                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "AutoTick")) == true)
                    {
                        chkMedSelect.Checked = true;
                        e.Row.Attributes["class"] = "highlightRow";
                    }
                    else
                    {
                        chkMedSelect.Checked = false;
                    }
                }
            }
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

        private void InitSetupGroupMethod()
        {
           

            gvTreatmentList.DataSource = new List<SETUPGROUPMETHODTREATMENT>();
            gvTreatmentList.DataBind();

            gvMedicineList.DataSource = new List<SETUPGROUPMETHODTREATMENT>();
            gvMedicineList.DataBind();
            
        }
        private bool CheckUpdatedMedicineValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string medicineCode = hdMedicineCode.Value;
            string store = ddlEditStore.SelectedItem.Value;
            string orderType = ddlEditOrderType.SelectedItem.Value;
            string unit = ddlEditUnit.SelectedItem.Value;
            double AMT;
            bool isAMT = Double.TryParse(txtEditMedAMT.Text.Trim(), out AMT);
            double QTY;
            bool isQTY = Double.TryParse(txtEditMedQTY.Text.Trim(), out QTY);
            if (medicineCode == string.Empty || unit == string.Empty || store == string.Empty || orderType == string.Empty ||
                isAMT == false || AMT <= 0 ||
                isQTY == false || QTY <= 0)
            {
                message = string.IsNullOrEmpty(medicineCode) ? "   - Medicine Name " : "";
                message += string.IsNullOrEmpty(store) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Store " : "";
                message += string.IsNullOrEmpty(orderType) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Order Type " : "";
                message += string.IsNullOrEmpty(unit) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Unit " : "";
                message += AMT <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - AMT " : "";
                message += QTY <= 0 ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - QTY " : "";
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
        protected void btnUpdateMedicine_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckUpdatedMedicineValidation())
                {
                    VT_VNMEDICINE _VT_VNMEDICINE = new VT_VNMEDICINE();
                    _VT_VNMEDICINE.VISITDATE = visitDate;
                    _VT_VNMEDICINE.VN = VN;
                    _VT_VNMEDICINE.SUFFIX = suffix;
                    _VT_VNMEDICINE.SUBSUFFIX = Convert.ToInt32(hdSubSuffix.Value);
                    _VT_VNMEDICINE.MEDICINECODE = hdMedicineCode.Value;
                    _VT_VNMEDICINE.AMT = Convert.ToDouble(txtEditMedAMT.Text);
                    _VT_VNMEDICINE.QTY = Convert.ToDouble(txtEditMedQTY.Text);
                    _VT_VNMEDICINE.UNITPRICE = Convert.ToDouble(txtEditUnitPrice.Text);
                    _VT_VNMEDICINE.UNITCODE = ddlEditUnit.SelectedItem.Value;
                    _VT_VNMEDICINE.DOSETYPE = ddlEditDoseType.SelectedItem.Value;
                    _VT_VNMEDICINE.DOSEQTYCODE = ddlEditDoseQTY.SelectedItem.Value;
                    _VT_VNMEDICINE.DOSEUNITCODE = ddlEditDoseUnit.SelectedItem.Value;
                    _VT_VNMEDICINE.DOSECODE = ddlEditDoseCode.SelectedItem.Value;
                    _VT_VNMEDICINE.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                    _VT_VNMEDICINE.REVERSE = string.IsNullOrEmpty(ddlEditOrderType.SelectedItem.Value) ? 0 : (Convert.ToInt32(ddlEditOrderType.SelectedItem.Value) == 2 ? 1 : 0);
                    _VT_VNMEDICINE.STORE = ddlEditStore.SelectedItem.Value;
                    _VT_VNMEDICINE.TYPEOFCHARGE = string.IsNullOrEmpty(ddlEditOrderType.SelectedItem.Value) ? 0 : Convert.ToInt32(ddlEditOrderType.SelectedItem.Value);
                    _VT_VNMEDICINE.AUXLABEL1 = ddlEditAuxLabel1.SelectedItem.Value;
                    _VT_VNMEDICINE.AUXLABEL2 = ddlEditAuxLabel2.SelectedItem.Value;
                    _VT_VNMEDICINE.AUXLABEL3 = ddlEditAuxLabel3.SelectedItem.Value;
                    _VT_VNMEDICINE.DOSEMEMO = txtEditRemark.Text.Trim();
                    ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).Update(_VT_VNMEDICINE);
                    if (rv.Value == true)
                    {

                        TREATMENT treatment = new TREATMENT();
                        treatment.VN = VN;
                        treatment.VISITDATE = visitDate;
                        treatment.SUFFIX = suffix;
                        LoadOPDTreatmentList(treatment);

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


        protected void gvTreatmentList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < gvTreatmentList.Columns.Count - 1; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = gvTreatmentList.Columns.Count;
            }
        }


        protected void gvMedicineList_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < gvTreatmentList.Columns.Count - 1; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = gvTreatmentList.Columns.Count;
            }
        }
        protected void gvTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");


                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "AutoTick")) == true)
                    {
                        chkSelect.Checked = true;
                        e.Row.Attributes["class"] = "highlightRow";
                    }
                    else
                    {
                        chkSelect.Checked = false;
                    }
                }
            }



        }






        protected void btnSubmitTreatmentSelect_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTreatmentList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("chkSelect");
                    if (c.Checked)
                    {
                        int groupMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                        string groupMethodCode = gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodCode"].ToString();
                        string itemCode = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();
                        string store = ddlStore.SelectedItem.Value;
                        SETUPGROUPMETHODTREATMENT _setupGroupMethodTreatment = new SETUPGROUPMETHODTREATMENT() { GroupMethodID = groupMethodID, TreatmentCode = itemCode };
                        SETUPGROUPMETHODTREATMENT setupGroupMethodTreatment = new BLSETUPGROUPMETHODTREATMENT(appConnDBInfo).GetSETUPGROUPMETHODTREATMENTByKey(_setupGroupMethodTreatment);
                        


                        VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(itemCode);
                        
                        VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                        _VT_VNTREAT.VISITDATE = visitDate;
                        _VT_VNTREAT.VN = VN;
                        _VT_VNTREAT.SUFFIX = suffix;
                        _VT_VNTREAT.TREATMENTCODE = itemCode;
                        _VT_VNTREAT.CHARGECODE = setupGroupMethodTreatment.CHARGECODE;
                        _VT_VNTREAT.AMT = setupGroupMethodTreatment.AMT;
                        _VT_VNTREAT.QTY = setupGroupMethodTreatment.QTY;
                        _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value;
                        _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
                        _VT_VNTREAT.TREATMENTENTRYSTYLE = setupGroupMethodTreatment.TREATMENTENTRYSTYLE;
                        _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                        _VT_VNTREAT.TYPEOFCHARGE = 0;
                        _VT_VNTREAT.REVERSE = 0;
                        _VT_VNTREAT.PAIDFLAG = 0;
                        _VT_VNTREAT.GROUPREQUESTCODE = !string.IsNullOrEmpty(groupMethodCode) ? groupMethodCode : string.Empty;

                        if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT))
                        {
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('[{0}] : {1} This treatment item was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", setupGroupMethodTreatment.TreatmentCode, setupGroupMethodTreatment.TreatmentName);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }

                        if (setupGroupMethodTreatment.AMT <= 0)
                        {
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", string.Format("Item [{0}] : {1} cannot insert. Amount: Must be value greater than 0 ", setupGroupMethodTreatment.TreatmentCode, setupGroupMethodTreatment.TreatmentName));
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }



                    }
                }
            }

            int i = 0;
            foreach (GridViewRow row in gvTreatmentList.Rows)
            {
                
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("chkSelect");
                    if (c.Checked)
                    {

                        int groupMethodID = Convert.ToInt32(gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                        string groupMethodCode = gvTreatmentList.DataKeys[row.RowIndex].Values["GroupMethodCode"].ToString();
                        string itemCode = gvTreatmentList.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();
                        string store = ddlStore.SelectedItem.Value;
                        InsertTreatment(groupMethodID, groupMethodCode, itemCode, false);
                        i++;

                    }
                }
              
            }
            if (i > 0)
            {
                string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                          " $.notify('Add {0} item(s) completed.', " +
                                          "      {{ " +
                                          "          className: 'success', " +
                                          "          position: 'bottom right', " +
                                          "          clickToHide: true " +
                                          "      }} " +
                                          "  ); " +
                                          " }}); ",i);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                          alertScript, true);
            }
        }

        

        protected void btnSubmitMedSelect_Click(object sender, EventArgs e) {

            foreach (GridViewRow row in gvMedicineList.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("chkMedSelect");
                    if (c.Checked)
                    {

                        if (CheckMedicineValidation() == false)
                            return;

                        int groupMethodID = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                        string itemCode = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineCode"].ToString();
                        string store = ddlStore.SelectedItem.Value;

                        SETUPGROUPMETHODMEDICINE obj = new SETUPGROUPMETHODMEDICINE();
                        obj.GroupMethodID = groupMethodID;
                        obj.MedicineCode = itemCode;

                        SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(obj);
                        VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(itemCode,store,hdMedicinePriceType.Value);

                        VT_VNMEDICINE VT_VNMEDICINE = new VT_VNMEDICINE();
                        VT_VNMEDICINE.VISITDATE = visitDate;
                        VT_VNMEDICINE.VN = VN;
                        VT_VNMEDICINE.SUFFIX = suffix;
                        VT_VNMEDICINE.MEDICINECODE = itemCode;
                        VT_VNMEDICINE.MEDICINE_THAINAME = SETUPGROUPMETHODMEDICINE.MedicineName_TH;
                        VT_VNMEDICINE.AMT = Convert.ToDouble(VT_STOCK_MASTER.Price)* Convert.ToDouble(SETUPGROUPMETHODMEDICINE.QTY);
                        VT_VNMEDICINE.QTY = SETUPGROUPMETHODMEDICINE.QTY;
                        VT_VNMEDICINE.UNITPRICE = Convert.ToDouble(VT_STOCK_MASTER.Price);
                        VT_VNMEDICINE.UNITCODE = SETUPGROUPMETHODMEDICINE.UnitCode;
                        VT_VNMEDICINE.DOSETYPE = SETUPGROUPMETHODMEDICINE.DoseTypeCode;
                        VT_VNMEDICINE.DOSEQTYCODE = SETUPGROUPMETHODMEDICINE.DoseQTY;
                        VT_VNMEDICINE.DOSEUNITCODE = SETUPGROUPMETHODMEDICINE.DoseUnitCode;
                        VT_VNMEDICINE.DOSECODE = SETUPGROUPMETHODMEDICINE.DoseCode;
                        VT_VNMEDICINE.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                        VT_VNMEDICINE.REVERSE = 0;
                        VT_VNMEDICINE.STORE = store;
                        VT_VNMEDICINE.TYPEOFCHARGE = 0;
                        VT_VNMEDICINE.CHARGECODE = VT_STOCK_MASTER.ChargeCode;
                        VT_VNMEDICINE.PAIDFLAG = 0;

                        string alertScript = string.Empty;
                        if (new BLVT_VNMEDICINE(extConnDBInfo).CheckDup(VT_VNMEDICINE))
                        {
                            alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('[{0}] : {1} This medicine item was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", SETUPGROUPMETHODMEDICINE.MedicineCode, SETUPGROUPMETHODMEDICINE.MedicineName);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }
                        if (VT_VNMEDICINE.AMT <= 0) {
                            alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                        $.notify('{0}',
                                            {{
                                                className: 'error',
                                                position: 'bottom right',
                                                clickToHide: true
                                            }}
                                        );
                                        }});   ", string.Format("Item [{0}] : {1} cannot insert. Amount: Must be value greater than 0 ", VT_VNMEDICINE.MEDICINECODE, VT_VNMEDICINE.MEDICINE_THAINAME));
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }

           
                    }
                }
            }

            int i = 0;
            foreach (GridViewRow row in gvMedicineList.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("chkMedSelect");
                    if (c.Checked)
                    {
                        int groupMethodID = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["GroupMethodID"]);
                        string itemCode = gvMedicineList.DataKeys[row.RowIndex].Values["MedicineCode"].ToString();
                        string store = ddlStore.SelectedItem.Value;

                        InsertMedicine(groupMethodID, store, itemCode, false);
                        i++;

                    }
                }

            }
            if (i > 0)
            {
                string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                          " $.notify('Add {0} item(s) completed.', " +
                                          "      {{ " +
                                          "          className: 'success', " +
                                          "          position: 'bottom right', " +
                                          "          clickToHide: true " +
                                          "      }} " +
                                          "  ); " +
                                          " }}); ", i);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                          alertScript, true);
            }

        }

        private void InsertMedicine(int _groupMethodID,string _store, string _itemCode, bool showNotify = true)
        {
            int groupMethodID = _groupMethodID;
            string itemCode = _itemCode;
            string store = _store;

            SETUPGROUPMETHODMEDICINE obj = new SETUPGROUPMETHODMEDICINE();
            obj.GroupMethodID = groupMethodID;
            obj.MedicineCode = itemCode;

            SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).GetSETUPGROUPMETHODMEDICINEByKey(obj);
            VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(itemCode);

            VT_VNMEDICINE VT_VNMEDICINE = new VT_VNMEDICINE();
            VT_VNMEDICINE.VISITDATE = visitDate;
            VT_VNMEDICINE.VN = VN;
            VT_VNMEDICINE.SUFFIX = suffix;
            VT_VNMEDICINE.MEDICINECODE = itemCode;
            VT_VNMEDICINE.MEDICINE_THAINAME = SETUPGROUPMETHODMEDICINE.MedicineName_TH;
            VT_VNMEDICINE.AMT = SETUPGROUPMETHODMEDICINE.AMT;
            VT_VNMEDICINE.QTY = SETUPGROUPMETHODMEDICINE.QTY;
            VT_VNMEDICINE.UNITPRICE = Convert.ToDouble(VT_STOCK_MASTER.Price);
            VT_VNMEDICINE.UNITCODE = SETUPGROUPMETHODMEDICINE.UnitCode;
            VT_VNMEDICINE.DOSETYPE = SETUPGROUPMETHODMEDICINE.DoseTypeCode;
            VT_VNMEDICINE.DOSEQTYCODE = SETUPGROUPMETHODMEDICINE.DoseQTY;
            VT_VNMEDICINE.DOSEUNITCODE = SETUPGROUPMETHODMEDICINE.DoseUnitCode;
            VT_VNMEDICINE.DOSECODE = SETUPGROUPMETHODMEDICINE.DoseCode;
            VT_VNMEDICINE.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
            VT_VNMEDICINE.REVERSE = 0;
            VT_VNMEDICINE.STORE = store;
            VT_VNMEDICINE.TYPEOFCHARGE = 0;
            VT_VNMEDICINE.CHARGECODE = VT_STOCK_MASTER.ChargeCode;
            VT_VNMEDICINE.PAIDFLAG = 0;

            string alertScript = string.Empty;
            if (new BLVT_VNMEDICINE(extConnDBInfo).CheckDup(VT_VNMEDICINE) == false)
            {
                ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).Insert(VT_VNMEDICINE);
                if (rv.Value == true)
                {
                    TREATMENT treatment = new TREATMENT();
                    treatment.VN = VN;
                    treatment.VISITDATE = visitDate;
                    treatment.SUFFIX = suffix;
                    LoadOPDTreatmentList(treatment);

                    if (showNotify == true)
                    {
                        alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                               "     $(\"[id *= 'ddlMedicine'],[id *= 'ddlUnit'],[id *= 'ddlDoseType'],[id *= 'ddlDoseQTY'],[id *= 'ddlDoseUnit'],[id *= 'ddlDoseCode']\").val(null).trigger('change');" +
                                               "     $(\"[id *= 'txtMedQTY'] , [id *= 'txtMedAMT']\").val('');" +
                                               "     $(\"[id *= 'ddlMedicine']\").prop('disabled', false);" +
                                               " $.notify('Add {0} : {1} completed.', " +
                                               "      {{ " +
                                               "          className: 'success', " +
                                               "          position: 'bottom right', " +
                                               "          clickToHide: true " +
                                               "      }} " +
                                               "  ); " +
                                               " }}); ", VT_VNMEDICINE.MEDICINECODE, VT_VNMEDICINE.MEDICINE_THAINAME);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                  alertScript, true);
                    }

                }
                else
                {
                    alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                $.notify({0},
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
            else
            {
                alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                $.notify('[{0}] : {1} This medicine item was already entered. Please try another item.',
                                    {{
                                        className: 'error',
                                        position: 'bottom right',
                                        clickToHide: true
                                    }}
                                );
                              }});   ", VT_VNMEDICINE.MEDICINECODE, SETUPGROUPMETHODMEDICINE.MedicineName);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
            }
        }


    }
}