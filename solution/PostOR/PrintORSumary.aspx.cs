using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.PostOR
{
    public partial class ORSumary : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
        public string PictureFileName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["d"] != null)
                {
                    string _ORID = Request.QueryString["d"];
                    loadvalue(_ORID);
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        public void loadvalue(string orid)
        {
            List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByORID(orid);
            foreach (ORHEADERVO hd in lsth)
            {
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
                loadPostOROperation(hd);
                loadImplant(hd);
            }
        }

        private void loadImplant(ORHEADERVO orhd)
        {
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = orhd.ORID;
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
            gvImplant.DataSource = templstOSTORIMPLANTVO;
            gvImplant.DataBind();
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
                        imgPatient.ImageUrl = "../Images/17241-200.png";
                    }

                    lblHN.Text = ORPATIENTVO.HN;
                    lblPatientName.Text = ORPATIENTVO.PatientName;
                    lblAge.Text = ORUtils.getAge(ORPATIENTVO.BirthDateTime) + " (" + ORPATIENTVO.BirthDateTime.Value.ToString("dd/MM/yyyy") + ")";
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
                            lblPatientallegic.Text += "<strong>" + pt.allegicname + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<strong>" + pt.Reaction + "</strong>";
                        }
                    }
                    else
                    {
                        lblPatientallegic.Text = "<strong>ไม่มี</strong>";
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
                            lblPatientalDiag.Text = "<strong>" + pt.diagname + "</strong>";
                        }

                    }
                    else
                    {
                        lblPatientalDiag.Text = "<strong>ไม่มี</strong>";
                    }

                }
            }
            catch { }
        }

        private void loadOnmed(ORHEADERVO hd)
        {
            chOnmedTure.Visible = hd.Onmed;
            chOnmedFalse.Visible = !hd.Onmed;
            txtOnmed.Visible = hd.Onmed;
            txtOnmed.Text = hd.OnmedNote;
        }

        private void loadPrevOR(ORHEADERVO hd)
        {
            List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchPrevOR(hd);
            if (lsth.Count > 0)
            {
                POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                POSTOROPERATIONVO.ORID = lsth[0].ORID;

                string r = string.Empty;
                string l = string.Empty;
                string b = string.Empty;
                string None = string.Empty;
                string NA = string.Empty;

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

                lblPrevOR.Text = r + l + b + None + NA;
            }
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
                if (op1.Side == (int)EnumOR.ORSide.RE)
                {
                    if (!string.IsNullOrEmpty(r))
                    {
                        r += "/";
                    }
                    r += getSubName(op1);
                    if (!string.IsNullOrEmpty(r))
                    {

                        string doctor = getDoctor(op1, hd);
                        if (!string.IsNullOrEmpty(doctor))
                        {
                            r += "(" + doctor + ")";
                        }
                    }
                }
                else if (op1.Side == (int)EnumOR.ORSide.LE)
                {
                    if (!string.IsNullOrEmpty(l))
                    {
                        l += "/";
                    }
                    l += getSubName(op1);
                    if (!string.IsNullOrEmpty(l))
                    {
                        string doctor = getDoctor(op1, hd);
                        if (!string.IsNullOrEmpty(doctor))
                        {
                            l += "(" + doctor + ")";
                        }
                    }
                }
                else if (op1.Side == (int)EnumOR.ORSide.BE)
                {
                    if (!string.IsNullOrEmpty(b))
                    {
                        b += "/";
                    }
                    b += getSubName(op1);
                    if (!string.IsNullOrEmpty(b))
                    {
                        string doctor = getDoctor(op1, hd);
                        if (!string.IsNullOrEmpty(doctor))
                        {
                            b += "(" + doctor + ")";
                        }
                    }
                }
                else if (op1.Side == (int)EnumOR.ORSide.None)
                {
                    if (!string.IsNullOrEmpty(None))
                    {
                        None += "/";
                    }
                    None += getSubName(op1);
                    if (!string.IsNullOrEmpty(None))
                    {
                        string doctor = getDoctor(op1, hd);
                        if (!string.IsNullOrEmpty(doctor))
                        {
                            None += "(" + doctor + ")";
                        }
                    }
                }
                else if (op1.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                {
                    if (!string.IsNullOrEmpty(NA))
                    {
                        NA += "/";
                    }
                    NA += getSubName(op1);
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string doctor = getDoctor(op1, hd);
                        if (!string.IsNullOrEmpty(doctor))
                        {
                            NA += "(" + doctor + ")";
                        }
                    }
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

            if (doctor == string.Empty)
            {
                doctor = getDoctorByHeader(orhd);
            }
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
    }
}