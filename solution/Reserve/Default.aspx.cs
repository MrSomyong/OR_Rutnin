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
using System.Configuration;

namespace solution.Reserve
{
    public partial class Default : System.Web.UI.Page
    {      
        
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        private string prvgv1Surgeon = string.Empty;
        private string prvgv2Surgeon = string.Empty;
        private string prvgv3Surgeon = string.Empty;
        private string prvgv4Surgeon = string.Empty;
        private string prvgv5Surgeon = string.Empty;
        private string prvgv6Surgeon = string.Empty;
        private string prvgv7Surgeon = string.Empty;
        private string prvgv8Surgeon = string.Empty;
        private string prvgv9Surgeon = string.Empty;
        public string PictureFileName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //try { 
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
            //}
            //catch (Exception ex) { throw ex; }
        }

        private List<ORHEADERVO> loadORHeader(string CODE)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            System.Diagnostics.Debug.WriteLine("value ==> " + hdDate.Value.ToString());
            DateTime dd = DateTime.Parse(Utilities.ConvertYMD(hdDate.Value));
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByRoom(CODE, dd);            
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

                //--------------------------//
                //https://github.com/MrSomyong/OR/issues/1
                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                APPOINTMENTVO.HN = ORHEADERVO.HN;
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
                        b = _br + b + " : <code>" + EnumOR.ORSide.BE.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(None))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                            _br = "<br/>";
                        None = _br + None + " : <code>" + EnumOR.ORSide.None.ToString() + "</code>";
                    }
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                            _br = "<br/>";
                        NA = _br + NA + " : <code>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</code>";
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

                    lstORHEADERVO.Add(ORHEADERVO);
                }
                //} //https://github.com/MrSomyong/OR/issues/1
            }

            return lstORHEADERVO;
        }

        private void loadvalue()
        {
            //try
            //{
                string strlatetime = ConfigurationManager.AppSettings["LateTime"];
                try {
                    lblLateTime.Text = strlatetime.Substring(0, 2) + "." + strlatetime.Substring(2, 2);
                } catch { }
                if (string.IsNullOrEmpty(hdDate.Value))
                {
                    DateTime dd = DateTime.Now;
                    hdDate.Value = dd.ToString("dd/MM/yyyy");
                    hdDateEn.Value = dd.ToString("dd/MM/yyyy");
            }
                else
                {
                    DateTime dd = DateTime.Parse(Utilities.ConvertYMD(hdDate.Value));
                    hdDate.Value = dd.ToString("dd/MM/yyyy");
                    hdDateEn.Value = dd.ToString("dd/MM/yyyy");
            }
                Session["ORDate"] = hdDate.Value;

                SETUPUSERROOMTYPEVO SETUPUSERROOMTYPEVO = new SETUPUSERROOMTYPEVO();
                SETUPUSERROOMTYPEVO.UserID = Session["USERID"].ToString();
                List<SETUPUSERROOMTYPEVO> lstval = new BLSETUPUSERROOMTYPE(dbInfo).SearchByKey(SETUPUSERROOMTYPEVO);
                string arRoomType = string.Empty;
                foreach (SETUPUSERROOMTYPEVO vl1 in lstval)
                {
                    if (arRoomType != string.Empty)
                        arRoomType += ",";
                    arRoomType += "'" + vl1.RoomType + "'";
                }

                SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();

                if (Session["USERNANME"].ToString() != "ADMIN")
                    SETUPORROOMVO.ArCodeType = arRoomType;

                List<SETUPORROOMVO> lstorromm = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                int i = 1;
                ClearValue();
                foreach (SETUPORROOMVO xx in lstorromm)
                {
                    if (i == 1)
                    {
                        pnORRoom1.Visible = true;
                        lblORRoom1.Text = xx.Name;
                        hdORRoom1.Value = xx.CODE;
                        gvORRoom1.DataSource = loadORHeader(xx.CODE);
                        gvORRoom1.DataBind();
                    }
                    else if (i == 2)
                    {
                        pnORRoom2.Visible = true;
                        lblORRoom2.Text = xx.Name;
                        hdORRoom2.Value = xx.CODE;
                        gvORRoom2.DataSource = loadORHeader(xx.CODE);
                        gvORRoom2.DataBind();
                    }
                    else if (i == 3)
                    {
                        pnORRoom3.Visible = true;
                        lblORRoom3.Text = xx.Name;
                        hdORRoom3.Value = xx.CODE;
                        gvORRoom3.DataSource = loadORHeader(xx.CODE);
                        gvORRoom3.DataBind();
                    }
                    else if (i == 4)
                    {
                        pnORRoom4.Visible = true;
                        lblORRoom4.Text = xx.Name;
                        hdORRoom4.Value = xx.CODE;
                        gvORRoom4.DataSource = loadORHeader(xx.CODE);
                        gvORRoom4.DataBind();
                    }
                    else if (i == 5)
                    {
                        pnORRoom5.Visible = true;
                        lblORRoom5.Text = xx.Name;
                        hdORRoom5.Value = xx.CODE;
                        gvORRoom5.DataSource = loadORHeader(xx.CODE);
                        gvORRoom5.DataBind();
                    }
                    else if (i == 6)
                    {
                        pnORRoom6.Visible = true;
                        lblORRoom6.Text = xx.Name;
                        hdORRoom6.Value = xx.CODE;
                        gvORRoom6.DataSource = loadORHeader(xx.CODE);
                        gvORRoom6.DataBind();
                    }
                    else if (i == 7)
                    {
                        pnORRoom7.Visible = true;
                        lblORRoom7.Text = xx.Name;
                        hdORRoom7.Value = xx.CODE;
                        gvORRoom7.DataSource = loadORHeader(xx.CODE);
                        gvORRoom7.DataBind();
                    }
                    else if (i == 8)
                    {
                        pnORRoom8.Visible = true;
                        lblORRoom8.Text = xx.Name;
                        hdORRoom8.Value = xx.CODE;
                        gvORRoom8.DataSource = loadORHeader(xx.CODE);
                        gvORRoom8.DataBind();
                    }
                    else if (i == 9)
                    {
                        pnORRoom9.Visible = true;
                        lblORRoom9.Text = xx.Name;
                        hdORRoom9.Value = xx.CODE;
                        gvORRoom9.DataSource = loadORHeader(xx.CODE);
                        gvORRoom9.DataBind();
                    }
                    i++;
                }

                loadAnesthesia();
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

        }

        private void manageRow(GridViewRowEventArgs e)
        {
            string lblgvSurgeon = (e.Row.FindControl("lblgvSurgeon") as Label).Text;

            //
            string iORDate = (e.Row.FindControl("hdgvORDate") as HiddenField).Value;
            string iCreDate = (e.Row.FindControl("hdgvCreateDate") as HiddenField).Value;
            string iUpDate = (e.Row.FindControl("hdgvUpdateDate") as HiddenField).Value;
            string iCancelDate = (e.Row.FindControl("hdgvCancelDate") as HiddenField).Value;

            if (!string.IsNullOrEmpty(iCancelDate))
            {
                e.Row.Font.Strikeout = true;
            }
            if (!string.IsNullOrEmpty(iUpDate))
            {
                DateTime dOrdate = DateTime.Parse(iORDate);
                DateTime dUpDate = DateTime.Parse(iUpDate);
                checkcolorrow(dOrdate, dUpDate, e);
            }
            else
            {
                DateTime dOrdate = DateTime.Parse(iORDate);
                DateTime dCreDate = DateTime.Parse(iCreDate);
                checkcolorrow(dOrdate, dCreDate, e);
            }
            //if (!string.IsNullOrEmpty(lblgvSurgeon) && lblgvSurgeon == prvgv1Surgeon)
            //{
            //    (e.Row.FindControl("lblgvSurgeon") as Label).Text = "\"";
            //}
            //prvgv1Surgeon = lblgvSurgeon;
            //
            string hdgvORStatCase = (e.Row.FindControl("hdgvORStatCase") as HiddenField).Value;
            if (hdgvORStatCase == "True")
            {
                e.Row.BackColor = System.Drawing.Color.LightPink;
                e.Row.Cells[0].Text = "<i runat=\"server\" class=\"fa fa-warning faa-flash animated\" style=\"color:red\"></i>";
            }
            else
            { e.Row.Cells[0].Text = ""; }
        }

        private void checkcolorrow(DateTime iORDate, DateTime iCreDate, GridViewRowEventArgs e)
        {
            int iordate = int.Parse(iORDate.ToString("yyyMMdd")) - int.Parse(iCreDate.ToString("yyyMMdd"));
            if (iordate == 1)
            {
                string strlatetime = ConfigurationManager.AppSettings["LateTime"];
                int latetime = 1530;
                try { latetime = int.Parse(strlatetime); } catch { }
                int itime = latetime - int.Parse(iCreDate.ToString("HHmm"));
                if (itime <= 0)
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                }
            }
            else if (iordate < 1)
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
            }
        }

        protected void gvORRoom1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom1, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                //
                manageRow(e);
            }
        }

        protected void gvORRoom2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom2, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                //
                manageRow(e);
            }
        }

        protected void gvORRoom3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom3, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }
        }

        protected void gvORRoom4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom4, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }
        }

        protected void gvORRoom5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom5, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }

        }

        protected void gvORRoom6_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom6, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }

        }

        protected void gvORRoom7_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom7, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }

        }

        protected void gvORRoom8_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom8, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }

        }

        protected void gvORRoom9_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[5].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvORRoom9, "select$" + e.Row.RowIndex);
                e.Row.Cells[5].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[5].Attributes["data-toggle"] = "modal";
                e.Row.Cells[5].Attributes["data-target"] = "#exampleModalLong";
                manageRow(e);
            }

        }

        protected void gvORRoom1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom1.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom1.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom2.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom2.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom3_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom3.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom3.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom4.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom4.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom5.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom5.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom6.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom6.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom7_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom7.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom7.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom8_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom8.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom8.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        protected void gvORRoom9_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom9.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvORRoom9.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/Reserve/Edit/?d=" + orid, false);
            }
        }

        public void loadvalueDetail(GridViewRow row)
        {
            try
            {
                ClearvalueDetail();
                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORID = (row.FindControl("hdgvORID") as HiddenField).Value;

                List<ORHEADERVO> _lstORHEADERVO = new BLORHEADER(dbInfo).SearchByKey(ORHEADERVO);
                if (_lstORHEADERVO.Count > 0)
                {
                    byte[] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(_lstORHEADERVO[0].HN);
                    if (bytes != null)
                    {
                        string base64String = Convert.ToBase64String(bytes);
                        imgPatient.ImageUrl = "data:image/png;base64," + base64String;
                    }
                    else
                    {
                        string strURL = "http://172.25.41.30/pdp/upload/hn/" + _lstORHEADERVO[0].HN + ".jpg";
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
                    hdORID.Value = _lstORHEADERVO[0].ORID;
                    lblHN.Text = _lstORHEADERVO[0].HN;
                    chbPatientInfection.Checked = _lstORHEADERVO[0].PatientInfection;
                    chbPatientType1.Checked = _lstORHEADERVO[0].PatientType1;
                    chbPatientType2.Checked = _lstORHEADERVO[0].PatientType2;
                    chbPatientUP.Checked = _lstORHEADERVO[0].PatientUP;
                    lblORDate.Text = _lstORHEADERVO[0].strORDate;
                    lblORCASE.Text = _lstORHEADERVO[0].ORCase.ToString();
                    lblORTime.Text = _lstORHEADERVO[0].ORTime;
                    lblArrivalTime.Text = _lstORHEADERVO[0].ArrivalTime;
                    chbORTimeFollow.Checked = _lstORHEADERVO[0].ORTimeFollow;
                    if (chbORTimeFollow.Checked)
                    {
                        lblORTimeH.Visible = false;
                        lblORTime.Visible = false;
                    }
                    else
                    {
                        lblORTimeH.Visible = true;
                        lblORTime.Visible = true;
                    }
                    chbORStatCase.Checked = _lstORHEADERVO[0].ORStatCase;

                    if (_lstORHEADERVO[0].ORSpecificType == ((int)EnumOR.ORSpecificType.Refer).ToString())
                    {
                        lblORSpecificType.Text = EnumOR.ORSpecificType.Refer.ToString();
                    }
                    else if (_lstORHEADERVO[0].ORSpecificType == ((int)EnumOR.ORSpecificType.Specific).ToString())
                    {
                        lblORSpecificType.Text = EnumOR.ORSpecificType.Specific.ToString();
                    }
                    else
                    {
                        lblORSpecificType.Text = EnumOR.ORSpecificType.None.ToString();
                    }

                    if (_lstORHEADERVO[0].ORStatus == ((int)EnumOR.ORStatus.IPD).ToString())
                    {
                        lblORStatus.Text = EnumOR.ORStatus.IPD.ToString();
                    }
                    else if (_lstORHEADERVO[0].ORStatus == ((int)EnumOR.ORStatus.Observe).ToString())
                    {
                        lblORStatus.Text = EnumOR.ORStatus.Observe.ToString();
                    }
                    else if (_lstORHEADERVO[0].ORStatus == ((int)EnumOR.ORStatus.OPD).ToString())
                    {
                        lblORStatus.Text = EnumOR.ORStatus.OPD.ToString();
                    }
                    else if (_lstORHEADERVO[0].ORStatus == ((int)EnumOR.ORStatus.Reserve).ToString())
                    {
                        lblORStatus.Text = EnumOR.ORStatus.Reserve.ToString();
                    }
                    else
                    {
                        lblORStatus.Text = EnumOR.ORStatus.None.ToString();
                    }

                    if (_lstORHEADERVO[0].AdmitTimeType == ((int)EnumOR.AdmitTimeType.เช้า).ToString())
                    {
                        lblAdmitTimeType.Text = EnumOR.AdmitTimeType.เช้า.ToString();
                    }
                    else if (_lstORHEADERVO[0].AdmitTimeType == ((int)EnumOR.AdmitTimeType.บ่าย).ToString())
                    {
                        lblAdmitTimeType.Text = EnumOR.AdmitTimeType.บ่าย.ToString();
                    }
                    else
                    {
                        lblAdmitTimeType.Text = string.Empty;
                    }

                    lblRoomType.Text = _lstORHEADERVO[0].RoomType;
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].RoomType))
                    {
                        divRoomType.Visible = true;
                        lblRoomType.Text = (new BLROOMTYPE(dbInfo).SearchByCode(_lstORHEADERVO[0].RoomType)).NAME;
                    }
                    lblORRoom.Text = new BLOPERATIONROOM(dbInfo).SearchByCode(_lstORHEADERVO[0].ORRoom).NAME;
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaType1))
                    {
                        lblAnesthesiaType1.Text = (new BLANESTHESIA(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaType1)).NAME;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaSign))
                    {
                        lblAnesthesiaSign.Text = ORUtils.getAnesthesiaSign(_lstORHEADERVO[0].AnesthesiaSign);
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaType2))
                    {
                        divAnesthesiaType2.Visible = true;
                        lblAnesthesiaType2.Text = (new BLANESTHESIA(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaType2)).NAME;
                    }
                    lblRemark.Text = _lstORHEADERVO[0].Remark;

                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].Surgeon1))
                    {
                        lblSurgeon1.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].Surgeon1)).DoctorName;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].Surgeon2))
                    {
                        lblSurgeon2.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].Surgeon2)).DoctorName;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].Surgeon3))
                    {
                        lblSurgeon3.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].Surgeon3)).DoctorName;
                    }

                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaDoctor1))
                    {
                        lblAnesthesiaDoctor1.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaDoctor1)).DoctorName;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaDoctor2))
                    {
                        lblAnesthesiaDoctor2.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaDoctor2)).DoctorName;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaDoctor2))
                    {
                        lblAnesthesiaDoctor3.Text = (new BLDOCTORMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaDoctor3)).DoctorName;
                    }

                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaNurse1))
                    {
                        lblAnesthesiaNurse1.Text = (new BLNURSEMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaNurse1)).NAME;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaNurse2))
                    {
                        lblAnesthesiaNurse2.Text = (new BLNURSEMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaNurse2)).NAME;
                    }
                    if (!string.IsNullOrEmpty(_lstORHEADERVO[0].AnesthesiaNurse3))
                    {
                        lblAnesthesiaNurse3.Text = (new BLNURSEMASTER(dbInfo).SearchByCode(_lstORHEADERVO[0].AnesthesiaNurse3)).NAME;
                    }

                    ORPATIENTVO _ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(_lstORHEADERVO[0].HN);
                    lblPatientName.Text = _ORPATIENTVO.PatientName;
                    lblGender.Text = _ORPATIENTVO.Sex;
                    lblAge.Text = _ORPATIENTVO.Age;
                    lblBirthDateTime.Text = CultureInfo.GetDatetime(_ORPATIENTVO.BirthDateTime.Value, YearType.English).ToString("dd-MM-yyyy");
                    lblIDCARD.Text = _ORPATIENTVO.Ref;
                    lblNationality.Text = _ORPATIENTVO.Nationality;

                    PictureFileName = _ORPATIENTVO.PictureFileName;

                    PATIENTALLEGICVO _vl = new PATIENTALLEGICVO();
                    _vl.HN = _ORPATIENTVO.HN;
                    List<PATIENTALLEGICVO> lstPATIENTALLEGICVO = new BLPATIENTALLEGIC(dbInfo).SearchByKey(_vl);
                    string allegicname = string.Empty;
                    string Reaction = string.Empty;
                    gvPatientallegic.Visible = false;
                    lblPatientallegic.Text = string.Empty;
                    if (lstPATIENTALLEGICVO.Count == 1)
                    {
                        lblPatientallegic.Text = "แพ้ยา : <strong>" + lstPATIENTALLEGICVO[0].allegicname + "</strong>   ||   อาการ : <strong>" + lstPATIENTALLEGICVO[0].Reaction + "</strong>";
                    }
                    else if (lstPATIENTALLEGICVO.Count > 1)
                    {
                        gvPatientallegic.Visible = true;
                        gvPatientallegic.DataSource = lstPATIENTALLEGICVO;
                        gvPatientallegic.DataBind();
                    }

                    PATIENTDIAGVO _PATIENTDIAGVO = new PATIENTDIAGVO();
                    _PATIENTDIAGVO.HN = _ORPATIENTVO.HN;
                    List<PATIENTDIAGVO> lstPATIENTDIAGVO = new BLPATIENTDIAG(dbInfo).SearchByKey(_PATIENTDIAGVO);
                    string diagname = string.Empty;
                    gvPatientDiag.Visible = false;
                    lblPatientDiag.Text = string.Empty;
                    if (lstPATIENTDIAGVO.Count == 1)
                    {
                        lblPatientDiag.Text = "โรคประจำตัว : <strong>" + lstPATIENTDIAGVO[0].diagname + "</strong>";
                    }
                    else if (lstPATIENTDIAGVO.Count > 1)
                    {
                        gvPatientDiag.Visible = true;
                        gvPatientDiag.DataSource = lstPATIENTDIAGVO;
                        gvPatientDiag.DataBind();
                    }

                    List<OROPERATIONVO> lstOROPERATIONVOTemp = new List<OROPERATIONVO>();
                    OROPERATIONVO opRight = new OROPERATIONVO();
                    opRight.Side = (int)EnumOR.ORSide.RE;
                    opRight.strSide = EnumOR.ORSide.RE.ToString();
                    OROPERATIONVO opLeft = new OROPERATIONVO();
                    opLeft.Side = (int)EnumOR.ORSide.LE;
                    opLeft.strSide = EnumOR.ORSide.LE.ToString();
                    OROPERATIONVO opBoth = new OROPERATIONVO();
                    opBoth.Side = (int)EnumOR.ORSide.BE;
                    opBoth.strSide = EnumOR.ORSide.BE.ToString();
                    OROPERATIONVO opNone = new OROPERATIONVO();
                    opNone.Side = (int)EnumOR.ORSide.None;
                    opNone.strSide = EnumOR.ORSide.None.ToString();
                    OROPERATIONVO opNA = new OROPERATIONVO();
                    opNA.Side = (int)EnumOR.ORSide.ยังไม่ระบุตา;
                    opNA.strSide = EnumOR.ORSide.ยังไม่ระบุตา.ToString();

                    List<OROPERATIONVO> _lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(_lstORHEADERVO[0].ORID);
                    foreach (OROPERATIONVO x in _lstOROPERATIONVO)
                    {
                        if (x.Side == (int)EnumOR.ORSide.RE)
                        {
                            string strsubmark = ORUtils.getSide(x.SubMark);
                            if (!string.IsNullOrEmpty(opRight.SubName) && string.IsNullOrEmpty(strsubmark))
                                strsubmark = ",";
                            opRight.SubName += strsubmark + x.SubName;
                        }
                        else if (x.Side == (int)EnumOR.ORSide.LE)
                        {
                            string strsubmark = ORUtils.getSide(x.SubMark);
                            if (!string.IsNullOrEmpty(opLeft.SubName) && string.IsNullOrEmpty(strsubmark))
                                strsubmark = ",";
                            opLeft.SubName += strsubmark + x.SubName;
                        }
                        else if (x.Side == (int)EnumOR.ORSide.BE)
                        {
                            string strsubmark = ORUtils.getSide(x.SubMark);
                            if (!string.IsNullOrEmpty(opBoth.SubName) && string.IsNullOrEmpty(strsubmark))
                                strsubmark = ",";
                            opBoth.SubName += strsubmark + x.SubName;
                        }
                        else if (x.Side == (int)EnumOR.ORSide.None)
                        {
                            string strsubmark = ORUtils.getSide(x.SubMark);
                            if (!string.IsNullOrEmpty(opNone.SubName) && string.IsNullOrEmpty(strsubmark))
                                strsubmark = ",";
                            opNone.SubName += strsubmark + x.SubName;
                        }
                        else if (x.Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                        {
                            string strsubmark = ORUtils.getSide(x.SubMark);
                            if (!string.IsNullOrEmpty(opNA.SubName) && string.IsNullOrEmpty(strsubmark))
                                strsubmark = ",";
                            opNA.SubName += strsubmark + x.SubName;
                        }
                    }
                    if (!string.IsNullOrEmpty(opRight.SubName))
                        lstOROPERATIONVOTemp.Add(opRight);
                    if (!string.IsNullOrEmpty(opLeft.SubName))
                        lstOROPERATIONVOTemp.Add(opLeft);
                    if (!string.IsNullOrEmpty(opBoth.SubName))
                        lstOROPERATIONVOTemp.Add(opBoth);
                    if (!string.IsNullOrEmpty(opNone.SubName))
                        lstOROPERATIONVOTemp.Add(opNone);
                    if (!string.IsNullOrEmpty(opNA.SubName))
                        lstOROPERATIONVOTemp.Add(opNA);
                    gvOROperation.DataSource = lstOROPERATIONVOTemp;
                    gvOROperation.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadAnesthesia()
        {
            lblAnesthesiaDoctor.Text = string.Empty;
            ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
            ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(Utilities.ConvertYMD(hdDate.Value));
            List<ORANESTHESIADOCTORSCHEDULEVO> lstORANESTHESIADOCTORSCHEDULEVO = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByKey(ORANESTHESIADOCTORSCHEDULEVO);
            foreach (ORANESTHESIADOCTORSCHEDULEVO xx in lstORANESTHESIADOCTORSCHEDULEVO)
            {
                if (!string.IsNullOrEmpty(lblAnesthesiaDoctor.Text))
                {
                    lblAnesthesiaDoctor.Text += "<br/>";
                }
                lblAnesthesiaDoctor.Text += " <code>" + xx.StartAnesthesiaDateTime.Value.ToShortTimeString() + "</code> " + xx.DoctorName + " : " + xx.Reamrk;
            }

            ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
            ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(Utilities.ConvertYMD(hdDate.Value));
            List<ORANESTHESIANURSESCHEDULEVO> lstORANESTHESIANURSESCHEDULEVO = new BLORANESTHESIANURSESCHEDULE(dbInfo).SearchByKey(ORANESTHESIANURSESCHEDULEVO);
            lblAnesthesiaNurse.Text = string.Empty;

            foreach (ORANESTHESIANURSESCHEDULEVO xx in lstORANESTHESIANURSESCHEDULEVO)
            {
                if (!string.IsNullOrEmpty(lblAnesthesiaNurse.Text))
                {
                    lblAnesthesiaNurse.Text += "<br/>";
                }
                lblAnesthesiaNurse.Text += " <code>" + xx.StartAnesthesiaDateTime.Value.ToShortTimeString() + "</code> " + xx.Name + " : " + xx.Reamrk;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        private void ClearValue()
        {
            pnORRoom1.Visible = false;
            pnORRoom2.Visible = false;
            pnORRoom3.Visible = false;
            pnORRoom4.Visible = false;
            pnORRoom5.Visible = false;
            pnORRoom6.Visible = false;
            pnORRoom7.Visible = false;
            pnORRoom8.Visible = false;
            pnORRoom9.Visible = false;
        }

        private void ClearvalueDetail()
        {
            lblHN.Text = string.Empty;
            chbPatientInfection.Checked = false;
            chbPatientType1.Checked = false;
            chbPatientType2.Checked = false;
            chbPatientUP.Checked = false;
            lblORDate.Text = string.Empty;
            lblORCASE.Text = string.Empty;
            lblORTime.Text = string.Empty;
            chbORTimeFollow.Checked = false;
            chbORStatCase.Checked = false;
            lblORSpecificType.Text = string.Empty;
            lblORStatus.Text = string.Empty;
            lblAdmitTimeType.Text = string.Empty;
            divRoomType.Visible = false;
            lblRoomType.Text = string.Empty;
            lblORRoom.Text = string.Empty;

            lblAnesthesiaType1.Text = string.Empty;

            divAnesthesiaType2.Visible = false;
            lblAnesthesiaSign.Text = string.Empty;

            lblAnesthesiaType2.Text = string.Empty;

            lblRemark.Text = string.Empty;

            lblSurgeon1.Text = string.Empty;

            lblSurgeon2.Text = string.Empty;

            lblSurgeon3.Text = string.Empty;

            lblAnesthesiaDoctor1.Text = string.Empty;

            lblAnesthesiaDoctor2.Text = string.Empty;

            lblAnesthesiaDoctor3.Text = string.Empty;

            lblAnesthesiaNurse1.Text = string.Empty;

            lblAnesthesiaNurse2.Text = string.Empty;

            lblAnesthesiaNurse3.Text = string.Empty;


            lblPatientName.Text = string.Empty;
            lblGender.Text = string.Empty;
            lblAge.Text = string.Empty;
            lblBirthDateTime.Text = string.Empty;
            lblIDCARD.Text = string.Empty;
            lblNationality.Text = string.Empty;

            List<OROPERATIONVO> lstOROPERATIONVOTemp = new List<OROPERATIONVO>();
            OROPERATIONVO opRight = new OROPERATIONVO();
            opRight.Side = (int)EnumOR.ORSide.RE;
            OROPERATIONVO opLeft = new OROPERATIONVO();
            opLeft.Side = (int)EnumOR.ORSide.LE;
            OROPERATIONVO opBoth = new OROPERATIONVO();
            opBoth.Side = (int)EnumOR.ORSide.BE;
            OROPERATIONVO opNone = new OROPERATIONVO();
            opNone.Side = (int)EnumOR.ORSide.None;
            OROPERATIONVO opNA = new OROPERATIONVO();
            opNA.Side = (int)EnumOR.ORSide.ยังไม่ระบุตา;

            lstOROPERATIONVOTemp.Add(opRight);
            lstOROPERATIONVOTemp.Add(opLeft);
            lstOROPERATIONVOTemp.Add(opBoth);
            lstOROPERATIONVOTemp.Add(opNone);
            lstOROPERATIONVOTemp.Add(opNA);
            gvOROperation.DataSource = lstOROPERATIONVOTemp;
            gvOROperation.DataBind();
        }

        protected void lnkbtnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Reserve/Add/", false);
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Print/prtReport/?d=" + hdDate.Value, false);
            Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void lnkbtnPrint1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom1.Value, false);
        }
        protected void lnkbtnPrint2_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom2.Value, false);
        }
        protected void lnkbtnPrint3_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom3.Value, false);
        }
        protected void lnkbtnPrint4_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom4.Value, false);
        }
        protected void lnkbtnPrint5_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom5.Value, false);
        }
        protected void lnkbtnPrint6_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom6.Value, false);
        }
        protected void lnkbtnPrint7_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom7.Value, false);
        }
        protected void lnkbtnPrint8_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom8.Value, false);
        }
        protected void lnkbtnPrint9_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Print/ORHeaderAdmint/?d=" + hdDate.Value + "&r=" + hdORRoom9.Value, false);
        }

        protected void btnStickerOR_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Print/prtReportStickerOR/?d=" + hdDate.Value + "&d=" + hdDate.Value, false);
            Response.Redirect("/Print/StickerOR/?o=" + hdORID.Value, false);
            //Response.Redirect("/Print/prtReportStickerOR/?o = " + hdORID.Value, false);
            //d = " + orid
        }
        protected void btnStickerAdmint_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Print/prtReportStickerAdmit/?d=" + hdDate.Value + "&d=" + hdDate.Value, false);
            Response.Redirect("/Print/StickerAdmit/?o=" + hdORID.Value, false);
        }
        protected void btnStickerWard_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Print/prtReportStickerWard/?d=" + hdDate.Value + "&o=" + hdORID.Value, false);
            Response.Redirect("/Print/StickerWard/?o=" + hdORID.Value, false);
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