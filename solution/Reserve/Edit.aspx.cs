using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Reserve
{
    public partial class Edit : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

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
                setdefaultvalue();
                MapDDL();
                loadgvmain();
                loadgvsubEmpty();
                loadgvOROperationEmpty();

                if (Request.QueryString["d"] != null)
                {
                    hdORID.Value = Request.QueryString["d"];
                    loadvalue(hdORID.Value);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }
                
            }

            loadViewLog();
        }

        private void loadViewLog()
        {
            try
            {
                ORLogVO orlog = new ORLogVO();
                orlog.ORID = hdORID.Value;
                List<ORLogVO> lstORLogVO = new BLORLog(dbInfo).SearchByKey(orlog);
                gvLogOR.DataSource = lstORLogVO;
                gvLogOR.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = save();
            if (rtv.Value)
            {
                AlertMessage(true, false, "Update Complete.");
                //loadvalue(hdORID.Value);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, true);
            //setdefaultvalue();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Reserve/");
        }

        private void setdefaultvalue()
        {
            //pnORHEADER.Enabled = false;
            txtHN.Text = string.Empty;
            txtHN.Enabled = true;
            lblPatientName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblBirthDateTime.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            lblGender.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblAge.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblIDCARD.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblNationality.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            divError.Visible = false;
            btnPatientallegicMore.Visible = false;
            btnPatientalDiagMore.Visible = false;
            setbtnDisable();

        }

        public bool URLExists(string url)
        {
            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
            webRequest.Method = "HEAD";
            webRequest.Timeout = 10;
            try
            {
                using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)webRequest.GetResponse())
                {
                    if (response.StatusCode.ToString() == "OK")
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public void loadvalue(string orid)
        {
            try
            {
                ClearSelection();
                ORHEADERVO xx = new ORHEADERVO();
                xx.ORID = orid;
                
                List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByKey(xx);

                foreach (ORHEADERVO hd in lsth)
                {
                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(hd.HN);
                    if (!string.IsNullOrEmpty(ORPATIENTVO.HN))
                    {
 
                        byte[] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(hd.HN);
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

                        pnORHEADER.Enabled = true;
                        setbtnEnable();
                        txtHN.Text = ORPATIENTVO.HN;
                        lblPatientName.Text = ORPATIENTVO.PatientName;
                        lblGender.Text = ORPATIENTVO.Sex;
                        lblBirthDateTime.Text = CultureInfo.GetDatetime(ORPATIENTVO.BirthDateTime.Value, YearType.English).ToString("dd-MM-yyyy");
                        lblAge.Text = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                        lblIDCARD.Text = ORPATIENTVO.Ref;
                        lblNationality.Text = ORPATIENTVO.Nationality;
                        PictureFileName = ORPATIENTVO.PictureFileName;
                        
                        divError.Visible = false;
                        PATIENTALLEGICVO _vl = new PATIENTALLEGICVO();
                        _vl.HN = ORPATIENTVO.HN;
                        List<PATIENTALLEGICVO> lstPATIENTALLEGICVO = new BLPATIENTALLEGIC(dbInfo).SearchByKey(_vl);
                        string allegicname = string.Empty;
                        string Reaction = string.Empty;
                        
                        if (lstPATIENTALLEGICVO.Count > 0)
                        {
                            lblPatientallegic.Text = "แพ้ยา : <strong>" + lstPATIENTALLEGICVO[0].allegicname + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;อาการ : <strong>" + lstPATIENTALLEGICVO[0].Reaction+ "</strong>";
                        }
                        if (lstPATIENTALLEGICVO.Count > 0)
                        {
                            btnPatientallegicMore.Visible = true;
                            gvPatientallegic.DataSource = lstPATIENTALLEGICVO;
                            gvPatientallegic.DataBind();
                        }

                        PATIENTDIAGVO _vlDiagVO = new PATIENTDIAGVO();
                        _vlDiagVO.HN = ORPATIENTVO.HN;
                        List<PATIENTDIAGVO> lstPATIENTDIAGVO = new BLPATIENTDIAG(dbInfo).SearchByKey(_vlDiagVO);
                        string diagname = string.Empty;

                        if (lstPATIENTDIAGVO.Count > 0)
                        {
                            lblPatientalDiag.Text = "โรคประจำตัว : <strong>" + lstPATIENTDIAGVO[0].diagname + "</strong>";
                        }
                        if (lstPATIENTDIAGVO.Count > 0)
                        {
                            btnPatientalDiagMore.Visible = true;
                            gvPatientalDiag.DataSource = lstPATIENTDIAGVO;
                            gvPatientalDiag.DataBind();
                        }
                    }
                    chbPatientInfection.Checked = hd.PatientInfection;
                    chbPatientType1.Checked = hd.PatientType1;
                    chbPatientType2.Checked = hd.PatientType2;
                    chbPatientUP.Checked = hd.PatientUP;

                    hdDate.Value = hd.ORDate.Value.ToString("dd/MM/yyyy");
                    hdDateEn.Value = CultureInfo.GetDateString(hd.ORDate.Value, YearType.English);
                    ddlORCASE.SelectedValue = hd.ORCase.ToString();
                    if (hd.ORTime != "TF")
                    {
                        ddlORTimeH.SelectedValue = hd.ORTime.Substring(0, 2);
                        ddlORTimeM.SelectedValue = hd.ORTime.Substring(3, 2);
                    }
                    ddlArrivalTimeH.SelectedValue = hd.ArrivalTime.Substring(0, 2);
                    ddlArrivalTimeM.SelectedValue = hd.ArrivalTime.Substring(3, 2);
                    chbORTimeFollow.Checked = hd.ORTimeFollow;
                    chbORStatCase.Checked = hd.ORStatCase;
                    ddlORSpecificType.SelectedValue = hd.ORSpecificType;

                    ddlORStatus.SelectedValue = hd.ORStatus;

                    ddlAdmitTimeType.SelectedValue = hd.AdmitTimeType;
                    ddlRoomType.SelectedValue = hd.RoomType;
                    ddlORRoom.SelectedValue = hd.ORRoom;
                    ddlAnesthesiaType1.SelectedValue = hd.AnesthesiaType1;
                    ddlAnesthesiaSign.SelectedValue = hd.AnesthesiaSign;
                    ddlAnesthesiaType2.SelectedValue = hd.AnesthesiaType2;
                    txtRemark.Text = hd.Remark;
                    txtPrediag.Text = hd.Prediag;
                    if (!string.IsNullOrEmpty(hd.Surgeon1))
                        ddlSurgeon1.SelectedValue = hd.Surgeon1;
                    if (!string.IsNullOrEmpty(hd.Surgeon2))
                        ddlSurgeon2.SelectedValue = hd.Surgeon2;
                    if (!string.IsNullOrEmpty(hd.Surgeon3))
                        ddlSurgeon3.SelectedValue = hd.Surgeon3;
                    //
                    if (hd.SurgeonMaster == hd.Surgeon3)
                        rbSurgeon3.Checked = true;
                    else if (hd.SurgeonMaster == hd.Surgeon2)
                        rbSurgeon2.Checked = true;
                    else
                        rbSurgeon1.Checked = true;
                    //
                    if (!string.IsNullOrEmpty(hd.AnesthesiaDoctor1))
                        ddlAnesthesiaDoctor1.SelectedValue = hd.AnesthesiaDoctor1;
                    if (!string.IsNullOrEmpty(hd.AnesthesiaDoctor2))
                        ddlAnesthesiaDoctor2.SelectedValue = hd.AnesthesiaDoctor2;
                    if (!string.IsNullOrEmpty(hd.AnesthesiaDoctor3))
                        ddlAnesthesiaDoctor3.SelectedValue = hd.AnesthesiaDoctor3;

                    if (!string.IsNullOrEmpty(hd.AnesthesiaNurse1))
                        ddlAnesthesiaNurse1.SelectedValue = hd.AnesthesiaNurse1;
                    if (!string.IsNullOrEmpty(hd.AnesthesiaNurse2))
                        ddlAnesthesiaNurse2.SelectedValue = hd.AnesthesiaNurse2;
                    if (!string.IsNullOrEmpty(hd.AnesthesiaNurse3))
                        ddlAnesthesiaNurse3.SelectedValue = hd.AnesthesiaNurse3;
                    ddlSuggestByUser.SelectedValue = hd.SuggestByUser;
                    ddlRequestByUser.SelectedValue = hd.RequestByUser;
                    List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(hd.ORID);
                    gvOROperation.DataSource = lstOROPERATIONVO;
                    gvOROperation.DataBind();

                    LoadlblProcedure();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearSelection()
        {
            ddlORCASE.ClearSelection();
            ddlORTimeH.ClearSelection();
            ddlORTimeM.ClearSelection();
            ddlArrivalTimeH.ClearSelection();
            ddlArrivalTimeM.ClearSelection();
            ddlORSpecificType.ClearSelection();
            ddlORStatus.ClearSelection();
            ddlAdmitTimeType.ClearSelection();
            ddlRoomType.ClearSelection();
            ddlORRoom.ClearSelection();
            ddlAnesthesiaType1.ClearSelection();
            ddlAnesthesiaSign.ClearSelection();
            ddlAnesthesiaType2.ClearSelection();

            ddlSurgeon1.ClearSelection();
            ddlSurgeon2.ClearSelection();
            ddlSurgeon3.ClearSelection();

            ddlAnesthesiaDoctor1.ClearSelection();
            ddlAnesthesiaDoctor2.ClearSelection();
            ddlAnesthesiaDoctor3.ClearSelection();

            ddlAnesthesiaNurse1.ClearSelection();
            ddlAnesthesiaNurse2.ClearSelection();
            ddlAnesthesiaNurse3.ClearSelection();
        }

        private ReturnValue save()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {

                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORID = hdORID.Value;
                ORHEADERVO.HN = txtHN.Text;
                ORHEADERVO.PatientName = lblPatientName.Text;
                ORHEADERVO.PatientInfection = chbPatientInfection.Checked;
                ORHEADERVO.PatientType1 = chbPatientType1.Checked;
                ORHEADERVO.PatientType2 = chbPatientType2.Checked;
                ORHEADERVO.PatientUP = chbPatientUP.Checked;

                ORHEADERVO.ORDate = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value)), YearType.English);
                ORHEADERVO.ORTimeFollow = chbORTimeFollow.Checked;
                ORHEADERVO.ArrivalTime = ddlArrivalTimeH.SelectedValue + ":" + ddlArrivalTimeM.SelectedValue;

                ORHEADERVO.ORStatCase = chbORStatCase.Checked;
                ORHEADERVO.ORCase = int.Parse(ddlORCASE.SelectedValue);

                ORHEADERVO.ORSpecificType = ddlORSpecificType.SelectedValue;

                ORHEADERVO.ORStatus = ddlORStatus.SelectedValue;

                if (ddlORStatus.SelectedValue == ((int)EnumOR.ORStatus.IPD).ToString()
                    || ddlORStatus.SelectedValue == ((int)EnumOR.ORStatus.Reserve).ToString())
                {
                    ORHEADERVO.AdmitTimeType = ddlAdmitTimeType.SelectedValue;
                }
                else
                {
                    ORHEADERVO.AdmitTimeType = string.Empty;
                    ddlRoomType.ClearSelection();
                }
                ORHEADERVO.RoomType = ddlRoomType.SelectedValue;
                ORHEADERVO.ORRoom = ddlORRoom.SelectedValue;
                ORHEADERVO.AnesthesiaType1 = ddlAnesthesiaType1.SelectedValue;
                ORHEADERVO.AnesthesiaSign = ddlAnesthesiaSign.SelectedValue;
                if (ORHEADERVO.AnesthesiaSign == ((int)EnumOR.AnesthesiaSign.Plus).ToString() || ORHEADERVO.AnesthesiaSign == ((int)EnumOR.AnesthesiaSign.Both).ToString())
                {
                    ORHEADERVO.AnesthesiaType2 = ddlAnesthesiaType2.SelectedValue;
                }
                else
                {
                    ORHEADERVO.AnesthesiaType2 = string.Empty;
                }
                ORHEADERVO.Surgeon1 = ddlSurgeon1.SelectedValue;
                ORHEADERVO.Surgeon2 = ddlSurgeon2.SelectedValue;
                ORHEADERVO.Surgeon3 = ddlSurgeon3.SelectedValue;
                //
                if (rbSurgeon3.Checked)
                {
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                        ORHEADERVO.SurgeonMaster = ORHEADERVO.Surgeon3;
                    else
                        ORHEADERVO.SurgeonMaster = ORHEADERVO.Surgeon1;
                }
                else if (rbSurgeon2.Checked)
                {
                    if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                        ORHEADERVO.SurgeonMaster = ORHEADERVO.Surgeon2;
                    else
                        ORHEADERVO.SurgeonMaster = ORHEADERVO.Surgeon1;
                }
                else
                {
                    ORHEADERVO.SurgeonMaster = ORHEADERVO.Surgeon1;
                }


                List<ORHEADERVO> lstorh = new BLORHEADER(dbInfo).SearchBySurgeon(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);

                if (ORHEADERVO.ORTimeFollow)
                {
                    if (lstorh.Count > 0)
                        ORHEADERVO.ORTime = lstorh[0].ORTime + ":00";
                    else
                        ORHEADERVO.ORTime = "00:00";
                }
                else
                    ORHEADERVO.ORTime = ddlORTimeH.SelectedValue + ":" + ddlORTimeM.SelectedValue;


                ORHEADERVO.AnesthesiaDoctor1 = ddlAnesthesiaDoctor1.SelectedValue;
                ORHEADERVO.AnesthesiaDoctor2 = ddlAnesthesiaDoctor2.SelectedValue;
                ORHEADERVO.AnesthesiaDoctor3 = ddlAnesthesiaDoctor3.SelectedValue;
                ORHEADERVO.AnesthesiaNurse1 = ddlAnesthesiaNurse1.SelectedValue;
                ORHEADERVO.AnesthesiaNurse2 = ddlAnesthesiaNurse2.SelectedValue;
                ORHEADERVO.AnesthesiaNurse3 = ddlAnesthesiaNurse3.SelectedValue;
                ORHEADERVO.Remark = txtRemark.Text;
                ORHEADERVO.Prediag = txtPrediag.Text;
                ORHEADERVO.UpdateBy = Session["USERID"].ToString();

                ORHEADERVO.SuggestByUser = ddlSuggestByUser.SelectedValue;
                ORHEADERVO.RequestByUser = ddlRequestByUser.SelectedValue;

                List<ORHEADERVO> lsORHEADERVOPre = new BLORHEADER(dbInfo).SearchByORID(ORHEADERVO.ORID);

                rtv = new BLORHEADER(dbInfo).Update(ORHEADERVO);
                if (rtv.Value)
                {
                    List<OROPERATIONVO> lstOROPERATIONVOPre = new BLOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                    List<OROPERATIONVO> lstOROPERATIONVONew = new List<OROPERATIONVO>();
                    //new BLOROPERATION(dbInfo).DeleteByORID(hdORID.Value);
                    //int j = 1;
                    //foreach (OROPERATIONVO OROPERATIONVO in GetListValue_gvOROperatoin())
                    //{
                    //    OROPERATIONVO.ID = Guid.NewGuid().ToString();
                    //    OROPERATIONVO.ORID = ORHEADERVO.ORID;
                    //    OROPERATIONVO.Seq = j;
                    //    lstOROPERATIONVONew.Add(OROPERATIONVO);
                    //    new BLOROPERATION(dbInfo).Insert(OROPERATIONVO);
                    //    j++;
                    //}

                    ORLog(lsORHEADERVOPre[0], ORHEADERVO, lstOROPERATIONVOPre, lstOROPERATIONVONew);
                }

                lstorh = new BLORHEADER(dbInfo).SearchBySurgeon(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);
                if (lstorh.Count > 0)
                {
                    int i = 0;
                    List<ORHEADERVO> lstorhTF = new BLORHEADER(dbInfo).SearchBySurgeonTF(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);
                    foreach (ORHEADERVO orh1 in lstorhTF)
                    {
                        APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                        APPOINTMENTVO.AppointmentNo = lstorh[i].AppointmentNo;
                        List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
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
                        if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                        {
                            orh1.ORTime = lstorh[i].ORTime + ":00";
                            ReturnValue rvtf = new BLORHEADER(dbInfo).UpdateTimeTF(orh1);
                        }
                        i = i++;
                    }
                }

            }
            catch (Exception ex)
            {
                AlertMessage(true, true, ex.Message);
            }
            return rtv;
        }

        private void setbtnDisable()
        {
            btnSave.Attributes["disabled"] = "disabled";
            btnClear.Attributes["disabled"] = "disabled";

            btnSave.CssClass = "btn btn-secondary pull-right";
            btnClear.CssClass = "btn btn-secondary pull-right";
        }

        private void setbtnEnable()
        {
            btnSave.Attributes.Remove("disabled");
            btnClear.Attributes.Remove("disabled");

            btnSave.CssClass = "btn btn-success pull-right";
            btnClear.CssClass = "btn btn-info pull-right";
        }

        private void MapDDL()
        {
            try
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

                if (Session["USERNANME"].ToString() != "ADMIN")
                    SETUPORROOMVO.ArCodeType = arRoomType;

                ddlORRoom.DataSource = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                ddlORRoom.DataValueField = "CODE";
                ddlORRoom.DataTextField = "Name";
                ddlORRoom.DataBind();

                ListItem litRoomType = new ListItem();
                litRoomType.Text = "None";
                litRoomType.Value = "";
                ddlRoomType.DataSource = new BLROOMTYPE(dbInfo).SearchAll();
                ddlRoomType.DataValueField = "CODE";
                ddlRoomType.DataTextField = "NAME";
                ddlRoomType.DataBind();
                ddlRoomType.Items.Insert(0, litRoomType);

                ListItem litSurgeon1 = new ListItem();
                litSurgeon1.Text = "None";
                litSurgeon1.Value = "";
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                //List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(DOCTORMASTERVO);
                ddlSurgeon1.DataSource = lstDOCTORMASTERVO;
                ddlSurgeon1.DataValueField = "DOCTOR";
                ddlSurgeon1.DataTextField = "DoctorName";
                ddlSurgeon1.DataBind();
                ddlSurgeon1.Items.Insert(0, litSurgeon1);

                ListItem litSurgeon2 = new ListItem();
                litSurgeon2.Text = "None";
                litSurgeon2.Value = "";
                ddlSurgeon2.DataSource = lstDOCTORMASTERVO;
                ddlSurgeon2.DataValueField = "DOCTOR";
                ddlSurgeon2.DataTextField = "DoctorName";
                ddlSurgeon2.DataBind();
                ddlSurgeon2.Items.Insert(0, litSurgeon2);

                ListItem litSurgeon3 = new ListItem();
                litSurgeon3.Text = "None";
                litSurgeon3.Value = "";
                ddlSurgeon3.DataSource = lstDOCTORMASTERVO;
                ddlSurgeon3.DataValueField = "DOCTOR";
                ddlSurgeon3.DataTextField = "DoctorName";
                ddlSurgeon3.DataBind();
                ddlSurgeon3.Items.Insert(0, litSurgeon3);

                DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
                //lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(DOCTORMASTERVO);
                ListItem litAnesthesiaDoctor1 = new ListItem();
                litAnesthesiaDoctor1.Text = "None";
                litAnesthesiaDoctor1.Value = "";
                ddlAnesthesiaDoctor1.DataSource = lstDOCTORMASTERVO;
                ddlAnesthesiaDoctor1.DataValueField = "DOCTOR";
                ddlAnesthesiaDoctor1.DataTextField = "DoctorName";
                ddlAnesthesiaDoctor1.DataBind();
                ddlAnesthesiaDoctor1.Items.Insert(0, litAnesthesiaDoctor1);

                ListItem litAnesthesiaDoctor2 = new ListItem();
                litAnesthesiaDoctor2.Text = "None";
                litAnesthesiaDoctor2.Value = "";
                ddlAnesthesiaDoctor2.DataSource = lstDOCTORMASTERVO;
                ddlAnesthesiaDoctor2.DataValueField = "DOCTOR";
                ddlAnesthesiaDoctor2.DataTextField = "DoctorName";
                ddlAnesthesiaDoctor2.DataBind();
                ddlAnesthesiaDoctor2.Items.Insert(0, litAnesthesiaDoctor2);

                ListItem litAnesthesiaDoctor3 = new ListItem();
                litAnesthesiaDoctor3.Text = "None";
                litAnesthesiaDoctor3.Value = "";
                ddlAnesthesiaDoctor3.DataSource = lstDOCTORMASTERVO;
                ddlAnesthesiaDoctor3.DataValueField = "DOCTOR";
                ddlAnesthesiaDoctor3.DataTextField = "DoctorName";
                ddlAnesthesiaDoctor3.DataBind();
                ddlAnesthesiaDoctor3.Items.Insert(0, litAnesthesiaDoctor3);

                ListItem litAnesthesiaNurs1 = new ListItem();
                litAnesthesiaNurs1.Text = "None";
                litAnesthesiaNurs1.Value = "";
                NURSEMASTERVO NURSEMASTERVO = new NURSEMASTERVO();
                NURSEMASTERVO.EDUCATIONSTANDARD = "AN";
                List<NURSEMASTERVO> lstNURSEMASTERVO = new BLNURSEMASTER(dbInfo).SearchByKey(NURSEMASTERVO);
                ddlAnesthesiaNurse1.DataSource = lstNURSEMASTERVO;
                ddlAnesthesiaNurse1.DataValueField = "CODE";
                ddlAnesthesiaNurse1.DataTextField = "NAME";
                ddlAnesthesiaNurse1.DataBind();
                ddlAnesthesiaNurse1.Items.Insert(0, litAnesthesiaNurs1);

                ListItem litAnesthesiaNurs2 = new ListItem();
                litAnesthesiaNurs2.Text = "None";
                litAnesthesiaNurs2.Value = "";
                ddlAnesthesiaNurse2.DataSource = lstNURSEMASTERVO;
                ddlAnesthesiaNurse2.DataValueField = "CODE";
                ddlAnesthesiaNurse2.DataTextField = "NAME";
                ddlAnesthesiaNurse2.DataBind();
                ddlAnesthesiaNurse2.Items.Insert(0, litAnesthesiaNurs2);

                ListItem litAnesthesiaNurs3 = new ListItem();
                litAnesthesiaNurs3.Text = "None";
                litAnesthesiaNurs2.Value = "";
                ddlAnesthesiaNurse3.DataSource = lstNURSEMASTERVO;
                ddlAnesthesiaNurse3.DataValueField = "CODE";
                ddlAnesthesiaNurse3.DataTextField = "NAME";
                ddlAnesthesiaNurse3.DataBind();
                ddlAnesthesiaNurse3.Items.Insert(0, litAnesthesiaNurs3);

                ListItem litAnesthesiaType1 = new ListItem();
                litAnesthesiaType1.Text = "None";
                litAnesthesiaType1.Value = "";
                List<ANESTHESIAVO> lstANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchAll();
                ddlAnesthesiaType1.DataSource = lstANESTHESIAVO;
                ddlAnesthesiaType1.DataValueField = "CODE";
                ddlAnesthesiaType1.DataTextField = "NAME";
                ddlAnesthesiaType1.DataBind();
                ddlAnesthesiaType1.Items.Insert(0, litAnesthesiaType1);

                ListItem litAnesthesiaType2 = new ListItem();
                litAnesthesiaType2.Text = "None";
                litAnesthesiaType2.Value = "";
                ddlAnesthesiaType2.DataSource = lstANESTHESIAVO;
                ddlAnesthesiaType2.DataValueField = "CODE";
                ddlAnesthesiaType2.DataTextField = "NAME";
                ddlAnesthesiaType2.DataBind();
                ddlAnesthesiaType2.Items.Insert(0, litAnesthesiaType2);

                // OR Status
                ddlORStatus.DataSource = EnumOR.GetAllName<EnumOR.ORStatus>();
                ddlORStatus.DataTextField = "Value";
                ddlORStatus.DataValueField = "Key";
                ddlORStatus.DataBind();

                // OR SpecificType
                ddlORSpecificType.DataSource = EnumOR.GetAllName<EnumOR.ORSpecificType>();
                ddlORSpecificType.DataTextField = "Value";
                ddlORSpecificType.DataValueField = "Key";
                ddlORSpecificType.DataBind();

                // AdmitTimeType
                ddlAdmitTimeType.DataSource = EnumOR.GetAllName<EnumOR.AdmitTimeType>();
                ddlAdmitTimeType.DataTextField = "Value";
                ddlAdmitTimeType.DataValueField = "Key";
                ddlAdmitTimeType.DataBind();

                // ORSide
                ddlORSide.DataSource = EnumOR.GetAllName<EnumOR.ORSide>();
                ddlORSide.DataTextField = "Value";
                ddlORSide.DataValueField = "Key";
                ddlORSide.DataBind();

                ddlORSide.SelectedValue = ((byte)EnumOR.ORSide.ยังไม่ระบุตา).ToString();

                // AnesthesiaSign
                ddlAnesthesiaSign.DataSource = EnumOR.GetAnesthesiaSign<EnumOR.AnesthesiaSign>();
                ddlAnesthesiaSign.DataTextField = "Value";
                ddlAnesthesiaSign.DataValueField = "Key";
                ddlAnesthesiaSign.DataBind();

                for (int i = 0; i < 24; i++)
                {
                    ListItem litORTimeH = new ListItem();
                    litORTimeH.Text = i.ToString().PadLeft(2, '0');
                    litORTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlORTimeH.Items.Add(litORTimeH);

                    ListItem litArrivalTimeH = new ListItem();
                    litArrivalTimeH.Text = i.ToString().PadLeft(2, '0');
                    litArrivalTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlArrivalTimeH.Items.Add(litArrivalTimeH);
                }

                for (int i = 0; i < 60; i++)
                {
                    ListItem litORTimeM = new ListItem();
                    litORTimeM.Text = i.ToString().PadLeft(2, '0');
                    litORTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlORTimeM.Items.Add(litORTimeM);

                    ListItem litArrivalTimeM = new ListItem();
                    litArrivalTimeM.Text = i.ToString().PadLeft(2, '0');
                    litArrivalTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlArrivalTimeM.Items.Add(litArrivalTimeM);
                }

                ListItem litSuggestByUser = new ListItem();
                litSuggestByUser.Text = "";
                litSuggestByUser.Value = "";
                List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(new SETUPLOGONVO());
                ddlSuggestByUser.DataSource = lstSETUPLOGONVO;
                ddlSuggestByUser.DataValueField = "UserID";
                ddlSuggestByUser.DataTextField = "Name";
                ddlSuggestByUser.DataBind();
                ddlSuggestByUser.Items.Insert(0, litSuggestByUser);

                ListItem litRequestByUser = new ListItem();
                litRequestByUser.Text = "";
                litRequestByUser.Value = "";
                lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(new SETUPLOGONVO());
                ddlRequestByUser.DataSource = lstSETUPLOGONVO;
                ddlRequestByUser.DataValueField = "UserID";
                ddlRequestByUser.DataTextField = "Name";
                ddlRequestByUser.DataBind();
                ddlRequestByUser.Items.Insert(0, litRequestByUser);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ORPATIENTVO> getORPatient(string strSearch)
        {
            List<ORPATIENTVO> lstOR = new List<ORPATIENTVO>();
            lstOR = new BLORPATIENT(dbInfo).SearchByKey(strSearch);

            return lstOR;
        }

        private void loadgvmain()
        {
            List<SETUPOPERATIONMAINVO> lstSETUPOPERATIONMAINVO = new BLSETUPOPERATIONMAIN(dbInfo).SearchAll();

            gvOROperationMain.DataSource = lstSETUPOPERATIONMAINVO;
            gvOROperationMain.DataBind();
        }

        private void loadgvsub(SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO)
        {
            List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(SETUPOPERATIONSUBVO);
            if (lstSETUPOPERATIONSUBVO.Count > 0)
            {
                hdMainCode.Value = lstSETUPOPERATIONSUBVO[0].MainCode;
                hdMainName.Value = lstSETUPOPERATIONSUBVO[0].Name;
            }
            gvOROperationSub.DataSource = lstSETUPOPERATIONSUBVO;
            gvOROperationSub.DataBind();
        }

        private void loadgvsubEmpty()
        {
            gvOROperationSub.DataSource = new List<SETUPOPERATIONSUBVO>();
            gvOROperationSub.DataBind();
        }

        private void loadgvOROperation(OROPERATIONVO OROPERATIONVO)
        {
            List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByKey(OROPERATIONVO);

            gvOROperation.DataSource = lstOROPERATIONVO;
            gvOROperation.DataBind();

        }

        private void loadgvOROperationEmpty()
        {
            gvOROperation.DataSource = new List<OROPERATIONVO>();
            gvOROperation.DataBind();
        }

        private void LoadlblProcedure()
        {
            lblProcedureRE.Text = string.Empty;
            lblProcedureLE.Text = string.Empty;
            lblProcedureBE.Text = string.Empty;
            foreach (GridViewRow grow in gvOROperation.Rows)
            {
                int Side = int.Parse((grow.FindControl("hdgvSide") as HiddenField).Value);
                string strSubMark = (grow.FindControl("lblgvstrSubMark") as Label).Text;
                string SubName = (grow.FindControl("lblgvSubName") as Label).Text;

                if (Side == (int)EnumOR.ORSide.RE)
                {
                    string submark = strSubMark;
                    if (string.IsNullOrEmpty(lblProcedureRE.Text))
                    {
                        lblProcedureRE.Text = "<code>" + EnumOR.ORSide.Parse(typeof(EnumOR.ORSide), Side.ToString()) + "</code> :";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strSubMark))
                        {
                            submark = " ";
                        }
                    }
                    lblProcedureRE.Text += " " + submark + SubName;
                }
                else if (Side == (int)EnumOR.ORSide.LE)
                {
                    string submark = strSubMark;
                    if (string.IsNullOrEmpty(lblProcedureLE.Text))
                    {
                        lblProcedureLE.Text = "<code>" + EnumOR.ORSide.Parse(typeof(EnumOR.ORSide), Side.ToString()) + "</code> :";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strSubMark))
                        {
                            submark = " ";
                        }
                    }
                    lblProcedureLE.Text += " " + submark + SubName;
                }
                else if (Side == (int)EnumOR.ORSide.BE)
                {
                    string submark = strSubMark;
                    if (string.IsNullOrEmpty(lblProcedureBE.Text))
                    {
                        lblProcedureBE.Text = "<code>" + EnumOR.ORSide.Parse(typeof(EnumOR.ORSide), Side.ToString()) + "</code> :";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strSubMark))
                        {
                            submark = " ";
                        }
                    }
                    lblProcedureBE.Text += " " + submark + SubName;
                }
                else if (Side == (int)EnumOR.ORSide.ยังไม่ระบุตา)
                {
                    string submark = strSubMark;
                    if (string.IsNullOrEmpty(lblProcedureUnknown.Text))
                    {
                        lblProcedureUnknown.Text = "<code>" + EnumOR.ORSide.Parse(typeof(EnumOR.ORSide), Side.ToString()) + "</code> :";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strSubMark))
                        {
                            submark = " ";
                        }
                    }
                    lblProcedureUnknown.Text += " " + submark + SubName;
                }
                else if (Side == (int)EnumOR.ORSide.None)
                {
                    string submark = strSubMark;
                    if (string.IsNullOrEmpty(lblProcedureNone.Text))
                    {
                        lblProcedureNone.Text = "<code>" + EnumOR.ORSide.Parse(typeof(EnumOR.ORSide), Side.ToString()) + "</code> :";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(strSubMark))
                        {
                            submark = " ";
                        }
                    }
                    lblProcedureNone.Text += " " + submark + SubName;
                }
            }
        }

        protected void gvOROperationMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvOROperationMain, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void gvOROperationMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            foreach (GridViewRow r in gvOROperationMain.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
            GridViewRow row = gvOROperationMain.Rows[e.NewEditIndex];
            row.BackColor = System.Drawing.Color.LightPink;
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = (row.FindControl("hdgvMainCode") as HiddenField).Value;
            loadgvsub(SETUPOPERATIONSUBVO);
        }

        protected void gvOROperationSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                if (ddlORSide.SelectedValue == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalSide();", true);
                    ddlORSide.Focus();
                    return;
                }
                List<OROPERATIONVO> _lstor = GetListValue_gvOROperatoin();

                int rowIndex = Convert.ToInt32(e.CommandArgument);

                OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                if (rowIndex > -1)
                {
                    GridViewRow row = gvOROperationSub.Rows[rowIndex];
                    OROPERATIONVO = GetValue_gvOROperatoinSub(row);
                }
                else
                {
                    OROPERATIONVO = GetValue_gvOROperatoinSubFt();
                    (gvOROperationSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text = string.Empty;
                }

                OROPERATIONVO.ID = Guid.NewGuid().ToString();
                OROPERATIONVO.ORID = hdORID.Value;
                OROPERATIONVO.Seq = gvOROperation.Rows.Count + 1;

                new BLOROPERATION(dbInfo).Insert(OROPERATIONVO);
                Log("Add OROperation: " + OROPERATIONVO.Name + "(" + OROPERATIONVO.SubName + ")");
                _lstor.Add(OROPERATIONVO);
                gvOROperation.DataSource = _lstor;
                gvOROperation.DataBind();
                LoadlblProcedure();

            }
        }

        protected void gvOROperation_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "implant")
                {
                    int irow = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvOROperation.Rows[irow];
                    hdPostOROperationID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                    loadgvImplantMain();
                    loadgvImplantSubEmpty();
                    loadImplant(hdPostOROperationID.Value);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalImplant();", true);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvOROperation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {

                GridViewRow row = gvOROperation.Rows[e.RowIndex];
                OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                OROPERATIONVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                OROPERATIONVO.Name = ((Label)row.FindControl("lblgvName")).Text;
                OROPERATIONVO.SubName = ((Label)row.FindControl("lblgvSubName")).Text; 
                new BLOROPERATION(dbInfo).DeleteByID(OROPERATIONVO);
                Log("Delete OROperation: " + OROPERATIONVO.Name + "(" + OROPERATIONVO.SubName + ")");
                List<OROPERATIONVO> _lstor = GetListValue_gvOROperatoin();
                _lstor.RemoveAt(e.RowIndex);
                gvOROperation.DataSource = _lstor;
                gvOROperation.DataBind();
                LoadlblProcedure();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvImplantMain_RowEditing(object sender, GridViewEditEventArgs e)
        {
            foreach (GridViewRow r in gvImplantMain.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
            //loadgvmain();
            GridViewRow row = gvImplantMain.Rows[e.NewEditIndex];
            row.BackColor = System.Drawing.Color.LightPink;
            SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO = new SETUPIMPLANTSUBVO();
            SETUPIMPLANTSUBVO.MainCode = (row.FindControl("hdgvMainCode") as HiddenField).Value;
            loadgvImplantSub(SETUPIMPLANTSUBVO);
        }

        protected void gvImplantMain_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvImplantMain, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void gvImplantSub_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                List<POSTORIMPLANTVO> _lstor = GetListValue_gvImplant();

                int rowIndex = Convert.ToInt32(e.CommandArgument);

                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                if (rowIndex > -1)
                {
                    GridViewRow row = gvImplantSub.Rows[rowIndex];
                    POSTORIMPLANTVO = GetValue_gvImplantSub(row);
                }
                else
                {
                    POSTORIMPLANTVO = GetValue_gvImplantSubFt();
                    (gvImplantSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text = string.Empty;
                }

                POSTORIMPLANTVO.ID = Guid.NewGuid().ToString();
                POSTORIMPLANTVO.PostOperation_ID = hdPostOROperationID.Value;
                POSTORIMPLANTVO.Seq = gvImplant.Rows.Count + 1;

                new BLPOSTORIMPLANT(dbInfo).Insert(POSTORIMPLANTVO);

                Log("Add Implant: " + POSTORIMPLANTVO.Name + "(" + POSTORIMPLANTVO.SubName + ")");

                _lstor.Add(POSTORIMPLANTVO);
                gvImplant.DataSource = _lstor;
                gvImplant.DataBind();
                //SetButtonUpDown();

                //LoadlblProcedure();
            }
        }

        protected void gvImplant_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = gvImplant.Rows[e.RowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                new BLPOSTORIMPLANT(dbInfo).Delete(POSTORIMPLANTVO.ID);
                Log("Delete Implant: " + POSTORIMPLANTVO.Name + "(" + POSTORIMPLANTVO.SubName + ")");
                List<POSTORIMPLANTVO> _lstor = GetListValue_gvImplant();
                _lstor.RemoveAt(e.RowIndex);
                gvImplant.DataSource = _lstor;
                gvImplant.DataBind();
                //SetButtonUpDown();
                //LoadlblProcedure();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvImplant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "remark")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvImplant.Rows[rowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                POSTORIMPLANTVO.Remark = ((TextBox)row.FindControl("txtRemark")).Text;
                ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).UpdateRemark(POSTORIMPLANTVO);
            }
        }

        private void loadgvImplantMain()
        {
            List<SETUPIMPLANTMAINVO> lstSETUPIMPLANTMAINVO = new BLSETUPIMPLANTMAIN(dbInfo).SearchAll();

            gvImplantMain.DataSource = lstSETUPIMPLANTMAINVO;
            gvImplantMain.DataBind();
        }

        private void loadgvImplantSub(SETUPIMPLANTSUBVO SETUPIMPLANTSUBVO)
        {
            List<SETUPIMPLANTSUBVO> lstSETUPIMPLANTSUBVO = new BLSETUPIMPLANTSUB(dbInfo).SearchByKey(SETUPIMPLANTSUBVO);
            if (lstSETUPIMPLANTSUBVO.Count > 0)
            {
                hdImplantMainCode.Value = lstSETUPIMPLANTSUBVO[0].MainCode;
                hdImplantMainName.Value = lstSETUPIMPLANTSUBVO[0].Name;
            }
            gvImplantSub.DataSource = lstSETUPIMPLANTSUBVO;
            gvImplantSub.DataBind();
        }

        private POSTORIMPLANTVO GetValue_gvImplantSub(GridViewRow row)
        {
            POSTORIMPLANTVO _or = new POSTORIMPLANTVO();
            _or.MainCode = (row.FindControl("hdgvMainCode") as HiddenField).Value;
            _or.SubCode = (row.FindControl("hdgvSubCode") as HiddenField).Value;
            _or.Name = (row.FindControl("hdgvName") as HiddenField).Value;
            _or.SubName = (row.FindControl("lblgvSubName") as Label).Text;
            return _or;
        }

        private List<POSTORIMPLANTVO> GetListValue_gvImplant()
        {
            List<POSTORIMPLANTVO> _lstor = new List<POSTORIMPLANTVO>();
            int i = 1;
            foreach (GridViewRow drow in gvImplant.Rows)
            {
                POSTORIMPLANTVO _or = new POSTORIMPLANTVO();

                _or.ID = (drow.FindControl("hdgvID") as HiddenField).Value;
                _or.PostOperation_ID = hdPostOROperationID.Value;
                _or.Seq = i;
                _or.MainCode = (drow.FindControl("hdgvMainCode") as HiddenField).Value;
                _or.SubCode = (drow.FindControl("hdgvSubCode") as HiddenField).Value;
                _or.Name = (drow.FindControl("lblgvName") as Label).Text;
                _or.SubName = (drow.FindControl("lblgvSubName") as Label).Text;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;
        }

        private POSTORIMPLANTVO GetValue_gvImplantSubFt()
        {
            POSTORIMPLANTVO _or = new POSTORIMPLANTVO();
            _or.MainCode = hdImplantMainCode.Value;
            _or.Name = hdImplantMainName.Value;
            _or.SubName = (gvImplantSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text;
            return _or;
        }

        private void loadgvImplantSubEmpty()
        {
            gvImplantSub.DataSource = new List<SETUPIMPLANTSUBVO>();
            gvImplantSub.DataBind();
        }

        private void loadgvImplantEmpty()
        {
            gvImplant.DataSource = new List<OROPERATIONVO>();
            gvImplant.DataBind();
        }

        public void loadImplant(string opid)
        {
            POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
            POSTORIMPLANTVO.PostOperation_ID = opid;
            List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
            if (lstPOSTORIMPLANTVO.Count > 0)
            {
                gvImplant.DataSource = lstPOSTORIMPLANTVO;
                gvImplant.DataBind();
            }
            else
            {
                loadgvImplantEmpty();
            }
            //LoadlblProcedure();
        }

        private OROPERATIONVO GetValue_gvOROperatoinSubFt()
        {
            OROPERATIONVO _or = new OROPERATIONVO();
            _or.MainCode = hdMainCode.Value;
            _or.Name = hdMainName.Value;
            _or.SubName = (gvOROperationSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text;
            _or.SubMark = "0";
            _or.Side = int.Parse(ddlORSide.SelectedValue);
            _or.strSide = ((EnumOR.ORSide)_or.Side).ToString();

            return _or;
        }

        private OROPERATIONVO GetValue_gvOROperatoinSub(GridViewRow row)
        {
            OROPERATIONVO _or = new OROPERATIONVO();
            _or.MainCode = (row.FindControl("hdgvMainCode") as HiddenField).Value;
            _or.SubCode = (row.FindControl("hdgvSubCode") as HiddenField).Value;
            _or.Side = int.Parse(ddlORSide.SelectedValue);
            _or.strSide = ((EnumOR.ORSide)_or.Side).ToString();
            _or.SubMark = (row.FindControl("hdgvddlSubMask") as DropDownList).SelectedValue;
            _or.strSubMark = (row.FindControl("hdgvddlSubMask") as DropDownList).SelectedItem.Text;
            _or.Name = (row.FindControl("hdgvName") as HiddenField).Value;
            _or.SubName = (row.FindControl("lblgvSubName") as Label).Text;
            return _or;
        }

        private List<OROPERATIONVO> GetListValue_gvOROperatoin()
        {
            List<OROPERATIONVO> _lstor = new List<OROPERATIONVO>();
            int i = 1;
            foreach (GridViewRow drow in gvOROperation.Rows)
            {
                OROPERATIONVO _or = new OROPERATIONVO();

                _or.ID = (drow.FindControl("hdgvID") as HiddenField).Value;
                _or.ORID = hdORID.Value;
                _or.Seq = i;
                _or.MainCode = (drow.FindControl("hdgvMainCode") as HiddenField).Value;
                _or.SubCode = (drow.FindControl("hdgvSubCode") as HiddenField).Value;
                _or.Side = int.Parse((drow.FindControl("hdgvSide") as HiddenField).Value);
                _or.strSide = ((EnumOR.ORSide)_or.Side).ToString();
                _or.SubMark = (drow.FindControl("hdgvSubMark") as HiddenField).Value;
                _or.strSubMark = (drow.FindControl("lblgvstrSubMark") as Label).Text;
                _or.Name = (drow.FindControl("lblgvName") as Label).Text;
                _or.SubName = (drow.FindControl("lblgvSubName") as Label).Text;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;

        }

        private void AlertMessage(bool hidmsg, bool iserror, string msg)
        {
            if (iserror)
            {
                divError.Attributes["class"] = "alert alert-warning alert - dismissible fade show";
            }
            else
            {
                divError.Attributes["class"] = "alert alert-success alert - dismissible fade show";
            }
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Reserve/Cancel/?d=" + hdORID.Value, false);
        }

        private void ORLog(ORHEADERVO ORHEADERVOPrv, ORHEADERVO ORHEADERVO, List<OROPERATIONVO> lstOROPre, List<OROPERATIONVO> lstORONew)
        {
            string log = string.Empty;
            if (ORHEADERVO.PatientInfection != ORHEADERVOPrv.PatientInfection)
            {
                string curent = ORHEADERVO.PatientInfection == true ? "Check" : "Not Check";
                string prv = ORHEADERVOPrv.PatientInfection == true ? "Check" : "Not Check";
                log += "Infection [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.PatientType1 != ORHEADERVOPrv.PatientType1)
            {
                string curent = ORHEADERVO.PatientType1 == true ? "Check" : "Not Check";
                string prv = ORHEADERVOPrv.PatientType1 == true ? "Check" : "Not Check";
                log += "Patient Type 1(**) [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.PatientType2 != ORHEADERVOPrv.PatientType2)
            {
                string curent = ORHEADERVO.PatientType2 == true ? "Check" : "Not Check";
                string prv = ORHEADERVOPrv.PatientType2 == true ? "Check" : "Not Check";
                log += "Patient Type 2(***) [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.PatientUP != ORHEADERVOPrv.PatientUP)
            {
                string curent = ORHEADERVO.PatientUP == true ? "Check" : "Not Check";
                string prv = ORHEADERVOPrv.PatientUP == true ? "Check" : "Not Check";
                log += "PatientUP [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.ORDate.Value != ORHEADERVOPrv.ORDate.Value)
            {
                log += " OR Date [" + ORHEADERVOPrv.ORDate.Value.ToString("d MMM yyyy") + " => " + ORHEADERVO.ORDate.Value.ToString("d MMM yyyy") + "]<br/>";
            }
            if (ORHEADERVO.ORCase != ORHEADERVOPrv.ORCase)
            {
                log += " OR Case [" + ORHEADERVOPrv.ORCase + " => " + ORHEADERVO.ORCase + "]<br/>";
            }
            if (ORHEADERVO.ORTimeFollow)
            {
                ORHEADERVO.ORTime = "TF";
            }
            if (ORHEADERVO.ORTime != ORHEADERVOPrv.ORTime)
            {
                log += "OR Time [" + ORHEADERVOPrv.ORTime + " => " + ORHEADERVO.ORTime + "]<br/>";
            }
            if (ORHEADERVO.ORStatCase != ORHEADERVOPrv.ORStatCase)
            {
                string curent = ORHEADERVO.ORStatCase == true ? "Check" : "Not Check";
                string prv = ORHEADERVOPrv.ORStatCase == true ? "Check" : "Not Check";
                log += "Stat Case [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.ArrivalTime != ORHEADERVOPrv.ArrivalTime)
            {
                log += " Arrival [" + ORHEADERVOPrv.ArrivalTime + " => " + ORHEADERVO.ArrivalTime + "]<br/>";
            }
            if (ORHEADERVO.ORSpecificType != ORHEADERVOPrv.ORSpecificType)
            {
                string curent = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVO.ORSpecificType)).ToString();
                string prv = ((EnumOR.ORSpecificType)int.Parse(ORHEADERVOPrv.ORSpecificType)).ToString();
                log += " Specific Type [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.ORStatus != ORHEADERVOPrv.ORStatus)
            {
                string curent = ((EnumOR.ORStatus)int.Parse(ORHEADERVO.ORStatus)).ToString();
                string prv = ((EnumOR.ORStatus)int.Parse(ORHEADERVOPrv.ORStatus)).ToString();
                log += " Status [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AdmitTimeType == null)
            {
                ORHEADERVO.AdmitTimeType = "";
            }
            if (ORHEADERVOPrv.AdmitTimeType == null)
            {
                ORHEADERVOPrv.AdmitTimeType = "";
            }
            if (ORHEADERVO.AdmitTimeType != ORHEADERVOPrv.AdmitTimeType)
            {
                string curent = string.Empty;
                string prv = string.Empty;
                if (!string.IsNullOrEmpty(ORHEADERVO.AdmitTimeType))
                    curent = ((EnumOR.AdmitTimeType)int.Parse(ORHEADERVO.AdmitTimeType)).ToString();
                if (!string.IsNullOrEmpty(ORHEADERVOPrv.AdmitTimeType))
                    prv = ((EnumOR.AdmitTimeType)int.Parse(ORHEADERVOPrv.AdmitTimeType)).ToString();
                log += " AdmitTimeType [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.ORRoom != ORHEADERVOPrv.ORRoom)
            {
                string curent = string.Empty;
                string prv = string.Empty;
                
                try {
                    SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.CODE = ORHEADERVO.ORRoom;
                    curent = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO)[0].Name;

                    SETUPORROOMVO = new SETUPORROOMVO();
                    SETUPORROOMVO.CODE = ORHEADERVOPrv.ORRoom;
                    prv = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO)[0].Name;
                } catch { }

                log += " OR Room  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaType1 == null)
            {
                ORHEADERVO.AnesthesiaType1 = "";
            }
            if (ORHEADERVOPrv.AnesthesiaType1 == null)
            {
                ORHEADERVOPrv.AnesthesiaType1 = "";
            }
            if (ORHEADERVO.AnesthesiaType1 != ORHEADERVOPrv.AnesthesiaType1)
            {
                string curent = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType1).NAME;
                string prv = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaType1).NAME;
                log += " Anesthesia Type 1 [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaType2 == null)
            {
                ORHEADERVO.AnesthesiaType2 = "";
            }
            if (ORHEADERVOPrv.AnesthesiaType2 == null)
            {
                ORHEADERVOPrv.AnesthesiaType2 = "";
            }
            if (ORHEADERVO.AnesthesiaType2 != ORHEADERVOPrv.AnesthesiaType2)
            {
                string curent = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType2).NAME;
                string prv = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaType2).NAME;
                log += " Anesthesia Type 2  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.Remark != ORHEADERVOPrv.Remark)
            {
                log += " Remarks  [" + ORHEADERVOPrv.Remark + " => " + ORHEADERVO.Remark + "]<br/>";
            }
            if (ORHEADERVO.Surgeon1 != ORHEADERVOPrv.Surgeon1)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon1).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.Surgeon1).DoctorName;
                log += " Surgeon (1)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.Surgeon2 != ORHEADERVOPrv.Surgeon2)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon2).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.Surgeon2).DoctorName;
                log += " Surgeon (2)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.Surgeon3 != ORHEADERVOPrv.Surgeon3)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon3).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.Surgeon3).DoctorName;
                log += " Surgeon (3)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaDoctor1 != ORHEADERVOPrv.AnesthesiaDoctor1)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaDoctor1).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaDoctor1).DoctorName;
                log += " Anes Doctor(1)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaDoctor2 != ORHEADERVOPrv.AnesthesiaDoctor2)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaDoctor2).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaDoctor2).DoctorName;
                log += " Anes Doctor(2)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaDoctor3 != ORHEADERVOPrv.AnesthesiaDoctor3)
            {
                string curent = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaDoctor3).DoctorName;
                string prv = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaDoctor3).DoctorName;
                log += " Anes Doctor(3)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaNurse1 != ORHEADERVOPrv.AnesthesiaNurse1)
            {
                string curent = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaNurse1).NAME;
                string prv = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaNurse1).NAME;
                log += " Anes Nurse(1)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaNurse2 != ORHEADERVOPrv.AnesthesiaNurse2)
            {
                string curent = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaNurse2).NAME;
                string prv = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaNurse2).NAME;
                log += " Anes Nurse(2)  [" + prv + " => " + curent + "]<br/>";
            }
            if (ORHEADERVO.AnesthesiaNurse3 != ORHEADERVOPrv.AnesthesiaNurse3)
            {
                string curent = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaNurse3).NAME;
                string prv = new BLNURSEMASTER(dbInfo).SearchByCode(ORHEADERVOPrv.AnesthesiaNurse3).NAME;
                log += " Anes Nurse(3)  [" + prv + " => " + curent + "]<br/>";
            }

            //string log_oro = string.Empty;

            //if (lstORONew.Count >= lstOROPre.Count)
            //{
            //    for (int i = 0; i < lstORONew.Count; i++)
            //    {
            //        if (lstOROPre.Count > i)
            //        {
            //            if (lstORONew[i].Side != lstOROPre[i].Side ||
            //                lstORONew[i].MainCode != lstOROPre[i].MainCode ||
            //                lstORONew[i].SubCode != lstOROPre[i].SubCode)
            //            {
            //                log_oro += "ลำดับ " + lstOROPre[i].Seq + " [";
            //                if (lstORONew[i].Side != lstOROPre[i].Side)
            //                    log_oro += "Side : " + lstOROPre[i].strSide + " => " + lstORONew[i].strSide + ",";
            //                if (lstORONew[i].MainCode != lstOROPre[i].MainCode)
            //                    log_oro += "Type Of Operation : " + lstOROPre[i].Name + " => " + lstORONew[i].Name + ",";
            //                if (lstORONew[i].SubCode != lstOROPre[i].SubCode)
            //                    log_oro += "Procedure : " + lstOROPre[i].SubName + " => " + lstORONew[i].SubName + ",";
            //                log_oro += "]<br/>";
            //            }
            //        }
            //        else
            //        {
            //            log_oro += "ลำดับ " + lstORONew[i].Seq + " [ Side : '' => " + lstORONew[i].strSide + ",";
            //            log_oro += "Type Of Operation : '' => " + lstORONew[i].Name + ",";
            //            log_oro += "Procedure : '' => " + lstORONew[i].SubName;
            //            log_oro += "]<br/>";
            //        }
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < lstOROPre.Count; i++)
            //    {
            //        if (lstORONew.Count > i)
            //        {
            //            if (lstORONew[i].Side != lstOROPre[i].Side ||
            //                lstORONew[i].MainCode != lstOROPre[i].MainCode ||
            //                lstORONew[i].SubCode != lstOROPre[i].SubCode)
            //            {
            //                log_oro += "ลำดับ " + lstOROPre[i].Seq + " [";
            //                if (lstORONew[i].Side != lstOROPre[i].Side)
            //                    log_oro += "Side : " + lstOROPre[i].strSide + " => " + lstORONew[i].strSide + ",";
            //                if (lstORONew[i].MainCode != lstOROPre[i].MainCode)
            //                    log_oro += "Type Of Operation : " + lstOROPre[i].Name + " => " + lstORONew[i].Name + ",";
            //                if (lstORONew[i].SubCode != lstOROPre[i].SubCode)
            //                    log_oro += "Procedure : " + lstOROPre[i].SubName + " => " + lstORONew[i].SubName + ",";
            //                log_oro += "]<br/>";
            //            }
            //        }
            //        else
            //        {
            //            log_oro += "ลำดับ " + lstOROPre[i].Seq + " [ Side : " + lstOROPre[i].strSide + " => '', ";
            //            log_oro += "Type Of Operation : " + lstOROPre[i].Name + "  => '', ";
            //            log_oro += "Procedure : " + lstOROPre[i].SubName + " => ''";
            //            log_oro += "]<br/>";
            //        }
            //    }
            //}

            //log += log_oro;
            if (!string.IsNullOrEmpty(log))
            {
                ORLogVO orlog = new ORLogVO();
                orlog.ORID = ORHEADERVO.ORID;
                orlog.HN = ORHEADERVO.HN;
                orlog.PatientName = ORHEADERVO.PatientName;
                orlog.Detail = log;
                orlog.UpdateBy = Session["USERID"].ToString();
                ReturnValue rv = new BLORLog(dbInfo).Insert(orlog);
                if (rv.Value)
                {
                    //Happy
                }
            }
        }

        private void Log(string log)
        {
            //string log = string.Empty;
            //if (!string.IsNullOrEmpty(log))
            //{
            ORLogVO orlog = new ORLogVO();
            orlog.ORID = hdORID.Value;
            orlog.HN = txtHN.Text;
            orlog.PatientName = lblPatientName.Text;
            orlog.Detail = log;
            orlog.UpdateBy = Session["USERID"].ToString();
            ReturnValue rv = new BLORLog(dbInfo).Insert(orlog);
            if (rv.Value)
            {
                //Happy
            }
            //}
        }

        protected void txtSearchRequestByUser_TextChanged(object sender, EventArgs e)
        {
            SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
            _SETUPLOGONVO.Username = txtSearchRequestByUser.Text;
            _SETUPLOGONVO.FirstName = txtSearchRequestByUser.Text;
            _SETUPLOGONVO.LastName = txtSearchRequestByUser.Text;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchDDL(_SETUPLOGONVO);
            ddlRequestByUser.DataSource = lstSETUPLOGONVO;
            ddlRequestByUser.DataTextField = "Name";
            ddlRequestByUser.DataValueField = "UserID";
            ddlRequestByUser.DataBind();
        }

        protected void txtSearchSuggestByUser_TextChanged(object sender, EventArgs e)
        {
            SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
            _SETUPLOGONVO.Username = txtSearchSuggestByUser.Text;
            _SETUPLOGONVO.FirstName = txtSearchSuggestByUser.Text;
            _SETUPLOGONVO.LastName = txtSearchSuggestByUser.Text;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchDDL(_SETUPLOGONVO);
            ddlSuggestByUser.DataSource = lstSETUPLOGONVO;
            ddlSuggestByUser.DataTextField = "Name";
            ddlSuggestByUser.DataValueField = "UserID";
            ddlSuggestByUser.DataBind();
        }

        protected void ddlORRoom_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}