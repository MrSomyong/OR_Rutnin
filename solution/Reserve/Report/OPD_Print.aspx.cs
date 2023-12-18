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
    public partial class OPD_Print : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            try
            {
                if (Session["gvOPD"] != null)
                {
                    ORHEADERVO ORHEADERVO = new ORHEADERVO();
                    gvOperation.DataSource = Session["gvOPD"];

                    int iSum = 0;
                    foreach (ORHEADERVO lst in (List<ORHEADERVO>)Session["gvOPD"])
                    {
                        iSum += lst.OROPERATIONVO.QTY;
                    }

                    gvOperation.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                    gvOperation.Columns[0].FooterStyle.Font.Bold = true;
                    gvOperation.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                    gvOperation.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                    gvOperation.DataBind();
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
                    r = r + " : " + EnumOR.ORSide.RE.ToString() + "";
                }
                if (!string.IsNullOrEmpty(l))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    l = _br + l + " : " + EnumOR.ORSide.LE.ToString() + "";
                }
                if (!string.IsNullOrEmpty(b))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                        _br = "<br/>";
                    b = _br + b + " : " + EnumOR.ORSide.BE.ToString() + "";
                }
                if (!string.IsNullOrEmpty(None))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                        _br = "<br/>";
                    None = _br + None + " : " + EnumOR.ORSide.None.ToString() + "";
                }
                if (!string.IsNullOrEmpty(NA))
                {
                    string _br = string.Empty;
                    if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                        _br = "<br/>";
                    NA = _br + NA + " : " + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "";
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

        protected void gvOperation_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvOperation.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvOperation.Columns.Count;
                }
            }
        }
    }
}