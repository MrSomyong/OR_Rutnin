using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using SelectPdf;

namespace solution.PostOR
{
    public partial class PostOR : System.Web.UI.Page
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
                MapDDLPro();
                MapDDLTabICD();
                loadgvmain();
                loadgvsubEmpty();
                loadgvOROperationEmpty();
                loadgvImplantEmpty();
                loadgvNurseEmpty();
                loadgvICDEmpty();

                loadgvUnderPatientEmpty();

                if (Session["ORDate"] != null)
                {
                    hdORDate.Value = Session["ORDate"].ToString();
                }
                else
                {
                    hdORDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }
                DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdORDate.Value));
                hdORDate.Value = dd.ToString("dd/MM/yyyy");
                ddlORTimeH.SelectedValue = dd.Hour.ToString("hh");
                ddlORTimeM.SelectedValue = dd.Minute.ToString("mm");

                if (Request.QueryString["d"] != null)
                {
                    hdORID.Value = Request.QueryString["d"];
                    loadICDLaoutEmpty();

                    loadvalue(hdORID.Value);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }
            }


        }

        protected void Save_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = save();
            if (rtv.Value)
            {
                Response.Redirect(Request.RawUrl, true);
                AlertMessage(true, false, "Update Complete.");
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void Clear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, true);
            //setdefaultvalue();
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("/Reserve/Cancel/?d=" + hdORID.Value, false);
        }

        protected void PostTreatment_Click(object sender, EventArgs e)
        {
            Response.Redirect("/PostTreatment/Default/?UID=" + Session["USERID"] + "&hn=" + hdHN.Value + "&vn=" + hdVN.Value + "&vdate=" + hdVisitDate.Value, false);
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
            ORHEADERVO ORHEADERVO = new ORHEADERVO();
            ORHEADERVO.ORID = hdORID.Value;
            ORHEADERVO.CxlPostOR = false;
            ORHEADERVO.CxlPostORReason = string.Empty;

            ReturnValue rv = new BLORHEADER(dbInfo).CancelPostOR(ORHEADERVO);
            if (rv.Value)
            {
            }
            Response.Redirect(Request.RawUrl, true);
        }

        protected void btnCanCelReason_Click(object sender, EventArgs e)
        {
            ORHEADERVO ORHEADERVO = new ORHEADERVO();
            ORHEADERVO.ORID = hdORID.Value;
            ORHEADERVO.CxlPostOR = true;
            ORHEADERVO.CxlPostORReason = ddlREASON.SelectedValue;
            if (ddlREASON.SelectedValue == "99")
            {
                ORHEADERVO.CxlPostORReasonOther = txtReason.Text;
            }
            ReturnValue rv = new BLORHEADER(dbInfo).CancelPostOR(ORHEADERVO);
            if (rv.Value)
            {
                //ORLogVO orlog = new ORLogVO();
                //orlog.ORID = ORHEADERVO.ORID;
                //orlog.HN = ORHEADERVO.HN;
                //orlog.PatientName = ORHEADERVO.PatientName;
                //orlog.Detail = "ยกเลิก : " + ddlREASON.SelectedItem.Text;
                //orlog.UpdateBy = Session["USERID"].ToString();
                //ReturnValue rv1 = new BLORLog(dbInfo).Insert(orlog);
                //if (rv.Value)
                //{
                //    //Happy
                //}
            }
            Response.Redirect(Request.RawUrl, true);
        }

        protected void btnAddNurse_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = saveNurse();
            if (rtv.Value)
            {
                loadPostORNurse(hdORID.Value);
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutNurse();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnAddICD_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = saveICD();
            if (rtv.Value)
            {
                loadPostORICD(hdORID.Value);
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutICD();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnAddComplication_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = savePostORComplication();
            if (rtv.Value)
            {
                loadPostORComplication(hdORID.Value);
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutComplication();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnAddWarning_Click(object sender, EventArgs e)
        {
            if (txtWarning.Text.Trim() == string.Empty)
            {
                AlertMessage(true, false, "Update Complete.");
                return;
            }
            ReturnValue rtv = savePostORWarning();
            if (rtv.Value)
            {
                loadPostORWarning(hdORID.Value);
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutWarning();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnAddImplant_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = savePostORImplant();
            if (rtv.Value)
            {
                loadPostORImplant(hdORID.Value);
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutImplant();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnAddMigration_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = saveORMigration();
            if (rtv.Value)
            {
                loadORMIGRATION(lblHN.Text);                
                AlertMessage(true, false, "Update Complete.");
                ClearLaoutNurse();
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        protected void btnClearNurse_Click(object sender, EventArgs e)
        {
            ClearLaoutNurse();
        }

        private void ClearLaoutNurse()
        {
            hdSuffixNurse.Value = string.Empty;
            ddlNurseType.SelectedIndex = 0;
            ddlNurseCode.SelectedIndex = 0;
            txtNurseRemark.Text = string.Empty;
            foreach (GridViewRow r in gvPostORNurse.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
        }

        protected void btnClearICD_Click(object sender, EventArgs e)
        {
            MapDDLTabICD();
            ClearLaoutICD();
            loadICDLaoutEmpty();

        }

        private void ClearLaoutICD()
        {
            hdPOSTORICD_ID.Value = string.Empty;
            ddlICD.SelectedIndex = 0;

            ddlICDCM1.SelectedIndex = 0;
            ddlICDCM2.SelectedIndex = 0;
            ddlICDCM3.SelectedIndex = 0;
            ddlICDCM4.SelectedIndex = 0;
            ddlICDCM5.SelectedIndex = 0;

            ddlcm1doc1.SelectedIndex = 0;
            ddlcm1doc2.SelectedIndex = 0;
            ddlcm1doc3.SelectedIndex = 0;
            ddlcm1doc4.SelectedIndex = 0;

            ddlcm2doc1.SelectedIndex = 0;
            ddlcm2doc2.SelectedIndex = 0;
            ddlcm2doc3.SelectedIndex = 0;
            ddlcm2doc4.SelectedIndex = 0;

            ddlcm3doc1.SelectedIndex = 0;
            ddlcm3doc2.SelectedIndex = 0;
            ddlcm3doc3.SelectedIndex = 0;
            ddlcm3doc4.SelectedIndex = 0;

            ddlcm4doc1.SelectedIndex = 0;
            ddlcm4doc2.SelectedIndex = 0;
            ddlcm4doc3.SelectedIndex = 0;
            ddlcm4doc4.SelectedIndex = 0;

            ddlcm5doc1.SelectedIndex = 0;
            ddlcm5doc2.SelectedIndex = 0;
            ddlcm5doc3.SelectedIndex = 0;
            ddlcm5doc4.SelectedIndex = 0;

            txtICDSearch.Text = string.Empty;
            txtICDCM1Search.Text = string.Empty;
            txtICDCM2Search.Text = string.Empty;
            txtICDCM3Search.Text = string.Empty;
            txtICDCM4Search.Text = string.Empty;
            txtICDCM5Search.Text = string.Empty;
            txtICD_Remark.Text = string.Empty;

            foreach (GridViewRow r in gvPostORICD.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
        }

        protected void btnClearImplant_Click(object sender, EventArgs e)
        {
            ClearLaoutImplant();
        }

        private void ClearLaoutImplant()
        {
            //hdSuffixImplant.Value = string.Empty;
            //ddlImplantType.SelectedIndex = 0;
            //txtRemarkImplant.Text = string.Empty;
            //foreach (GridViewRow r in gvPostORImplant.Rows)
            //{
            //    if (r.RowType == DataControlRowType.DataRow)
            //    {
            //        r.BackColor = System.Drawing.Color.White;
            //    }
            //}
        }

        protected void btnClearComplication_Click(object sender, EventArgs e)
        {
            ClearLaoutComplication();
        }

        private void ClearLaoutComplication()
        {
            ddlComplicationID.ClearSelection();
            hdComplicationID.Value = string.Empty;
            txtComplicationHeader.Text = string.Empty;
            txtComplicationDetail.Text = string.Empty;
            ddlComplicationID.Enabled = true;
            foreach (GridViewRow r in gvComplication.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
        }

        protected void btnClearWarning_Click(object sender, EventArgs e)
        {
            ClearLaoutWarning();
        }

        private void ClearLaoutWarning()
        {
            hdWarningID.Value = string.Empty;
            txtWarning.Text = string.Empty;
            foreach (GridViewRow r in gvWarning.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
        }

        protected void btnClearMigration_Click(object sender, EventArgs e)
        {
            ClearLaoutMigration();
        }

        private void ClearLaoutMigration()
        {
            hdMigrationID.Value = string.Empty;
            ddlORMigrationSide.SelectedIndex = 0;
            //ordate.SelectedIndex = 0;
            txtProcedureMemo.Text = string.Empty;
            txtNote.Text = string.Empty;
            foreach (GridViewRow r in gvORMigration.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
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

        private void setdefaultvalue()
        {
            //pnORHEADER.Enabled = false;
            lblHN.Text = string.Empty;
            lblPatientName.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblBirthDateTime.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            lblGender.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblAge.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblIDCARD.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            lblNationality.Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";

            lblPatientallegic.Text = string.Empty;
            lblPatientalDiag.Text = string.Empty;
            btnPatientallegicMore.Visible = false;
            btnPatientalDiagMore.Visible = false;

            divError.Visible = false;
            setbtnDisable();

            ddlComplicationID.Enabled = true;
        }

        public void loadvalue(string orid)
        {
            loadHeadWarningMore(orid);
            loadDetail(orid);
            loadPostORDetail(orid);
            loadPostOROperation(orid);
            loadPostORNurse(orid);
            loadPostORICD(orid);
            loadPostORImplant(orid);
            loadPostORComplication(orid);
            loadPostORWarning(orid);
            loadORSummary();
            loadORMIGRATION(lblHN.Text);
        }

        public void loadDetail(string orid)
        {
            try
            {
                ClearSelection();

                ORHEADERVO xx = new ORHEADERVO();
                xx.ORID = orid;

                List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByKey(xx);

                foreach (ORHEADERVO hd in lsth)
                {
                    btnConfirm.Visible = hd.CxlPostOR.Value;
                    btnCancel.Visible = !hd.CxlPostOR.Value;

                    loadORPatient(hd.HN);
                    loadUnderPatent(hd.HN, hd.ORDate.Value);
                    if (!loadAN(hd))
                    {
                        loadVN(hd);
                    }
                    loadHNUnderly(hd.HN);

                    chbPatientInfection.Checked = hd.PatientInfection;
                    chbPatientType1.Checked = hd.PatientType1;
                    chbPatientType2.Checked = hd.PatientType2;
                    chbPatientUP.Checked = hd.PatientUP;
                    chOnmed.Checked = hd.Onmed;
                    txtOnmed.Visible = hd.Onmed;
                    txtOnmed.Text = hd.OnmedNote;

                    hdORDate.Value = hd.ORDate.Value.ToString("dd/MM/yyyy");
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
                    if (!string.IsNullOrEmpty(hd.AdmitTimeType))
                    { ddlAdmitTimeType.SelectedValue = hd.AdmitTimeType; }
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

                    SETUPLOGONVO sETUPLOGONVO = new SETUPLOGONVO();
                    sETUPLOGONVO.UserID = hd.RequestByUser;
                    List<SETUPLOGONVO> lst = new BLSETUPLOGON(dbInfo).SearchByKey(sETUPLOGONVO);
                    if (lst.Count > 0)
                    {
                        txtRequestByUser.Text = lst[0].Name;
                    }

                    sETUPLOGONVO = new SETUPLOGONVO();
                    sETUPLOGONVO.UserID = hd.SuggestByUser;
                    lst = new BLSETUPLOGON(dbInfo).SearchByKey(sETUPLOGONVO);
                    if (lst.Count > 0)
                    {
                        txtSuggestByUser.Text = lst[0].Name;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadPostORDetail(string orid)
        {
            POSTORDETAILVO POSTORDETAILVO = new POSTORDETAILVO();
            POSTORDETAILVO.ORID = orid;
            List<POSTORDETAILVO> lstPOSTORDETAILVO = new BLPOSTORDETAIL(dbInfo).SearchByKey(POSTORDETAILVO);
            if (lstPOSTORDETAILVO.Count == 0)
            {
                hdSORDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdSORDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdFORDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdFORDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdSAnesDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdSAnesDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdFAnesDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdFAnesDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdSBlockDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdSBlockDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdFBlockDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdFBlockDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdSRecoveryDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdSRecoveryDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);

                hdFRecoveryDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                hdFRecoveryDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);
            }
            foreach (POSTORDETAILVO val in lstPOSTORDETAILVO)
            {
                if (val.StartORDateTime != null)
                {
                    hdSORDate.Value = val.StartORDateTime.Value.ToString("dd/MM/yyyy");
                    hdSORDateEn.Value = CultureInfo.GetDateString(val.StartORDateTime.Value, YearType.English);
                    ddlSORTimeH.SelectedValue = val.StartORDateTime.Value.Hour.ToString("0#");
                    ddlSORTimeM.SelectedValue = val.StartORDateTime.Value.Minute.ToString("0#");
                    if (val.StartORDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtSORTime.Text = val.StartORDateTime.Value.Hour.ToString("0#") + ":" + val.StartORDateTime.Value.Minute.ToString("0#");
                        if (txtSORTime.Text == "00:00")
                        {
                            txtSORTime.Text = "";
                        }
                    }
                }

                if (val.FinishORDateTime != null)
                {
                    hdFORDate.Value = val.FinishORDateTime.Value.ToString("dd/MM/yyyy");
                    hdFORDateEn.Value = CultureInfo.GetDateString(val.FinishORDateTime.Value, YearType.English);
                    ddlFORTimeH.SelectedValue = val.FinishORDateTime.Value.Hour.ToString("0#");
                    ddlFORTimeM.SelectedValue = val.FinishORDateTime.Value.Minute.ToString("0#");

                    if (val.FinishORDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtFORTime.Text = val.FinishORDateTime.Value.Hour.ToString("0#") + ":" + val.FinishORDateTime.Value.Minute.ToString("0#");
                        if (txtFORTime.Text == "00:00")
                        {
                            txtFORTime.Text = "";
                        }
                    }
                }

                if (val.StartAnesDateTime != null)
                {
                    hdSAnesDate.Value = val.StartAnesDateTime.Value.ToString("dd/MM/yyyy");
                    hdSAnesDateEn.Value = CultureInfo.GetDateString(val.StartAnesDateTime.Value, YearType.English);
                    ddlSAnesTimeH.SelectedValue = val.StartAnesDateTime.Value.Hour.ToString("0#");
                    ddlSAnesTimeM.SelectedValue = val.StartAnesDateTime.Value.Minute.ToString("0#");

                    if (val.StartAnesDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtSAnesTime.Text = val.StartAnesDateTime.Value.Hour.ToString("0#") + ":" + val.StartAnesDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtSAnesTime.Text == "00:00")
                    {
                        txtSAnesTime.Text = "";
                    }
                }

                if (val.FinishAnesDateTime != null)
                {
                    hdFAnesDate.Value = val.FinishAnesDateTime.Value.ToString("dd/MM/yyyy");
                    hdFAnesDateEn.Value = CultureInfo.GetDateString(val.FinishAnesDateTime.Value, YearType.English);
                    ddlFAnesTimeH.SelectedValue = val.FinishAnesDateTime.Value.Hour.ToString("0#");
                    ddlFAnesTimeM.SelectedValue = val.FinishAnesDateTime.Value.Minute.ToString("0#");

                    if (val.FinishAnesDateTime.Value.ToShortTimeString() != "0:00") 
                            {
                        txtFAnesTime.Text = val.FinishAnesDateTime.Value.Hour.ToString("0#") + ":" + val.FinishAnesDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtFAnesTime.Text == "00:00")
                    {
                        txtFAnesTime.Text = "";
                    }
                }

                if (val.StartBlockDateTime != null)
                {
                    hdSBlockDate.Value = val.StartBlockDateTime.Value.ToString("dd/MM/yyyy");
                    hdSBlockDateEn.Value = CultureInfo.GetDateString(val.StartBlockDateTime.Value, YearType.English);
                    ddlSBlockTimeH.SelectedValue = val.StartBlockDateTime.Value.Hour.ToString("0#");
                    ddlSBlockTimeM.SelectedValue = val.StartBlockDateTime.Value.Minute.ToString("0#");

                    if (val.StartBlockDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtSBlockTime.Text = val.StartBlockDateTime.Value.Hour.ToString("0#") + ":" + val.StartBlockDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtSBlockTime.Text == "00:00")
                    {
                        txtSBlockTime.Text = "";
                    }
                }

                if (val.FinishBlockDateTime != null)
                {
                    hdFBlockDate.Value = val.FinishBlockDateTime.Value.ToString("dd/MM/yyyy");
                    hdFBlockDateEn.Value = CultureInfo.GetDateString(val.FinishBlockDateTime.Value, YearType.English);
                    ddlFBlockTimeH.SelectedValue = val.FinishBlockDateTime.Value.Hour.ToString("0#");
                    ddlFBlockTimeM.SelectedValue = val.FinishBlockDateTime.Value.Minute.ToString("0#");
                    if (val.FinishBlockDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtFBlockTime.Text = val.FinishBlockDateTime.Value.Hour.ToString("0#") + ":" + val.FinishBlockDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtFBlockTime.Text == "00:00")
                    {
                        txtFBlockTime.Text = "";
                    }
                }

                if (val.StartRecoveryDateTime != null)
                {
                    hdSRecoveryDate.Value = val.StartRecoveryDateTime.Value.ToString("dd/MM/yyyy");
                    hdSRecoveryDateEn.Value = CultureInfo.GetDateString(val.StartRecoveryDateTime.Value, YearType.English);
                    ddlSRecoveryTimeH.SelectedValue = val.StartRecoveryDateTime.Value.Hour.ToString("0#");
                    ddlSRecoveryTimeM.SelectedValue = val.StartRecoveryDateTime.Value.Minute.ToString("0#");
                    if (val.StartRecoveryDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtSRecoveryTime.Text = val.StartRecoveryDateTime.Value.Hour.ToString("0#") + ":" + val.StartRecoveryDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtSRecoveryTime.Text == "00:00")
                    {
                        txtSRecoveryTime.Text = "";
                    }
                }

                if (val.FinishRecoveryDateTime != null)
                {
                    hdFRecoveryDate.Value = val.FinishRecoveryDateTime.Value.ToString("dd/MM/yyyy");
                    hdFRecoveryDateEn.Value = CultureInfo.GetDateString(val.FinishRecoveryDateTime.Value, YearType.English);
                    ddlFRecoveryTimeH.SelectedValue = val.FinishRecoveryDateTime.Value.Hour.ToString("0#");
                    ddlFRecoveryTimeM.SelectedValue = val.FinishRecoveryDateTime.Value.Minute.ToString("0#");
                    if (val.FinishRecoveryDateTime.Value.ToShortTimeString() != "0:00")
                    {
                        txtFRecoveryTime.Text = val.FinishRecoveryDateTime.Value.Hour.ToString("0#") + ":" + val.FinishRecoveryDateTime.Value.Minute.ToString("0#");
                    }
                    if (txtFRecoveryTime.Text == "00:00")
                    {
                        txtFRecoveryTime.Text = "";
                    }
                }

                ddlORCaseType.SelectedValue = val.ORCaseType.ToString();
                ddlORWrongCase.SelectedValue = val.ORWrongCase.ToString();

                chORWoundType1.Checked = val.ORWoundType1.Value;
                chORWoundType2.Checked = val.ORWoundType2.Value;
                chORWoundType3.Checked = val.ORWoundType3.Value;
                chORWoundType4.Checked = val.ORWoundType4.Value;

                chExternal.Checked = val.External.Value;
                chAnterior.Checked = val.Anterior.Value;
                chPosterior.Checked = val.Posterior.Value;

                chChangOperation.Checked = val.ChangOperation.Value;
                chHR48.Checked = val.HR48.Value;
                chDay30.Checked = val.Day30.Value;
                txtIndicator.Text = val.Indicator;

                //ddlORWoundType.SelectedValue = val.ORWoundType.ToString();
                //ddlORUnplantType.SelectedValue = val.ORUnplantType.ToString();
            }
        }

        public void loadPostOROperation(string orid)
        {
            POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
            POSTOROPERATIONVO.ORID = orid;
            List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
            if (lstPOSTOROPERATIONVO.Count > 0)
            {
                gvOROperation.DataSource = lstPOSTOROPERATIONVO;
                gvOROperation.DataBind();
            }
            else
            {
                List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(orid);

                foreach (OROPERATIONVO OROPERATIONVO in lstOROPERATIONVO)
                {
                    POSTOROPERATIONVO xx = new POSTOROPERATIONVO();
                    xx.ID = OROPERATIONVO.ID;
                    xx.ORID = OROPERATIONVO.ORID;
                    //xx.ID = Guid.NewGuid().ToString();
                    //xx.ORID = hdORID.Value;
                    xx.Seq = OROPERATIONVO.Seq;

                    xx.MainCode = OROPERATIONVO.MainCode;
                    xx.SubCode = OROPERATIONVO.SubCode;
                    xx.Side = OROPERATIONVO.Side;
                    xx.strSide = ((EnumOR.ORSide)OROPERATIONVO.Side).ToString();
                    xx.SubMark = OROPERATIONVO.SubMark;
                    xx.strSubMark = OROPERATIONVO.strSubMark;
                    xx.Name = OROPERATIONVO.Name;
                    xx.SubName = OROPERATIONVO.SubName;
                    xx.Surgeon1 = ddlSurgeon1.SelectedValue;
                    xx.Surgeon2 = ddlSurgeon2.SelectedValue;
                    xx.Surgeon3 = ddlSurgeon3.SelectedValue;

                    new BLPOSTOROPERATION(dbInfo).Insert(xx);
                }
                gvOROperation.DataSource = lstOROPERATIONVO;
                gvOROperation.DataBind();
            }
            LoadlblProcedure();
        }

        private void loadPostORNurse(string orid)
        {
            POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
            POSTORNURSEVO.ORID = orid;
            List<POSTORNURSEVO> lstPOSTORNURSEVO = new BLPOSTORNURSE(dbInfo).SearchByKey(POSTORNURSEVO);
            gvPostORNurse.DataSource = lstPOSTORNURSEVO;
            gvPostORNurse.DataBind();
        }

        private void loadPostORICD(string orid)
        {
            POSTORICDVO POSTORICDVO = new POSTORICDVO();
            POSTORICDVO.ORID = orid;
            List<POSTORICDVO> lstPOSTORICDVO = new BLPOSTORICD(dbInfo).SearchByKey(POSTORICDVO);

            int i = 0;
            foreach (POSTORICDVO xx in lstPOSTORICDVO)
            {
                List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchByCode(xx.ICD);
                if (lstSETUPICDVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICD_Name = lstSETUPICDVO[0].Name;
                }

                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByCode(xx.ICDCM1);
                if (lstSETUPICDCMVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICDCM1_Name = lstSETUPICDCMVO[0].Name;
                }
                lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByCode(xx.ICDCM2);
                if (lstSETUPICDCMVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICDCM2_Name = lstSETUPICDCMVO[0].Name;
                }

                lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByCode(xx.ICDCM3);
                if (lstSETUPICDCMVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICDCM3_Name = lstSETUPICDCMVO[0].Name;
                }

                lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByCode(xx.ICDCM4);
                if (lstSETUPICDCMVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICDCM4_Name = lstSETUPICDCMVO[0].Name;
                }
                lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByCode(xx.ICDCM5);
                if (lstSETUPICDCMVO.Count > 0)
                {
                    lstPOSTORICDVO[i].ICDCM5_Name = lstSETUPICDCMVO[0].Name;
                }



                i++;
            }


            gvPostORICD.DataSource = lstPOSTORICDVO;
            gvPostORICD.DataBind();
        }

        private void loadPostORImplant(string orid)
        {
            //List<SETUPIMPLANTMAINVO> lstSETUPIMPLANTMAINVO = new BLSETUPIMPLANTMAIN(dbInfo).SearchAll();

            //gvImplant.DataSource = lstSETUPIMPLANTMAINVO;
            //gvImplant.DataBind();

            //POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
            //POSTORIMPLANTVO.ORID = orid;
            //List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
            //gvPostORImplant.DataSource = lstPOSTORIMPLANTVO;
            //gvPostORImplant.DataBind();
        }

        private void loadPostORComplication(string orid)
        {
            ddlComplication.Enabled = true;
            POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO = new POSTORCOMPLICATIONVO();
            POSTORCOMPLICATIONVO.ORID = orid;
            List<POSTORCOMPLICATIONVO> lstPOSTORCOMPLICATIONVO = new BLPOSTORCOMPLICATION(dbInfo).SearchByKey(POSTORCOMPLICATIONVO);
            gvComplication.DataSource = lstPOSTORCOMPLICATIONVO;
            gvComplication.DataBind();
        }

        private void loadPostORWarning(string orid)
        {
            POSTORWARNINGVO POSTORWARNINGVO = new POSTORWARNINGVO();
            POSTORWARNINGVO.ORID = orid;
            List<POSTORWARNINGVO> lstPOSTORWARNINGVO = new BLPOSTORWARNING(dbInfo).SearchByKey(POSTORWARNINGVO);
            gvWarning.DataSource = lstPOSTORWARNINGVO;
            gvWarning.DataBind();
        }

        public void loadUnderPatent(string _HN, DateTime _ORDate)
        {
            try
            {
                ORHEADERVO xx = new ORHEADERVO();
                xx.HN = _HN;
                xx.ORDate = _ORDate;
                List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchUnderPatient(xx);
                List<ORHEADERVO> lsthTemp = new List<ORHEADERVO>();
                foreach (ORHEADERVO hd in lsth)
                {
                    POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                    POSTOROPERATIONVO.ORID = hd.ORID;

                    string r = string.Empty;
                    string l = string.Empty;
                    string b = string.Empty;
                    string None = string.Empty;
                    string NA = string.Empty;

                    List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByKey(POSTOROPERATIONVO);
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

                        //hd.lstPOSTOROPERATIONVO.Add(op1);
                    }

                    if (!string.IsNullOrEmpty(r))
                    {
                        r = "<code>" + EnumOR.ORSide.RE.ToString() + "</code> : " + r ;
                    }
                    if (!string.IsNullOrEmpty(l))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        l = _br + " <code>" + EnumOR.ORSide.LE.ToString() + "</code> : " + l ;
                    }
                    if (!string.IsNullOrEmpty(b))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r))
                            _br = "<br/>";
                        b = _br + " <code>" + EnumOR.ORSide.BE.ToString() + "</code> : " + b ;
                    }
                    if (!string.IsNullOrEmpty(None))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b))
                            _br = "<br/>";
                        None = _br + " <code>" + EnumOR.ORSide.None.ToString() + "</code> : " + None ;
                    }
                    if (!string.IsNullOrEmpty(NA))
                    {
                        string _br = string.Empty;
                        if (!string.IsNullOrEmpty(l) || !string.IsNullOrEmpty(r) || !string.IsNullOrEmpty(b) || !string.IsNullOrEmpty(None))
                            _br = "<br/>";
                        NA = _br + " <code>" + EnumOR.ORSide.ยังไม่ระบุตา.ToString() + "</code> : " + NA;
                    }

                    hd.POSTOROPERATION = r + l + b + None + NA;

                    lsthTemp.Add(hd);
                }
                ORMIGRATIONVO _ORMIGRATION = new ORMIGRATIONVO();
                _ORMIGRATION.HN = _HN;
                List<ORMIGRATIONVO> lst_ORMIGRATION = new BLORMIGRATION(dbInfo).SearchByKey(_ORMIGRATION);
                foreach (ORMIGRATIONVO op1 in lst_ORMIGRATION)
                {
                    ORHEADERVO orh = new ORHEADERVO();
                    orh.ORDate = op1.ORDate;
                    orh.strORDate = op1.strORDate;
                    orh.ORTime = op1.ORDate.Value.ToString("hh:mm");
                    orh.POSTOROPERATION = op1.ProcedureMemo;
                    orh.strORRoom = op1.ORRoomName;
                    orh.SurgeonName = op1.SurgeonName;
                    orh.strSide = op1.strSide;
                    orh.Note = op1.Note;
                    lsthTemp.Add(orh);
                }

                IEnumerable<ORHEADERVO> query = lsthTemp.OrderByDescending(i => i.ORDate);

                gvUnderPatient.DataSource = query;
                gvUnderPatient.DataBind();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void loadORPatient(string hn)
        {
            try
            {
                ORPATIENTVO ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(hn);
                if (!string.IsNullOrEmpty(ORPATIENTVO.HN))
                {
                    byte[] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(hn);
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
                    lblHN.Text = ORPATIENTVO.HN;
                    lblPatientName.Text = ORPATIENTVO.PatientName;
                    lblGender.Text = ORPATIENTVO.Sex;
                    lblBirthDateTime.Text = ORPATIENTVO.BirthDateTime.Value.ToString("dd-MM-yyyy");
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
                        lblPatientallegic.Text = "Allergy : <strong>" + lstPATIENTALLEGICVO[0].allegicname + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;อาการ : <strong>" + lstPATIENTALLEGICVO[0].Reaction + "</strong>";
                    }
                    else
                    {
                        lblPatientallegic.Text = "Allergy : <strong>ไม่มี</strong>";
                    }
                    if (lstPATIENTALLEGICVO.Count > 1)
                    {
                        btnPatientallegicMore.Visible = true;
                        gvPatientallegic.DataSource = lstPATIENTALLEGICVO;
                        gvPatientallegic.DataBind();
                    }
                    else
                    {
                        btnPatientallegicMore.Visible = false;
                    }

                    PATIENTDIAGVO _vlDiagVO = new PATIENTDIAGVO();
                    _vlDiagVO.HN = ORPATIENTVO.HN;
                    List<PATIENTDIAGVO> lstPATIENTDIAGVO = new BLPATIENTDIAG(dbInfo).SearchByKey(_vlDiagVO);
                    string diagname = string.Empty;

                    if (lstPATIENTDIAGVO.Count > 0)
                    {
                        //lblPatientalDiag.Text = "Patient Diag : <strong>" + lstPATIENTDIAGVO[0].diagname + "</strong>";
                        lblPatientalDiag.Text = "Underlying disease : <strong>" + lstPATIENTDIAGVO[0].diagname + "</strong>";
                    }
                    else
                    {
                        //lblPatientalDiag.Text = "Patient Diag : <strong>ไม่มี</strong>";
                        lblPatientalDiag.Text = "Underlying disease : <strong>ไม่มี</strong>";
                    }


                    if (lstPATIENTDIAGVO.Count > 1)
                    {
                        btnPatientalDiagMore.Visible = true;
                        gvPatientalDiag.DataSource = lstPATIENTDIAGVO;
                        gvPatientalDiag.DataBind();
                    }
                    else
                    {
                        btnPatientalDiagMore.Visible = false;
                    }
                }
            }
            catch { }
        }

        public bool loadAN(ORHEADERVO hd)
        {
            bool rv = false;
            try
            {
                VT_PATIENT_ANVO vl = new VT_PATIENT_ANVO();
                vl.HN = hd.HN;
                vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " " + hd.ORTime);
                List<VT_PATIENT_ANVO> lstVT_PATIENT_ANVO = new BLVT_PATIENT_AN(dbInfo).SearchAN(vl);
                if (lstVT_PATIENT_ANVO.Count > 0)
                {
                    lblAN.Text = lstVT_PATIENT_ANVO[0].AN;
                    divAN.Visible = true;
                    rv = true;
                }
                else
                {
                    loadVN(hd);
                    lblAN.Text = string.Empty;
                    divAN.Visible = false;
                    rv = false;
                }
            }
            catch { }
            return rv;
        }

        public void loadVN(ORHEADERVO hd)
        {
            try
            {
                VT_PATIENT_VNVO vl = new VT_PATIENT_VNVO();
                vl.HN = hd.HN;
                vl.ORDateTime = hd.ORDate;
                List<VT_PATIENT_VNVO> lstVT_PATIENT_VNVO = new BLVT_PATIENT_VN(dbInfo).SearchVN(vl);
                if (lstVT_PATIENT_VNVO.Count > 0)
                {
                    lblVN.Text = lstVT_PATIENT_VNVO[0].VN;
                    lblVN_VisitDate.Text = CultureInfo.GetDatetime(lstVT_PATIENT_VNVO[0].VisitDate.Value, YearType.Thai).ToString("dd MMM yyyy");
                    divVN.Visible = true;

                    hdVisitDate.Value = CultureInfo.GetDatetime(lstVT_PATIENT_VNVO[0].VisitDate.Value, YearType.English).ToString("yyyyMMdd");
                    hdVN.Value = lstVT_PATIENT_VNVO[0].VN.ToString();
                    hdHN.Value = hd.HN.ToString();
                    //btnPostTreatment.Visible = true;
                    btnPostTreatment.Enabled = true;
                }
                else
                {
                    divVN.Visible = false;
                    lblVN.Text = string.Empty;
                    //btnPostTreatment.Visible = false;
                    btnPostTreatment.Enabled = false;
                }
            }
            catch { }
        }

        private void loadHeadWarningMore(string ORID)
        {
            POSTORWARNINGVO _vlPOSTORWARNINGVO = new POSTORWARNINGVO();
            _vlPOSTORWARNINGVO.ORID = ORID;
            List<POSTORWARNINGVO> lstPOSTORWARNINGVO = new BLPOSTORWARNING(dbInfo).SearchByKey(_vlPOSTORWARNINGVO);
            string warning = string.Empty;
            if (lstPOSTORWARNINGVO.Count > 0)
            {
                lblHeadWarning.Text = "Warning : <strong>" + lstPOSTORWARNINGVO[0].Warning + "</strong>";
            }
            else
            {
                lblHeadWarning.Text = "Warning : <strong>ไม่มี</strong>";
            }


            if (lstPOSTORWARNINGVO.Count > 1)
            {
                btnHeadWarningMore.Visible = true;
                gvHeadWarningMore.DataSource = lstPOSTORWARNINGVO;
                gvHeadWarningMore.DataBind();
            }
            else
            {
                btnHeadWarningMore.Visible = false;
            }
        }

        public void loadHNUnderly(string HN)
        {
            try
            {
                txtUnderlyingtext.Text = string.Empty;
                HNUnderlyVO HNUnderlyVO = new HNUnderlyVO();
                HNUnderlyVO.HN = HN;
                List<HNUnderlyVO> lstHNUnderlyVO = new BLHNUnderly(dbInfo).SearchByHN(HNUnderlyVO);
                if (lstHNUnderlyVO.Count > 0)
                {
                    txtUnderlyingtext.Text = lstHNUnderlyVO[0].Underlyingtext;
                }
            }
            catch { }
        }

        private void loadORMIGRATION(string HN)
        {
            ORMIGRATIONVO ORMIGRATIONVO = new ORMIGRATIONVO();
            ORMIGRATIONVO.HN = HN;
            List<ORMIGRATIONVO> lstORMIGRATIONVO = new BLORMIGRATION(dbInfo).SearchByKey(ORMIGRATIONVO);
            gvORMigration.DataSource = lstORMIGRATIONVO;
            gvORMigration.DataBind();
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
                bool checkdup = false;
                ORHEADERVO ORHEADERVO = getORHeader();
                POSTORDETAILVO POSTORDETAILVO = getPostORDetail();
                //List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = getPostOROperation();
                //List<POSTORNURSEVO> lstPOSTORNURSEVO = getPostORNurse();
                //List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = getPostORImplant();
                //List<POSTORCOMPLICATIONVO> lstPOSTORCOMPLICATIONVO = getPostORComplication();

                // POSTORHEADER
                //checkdup = new BLPOSTORHEADER(dbInfo).Checkdup(ORHEADERVO.ORID);
                //if (!checkdup)
                //    rtv = new BLPOSTORHEADER(dbInfo).Insert(POSTORHEADERVO);
                //else
                rtv = new BLORHEADER(dbInfo).Update(ORHEADERVO);

                // POSTORDETAIL
                checkdup = new BLPOSTORDETAIL(dbInfo).Checkdup(POSTORDETAILVO.ORID);
                if (!checkdup)
                    rtv = new BLPOSTORDETAIL(dbInfo).Insert(POSTORDETAILVO);
                else
                    rtv = new BLPOSTORDETAIL(dbInfo).Update(POSTORDETAILVO);

                saveHNUnderly();

                if (!string.IsNullOrEmpty(ddlNurseType.SelectedValue) && ddlNurseType.SelectedValue != "0")
                {
                    saveNurse();
                    loadPostORNurse(hdORID.Value);
                    ClearLaoutNurse();
                }

                if (!string.IsNullOrEmpty(hdPOSTORICD_ID.Value))
                {
                    saveICD();
                    loadPostORICD(hdORID.Value);
                    ClearLaoutICD();
                }

                if (!string.IsNullOrEmpty(txtComplicationHeader.Text))
                {
                    savePostORComplication();
                    loadPostORComplication(hdORID.Value);
                    ClearLaoutComplication();

                }
                if (!string.IsNullOrEmpty(txtWarning.Text))
                {
                    savePostORWarning();
                    loadPostORWarning(hdORID.Value);
                    ClearLaoutWarning();

                }
                // POSTORNURSE
                //new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value);
                //foreach (POSTORNURSEVO POSTORNURSEVO in GetListValue_gvPostORNurse())
                //{
                //    rtv = new BLPOSTORNURSE(dbInfo).Insert(POSTORNURSEVO);
                //}

                // POSTORNURSE
                //new BLPOSTORIMPLANT(dbInfo).Delete(hdORID.Value);
                //foreach (POSTORIMPLANTVO POSTORIMPLANTVO in GetListValue_gvPostORImplant())
                //{
                //    rtv = new BLPOSTORIMPLANT(dbInfo).Insert(POSTORIMPLANTVO);
                //}

                // POSTORNURSE
                //new BLPOSTORCOMPLICATION(dbInfo).Delete(hdORID.Value);
                //foreach (POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO in GetListValue_gvPostORComplication())
                //{
                //    rtv = new BLPOSTORCOMPLICATION(dbInfo).Insert(POSTORCOMPLICATIONVO);
                //}
                loadvalue(hdORID.Value);
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue saveNurse()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
                POSTORNURSEVO.ORID = hdORID.Value;
                string Suffix = hdSuffixNurse.Value == "" ? "0" : hdSuffixNurse.Value;
                POSTORNURSEVO.Suffix = int.Parse(Suffix);
                string NurseType = ddlNurseType.SelectedValue == "" ? "0" : ddlNurseType.SelectedValue;
                POSTORNURSEVO.NurseType = int.Parse(NurseType);
                POSTORNURSEVO.NurseCode = ddlNurseCode.SelectedValue;
                POSTORNURSEVO.Remark = txtNurseRemark.Text;

                List<POSTORNURSEVO> _lstPOSTORNURSEVO = new BLPOSTORNURSE(dbInfo).SearchByKey(POSTORNURSEVO);
                if (POSTORNURSEVO.Suffix > 0 && _lstPOSTORNURSEVO.Count > 0)
                {
                    rtv = new BLPOSTORNURSE(dbInfo).Update(POSTORNURSEVO);
                    //hdSuffixNurse.Value = POSTORNURSEVO.Suffix.ToString();
                }
                else
                {
                    POSTORNURSEVO.Suffix = new BLPOSTORNURSE(dbInfo).GetSuffixNext(hdORID.Value);
                    rtv = new BLPOSTORNURSE(dbInfo).Insert(POSTORNURSEVO);
                    //hdSuffixNurse.Value = POSTORNURSEVO.Suffix.ToString();
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue saveICD()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                POSTORICDVO POSTORICDVO = new POSTORICDVO();
                POSTORICDVO.ORID = hdORID.Value;
                POSTORICDVO.ID = hdPOSTORICD_ID.Value;

                POSTORICDVO.ICD = ddlICD.SelectedValue;
                POSTORICDVO.ICDCM1 = ddlICDCM1.SelectedValue;
                POSTORICDVO.ICDCM2 = ddlICDCM2.SelectedValue;
                POSTORICDVO.ICDCM3 = ddlICDCM3.SelectedValue;
                POSTORICDVO.ICDCM4 = ddlICDCM4.SelectedValue;
                POSTORICDVO.ICDCM5 = ddlICDCM5.SelectedValue;

                POSTORICDVO.ICDCM1DOC1 = ddlcm1doc1.SelectedValue;
                POSTORICDVO.ICDCM1DOC2 = ddlcm1doc2.SelectedValue;
                POSTORICDVO.ICDCM1DOC3 = ddlcm1doc3.SelectedValue;
                POSTORICDVO.ICDCM1DOC4 = ddlcm1doc4.SelectedValue;

                POSTORICDVO.ICDCM2DOC1 = ddlcm2doc1.SelectedValue;
                POSTORICDVO.ICDCM2DOC2 = ddlcm2doc2.SelectedValue;
                POSTORICDVO.ICDCM2DOC3 = ddlcm2doc3.SelectedValue;
                POSTORICDVO.ICDCM2DOC4 = ddlcm2doc4.SelectedValue;

                POSTORICDVO.ICDCM3DOC1 = ddlcm3doc1.SelectedValue;
                POSTORICDVO.ICDCM3DOC2 = ddlcm3doc2.SelectedValue;
                POSTORICDVO.ICDCM3DOC3 = ddlcm3doc3.SelectedValue;
                POSTORICDVO.ICDCM3DOC4 = ddlcm3doc4.SelectedValue;

                POSTORICDVO.ICDCM4DOC1 = ddlcm4doc1.SelectedValue;
                POSTORICDVO.ICDCM4DOC2 = ddlcm4doc2.SelectedValue;
                POSTORICDVO.ICDCM4DOC3 = ddlcm4doc3.SelectedValue;
                POSTORICDVO.ICDCM4DOC4 = ddlcm4doc4.SelectedValue;

                POSTORICDVO.ICDCM5DOC1 = ddlcm5doc1.SelectedValue;
                POSTORICDVO.ICDCM5DOC2 = ddlcm5doc2.SelectedValue;
                POSTORICDVO.ICDCM5DOC3 = ddlcm5doc3.SelectedValue;
                POSTORICDVO.ICDCM5DOC4 = ddlcm5doc4.SelectedValue;

                POSTORICDVO.Remark = txtICD_Remark.Text;
                if (string.IsNullOrEmpty(POSTORICDVO.ID))
                {
                    POSTORICDVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLPOSTORICD(dbInfo).Insert(POSTORICDVO);
                }
                else
                {
                    rtv = new BLPOSTORICD(dbInfo).Update(POSTORICDVO);
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue savePostORImplant()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                //POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                //POSTORIMPLANTVO.ORID = hdORID.Value;
                //string Suffix = hdSuffixImplant.Value == "" ? "0" : hdSuffixImplant.Value;
                //POSTORIMPLANTVO.Suffix = int.Parse(Suffix);
                //string ImplantType = ddlImplantType.SelectedValue == "" ? "0" : ddlImplantType.SelectedValue;
                //POSTORIMPLANTVO.ImplantType = int.Parse(ImplantType);
                //POSTORIMPLANTVO.Remark = txtRemarkImplant.Text;

                //List<POSTORIMPLANTVO> _lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
                //if (POSTORIMPLANTVO.Suffix > 0 && _lstPOSTORIMPLANTVO.Count > 0)
                //{
                //    rtv = new BLPOSTORIMPLANT(dbInfo).Update(POSTORIMPLANTVO);
                //}
                //else
                //{
                //    POSTORIMPLANTVO.Suffix = new BLPOSTORIMPLANT(dbInfo).GetSuffixNext(hdORID.Value);
                //    rtv = new BLPOSTORIMPLANT(dbInfo).Insert(POSTORIMPLANTVO);
                //}

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue savePostORComplication()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO = new POSTORCOMPLICATIONVO();
                POSTORCOMPLICATIONVO.ORID = hdORID.Value;
                POSTORCOMPLICATIONVO.ID = ddlComplicationID.SelectedValue;
                //POSTORCOMPLICATIONVO.ComplicationHeader = txtComplicationHeader.Text;
                POSTORCOMPLICATIONVO.ComplicationDetail = txtComplicationDetail.Text;
                List<POSTORCOMPLICATIONVO> _lstPOSTORCOMPLICATIONVO = new BLPOSTORCOMPLICATION(dbInfo).SearchByPrimary(hdORID.Value, ddlComplicationID.SelectedValue);
                if (_lstPOSTORCOMPLICATIONVO.Count > 0)
                {
                    rtv = new BLPOSTORCOMPLICATION(dbInfo).Update(POSTORCOMPLICATIONVO);
                }
                else
                {
                    //POSTORCOMPLICATIONVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLPOSTORCOMPLICATION(dbInfo).Insert(POSTORCOMPLICATIONVO);
                    //hdSuffixNurse.Value = POSTORNURSEVO.Suffix.ToString();
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue savePostORWarning()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                POSTORWARNINGVO POSTORWARNINGVO = new POSTORWARNINGVO();
                POSTORWARNINGVO.ORID = hdORID.Value;
                POSTORWARNINGVO.ID = hdWarningID.Value;
                POSTORWARNINGVO.Warning = txtWarning.Text;
                List<POSTORWARNINGVO> _lstPOSTORWARNINGVO = new BLPOSTORWARNING(dbInfo).SearchByPrimary(hdORID.Value, hdWarningID.Value);
                if (_lstPOSTORWARNINGVO.Count > 0)
                {
                    rtv = new BLPOSTORWARNING(dbInfo).Update(POSTORWARNINGVO);
                }
                else
                {
                    POSTORWARNINGVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLPOSTORWARNING(dbInfo).Insert(POSTORWARNINGVO);
                    //hdSuffixNurse.Value = POSTORNURSEVO.Suffix.ToString();
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue saveHNUnderly()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                HNUnderlyVO HNUnderlyVO = new HNUnderlyVO();
                HNUnderlyVO.HN = lblHN.Text;
                HNUnderlyVO.Underlyingtext = txtUnderlyingtext.Text;
                try { HNUnderlyVO.CreateUser = Session["USERID"].ToString(); } catch { }
                try { HNUnderlyVO.UpdateUser = Session["USERID"].ToString(); } catch { }
                List<HNUnderlyVO> _lstHNUnderlyVO = new BLHNUnderly(dbInfo).SearchByHN(HNUnderlyVO);
                if (_lstHNUnderlyVO.Count > 0)
                {
                    rtv = new BLHNUnderly(dbInfo).Update(HNUnderlyVO);
                }
                else
                {
                    rtv = new BLHNUnderly(dbInfo).Insert(HNUnderlyVO);
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ReturnValue saveORMigration()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                ORMIGRATIONVO ORMIGRATIONVO = new ORMIGRATIONVO();
                ORMIGRATIONVO.ID = hdMigrationID.Value;
                ORMIGRATIONVO.HN = lblHN.Text;
                ORMIGRATIONVO.ProcedureMemo = txtProcedureMemo.Text;
                ORMIGRATIONVO.Note = txtNote.Text;
                ORMIGRATIONVO.Surgeon = ddlORMigrationSurgeon.SelectedValue;
                ORMIGRATIONVO.ORRoom = ddlORMigrationORRoom.SelectedValue;
                ORMIGRATIONVO.Side = int.Parse(ddlORMigrationSide.SelectedValue);
                ORMIGRATIONVO.ORDate = DateTime.Parse(DateFormat.dmy2ymd(hdORMigrationDate.Value));
                List<ORMIGRATIONVO> _lstORMIGRATIONVO = new BLORMIGRATION(dbInfo).SearchByKey(ORMIGRATIONVO);
                if (!string.IsNullOrEmpty(ORMIGRATIONVO.ID) && _lstORMIGRATIONVO.Count > 0)
                {
                    rtv = new BLORMIGRATION(dbInfo).Update(ORMIGRATIONVO);
                }
                else
                {
                    ORMIGRATIONVO.ID = Guid.NewGuid().ToString();
                    rtv = new BLORMIGRATION(dbInfo).Insert(ORMIGRATIONVO);
                }

            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private ORHEADERVO getORHeader()
        {
            ORHEADERVO ORHEADERVO = new ORHEADERVO();
            try
            {
                ORHEADERVO.ORID = hdORID.Value;
                ORHEADERVO.HN = lblHN.Text;
                ORHEADERVO.PatientName = lblPatientName.Text;

                ORHEADERVO.PatientInfection = chbPatientInfection.Checked;
                ORHEADERVO.PatientType1 = chbPatientType1.Checked;
                ORHEADERVO.PatientType2 = chbPatientType2.Checked;
                ORHEADERVO.PatientUP = chbPatientUP.Checked;
                ORHEADERVO.Onmed = chOnmed.Checked;
                ORHEADERVO.OnmedNote = txtOnmed.Text;

                if (!string.IsNullOrEmpty(hdORDate.Value.ToString()))
                {
                    ORHEADERVO.ORDate = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdORDate.Value)), YearType.English);
                }
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
                ORHEADERVO.Prediag = txtPrediag.Text;
                ORHEADERVO.CreateDate = DateTime.Now;
                ORHEADERVO.CreateBy = Session["USERID"].ToString();
            }
            catch { }
            return ORHEADERVO;
        }

        private POSTORDETAILVO getPostORDetail()
        {
            POSTORDETAILVO val = new POSTORDETAILVO();
            try
            {
                val.ORID = hdORID.Value;
                //string SORTime = "00:00";
                //string FORTime = "00:00";
                //string SAnesTime = "00:00";
                //string FAnesTime = "00:00";
                //string SBlockTime = "00:00";
                //string FBlockTime = "00:00";
                //string SRecoveryTime = "00:00";
                //string FRecoveryTime = "00:00";
                //int error = 0;
                //try
                //{
                //    txtSORTime.Text = txtSORTime.Text.Replace(":", "").PadRight(4, '0');

                //    if (int.TryParse(txtSORTime.Text, out error))
                //    {
                //        SORTime = txtSORTime.Text.Substring(0, 2) + ":" + txtSORTime.Text.Substring(2, 2);
                //    }
                //}
                //catch { }
                //try
                //{
                //    txtFORTime.Text = txtFORTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtFORTime.Text, out error))
                //        FORTime = txtFORTime.Text.Substring(0, 2) + ":" + txtFORTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtSAnesTime.Text = txtSAnesTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtSAnesTime.Text, out error))
                //        SAnesTime = txtSAnesTime.Text.Substring(0, 2) + ":" + txtSAnesTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtFAnesTime.Text = txtFAnesTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtFAnesTime.Text, out error))
                //        FAnesTime = txtFAnesTime.Text.Substring(0, 2) + ":" + txtFAnesTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtSBlockTime.Text = txtSBlockTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtSBlockTime.Text, out error))
                //        SBlockTime = txtSBlockTime.Text.Substring(0, 2) + ":" + txtSBlockTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtFBlockTime.Text = txtFBlockTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtFBlockTime.Text, out error))
                //        FBlockTime = txtFBlockTime.Text.Substring(0, 2) + ":" + txtFBlockTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtSRecoveryTime.Text = txtSRecoveryTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtSRecoveryTime.Text, out error))
                //        SRecoveryTime = txtSRecoveryTime.Text.Substring(0, 2) + ":" + txtSRecoveryTime.Text.Substring(2, 2);
                //}
                //catch { }
                //try
                //{
                //    txtFRecoveryTime.Text = txtFRecoveryTime.Text.Replace(":", "").PadRight(4, '0');
                //    if (int.TryParse(txtFRecoveryTime.Text, out error))
                //        FRecoveryTime = txtFRecoveryTime.Text.Substring(0, 2) + ":" + txtFRecoveryTime.Text.Substring(2, 2);
                //}
                //catch { }

                if (!string.IsNullOrEmpty(hdSORDate.Value.ToString()))
                    val.StartORDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSORDate.Value) + " " + txtSORTime.Text), YearType.English);
                if (!string.IsNullOrEmpty(hdFORDate.Value.ToString()))
                    val.FinishORDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFORDate.Value) + " " + txtFORTime.Text), YearType.English);

                if (!string.IsNullOrEmpty(hdSAnesDate.Value.ToString()))
                    val.StartAnesDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSAnesDate.Value) + " " + txtSAnesTime.Text), YearType.English);
                if (!string.IsNullOrEmpty(hdFAnesDate.Value.ToString()))
                    val.FinishAnesDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFAnesDate.Value) + " " + txtFAnesTime.Text), YearType.English);

                if (!string.IsNullOrEmpty(hdSBlockDate.Value.ToString()))
                    val.StartBlockDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSBlockDate.Value) + " " + txtSBlockTime.Text), YearType.English);
                if (!string.IsNullOrEmpty(hdFBlockDate.Value.ToString()))
                    val.FinishBlockDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFBlockDate.Value) + " " + txtFBlockTime.Text), YearType.English);

                if (!string.IsNullOrEmpty(hdSRecoveryDate.Value.ToString()))
                    val.StartRecoveryDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSRecoveryDate.Value) + " " + txtSRecoveryTime.Text), YearType.English);
                if (!string.IsNullOrEmpty(hdFRecoveryDate.Value.ToString()))
                    val.FinishRecoveryDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFRecoveryDate.Value) + " " + txtFRecoveryTime.Text), YearType.English);


                //if (!string.IsNullOrEmpty(hdSORDate.Value.ToString()))
                //    val.StartORDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSORDate.Value) + " " + ddlSORTimeH.SelectedValue + ":" + ddlSORTimeM.SelectedValue), YearType.English);
                //if (!string.IsNullOrEmpty(hdFORDate.Value.ToString()))
                //    val.FinishORDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFORDate.Value) + " " + ddlFORTimeH.SelectedValue + ":" + ddlFORTimeM.SelectedValue), YearType.English);

                //if (!string.IsNullOrEmpty(hdSAnesDate.Value.ToString()))
                //    val.StartAnesDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSAnesDate.Value) + " " + ddlSAnesTimeH.SelectedValue + ":" + ddlSAnesTimeM.SelectedValue), YearType.English);
                //if (!string.IsNullOrEmpty(hdFAnesDate.Value.ToString()))
                //    val.FinishAnesDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFAnesDate.Value) + " " + ddlFAnesTimeH.SelectedValue + ":" + ddlFAnesTimeM.SelectedValue), YearType.English);

                //if (!string.IsNullOrEmpty(hdSBlockDate.Value.ToString()))
                //    val.StartBlockDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSBlockDate.Value) + " " + ddlSBlockTimeH.SelectedValue + ":" + ddlSBlockTimeM.SelectedValue), YearType.English);
                //if (!string.IsNullOrEmpty(hdFBlockDate.Value.ToString()))
                //    val.FinishBlockDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFBlockDate.Value) + " " + ddlFBlockTimeH.SelectedValue + ":" + ddlFBlockTimeM.SelectedValue), YearType.English);

                //if (!string.IsNullOrEmpty(hdSRecoveryDate.Value.ToString()))
                //    val.StartRecoveryDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdSRecoveryDate.Value) + " " + ddlSRecoveryTimeH.SelectedValue + ":" + ddlSRecoveryTimeM.SelectedValue), YearType.English);
                //if (!string.IsNullOrEmpty(hdFRecoveryDate.Value.ToString()))
                //    val.FinishRecoveryDateTime = CultureInfo.GetDatetime(DateTime.Parse(DateFormat.dmy2ymd(hdFRecoveryDate.Value) + " " + ddlFRecoveryTimeH.SelectedValue + ":" + ddlFRecoveryTimeM.SelectedValue), YearType.English);

                val.ORCaseType = int.Parse(ddlORCaseType.SelectedValue);
                val.ORWrongCase = int.Parse(ddlORWrongCase.SelectedValue);
                //val.ORWoundType = int.Parse(ddlORWoundType.SelectedValue);
                //val.ORUnplantType = int.Parse(ddlORUnplantType.SelectedValue);

                val.ORWoundType1 = chORWoundType1.Checked;
                val.ORWoundType2 = chORWoundType2.Checked;
                val.ORWoundType3 = chORWoundType3.Checked;
                val.ORWoundType4 = chORWoundType4.Checked;

                val.External = chExternal.Checked;
                val.Anterior = chAnterior.Checked;
                val.Posterior = chPosterior.Checked;

                val.ChangOperation = chChangOperation.Checked;
                val.HR48 = chHR48.Checked;
                val.Day30 = chDay30.Checked;
                val.Indicator = txtIndicator.Text;
            }
            catch { }
            return val;
        }

        private List<POSTORNURSEVO> getPostORNurse()
        {
            List<POSTORNURSEVO> lstval = new List<POSTORNURSEVO>();
            try
            {
                int j = 1;
                foreach (POSTORNURSEVO POSTORNURSEVO in GetListValue_gvPostORNurse())
                {
                    POSTORNURSEVO.ORID = hdORID.Value;
                    POSTORNURSEVO.Suffix = j;
                    lstval.Add(POSTORNURSEVO);
                    j++;
                }
            }
            catch { }
            return lstval;
        }

        private List<POSTORCOMPLICATIONVO> getPostORComplication()
        {
            List<POSTORCOMPLICATIONVO> lstval = new List<POSTORCOMPLICATIONVO>();
            try
            {
                //int j = 1;
                //foreach (POSTORCOMPLICATIONVO POSTORCOMPLICATIONVO in GetListValue_gvPostORComplication())
                //{
                //    POSTORCOMPLICATIONVO.ORID = hdORID.Value;
                //    POSTORCOMPLICATIONVO.Suffix = j;
                //    lstval.Add(POSTORCOMPLICATIONVO);
                //    j++;
                //}
            }
            catch { }
            return lstval;
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

                ddlORMigrationORRoom.DataSource = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                ddlORMigrationORRoom.DataValueField = "CODE";
                ddlORMigrationORRoom.DataTextField = "Name";
                ddlORMigrationORRoom.DataBind();


                // Room Type
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
                litAnesthesiaNurs3.Value = "";
                ddlAnesthesiaNurse3.DataSource = lstNURSEMASTERVO;
                ddlAnesthesiaNurse3.DataValueField = "CODE";
                ddlAnesthesiaNurse3.DataTextField = "NAME";
                ddlAnesthesiaNurse3.DataBind();
                ddlAnesthesiaNurse3.Items.Insert(0, litAnesthesiaNurs3);

                //Nurse Code
                ListItem litNurseCode = new ListItem();
                litNurseCode.Text = "None";
                litNurseCode.Value = "";
                ddlNurseCode.DataSource = lstNURSEMASTERVO;
                ddlNurseCode.DataValueField = "CODE";
                ddlNurseCode.DataTextField = "NAME";
                ddlNurseCode.DataBind();
                ddlNurseCode.Items.Insert(0, litNurseCode);

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

                // MIGRATIONORSide
                ddlORMigrationSide.DataSource = EnumOR.GetAllName<EnumOR.ORSide>();
                ddlORMigrationSide.DataTextField = "Value";
                ddlORMigrationSide.DataValueField = "Key";
                ddlORMigrationSide.DataBind();

                ddlORMigrationSide.SelectedValue = ((byte)EnumOR.ORSide.ยังไม่ระบุตา).ToString();

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


                    ListItem litSORTimeH = new ListItem();
                    litSORTimeH.Text = i.ToString().PadLeft(2, '0');
                    litSORTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlSORTimeH.Items.Add(litSORTimeH);

                    ListItem litFORTimeH = new ListItem();
                    litFORTimeH.Text = i.ToString().PadLeft(2, '0');
                    litFORTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlFORTimeH.Items.Add(litFORTimeH);

                    ListItem litSAnesTimeH = new ListItem();
                    litSAnesTimeH.Text = i.ToString().PadLeft(2, '0');
                    litSAnesTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlSAnesTimeH.Items.Add(litSAnesTimeH);

                    ListItem litFAnesTimeH = new ListItem();
                    litFAnesTimeH.Text = i.ToString().PadLeft(2, '0');
                    litFAnesTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlFAnesTimeH.Items.Add(litFAnesTimeH);

                    ListItem litSBlockTimeH = new ListItem();
                    litSBlockTimeH.Text = i.ToString().PadLeft(2, '0');
                    litSBlockTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlSBlockTimeH.Items.Add(litSBlockTimeH);

                    ListItem litFBlockTimeH = new ListItem();
                    litFBlockTimeH.Text = i.ToString().PadLeft(2, '0');
                    litFBlockTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlFBlockTimeH.Items.Add(litFBlockTimeH);

                    ListItem litSRecoveryTimeH = new ListItem();
                    litSRecoveryTimeH.Text = i.ToString().PadLeft(2, '0');
                    litSRecoveryTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlSRecoveryTimeH.Items.Add(litSRecoveryTimeH);

                    ListItem litFRecoveryTimeH = new ListItem();
                    litFRecoveryTimeH.Text = i.ToString().PadLeft(2, '0');
                    litFRecoveryTimeH.Value = i.ToString().PadLeft(2, '0');
                    ddlFRecoveryTimeH.Items.Add(litFRecoveryTimeH);
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

                    ListItem litSORTimeM = new ListItem();
                    litSORTimeM.Text = i.ToString().PadLeft(2, '0');
                    litSORTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlSORTimeM.Items.Add(litSORTimeM);

                    ListItem litFORTimeM = new ListItem();
                    litFORTimeM.Text = i.ToString().PadLeft(2, '0');
                    litFORTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlFORTimeM.Items.Add(litFORTimeM);

                    ListItem litSAnesTimeM = new ListItem();
                    litSAnesTimeM.Text = i.ToString().PadLeft(2, '0');
                    litSAnesTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlSAnesTimeM.Items.Add(litSAnesTimeM);

                    ListItem litFAnesTimeM = new ListItem();
                    litFAnesTimeM.Text = i.ToString().PadLeft(2, '0');
                    litFAnesTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlFAnesTimeM.Items.Add(litFAnesTimeM);

                    ListItem litSBlockTimeM = new ListItem();
                    litSBlockTimeM.Text = i.ToString().PadLeft(2, '0');
                    litSBlockTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlSBlockTimeM.Items.Add(litSBlockTimeM);

                    ListItem litFBlockTimeM = new ListItem();
                    litFBlockTimeM.Text = i.ToString().PadLeft(2, '0');
                    litFBlockTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlFBlockTimeM.Items.Add(litFBlockTimeM);

                    ListItem litSRecoveryTimeM = new ListItem();
                    litSRecoveryTimeM.Text = i.ToString().PadLeft(2, '0');
                    litSRecoveryTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlSRecoveryTimeM.Items.Add(litSRecoveryTimeM);

                    ListItem litFRecoveryTimeM = new ListItem();
                    litFRecoveryTimeM.Text = i.ToString().PadLeft(2, '0');
                    litFRecoveryTimeM.Value = i.ToString().PadLeft(2, '0');
                    ddlFRecoveryTimeM.Items.Add(litFRecoveryTimeM);
                }

                //OR Case Type
                ddlORCaseType.DataSource = EnumOR.GetORCaseType<EnumOR.ORCaseType>();
                ddlORCaseType.DataTextField = "Value";
                ddlORCaseType.DataValueField = "Key";
                ddlORCaseType.DataBind();

                //OR Wrong Case
                ddlORWrongCase.DataSource = EnumOR.GetORWrongCase<EnumOR.ORWrongCase>();
                ddlORWrongCase.DataTextField = "Value";
                ddlORWrongCase.DataValueField = "Key";
                ddlORWrongCase.DataBind();

                //OR Wrond Type
                //ddlORWoundType.DataSource = EnumOR.GetORWoundType<EnumOR.ORWoundType>();
                //ddlORWoundType.DataTextField = "Value";
                //ddlORWoundType.DataValueField = "Key";
                //ddlORWoundType.DataBind();

                //Unplant Type
                //ddlORUnplantType.DataSource = EnumOR.GetUnplantType<EnumOR.ORUnplantType>();
                //ddlORUnplantType.DataTextField = "Value";
                //ddlORUnplantType.DataValueField = "Key";
                //ddlORUnplantType.DataBind();

                //Nurse Type
                ddlNurseType.DataSource = EnumOR.GetNurseType<EnumOR.NurseType>();
                ddlNurseType.DataTextField = "Value";
                ddlNurseType.DataValueField = "Key";
                ddlNurseType.DataBind();

                //ICD
                //ListItem litddlPOSTORICD = new ListItem();
                //litddlPOSTORICD.Text = "None";
                //litddlPOSTORICD.Value = "";
                //SETUPICDVO SETUPICDVO = new SETUPICDVO();
                //List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchByKey(SETUPICDVO);
                //ddlPOSTORICD.DataSource = lstSETUPICDVO;
                //ddlPOSTORICD.DataValueField = "Code";
                //ddlPOSTORICD.DataTextField = "Name";
                //ddlPOSTORICD.DataBind();
                //ddlPOSTORICD.Items.Insert(0, litddlPOSTORICD);

                //ListItem litddlPOSTORICDCM1 = new ListItem();
                //litddlPOSTORICDCM1.Text = "None";
                //litddlPOSTORICDCM1.Value = "";
                //SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
                //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(SETUPICDCMVO);
                //ddlPOSTORICDCM1.DataSource = lstSETUPICDCMVO;
                //ddlPOSTORICDCM1.DataValueField = "Code";
                //ddlPOSTORICDCM1.DataTextField = "Name";
                //ddlPOSTORICDCM1.DataBind();
                //ddlPOSTORICDCM1.Items.Insert(0, litddlPOSTORICDCM1);

                //ListItem litddlPOSTORICDCM2 = new ListItem();
                //litddlPOSTORICDCM2.Text = "None";
                //litddlPOSTORICDCM2.Value = "";
                //SETUPICDCMVO = new SETUPICDCMVO();
                //lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(SETUPICDCMVO);
                //ddlPOSTORICDCM2.DataSource = lstSETUPICDCMVO;
                //ddlPOSTORICDCM2.DataValueField = "Code";
                //ddlPOSTORICDCM2.DataTextField = "Name";
                //ddlPOSTORICDCM2.DataBind();
                //ddlPOSTORICDCM2.Items.Insert(0, litddlPOSTORICDCM2);

                //ListItem litddlPOSTORICDCM3 = new ListItem();
                //litddlPOSTORICDCM3.Text = "None";
                //litddlPOSTORICDCM3.Value = "";
                //SETUPICDCMVO = new SETUPICDCMVO();
                //lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(SETUPICDCMVO);
                //ddlPOSTORICDCM3.DataSource = lstSETUPICDCMVO;
                //ddlPOSTORICDCM3.DataValueField = "Code";
                //ddlPOSTORICDCM3.DataTextField = "Name";
                //ddlPOSTORICDCM3.DataBind();
                //ddlPOSTORICDCM3.Items.Insert(0, litddlPOSTORICDCM3);

                //ListItem litddlPOSTORICDCM4 = new ListItem();
                //litddlPOSTORICDCM4.Text = "None";
                //litddlPOSTORICDCM4.Value = "";
                //SETUPICDCMVO = new SETUPICDCMVO();
                //lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(SETUPICDCMVO);
                //ddlPOSTORICDCM4.DataSource = lstSETUPICDCMVO;
                //ddlPOSTORICDCM4.DataValueField = "Code";
                //ddlPOSTORICDCM4.DataTextField = "Name";
                //ddlPOSTORICDCM4.DataBind();
                //ddlPOSTORICDCM4.Items.Insert(0, litddlPOSTORICDCM4);

                //ListItem litddlPOSTORICDCM5 = new ListItem();
                //litddlPOSTORICDCM5.Text = "None";
                //litddlPOSTORICDCM5.Value = "";
                //SETUPICDCMVO = new SETUPICDCMVO();
                //lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(SETUPICDCMVO);
                //ddlPOSTORICDCM5.DataSource = lstSETUPICDCMVO;
                //ddlPOSTORICDCM5.DataValueField = "Code";
                //ddlPOSTORICDCM5.DataTextField = "Name";
                //ddlPOSTORICDCM5.DataBind();
                //ddlPOSTORICDCM5.Items.Insert(0, litddlPOSTORICDCM5);

                //Implant Type
                //ddlImplantType.DataSource = EnumOR.GetImplantType<EnumOR.ImplantType>();
                //ddlImplantType.DataTextField = "Value";
                //ddlImplantType.DataValueField = "Key";
                //ddlImplantType.DataBind();

                ListItem litcm1doc1 = new ListItem();
                litcm1doc1.Text = "None";
                litcm1doc1.Value = "";
                DOCTORMASTERVO = new DOCTORMASTERVO();
                lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                ddlcm1doc1.DataSource = lstDOCTORMASTERVO;
                ddlcm1doc1.DataValueField = "DOCTOR";
                ddlcm1doc1.DataTextField = "DoctorName";
                ddlcm1doc1.DataBind();
                ddlcm1doc1.Items.Insert(0, litcm1doc1);

                ListItem litcm1doc2 = new ListItem();
                litcm1doc2.Text = "None";
                litcm1doc2.Value = "";
                ddlcm1doc2.DataSource = lstDOCTORMASTERVO;
                ddlcm1doc2.DataValueField = "DOCTOR";
                ddlcm1doc2.DataTextField = "DoctorName";
                ddlcm1doc2.DataBind();
                ddlcm1doc2.Items.Insert(0, litcm1doc2);

                ListItem litcm1doc3 = new ListItem();
                litcm1doc3.Text = "None";
                litcm1doc3.Value = "";
                ddlcm1doc3.DataSource = lstDOCTORMASTERVO;
                ddlcm1doc3.DataValueField = "DOCTOR";
                ddlcm1doc3.DataTextField = "DoctorName";
                ddlcm1doc3.DataBind();
                ddlcm1doc3.Items.Insert(0, litcm1doc3);

                ListItem litcm1doc4 = new ListItem();
                litcm1doc4.Text = "None";
                litcm1doc4.Value = "";
                ddlcm1doc4.DataSource = lstDOCTORMASTERVO;
                ddlcm1doc4.DataValueField = "DOCTOR";
                ddlcm1doc4.DataTextField = "DoctorName";
                ddlcm1doc4.DataBind();
                ddlcm1doc4.Items.Insert(0, litcm1doc4);

                ListItem litcm2doc1 = new ListItem();
                litcm2doc1.Text = "None";
                litcm2doc1.Value = "";
                ddlcm2doc1.DataSource = lstDOCTORMASTERVO;
                ddlcm2doc1.DataValueField = "DOCTOR";
                ddlcm2doc1.DataTextField = "DoctorName";
                ddlcm2doc1.DataBind();
                ddlcm2doc1.Items.Insert(0, litcm2doc1);

                ListItem litcm2doc2 = new ListItem();
                litcm2doc2.Text = "None";
                litcm2doc2.Value = "";
                ddlcm2doc2.DataSource = lstDOCTORMASTERVO;
                ddlcm2doc2.DataValueField = "DOCTOR";
                ddlcm2doc2.DataTextField = "DoctorName";
                ddlcm2doc2.DataBind();
                ddlcm2doc2.Items.Insert(0, litcm2doc2);

                ListItem litcm2doc3 = new ListItem();
                litcm2doc3.Text = "None";
                litcm2doc3.Value = "";
                ddlcm2doc3.DataSource = lstDOCTORMASTERVO;
                ddlcm2doc3.DataValueField = "DOCTOR";
                ddlcm2doc3.DataTextField = "DoctorName";
                ddlcm2doc3.DataBind();
                ddlcm2doc3.Items.Insert(0, litcm2doc3);

                ListItem litcm2doc4 = new ListItem();
                litcm2doc4.Text = "None";
                litcm2doc4.Value = "";
                ddlcm2doc4.DataSource = lstDOCTORMASTERVO;
                ddlcm2doc4.DataValueField = "DOCTOR";
                ddlcm2doc4.DataTextField = "DoctorName";
                ddlcm2doc4.DataBind();
                ddlcm2doc4.Items.Insert(0, litcm2doc4);

                ListItem litcm3doc1 = new ListItem();
                litcm3doc1.Text = "None";
                litcm3doc1.Value = "";
                ddlcm3doc1.DataSource = lstDOCTORMASTERVO;
                ddlcm3doc1.DataValueField = "DOCTOR";
                ddlcm3doc1.DataTextField = "DoctorName";
                ddlcm3doc1.DataBind();
                ddlcm3doc1.Items.Insert(0, litcm3doc1);

                ListItem litcm3doc2 = new ListItem();
                litcm3doc2.Text = "None";
                litcm3doc2.Value = "";
                ddlcm3doc2.DataSource = lstDOCTORMASTERVO;
                ddlcm3doc2.DataValueField = "DOCTOR";
                ddlcm3doc2.DataTextField = "DoctorName";
                ddlcm3doc2.DataBind();
                ddlcm3doc2.Items.Insert(0, litcm3doc2);

                ListItem litcm3doc3 = new ListItem();
                litcm3doc3.Text = "None";
                litcm3doc3.Value = "";
                ddlcm3doc3.DataSource = lstDOCTORMASTERVO;
                ddlcm3doc3.DataValueField = "DOCTOR";
                ddlcm3doc3.DataTextField = "DoctorName";
                ddlcm3doc3.DataBind();
                ddlcm3doc3.Items.Insert(0, litcm3doc3);

                ListItem litcm3doc4 = new ListItem();
                litcm3doc4.Text = "None";
                litcm3doc4.Value = "";
                ddlcm3doc4.DataSource = lstDOCTORMASTERVO;
                ddlcm3doc4.DataValueField = "DOCTOR";
                ddlcm3doc4.DataTextField = "DoctorName";
                ddlcm3doc4.DataBind();
                ddlcm3doc4.Items.Insert(0, litcm3doc4);

                ListItem litcm4doc1 = new ListItem();
                litcm4doc1.Text = "None";
                litcm4doc1.Value = "";
                ddlcm4doc1.DataSource = lstDOCTORMASTERVO;
                ddlcm4doc1.DataValueField = "DOCTOR";
                ddlcm4doc1.DataTextField = "DoctorName";
                ddlcm4doc1.DataBind();
                ddlcm4doc1.Items.Insert(0, litcm4doc1);

                ListItem litcm4doc2 = new ListItem();
                litcm4doc2.Text = "None";
                litcm4doc2.Value = "";
                ddlcm4doc2.DataSource = lstDOCTORMASTERVO;
                ddlcm4doc2.DataValueField = "DOCTOR";
                ddlcm4doc2.DataTextField = "DoctorName";
                ddlcm4doc2.DataBind();
                ddlcm4doc2.Items.Insert(0, litcm4doc2);

                ListItem litcm4doc3 = new ListItem();
                litcm4doc3.Text = "None";
                litcm4doc3.Value = "";
                ddlcm4doc3.DataSource = lstDOCTORMASTERVO;
                ddlcm4doc3.DataValueField = "DOCTOR";
                ddlcm4doc3.DataTextField = "DoctorName";
                ddlcm4doc3.DataBind();
                ddlcm4doc3.Items.Insert(0, litcm4doc3);

                ListItem litcm4doc4 = new ListItem();
                litcm4doc4.Text = "None";
                litcm4doc4.Value = "";
                ddlcm4doc4.DataSource = lstDOCTORMASTERVO;
                ddlcm4doc4.DataValueField = "DOCTOR";
                ddlcm4doc4.DataTextField = "DoctorName";
                ddlcm4doc4.DataBind();
                ddlcm4doc4.Items.Insert(0, litcm4doc4);

                ListItem litcm5doc1 = new ListItem();
                litcm5doc1.Text = "None";
                litcm5doc1.Value = "";
                ddlcm5doc1.DataSource = lstDOCTORMASTERVO;
                ddlcm5doc1.DataValueField = "DOCTOR";
                ddlcm5doc1.DataTextField = "DoctorName";
                ddlcm5doc1.DataBind();
                ddlcm5doc1.Items.Insert(0, litcm5doc1);

                ListItem litcm5doc2 = new ListItem();
                litcm5doc2.Text = "None";
                litcm5doc2.Value = "";
                ddlcm5doc2.DataSource = lstDOCTORMASTERVO;
                ddlcm5doc2.DataValueField = "DOCTOR";
                ddlcm5doc2.DataTextField = "DoctorName";
                ddlcm5doc2.DataBind();
                ddlcm5doc2.Items.Insert(0, litcm5doc2);

                ListItem litcm5doc3 = new ListItem();
                litcm5doc3.Text = "None";
                litcm5doc3.Value = "";
                ddlcm5doc3.DataSource = lstDOCTORMASTERVO;
                ddlcm5doc3.DataValueField = "DOCTOR";
                ddlcm5doc3.DataTextField = "DoctorName";
                ddlcm5doc3.DataBind();
                ddlcm5doc3.Items.Insert(0, litcm5doc3);

                ListItem litcm5doc4 = new ListItem();
                litcm5doc4.Text = "None";
                litcm5doc4.Value = "";
                ddlcm5doc4.DataSource = lstDOCTORMASTERVO;
                ddlcm5doc4.DataValueField = "DOCTOR";
                ddlcm5doc4.DataTextField = "DoctorName";
                ddlcm5doc4.DataBind();
                ddlcm5doc4.Items.Insert(0, litcm5doc4);

                // ORProcedureType
                ddlORProcedureType.DataSource = EnumOR.GetORProcedureType<EnumOR.ORProcedureType>();
                ddlORProcedureType.DataTextField = "Value";
                ddlORProcedureType.DataValueField = "Key";
                ddlORProcedureType.DataBind();

                SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                List<SETUPCOMPLICATIONVO> lstSETUPCOMPLICATIONVO = new BLSETUPCOMPLICATION(dbInfo).SearchByKey(SETUPCOMPLICATIONVO);
                ddlComplicationID.DataSource = lstSETUPCOMPLICATIONVO;
                ddlComplicationID.DataValueField = "ID";
                ddlComplicationID.DataTextField = "ComplicationHeader";
                ddlComplicationID.DataBind();
                ListItem litComplication = new ListItem();
                litComplication.Text = "None";
                litComplication.Value = "";
                ddlComplicationID.Items.Insert(0, litComplication);

                MapDDLREASON();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MapDDLREASON()
        {
            ListItem liVT_REASON = new ListItem();
            liVT_REASON.Text = "None";
            liVT_REASON.Value = "00";
            VT_REASONVO _VT_REASONVO = new VT_REASONVO();
            List<VT_REASONVO> lstVT_REASONVO = new BLVT_REASON(dbInfo).SearchByKey(_VT_REASONVO);
            ddlREASON.DataSource = lstVT_REASONVO;
            ddlREASON.DataValueField = "CODE";
            ddlREASON.DataTextField = "NAME";
            ddlREASON.DataBind();
            ddlREASON.Items.Insert(0, liVT_REASON);

            ListItem liVT_REASONOther = new ListItem();
            liVT_REASONOther.Text = "อื่นๆ";
            liVT_REASONOther.Value = "99";
            ddlREASON.Items.Add(liVT_REASONOther);
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

        private void loadgvNurseEmpty()
        {
            gvPostORNurse.DataSource = new List<POSTORNURSEVO>();
            gvPostORNurse.DataBind();
        }

        private void loadgvICDEmpty()
        {
            gvPostORICD.DataSource = new List<POSTORICDVO>();
            gvPostORICD.DataBind();
        }

        private void loadgvUnderPatientEmpty()
        {

        }

        private void loadNurseLaout()
        {
            try
            {
                POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
                POSTORNURSEVO.ORID = hdORID.Value;
                POSTORNURSEVO.Suffix = int.Parse(hdSuffixNurse.Value);
                List<POSTORNURSEVO> lstPOSTORNURSEVO = new BLPOSTORNURSE(dbInfo).SearchByKey(POSTORNURSEVO);
                foreach (POSTORNURSEVO xx in lstPOSTORNURSEVO)
                {
                    ddlNurseType.SelectedValue = xx.NurseType.ToString();
                    ddlNurseCode.SelectedValue = xx.NurseCode;
                    txtNurseRemark.Text = xx.Remark;
                }
            }
            catch { }
        }

        private void loadORMIGRATIONLaout()
        {
            try
            {
                ORMIGRATIONVO ORMIGRATIONVO = new ORMIGRATIONVO();
                ORMIGRATIONVO.ID = hdMigrationID.Value;
                List<ORMIGRATIONVO> lstORMIGRATIONVO = new BLORMIGRATION(dbInfo).SearchByKey(ORMIGRATIONVO);
                foreach (ORMIGRATIONVO xx in lstORMIGRATIONVO)
                {
                    ddlORMigrationSide.SelectedValue = xx.Side.ToString();
                    txtProcedureMemo.Text = xx.ProcedureMemo;
                    txtNote.Text = xx.Note;
                    ddlORMigrationSurgeon.SelectedValue = xx.Surgeon.ToString();
                    ddlORMigrationORRoom.SelectedValue = xx.ORRoom.ToString();
                    hdORMigrationDate.Value = xx.ORDate.Value.ToString("dd/MM/yyyy");
                    hdORMigrationDateEn.Value = CultureInfo.GetDateString(xx.ORDate.Value, YearType.English);
                    ScriptManager.RegisterStartupScript(this, GetType(), "key", "SetMigationORDate()", true);
                    //txtMigrationORDate.Value = xx.ORDate.Value.ToString("dd/MM/yyyy");
                }
            }
            catch { }
        }

        private void loadICDLaout()
        {
            try
            {
                POSTORICDVO POSTORICDVO = new POSTORICDVO();
                POSTORICDVO.ID = hdPOSTORICD_ID.Value;
                POSTORICDVO.ORID = hdORID.Value;
                List<POSTORICDVO> lstPOSTORICDVO = new BLPOSTORICD(dbInfo).SearchByKey(POSTORICDVO);
                foreach (POSTORICDVO xx in lstPOSTORICDVO)
                {
                    hdPOSTORICD_ID.Value = xx.ID;

                    if (!string.IsNullOrEmpty(xx.ICD))
                    {
                        ddlICD.SelectedValue = xx.ICD;
                        SETUPICDVO icdvo = new SETUPICDVO();
                        icdvo.Code = xx.ICD;
                        List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchByKey(icdvo);
                        if (lstSETUPICDVO.Count > 0)
                        {
                            ddlICD.SelectedValue = xx.ICD;
                        }
                    }

                    if (!string.IsNullOrEmpty(xx.ICDCM1))
                    {
                        ddlICDCM1.SelectedValue = xx.ICDCM1;
                        //SETUPICDCMVO icdcmvo = new SETUPICDCMVO();
                        //icdcmvo.Code = xx.ICDCM1;
                        //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdcmvo);
                        //if (lstSETUPICDCMVO.Count > 0)
                        //    txtICDCM1.Text = xx.ICDCM1 + " : " + lstSETUPICDCMVO[0].Name;
                    }
                    if (!string.IsNullOrEmpty(xx.ICDCM2))
                    {
                        ddlICDCM2.SelectedValue = xx.ICDCM2;
                        //SETUPICDCMVO icdcmvo = new SETUPICDCMVO();
                        //icdcmvo.Code = xx.ICDCM2;
                        //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdcmvo);
                        //if (lstSETUPICDCMVO.Count > 0)
                        //    txtICDCM2.Text = xx.ICDCM2 + " : " + lstSETUPICDCMVO[0].Name;
                    }
                    if (!string.IsNullOrEmpty(xx.ICDCM3))
                    {
                        ddlICDCM3.SelectedValue = xx.ICDCM3;
                        //SETUPICDCMVO icdcmvo = new SETUPICDCMVO();
                        //icdcmvo.Code = xx.ICDCM3;
                        //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdcmvo);
                        //if (lstSETUPICDCMVO.Count > 0)
                        //    txtICDCM3.Text = xx.ICDCM3 + " : " + lstSETUPICDCMVO[0].Name;
                    }
                    if (!string.IsNullOrEmpty(xx.ICDCM4))
                    {
                        ddlICDCM4.SelectedValue = xx.ICDCM4;
                        //SETUPICDCMVO icdcmvo = new SETUPICDCMVO();
                        //icdcmvo.Code = xx.ICDCM4;
                        //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdcmvo);
                        //if (lstSETUPICDCMVO.Count > 0)
                        //    txtICDCM4.Text = xx.ICDCM4 + " : " + lstSETUPICDCMVO[0].Name;
                    }

                    if (!string.IsNullOrEmpty(xx.ICDCM5))
                    {
                        ddlICDCM5.SelectedValue = xx.ICDCM5;
                        //SETUPICDCMVO icdcmvo = new SETUPICDCMVO();
                        //icdcmvo.Code = xx.ICDCM5;
                        //List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchByKey(icdcmvo);
                        //if (lstSETUPICDCMVO.Count > 0)
                        //    txtICDCM5.Text = xx.ICDCM5 + " : " + lstSETUPICDCMVO[0].Name;
                    }
                    ddlcm1doc1.SelectedValue = xx.ICDCM1DOC1;
                    ddlcm1doc2.SelectedValue = xx.ICDCM1DOC2;
                    ddlcm1doc3.SelectedValue = xx.ICDCM1DOC3;
                    ddlcm1doc4.SelectedValue = xx.ICDCM1DOC4;

                    ddlcm2doc1.SelectedValue = xx.ICDCM2DOC1;
                    ddlcm2doc2.SelectedValue = xx.ICDCM2DOC2;
                    ddlcm2doc3.SelectedValue = xx.ICDCM2DOC3;
                    ddlcm2doc4.SelectedValue = xx.ICDCM2DOC4;

                    ddlcm3doc1.SelectedValue = xx.ICDCM3DOC1;
                    ddlcm3doc2.SelectedValue = xx.ICDCM3DOC2;
                    ddlcm3doc3.SelectedValue = xx.ICDCM3DOC3;
                    ddlcm3doc4.SelectedValue = xx.ICDCM3DOC4;

                    ddlcm4doc1.SelectedValue = xx.ICDCM4DOC1;
                    ddlcm4doc2.SelectedValue = xx.ICDCM4DOC2;
                    ddlcm4doc3.SelectedValue = xx.ICDCM4DOC3;
                    ddlcm4doc4.SelectedValue = xx.ICDCM4DOC4;

                    ddlcm5doc1.SelectedValue = xx.ICDCM5DOC1;
                    ddlcm5doc2.SelectedValue = xx.ICDCM5DOC2;
                    ddlcm5doc3.SelectedValue = xx.ICDCM5DOC3;
                    ddlcm5doc4.SelectedValue = xx.ICDCM5DOC4;

                    txtICD_Remark.Text = xx.Remark;
                }
            }
            catch { }
        }

        private void loadICDLaoutEmpty()
        {
            try
            {

                List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByORID(hdORID.Value);
                int i = 1;
                foreach (POSTOROPERATIONVO xPOSTOROPERATIONVO in lstPOSTOROPERATIONVO)
                {
                    if (i == 1)
                    {
                        ddlcm1doc1.SelectedValue = xPOSTOROPERATIONVO.Surgeon1;
                        ddlcm1doc2.SelectedValue = xPOSTOROPERATIONVO.Surgeon2;
                        ddlcm1doc3.SelectedValue = xPOSTOROPERATIONVO.Surgeon3;
                    }
                    else if (i == 2)
                    {
                        ddlcm2doc1.SelectedValue = xPOSTOROPERATIONVO.Surgeon1;
                        ddlcm2doc2.SelectedValue = xPOSTOROPERATIONVO.Surgeon2;
                        ddlcm2doc3.SelectedValue = xPOSTOROPERATIONVO.Surgeon3;
                    }
                    else if (i == 3)
                    {
                        ddlcm3doc1.SelectedValue = xPOSTOROPERATIONVO.Surgeon1;
                        ddlcm3doc2.SelectedValue = xPOSTOROPERATIONVO.Surgeon2;
                        ddlcm3doc3.SelectedValue = xPOSTOROPERATIONVO.Surgeon3;
                    }
                    else if (i == 4)
                    {
                        ddlcm4doc1.SelectedValue = xPOSTOROPERATIONVO.Surgeon1;
                        ddlcm4doc2.SelectedValue = xPOSTOROPERATIONVO.Surgeon2;
                        ddlcm4doc3.SelectedValue = xPOSTOROPERATIONVO.Surgeon3;
                    }
                    else if (i == 5)
                    {
                        ddlcm5doc1.SelectedValue = xPOSTOROPERATIONVO.Surgeon1;
                        ddlcm5doc2.SelectedValue = xPOSTOROPERATIONVO.Surgeon2;
                        ddlcm5doc3.SelectedValue = xPOSTOROPERATIONVO.Surgeon3;
                    }
                    i++;

                    SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                    SETUPOPERATIONSUBVO.MainCode = xPOSTOROPERATIONVO.MainCode;
                    SETUPOPERATIONSUBVO.SubCode = xPOSTOROPERATIONVO.SubCode;
                    List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(SETUPOPERATIONSUBVO);
                    if (lstSETUPOPERATIONSUBVO.Count > 0)
                    {
                        if (string.IsNullOrEmpty(ddlICDCM1.SelectedValue))
                        {
                            ddlICDCM1.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;
                        }
                        else if (string.IsNullOrEmpty(ddlICDCM2.SelectedValue))
                        {
                            ddlICDCM2.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;
                        }
                        else if (string.IsNullOrEmpty(ddlICDCM3.SelectedValue))
                        {
                            ddlICDCM3.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;

                        }
                        else if (string.IsNullOrEmpty(ddlICDCM4.SelectedValue))
                        {
                            ddlICDCM4.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;
                        }
                        else if (string.IsNullOrEmpty(ddlICDCM5.SelectedValue))
                        {
                            ddlICDCM5.SelectedValue = lstSETUPOPERATIONSUBVO[0].ICDCM;
                        }
                    }
                }

            }
            catch { }
        }

        private void loadImplantLaout()
        {
            try
            {
                //POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                //POSTORIMPLANTVO.ORID = hdORID.Value;
                //POSTORIMPLANTVO.Suffix = int.Parse(hdSuffixImplant.Value);
                //List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
                //foreach (POSTORIMPLANTVO xx in lstPOSTORIMPLANTVO)
                //{
                //    ddlImplantType.SelectedValue = xx.ImplantType.ToString();
                //    txtRemarkImplant.Text = xx.Remark;
                //}
            }
            catch { }
        }

        private void loadComplicationLaout()
        {
            try
            {
                ddlComplicationID.Enabled = false;
                List<POSTORCOMPLICATIONVO> lstPOSTORCOMPLICATIONVO = new BLPOSTORCOMPLICATION(dbInfo).SearchByPrimary(hdORID.Value, hdComplicationID.Value);
                foreach (POSTORCOMPLICATIONVO xx in lstPOSTORCOMPLICATIONVO)
                {
                    ddlComplicationID.SelectedValue = xx.ID;
                    txtComplicationDetail.Text = xx.ComplicationDetail;
                }
            }
            catch { }
        }

        private void loadWarningLaout()
        {
            try
            {
                POSTORWARNINGVO POSTORWARNINGVO = new POSTORWARNINGVO();
                POSTORWARNINGVO.ORID = hdORID.Value;
                POSTORWARNINGVO.ID = hdWarningID.Value;
                List<POSTORWARNINGVO> lstPOSTORWARNINGVO = new BLPOSTORWARNING(dbInfo).SearchByKey(POSTORWARNINGVO);
                foreach (POSTORWARNINGVO xx in lstPOSTORWARNINGVO)
                {
                    txtWarning.Text = xx.Warning;
                }
            }
            catch { }
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
                List<POSTOROPERATIONVO> _lstor = GetListValue_gvOROperatoin();

                int rowIndex = Convert.ToInt32(e.CommandArgument);

                POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                if (rowIndex > -1)
                {
                    GridViewRow row = gvOROperationSub.Rows[rowIndex];
                    POSTOROPERATIONVO = GetValue_gvOROperatoinSub(row);
                }
                else
                {
                    POSTOROPERATIONVO = GetValue_gvOROperatoinSubFt();
                    (gvOROperationSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text = string.Empty;
                }

                POSTOROPERATIONVO.ID = Guid.NewGuid().ToString();
                POSTOROPERATIONVO.ORID = hdORID.Value;
                POSTOROPERATIONVO.Seq = gvOROperation.Rows.Count + 1;

                new BLPOSTOROPERATION(dbInfo).Insert(POSTOROPERATIONVO);
                _lstor.Add(POSTOROPERATIONVO);
                gvOROperation.DataSource = _lstor;
                gvOROperation.DataBind();
                //SetButtonUpDown();

                LoadlblProcedure();
            }
        }

        private void LoadlblProcedure()
        {
            lblProcedureRE.Text = string.Empty;
            lblProcedureLE.Text = string.Empty;
            lblProcedureBE.Text = string.Empty;
            lblProcedureNone.Text = string.Empty;
            lblProcedureUnknown.Text = string.Empty;
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
                if (e.CommandName == "setup")
                {
                    lblSide.Text = string.Empty;
                    lblTypeOfOperation.Text = string.Empty;
                    lblProcedure.Text = string.Empty;
                    ddlProSurgeon1.SelectedIndex = ddlSurgeon1.SelectedIndex;
                    ddlProSurgeon2.SelectedIndex = ddlSurgeon2.SelectedIndex;
                    ddlProSurgeon3.SelectedIndex = ddlSurgeon3.SelectedIndex;
                    ddlComplication.SelectedIndex = 0;
                    txtComplication.Text = string.Empty;
                    // ddlICD.SelectedIndex = 0;
                    ddlOrgan.SelectedIndex = 0;
                    ddlORProcedureType.SelectedIndex = 0;
                    LoadLaoutSetupProcedure(e);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalProcedure();", true);
                }
                else if (e.CommandName == "implant")
                {
                    int irow = int.Parse(e.CommandArgument.ToString());
                    GridViewRow row = gvOROperation.Rows[irow];
                    //hdPostOperation_ID.Value = ((HiddenField)row.FindControl("hdPostOperation_ID")).Value;
                    hdPostOperation_ID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                    loadgvImplantMain();
                    loadgvImplantSubEmpty();
                    loadImplant(hdPostOperation_ID.Value);
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
                POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                POSTOROPERATIONVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                new BLPOSTOROPERATION(dbInfo).DeleteByID(POSTOROPERATIONVO);

                List<POSTOROPERATIONVO> _lstor = GetListValue_gvOROperatoin();
                _lstor.RemoveAt(e.RowIndex);
                gvOROperation.DataSource = _lstor;
                gvOROperation.DataBind();
                //SetButtonUpDown();
                LoadlblProcedure();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvOROperation_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    string hdgvID = ((HiddenField)e.Row.FindControl("hdgvID")).Value;

            //    if (string.IsNullOrEmpty(hdgvID))
            //    {
            //        ((Button)e.Row.FindControl("btnSetup")).Visible = false;
            //        ((Button)e.Row.FindControl("btnImplant")).Visible = false;
            //    }
            //}
        }

        protected void gvPostORNurse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvPostORNurse.Rows[index];
                HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                int suffix = int.Parse(hdsufiix.Value);
                ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                if (rv.Value)
                {
                    loadPostORNurse(hdORID.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPostORNurse_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                foreach (GridViewRow r in gvPostORNurse.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvPostORNurse.Rows[e.NewEditIndex];

                hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadNurseLaout();
            }
            catch { }
        }

        protected void gvPostORNurse_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                //int index = Convert.ToInt32(e.CommandArgument);  
                //GridViewRow row = gvPostORNurse.Rows[index];

                //hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;

                //loadNurseLaout();

            }
            else if (e.CommandName == "delete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvPostORNurse.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORNurse(hdORID.Value);
                //}
            }
        }

        protected void gvPostORICD_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvPostORICD.Rows[index];
                HiddenField hdgvID = (HiddenField)row.FindControl("hdgvID");
                ReturnValue rv = new BLPOSTORICD(dbInfo).Delete(hdORID.Value, hdgvID.Value);
                if (rv.Value)
                {
                    loadPostORICD(hdORID.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPostORICD_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                ClearLaoutICD();
                foreach (GridViewRow r in gvPostORICD.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvPostORICD.Rows[e.NewEditIndex];

                hdPOSTORICD_ID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadICDLaout();
            }
            catch { }
        }

        protected void gvPostORICD_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                //int index = Convert.ToInt32(e.CommandArgument);  
                //GridViewRow row = gvPostORNurse.Rows[index];

                //hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;

                //loadNurseLaout();

            }
            else if (e.CommandName == "delete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvPostORNurse.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORNurse(hdORID.Value);
                //}
            }
        }

        protected void gvPostORImplant_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                //int index = Convert.ToInt32(e.RowIndex);
                //GridViewRow row = gvPostORImplant.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORImplant(hdORID.Value);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvPostORImplant_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //foreach (GridViewRow r in gvPostORImplant.Rows)
                //{
                //    if (r.RowType == DataControlRowType.DataRow)
                //    {
                //        r.BackColor = System.Drawing.Color.White;
                //    }
                //}

                //GridViewRow row = gvPostORImplant.Rows[e.NewEditIndex];

                //hdSuffixImplant.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;
                //row.BackColor = System.Drawing.Color.LightGray;

                //loadImplantLaout();
            }
            catch { }
        }

        protected void gvComplication_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvComplication.Rows[index];
                //HiddenField hdORID = (HiddenField)row.FindControl("hdORID");
                HiddenField hdID = (HiddenField)row.FindControl("hdID");
                ReturnValue rv = new BLPOSTORCOMPLICATION(dbInfo).Delete(hdORID.Value, hdID.Value);
                if (rv.Value)
                {
                    loadPostORComplication(hdORID.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvComplication_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                foreach (GridViewRow r in gvComplication.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvComplication.Rows[e.NewEditIndex];
                hdComplicationID.Value = ((HiddenField)row.FindControl("hdID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadComplicationLaout();
            }
            catch { }
        }

        protected void gvComplication_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                //int index = Convert.ToInt32(e.CommandArgument);  
                //GridViewRow row = gvPostORNurse.Rows[index];

                //hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;

                //loadNurseLaout();

            }
            else if (e.CommandName == "delete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvPostORNurse.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORNurse(hdORID.Value);
                //}
            }
        }

        protected void gvWarning_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvWarning.Rows[index];
                //HiddenField hdORID = (HiddenField)row.FindControl("hdORID");
                HiddenField hdID = (HiddenField)row.FindControl("hdID");
                ReturnValue rv = new BLPOSTORWARNING(dbInfo).Delete(hdORID.Value, hdID.Value);
                if (rv.Value)
                {
                    loadPostORWarning(hdORID.Value);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvWarning_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                foreach (GridViewRow r in gvWarning.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvWarning.Rows[e.NewEditIndex];
                hdWarningID.Value = ((HiddenField)row.FindControl("hdID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadWarningLaout();
            }
            catch { }
        }

        protected void gvWarning_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                //int index = Convert.ToInt32(e.CommandArgument);  
                //GridViewRow row = gvPostORNurse.Rows[index];

                //hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;

                //loadNurseLaout();

            }
            else if (e.CommandName == "delete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvPostORNurse.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORNurse(hdORID.Value);
                //}
            }
        }

        protected void gvORMigration_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = gvORMigration.Rows[index];
                HiddenField hdID = (HiddenField)row.FindControl("hdgvID");
                ReturnValue rv = new BLORMIGRATION(dbInfo).Delete(hdID.Value);
                if (rv.Value)
                {
                    loadORMIGRATION(lblHN.Text);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvORMigration_RowEditing(object sender, GridViewEditEventArgs e)
        {

            try
            {
                foreach (GridViewRow r in gvORMigration.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }

                GridViewRow row = gvORMigration.Rows[e.NewEditIndex];

                hdMigrationID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                row.BackColor = System.Drawing.Color.LightGray;

                loadORMIGRATIONLaout();
            }
            catch { }
        }

        protected void gvORMigration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                //int index = Convert.ToInt32(e.CommandArgument);  
                //GridViewRow row = gvPostORNurse.Rows[index];

                //hdSuffixNurse.Value = ((HiddenField)row.FindControl("hdgvSuffix")).Value;

                //loadNurseLaout();

            }
            else if (e.CommandName == "delete")
            {
                //int index = Convert.ToInt32(e.CommandArgument);
                //GridViewRow row = gvPostORNurse.Rows[index];
                //HiddenField hdsufiix = (HiddenField)row.FindControl("hdgvSuffix");
                //int suffix = int.Parse(hdsufiix.Value);
                //ReturnValue rv = new BLPOSTORNURSE(dbInfo).Delete(hdORID.Value, suffix);
                //if (rv.Value)
                //{
                //    loadPostORNurse(hdORID.Value);
                //}
            }
        }

        private POSTOROPERATIONVO GetValue_gvOROperatoinSubFt()
        {
            POSTOROPERATIONVO _or = new POSTOROPERATIONVO();
            _or.MainCode = hdMainCode.Value;
            _or.Name = hdMainName.Value;
            _or.SubName = (gvOROperationSub.FooterRow.FindControl("txtgvSubNameFt") as TextBox).Text;
            _or.SubMark = "0";
            _or.Side = int.Parse(ddlORSide.SelectedValue);
            _or.strSide = ((EnumOR.ORSide)_or.Side).ToString();

            return _or;
        }

        private POSTOROPERATIONVO GetValue_gvOROperatoinSub(GridViewRow row)
        {
            POSTOROPERATIONVO _or = new POSTOROPERATIONVO();
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

        private List<POSTOROPERATIONVO> GetListValue_gvOROperatoin()
        {
            List<POSTOROPERATIONVO> _lstor = new List<POSTOROPERATIONVO>();
            int i = 1;
            foreach (GridViewRow drow in gvOROperation.Rows)
            {
                POSTOROPERATIONVO _or = new POSTOROPERATIONVO();

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

        private List<POSTORNURSEVO> GetListValue_gvPostORNurse()
        {
            List<POSTORNURSEVO> _lstor = new List<POSTORNURSEVO>();
            int i = 1;
            foreach (GridViewRow drow in gvPostORNurse.Rows)
            {
                POSTORNURSEVO _or = new POSTORNURSEVO();
                _or.ORID = hdORID.Value;
                _or.Suffix = i;
                _or.NurseType = (int)EnumOR.NurseType.Parse(typeof(EnumOR.NurseType), (drow.FindControl("hdgvNurseTypeCode") as HiddenField).Value);
                _or.NurseCode = (drow.FindControl("hdgvNurseCode") as HiddenField).Value;
                _or.Remark = (drow.FindControl("lblgvRemark") as Label).Text;
                _lstor.Add(_or);
                i++;
            }

            return _lstor;
        }

        private List<POSTORCOMPLICATIONVO> GetListValue_gvPostORComplication()
        {
            List<POSTORCOMPLICATIONVO> _lstor = new List<POSTORCOMPLICATIONVO>();
            //int i = 1;
            //foreach (GridViewRow drow in gvPostORComplication.Rows)
            //{
            //    POSTORCOMPLICATIONVO _or = new POSTORCOMPLICATIONVO();
            //    _or.ORID = hdORID.Value;
            //    _or.Suffix = i;
            //    _or.ComplicationHeader = (drow.FindControl("lblgvComplicationHeader") as Label).Text;
            //    _or.ComplicationDetail = (drow.FindControl("lblgvComplicationDetail") as Label).Text;
            //    _lstor.Add(_or);
            //    i++;
            //}

            return _lstor;
        }

        private void LoadLaoutSetupProcedure(GridViewCommandEventArgs e)
        {
            try
            {
                foreach (GridViewRow r in gvOROperation.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        r.BackColor = System.Drawing.Color.White;
                    }
                }
                int irow = int.Parse(e.CommandArgument.ToString());
                GridViewRow row = gvOROperation.Rows[irow];

                row.BackColor = System.Drawing.Color.LightGray;
                hdPostOperation_ID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;
                lblSide.Text = ((Label)row.FindControl("lblgvSide")).Text;
                lblTypeOfOperation.Text = ((Label)row.FindControl("lblgvName")).Text;
                lblProcedure.Text = ((Label)row.FindControl("lblgvstrSubMark")).Text + ((Label)row.FindControl("lblgvSubName")).Text;
                string MainCode = ((HiddenField)row.FindControl("hdgvMainCode")).Value;
                string SubCode = ((HiddenField)row.FindControl("hdgvSubCode")).Value;

                string _id = hdPostOperation_ID.Value;
                List<POSTOROPERATIONVO> lstPOSTOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByID(_id);
                foreach (POSTOROPERATIONVO _POSTVO in lstPOSTOROPERATIONVO)
                {
                    ddlProSurgeon1.SelectedValue = _POSTVO.Surgeon1;
                    ddlProSurgeon2.SelectedValue = _POSTVO.Surgeon2;
                    ddlProSurgeon3.SelectedValue = _POSTVO.Surgeon3;
                    ddlComplication.SelectedValue = _POSTVO.Complication;
                    if (_POSTVO.Complication == string.Empty)
                    {
                        divComplicationDoctor.Visible = false;
                        txtComplication.Visible = false;
                        txtComplication.Text = string.Empty;
                    }
                    else
                    {
                        divComplicationDoctor.Visible = true;
                        txtComplication.Visible = true;
                        txtComplication.Text = _POSTVO.strComplication;
                    }
                    // ddlICD.SelectedValue = _POSTVO.ICD;

                    if (!string.IsNullOrEmpty(_POSTVO.Organ) && _POSTVO.Organ != "0")
                    {
                        ddlOrgan.SelectedValue = _POSTVO.Organ;
                        MapDllORGANSUB(ddlOrgan.SelectedValue);
                        ddlOrganPosition.SelectedValue = _POSTVO.OrganPosition;
                    }
                    else
                    {
                        SETUPOPERATIONSUBVO sSETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                        sSETUPOPERATIONSUBVO.MainCode = MainCode;
                        sSETUPOPERATIONSUBVO.SubCode = SubCode;
                        List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(sSETUPOPERATIONSUBVO);
                        if (lstSETUPOPERATIONSUBVO.Count > 0)
                        {
                            ddlOrgan.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORGANMAIN;
                            MapDllORGANSUB(ddlOrgan.SelectedValue);
                            ddlOrganPosition.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORGANSUB;
                        }
                    }

                    if (!string.IsNullOrEmpty(_POSTVO.ORProcedureType) && _POSTVO.ORProcedureType != "0")
                    { ddlORProcedureType.SelectedValue = _POSTVO.ORProcedureType; }
                    else
                    {
                        SETUPOPERATIONSUBVO sSETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
                        sSETUPOPERATIONSUBVO.MainCode = MainCode;
                        sSETUPOPERATIONSUBVO.SubCode = SubCode;
                        List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(sSETUPOPERATIONSUBVO);
                        if (lstSETUPOPERATIONSUBVO.Count > 0)
                        {
                            ddlORProcedureType.SelectedValue = lstSETUPOPERATIONSUBVO[0].ORProcedureType.Value.ToString();
                        }
                    }
                }


            }
            catch (Exception ex)
            { }
        }

        protected void btnSaveSetupProcedure_Click(object sender, EventArgs e)
        {
            ReturnValue rtv = saveSetupProcedure();
            if (rtv.Value)
            {
                Response.Redirect(Request.RawUrl, true);
                AlertMessage(true, false, "Update Complete.");
                //Response.Redirect("/PostOR/?m=complete", false);
            }
        }

        private ReturnValue saveSetupProcedure()
        {
            ReturnValue rtv = new ReturnValue();
            try
            {
                POSTOROPERATIONVO POSTOROPERATIONVO = new POSTOROPERATIONVO();
                POSTOROPERATIONVO.ID = hdPostOperation_ID.Value;
                POSTOROPERATIONVO.Surgeon1 = ddlProSurgeon1.SelectedValue;
                POSTOROPERATIONVO.Surgeon2 = ddlProSurgeon2.SelectedValue;
                POSTOROPERATIONVO.Surgeon3 = ddlProSurgeon3.SelectedValue;
                POSTOROPERATIONVO.Complication = ddlComplication.SelectedValue;
                POSTOROPERATIONVO.strComplication = txtComplication.Text;
                // POSTOROPERATIONVO.ICD = ddlICD.SelectedValue;
                POSTOROPERATIONVO.Organ = ddlOrgan.SelectedValue;
                POSTOROPERATIONVO.OrganPosition = ddlOrganPosition.SelectedValue;
                POSTOROPERATIONVO.ORProcedureType = ddlORProcedureType.SelectedValue;
                rtv = new BLPOSTOROPERATION(dbInfo).UpdateSetupProcedure(POSTOROPERATIONVO);

                // Implant
                //new BLPOSTOROPERATION(dbInfo).DeleteByORID(hdORID.Value);
                //foreach (POSTOROPERATIONVO POSTOROPERATIONVO in GetListValue_gvOROperatoin())
                //{
                //    if (string.IsNullOrEmpty(POSTOROPERATIONVO.ID))
                //    {
                //        POSTOROPERATIONVO.ID = Guid.NewGuid().ToString();
                //        rtv = new BLPOSTOROPERATION(dbInfo).Insert(POSTOROPERATIONVO);
                //    }
                //    else
                //    {
                //        rtv = new BLPOSTOROPERATION(dbInfo).Insert(POSTOROPERATIONVO);
                //    }
                //}
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalProcedure();", true);
            }
            catch (Exception ex)
            {
                divError.Visible = true;
                lblMessageError.Text = ex.Message;
            }
            return rtv;
        }

        private void MapDDLPro()
        {
            try
            {
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(DOCTORMASTERVO);
                ddlProSurgeon1.DataSource = lstDOCTORMASTERVO;
                ddlProSurgeon1.DataValueField = "DOCTOR";
                ddlProSurgeon1.DataTextField = "DoctorName";
                ddlProSurgeon1.DataBind();
                ListItem litSurgeon1 = new ListItem();
                litSurgeon1.Text = "None";
                litSurgeon1.Value = "";
                ddlProSurgeon1.Items.Insert(0, litSurgeon1);

                ListItem litSurgeon2 = new ListItem();
                litSurgeon2.Text = "None";
                litSurgeon2.Value = "";
                ddlProSurgeon2.DataSource = lstDOCTORMASTERVO;
                ddlProSurgeon2.DataValueField = "DOCTOR";
                ddlProSurgeon2.DataTextField = "DoctorName";
                ddlProSurgeon2.DataBind();
                ddlProSurgeon2.Items.Insert(0, litSurgeon2);

                ListItem litSurgeon3 = new ListItem();
                litSurgeon3.Text = "None";
                litSurgeon3.Value = "";
                ddlProSurgeon3.DataSource = lstDOCTORMASTERVO;
                ddlProSurgeon3.DataValueField = "DOCTOR";
                ddlProSurgeon3.DataTextField = "DoctorName";
                ddlProSurgeon3.DataBind();
                ddlProSurgeon3.Items.Insert(0, litSurgeon3);

                ListItem litSurgeonMigration = new ListItem();
                litSurgeonMigration.Text = "None";
                litSurgeonMigration.Value = "";
                ddlORMigrationSurgeon.DataSource = lstDOCTORMASTERVO;
                ddlORMigrationSurgeon.DataValueField = "DOCTOR";
                ddlORMigrationSurgeon.DataTextField = "DoctorName";
                ddlORMigrationSurgeon.DataBind();
                ddlORMigrationSurgeon.Items.Insert(0, litSurgeonMigration);

                SETUPCOMPLICATIONVO SETUPCOMPLICATIONVO = new SETUPCOMPLICATIONVO();
                List<SETUPCOMPLICATIONVO> lstSETUPCOMPLICATIONVO = new BLSETUPCOMPLICATION(dbInfo).SearchByKey(SETUPCOMPLICATIONVO);
                ddlComplication.DataSource = lstSETUPCOMPLICATIONVO;
                ddlComplication.DataValueField = "ID";
                ddlComplication.DataTextField = "ComplicationHeader";
                ddlComplication.DataBind();
                ListItem litComplication = new ListItem();
                litComplication.Text = "None";
                litComplication.Value = "";
                ddlComplication.Items.Insert(0, litComplication);


                SETUPORGANMAINVO SETUPORGANMAINVO = new SETUPORGANMAINVO();
                List<SETUPORGANMAINVO> lstBLSETUPORGANMAINVO = new BLSETUPORGANMAIN(dbInfo).SearchByKey(SETUPORGANMAINVO);
                ddlOrgan.DataSource = lstBLSETUPORGANMAINVO;
                ddlOrgan.DataValueField = "MainCode";
                ddlOrgan.DataTextField = "Name";
                ddlOrgan.DataBind();
                ListItem litOrgan = new ListItem();
                litOrgan.Text = "None";
                litOrgan.Value = "";
                ddlOrgan.Items.Insert(0, litOrgan);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MapDDLTabICD()
        {
            MapDDLICD(new SETUPICDVO());
            MapDDLICDCM1(new SETUPICDCMVO());
            MapDDLICDCM2(new SETUPICDCMVO());
            MapDDLICDCM3(new SETUPICDCMVO());
            MapDDLICDCM4(new SETUPICDCMVO());
            MapDDLICDCM5(new SETUPICDCMVO());
        }

        private void MapDDLICD(SETUPICDVO SETUPICDVO)
        {
            try
            {
                List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchLikeByKey(SETUPICDVO);
                ddlICD.DataSource = lstSETUPICDVO;
                ddlICD.DataValueField = "Code";
                ddlICD.DataTextField = "CodeName";
                ddlICD.DataBind();
                ListItem litICD = new ListItem();
                litICD.Text = "";
                litICD.Value = "";
                ddlICD.Items.Insert(0, litICD);
                ddlICD.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM1(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM1.DataSource = lstSETUPICDCMVO;
                ddlICDCM1.DataValueField = "Code";
                ddlICDCM1.DataTextField = "CodeName";
                ddlICDCM1.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM1.Items.Insert(0, litICDCM);
                ddlICDCM1.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM2(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM2.DataSource = lstSETUPICDCMVO;
                ddlICDCM2.DataValueField = "Code";
                ddlICDCM2.DataTextField = "CodeName";
                ddlICDCM2.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM2.Items.Insert(0, litICDCM);
                ddlICDCM2.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM3(SETUPICDCMVO SETUPICDCMVO)
        {
                try
                {
                    List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                    ddlICDCM3.DataSource = lstSETUPICDCMVO;
                    ddlICDCM3.DataValueField = "Code";
                    ddlICDCM3.DataTextField = "CodeName";
                    ddlICDCM3.DataBind();
                    ListItem litICDCM = new ListItem();
                    litICDCM.Text = "";
                    litICDCM.Value = "";
                    ddlICDCM3.Items.Insert(0, litICDCM);
                    ddlICDCM3.SelectedIndex = 0;
                }
                catch (Exception ex)
                {
                    //throw ex;
                }
        }

        private void MapDDLICDCM4(SETUPICDCMVO SETUPICDCMVO)
        {
                    try
                    {
                        List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                        ddlICDCM4.DataSource = lstSETUPICDCMVO;
                        ddlICDCM4.DataValueField = "Code";
                        ddlICDCM4.DataTextField = "CodeName";
                        ddlICDCM4.DataBind();
                        ListItem litICDCM = new ListItem();
                        litICDCM.Text = "";
                        litICDCM.Value = "";
                        ddlICDCM4.Items.Insert(0, litICDCM);
                        ddlICDCM4.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
        }

        private void MapDDLICDCM5(SETUPICDCMVO SETUPICDCMVO)
        {
                        try
                        {
                            List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                            ddlICDCM5.DataSource = lstSETUPICDCMVO;
                            ddlICDCM5.DataValueField = "Code";
                            ddlICDCM5.DataTextField = "CodeName";
                            ddlICDCM5.DataBind();
                            ListItem litICDCM = new ListItem();
                            litICDCM.Text = "";
                            litICDCM.Value = "";
                            ddlICDCM5.Items.Insert(0, litICDCM);
                            ddlICDCM5.SelectedIndex = 0;
                        }
                        catch (Exception ex)
                        {
                            //throw ex;
                        }
        }

        //Search ICD / ICD CM

        private void MapDDLICD_Search(SETUPICDVO SETUPICDVO)
        {
            try
            {
                List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchLikeByKey(SETUPICDVO);
                ddlICD.DataSource = lstSETUPICDVO;
                ddlICD.DataValueField = "Code";
                ddlICD.DataTextField = "CodeName";
                ddlICD.DataBind();
                ListItem litICD = new ListItem();
                litICD.Text = "";
                litICD.Value = "";
                ddlICD.Items.Insert(0, litICD);
                ddlICD.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM1_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM1.DataSource = lstSETUPICDCMVO;
                ddlICDCM1.DataValueField = "Code";
                ddlICDCM1.DataTextField = "CodeName";
                ddlICDCM1.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM1.Items.Insert(0, litICDCM);
                ddlICDCM1.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM2_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM2.DataSource = lstSETUPICDCMVO;
                ddlICDCM2.DataValueField = "Code";
                ddlICDCM2.DataTextField = "CodeName";
                ddlICDCM2.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM2.Items.Insert(0, litICDCM);
                ddlICDCM2.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM3_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM3.DataSource = lstSETUPICDCMVO;
                ddlICDCM3.DataValueField = "Code";
                ddlICDCM3.DataTextField = "CodeName";
                ddlICDCM3.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM3.Items.Insert(0, litICDCM);
                ddlICDCM3.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM4_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM4.DataSource = lstSETUPICDCMVO;
                ddlICDCM4.DataValueField = "Code";
                ddlICDCM4.DataTextField = "CodeName";
                ddlICDCM4.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM4.Items.Insert(0, litICDCM);
                ddlICDCM4.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
            }
        }

        private void MapDDLICDCM5_Search(SETUPICDCMVO SETUPICDCMVO)
        {
            try
            {
                List<SETUPICDCMVO> lstSETUPICDCMVO = new BLSETUPICDCM(dbInfo).SearchLikeByKey(SETUPICDCMVO);
                ddlICDCM5.DataSource = lstSETUPICDCMVO;
                ddlICDCM5.DataValueField = "Code";
                ddlICDCM5.DataTextField = "CodeName";
                ddlICDCM5.DataBind();
                ListItem litICDCM = new ListItem();
                litICDCM.Text = "";
                litICDCM.Value = "";
                ddlICDCM5.Items.Insert(0, litICDCM);
                ddlICDCM5.SelectedIndex = 1;
            }
            catch (Exception ex)
            {
                //throw ex;
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
                POSTORIMPLANTVO.PostOperation_ID = hdPostOperation_ID.Value;
                POSTORIMPLANTVO.Seq = gvImplant.Rows.Count + 1;

                new BLPOSTORIMPLANT(dbInfo).Insert(POSTORIMPLANTVO);
                _lstor.Add(POSTORIMPLANTVO);
                gvImplant.DataSource = _lstor;
                gvImplant.DataBind();
                //SetButtonUpDown();

                //LoadlblProcedure();
            }
        }

        protected void gvImplant_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "image")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvImplant.Rows[rowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                hdPOSTORIMPLANT_ID.Value = ((HiddenField)row.FindControl("hdgvID")).Value;

                if (!string.IsNullOrEmpty(POSTORIMPLANTVO.ID))
                {
                    List<POSTORIMPLANTVO> lstPOSTORIMPLANTVO = new BLPOSTORIMPLANT(dbInfo).SearchByKey(POSTORIMPLANTVO);
                    if (lstPOSTORIMPLANTVO.Count > 0)
                    {
                        img1.ImageUrl = "";
                        img2.ImageUrl = "";
                        img3.ImageUrl = "";
                        img4.ImageUrl = "";
                        img5.ImageUrl = "";
                        if (lstPOSTORIMPLANTVO[0].Img1 != null && lstPOSTORIMPLANTVO[0].Img1.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img1);
                            img1.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img2 != null && lstPOSTORIMPLANTVO[0].Img2.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img2);
                            img2.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img3 != null && lstPOSTORIMPLANTVO[0].Img3.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img3);
                            img3.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img4 != null && lstPOSTORIMPLANTVO[0].Img4.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img4);
                            img4.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                        if (lstPOSTORIMPLANTVO[0].Img5 != null && lstPOSTORIMPLANTVO[0].Img5.Length > 100)
                        {
                            string base64String = Convert.ToBase64String(lstPOSTORIMPLANTVO[0].Img5);
                            img5.ImageUrl = "data:image/jpg;base64," + base64String;

                        }
                    }
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalImplantImage();", true);
            }
            else if (e.CommandName == "remark")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvImplant.Rows[rowIndex];
                POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                POSTORIMPLANTVO.Remark = ((TextBox)row.FindControl("txtRemark")).Text;
                ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).UpdateRemark(POSTORIMPLANTVO);
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

        protected void gvImplant_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                //GridViewRow row = gvImplant.Rows[e..RowIndex];
                //POSTORIMPLANTVO POSTORIMPLANTVO = new POSTORIMPLANTVO();
                //POSTORIMPLANTVO.ID = ((HiddenField)row.FindControl("hdgvID")).Value;
                //new BLPOSTORIMPLANT(dbInfo).Delete(POSTORIMPLANTVO.ID);

                //List<POSTORIMPLANTVO> _lstor = GetListValue_gvImplant();
                //_lstor.RemoveAt(e.RowIndex);
                //gvImplant.DataSource = _lstor;
                //gvImplant.DataBind();
                //SetButtonUpDown();
                //LoadlblProcedure();
            }
            catch (Exception ex)
            {
                throw ex;
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
                _or.PostOperation_ID = hdPostOperation_ID.Value;
                _or.Seq = i;
                _or.MainCode = (drow.FindControl("hdgvMainCode") as HiddenField).Value;
                _or.SubCode = (drow.FindControl("hdgvSubCode") as HiddenField).Value;
                _or.Name = (drow.FindControl("lblgvName") as Label).Text;
                _or.SubName = (drow.FindControl("lblgvSubName") as Label).Text;
                _or.Remark = (drow.FindControl("txtRemark") as TextBox).Text;
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

        protected void ddlOrgan_SelectedIndexChanged(object sender, EventArgs e)
        {
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = ddlOrgan.SelectedValue;
            MapDllORGANSUB(ddlOrgan.SelectedValue);
        }

        private void MapDllORGANSUB(string MainCode)
        {
            SETUPORGANSUBVO SETUPORGANSUBVO = new SETUPORGANSUBVO();
            SETUPORGANSUBVO.MainCode = MainCode;
            List<SETUPORGANSUBVO> lstSETUPORGANSUBVO = new BLSETUPORGANSUB(dbInfo).SearchByKey(SETUPORGANSUBVO);
            ddlOrganPosition.DataSource = lstSETUPORGANSUBVO;
            ddlOrganPosition.DataValueField = "SubCode";
            ddlOrganPosition.DataTextField = "SubName";
            ddlOrganPosition.DataBind();
            ListItem litOrgan = new ListItem();
            litOrgan.Text = "None";
            litOrgan.Value = "";
            ddlOrganPosition.Items.Insert(0, litOrgan);
        }

        private void loadORSummary()
        {
            lblProcedureSummary.Text = string.Empty;
            string str = string.Empty;
            foreach (GridViewRow drow in gvPostORICD.Rows)
            {
                if (!string.IsNullOrEmpty(str))
                {
                    str += "<br/>";
                }

                str += "<strong>" + (drow.FindControl("lblgvICD_Name") as Label).Text + "</strong>";
            }

            lblDiage.Text = str;

            lblORDate.Text = "<strong>" + hdSORDate.Value.Replace("/","-") + "</strong>";
            lblORTimeS.Text = "<strong>" + ddlSORTimeH.SelectedValue + ":" + ddlSORTimeM.SelectedValue + "</strong>";
            lblORTimeF.Text = "<strong>" + ddlFORTimeH.SelectedValue + ":" + ddlFORTimeM.SelectedValue + "</strong>";

            string allProcedure = string.Empty;

            foreach (GridViewRow drow in gvOROperation.Rows)
            {
                string Procedure = string.Empty;
                string docs = string.Empty;
                string doc1 = string.Empty;
                string doc2 = string.Empty;
                string doc3 = string.Empty;
                Procedure += (drow.FindControl("lblgvSide") as Label).Text + " : ";
                Procedure += (drow.FindControl("lblgvstrSubMark") as Label).Text;
                Procedure += (drow.FindControl("lblgvSubName") as Label).Text;
                bool xDoctorFromImplant = false;
                doc1 = (drow.FindControl("hdgvSurgeon1") as HiddenField).Value;
                if (doc1 != string.Empty)
                { xDoctorFromImplant = true; }
                if (!string.IsNullOrEmpty(doc1))
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = doc1;
                    List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                    if (lstDOCTORMASTERVO.Count > 0)
                    {
                        docs = lstDOCTORMASTERVO[0].DoctorName;
                    }
                }
                else if (ddlSurgeon1.SelectedValue != string.Empty)
                {
                    docs = ddlSurgeon1.SelectedItem.Text;
                }

                doc2 = (drow.FindControl("hdgvSurgeon2") as HiddenField).Value;
                if (!string.IsNullOrEmpty(doc2))
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = doc2;
                    List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                    if (lstDOCTORMASTERVO.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(docs))
                        { docs += ", "; }
                        docs += lstDOCTORMASTERVO[0].DoctorName;
                    }
                }
                else if (!xDoctorFromImplant && ddlSurgeon2.SelectedValue != string.Empty)
                {
                    if (!string.IsNullOrEmpty(docs))
                    { docs += ", "; }
                    docs += ddlSurgeon2.SelectedItem.Text;
                }

                doc3 = (drow.FindControl("hdgvSurgeon3") as HiddenField).Value;
                if (!string.IsNullOrEmpty(doc3))
                {
                    DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                    DOCTORMASTERVO.DOCTOR = doc3;
                    List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                    if (lstDOCTORMASTERVO.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(docs))
                        { docs += ", "; }
                        docs += lstDOCTORMASTERVO[0].DoctorName;
                    }
                }
                else if (!xDoctorFromImplant && ddlSurgeon3.SelectedValue != string.Empty)
                {
                    if (!string.IsNullOrEmpty(docs))
                    { docs += ", "; }
                    docs += ddlSurgeon3.SelectedItem.Text;
                }

                if (!string.IsNullOrEmpty(Procedure))
                {
                    if (!string.IsNullOrEmpty(allProcedure))
                    {
                        allProcedure += "<br/>";
                    }
                    if (string.IsNullOrEmpty(docs))
                    {
                        docs = "ไม่มี";
                    }
                    allProcedure += "<strong>" + Procedure + " </strong> <strong>" + docs + " </strong>";
                }

            }
            lblProcedureSummary.Text = allProcedure;
            string nScrub = string.Empty;
            string nCircalate = string.Empty;
            string nAnes = string.Empty;
            lblScrub.Text = string.Empty;
            lblCircalate.Text = string.Empty;
            foreach (GridViewRow drow in gvPostORNurse.Rows)
            {
                int xType = int.Parse((drow.FindControl("hdgvNurseType") as HiddenField).Value);
                string xNurse = (drow.FindControl("lblgvNurse") as Label).Text;
                string xNurseRemark = (drow.FindControl("lblgvRemark") as Label).Text;

                if (xType == 1)
                {
                    if (!string.IsNullOrEmpty(nScrub))
                    {
                        nScrub += ", ";
                    }
                    if (xNurse != string.Empty)
                        nScrub += "<strong>" + xNurse + "</strong>";
                    else if (xNurseRemark != string.Empty)
                        nScrub += "<strong>" + xNurseRemark + "</strong>";
                }
                if (xType == 2)
                {
                    if (!string.IsNullOrEmpty(nCircalate))
                    {
                        nCircalate += ", ";
                    }
                    if (xNurse != string.Empty)
                        nCircalate += "<strong>" + xNurse + "</strong>";
                    else if (xNurseRemark != string.Empty)
                        nCircalate += "<strong>" + xNurseRemark + "</strong>";
                }
                if (xType == 3)
                {
                    if (!string.IsNullOrEmpty(nAnes))
                    {
                        nAnes += ", ";
                    }
                    if (xNurse != string.Empty)
                        nAnes += "<strong>" + xNurse + "</strong>";
                    else if (xNurseRemark != string.Empty)
                        nAnes += "<strong>" + xNurseRemark + "</strong>";
                }
            }
            lblScrub.Text = nScrub;
            lblCircalate.Text = nCircalate;
            lblAnesthesiaNurse.Text = nAnes;
            if (!string.IsNullOrEmpty(hdSAnesDate.Value))
            {
                lblAnesDate.Text = hdFAnesDate.Value.Replace("/","-");
            }
            if (!string.IsNullOrEmpty(hdSAnesDate.Value))
            {
                lblStartAnesTime.Text = ddlSAnesTimeH.Text + ":" + ddlSAnesTimeM.Text;
                lblFinishAnesTime.Text = ddlFAnesTimeH.Text + ":" + ddlFAnesTimeM.Text;
            }

            //lblAnesthesiaType.Text = ddlAnesthesiaType1.SelectedItem.Text + ddlAnesthesiaSign.SelectedItem.Text + ddlAnesthesiaType2.SelectedItem.Text;

            if (ddlAnesthesiaType1.SelectedValue != "" && ddlAnesthesiaType1.SelectedValue != "0")
            {
                lblAnesthesiaType.Text += ddlAnesthesiaType1.SelectedItem.Text;
            }
            if (ddlAnesthesiaSign.SelectedValue != "" && ddlAnesthesiaSign.SelectedValue != "0")
            {
                lblAnesthesiaType.Text += ddlAnesthesiaSign.SelectedItem.Text;
            }
            if (ddlAnesthesiaType2.SelectedValue != "" && ddlAnesthesiaType2.SelectedValue != "0")
            {
                lblAnesthesiaType.Text += ddlAnesthesiaType2.SelectedItem.Text;
            }
            if (ddlAnesthesiaDoctor1.SelectedValue != string.Empty)
            {
                lblAnesthesiaDoctor.Text = ddlAnesthesiaDoctor1.SelectedItem.Text;
            }
            if (ddlAnesthesiaDoctor2.SelectedValue != string.Empty)
            {
                if (lblAnesthesiaDoctor.Text != string.Empty)
                {
                    lblAnesthesiaDoctor.Text += ",";
                }
                lblAnesthesiaDoctor.Text += ddlAnesthesiaDoctor2.SelectedItem.Text;
            }
            if (ddlAnesthesiaDoctor3.SelectedValue != string.Empty)
            {
                if (lblAnesthesiaDoctor.Text != string.Empty)
                {
                    lblAnesthesiaDoctor.Text += ",";
                }
                lblAnesthesiaDoctor.Text += ddlAnesthesiaDoctor3.SelectedItem.Text;
            }
            //load ICDCM
            str = string.Empty;
            foreach (GridViewRow drow in gvPostORICD.Rows)
            {
                string stricdcm = string.Empty;
                if (!string.IsNullOrEmpty(str))
                {
                    str += "<br/>";
                }
                if ((drow.FindControl("lblgvICDCM1") as Label).Text != string.Empty)
                {
                    if (stricdcm != string.Empty)
                        stricdcm += ", ";
                    stricdcm += "<strong>" + (drow.FindControl("lblgvICDCM1") as Label).Text + "</strong>";
                }
                if ((drow.FindControl("lblgvICDCM2") as Label).Text != string.Empty)
                {
                    if (stricdcm != string.Empty)
                        stricdcm += ", ";
                    stricdcm += "<strong>" + (drow.FindControl("lblgvICDCM2") as Label).Text + "</strong>";
                }
                if ((drow.FindControl("lblgvICDCM3") as Label).Text != string.Empty)
                {
                    if (stricdcm != string.Empty)
                        stricdcm += ", ";
                    stricdcm += "<strong>" + (drow.FindControl("lblgvICDCM3") as Label).Text + "</strong>";
                }
                if ((drow.FindControl("lblgvICDCM4") as Label).Text != string.Empty)
                {
                    if (stricdcm != string.Empty)
                        stricdcm += ", ";
                    stricdcm += "<strong>" + (drow.FindControl("lblgvICDCM4") as Label).Text + "</strong>";
                }
                if ((drow.FindControl("lblgvICDCM5") as Label).Text != string.Empty)
                {
                    if (stricdcm != string.Empty)
                        stricdcm += ", ";
                    stricdcm += "<strong>" + (drow.FindControl("lblgvICDCM5") as Label).Text + "</strong>";
                }
                str += stricdcm;
            }

            lblICDCM.Text = str;
        }

        protected void btnSaveImageImplant_Click(object sender, EventArgs e)
        {
            POSTORIMPLANTVO por = new POSTORIMPLANTVO();
            por.ID = hdPOSTORIMPLANT_ID.Value;
            if (!string.IsNullOrEmpty(FileUpload1.PostedFile.FileName))
            {
                System.Drawing.Image imag1 = System.Drawing.Image.FromStream(FileUpload1.PostedFile.InputStream);
                por.Img1 = ConvertImageToByteArray(imag1, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload2.PostedFile.FileName))
            {
                System.Drawing.Image imag2 = System.Drawing.Image.FromStream(FileUpload2.PostedFile.InputStream);
                por.Img2 = ConvertImageToByteArray(imag2, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload3.PostedFile.FileName))
            {
                System.Drawing.Image imag3 = System.Drawing.Image.FromStream(FileUpload3.PostedFile.InputStream);
                por.Img3 = ConvertImageToByteArray(imag3, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload4.PostedFile.FileName))
            {
                System.Drawing.Image imag4 = System.Drawing.Image.FromStream(FileUpload4.PostedFile.InputStream);
                por.Img4 = ConvertImageToByteArray(imag4, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            if (!string.IsNullOrEmpty(FileUpload5.PostedFile.FileName))
            {
                System.Drawing.Image imag5 = System.Drawing.Image.FromStream(FileUpload5.PostedFile.InputStream);
                por.Img5 = ConvertImageToByteArray(imag5, System.Drawing.Imaging.ImageFormat.Jpeg);
            }

            por.delimg1 = chimg1.Checked;
            por.delimg2 = chimg2.Checked;
            por.delimg3 = chimg3.Checked;
            por.delimg4 = chimg4.Checked;
            por.delimg5 = chimg5.Checked;

            ReturnValue rv = new BLPOSTORIMPLANT(dbInfo).UpdateImage(por);
            if (rv.Value)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "closeModalImplantImage();", true);

                chimg1.Checked = false;
                chimg2.Checked = false;
                chimg3.Checked = false;
                chimg4.Checked = false;
                chimg5.Checked = false;
            }
        }

        private byte[] ConvertImageToByteArray(System.Drawing.Image imageToConvert, System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] Ret;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    Ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return Ret;
        }

        protected void chOnmed_CheckedChanged(object sender, EventArgs e)
        {
            txtOnmed.Visible = chOnmed.Checked;
        }

        protected void txtSORTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSORTime.Text, txtFORTime.Text, "OR Time"))
            {
                txtSORTime.Text = string.Empty;
            }
        }

        protected void txtFORTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSORTime.Text, txtFORTime.Text, "OR Time"))
            {
                txtFORTime.Text = string.Empty;
            }
        }

        protected void txtSAnesTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSAnesTime.Text, txtFAnesTime.Text, "Anes Time"))
            {
                txtSAnesTime.Text = string.Empty;
            }
        }

        protected void txtFAnesTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSAnesTime.Text, txtFAnesTime.Text, "Anes Time"))
            {
                txtFAnesTime.Text = string.Empty;
            }
        }

        protected void txtSBlockTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSBlockTime.Text, txtFBlockTime.Text, "Block Time"))
            {
                txtSBlockTime.Text = string.Empty;
            }
        }

        protected void txtFBlockTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSBlockTime.Text, txtFBlockTime.Text, "Block Time"))
            {
                txtFBlockTime.Text = string.Empty;
            }
        }

        protected void txtSRecoveryTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSRecoveryTime.Text, txtFRecoveryTime.Text, "Block Time"))
            {
                txtSRecoveryTime.Text = string.Empty;
            }
        }

        protected void txtFRecoveryTime_TextChanged(object sender, EventArgs e)
        {
            if (checkTime(txtSRecoveryTime.Text, txtFRecoveryTime.Text, "Block Time"))
            {
                txtFRecoveryTime.Text = string.Empty;
            }
        }

        private bool checkTime(string s, string f, string sTime)
        {
            bool result = false;
            try
            {
                if (s == string.Empty)
                    return result;
                else if (f == string.Empty)
                    return result;

                int _SORTime = 0;
                int _FORTime = 0;
                _SORTime = int.Parse(s.Replace(":", "").PadRight(4, '0'));
                _FORTime = int.Parse(f.Replace(":", "").PadRight(4, '0'));
                if (_SORTime > _FORTime)
                {
                    result = true;
                    txtStrCheckTime.Text = "กรอกข้อมูล " + sTime + " ไม่ถูกต้อง";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalCheckTime();", true);
                }
            }
            catch
            {
                result = true;
                txtStrCheckTime.Text = "กรอกข้อมูล " + sTime + " ไม่ถูกต้อง";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "showModalCheckTime();", true);
            }
            return result;
        }

        protected void ddlComplicationID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                List<SETUPCOMPLICATIONVO> lstSETUPCOMPLICATIONVO = new BLSETUPCOMPLICATION(dbInfo).SearchByPrimary(ddlComplicationID.SelectedValue);
                if (lstSETUPCOMPLICATIONVO.Count > 0)
                    txtComplicationDetail.Text = lstSETUPCOMPLICATIONVO[0].ComplicationDetail;
            }
            catch { }
        }

        protected void ddlREASON_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlREASON.SelectedValue == "99")
            {
                txtReason.Visible = true;
            }
            else
            {
                txtReason.Visible = false;
            }
            //
        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            loadUnderPatent(lblHN.Text, DateTime.Parse(DateFormat.dmy2ymd(hdORDate.Value)));
            //Response.Redirect("/PostOR/PrintORSumary/?d=" + hdORID.Value, false);
            string imgpath = "../../Images/logo.png";
            byte[] imgdata = System.IO.File.ReadAllBytes(Server.MapPath(imgpath));


            var base64 = Convert.ToBase64String(imgdata);
            var imgSrc = String.Format("data:image/jpg;base64,{0}", base64);

            string HN = "HN : " + lblHN.Text + "&nbsp;&nbsp;&nbsp;" + lblPatientName.Text;
            string xAge = string.Empty;
            try
            {
                DateTime dtb = DateTime.Parse(lblBirthDateTime.Text);
                double b = double.Parse(ORUtils.getAge(dtb));
                if (b >= 1)
                {
                    xAge = b.ToString() + " ปี" + " (" + lblBirthDateTime.Text + ")";
                }
                else if (b < 1)
                {
                    xAge = (b * 10).ToString("#") + " เดือน" + " (" + lblBirthDateTime.Text + ")";
                }
            }
            catch { }

            List<ORHEADERVO> lsthTemp = new List<ORHEADERVO>();
            foreach (GridViewRow r in gvUnderPatient.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    ORHEADERVO v = new ORHEADERVO();
                    v.ORDate = DateTime.Parse((r.FindControl("hdORDate") as HiddenField).Value);
                    v.strSide = (r.FindControl("hdstrSide") as HiddenField).Value;
                    v.Procedure = (r.FindControl("lblgvOperation") as Label).Text;
                    v.Remark = (r.FindControl("hdNote") as HiddenField).Value;
                    v.SurgeonName = (r.FindControl("hdSurgeonName") as HiddenField).Value; 
                    lsthTemp.Add(v);
                }
            }

            lsthTemp.Sort((x, y) => DateTime.Compare(x.ORDate.Value, y.ORDate.Value));

            string tr = string.Empty;
            foreach (ORHEADERVO r in lsthTemp)
            {
                string ORDate = r.ORDate.Value.ToString("yyyy-MM-dd");
                string Side = r.strSide;
                string ProcedureMemo = r.Procedure;
                string Note = r.Remark;
                string SurgeonName = r.SurgeonName;
                tr +=
                "<div> " + ORDate + " " + Side + " : " + ProcedureMemo + " " + SurgeonName + "</div>" +
                "<div style=\"padding-bottom:5px;\"> " + Note + " </div>";

            }

            //foreach (GridViewRow r in g.Rows)
            //{
            //    if (r.RowType == DataControlRowType.DataRow)
            //    {
            //        string ORDate = DateTime.Parse((r.FindControl("hdORDate") as HiddenField).Value).ToString("yyyy/MM/dd");
            //        string Side = (r.FindControl("hdstrSide") as HiddenField).Value;
            //        string ProcedureMemo = (r.FindControl("lblgvOperation") as Label).Text;
            //        string Note = (r.FindControl("hdNote") as HiddenField).Value;
            //        tr +=
            //        "<div> " + ORDate + " " + Side + ": " + ProcedureMemo + "</div>" +
            //        "<div style=\"padding-bottom:5px;\"> " + Note + " </div>";
            //    }
            //}
            string arOrDate = hdSORDate.Value.Split('/')[2] + "/" + hdSORDate.Value.Split('/')[1] + "/" + hdSORDate.Value.Split('/')[0];
            tr += "<div> <strong>" + arOrDate.Replace('/','-') + "</strong> " + lblProcedureSummary.Text + "</div>";
            string path = Server.MapPath("/table.html");
            string html = File.ReadAllText(path);
            string htmlString = string.Format(html, imgSrc, HN, xAge, tr);
            string baseUrl = string.Empty;
            string pdf_page_size = "A4";
            PdfPageSize pageSize = (PdfPageSize)System.Enum.Parse(typeof(PdfPageSize),
                pdf_page_size, true);

            string pdf_orientation = "Portrait";
            PdfPageOrientation pdfOrientation =
                (PdfPageOrientation)System.Enum.Parse(typeof(PdfPageOrientation),
                pdf_orientation, true);

            int webPageWidth = 1024;

            int webPageHeight = 0;

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();

            // set converter options
            converter.Options.PdfPageSize = pageSize;
            converter.Options.PdfPageOrientation = pdfOrientation;
            converter.Options.WebPageWidth = webPageWidth;
            converter.Options.WebPageHeight = webPageHeight;

            // create a new pdf document converting an url
            PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

            // save pdf document
            doc.Save(Response, false, "ORSumary_" + lblHN.Text + ".pdf");

            // close pdf document
            doc.Close();
        }

        protected void ddlComplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlComplication.Text == "")
            {
                divComplicationDoctor.Visible = false;
                txtComplication.Visible = false;
                //txtComplication.Text = string.Empty;
            }
            else
            {
                divComplicationDoctor.Visible = true;
                txtComplication.Visible = true;
            }
        }
        protected void txtSearchSurgeon1_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchSurgeon1.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchSurgeon1.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlSurgeon1.DataSource = lstDOCTORMASTERVO;
            ddlSurgeon1.DataTextField = "DoctorName";
            ddlSurgeon1.DataValueField = "DOCTOR";
            ddlSurgeon1.DataBind();
        }
        protected void txtSearchSurgeon2_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchSurgeon2.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchSurgeon2.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlSurgeon2.DataSource = lstDOCTORMASTERVO;
            ddlSurgeon2.DataTextField = "DoctorName";
            ddlSurgeon2.DataValueField = "DOCTOR";
            ddlSurgeon2.DataBind();
        }
        protected void txtSearchSurgeon3_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchSurgeon3.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchSurgeon3.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlSurgeon3.DataSource = lstDOCTORMASTERVO;
            ddlSurgeon3.DataTextField = "DoctorName";
            ddlSurgeon3.DataValueField = "DOCTOR";
            ddlSurgeon3.DataBind();
        }
        protected void txtSearchAnesDoctor1_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchAnesDoctor1.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchAnesDoctor1.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlAnesthesiaDoctor1.DataSource = lstDOCTORMASTERVO;
            ddlAnesthesiaDoctor1.DataTextField = "DoctorName";
            ddlAnesthesiaDoctor1.DataValueField = "DOCTOR";
            ddlAnesthesiaDoctor1.DataBind();
        }
        protected void txtSearchAnesDoctor2_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchAnesDoctor2.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchAnesDoctor2.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlAnesthesiaDoctor2.DataSource = lstDOCTORMASTERVO;
            ddlAnesthesiaDoctor2.DataTextField = "DoctorName";
            ddlAnesthesiaDoctor2.DataValueField = "DOCTOR";
            ddlAnesthesiaDoctor2.DataBind();
        }
        protected void txtSearchAnesDoctor3_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchAnesDoctor3.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchAnesDoctor3.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlAnesthesiaDoctor3.DataSource = lstDOCTORMASTERVO;
            ddlAnesthesiaDoctor3.DataTextField = "DoctorName";
            ddlAnesthesiaDoctor3.DataValueField = "DOCTOR";
            ddlAnesthesiaDoctor3.DataBind();
        }
        protected void txtSearchProSurgeon1_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchProSurgeon1.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchProSurgeon1.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlProSurgeon1.DataSource = lstDOCTORMASTERVO;
            ddlProSurgeon1.DataTextField = "DoctorName";
            ddlProSurgeon1.DataValueField = "DOCTOR";
            ddlProSurgeon1.DataBind();
        }
        protected void txtSearchProSurgeon2_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchProSurgeon2.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchProSurgeon2.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlProSurgeon2.DataSource = lstDOCTORMASTERVO;
            ddlProSurgeon2.DataTextField = "DoctorName";
            ddlProSurgeon2.DataValueField = "DOCTOR";
            ddlProSurgeon2.DataBind();
        }
        protected void txtSearchProSurgeon3_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtSearchProSurgeon3.Text;
            _DOCTORMASTERVO.DoctorName = txtSearchProSurgeon3.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlProSurgeon3.DataSource = lstDOCTORMASTERVO;
            ddlProSurgeon3.DataTextField = "DoctorName";
            ddlProSurgeon3.DataValueField = "DOCTOR";
            ddlProSurgeon3.DataBind();
        }
        protected void txtORMigrationSurgeonSearch_TextChanged(object sender, EventArgs e)
        {
            DOCTORMASTERVO _DOCTORMASTERVO = new DOCTORMASTERVO();
            _DOCTORMASTERVO.DOCTOR = txtORMigrationSurgeonSearch.Text;
            _DOCTORMASTERVO.DoctorName = txtORMigrationSurgeonSearch.Text;
            _DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchDDL(_DOCTORMASTERVO);
            ddlORMigrationSurgeon.DataSource = lstDOCTORMASTERVO;
            ddlORMigrationSurgeon.DataTextField = "DoctorName";
            ddlORMigrationSurgeon.DataValueField = "DOCTOR";
            ddlORMigrationSurgeon.DataBind();
        }

        protected void txtICDSearch_TextChanged(object sender, EventArgs e)
        {
            SETUPICDVO SETUPICDVO = new SETUPICDVO();
            SETUPICDVO.Code = txtICDSearch.Text;
            SETUPICDVO.Name = txtICDSearch.Text;
            MapDDLICD_Search(SETUPICDVO);
            //List<SETUPICDVO> lstSETUPICDVO = new BLSETUPICD(dbInfo).SearchLikeByKey(SETUPICDVO);
            //ddlICD.DataSource = lstSETUPICDVO;
            //ddlICD.DataValueField = "Code";
            //ddlICD.DataTextField = "CodeName";
            //ddlICD.DataBind();
            //ddlICD.SelectedValue = lstSETUPICDVO[0].Code;
            //ListItem litICD = new ListItem();
            //litICD.Text = "";
            //litICD.Value = "";
            //ddlICD.Items.Insert(0, litICD); ddlICD.Items.Insert(0, litICD);
        }

        protected void txtICDCM1Search_TextChanged(object sender, EventArgs e)
        {
            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM1Search.Text;
            SETUPICDCMVO.Name = txtICDCM1Search.Text;
            MapDDLICDCM1_Search(SETUPICDCMVO);
            ddlcm1doc1.SelectedValue = ddlSurgeon1.Text;
            ddlcm1doc2.SelectedValue = ddlSurgeon2.Text;
            ddlcm1doc3.SelectedValue = ddlSurgeon3.Text;
        }

        protected void txtICDCM2Search_TextChanged(object sender, EventArgs e)
        {

            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM2Search.Text;
            SETUPICDCMVO.Name = txtICDCM2Search.Text;
            MapDDLICDCM2_Search(SETUPICDCMVO);
            ddlcm2doc1.SelectedValue = ddlSurgeon1.Text;
            ddlcm2doc2.SelectedValue = ddlSurgeon2.Text;
            ddlcm2doc3.SelectedValue = ddlSurgeon3.Text;
        }

        protected void txtICDCM3Search_TextChanged(object sender, EventArgs e)
        {

            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM3Search.Text;
            SETUPICDCMVO.Name = txtICDCM3Search.Text;
            MapDDLICDCM3_Search(SETUPICDCMVO);
            ddlcm3doc1.SelectedValue = ddlSurgeon1.Text;
            ddlcm3doc2.SelectedValue = ddlSurgeon2.Text;
            ddlcm3doc3.SelectedValue = ddlSurgeon3.Text;
        }

        protected void txtICDCM4Search_TextChanged(object sender, EventArgs e)
        {
            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM4Search.Text;
            SETUPICDCMVO.Name = txtICDCM4Search.Text;
            MapDDLICDCM4_Search(SETUPICDCMVO);
            ddlcm4doc1.SelectedValue = ddlSurgeon1.Text;
            ddlcm4doc2.SelectedValue = ddlSurgeon2.Text;
            ddlcm4doc3.SelectedValue = ddlSurgeon3.Text;
        }

        protected void txtICDCM5Search_TextChanged(object sender, EventArgs e)
        {

            SETUPICDCMVO SETUPICDCMVO = new SETUPICDCMVO();
            SETUPICDCMVO.Code = txtICDCM5Search.Text;
            SETUPICDCMVO.Name = txtICDCM5Search.Text;
            MapDDLICDCM5_Search(SETUPICDCMVO);
            ddlcm5doc1.SelectedValue = ddlSurgeon1.Text;
            ddlcm5doc2.SelectedValue = ddlSurgeon2.Text;
            ddlcm5doc3.SelectedValue = ddlSurgeon3.Text;
        }

    }
}