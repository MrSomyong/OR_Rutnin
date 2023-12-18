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
    public partial class ORHeader : System.Web.UI.Page
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
                    string _date = Request.QueryString["d"];
                    DateTime ORDate = DateTime.Parse(Utilities.ConvertYMD(_date));
                    //DateTime ORDate = DateTime.Parse(_date);
                    //
                    lblORHearder.Text = "ใบรายชื่อผู้ป่วย Admit";
                    lblRoomType.Text = "จำนวนห้องพัก";
                    string _day = DateFormat.getDayTH(ORDate.DayOfWeek.ToString());
                    string _month = DateFormat.getMonthTH(ORDate.Month);
                    lblORDate.Text = "วัน " + _day + " ที่ " + ORDate.Day + " เดือน " + _month + " พ.ศ." + ORDate.ToString("yyyy");
                    gvORHeader.DataSource = loadORHeader(ORDate);
                    gvORHeader.DataBind();

                    gvRoomType.DataSource = loadORRoomType(ORDate);
                    gvRoomType.DataBind();
                }
            }
            catch (Exception ex)
            { throw ex; }
        }

        private List<ORHEADERVO> loadORHeader(DateTime ORDate)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByIPD(ORDate);
            foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
            {
                if (ORHEADERVO.ORPATIENTVO.Sex == "หญิง")
                    ORHEADERVO.ORPATIENTVO.Sex = "F";
                else if (ORHEADERVO.ORPATIENTVO.Sex == "ชาย")
                    ORHEADERVO.ORPATIENTVO.Sex = "M";

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
                if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                {
                    //--------------------------//
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
                        r = r + ":" + EnumOR.ORSide.RE.ToString();
                    }
                    if (!string.IsNullOrEmpty(l))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        l = _br + l + ":" + EnumOR.ORSide.LE.ToString();
                    }
                    if (!string.IsNullOrEmpty(b))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        b = _br + b + ":" + EnumOR.ORSide.BE.ToString();
                    }
                    if (!string.IsNullOrEmpty(None))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                            _br = "<br/>";
                        None = _br + None + ":" + EnumOR.ORSide.None.ToString();
                    }
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                            _br = "<br/>";
                        NA = _br + NA + ":" + EnumOR.ORSide.ยังไม่ระบุตา.ToString();
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
                    if (!string.IsNullOrEmpty(ORHEADERVO.RoomType))
                    {
                        ROOMTYPEVO roomvo = new BLROOMTYPE(dbInfo).SearchByCode(ORHEADERVO.RoomType);
                        ORHEADERVO.RoomType = roomvo.NAME;
                    }
                    lstORHEADERVO.Add(ORHEADERVO);
                }
                    //} //https://github.com/MrSomyong/OR/issues/1

                }
                return lstORHEADERVO;
        }

        private List<ORHEADERVO> loadORRoomType(DateTime ORDate)
        {
            List<ORHEADERVO> lstORHEADERVO = new BLORHEADER(dbInfo).SearchRoomType(ORDate);

            return lstORHEADERVO;
        }

        protected void gvORHeader_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                manageRow(e);
                e.Row.CssClass = "align-text-top";
            }
            //else if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    gvORHeader.Rows[gvORHeader.Rows.Count - 1].CssClass = "blankrow";
            //}
        }

        private string prvgvORHeaderSurgeon = string.Empty;

        private void manageRow(GridViewRowEventArgs e)
        {
            string lblgvSurgeon = (e.Row.FindControl("lblgvSurgeon") as Label).Text;
            if (!string.IsNullOrEmpty(prvgvORHeaderSurgeon) && lblgvSurgeon != prvgvORHeaderSurgeon)
            {
                gvORHeader.Rows[e.Row.RowIndex - 1].CssClass = "blankrow";
            }

            prvgvORHeaderSurgeon = lblgvSurgeon;

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