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
using Newtonsoft.Json;

namespace solution.PostTreatment
{
    public partial class PostMedicine : System.Web.UI.Page
    {

        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected static DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;

        public string HN { get; set; }
        public string VN { get; set; }
        public int suffix { get; set; }
        public DateTime visitDate { get; set; }
        public string CloseVN { get; set; }
        public string RIGHTCODE { get; set; }
        public int FixRate { get; set; }
        public string MedicinePriceType { get; set; }
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
                LoadSetupGroupMethod();
                LoadStore();
                //LoadMedicineItem();
                LoadOrderType();
                LoadDoseUnit();
                LoadDoseQTY();
                LoadDoseType();
                LoadDoseCode();
                LoadAuxLabel();
                LoadUnit();
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

                    VT_VNMEDICINE _VT_VNMEDICINE = new VT_VNMEDICINE();
                    _VT_VNMEDICINE.VN = VN;
                    _VT_VNMEDICINE.VISITDATE = visitDate;
                    _VT_VNMEDICINE.SUFFIX = suffix;
                    LoadVNMedicineList(_VT_VNMEDICINE);



                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }






                //LoadTreatmentCode();


            }
            else
            {
                RIGHTCODE = hdRightCode.Value;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ? new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1;
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

        private void setDefaultControl()
        {
            ddlStore.SelectedIndex = 0;
            //ddlMedicine.SelectedIndex = 0;
            ddlOrderType.SelectedIndex = 0;
            txtMedAMT.Text = string.Empty;
            txtMedQTY.Text = string.Empty;
            txtMedUnitPrice.Text = string.Empty;
            ddlUnit.SelectedIndex = 0;
            ddlDoseType.SelectedIndex = 0;
            ddlDoseQTY.SelectedIndex = 0;
            ddlDoseUnit.SelectedIndex = 0;
            ddlDoseCode.SelectedIndex = 0;
            hdChargeCode.Value = string.Empty;
            ddlAuxLabel1.SelectedIndex = 0;
            ddlAuxLabel2.SelectedIndex = 0;
            ddlAuxLabel3.SelectedIndex = 0;
            txtRemark.Text = string.Empty;
        }
        private void setDefaultControlEdit()
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
            ddlEditAuxLabel1.SelectedIndex = 0;
            ddlEditAuxLabel2.SelectedIndex = 0;
            ddlEditAuxLabel3.SelectedIndex = 0;
            txtEditRemark.Text = string.Empty;

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


        public void LoadSetupGroupMethod()
        {
           
            try
            {


                ListItem litGroupMethod = new ListItem();
                litGroupMethod.Text = "-Setup Group Method-";
                litGroupMethod.Value = "";


                SETUPGROUPMETHOD SETUPGROUPMETHOD = new SETUPGROUPMETHOD();
                List<SETUPGROUPMETHOD> lstSetupGroupMethod = new BLSETUPGROUPMETHOD(appConnDBInfo).SearchMedicineOnly();
                ddlSetupGroupMethod.DataSource = lstSetupGroupMethod;
                ddlSetupGroupMethod.DataValueField = "GroupMethodID";
                ddlSetupGroupMethod.DataTextField = "NameInfo";
                ddlSetupGroupMethod.DataBind();
                ddlSetupGroupMethod.Items.Insert(0, litGroupMethod);

                ddlEditSetupGroupMethod.DataSource = lstSetupGroupMethod;
                ddlEditSetupGroupMethod.DataValueField = "GroupMethodID";
                ddlEditSetupGroupMethod.DataTextField = "NameInfo";
                ddlEditSetupGroupMethod.DataBind();
                ddlEditSetupGroupMethod.Items.Insert(0, litGroupMethod);

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

                string HostName = Dns.GetHostName();
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

                //ListItem litOrderType = new ListItem();
                //litOrderType.Text = "-Please Select-";
                //litOrderType.Value = "";

                ddlOrderType.DataSource = typeOfCharges;
                ddlOrderType.DataValueField = "Code";
                ddlOrderType.DataTextField = "Name";
                ddlOrderType.DataBind();
                //ddlOrderType.Items.Insert(0, litOrderType);

                ddlEditOrderType.DataSource = typeOfCharges;
                ddlEditOrderType.DataValueField = "Code";
                ddlEditOrderType.DataTextField = "Name";
                ddlEditOrderType.DataBind();
                //ddlEditOrderType.Items.Insert(0, litOrderType);

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

        public void LoadOPDVisitInfo(VT_VNMASTER _VT_VNMASTER)
        {
            try
            {
                VT_VNMASTER VT_VNMASTER = new BLVT_VNMASTER(extConnDBInfo).GetVNDetailByKey(_VT_VNMASTER);
                lblVN.Text = string.Format("{0}/{1}", VT_VNMASTER.VN, VT_VNMASTER.SUFFIX);
                lblVisitDate.Text = CultureInfo.GetDatetime(VT_VNMASTER.VISITINDATETIME.Value, YearType.Thai).ToString("dd MMM yyyy");
                lblClinic.Text = VT_VNMASTER.CLINICNAME;
                lblDoctor.Text = VT_VNMASTER.DoctorName;
                lblRight.Text = VT_VNMASTER.RIGHTNAME;
                hdDoctorCode.Value = VT_VNMASTER.DOCTOR;
                RIGHTCODE = VT_VNMASTER.RIGHTCODE;
                hdRightCode.Value = VT_VNMASTER.RIGHTCODE;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ? new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1;
                hdFixRate.Value = Convert.ToString(FixRate);
                hdMedicinePriceType.Value = VT_VNMASTER.MedicinePriceType;

                if (VT_VNMASTER.Close == "Y" || VT_VNMASTER.HoldBill == true)
                {
                    
                    CloseVN = "Y";
                    btnAddMedicine.Enabled = false;

                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $('[id*=ddlMedicine]').attr('disabled', 'disabled');
                                          }});   ");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                        alertScript, true);
                }


            }
            catch { }

        }

        public void LoadVNMedicineList(VT_VNMEDICINE _VT_VNMEDICINE)
        {
            try
            {
                List<VT_VNMEDICINE> lstVNMEDICINE = new BLVT_VNMEDICINE(extConnDBInfo).SearchByKey(_VT_VNMEDICINE);
                gvMedicineList.DataSource = lstVNMEDICINE;
                gvMedicineList.DataBind();

            }
            catch { }

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
                    PictureFileName = ORPATIENTVO.PictureFileName;
                    lblPatientType.Text = ORPATIENTVO.PatientType;
                }
            }
            catch { }
        }

        protected void gvMedicineList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteMedicineItem")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvMedicineList.Rows[index];
                VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                VT_VNMEDICINE _VT_VNMEDINE = new VT_VNMEDICINE();
                _VT_VNMEDINE.VN = gvMedicineList.DataKeys[row.RowIndex].Values["VN"].ToString();
                _VT_VNMEDINE.VISITDATE = Convert.ToDateTime(gvMedicineList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                _VT_VNMEDINE.SUFFIX = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                _VT_VNMEDINE.SUBSUFFIX = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                _VT_VNMEDINE.CXLBYUSERCODE = Session["USERNANME"].ToString();

                ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).CXLVNMEDICINE(_VT_VNMEDINE);
                if (rv.Value)
                {
                    LoadVNMedicineList(_VT_VNMEDINE);
                    string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('Delete completed.',
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
            else if (e.CommandName == "EditMedicineItem")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvMedicineList.Rows[index];
                VT_VNMEDICINE _VT_VNMEDINE = new VT_VNMEDICINE();
                _VT_VNMEDINE.VN = gvMedicineList.DataKeys[row.RowIndex].Values["VN"].ToString();
                _VT_VNMEDINE.VISITDATE = Convert.ToDateTime(gvMedicineList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                _VT_VNMEDINE.SUFFIX = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                _VT_VNMEDINE.SUBSUFFIX = Convert.ToInt32(gvMedicineList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);

                setDefaultControlEdit();

                VT_VNMEDICINE VT_VNMEDICINE = new BLVT_VNMEDICINE(extConnDBInfo).GetVT_VNMEDICINEByKey(_VT_VNMEDINE);
                hdMedicineCode.Value = VT_VNMEDICINE.MEDICINECODE;
                hdSubSuffix.Value = VT_VNMEDICINE.SUBSUFFIX.ToString();
                ddlEditSetupGroupMethod.ClearSelection();
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





        protected void gvMedicineList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    if (CloseVN == "Y")
                    {
                        Button btnDel = (Button)e.Row.FindControl("btnDeleteMedicine");
                        btnDel.Enabled = false;
                        Button btnEdit = (Button)e.Row.FindControl("btnEditMedicine");
                        btnEdit.Enabled = false;
                        //LinkButton btnDel = (LinkButton)e.Row.Cells[0].Controls[0];
                        //btnDel.Enabled = false; //
                        //LinkButton btnEdit = (LinkButton)e.Row.Cells[1].Controls[0];
                        //btnEdit.Enabled = false; //
                    }
                }
            }


        }

        private bool CheckMedicineValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string medicineCode = hfMedicine.Value;//ddlMedicine.SelectedItem.Value;
            string store = ddlStore.SelectedItem.Value;
            string orderType = ddlOrderType.SelectedItem.Value;
            string unit = ddlUnit.SelectedItem.Value;
            double AMT;
            bool isAMT = Double.TryParse(txtMedAMT.Text.Trim(), out AMT);
            double QTY;
            bool isQTY = Double.TryParse(txtMedQTY.Text.Trim(), out QTY);
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
                if (isQTY == false || QTY <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMedQTY);
                }
                else if (isAMT == false || AMT <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtMedAMT);
                }
                valid = false;
            }
            return valid;
        }

        protected void btnAddMedicine_Click(object sender, EventArgs e)
        {
            bool status = true;
            if (CheckMedicineValidation())
            {

                VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(hfMedicine.Value);//ddlMedicine.SelectedItem.Value);

                VT_VNMEDICINE VT_VNMEDICINE = new VT_VNMEDICINE();
                VT_VNMEDICINE.VISITDATE = visitDate;
                VT_VNMEDICINE.VN = VN;
                VT_VNMEDICINE.SUFFIX = suffix;
                VT_VNMEDICINE.MEDICINECODE = hfMedicine.Value;//ddlMedicine.SelectedItem.Value;
                VT_VNMEDICINE.MEDICINE_THAINAME = VT_STOCK_MASTER.MedicineName; //ddlMedicine.SelectedItem.Text;
                VT_VNMEDICINE.AMT = Convert.ToDouble(txtMedAMT.Text);
                VT_VNMEDICINE.QTY = Convert.ToDouble(txtMedQTY.Text);
                VT_VNMEDICINE.UNITPRICE = Convert.ToDouble(txtMedUnitPrice.Text);
                VT_VNMEDICINE.UNITCODE = ddlUnit.SelectedItem.Value;
                VT_VNMEDICINE.DOSETYPE = ddlDoseType.SelectedItem.Value;
                VT_VNMEDICINE.DOSEQTYCODE = ddlDoseQTY.SelectedItem.Value;
                VT_VNMEDICINE.DOSEUNITCODE = ddlDoseUnit.SelectedItem.Value;
                VT_VNMEDICINE.DOSECODE = ddlDoseCode.SelectedItem.Value;
                VT_VNMEDICINE.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                VT_VNMEDICINE.REVERSE = string.IsNullOrEmpty(ddlOrderType.SelectedItem.Value) ? 0 : (Convert.ToInt32(ddlOrderType.SelectedItem.Value) == 2 ? 1 : 0);
                VT_VNMEDICINE.STORE = ddlStore.SelectedItem.Value;
                VT_VNMEDICINE.TYPEOFCHARGE = string.IsNullOrEmpty(ddlOrderType.SelectedItem.Value) ? 0 : Convert.ToInt32(ddlOrderType.SelectedItem.Value);
                VT_VNMEDICINE.CHARGECODE = hdChargeCode.Value;
                VT_VNMEDICINE.PAIDFLAG = 0;
                VT_VNMEDICINE.AUXLABEL1 = ddlAuxLabel1.SelectedItem.Value;
                VT_VNMEDICINE.AUXLABEL2 = ddlAuxLabel2.SelectedItem.Value;
                VT_VNMEDICINE.AUXLABEL3 = ddlAuxLabel3.SelectedItem.Value;
                VT_VNMEDICINE.DOSEMEMO = txtRemark.Text.Trim();
                //AddMedicine(VT_VNMEDICINE);
                string alertScript = string.Empty;
                if (new BLVT_VNMEDICINE(extConnDBInfo).CheckDup(VT_VNMEDICINE) == false)
                {
                    if (!string.IsNullOrEmpty(ddlSetupGroupMethod.SelectedItem.Value))
                    {
                        //VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(VT_VNMEDICINE.MEDICINECODE);//ddlMedicine.SelectedItem.Value);
                        SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                        SETUPGROUPMETHODMEDICINE.MedicineCode = VT_VNMEDICINE.MEDICINECODE; 
                        SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(ddlSetupGroupMethod.SelectedItem.Value);
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
                        SETUPGROUPMETHODMEDICINE.Remark = txtRemark.Text.Trim();
                        if (new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).CheckDup(SETUPGROUPMETHODMEDICINE) == false)
                        {

                            ReturnValue rvSetup = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).Insert(SETUPGROUPMETHODMEDICINE);
                            if (rvSetup.Value == true)
                            {
                                alertScript += string.Format(" " +
                                               " $.notify('Add {0} to {1} completed.', " +
                                               "      {{ " +
                                               "          className: 'success', " +
                                               "          position: 'bottom right', " +
                                               "          clickToHide: true " +
                                               "      }} " +
                                               "  ); " +
                                               "  ",  SETUPGROUPMETHODMEDICINE.MedicineName, ddlSetupGroupMethod.SelectedItem.Text);

                            }
                            else
                            {
                                alertScript += string.Format(@"
                                                $.notify('{0}',
                                                    {{
                                                        className: 'error',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                                ", rvSetup.Exception);
                                status = false;

                            }
                        }
                        else {
                            alertScript += string.Format(@"
                                            $.notify('This medicine item was already entered in {0} Setup Group Method. Please try checked again',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                                      ", ddlSetupGroupMethod.SelectedItem.Text);
                            status = false;
                        }
                    }
                    if (status == true)
                    {
                        ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).Insert(VT_VNMEDICINE);
                        if (rv.Value == true)
                        {
                            //ddlMedicine.SelectedIndex = 0;
                            setDefaultControl();
                            LoadVNMedicineList(VT_VNMEDICINE);


                            alertScript += string.Format("  " +
                                                   "     $(\"[id *= 'ddlMedicine'],[id *= 'ddlUnit'],[id *= 'ddlDoseType'],[id *= 'ddlDoseQTY'],[id *= 'ddlDoseUnit'],[id *= 'ddlDoseCode'],[id *= 'ddlSetupGroupMethod']\").val(null).trigger('change');" +
                                                   "     $(\"[id *= 'txtMedQTY'] , [id *= 'txtMedAMT']\").val('');" +
                                                   "     $(\"[id *= 'ddlMedicine']\").prop('disabled', false);" +
                                                   " $.notify('Add {0} completed.', " +
                                                   "      {{ " +
                                                   "          className: 'success', " +
                                                   "          position: 'bottom right', " +
                                                   "          clickToHide: true " +
                                                   "      }} " +
                                                   "  ); " +
                                                   "  ",  VT_VNMEDICINE.MEDICINENAMEDETAIL);


                        }
                        else
                        {
                            alertScript += string.Format(@"
                                            $.notify({0},
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                                ", rv.Exception.ToString());

                        }
                    }
                }
                else
                {
                    alertScript += string.Format(@"
                                            $.notify('This medicine item was already entered. Please try another item.',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                                 ");

                }
                string msg = string.Format(@"javascript: $(document).ready(function(){{ {0} }});   ",alertScript);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                     msg, true);
            }
        }

        

        protected void btnUpdateMedicine_Click(object sender, EventArgs e)
        {
            bool status = true;
            string alertScript = string.Empty;
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


                    if (!string.IsNullOrEmpty(ddlEditSetupGroupMethod.SelectedItem.Value))
                    {
                        VT_STOCK_MASTER VT_STOCK_MASTER = new BLVT_STOCK_MASTER(extConnDBInfo).GetStockMasterByKey(_VT_VNMEDICINE.MEDICINECODE);//ddlMedicine.SelectedItem.Value);
                        SETUPGROUPMETHODMEDICINE SETUPGROUPMETHODMEDICINE = new SETUPGROUPMETHODMEDICINE();
                        SETUPGROUPMETHODMEDICINE.MedicineCode = _VT_VNMEDICINE.MEDICINECODE;
                        SETUPGROUPMETHODMEDICINE.GroupMethodID = Convert.ToInt32(ddlEditSetupGroupMethod.SelectedItem.Value);
                        SETUPGROUPMETHODMEDICINE.MedicineName_EN = VT_STOCK_MASTER.EngName;
                        SETUPGROUPMETHODMEDICINE.MedicineName_TH = VT_STOCK_MASTER.ThaiName;
                        SETUPGROUPMETHODMEDICINE.QTY = Convert.ToDouble(txtEditMedQTY.Text);
                        SETUPGROUPMETHODMEDICINE.AMT = Convert.ToDouble(txtEditMedAMT.Text);
                        SETUPGROUPMETHODMEDICINE.UnitPrice = Convert.ToDouble(txtEditUnitPrice.Text);
                        SETUPGROUPMETHODMEDICINE.UnitCode = ddlEditUnit.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.DoseTypeCode = ddlEditDoseType.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.DoseQTY = ddlEditDoseQTY.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.DoseUnitCode = ddlEditDoseUnit.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.DoseCode = ddlEditDoseCode.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.AUXLABEL1 = ddlEditAuxLabel1.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.AUXLABEL2 = ddlEditAuxLabel2.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.AUXLABEL3 = ddlEditAuxLabel3.SelectedItem.Value;
                        SETUPGROUPMETHODMEDICINE.Remark = txtEditRemark.Text.Trim();
                        if (new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).CheckDup(SETUPGROUPMETHODMEDICINE) == false)
                        {

                            ReturnValue rvSetup = new BLSETUPGROUPMETHODMEDICINE(appConnDBInfo).Insert(SETUPGROUPMETHODMEDICINE);
                            if (rvSetup.Value == true)
                            {
                                alertScript += string.Format(" " +
                                               " $.notify('Add {0} to {1} completed.', " +
                                               "      {{ " +
                                               "          className: 'success', " +
                                               "          position: 'bottom right', " +
                                               "          clickToHide: true " +
                                               "      }} " +
                                               "  ); " +
                                               "  ", SETUPGROUPMETHODMEDICINE.MedicineName, ddlEditSetupGroupMethod.SelectedItem.Text);

                            }
                            else
                            {
                                alertScript += string.Format(@"
                                                $.notify('{0}',
                                                    {{
                                                        className: 'error',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                                ", rvSetup.Exception);
                                status = false;

                            }
                        }
                        else
                        {
                            alertScript += string.Format(@"
                                            $.notify('This medicine item was already entered in {0} Setup Group Method. Please try checked again',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                                      ", ddlEditSetupGroupMethod.SelectedItem.Text);
                            status = false;
                        }
                    }


                    if (status == true)
                    {
                        ReturnValue rv = new BLVT_VNMEDICINE(extConnDBInfo).Update(_VT_VNMEDICINE);
                        if (rv.Value == true)
                        {
                            LoadVNMedicineList(_VT_VNMEDICINE);
                            alertScript += string.Format("" +
                                                   " $('#modalEditMedicine').modal('hide'); " +
                                                   " $.notify('Update completed.', " +
                                                   "      {{ " +
                                                   "          className: 'success', " +
                                                   "          position: 'bottom right', " +
                                                   "          clickToHide: true " +
                                                   "      }} " +
                                                   "  ); " +
                                                   "  ");

                        }
                        else
                        {

                            alertScript += string.Format(@"
                                                $('#modalEditMedicine').modal('hide'); 
                                                $.notify('Error : {0}',
                                                    {{
                                                        className: 'error',
                                                        position: 'bottom right',
                                                        clickToHide: true
                                                    }}
                                                );
                                                 ", rv.Exception.ToString());
                          
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                alertScript += string.Format(@"
                                            $.notify('Error : {0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                               ", ex.Message);

            }
            string msg = string.Format(@"javascript: $(document).ready(function(){{ {0} }});   ", alertScript);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                 msg, true);
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

                if (isQTY == false || QTY <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEditMedQTY);
                }
                else if (isAMT == false || AMT <= 0)
                {
                    ScriptManager.GetCurrent(this.Page).SetFocus(this.txtEditMedAMT);
                }


                valid = false;
            }
            return valid;
        }

        [System.Web.Services.WebMethod]
        public static string SearchMedicine(string textSearch, int startPage, int per_page)
        {

            VT_STOCK_MASTER VT_STOCKMASTER = new VT_STOCK_MASTER();
            var result = new BLVT_STOCK_MASTER(extConnDBInfo).SearchStockMaster(textSearch, startPage, per_page);
            var json = JsonConvert.SerializeObject(result);
            return json;

        }


    }
}