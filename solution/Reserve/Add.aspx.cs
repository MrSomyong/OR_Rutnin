using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Reserve
{
    public partial class Add : System.Web.UI.Page
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
                if (Session["ORDate"] != null)
                {
                    hdDate.Value = Session["ORDate"].ToString();
                }
                else
                {
                    hdDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                hdDate.Value = dd.ToString("dd/MM/yyyy");
                hdDateEn.Value = CultureInfo.GetDateString(dd, YearType.English);
                ddlORTimeH.SelectedValue = dd.Hour.ToString("hh");
                ddlORTimeM.SelectedValue = dd.Minute.ToString("mm");
                hdORID.Value = Guid.NewGuid().ToString();
                if (Request.QueryString["hn"] != null)
                {
                    txtHN.Text = Request.QueryString["hn"];
                    loadvalue();
                }
            }

        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = save();
            if (rtv.Value)
            {
                Response.Redirect("/Reserve/?m=complete", false);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            EnableCtl(false);
            setdefaultvalue();            
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
            setbtnDisable();

        }

        private void EnableCtl(bool bEnable)
        {
            txtHN.Enabled = !bEnable;
            pnORHEADER.Enabled = bEnable;
            
        }

        public void loadvalue()
        {
            try
            {
                ORPATIENTVO _ORPATIENTVO = new ORPATIENTVO();
                _ORPATIENTVO.HN = txtHN.Text.Trim().ToUpper();

                ORPATIENTVO ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(_ORPATIENTVO.HN);
                if (!string.IsNullOrEmpty(ORPATIENTVO.HN))
                {
                    byte[] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(ORPATIENTVO.HN);
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
                    
                    EnableCtl(true);
                    setbtnEnable();

                    txtHN.Text = ORPATIENTVO.HN;
                    lblPatientName.Text = ORPATIENTVO.PatientName;

                    lblGender.Text = ORUtils.getGender(ORPATIENTVO.Gender);

                    lblBirthDateTime.Text = CultureInfo.GetDatetime(ORPATIENTVO.BirthDateTime.Value, YearType.English).ToString("dd-MM-yyyy");
                    lblGender.Text = ORPATIENTVO.Sex;
                    lblAge.Text = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                    lblIDCARD.Text = ORPATIENTVO.Ref;
                    lblNationality.Text = ORPATIENTVO.Nationality;
                    PictureFileName = ORPATIENTVO.PictureFileName;

                    PATIENTALLEGICVO _vl = new PATIENTALLEGICVO();
                    _vl.HN = ORPATIENTVO.HN;
                    List<PATIENTALLEGICVO> lstPATIENTALLEGICVO = new BLPATIENTALLEGIC(dbInfo).SearchByKey(_vl);
                    string allegicname = string.Empty;
                    string Reaction = string.Empty;
                    
                    if (lstPATIENTALLEGICVO.Count > 0)
                    {
                        lblPatientallegic.Text = "แพ้ยา : <strong>" + lstPATIENTALLEGICVO[0].allegicname + "</strong>   ||   อาการ : <strong>" + lstPATIENTALLEGICVO[0].Reaction + "</strong>";
                    }
                    if (lstPATIENTALLEGICVO.Count > 1)
                    {
                        btnPatientallegicMore.Visible = true;
                        gvPatientallegic.DataSource = lstPATIENTALLEGICVO;
                        gvPatientallegic.DataBind();
                    }

                    divError.Visible = false;

                }
                else
                {
                    divError.Visible = true;
                    lblMessageError.Text = "HN not found";
                    setdefaultvalue();
                }

                loadgvOROperationEmpty();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                //if(ORHEADERVO.ORTimeFollow)
                //    ORHEADERVO.ORTime = "23:59";
                //else
                //    ORHEADERVO.ORTime = ddlORTime.SelectedValue;
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
                
                //
                List<ORHEADERVO> lstorh = new BLORHEADER(dbInfo).SearchBySurgeon(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);
                

                if (ORHEADERVO.ORTimeFollow)
                {
                    if (lstorh.Count > 0)
                    {
                        ORHEADERVO.ORTime = lstorh[0].ORTime + ":00";
                    }
                    else
                        ORHEADERVO.ORTime = "00:00";
                }
                else
                    ORHEADERVO.ORTime = ddlORTimeH.SelectedValue + ":" + ddlORTimeM.SelectedValue;
                //
                ORHEADERVO.ArrivalTime = ddlArrivalTimeH.SelectedValue + ":" + ddlArrivalTimeM.SelectedValue;
                ORHEADERVO.AnesthesiaDoctor1 = ddlAnesthesiaDoctor1.SelectedValue;
                ORHEADERVO.AnesthesiaDoctor2 = ddlAnesthesiaDoctor2.SelectedValue;
                ORHEADERVO.AnesthesiaDoctor3 = ddlAnesthesiaDoctor3.SelectedValue;
                ORHEADERVO.AnesthesiaNurse1 = ddlAnesthesiaNurse1.SelectedValue;
                ORHEADERVO.AnesthesiaNurse2 = ddlAnesthesiaNurse2.SelectedValue;
                ORHEADERVO.AnesthesiaNurse3 = ddlAnesthesiaNurse3.SelectedValue;
                ORHEADERVO.Remark = txtRemark.Text;
                ORHEADERVO.CreateDate = DateTime.Now;
                ORHEADERVO.CreateBy = Session["USERID"].ToString();
                ORHEADERVO.Prediag = txtPrediag.Text.Trim();
                ORHEADERVO.SuggestByUser = ddlSuggestByUser.SelectedValue;
                ORHEADERVO.RequestByUser = ddlRequestByUser.SelectedValue;

                rtv = new BLORHEADER(dbInfo).Insert(ORHEADERVO);
                if (rtv.Value)
                {
                    //int j = 1;
                    //foreach (OROPERATIONVO OROPERATIONVO in GetListValue_gvOROperatoin())
                    //{
                    //    OROPERATIONVO.ID = Guid.NewGuid().ToString();
                    //    OROPERATIONVO.ORID = ORHEADERVO.ORID;
                    //    OROPERATIONVO.Seq = j;
                    //    rtv = new BLOROPERATION(dbInfo).Insert(OROPERATIONVO);
                    //    j++;
                    //}
                }

                lstorh = new BLORHEADER(dbInfo).SearchBySurgeon(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);
                if (lstorh.Count > 0)
                {
                    List<ORHEADERVO> lstorhTF = new BLORHEADER(dbInfo).SearchBySurgeonTF(ORHEADERVO.SurgeonMaster, ORHEADERVO.ORDate.Value, ORHEADERVO.ORRoom);
                    foreach (ORHEADERVO orh1 in lstorhTF)
                    {
                        orh1.ORTime = lstorh[0].ORTime + ":00";
                        ReturnValue rvtf = new BLORHEADER(dbInfo).UpdateTimeTF(orh1);
                    }
                }
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
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

        protected void txtSearchHN_TextChanged(object sender, EventArgs e)
        {
            if(txtHN.Text.Trim() != string.Empty)
                loadvalue();
            //List<ORPATIENTVO> lstORPATIENTVO = getORPatient(txtSearchHN.Text);
            //if (lstORPATIENTVO.Count > 0)
            //{
            //    txtHN.Text = lstORPATIENTVO[0].HN;
            //}
            //ddlHN.DataSource = getORPatient(txtSearchHN.Text);
            //ddlHN.DataTextField = "HNName";
            //ddlHN.DataValueField = "HN";
            //ddlHN.DataBind();

            //if (ddlHN.Items.Count > 0)
            //{
            //    loadvalue();
            //}
            //else
            //{
            //    setdefaultvalue();
            //}
        }

        //protected void ddlHN_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    txtHN.Text = ddlHN.SelectedValue;
        //    loadvalue();
        //}

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
            //loadgvmain();
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
                _lstor.Add(OROPERATIONVO);
                gvOROperation.DataSource = _lstor;
                gvOROperation.DataBind();
                LoadlblProcedure();

            }
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

        private void SetButtonUpDown()
        {
            HyperLink lnkbtnUp = (gvOROperation.Rows[0].FindControl("lnkbtnUp") as HyperLink);
            HyperLink lnkbtnDown = (gvOROperation.Rows[gvOROperation.Rows.Count - 1].FindControl("lnkbtnDown") as HyperLink);
            lnkbtnUp.Visible = false;
            lnkbtnDown.Visible = false;
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
            foreach (GridViewRow drow in gvOROperation.Rows)
            {
                OROPERATIONVO _or = new OROPERATIONVO();
                _or.MainCode = (drow.FindControl("hdgvMainCode") as HiddenField).Value;
                _or.SubCode = (drow.FindControl("hdgvSubCode") as HiddenField).Value;
                _or.Side = int.Parse((drow.FindControl("hdgvSide") as HiddenField).Value);
                _or.strSide = ((EnumOR.ORSide)_or.Side).ToString();
                _or.SubMark = (drow.FindControl("hdgvSubMark") as HiddenField).Value;
                _or.strSubMark = (drow.FindControl("lblgvstrSubMark") as Label).Text;
                _or.Name = (drow.FindControl("lblgvName") as Label).Text;
                _or.SubName = (drow.FindControl("lblgvSubName") as Label).Text;
                _lstor.Add(_or);
            }

            return _lstor;
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
    }
}