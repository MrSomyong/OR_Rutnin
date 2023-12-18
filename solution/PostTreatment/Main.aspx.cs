using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using DAL.Info;
using System.Data;

namespace solution.PostTreatment
{
    public partial class Main : System.Web.UI.Page
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

                    VT_VNMEDICINE _VT_VNMEDICINE = new VT_VNMEDICINE();
                    _VT_VNMEDICINE.VN = VN;
                    _VT_VNMEDICINE.VISITDATE = visitDate;
                    _VT_VNMEDICINE.SUFFIX = suffix;
                    LoadVNMedicineList(_VT_VNMEDICINE);

                    TREATMENT treatment = new TREATMENT();
                    treatment.VN = VN;
                    treatment.VISITDATE = visitDate;
                    treatment.SUFFIX = suffix;
                    
                    LoadOPDTreatmentList(treatment);
                    LoadDFList(_VT_VNTREAT);
                    LoadVNMedicineList(_VT_VNMEDICINE);
                    LoadItemChargeList(treatment);
                    LoadItemChargeAllList(treatment);
                    LoadDFTreatmentChargeList(_VT_VNTREAT);
                    LoadDFTreatmentChargeAllList(_VT_VNTREAT);

                    decimal TotalTreatmentPrice = Convert.ToDecimal(new BLTREATMENT(extConnDBInfo).GetTotalTreatmentPrice(treatment));
                    decimal TotalDFPrice = Convert.ToDecimal(new BLVT_VNTREAT(extConnDBInfo).GetTotalTreatmentPrice(_VT_VNTREAT, 1));
                    ltlTotalTreatmentPrice.Text = Convert.ToDecimal(TotalTreatmentPrice).ToString("#,##0.00");
                    ltlTotalDFPrice.Text = Convert.ToDecimal(TotalDFPrice).ToString("#,##0.00");
                    ltlAllTotalPrice.Text = Convert.ToDecimal(TotalTreatmentPrice + TotalDFPrice).ToString("#,##0.00");

                    decimal TotalTreatmentPriceAll = Convert.ToDecimal(new BLTREATMENT(extConnDBInfo).GetTotalTreatmentPriceAll(treatment));
                    decimal TotalDFPriceAll = Convert.ToDecimal(new BLVT_VNTREAT(extConnDBInfo).GetTotalTreatmentPriceAll(_VT_VNTREAT, 1));
                    ltlTotalTreatmentPriceAll.Text = Convert.ToDecimal(TotalTreatmentPriceAll).ToString("#,##0.00");
                    ltlTotalDFPriceAll.Text = Convert.ToDecimal(TotalDFPriceAll).ToString("#,##0.00");
                    ltlTotalPriceAllClinic.Text = Convert.ToDecimal(TotalTreatmentPriceAll + TotalDFPriceAll).ToString("#,##0.00");
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }



            }
            else
            {
                RIGHTCODE = hdRightCode.Value;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ? new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1;
            }
        }


        private void LoadHyperLinkControl()
        {

            List<SETUPHYPERLINK> lstLink = (List<SETUPHYPERLINK>) new BLSETUPHYPERLINK(appConnDBInfo).SearchAll().Where(l => l.IsShow == true).ToList();
            Int32 i; //create a integer variable
            for (i = 0; i < lstLink.Count() ; i++) // will generate 10 LinkButton
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

        public void LoadOPDVisitInfo(VT_VNMASTER _VT_VNMASTER)
        {
            try
            {
                VT_VNMASTER VT_VNMASTER = new BLVT_VNMASTER(extConnDBInfo).GetVNDetailByKey(_VT_VNMASTER);
                lblVN.Text = string.Format("{0}/{1}", VT_VNMASTER.VN, VT_VNMASTER.SUFFIX);
                lblVisitDate.Text = CultureInfo.GetDatetime(VT_VNMASTER.VISITDATE.Value, YearType.Thai).ToString("dd MMM yyyy");
                lblClinic.Text = VT_VNMASTER.CLINICNAME;
                lblDoctor.Text = VT_VNMASTER.DoctorName;
                lblRight.Text = VT_VNMASTER.RIGHTNAME;
                hdDoctorCode.Value = VT_VNMASTER.DOCTOR;
                RIGHTCODE = VT_VNMASTER.RIGHTCODE;
                hdRightCode.Value = VT_VNMASTER.RIGHTCODE;
                FixRate = !string.IsNullOrEmpty(RIGHTCODE) ? new BLVT_RIGHTCODE(extConnDBInfo).GetRightCodeByKey(RIGHTCODE).FixRate : 1;
                hdFixRate.Value = Convert.ToString(FixRate);
                if (VT_VNMASTER.Close == "Y")
                {
                    CloseVN = "Y";
                }

                if (VT_VNMASTER.HoldBill == true || CloseVN == "Y") {
                    lnkTreatment.Enabled = false;
                    lnkDF.Enabled = false;
                    lnkMedicine.Enabled = false;
                    lnkGroup.Enabled = false;
                }


            }
            catch { }

        }
        public void LoadOPDTreatmentList(TREATMENT TREATMENT)
        {
            try
            {
                List<TREATMENT> lstVNTREAT = new BLTREATMENT(extConnDBInfo, appConnDBInfo).GetAllTREATMENT(TREATMENT,true);
                foreach (var list in lstVNTREAT.Where(i => i.GroupType == "Treatment"))
                {
                    VT_TREATMENTCODE_SPLITDF treatmentSplitDF = new BLVT_TREATMENTCODE_SPLITDF(extConnDBInfo).GetTreatmentCodeSplitDFByAllKey(list.ITEMCODE);
                    VT_TREATMENTCODE treatment = new BLVT_TREATMENTCODE(extConnDBInfo).GetTreatmentCodeByKey(list.ITEMCODE, FixRate);
                    if (!string.IsNullOrEmpty(treatmentSplitDF.TreatmentCode))
                    {
                        if (treatmentSplitDF.TreatmentCode == list.ITEMCODE)
                        {
                            treatment.StdPrice = treatment.StdPrice - treatmentSplitDF.StdPrice1;

                        }
                        else if (treatmentSplitDF.DFTreatmentCode == list.ITEMCODE)
                        {
                            treatment.StdPrice = treatmentSplitDF.StdPrice1;
                        }
                    }
                    list.UNITPRICE = treatment.StdPrice;
                }





                gvOPDTreatmentList.DataSource = lstVNTREAT;
                gvOPDTreatmentList.DataBind();

            }
            catch { }

        }

        public void LoadDFList(VT_VNTREAT _VT_VNTREAT)

        {
            try
            {
                List<VT_VNTREAT> lstVNTREAT = new BLVT_VNTREAT(extConnDBInfo,appConnDBInfo).GetAllVNTreatByKey(_VT_VNTREAT, true,true);
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
                gvDFList.DataSource = lstVNTREAT;
                gvDFList.DataBind();

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
        public void LoadItemChargeList(TREATMENT _TREATMENT)
        {
            try
            {
                List<TREATMENT> lstTreatment = new BLTREATMENT(extConnDBInfo, appConnDBInfo).GetAllItemCharge(_TREATMENT);
                gvItemChargeList.DataSource = lstTreatment;
                gvItemChargeList.DataBind();

            }
            catch { }

        }

        public void LoadItemChargeAllList(TREATMENT _TREATMENT)
        {
            try
            {
                List<TREATMENT> lstTreatment = new BLTREATMENT(extConnDBInfo, appConnDBInfo).GetAllItemChargeAll(_TREATMENT);
                gvItemChargeAllList.DataSource = lstTreatment;
                gvItemChargeAllList.DataBind();

            }
            catch { }

        }
        private void LoadDFTreatmentChargeList(VT_VNTREAT _VT_VNTREAT)
        {
            try
            {
                List<VT_VNTREAT> lstVNTREAT = new BLVT_VNTREAT(extConnDBInfo, appConnDBInfo).SearchVT_VNTreatByKey(_VT_VNTREAT, true);
                gvDFTreatmentCharge.DataSource = lstVNTREAT;
                gvDFTreatmentCharge.DataBind();

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void LoadDFTreatmentChargeAllList(VT_VNTREAT _VT_VNTREAT)
        {
            try
            {
                List<VT_VNTREAT> lstVNTREAT = new BLVT_VNTREAT(extConnDBInfo, appConnDBInfo).DFTreatmentChargeAll(_VT_VNTREAT);
                gvDFTreatmentChargeAll.DataSource = lstVNTREAT;
                gvDFTreatmentChargeAll.DataBind();

            }
            catch (Exception exc)
            {
                throw exc;
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

        protected void gvOPDTreatmentList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    Label lblType = (Label)e.Row.FindControl("lblType");

                   
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsDeleted")) == true)
                    {
                       lblType.Text = "<i class='fa fa-check fa-lg text-success'></i>";
                    }
                    else {
                        e.Row.Attributes["class"] += "inactive";
                        lblType.Text = "<i class='fa fa-times fa-lg text-alert'></i>";
                    }
                }
            }



        }

        protected void gvDFList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.DataItem != null)
                {
                    Label lblType = (Label)e.Row.FindControl("lblType");

                  
                    if (Convert.ToBoolean(DataBinder.Eval(e.Row.DataItem, "IsDeleted")) == true)
                    {
                        lblType.Text = "<i class='fa fa-check fa-lg text-success'></i>";
                    }
                    else
                    {
                        e.Row.Attributes["class"] += "inactive";
                        lblType.Text = "<i class='fa fa-times fa-lg text-alert'></i>";
                        //e.Row.Cells[5].CssClass = "inactive";
                    }
                }
            }



        }

        protected void gvItemChargeList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;

                GridViewRow gr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell thc1 = new TableHeaderCell();
                TableHeaderCell thc2 = new TableHeaderCell();
                TableHeaderCell thc3 = new TableHeaderCell();
                TableHeaderCell thc4 = new TableHeaderCell();

                thc1.Text = "Item Charge";
                thc1.ColumnSpan = 3;
                thc1.CssClass = "text-center header";

                gr.Cells.AddRange(new TableCell[] { thc1 });

                gvItemChargeList.Controls[0].Controls.AddAt(0, gr);
            }


        }

        protected void gvItemChargeAllList_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;

                GridViewRow gr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell thc1 = new TableHeaderCell();
                TableHeaderCell thc2 = new TableHeaderCell();
                TableHeaderCell thc3 = new TableHeaderCell();
                TableHeaderCell thc4 = new TableHeaderCell();

                thc1.Text = "Item Charge All";
                thc1.ColumnSpan = 3;
                thc1.CssClass = "text-center header";

                gr.Cells.AddRange(new TableCell[] { thc1 });

                gvItemChargeAllList.Controls[0].Controls.AddAt(0, gr);
            }


        }

        protected void gvDFTreatmentCharge_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;

                GridViewRow gr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell thc1 = new TableHeaderCell();
                TableHeaderCell thc2 = new TableHeaderCell();
                TableHeaderCell thc3 = new TableHeaderCell();

                thc1.Text = "DF Treatment Charge";
                thc1.ColumnSpan = 3;
                thc1.CssClass = "text-center table-header";

                gr.Cells.AddRange(new TableCell[] { thc1 });

                gvDFTreatmentCharge.Controls[0].Controls.AddAt(0, gr);
            }
        }


        protected void gvDFTreatmentChargeAll_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Visible = false;

                GridViewRow gr = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableHeaderCell thc1 = new TableHeaderCell();
                TableHeaderCell thc2 = new TableHeaderCell();
                TableHeaderCell thc3 = new TableHeaderCell();

                thc1.Text = "DF Treatment Charge All";
                thc1.ColumnSpan = 3;
                thc1.CssClass = "text-center table-header";

                gr.Cells.AddRange(new TableCell[] { thc1 });

                gvDFTreatmentChargeAll.Controls[0].Controls.AddAt(0, gr);
            }
        }

    }
}