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

namespace solution.PostOR
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
                    //hdDate.Value = Session["ORDate"].ToString();
                    hdDateFrom.Value = Session["ORDate"].ToString();
                    hdDateTo.Value = Session["ORDate"].ToString();
                }
                else
                {
                    //hdDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                MapDDL();
                loadvalue();
            }
        }

        private void loadvalue()
        {
            try
            {
                //if (string.IsNullOrEmpty(hdDate.Value))
                //{
                //    DateTime dd = DateTime.Now;
                //    hdDate.Value = dd.ToString("dd/MM/yyyy");
                //   //  hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                //}
                //else
                //{
                //    DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                //    hdDate.Value = dd.ToString("dd/MM/yyyy");
                //    // hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                //}
                //Session["ORDate"] = hdDate.Value;

                if (string.IsNullOrEmpty(hdDateFrom.Value))
                {
                    DateTime dd = DateTime.Now;
                    hdDateFrom.Value = dd.ToString("dd/MM/yyyy");
                    //  hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                    hdDateFrom.Value = dd.ToString("dd/MM/yyyy");
                    // hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }
                Session["ORDate"] = hdDateFrom.Value;

                if (string.IsNullOrEmpty(hdDateTo.Value))
                {
                    DateTime dd = DateTime.Now;
                    hdDateTo.Value = dd.ToString("dd/MM/yyyy");
                    //  hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                    hdDateTo.Value = dd.ToString("dd/MM/yyyy");
                    // hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }

                string arORRoom = string.Empty;
                if (Session["USERNANME"].ToString() != "ADMIN")
                {
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


                    SETUPORROOMVO.ArCodeType = arRoomType;

                    List<SETUPORROOMVO> lstBLSETUPORROOM = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                    arORRoom = string.Empty;
                    foreach (SETUPORROOMVO vl1 in lstBLSETUPORROOM)
                    {
                        if (arORRoom != string.Empty)
                            arORRoom += ",";
                        arORRoom += "'" + vl1.CODE + "'";
                    }
                }

                ORHEADERVO _ORHEADERVO = new ORHEADERVO();
                //_ORHEADERVO.ORDate = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                _ORHEADERVO.ORDateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                _ORHEADERVO.ORDateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                _ORHEADERVO.ORRoom = ddlORRoom.SelectedValue;
                _ORHEADERVO.HN = txtHN.Text;
                _ORHEADERVO.Surgeon = ddlSurgeon.SelectedValue;
                _ORHEADERVO.ArORRoom = arORRoom;

                List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
                List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchPostORByKey(_ORHEADERVO);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
                {
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
                        POSTOROPERATIONVO evl = new POSTOROPERATIONVO();
                        evl.ORID = ORHEADERVO.ORID;
                        List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(evl);
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

                        SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                        SETUPORROOMVO.CODE = ORHEADERVO.ORRoom;
                        List<SETUPORROOMVO> lstSETUPORROOMVO = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                        if (lstSETUPORROOMVO.Count > 0)
                        {
                            ORHEADERVO.strORRoom = lstSETUPORROOMVO[0].Name;
                        }
                        lstORHEADERVO.Add(ORHEADERVO);
                    }
                    //} Check Appointment Number https://github.com/MrSomyong/OR/issues/1
                }
                gvPostOR.DataSource = lstORHEADERVO;
                gvPostOR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MapDDL()
        {
            try
            {

                ListItem lit = new ListItem();
                lit.Text = "None";
                lit.Value = "";

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

                ddlORRoom.DataSource = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                ddlORRoom.DataValueField = "CODE";
                ddlORRoom.DataTextField = "Name";
                ddlORRoom.DataBind();
                ddlORRoom.Items.Insert(0, lit);

                ListItem litSurgeon = new ListItem();
                litSurgeon.Text = "None";
                litSurgeon.Value = "";
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                ddlSurgeon.DataSource = lstDOCTORMASTERVO;
                ddlSurgeon.DataValueField = "DOCTOR";
                ddlSurgeon.DataTextField = "DoctorName";
                ddlSurgeon.DataBind();
                ddlSurgeon.Items.Insert(0, litSurgeon);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void manageRow(GridViewRowEventArgs e)
        {
            string lblgvSurgeon = (e.Row.FindControl("lblgvSurgeon") as Label).Text;

            //
            string iORDate = (e.Row.FindControl("hdgvORDate") as HiddenField).Value;
            string iCreDate = (e.Row.FindControl("hdgvCreateDate") as HiddenField).Value;
            string iUpDate = (e.Row.FindControl("hdgvUpdateDate") as HiddenField).Value;
            string iCancelDate = (e.Row.FindControl("hdgvCancelDate") as HiddenField).Value;
            string iCxlPostOR = (e.Row.FindControl("hdgvCancelPostOR") as HiddenField).Value;

            if (iCxlPostOR == "True")
            {
                e.Row.Font.Strikeout = true;
            }

            //if (!string.IsNullOrEmpty(iCancelDate))
            //{
            //    e.Row.Font.Strikeout = true;
            //}
            //if (!string.IsNullOrEmpty(iUpDate))
            //{
            //    DateTime dOrdate = DateTime.Parse(iORDate);
            //    DateTime dUpDate = DateTime.Parse(iUpDate);
            //    checkcolorrow(dOrdate, dUpDate, e);
            //}
            //else
            //{
            //    DateTime dOrdate = DateTime.Parse(iORDate);
            //    DateTime dCreDate = DateTime.Parse(iCreDate);
            //    checkcolorrow(dOrdate, dCreDate, e);
            //}
            //if (!string.IsNullOrEmpty(lblgvSurgeon) && lblgvSurgeon == prvgv1Surgeon)
            //{
            //    (e.Row.FindControl("lblgvSurgeon") as Label).Text = "\"";
            //}
            //prvgv1Surgeon = lblgvSurgeon;
            ////
            //string hdgvORStatCase = (e.Row.FindControl("hdgvORStatCase") as HiddenField).Value;
            //if (hdgvORStatCase == "True")
            //{
            //    e.Row.BackColor = System.Drawing.Color.LightPink;
            //    e.Row.Cells[0].Text = "<i runat=\"server\" class=\"fa fa-warning faa-flash animated\" style=\"color:red\"></i>";
            //}
            //else
            //{ e.Row.Cells[0].Text = ""; }
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

        protected void gvPostOR_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[4].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPostOR, "select$" + e.Row.RowIndex);
                e.Row.Cells[4].Attributes["style"] = "cursor:pointer";
                e.Row.Cells[4].Attributes["data-toggle"] = "modal";
                e.Row.Cells[4].Attributes["data-target"] = "#exampleModalLong";
                //
                manageRow(e);
            }
        }

        protected void gvPostOR_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPostOR.Rows[rowIndex];
                loadvalueDetail(row);
            }
            else if (e.CommandName == "ed")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPostOR.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                Response.Redirect("/PostOR/PostOR/?d=" + orid, false);
            }
            else if (e.CommandName == "display")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPostOR.Rows[rowIndex];
                string orid = (row.FindControl("hdgvORID") as HiddenField).Value;
                string strSourceData = "ADC";
                string Surgeon1 = (row.FindControl("hdSurgeon1") as HiddenField).Value;
                string Surgeon2 = (row.FindControl("hdSurgeon2") as HiddenField).Value;
                string Surgeon3 = (row.FindControl("hdSurgeon3") as HiddenField).Value;
                SavePostOROperation(orid, Surgeon1, Surgeon2, Surgeon3);

                Response.Redirect("/PostOR/Display/?d=" + orid+"&s="+ strSourceData, false);
            }
        }

        private void SavePostOROperation(string orid, string Surgeon1, string Surgeon2, string Surgeon3)
        {
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = orid;
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
            if (lstPOSTOROPERATIONVO.Count == 0)
            {
                List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(orid);

                foreach (OROPERATIONVO OROPERATIONVO in lstOROPERATIONVO)
                {
                    POSTOROPERATIONVO xx = new POSTOROPERATIONVO();
                    xx.ID = OROPERATIONVO.ID;
                    xx.ORID = OROPERATIONVO.ORID;
                    xx.Seq = OROPERATIONVO.Seq;

                    xx.MainCode = OROPERATIONVO.MainCode;
                    xx.SubCode = OROPERATIONVO.SubCode;
                    xx.Side = OROPERATIONVO.Side;
                    xx.strSide = ((EnumOR.ORSide)OROPERATIONVO.Side).ToString();
                    xx.SubMark = OROPERATIONVO.SubMark;
                    xx.strSubMark = OROPERATIONVO.strSubMark;
                    xx.Name = OROPERATIONVO.Name;
                    xx.SubName = OROPERATIONVO.SubName;
                    xx.Surgeon1 = Surgeon1;
                    xx.Surgeon2 = Surgeon2;
                    xx.Surgeon3 = Surgeon3;

                    new BLPOSTOROPERATION(dbInfo).Insert(xx);
                }
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
                    SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.CODE = _lstORHEADERVO[0].ORRoom;
                    try { lblORRoom.Text = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO)[0].Name; } catch { }

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
                    lblBirthDateTime.Text = _ORPATIENTVO.BirthDateTime.Value.ToString("dd-MM-yyyy");
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

        public void loadvalueDisplay(GridViewRow row)
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
                    lblBirthDateTime.Text = CultureInfo.GetDatetime(_ORPATIENTVO.BirthDateTime.Value, YearType.Thai).ToString("dd MMM yyyy");
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        private void ClearValue()
        {
        }

        private void ClearvalueDetail()
        {
            
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Print/prtReport/?d=" + hdDate.Value, false);
            Response.Redirect("/Print/ORHeader/?d=" + hdDateFrom.Value, false);
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