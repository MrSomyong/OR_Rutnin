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
//using System.Globalization;


namespace solution.PostTreatment
{
    public partial class PostTreatment : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;
        // string HN = string.Empty;
        //string VN = string.Empty;
        //int suffix;
        //DateTime visitDate;
        public string HN { get; set; }
        public string VN { get; set; }
        public int suffix { get; set; }
        public DateTime visitDate { get; set; }
        public string CloseVN { get; set; }
        public string RIGHTCODE { get; set; }
        public int FixRate { get; set; }
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

                    VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                    _VT_VNTREAT.VN = VN;
                    _VT_VNTREAT.VISITDATE = visitDate;
                    _VT_VNTREAT.SUFFIX = suffix;
                    LoadOPDTreatmentList(_VT_VNTREAT);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }
                LoadOrderType();
                LoadTreatmentCode();
                LoadTreatmentMethod();
                LoadTreatmentItem();
                LoadDoctorList();
            }
            else
            {
                RIGHTCODE = hdRightCode.Value;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ? new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1;
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


            }
            catch (Exception ex)
            {
                throw ex;
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



        private void LoadTreatmentItem()
        {
            gvTreatmentItem.DataSource = new List<VT_TREATMENTCODE>();
            gvTreatmentItem.DataBind();
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
                lblRight.Text = VT_VNMASTER.RIGHTNAME;
                hdDoctorCode.Value = VT_VNMASTER.DOCTOR;
                RIGHTCODE = VT_VNMASTER.RIGHTCODE;
                hdRightCode.Value = VT_VNMASTER.RIGHTCODE;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ?  new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1 ;
                hdFixRate.Value = Convert.ToString(FixRate);
                if (VT_VNMASTER.Close == "Y" || VT_VNMASTER.HoldBill == true)
                {
                    ddlTreatment.Enabled = false;
                    ddlTreatmentMethod.Enabled = false;
                    CloseVN = "Y";
                }


            }
            catch { }

        }

      

        public void LoadOPDTreatmentList(VT_VNTREAT _VT_VNTREAT)
        {
            try
            {
                List<VT_VNTREAT> lstVNTREAT = new BLVT_VNTREAT(extConnDBInfo,appConnDBInfo).GetAllVNTreatByKey(_VT_VNTREAT, true);

                foreach (var list in lstVNTREAT)
                {
                    VT_TREATMENTCODE_SPLITDF treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByAllKey(list.TREATMENTCODE);
                    VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(list.TREATMENTCODE, FixRate);
                    if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                    {
                        if (treatmentSplitDF.TreatmentCode == list.TREATMENTCODE)
                        {
                            treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;

                        }
                        else if (treatmentSplitDF.DFTreatmentCode == list.TREATMENTCODE)
                        {
                            treatment.StdPrice = treatmentSplitDF.StdPrice1;
                        }
                    }
                    list.CHARGEAMT = treatment.StdPrice;
                }


                gvOPDTreatmentList.DataSource = lstVNTREAT;
                gvOPDTreatmentList.DataBind();

            }
            catch (Exception exc)
            {
                throw exc;
            }

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

        private void LoadTreatmentMethod()
        {
            try
            {
                ListItem litTreatmentMethod = new ListItem();
                litTreatmentMethod.Text = "None";
                litTreatmentMethod.Value = "";

                //List<VT_TREATMENTMETHODCODE> lstTreatmentMethod = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentMethodAll();

                //ddlTreatmentMethod.DataSource = lstTreatmentMethod;
                //ddlTreatmentMethod.DataValueField = "MethodCode";
                //ddlTreatmentMethod.DataTextField = "TreatmentMethodName";
                //ddlTreatmentMethod.DataBind();
                //ddlTreatmentMethod.Items.Insert(0, litTreatmentMethod);


                string HostName = Dns.GetHostName();
                SETUPCOMPUTER SetupComputer = new BLSETUPCOMPUTER(appConnDBInfo).SearchByKey(HostName);

                ddlTreatmentMethod.ClearSelection();
                if (!string.IsNullOrEmpty(SetupComputer.ComputerCode))
                {
                    List<SETUPCOMPUTERMETHOD> list = new BLSETUPCOMPUTERMETHOD(appConnDBInfo).GetAllByKey(SetupComputer.ComputerCode);
                    ddlTreatmentMethod.DataSource = list;
                    ddlTreatmentMethod.DataValueField = "MethodCode";
                    ddlTreatmentMethod.DataTextField = "MethodName";
                    ddlTreatmentMethod.DataBind();
                    ddlTreatmentMethod.Items.Insert(0, litTreatmentMethod);
                }
                //else {

                //    List<VT_TREATMENTMETHODCODE> lstTreatmentMethod = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentMethodAll();
                //    ddlTreatmentMethod.DataSource = lstTreatmentMethod;
                //    ddlTreatmentMethod.DataValueField = "MethodCode";
                //    ddlTreatmentMethod.DataTextField = "TreatmentMethodName";
                //    ddlTreatmentMethod.DataBind();
                //    ddlTreatmentMethod.Items.Insert(0, litTreatmentMethod);
                //}


            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                ddlDoctorList.DataSource = lstDOCTORMASTERVO;
                ddlDoctorList.DataValueField = "DOCTOR";
                ddlDoctorList.DataTextField = "DoctorName";
                ddlDoctorList.DataBind();
                ddlDoctorList.Items.Insert(0, litSurgeon);
                ddlDoctorList.ClearSelection();

                ddlEditDoctorList.DataSource = lstDOCTORMASTERVO;
                ddlEditDoctorList.DataValueField = "DOCTOR";
                ddlEditDoctorList.DataTextField = "DoctorName";
                ddlEditDoctorList.DataBind();
                ddlEditDoctorList.Items.Insert(0, litSurgeon);
                if (!string.IsNullOrEmpty(hdDoctorCode.Value))
                {
                    //ddlDoctorList.Items.FindByValue(hdDoctorCode.Value).Selected = true;
                    Boolean isSelected = ddlDoctorList.Items.FindByValue(hdDoctorCode.Value) == null ? false : ddlDoctorList.Items.FindByValue(hdDoctorCode.Value).Selected = true;
                }




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
                    PictureFileName = ORPATIENTVO.PictureFileName;
                    lblPatientType.Text = ORPATIENTVO.PatientType;
                }
            }
            catch { }
        }

        protected void gvOPDTreatmentList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "delTreatment")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOPDTreatmentList.Rows[index];
                VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                _VT_VNTREAT.TREATMENTCODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["TREATMENTCODE"].ToString();
                _VT_VNTREAT.CHARGECODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["CHARGECODE"].ToString();
                _VT_VNTREAT.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                _VT_VNTREAT.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                _VT_VNTREAT.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                _VT_VNTREAT.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                _VT_VNTREAT.CXLBYUSERCODE = Session["USERNANME"].ToString();
                ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).CXLVNTREAT(_VT_VNTREAT);
                if (rv.Value)
                {
                    LoadOPDTreatmentList(_VT_VNTREAT);
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
            else if (e.CommandName == "editTreatment")
            {

                setDefaultControlEdit();

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvOPDTreatmentList.Rows[index];
                string treatmentCode = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["TREATMENTCODE"].ToString();
                //VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode,FixRate);

                VT_TREATMENTCODE_SPLITDF treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByKey(treatmentCode);
                VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode, FixRate);
                if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                    treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;
                else if (treatmentSplitDF.DFTreatmentCode == treatmentCode)
                {
                    treatment.StdPrice = treatmentSplitDF.StdPrice1;
                }

                VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                _VT_VNTREAT.TREATMENTCODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["TREATMENTCODE"].ToString();
                _VT_VNTREAT.CHARGECODE = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["CHARGECODE"].ToString();
                _VT_VNTREAT.VN = gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VN"].ToString();
                _VT_VNTREAT.VISITDATE = Convert.ToDateTime(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["VISITDATE"]);
                _VT_VNTREAT.SUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUFFIX"]);
                _VT_VNTREAT.SUBSUFFIX = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["SUBSUFFIX"]);
                _VT_VNTREAT.TREATMENTENTRYSTYLE = Convert.ToInt32(gvOPDTreatmentList.DataKeys[row.RowIndex].Values["TREATMENTENTRYSTYLE"]);
                _VT_VNTREAT.CHARGEAMT = treatment.StdPrice;
                _VT_VNTREAT.ZeroPrice = treatment.ZeroPrice;
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
        }

        protected void btnAddTreatment_Click(object sender, EventArgs e)
        {
            string treatmentCode = ddlTreatment.SelectedItem.Value;
            InsertTreatment(null, treatmentCode);
        }

        protected void gvTreatmentItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvTreatmentItem.Rows[index];

                string methodCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["MethodCode"].ToString();
                string treatmentCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();



                foreach (GridViewRow rowx in gvTreatmentItem.Rows)
                {
                    CheckBox chkSelect = (CheckBox)rowx.Cells[0].FindControl("chkSelect");
                    if (chkSelect.Checked == true)
                    {
                        rowx.CssClass = "highlightRow";
                    }
                    else
                        rowx.CssClass = "";
                  
                }

                InsertTreatment(methodCode, treatmentCode);
            }
        }

        private bool CheckTreatmentValidation()
        {
            bool valid = true;
            string message = string.Empty;
            string treatementCode = ddlTreatment.SelectedItem.Value;
            string doctorCode = ddlDoctorList.SelectedItem.Value;
           
            if (treatementCode == string.Empty || doctorCode == string.Empty)
            {
                message = string.IsNullOrEmpty(treatementCode) ? "   - Treatment " : "";
                message += string.IsNullOrEmpty(doctorCode) ? (!string.IsNullOrEmpty(message) ? "\\n" : "") + "  - Doctor " : "";
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


        private void InsertTreatment(string methodCode, string treatmentCode, bool showNotify = true)
        {
            VT_TREATMENTCODE treatment;
            VT_TREATMENTCODE_SPLITDF treatmentSplitDF = null;
            if (string.IsNullOrEmpty(methodCode) == true)
            {
                if (CheckTreatmentValidation())
                {
                    treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByKey(treatmentCode);
                    treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode, FixRate);
                    if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                        treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;
                    else if (treatmentSplitDF.DFTreatmentCode == treatmentCode)
                    {
                        treatment.StdPrice = treatmentSplitDF.StdPrice1;
                    }
                }
                else
                    return;

            }
            else
                //Edit 15.02 แก้ไขให้อ่านจาก group method
                //treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(methodCode, treatmentCode);
            treatment = new BLVT_TREATMENTCODE(appConnDBInfo).GetTreatmentCodeByKey(methodCode, treatmentCode);
           
            VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
            _VT_VNTREAT.VISITDATE = visitDate;
            _VT_VNTREAT.VN = VN;
            _VT_VNTREAT.SUFFIX = suffix;
            _VT_VNTREAT.TREATMENTCODE = treatment.CODE;
            _VT_VNTREAT.TREATMENTNAME = treatment.Name;
            _VT_VNTREAT.CHARGEAMT = treatment.StdPrice;
            _VT_VNTREAT.CHARGECODE = treatment.Activity;
            _VT_VNTREAT.AMT = treatment.StdPrice;
            _VT_VNTREAT.QTY = 1;
            _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(methodCode) ? ddlDoctorList.SelectedItem.Value  : (string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value);
            _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
            _VT_VNTREAT.TREATMENTENTRYSTYLE = treatment.TreatmentStyle;
            _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
            _VT_VNTREAT.TYPEOFCHARGE = 0;
            _VT_VNTREAT.REVERSE = 0;
            _VT_VNTREAT.PAIDFLAG = 0;
            _VT_VNTREAT.ZeroPrice = treatment.ZeroPrice;
            

            if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT) == false)
            {
                if ((_VT_VNTREAT.AMT == 0 && _VT_VNTREAT.ZeroPrice != 1) || string.IsNullOrEmpty(methodCode) == true)
                //if(string.IsNullOrEmpty(methodCode) == true)
                {
                    _VT_VNTREAT.TreatmentCodeInfo = treatment;
                    EditTreatment(_VT_VNTREAT);
                }
                else
                {
                    ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).Insert(_VT_VNTREAT);
                    if (rv.Value == true)
                    {
                        if (treatmentSplitDF != null && !string.IsNullOrEmpty(treatmentSplitDF.DFTreatmentCode))
                            InsertTreatmentSplitDF(treatmentSplitDF.TreatmentCode);
                        ddlTreatment.SelectedIndex = 0;
                        LoadOPDTreatmentList(_VT_VNTREAT);
                        if (showNotify)
                        {
                            string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                " setTimeout( " +
                                                "    function() {{ " +
                                                "      $('[id*=ddlTreatment]').select2('open');" +
                                                "    }}, 1000);" +
                                                " $.notify('Add {0} : {1} completed.', " +
                                                "      {{ " +
                                                "          className: 'success', " +
                                                "          position: 'bottom right', " +
                                                "          clickToHide: true " +
                                                "      }} " +
                                                "  ); " +
                                                " }}); ", treatment.Name, treatment.CODE);
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



        private bool InsertTreatmentSplitDF(string treatmentCode)
        {
            VT_TREATMENTCODE treatment;
            VT_TREATMENTCODE_SPLITDF treatmentSplitDF;

            treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByKey(treatmentCode);
            treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentSplitDF.DFTreatmentCode, FixRate);
            if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                treatment.StdPrice = treatmentSplitDF.StdPrice1;


            VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
            _VT_VNTREAT.VISITDATE = visitDate;
            _VT_VNTREAT.VN = VN;
            _VT_VNTREAT.SUFFIX = suffix;
            _VT_VNTREAT.TREATMENTCODE = treatment.CODE;
            _VT_VNTREAT.TREATMENTNAME = treatment.Name;
            _VT_VNTREAT.CHARGEAMT = treatment.StdPrice;
            _VT_VNTREAT.CHARGECODE = treatment.Activity;
            _VT_VNTREAT.AMT = treatment.StdPrice;
            _VT_VNTREAT.QTY = 1;
            _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value;
            _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
            _VT_VNTREAT.TREATMENTENTRYSTYLE = treatment.TreatmentStyle;
            _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
            _VT_VNTREAT.TYPEOFCHARGE = 0;
            _VT_VNTREAT.REVERSE = 0;
            _VT_VNTREAT.PAIDFLAG = 0;
            if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT) == false)
            {
                ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).Insert(_VT_VNTREAT);
                return rv.Value;
            }
            return false;
        }



        private void EditTreatment(VT_VNTREAT _VT_VNTREAT)
        {
            setDefaultControlEdit();
            hdTreatmentCode.Value = _VT_VNTREAT.TREATMENTCODE;
            hdTreatmentStyle.Value = _VT_VNTREAT.TREATMENTENTRYSTYLE.ToString();
            hdSubSuffix.Value = _VT_VNTREAT.SUBSUFFIX.ToString();
            ddlOrderType.ClearSelection();
            ddlOrderType.Items.FindByValue(_VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
            lblTreatmentName.Text = string.Format("[{0}] {1}", _VT_VNTREAT.TREATMENTCODE, _VT_VNTREAT.TREATMENTNAME);
            txtAmt.Text = string.Format("{0:0.##}", _VT_VNTREAT.AMT);
            txtQty.Text = string.Format("{0:0.##}", _VT_VNTREAT.QTY);
            hdChargeCode.Value = string.Format("{0:0.##}",_VT_VNTREAT.CHARGECODE);
            txtChargeAMT.Text = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT);
            hdChargeAMT.Value = string.Format("{0:0.##}", _VT_VNTREAT.CHARGEAMT); 
            txtRemark.Text = _VT_VNTREAT.REMARKS;
            ddlOrderType.ClearSelection();
            ddlOrderType.Items.FindByValue(_VT_VNTREAT.TYPEOFCHARGE.ToString()).Selected = true;
            hdZeroPrice.Value = _VT_VNTREAT.ZeroPrice.ToString();
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


        protected void ddlTreatmentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string methodCode = ddlTreatmentMethod.SelectedItem.Value;

                //Edit 15.02 แก้ให้อ่านจาก group method
                //List<VT_TREATMENTMETHODCODE> lstTreatmentCode = new BLVT_TREATMENTMETHODCODE(extConnDBInfo).GetTreatmentCodeByMethodCode(methodCode);

                List<VT_TREATMENTMETHODCODE> lstTreatmentCode = new BLVT_TREATMENTMETHODCODE(appConnDBInfo).GetTreatmentCodeByMethodCode(methodCode);
                gvTreatmentItem.DataSource = lstTreatmentCode;  //.Where(item => item.StdPrice1 > 0).OrderByDescending(i => i.Name).Take(5);
                gvTreatmentItem.DataBind();



            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected bool CheckDate(String date)
        {
            DateTime Temp;


            if (DateTime.TryParse(date, out Temp) == true)
                return true;
            else
                return false;
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
                isAMT == false || (AMT <= 0 && zeroPrice == 0)||
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
                else if(string.IsNullOrEmpty(doctor))
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
                    _VT_VNTREAT.GROUPREQUESTCODE = string.Empty;
                    _VT_VNTREAT.PAIDFLAG = 0;
                    _VT_VNTREAT.ZeroPrice = Convert.ToInt32(hdZeroPrice.Value);
                    
                    // DateTime dateTimeStart = DateTime.ParseExact(txtStartDateTime.Text.Trim(), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"));
                    // _VT_VNTREAT.TREATMENTDATETIMEFROM = CheckDate(dateTimeStart.ToString()) ? dateTimeStart : _VT_VNTREAT.TREATMENTDATETIMEFROM = null;
                    //DateTime dateTimeEnd = DateTime.ParseExact(txtEndDateTime.Text.Trim(), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"));
                    //_VT_VNTREAT.TREATMENTDATETIMEFROM = CheckDate(dateTimeEnd.ToString()) ? dateTimeEnd : _VT_VNTREAT.TREATMENTDATETIMETO = null;


                    DateTime dateTimeStart; 
                    bool isDateTimeStart = DateTime.TryParseExact(string.Format("{0} {1}", txtStartDateTime.Text.Trim(), txtStartTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dateTimeStart);
                    _VT_VNTREAT.TREATMENTDATETIMEFROM = isDateTimeStart ? dateTimeStart : _VT_VNTREAT.TREATMENTDATETIMEFROM = null;

                    DateTime dateTimeEnd;
                    bool isDateTimeEnd = DateTime.TryParseExact(string.Format("{0} {1}", txtEndDateTime.Text.Trim(), txtEndTime.Text.Trim()), "dd/MM/yyyy HH:mm", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out dateTimeEnd);
                    _VT_VNTREAT.TREATMENTDATETIMETO = isDateTimeEnd ? dateTimeEnd : _VT_VNTREAT.TREATMENTDATETIMETO = null;

                    if (btnUpdateTreatment.Text == "Add")
                    {


                        _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value;
                        _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
                        _VT_VNTREAT.TREATMENTENTRYSTYLE = !string.IsNullOrEmpty(hdTreatmentStyle.Value) ? Convert.ToInt32(hdTreatmentStyle.Value) : _VT_VNTREAT.TREATMENTENTRYSTYLE =  null;
                        _VT_VNTREAT.TIMETYPE = !string.IsNullOrEmpty(hfTimeType.Value) ? Convert.ToInt32(hfTimeType.Value) : _VT_VNTREAT.TIMETYPE = null;
                        _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();

                       /* Begin find TreatmentCodeSplitDF */
                        VT_TREATMENTCODE treatment;
                        string methodCode = string.Empty;
                        string treatmentCode = hdTreatmentCode.Value;
                        if (string.IsNullOrEmpty(methodCode) == true)
                            treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode, FixRate);
                        else
                            treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(methodCode, treatmentCode);

                        VT_TREATMENTCODE_SPLITDF treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByKey(treatmentCode);
                        if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                            treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;
                        else if (treatmentSplitDF.DFTreatmentCode == treatmentCode)
                        {
                            treatment.StdPrice = treatmentSplitDF.StdPrice1;
                        }
                        /* End */

                        if (new BLVT_VNTREAT(extConnDBInfo).CheckDup(_VT_VNTREAT) == false)
                        {
                            ReturnValue rv = new BLVT_VNTREAT(extConnDBInfo).Insert(_VT_VNTREAT);
                            if (rv.Value == true)
                            {
                                /* Begin Save TreatmentCodeSplitDF */
                                if (treatmentSplitDF != null && !string.IsNullOrEmpty(treatmentSplitDF.DFTreatmentCode))
                                    InsertTreatmentSplitDF(treatmentSplitDF.TreatmentCode);
                                /* End */
                                ddlTreatment.SelectedIndex = 0;
                                LoadOPDTreatmentList(_VT_VNTREAT);

                                string alertScript = string.Format("javascript: $(document).ready(function(){{ " +
                                                        " setTimeout( " +
                                                        "    function() {{ " +
                                                        "      $('[id*=ddlTreatment]').select2('open');" +
                                                        "    }}, 1000);" +
                                                        " $('#modalEditTreatment').modal('hide'); " +
                                                        " $.notify('Add {0} : {1} completed.', " +
                                                        "      {{ " +
                                                        "          className: 'success', " +
                                                        "          position: 'bottom right', " +
                                                        "          clickToHide: true " +
                                                        "      }} " +
                                                        "  ); " +
                                                        " }}); ", ddlTreatment.SelectedItem.Text, ddlTreatment.SelectedItem.Value);
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
                            LoadOPDTreatmentList(_VT_VNTREAT);

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

        protected void gvTreatmentItem_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.DataItem != null)
                {
                    string treatmentCode = gvTreatmentItem.DataKeys[e.Row.RowIndex].Values["TreatmentCode"].ToString();
                    VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode, FixRate);

                    if (treatment.TreatmentStyle == 5 || (treatment.ZeroPrice != 1 && treatment.StdPrice == 0)) { 
                        CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                        chkSelect.Checked = false;
                        chkSelect.Enabled = false;
                    }

                }
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

                    Label lblType = (Label)e.Row.FindControl("lblType");
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsDeleted")) == true)
                    {
                        lblType.Text = "<i class='fa fa-check fa-lg text-success'></i>";
                    }
                    else
                    {
                        Button btnDel = (Button)e.Row.FindControl("btnDeleteTreatment");
                        btnDel.Enabled = false;
                        Button btnEdit = (Button)e.Row.FindControl("btnEditTreatment");
                        btnEdit.Enabled = false;

                        e.Row.Attributes["class"] += "inactive";
                        lblType.Text = "<i class='fa fa-times fa-lg text-alert'></i>";
                    }


                    //CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                    //if (chkSelect.Checked)
                    //{
                    //    e.Row.Attributes["class"] = "highlightRow";
                    //}
                  
                }
            }

           

        }

        private void setDefaultControlEdit()
        {
           
            ddlOrderType.SelectedIndex = 0;
            txtAmt.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtChargeAMT.Text = string.Empty;
        }

        protected void gvTreatmentItem_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 0; i < gvTreatmentItem.Columns.Count - 1; i++)
                {
                    e.Row.Cells.RemoveAt(1);
                }
                e.Row.Cells[0].ColumnSpan = gvTreatmentItem.Columns.Count;
            }
        }

        protected void btnSubmitSelect_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvTreatmentItem.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    //CheckBox chkSelect = (CheckBox)rowx.Cells[0].FindControl("chkSelect");
                    CheckBox c = (CheckBox)row.FindControl("chkSelect");
                    if (c.Checked)
                    {
                        string methodCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["MethodCode"].ToString();
                        string treatmentCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();

                        VT_TREATMENTCODE treatment;
                        VT_TREATMENTCODE_SPLITDF treatmentSplitDF = null;
                        if (string.IsNullOrEmpty(methodCode) == true)
                        {
                            if (CheckTreatmentValidation())
                            {
                                treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByKey(treatmentCode);
                                treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(treatmentCode, FixRate);
                                if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                                    treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;
                                else if (treatmentSplitDF.DFTreatmentCode == treatmentCode)
                                {
                                    treatment.StdPrice = treatmentSplitDF.StdPrice1;
                                }
                            }
                            else
                                return;

                        }
                        else
                            treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(methodCode, treatmentCode);

                        VT_VNTREAT _VT_VNTREAT = new VT_VNTREAT();
                        _VT_VNTREAT.VISITDATE = visitDate;
                        _VT_VNTREAT.VN = VN;
                        _VT_VNTREAT.SUFFIX = suffix;
                        _VT_VNTREAT.TREATMENTCODE = treatment.CODE;
                        _VT_VNTREAT.TREATMENTNAME = treatment.Name;
                        _VT_VNTREAT.CHARGEAMT = treatment.StdPrice;
                        _VT_VNTREAT.CHARGECODE = treatment.Activity;
                        _VT_VNTREAT.AMT = treatment.StdPrice;
                        _VT_VNTREAT.QTY = 1;
                        _VT_VNTREAT.DOCTOR = string.IsNullOrEmpty(methodCode) ? ddlDoctorList.SelectedItem.Value : (string.IsNullOrEmpty(hdDoctorCode.Value) ? null : hdDoctorCode.Value);
                        _VT_VNTREAT.MAKEDATETIME = DateTime.Now;
                        _VT_VNTREAT.TREATMENTENTRYSTYLE = treatment.TreatmentStyle;
                        _VT_VNTREAT.ENTRYBYUSERCODE = Session["USERNANME"].ToString();
                        _VT_VNTREAT.TYPEOFCHARGE = 0;
                        _VT_VNTREAT.REVERSE = 0;
                        _VT_VNTREAT.PAIDFLAG = 0;
                        _VT_VNTREAT.ZeroPrice = treatment.ZeroPrice;

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
                                          }});   ", _VT_VNTREAT.TREATMENTCODE, _VT_VNTREAT.TREATMENTNAME);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }

                        if (_VT_VNTREAT.AMT <= 0)
                        {
                            string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $.notify('{0}',
                                                {{
                                                    className: 'error',
                                                    position: 'bottom right',
                                                    clickToHide: true
                                                }}
                                            );
                                          }});   ", string.Format("Item [{0}] : {1} cannot insert. Amount: Must be value greater than 0 ", _VT_VNTREAT.TREATMENTCODE, _VT_VNTREAT.TREATMENTNAME));
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                                alertScript, true);
                            return;
                        }

                    }
                }
            }


            int i = 0;
            foreach (GridViewRow row in gvTreatmentItem.Rows)
            {

                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox c = (CheckBox)row.FindControl("chkSelect");
                    if (c.Checked)
                    {

                        string methodCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["MethodCode"].ToString();
                        string treatmentCode = gvTreatmentItem.DataKeys[row.RowIndex].Values["TreatmentCode"].ToString();
                        InsertTreatment(methodCode, treatmentCode, false);
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
    }
}