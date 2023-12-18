using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.PostOR
{
    public partial class Display : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;
        public string hdSource = string.Empty;

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
                if (Request.QueryString["d"] != null)
                {
                    hdORID.Value = Request.QueryString["d"];
                    hdSource = Request.QueryString["s"];
                    ClearValue();
                    HideValue(false);
                    loadvalue(hdORID.Value);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalPopProcedureSelection();", true);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }
            }

        }

        public void loadvalue(string orid)
        {
            List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByORID(orid);
            foreach (ORHEADERVO hd in lsth)
            {
                //New Patient Name // Req : 3/7/2565
                lblPatientName.Text = hd.PatientName_IPPU;

                string anes1 = (new BLANESTHESIA(dbInfo).SearchByCode(hd.AnesthesiaType1)).NAME;
                string anes2 = (new BLANESTHESIA(dbInfo).SearchByCode(hd.AnesthesiaType2)).NAME;
                string anesSign = ORUtils.getAnesthesiaSign(hd.AnesthesiaSign);
                lblAnesthesiaType.Text = anes1+" " + anesSign + " " + anes2;
                lblPrediag.Text = hd.Prediag;
                lblORDate.Text = hd.strORDate;
                if (!string.IsNullOrEmpty(hd.Surgeon1))
                {
                    lblDoctor.Text += (new BLDOCTORMASTER(dbInfo).SearchByCode(hd.Surgeon1)).DoctorName;
                }
                if (!string.IsNullOrEmpty(hd.Surgeon2))
                {
                    if (!string.IsNullOrEmpty(lblDoctor.Text))
                    { lblDoctor.Text += " / "; }
                    lblDoctor.Text += (new BLDOCTORMASTER(dbInfo).SearchByCode(hd.Surgeon2)).DoctorName;
                }
                if (!string.IsNullOrEmpty(hd.Surgeon3))
                {
                    if (!string.IsNullOrEmpty(lblDoctor.Text))
                    { lblDoctor.Text += " / "; }
                    lblDoctor.Text += (new BLDOCTORMASTER(dbInfo).SearchByCode(hd.Surgeon3)).DoctorName;
                }
                loadOnmed(hd);
                loadORPatient(hd.HN);
                loadPrevOR(hd);
                //loadPostOROperation(hd);
                if (hdSource == "ADC")
                {
                    loadOROperation(hd);
                    btnADC.Visible = false;
                    btnPostOR2.Visible = false;

                    btnADC2.Visible = true;
                    btnPostOR.Visible = true;
                }
                else
                {
                    loadPostOROperation(hd);
                    btnADC2.Visible = false;
                    btnPostOR.Visible = false;

                    btnADC.Visible = true;
                    btnPostOR2.Visible = true;
                }
                
                loadImplant(hd.ORID);                
                loadHeadWarningMore(hd.ORID);
                loadHNUnderly(hd.HN);

                if (string.IsNullOrEmpty(lblPatientalDiag.Text))
                {
                    if (string.IsNullOrEmpty(lblPatientalDiagDesc.Text))
                    {
                        lblPatientalDiag.Text = "<strong>ไม่มี</strong>";
                    }
                }

                if (!loadAN(hd))
                {
                    loadVN(hd);
                }

            }
        }

        public bool loadAN(ORHEADERVO hd)
        {
            bool rv = false;
            try
            {
                VT_PATIENT_ANVO vl = new VT_PATIENT_ANVO();
                vl.HN = hd.HN;
                vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " " + hd.ORTime);
                List<VT_PATIENT_ANVO> lstVT_PATIENT_ANVO = new BLVT_PATIENT_AN(dbInfo).SearchAN(vl);
                if (lstVT_PATIENT_ANVO.Count > 0)
                {
                    lblAN.Text = lstVT_PATIENT_ANVO[0].AN;
                    divAN.Visible = true;
                    divANVN.Visible = true;
                    rv = true;
                }
                else
                {
                    loadVN(hd);
                    lblAN.Text = string.Empty;
                    divAN.Visible = false;
                    divANVN.Visible = false;
                    rv = false;
                }
            }
            catch { }
            return rv;
        }

        public void loadVN(ORHEADERVO hd)
        {
            try
            {
                VT_PATIENT_VNVO vl = new VT_PATIENT_VNVO();
                vl.HN = hd.HN;
                vl.ORDateTime = hd.ORDate;
                List<VT_PATIENT_VNVO> lstVT_PATIENT_VNVO = new BLVT_PATIENT_VN(dbInfo).SearchVN(vl);
                if (lstVT_PATIENT_VNVO.Count > 0)
                {
                    lblVN.Text = lstVT_PATIENT_VNVO[0].VN;
                    lblVN_VisitDate.Text = CultureInfo.GetDatetime(lstVT_PATIENT_VNVO[0].VisitDate.Value, YearType.Thai).ToString("dd MMM yyyy");
                    divVN.Visible = true;
                    divANVN.Visible = true;
                }
                else
                {
                    lblVN.Text = string.Empty;
                    divVN.Visible = false;
                    divANVN.Visible = false;
                }
            }
            catch { }
        }

        private void loadPrevOR(ORHEADERVO hd)
        {
            List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchPrevOR(hd);
            if (lsth.Count > 0)
            {
                POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                POSTOROPERATIONVO.ORID = lsth[0].ORID;
                loadImplantPrev(POSTOROPERATIONVO.ORID);
                string r = string.Empty;
                string l = string.Empty;
                string b = string.Empty;
                string None = string.Empty;
                string NA = string.Empty;
                string ORDate = string.Empty;
                List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
                foreach (POSTOROPERATIONVO op1 in lstPOSTOROPERATIONVO)
                {
                    if (op1.Side == (int)EnumOR.ORSide.RE)
                    {
                        if (op1.SubMark == "1")
                            r += " +" + op1.SubName;
                        else if (op1.SubMark == "2")
                            r += " +-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            r += " /" + op1.SubName;
                        else
                        {
                            if (r == "")
                                r += op1.SubName;
                            else
                                r += "," + op1.SubName;
                        }
                    }
                    else if (op1.Side == (int)EnumOR.ORSide.LE)
                    {
                        if (op1.SubMark == "1")
                            l += " +" + op1.SubName;
                        else if (op1.SubMark == "2")
                            l += " +-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            l += " /" + op1.SubName;
                        else
                        {
                            if (l == "")
                                l += op1.SubName;
                            else
                                l += "," + op1.SubName;
                        }
                    }
                    else if (op1.Side == (int)EnumOR.ORSide.BE)
                    {
                        if (op1.SubMark == "1")
                            b += " +" + op1.SubName;
                        else if (op1.SubMark == "2")
                            b += " +-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            b += " /" + op1.SubName;
                        else
                        {
                            if (b == "")
                                b += op1.SubName;
                            else
                                b += "," + op1.SubName;
                        }
                    }
                    else if (op1.Side == (int)EnumOR.ORSide.None)
                    {
                        if (op1.SubMark == "1")
                            None += " +" + op1.SubName;
                        else if (op1.SubMark == "2")
                            None += " +-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            None += " /" + op1.SubName;
                        else if (op1.SubMark == "4")
                            None += " /" + op1.SubName;
                        else if (op1.SubMark == "5")
                            None += " /" + op1.SubName;
                        else
                        {
                            if (None == "")
                                None += op1.SubName;
                            else
                                None += "," + op1.SubName;
                        }
                    }
                    else if (op1.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                    {
                        if (op1.SubMark == "1")
                            NA += " +" + op1.SubName;
                        else if (op1.SubMark == "2")
                            NA += " +-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            NA += " /" + op1.SubName;
                        else if (op1.SubMark == "4")
                            NA += " /" + op1.SubName;
                        else if (op1.SubMark == "5")
                            NA += " /" + op1.SubName;
                        else
                        {
                            if (NA == "")
                                NA += op1.SubName;
                            else
                                NA += "," + op1.SubName;
                        }
                    }
                    ORDate = op1.strORDate + "&nbsp;&nbsp;";
                }

                if (!string.IsNullOrEmpty(r))
                {
                    r = r + " : <code>" + EnumOR.ORSide.RE.ToString() + "</code>&nbsp;";
                }
                if (!string.IsNullOrEmpty(l))
                {
                    string _br = string.Empty;
                    //if (!string.IsNullOrEmpty(r))
                    //    _br = "<br/>";
                    l = _br + l + " : <code>" + EnumOR.ORSide.LE.ToString() + "</code>&nbsp;";
                }
                if (!string.IsNullOrEmpty(b))
                {
                    string _br = string.Empty;
                    //if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                    //    _br = "<br/>";
                    b = _br + b + " : <code>" + EnumOR.ORSide.BE.ToString() + "</code>&nbsp;";
                }
                if (!string.IsNullOrEmpty(None))
                {
                    string _br = string.Empty;
                    //if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                    //    _br = "<br/>";
                    None = _br + None + " : <code>" + EnumOR.ORSide.None.ToString() + "</code>&nbsp;";
                }
                if (!string.IsNullOrEmpty(NA))
                {
                    string _br = string.Empty;
                    //if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                    //    _br = "<br/>";
                    NA = _br + NA + " : <code>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</code>&nbsp;";
                }

                lblPrevOR.Text = ORDate + r + l + b + None + NA;
            }
            if (lblPrevOR.Text == string.Empty)
            {
                lblPrevOR.Text = "ไม่มี";
            }
        }

        private void loadOROperation(ORHEADERVO hd)
        {
            OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
            OROPERATIONVO.ORID = hd.ORID;

            string r = string.Empty;
            string l = string.Empty;
            string b = string.Empty;
            string None = string.Empty;
            string NA = string.Empty;
            Site.Visible = true;
            List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByKey(OROPERATIONVO);
            foreach (OROPERATIONVO op1 in lstOROPERATIONVO)
            {
                string doctor = string.Empty;
                if (op1.Side == (int)EnumOR.ORSide.RE)
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        r += "/";
                    }
                    r += getSubNameOROPERATIONVO(op1);
                    //if (!string.IsNullOrEmpty(r))
                    //{

                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        r += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.LE)
                {
                    if (!string.IsNullOrEmpty(l))
                    {
                        l += "/";
                    }
                    l += getSubNameOROPERATIONVO(op1);
                    //if (!string.IsNullOrEmpty(l))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        l += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.BE)
                {
                    if (!string.IsNullOrEmpty(b))
                    {
                        b += "/";
                    }
                    b += getSubNameOROPERATIONVO(op1);
                    //if (!string.IsNullOrEmpty(b))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        b += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.None)
                {
                    if (!string.IsNullOrEmpty(None))
                    {
                        None += "/";
                    }
                    None += getSubNameOROPERATIONVO(op1);
                    //if (!string.IsNullOrEmpty(None))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        None += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                {
                    if (!string.IsNullOrEmpty(NA))
                    {
                        NA += "/";
                    }
                    NA += getSubNameOROPERATIONVO(op1);
                    //if (!string.IsNullOrEmpty(NA))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        NA += "(" + doctor + ")";
                    //    }
                    //}
                }
            }

            if (!string.IsNullOrEmpty(r))
            {
                r = "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.RE.ToString() + "</strong></code>&nbsp;&nbsp;&nbsp;:&nbsp;" + r;
            }
            if (!string.IsNullOrEmpty(l))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(r))
                    _br = "<br/>";
                l = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.LE.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + l;
            }
            if (!string.IsNullOrEmpty(b))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                    _br = "<br/>";
                b = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.BE.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + b;
            }
            if (!string.IsNullOrEmpty(None))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                    _br = "<br/>";
                None = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.None.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + None;
            }
            if (!string.IsNullOrEmpty(NA))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                    _br = "<br/>";
                NA = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + NA;
            }

            lblSite.Text = r + l + b + None + NA;
            if (lblSite.Text != string.Empty)
            {
                divSite.Visible = true;
            }
            else
            { divSite.Visible = false; }
        }

        private void loadPostOROperation(ORHEADERVO hd)
        {
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = hd.ORID;

            string r = string.Empty;
            string l = string.Empty;
            string b = string.Empty;
            string None = string.Empty;
            string NA = string.Empty;
            Site.Visible = true;
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
            foreach (POSTOROPERATIONVO op1 in lstPOSTOROPERATIONVO)
            {
                string doctor = string.Empty;
                if (op1.Side == (int)EnumOR.ORSide.RE)
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        r += "/";
                    }
                    r += getSubName(op1);
                    //if (!string.IsNullOrEmpty(r))
                    //{

                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        r += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.LE)
                {
                    if (!string.IsNullOrEmpty(l))
                    {
                        l += "/";
                    }
                    l += getSubName(op1);
                    //if (!string.IsNullOrEmpty(l))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        l += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.BE)
                {
                    if (!string.IsNullOrEmpty(b))
                    {
                        b += "/";
                    }
                    b += getSubName(op1);
                    //if (!string.IsNullOrEmpty(b))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        b += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.None)
                {
                    if (!string.IsNullOrEmpty(None))
                    {
                        None += "/";
                    }
                    None += getSubName(op1);
                    //if (!string.IsNullOrEmpty(None))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        None += "(" + doctor + ")";
                    //    }
                    //}
                }
                else if (op1.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                {
                    if (!string.IsNullOrEmpty(NA))
                    {
                        NA += "/";
                    }
                    NA += getSubName(op1);
                    //if (!string.IsNullOrEmpty(NA))
                    //{
                    //    doctor = getDoctor(op1, hd);
                    //    if (!string.IsNullOrEmpty(doctor))
                    //    {
                    //        NA += "(" + doctor + ")";
                    //    }
                    //}
                }
            }

            if (!string.IsNullOrEmpty(r))
            {
                r = "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.RE.ToString() + "</strong></code>&nbsp;&nbsp;&nbsp;:&nbsp;" + r;
            }
            if (!string.IsNullOrEmpty(l))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(r))
                    _br = "<br/>";
                l = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.LE.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + l;
            }
            if (!string.IsNullOrEmpty(b))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                    _br = "<br/>";
                b = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.BE.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + b;
            }
            if (!string.IsNullOrEmpty(None))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                    _br = "<br/>";
                None = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.None.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + None;
            }
            if (!string.IsNullOrEmpty(NA))
            {
                string _br = string.Empty;
                if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                    _br = "<br/>";
                NA = _br + "&nbsp;&nbsp;<code><strong>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</strong></code>&nbsp;&nbsp; : &nbsp;" + NA;
            }

            lblSite.Text = r + l + b + None + NA;
            if (lblSite.Text != string.Empty)
            {
                divSite.Visible = true;
            }
            else
            { divSite.Visible = false; }
        }

        private void loadHeadWarningMore(string ORID)
        {
            POSTORWARNINGVO _vlPOSTORWARNINGVO = new POSTORWARNINGVO();
            _vlPOSTORWARNINGVO.ORID = ORID;
            List<POSTORWARNINGVO> lstPOSTORWARNINGVO = new BLPOSTORWARNING(dbInfo).SearchByKey(_vlPOSTORWARNINGVO);
            string warning = string.Empty;
            if (lstPOSTORWARNINGVO.Count > 0)
            {
                lblHeadWarning.Text = "Warning : " + lstPOSTORWARNINGVO[0].Warning + "";
            }
            else
            {
                lblHeadWarning.Visible = false;
                //lblHeadWarning.Text = "Warning : <strong>ไม่มี</strong>";
            }


            if (lstPOSTORWARNINGVO.Count > 1)
            {
                btnHeadWarningMore.Visible = true;
                gvHeadWarningMore.DataSource = lstPOSTORWARNINGVO;
                gvHeadWarningMore.DataBind();
            }
            else
            {
                btnHeadWarningMore.Visible = false;
            }
        }

        private string getSubNameOROPERATIONVO(OROPERATIONVO op1)
        {
            string SubName = string.Empty;
            if (op1.SubMark == "1")
                SubName += " +" + op1.SubName;
            else if (op1.SubMark == "2")
                SubName += " +-" + op1.SubName;
            else if (op1.SubMark == "3")
                SubName += " /" + op1.SubName;
            else if (op1.SubMark == "4")
                SubName += " /" + op1.SubName;
            else if (op1.SubMark == "5")
                SubName += " /" + op1.SubName;
            else
            {
                if (SubName == "")
                    SubName += op1.SubName;
                else
                    SubName += "," + op1.SubName;
            }
            return SubName;

        }

        private string getSubName(POSTOROPERATIONVO op1)
        {
            string SubName = string.Empty;
            if (op1.SubMark == "1")
                SubName += " +" + op1.SubName;
            else if (op1.SubMark == "2")
                SubName += " +-" + op1.SubName;
            else if (op1.SubMark == "3")
                SubName += " /" + op1.SubName;
            else if (op1.SubMark == "4")
                SubName += " /" + op1.SubName;
            else if (op1.SubMark == "5")
                SubName += " /" + op1.SubName;
            else
            {
                if (SubName == "")
                    SubName += op1.SubName;
                else
                    SubName += "," + op1.SubName;
            }
            return SubName;

        }

        private string getDoctor(POSTOROPERATIONVO op1, ORHEADERVO orhd)
        {
            string doctor = string.Empty;
            if (!string.IsNullOrEmpty(op1.Surgeon1))
            {
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(op1.Surgeon1)).DoctorName;
                //
            }
            if (!string.IsNullOrEmpty(op1.Surgeon2))
            {
                if (!string.IsNullOrEmpty(doctor))
                {
                    doctor += ",";
                }
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(op1.Surgeon2)).DoctorName;
            }
            if (!string.IsNullOrEmpty(op1.Surgeon3))
            {
                if (!string.IsNullOrEmpty(doctor))
                {
                    doctor += ",";
                }
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(op1.Surgeon3)).DoctorName;
            }

            //if (doctor == string.Empty)
            //{
            //    doctor = getDoctorByHeader(orhd);
            //}
            return doctor;
        }

        private string getDoctorByHeader(ORHEADERVO orhd)
        {
            string doctor = string.Empty;
            if (!string.IsNullOrEmpty(orhd.Surgeon1))
            {
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(orhd.Surgeon1)).DoctorName;
                //
            }
            if (!string.IsNullOrEmpty(orhd.Surgeon2))
            {
                if (!string.IsNullOrEmpty(doctor))
                {
                    doctor += ",";
                }
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(orhd.Surgeon2)).DoctorName;
            }
            if (!string.IsNullOrEmpty(orhd.Surgeon3))
            {
                if (!string.IsNullOrEmpty(doctor))
                {
                    doctor += ",";
                }
                doctor += (new BLDOCTORMASTER(dbInfo).SearchByCode(orhd.Surgeon3)).DoctorName;
            }
            return doctor;
        }

        private void loadOnmed(ORHEADERVO hd)
        {
            if (hd.Onmed != false)
            {
                chOnmedTure.Visible = hd.Onmed;
                chOnmedFalse.Visible = !hd.Onmed;
                lblCheckOnmed.Visible = hd.Onmed;
                if(!string.IsNullOrEmpty(hd.OnmedNote))
                lblOnmed.Text = " : " + hd.OnmedNote;
            }
            else
            {
                chOnmedTure.Visible = false;
                chOnmedFalse.Visible = false;
                lblCheckOnmed.Visible = false;
                lblOnmed.Visible = false;
            }
        }

        public void loadORPatient(string hn)
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

                    lblHN.Text = ORPATIENTVO.HN;
                    //lblPatientName.Text = ORPATIENTVO.PatientName;
                    //lblPatientName.Text = ORPATIENTVO.;
                    lblAge.Text = string.Empty;
                    try
                    {
                        double b = double.Parse(ORUtils.getAge(ORPATIENTVO.BirthDateTime));
                        if (b >= 1)
                        {
                            lblAge.Text = b.ToString() + " ปี" + " (" + ORPATIENTVO.BirthDateTime.Value.ToString("dd-MM-yyyy") + ")";
                        }
                        else if( b < 1)
                        {
                            lblAge.Text = (b*10).ToString("#") + " เดือน" + " (" + ORPATIENTVO.BirthDateTime.Value.ToString("dd-MM-yyyy") + ")";
                        }
                    }
                    catch { }
                    //if (ORPATIENTVO.Nationality != "ไทย")
                    //{
                    //lblAge.Text = lblAge.Text + "&nbsp;&nbsp;สัญชาติ&nbsp;:&nbsp;"+ ORPATIENTVO.Nationality;
                    //}

                    lblNationality.Text = ORPATIENTVO.Nationality;

                    PictureFileName = ORPATIENTVO.PictureFileName;

                    PATIENTALLEGICVO _vl = new PATIENTALLEGICVO();
                    _vl.HN = ORPATIENTVO.HN;
                    List<PATIENTALLEGICVO> lstPATIENTALLEGICVO = new BLPATIENTALLEGIC(dbInfo).SearchByKey(_vl);
                    string allegicname = string.Empty;
                    string Reaction = string.Empty;

                    if (lstPATIENTALLEGICVO.Count > 0)
                    {
                        foreach (PATIENTALLEGICVO pt in lstPATIENTALLEGICVO)
                        {
                            if (!string.IsNullOrEmpty(lblPatientallegic.Text))
                            {
                                lblPatientallegic.Text += "<br/>";
                            }
                            lblPatientallegic.Text += "<strong>" + pt.allegicname + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + pt.Reaction + " " + pt.Remark + "</strong>";
                        }
                    }
                    else
                    {
                        lblPatientallegic.Text = "<strong>ไม่มี</strong>";
                    }
                    
                    lblPatientallegicOther.Text = string.Empty;
                    lblPatientallegicOther.Visible = false;
                    AllergicOtherLabel.Visible = false;

                    List<PATIENTALLEGICVO> lstPATIENTALLEGICOTHERVO = new BLPATIENTALLEGIC(dbInfo).SearchOtherByKey(_vl);
                    string strmomo = string.Empty;

                    if (lstPATIENTALLEGICOTHERVO.Count > 0)
                    {
                        foreach (PATIENTALLEGICVO pt in lstPATIENTALLEGICOTHERVO)
                        {
                            if (!string.IsNullOrEmpty(lblPatientallegicOther.Text))
                            {
                                lblPatientallegicOther.Text += "<br/>";
                            }
                            AllergicOtherLabel.Visible = true;
                            lblPatientallegicOther.Visible = true;
                            lblPatientallegicOther.Text += "<strong>" + pt.Memo + "</strong>";
                        }
                    }
                    else
                    {
                        //AllergicOtherLabel.Visible = false;
                        //lblPatientallegicOther.Visible = false;
                        lblPatientallegicOther.Text = string.Empty;
                    }

                    PATIENTDIAGVO _vlDiagVO = new PATIENTDIAGVO();
                    _vlDiagVO.HN = ORPATIENTVO.HN;
                    List<PATIENTDIAGVO> lstPATIENTDIAGVO = new BLPATIENTDIAG(dbInfo).SearchByKey(_vlDiagVO);
                    string diagname = string.Empty;

                    if (lstPATIENTDIAGVO.Count > 0)
                    {
                        foreach (PATIENTDIAGVO pt in lstPATIENTDIAGVO)
                        {
                            if (!string.IsNullOrEmpty(lblPatientalDiag.Text))
                            {
                                lblPatientalDiag.Text += "<br/>";
                            }
                            lblPatientalDiag.Text = lblPatientalDiag.Text + "<strong>" + pt.diagname + "</strong>";
                        }

                    }
                    else
                    {
                        //lblPatientalDiag.Text = "<strong>ไม่มี</strong>";
                        //lblPatientalDiag.Text = "<strong></strong>";
                        lblPatientalDiag.Text = string.Empty;
                        //chOnmedTure.Visible = false;
                        //chOnmedFalse.Visible = false;
                        //lblOnmed.Visible = false;
                        //lblCheckOnmed.Visible = false;
                    }

                }
            }
            catch { }
        }

        private void loadImplant(string orid)
        {
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = orid;
            List<POSTORIMPLANTVO> templstOSTORIMPLANTVO = new List<POSTORIMPLANTVO>();
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
            foreach (POSTOROPERATIONVO op1 in lstPOSTOROPERATIONVO)
            {
                POSTORIMPLANTVO pOSTORIMPLANTVO = new POSTORIMPLANTVO();
                pOSTORIMPLANTVO.PostOperation_ID = op1.ID;
                List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(pOSTORIMPLANTVO);
                foreach (POSTORIMPLANTVO p1 in lstPOSTORIMPLANTVO)
                {
                    templstOSTORIMPLANTVO.Add(p1);
                }
            }
            if (templstOSTORIMPLANTVO.Count == 0)
            {
                divImplant.Visible = false;
            }
            else
            {
                divImplant.Visible = true;
                gvImplant.DataSource = templstOSTORIMPLANTVO;
                gvImplant.DataBind();
            }
        }

        private void loadImplantPrev(string orid)
        {
            lblPrevORImplant.Text = string.Empty;
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = orid;
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
            foreach (POSTOROPERATIONVO op1 in lstPOSTOROPERATIONVO)
            {
                POSTORIMPLANTVO pOSTORIMPLANTVO = new POSTORIMPLANTVO();
                pOSTORIMPLANTVO.PostOperation_ID = op1.ID;
                List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(pOSTORIMPLANTVO);
                foreach (POSTORIMPLANTVO p1 in lstPOSTORIMPLANTVO)
                {
                    if (!string.IsNullOrEmpty(lblPrevORImplant.Text))
                    {
                        lblPrevORImplant.Text += ", ";
                    }
                    lblPrevORImplant.Text += p1.Name;
                    if (!string.IsNullOrEmpty(p1.Remark))
                    { lblPrevORImplant.Text += "+" + p1.Remark; }
                }
            }
            //if (templstOSTORIMPLANTVO.Count == 0)
            //{
            //    divImplant.Visible = false;
            //}
            //else
            //{
            //    divImplant.Visible = true;
            //    gvImplant.DataSource = templstOSTORIMPLANTVO;
            //    gvImplant.DataBind();
            //}
        }

        public void loadHNUnderly(string HN)
        {
            try
            {
                lblPatientalDiagDesc.Text = string.Empty;
                HNUnderlyVO HNUnderlyVO = new HNUnderlyVO();
                HNUnderlyVO.HN = HN;
                List<HNUnderlyVO> lstHNUnderlyVO = new BLHNUnderly(dbInfo).SearchByHN(HNUnderlyVO);
                if (lstHNUnderlyVO.Count > 0)
                {
                    lblPatientalDiagDesc.Text = lstHNUnderlyVO[0].Underlyingtext;
                }
                else
                {
                    lblPatientalDiagDesc.Text = string.Empty;
                }
            }
            catch { }
        }

        private void ClearValue()
        {
            lblORDate.Text = string.Empty;
            lblDoctor.Text = string.Empty;
            lblHN.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblPatientalDiag.Text = string.Empty;
            lblPatientallegic.Text = string.Empty;
            lblPrevOR.Text = string.Empty;
            lblPrevORImplant.Text = string.Empty;
        }

        private void HideValue(bool visible)
        {
            Site.Visible = visible;
        }

        protected void gvImplant_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                loadUsed(e.Row);
            }
        }

        protected void gvImplant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvImplant.Rows[rowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                POSTORIMPLANTVO.Used = !bool.Parse(((HiddenField)row.FindControl("hdUsed")).Value);
                ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).UpdateUsed(POSTORIMPLANTVO);
                if (rv.Value)
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    ORHEADERVO.ORID = hdORID.Value;
                    loadImplant(ORHEADERVO.ORID);
                }
            }
            else if (e.CommandName == "img")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvImplant.Rows[rowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                hdPOSTORIMPLANT_ID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                if (!string.IsNullOrEmpty(POSTORIMPLANTVO.ID))
                {
                    List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
                    if (lstPOSTORIMPLANTVO.Count > 0)
                    {
                        img1.ImageUrl = "";
                        img2.ImageUrl = "";
                        img3.ImageUrl = "";
                        img4.ImageUrl = "";
                        img5.ImageUrl = "";

                        if (lstPOSTORIMPLANTVO[0].Img1 != null && lstPOSTORIMPLANTVO[0].Img1.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img1);
                            img1.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img2 != null && lstPOSTORIMPLANTVO[0].Img2.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img2);
                            img2.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img3 != null && lstPOSTORIMPLANTVO[0].Img3.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img3);
                            img3.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img4 != null && lstPOSTORIMPLANTVO[0].Img4.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img4);
                            img4.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img5 != null && lstPOSTORIMPLANTVO[0].Img5.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img5);
                            img5.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalImplantImage();", true);
            }
        }

        private void loadUsed(GridViewRow r)
        {
            //if (r.RowType == DataControlRowType.DataRow)
            //{
            bool Used = bool.Parse(((HiddenField)r.FindControl("hdUsed")).Value);
            if (Used)
            {
                ((Button)r.FindControl("btnUsed")).Text = "Used";
                ((Button)r.FindControl("btnUsed")).CssClass = "btn btn-success btn-sm mousecursor";
                ((Button)r.FindControl("btnImg")).Visible = true;
                r.BackColor = System.Drawing.Color.Silver;
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)r.FindControl("hdgvID")).Value;
                List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
                if (lstPOSTORIMPLANTVO.Count > 0)
                {
                    bool haveimg = false;
                    if (lstPOSTORIMPLANTVO[0].Img1 != null && lstPOSTORIMPLANTVO[0].Img1.Length > 100)
                    {
                        haveimg = true;

                    }
                    if (lstPOSTORIMPLANTVO[0].Img2 != null && lstPOSTORIMPLANTVO[0].Img2.Length > 100)
                    {
                        haveimg = true;
                    }
                    if (lstPOSTORIMPLANTVO[0].Img3 != null && lstPOSTORIMPLANTVO[0].Img3.Length > 100)
                    {
                        haveimg = true;
                    }
                    if (lstPOSTORIMPLANTVO[0].Img4 != null && lstPOSTORIMPLANTVO[0].Img4.Length > 100)
                    {
                        haveimg = true;
                    }
                    if (lstPOSTORIMPLANTVO[0].Img5 != null && lstPOSTORIMPLANTVO[0].Img5.Length > 100)
                    {
                        haveimg = true;
                    }
                    if (haveimg)
                    {
                        r.BackColor = System.Drawing.Color.Yellow;
                    }
                }
            }
            else
            {
                ((Button)r.FindControl("btnUsed")).Text = "Use";
                ((Button)r.FindControl("btnUsed")).CssClass = "btn btn-secondary btn-sm mousecursor";
                ((Button)r.FindControl("btnImg")).Visible = false;
                r.BackColor = System.Drawing.Color.White;
            }
            //}
        }

        protected void gvImplant_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PostOR/PrintSumary/?d=" + hdORID.Value, false);
        }

        protected void lnkbtnPrintAdc_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PostOR/PrintSumaryadc/?d=" + hdORID.Value, false);
        }

        protected void btnSaveImageImplant_Click(object sender, EventArgs e)
        {
            POSTORIMPLANTVO por = new POSTORIMPLANTVO();
            por.ID = hdPOSTORIMPLANT_ID.Value;
            if (!string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
            {
                System.Drawing.Image imag1 = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                por.Img1 = ConvertImageToByteArray(imag1, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload2.PostedFile.FileName))
            {
                System.Drawing.Image imag2 = System.Drawing.Image.FromStream(FileUpload2.PostedFile.InputStream);
                por.Img2 = ConvertImageToByteArray(imag2, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload3.PostedFile.FileName))
            {
                System.Drawing.Image imag3 = System.Drawing.Image.FromStream(FileUpload3.PostedFile.InputStream);
                por.Img3 = ConvertImageToByteArray(imag3, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload4.PostedFile.FileName))
            {
                System.Drawing.Image imag4 = System.Drawing.Image.FromStream(FileUpload4.PostedFile.InputStream);
                por.Img4 = ConvertImageToByteArray(imag4, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload5.PostedFile.FileName))
            {
                System.Drawing.Image imag5 = System.Drawing.Image.FromStream(FileUpload5.PostedFile.InputStream);
                por.Img5 = ConvertImageToByteArray(imag5, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            por.delimg1 = chimg1.Checked;
            por.delimg2 = chimg2.Checked;
            por.delimg3 = chimg3.Checked;
            por.delimg4 = chimg4.Checked;
            por.delimg5 = chimg5.Checked;

            ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).UpdateImage(por);
            if (rv.Value)
            {
                loadImplant(hdORID.Value);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalImplantImage();", true);

                chimg1.Checked = false;
                chimg2.Checked = false;
                chimg3.Checked = false;
                chimg4.Checked = false;
                chimg5.Checked = false;
            }
        }

        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        protected void ADC_Click(object sender, EventArgs e)
        {
            hdSource = "ADC";
            ClearValue();
            HideValue(false);
            loadvalue(hdORID.Value);
        }
        protected void PostOR_Click(object sender, EventArgs e)
        {
            hdSource = "";
            ClearValue();
            HideValue(false);
            loadvalue(hdORID.Value);
        }

    }
}