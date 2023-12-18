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
    public partial class StickerAdmit : System.Web.UI.Page
    {
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
                        string _day = DateFormat.getDayTH(lstorh[0].ORDate.Value.DayOfWeek.ToString());
                        string _month = DateFormat.getMonthTH(lstorh[0].ORDate.Value.Month);
                        lblORDate.Text = "วัน " + _day + " ที่ " + lstorh[0].ORDate.Value.Day + " เดือน " + _month + " พ.ศ." + lstorh[0].ORDate.Value.ToString("yyyy");
                        lblRemark.Text = lstorh[0].Remark;
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
                    r = "<code>" + EnumOR.ORSide.RE.ToString() + "</code> : " + r;
                }
                if (!string.IsNullOrEmpty(l))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    l = _br + "<code>" + EnumOR.ORSide.LE.ToString() + "</code> : " + l;
                }
                if (!string.IsNullOrEmpty(b))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    b = _br + "<code>" + EnumOR.ORSide.BE.ToString() + "</code> : " + b;
                }
                OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                OROPERATIONVO.strSide = r + l + b;
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
    }
}