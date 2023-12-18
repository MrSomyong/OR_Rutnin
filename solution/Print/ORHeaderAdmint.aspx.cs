using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace solution.Print
{
    public partial class ORHeaderAdmint : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Request.QueryString["d"] != null)
                {
                    string _roomid = Request.QueryString["r"];
                    string _date = Request.QueryString["d"];
                    //DateTime ORDate = DateTime.Parse(_date);
                    DateTime ORDate = DateTime.Parse(Utilities.ConvertYMD(_date));
                    //
                    SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.CODE = _roomid;
                    List<SETUPORROOMVO> _lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                    string ORRoomName = string.Empty;
                    if (_lstSETUPORROOMVO.Count > 0)
                    {
                        ORRoomName = _lstSETUPORROOMVO[0].Name;
                    }
                    lblORHearder.Text = "Schedule  : " + ORRoomName;

                    string _day = DateFormat.getDayTH(ORDate.DayOfWeek.ToString());
                    string _month = DateFormat.getMonthTH(ORDate.Month);
                    lblORDate.Text = "ORDATE : วัน " + _day + " ที่ " + ORDate.Day + " เดือน " + _month + " พ.ศ." + ORDate.ToString("yyyy");

                    ORANESTHESIADOCTORSCHEDULEVO doctoranse = new ORANESTHESIADOCTORSCHEDULEVO();
                    doctoranse.StartAnesthesiaDateTime = ORDate;
                    List<ORANESTHESIADOCTORSCHEDULEVO> LstDoctorAnse = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByKey(doctoranse);
                    int ian = 0;
                    lblDoctorAnes.Text = string.Empty;
                    foreach (ORANESTHESIADOCTORSCHEDULEVO _docan in LstDoctorAnse)
                    {
                        if (ian == 0)
                            lblDoctorAnes.Text += " - ";
                        else
                            lblDoctorAnes.Text += "<br /> - ";
                        lblDoctorAnes.Text += _docan.StartAnesthesiaDateTime.Value.ToShortTimeString() + " " + _docan.DoctorName + " : " + _docan.Reamrk;
                        ian++;
                    }

                    ORANESTHESIANURSESCHEDULEVO nurseanse = new ORANESTHESIANURSESCHEDULEVO();
                    nurseanse.StartAnesthesiaDateTime = ORDate;
                    List<ORANESTHESIANURSESCHEDULEVO> LstNurseAnse = new BLORANESTHESIANURSESCHEDULE(dbInfo).SearchByKey(nurseanse);
                    ian = 0;
                    lblNurseAnes.Text = string.Empty;
                    foreach (ORANESTHESIANURSESCHEDULEVO _nuran in LstNurseAnse)
                    {
                        if (ian == 0)
                            lblNurseAnes.Text += " - ";
                        else
                            lblNurseAnes.Text += "<br /> - ";
                        lblNurseAnes.Text += _nuran.StartAnesthesiaDateTime.Value.ToShortTimeString() + " " + _nuran.Name + " : " + _nuran.Reamrk;
                        ian++;
                    }

                    //List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
                    //List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByRequestbyuser(_roomid, ORDate);
                    lblRequestByusername.Text = string.Empty;
                    //foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
                    //{

                    //    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    //    APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                    //    List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                    //    if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                    //    {
                    //        _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                    //        if (_lstAPPOINTMENTVO.Count == 0)
                    //        {
                    //            APPOINTMENTVO.ConfirmStatusType = 6;
                    //            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        APPOINTMENTVO.ConfirmStatusType = 0;
                    //        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    //    }

                    //    if ( _lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                    //    {
                    //        SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
                    //        _SETUPLOGONVO.UserID = ORHEADERVO.RequestByUser;
                    //        SETUPLOGONVO SETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchLogin(_SETUPLOGONVO);
                    //        if (!string.IsNullOrEmpty(SETUPLOGONVO.UserID))
                    //        {
                    //            lblRequestByusername.Text = lblRequestByusername.Text + SETUPLOGONVO.FirstName + " / ";
                    //        }
                    //    }
                    //}
                    //if (lblRequestByusername.Text != string.Empty)
                    //{
                    //    lblRequestByusername.Text = lblRequestByusername.Text.Substring(0, lblRequestByusername.Text.Length - 2);
                    //}
                    try
                    {
                        gvORHeader.DataSource = loadORHeader(_roomid, ORDate);
                        gvORHeader.DataBind();
                    }
                    catch (Exception ex)
                    { throw ex; }

                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private List<ORHEADERVO> loadORHeader(string Roomid, DateTime ORDate)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByRoom(Roomid, ORDate);
            string[] arr1 = new string[templstORHEADERVO.Count];
            int i = 0;
            foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
            {
                //ORHEADERVO.ORPATIENTVO = new ORPATIENTVO();
                //ORPATIENTVO _ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(ORHEADERVO.HN);
                //ORHEADERVO.ORPATIENTVO.PatientName = _ORPATIENTVO.PatientName;
                //ORHEADERVO.ORPATIENTVO.BirthDateTime = _ORPATIENTVO.BirthDateTime;
                //ORHEADERVO.ORPATIENTVO.Age = ORUtils.getAge(_ORPATIENTVO.BirthDateTime);
                //ORHEADERVO.ORPATIENTVO.Sex = _ORPATIENTVO.Sex;
                //ORHEADERVO.ORPATIENTVO.Ref = _ORPATIENTVO.Ref;
                //ORHEADERVO.ORPATIENTVO.Nationality = _ORPATIENTVO.Nationality;
                //ORHEADERVO.ORPATIENTVO.Initial = _ORPATIENTVO.Initial;

                //ORHEADERVO.PatientName = _ORPATIENTVO.Initial + " " + _ORPATIENTVO.FirstName + " " + _ORPATIENTVO.LastName;

                if (ORHEADERVO.ORPATIENTVO.Sex == "หญิง")
                    ORHEADERVO.ORPATIENTVO.Sex = "F";
                else if (ORHEADERVO.ORPATIENTVO.Sex == "ชาย")
                    ORHEADERVO.ORPATIENTVO.Sex = "M";
                //
                if (ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.Reserve).ToString())
                    ORHEADERVO.strORStatus = "Res";
                if (ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.Observe).ToString())
                    ORHEADERVO.strORStatus = "Obs";

                //--------------------------//
                //https://github.com/MrSomyong/OR/issues/1
                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();

                if (loadAN(ORHEADERVO) == true)
                {
                    APPOINTMENTVO.ConfirmStatusType = 0;
                    _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                }
                else if (loadVN(ORHEADERVO) == true)
                {
                    APPOINTMENTVO.ConfirmStatusType = 0;
                    _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                }
                else
                {

                    if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                    {
                        _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                        if (_lstAPPOINTMENTVO.Count == 0)
                        {
                            APPOINTMENTVO.ConfirmStatusType = 6;
                            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                        }
                    }
                    else
                    {
                        APPOINTMENTVO.ConfirmStatusType = 0;
                        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                    }

                }

                //if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                //{
                //    _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                //    if (_lstAPPOINTMENTVO.Count == 0)
                //    {
                //        APPOINTMENTVO.ConfirmStatusType = 6;
                //        _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                //    }
                //}
                //else
                //{
                //    APPOINTMENTVO.ConfirmStatusType = 0;
                //    _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                //}
                if ((ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.OPD).ToString() || ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.IPD).ToString()
                    || ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.Observe).ToString() || ORHEADERVO.ORStatus == "Obs")
                    && _lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                {
                    //--------------------------//

                    if ((ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.OPD).ToString() || ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.IPD).ToString()
                    || ORHEADERVO.ORStatus == ((int)EnumOR.ORStatus.Observe).ToString() || ORHEADERVO.ORStatus == "Obs"))
                {
                    string r = string.Empty;
                    string l = string.Empty;
                    string b = string.Empty;
                    string None = string.Empty;
                    string NA = string.Empty;
                    List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                    foreach (OROPERATIONVO op1 in lstOROPERATIONVO)
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
                    Surgeon1 = DOCTORMASTERVO.DoctorName;

                    DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon2);
                    Surgeon2 = DOCTORMASTERVO.DoctorName;

                    DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon3);
                    Surgeon3 = DOCTORMASTERVO.DoctorName;

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
                        r = r + ":<strong style=\"color: red; \">" + EnumOR.ORSide.RE.ToString() + "</strong>";
                    }
                    if (!string.IsNullOrEmpty(l))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        l = _br + l + ":<strong style=\"color: red; \">" + EnumOR.ORSide.LE.ToString() + "</strong>";
                    }
                    if (!string.IsNullOrEmpty(b))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        b = _br + b + ":<strong style=\"color: red; \">" + EnumOR.ORSide.BE.ToString() + "</strong>";
                    }
                    if (!string.IsNullOrEmpty(None))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                            _br = "<br/>";
                        None = _br + None + ":<strong style=\"color: red; \">" + EnumOR.ORSide.None.ToString() + "</strong>";
                    }
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                            _br = "<br/>";
                        NA = _br + NA + ":<strong style=\"color: red; \">" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</strong>";
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

                    ORHEADERVO.strORDate = ORHEADERVO.ORDate.Value.ToString("dd-MM-yyyy");
                    ORHEADERVO.strORCase = ORHEADERVO.ORCase.ToString();

                    string strSurgeon = ORHEADERVO.Surgeon;
                    if (!string.IsNullOrEmpty(prvgvORHeaderSurgeon) && strSurgeon != prvgvORHeaderSurgeon)
                    {
                        ORHEADERVO orbank = new ORHEADERVO();
                        orbank.strORCase = "";
                        lstORHEADERVO.Add(orbank);
                    }
                    if (!string.IsNullOrEmpty(strSurgeon) && strSurgeon == prvgvORHeaderSurgeon)
                    {
                        //ORHEADERVO.Surgeon = "\"";
                    }

                    prvgvORHeaderSurgeon = strSurgeon;

                    SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
                    _SETUPLOGONVO.UserID = ORHEADERVO.RequestByUser;
                    SETUPLOGONVO SETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchLogin(_SETUPLOGONVO);
                    if (!string.IsNullOrEmpty(SETUPLOGONVO.UserID))
                    {
                        ORHEADERVO.RequestByUserName = SETUPLOGONVO.FirstName;
                        arr1[i] = SETUPLOGONVO.FirstName;

                    }

                    lstORHEADERVO.Add(ORHEADERVO);

                }
            }
                }

                string[] q = arr1.Distinct().ToArray();

            for (int iX = 0; iX < q.Length; iX++)
            {
                if (!(string.IsNullOrEmpty(q[iX])))
                {
                    lblRequestByusername.Text = lblRequestByusername.Text + q[iX] + " / ";
                }                
            }
            if (lblRequestByusername.Text != string.Empty)
            {
                lblRequestByusername.Text = lblRequestByusername.Text.Substring(0, lblRequestByusername.Text.Length - 2);
            }

            return lstORHEADERVO;
        }

        protected void gvORHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                    e.Row.CssClass = "align-text-top";

                    manageRow(e);
                }
                //else if (e.Row.RowType == DataControlRowType.Footer)
                //{
                //    string surgeon = (gvORHeader.Rows[gvORHeader.Rows.Count - 1].FindControl("lblgvSurgeon") as Label).Text;

                    
                //    string[] lines = System.Text.RegularExpressions.Regex.Split(surgeon, "<br/>");
                //    string classcss = string.Empty;

                //    if (lines.Length == 1)
                //        classcss = "blankrow1";
                //    else if (lines.Length == 2)
                //        classcss = "blankrow2";
                //    else if (lines.Length == 3)
                //        classcss = "blankrow3";
                //    else if (lines.Length == 4)
                //        classcss = "blankrow4";
                //    else if (lines.Length == 5)
                //        classcss = "blankrow5";
                //    else if (lines.Length == 6)
                //        classcss = "blankrow6";

                //    gvORHeader.Rows[gvORHeader.Rows.Count].CssClass = "blankrowft";

                //}
            }
            catch (Exception ex)
            {

            }
        }

        private string prvgvORHeaderSurgeon = string.Empty;

        private void manageRow(GridViewRowEventArgs e)
        {
            string lblgvSurgeon = (e.Row.FindControl("lblgvSurgeon") as Label).Text;
                    
            if (!string.IsNullOrEmpty(prvgvORHeaderSurgeon) && lblgvSurgeon != prvgvORHeaderSurgeon)
            {
                //(e.Row.FindControl("lblgvORCase") as Label).Text = "";
            }
            //if (!string.IsNullOrEmpty(lblgvSurgeon) && lblgvSurgeon == prvgvORHeaderSurgeon)
            //{
            //    //(e.Row.FindControl("lblgvSurgeon") as Label).Text = "\"";
            //}
            prvgvORHeaderSurgeon = lblgvSurgeon;
            string iCancelDate = (e.Row.FindControl("hdgvCancelDate") as HiddenField).Value;

            if (!string.IsNullOrEmpty(iCancelDate))
            {
                e.Row.Font.Strikeout = true;
            }
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
                    vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " " + hd.ORTime);
                }
                catch
                {
                    vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " 23:59:59");
                }

                List<VT_PATIENT_ANVO> lstVT_PATIENT_ANVO = new BLVT_PATIENT_AN(dbInfo).SearchAN(vl);
                if (lstVT_PATIENT_ANVO.Count > 0)
                {
                    rv = true;
                }
                else
                {
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
                    rv = true;
                }
                else
                {
                    rv = false;
                }
            }
            catch { }
            return rv;
        }

    }
}