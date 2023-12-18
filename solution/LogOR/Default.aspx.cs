using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Text;
using System.Data;

namespace solution.LogOR
{
    public partial class Default : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            if (Session["USERID"] == null)
            {
                Response.Redirect("/Auth/Login");

                Response.End();

            }
            if (!IsPostBack)
            {
                if (Session["ORDate"] != null)
                {
                    hdDate.Value = Session["ORDate"].ToString();
                }
                else
                {
                    hdDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                loadvalue();
            }
        }

        private List<ORHEADERVO> loadORHeader(string CODE)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByRoom(CODE, DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value)));
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

                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                {
                    _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                }
                else
                {
                    APPOINTMENTVO.ConfirmStatusType = 0;
                    _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                }
                if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
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

                    ORHEADERVO.Surgeon = Surgeon1;
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon2;
                    }
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                    {
                        ORHEADERVO.Surgeon += "<br/>" + Surgeon3;
                    }
                    if (!string.IsNullOrEmpty(r))
                    {
                        r = r + " : <code>" + EnumOR.ORSide.RE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(l))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        l = _br + l + " : <code>" + EnumOR.ORSide.LE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(b))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        b = _br + b+ " : <code>" + EnumOR.ORSide.BE.ToString() + "</code>";
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

                    lstORHEADERVO.Add(ORHEADERVO);
                }
            }

            return lstORHEADERVO;
        }

        private void loadvalue()
        {
            try
            {
                ORLogVO orlog = new ORLogVO();
                List<ORLogVO> lstORLogVO = new BLORLog(dbInfo).SearchByKey(orlog);
                gvLogOR.DataSource = lstORLogVO;
                gvLogOR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void gvLogOR_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom1, "select$" + e.Row.RowIndex);
            //    e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
            //    e.Row.Cells[5].Attributes["data-toggle"] = "modal";
            //    e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
            //    //
            //    manageRow(e);
            //}
        }

        protected void gvLogOR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (e.CommandName == "select")
            //{
            //    int rowIndex = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = gvORRoom1.Rows[rowIndex];
            //    loadvalueDetail(row);
            //}
            //else if (e.CommandName == "ed")
            //{
            //    int rowIndex = Convert.ToInt32(e.CommandArgument);
            //    GridViewRow row = gvORRoom1.Rows[rowIndex];
            //    string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
            //    Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            //}
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

    }
}