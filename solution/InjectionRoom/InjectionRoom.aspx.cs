using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.InjectionRoom
{
    public partial class InjectionRoom : System.Web.UI.Page
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
                if (Request.QueryString["d"] != null)
                {
                    string ORID = Request.QueryString["d"];
                    loadvalue(ORID);
                }
                else
                {
                    Response.Redirect("/InjectionRoom/", false);
                }
            }

        }

        public void loadvalue(string orid)
        {
            try
            {
                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORID = orid;

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
                    lblHN.Text = _lstORHEADERVO[0].HN;

                    if (_lstORHEADERVO[0].PatientInfection)
                        ifInfection.Visible = false;
                    else
                        itInfection.Visible = false;

                    if (_lstORHEADERVO[0].PatientType1)
                        ifPatientType1.Visible = false;
                    else
                        itPatientType1.Visible = false;

                    if (_lstORHEADERVO[0].PatientType2)
                        ifPatientType2.Visible = false;
                    else
                        itPatientType2.Visible = false;

                    if (_lstORHEADERVO[0].PatientUP)
                        ifUp.Visible = false;
                    else
                        itUp.Visible = false;

                    lblORDate.Text = _lstORHEADERVO[0].strORDate;
                    lblORCASE.Text = _lstORHEADERVO[0].ORCase.ToString();
                    lblORTime.Text = _lstORHEADERVO[0].ORTime;
                    lblArrivalTime.Text = _lstORHEADERVO[0].ArrivalTime;
                    if (_lstORHEADERVO[0].ORTimeFollow)
                    {
                        iTFFlase.Visible = false;
                        lblORTimeH.Visible = false;
                        lblORTime.Visible = false;
                    }
                    else
                    {
                        iTFTrue.Visible = false;
                        lblORTimeH.Visible = true;
                        lblORTime.Visible = true;
                    }
                    if (_lstORHEADERVO[0].ORStatCase)
                    {
                        iORStatCaseFlase.Visible = false;
                    }
                    else
                    {
                        iORStatCaseTrue.Visible = false;
                    }
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

                    //ORPATIENTVO _ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(_lstORHEADERVO[0].HN);
                    if (_lstORHEADERVO[0].ORPATIENTVO.HN != null)
                    {
                        lblPatientName.Text = _lstORHEADERVO[0].ORPATIENTVO.PatientName;
                        lblGender.Text = _lstORHEADERVO[0].ORPATIENTVO.Sex;
                        lblAge.Text = _lstORHEADERVO[0].ORPATIENTVO.Age;
                        lblBirthDateTime.Text = _lstORHEADERVO[0].ORPATIENTVO.BirthDateTime.Value.ToString("dd-MM-yyyy");
                        lblIDCARD.Text = _lstORHEADERVO[0].ORPATIENTVO.Ref;
                        lblNationality.Text = _lstORHEADERVO[0].ORPATIENTVO.Nationality;

                        PictureFileName = _lstORHEADERVO[0].ORPATIENTVO.PictureFileName;

                        PATIENTALLEGICVO _vl = new PATIENTALLEGICVO();
                        _vl.HN = _lstORHEADERVO[0].ORPATIENTVO.HN;
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
                        _PATIENTDIAGVO.HN = _lstORHEADERVO[0].ORPATIENTVO.HN;
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
                    }
                    loadOROperation(_lstORHEADERVO[0].ORID);

                    loadPostOROperation(_lstORHEADERVO[0].ORID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadOROperation(string orid)
        {
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

            List<OROPERATIONVO> _lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(orid);
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

        private void loadPostOROperation(string orid)
        {
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVOTemp = new List<POSTOROPERATIONVO>();
            POSTOROPERATIONVO opRight = new POSTOROPERATIONVO();
            opRight.Side = (int)EnumOR.ORSide.RE;
            opRight.strSide = EnumOR.ORSide.RE.ToString();
            POSTOROPERATIONVO opLeft = new POSTOROPERATIONVO();
            opLeft.Side = (int)EnumOR.ORSide.LE;
            opLeft.strSide = EnumOR.ORSide.LE.ToString();
            POSTOROPERATIONVO opBoth = new POSTOROPERATIONVO();
            opBoth.Side = (int)EnumOR.ORSide.BE;
            opBoth.strSide = EnumOR.ORSide.BE.ToString();
            POSTOROPERATIONVO opNone = new POSTOROPERATIONVO();
            opNone.Side = (int)EnumOR.ORSide.None;
            opNone.strSide = EnumOR.ORSide.None.ToString();
            POSTOROPERATIONVO opNA = new POSTOROPERATIONVO();
            opNA.Side = (int)EnumOR.ORSide.ยังไม่ระบุตา;
            opNA.strSide = EnumOR.ORSide.ยังไม่ระบุตา.ToString();
            POSTOROPERATIONVO _POSTOROPERATIONVO = new POSTOROPERATIONVO();
            _POSTOROPERATIONVO.ORID = orid;
            List<POSTOROPERATIONVO> _lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(_POSTOROPERATIONVO);
            foreach (POSTOROPERATIONVO x in _lstPOSTOROPERATIONVO)
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
                lstPOSTOROPERATIONVOTemp.Add(opRight);
            if (!string.IsNullOrEmpty(opLeft.SubName))
                lstPOSTOROPERATIONVOTemp.Add(opLeft);
            if (!string.IsNullOrEmpty(opBoth.SubName))
                lstPOSTOROPERATIONVOTemp.Add(opBoth);
            if (!string.IsNullOrEmpty(opNone.SubName))
                lstPOSTOROPERATIONVOTemp.Add(opNone);
            if (!string.IsNullOrEmpty(opNA.SubName))
                lstPOSTOROPERATIONVOTemp.Add(opNA);
            gvPostOROperation.DataSource = lstPOSTOROPERATIONVOTemp;
            gvPostOROperation.DataBind();
        }

        private List<ORPATIENTVO> getORPatient(string strSearch)
        {
            List<ORPATIENTVO> lstOR = new List<ORPATIENTVO>();
            lstOR = new BLORPATIENT(dbInfo).SearchByKey(strSearch);

            return lstOR;
        }

    }
}