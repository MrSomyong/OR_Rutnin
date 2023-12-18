using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Print
{
    public partial class StickerORpost : System.Web.UI.Page
    {
        public string strORStatus;
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Request.QueryString["o"] != null)
                {
                    string _orid = Request.QueryString["o"];

                    List<ORHEADERVO> lstorh = loadORHeader(_orid);
                    if (lstorh.Count > 0)
                    {
                        lblHN.Text = lstorh[0].HN;
                        lblPatientName.Text = lstorh[0].PatientName;
                        lblAge.Text = lstorh[0].ORPATIENTVO.Age;
                        lblGender.Text = lstorh[0].ORPATIENTVO.Sex;
                        lblOperation.Text = lstorh[0].OROPERATIONVO.strSide;
                        lblAnesthesiaType.Text = lstorh[0].AnesthesiaType;
                        lblSurgeon.Text = lstorh[0].Surgeon;
                        lblPrediag.Text = lstorh[0].Prediag;
                        string _day = DateFormat.getDayTH(lstorh[0].ORDate.Value.DayOfWeek.ToString());
                        string _month = DateFormat.getMonthTH(lstorh[0].ORDate.Value.Month);
                        lblORDate.Text = lstorh[0].ORDate.Value.ToString("dd-MM-yyyy");
                        //lblORDate.Text = _day + " ที่ " + lstorh[0].ORDate.Value.Day + " " + _month + " " + lstorh[0].ORDate.Value.ToString("yyyy");
                        //lblORDate.Text = "วัน " + _day + " ที่ " + lstorh[0].ORDate.Value.Day + " เดือน " + _month + " พ.ศ." + lstorh[0].ORDate.Value.ToString("yyyy");
                        //lblStatus.Text = lstorh[0].ORStatus;
                        lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString();
                        if (lstorh[0].ORStatus == "2")
                        {
                            lblFM.Text = "FM-ADC-031";
                        }
                        else
                        {
                            lblFM.Text = "FM-ADC-030";
                        }

                        ORHEADERVO xx = new ORHEADERVO();
                        xx.ORID = _orid;

                        List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByKey(xx);
                        foreach (ORHEADERVO hd in lsth)
                        {

                            if (lstorh[0].ORStatus.ToString() == "1" && loadVN(hd))
                            {
                                lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + "  VN  " + strORStatus;
                            }
                            else if (lstorh[0].ORStatus.ToString() == "2" && loadAN(hd))
                            {
                                lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + "  AN  " + strORStatus;
                            }
                            else if (lstorh[0].ORStatus.ToString() == "1" && !loadVN(hd))
                            {
                                lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + " VN .........";
                            }
                            else if (lstorh[0].ORStatus.ToString() == "2" && !loadAN(hd))
                            {
                                lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + " AN .........";
                            }
                            else 
                            {
                                lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + " AN .........";
                            }

                            //if (!loadAN(hd))
                            //{
                            //    loadVN(hd);
                            //}
                        }

                        //List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByKey(xx);
                        //foreach (ORHEADERVO hd in lsth)
                        //{
                        //    if (!loadAN(hd))
                        //    {
                        //        loadVN(hd);
                        //    }
                        //}
                        //if (strORStatus != string.Empty)
                        //{
                        //    if (lstorh[0].ORStatus.ToString() == "1")
                        //    {



                        //        lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + "  VN  " + strORStatus;
                        //    }
                        //    else
                        //    {
                        //        lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + "  AN  " + strORStatus;
                        //    }
                        //}
                        //else
                        //{
                        //    if (lstorh[0].ORStatus.ToString() == "1")
                        //    {
                        //        lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + " VN ...............";
                        //    }
                        //    else
                        //    {
                        //        lblStatus.Text = ((EnumOR.ORStatus)int.Parse(lstorh[0].ORStatus.ToString())).ToString() + " AN ...............";
                        //    }
                        //}
                    }
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private List<ORHEADERVO> loadORHeader(string orid)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByORID(orid);
            foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
            {
                string r = string.Empty;
                string l = string.Empty;
                string b = string.Empty;
                string None = string.Empty;
                string NA = string.Empty;
                List<POSTOROPERATIONVO> lstOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                foreach (POSTOROPERATIONVO op1 in lstOROPERATIONVO)
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
                string Surgeon1 = string.Empty;
                string Surgeon2 = string.Empty;
                string Surgeon3 = string.Empty;

                DOCTORMASTERVO DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon1);
                //Surgeon1 = DOCTORMASTERVO.DoctorName;
                if (DOCTORMASTERVO.CHEQUECLIENTNAME != "")
                {
                    Surgeon1 = DOCTORMASTERVO.CHEQUECLIENTNAME;
                }
                else
                {
                    Surgeon1 = DOCTORMASTERVO.DoctorName;
                }

                DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon2);
                //Surgeon2 = DOCTORMASTERVO.DoctorName;
                if (DOCTORMASTERVO.CHEQUECLIENTNAME != "")
                {
                    Surgeon2 = DOCTORMASTERVO.CHEQUECLIENTNAME;
                }
                else
                {
                    Surgeon2 = DOCTORMASTERVO.DoctorName;
                }

                DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon3);
                //Surgeon3 = DOCTORMASTERVO.DoctorName;
                if (DOCTORMASTERVO.CHEQUECLIENTNAME != "")
                {
                    Surgeon3 = DOCTORMASTERVO.CHEQUECLIENTNAME;
                }
                else
                {
                    Surgeon3 = DOCTORMASTERVO.DoctorName;
                }

                //ORHEADERVO.Surgeon = Surgeon1;
                //if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                //{
                //    ORHEADERVO.Surgeon += "<br/>" + Surgeon2;
                //}
                //if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                //{
                //    ORHEADERVO.Surgeon += "<br/>" + Surgeon3;
                //}

                if (ORHEADERVO.Surgeon3 == ORHEADERVO.SurgeonMaster)
                {
                    ORHEADERVO.Surgeon = Surgeon3;
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon1))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon1;
                    }
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon2;
                    }
                }
                else if (ORHEADERVO.Surgeon2 == ORHEADERVO.SurgeonMaster)
                {
                    ORHEADERVO.Surgeon = Surgeon2;
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon1))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon1;
                    }
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon3;
                    }
                }
                else
                {
                    ORHEADERVO.Surgeon = Surgeon1;
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon2;
                    }
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon3;
                    }
                }

                if (!string.IsNullOrEmpty(r))
                {
                    r = r + " : " + "<b><font size=5px>" + EnumOR.ORSide.RE.ToString() + "</font></b>";
                }
                if (!string.IsNullOrEmpty(l))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    l = _br + l + " : " + "<b><font size=5px>" + EnumOR.ORSide.LE.ToString() + "</font></b>";
                }
                if (!string.IsNullOrEmpty(b))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    b = _br + b + " : " + "<b><font size=5px>" + EnumOR.ORSide.BE.ToString() + "</font></b>";
                }
                if (!string.IsNullOrEmpty(None))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                        _br = "<br/>";
                    None = _br + None + " : " + "<b><font size=5px>" + EnumOR.ORSide.None.ToString() + "</font></b>";
                }
                if (!string.IsNullOrEmpty(NA))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                        _br = "<br/>";
                    NA = _br + NA + " : " + "<b><font size=5px>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</font></b>";
                }
                OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                OROPERATIONVO.strSide = r + l + b + None + NA;
                ORHEADERVO.OROPERATIONVO = OROPERATIONVO;

                ANESTHESIAVO ANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType1);
                ORHEADERVO.AnesthesiaType = ANESTHESIAVO.NAME;
                if (ORHEADERVO.AnesthesiaSign == "1")
                    ORHEADERVO.AnesthesiaType += "+";
                else if (ORHEADERVO.AnesthesiaSign == "2")
                    ORHEADERVO.AnesthesiaType += "+-";
                else
                    ORHEADERVO.AnesthesiaType += " ";

                ANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType2);
                ORHEADERVO.AnesthesiaType += ANESTHESIAVO.NAME;

                ROOMTYPEVO roomvo = new BLROOMTYPE(dbInfo).SearchByCode(ORHEADERVO.RoomType);
                ORHEADERVO.RoomType = roomvo.NAME;
                lstORHEADERVO.Add(ORHEADERVO);
            }

            return lstORHEADERVO;
        }

        public bool loadAN(ORHEADERVO hd)
        {
            bool rv = false;
            try
            {

                VT_PATIENT_ANVO vl = new VT_PATIENT_ANVO();
                vl.HN = hd.HN;
                try
                {
                    vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " 00:00:00");
                }
                catch
                {
                    vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " 00:00:00");
                }

                List<VT_PATIENT_ANVO> lstVT_PATIENT_ANVO = new BLVT_PATIENT_AN(dbInfo).SearchAN(vl);
                if (lstVT_PATIENT_ANVO.Count > 0)
                {
                    strORStatus = lstVT_PATIENT_ANVO[0].AN;
                    rv = true;
                }
                else
                {
                    strORStatus = vl.ORDateTime + " | " + vl.HN ;
                    rv = false;
                }
            }
            catch { }
            return rv;
        }

        public bool loadVN(ORHEADERVO hd)
        {
            bool rv = false;
            try
            {
                VT_PATIENT_VNVO vl = new VT_PATIENT_VNVO();
                vl.HN = hd.HN;
                vl.ORDateTime = hd.ORDate;
                List<VT_PATIENT_VNVO> lstVT_PATIENT_VNVO = new BLVT_PATIENT_VN(dbInfo).SearchVN(vl);
                if (lstVT_PATIENT_VNVO.Count > 0)
                {
                    strORStatus = lstVT_PATIENT_VNVO[0].VN;
                    rv = true;
                }
                else
                {
                    strORStatus = string.Empty;
                    rv = false;
                }
            }
            catch { }
            return rv;
        }

    }
}