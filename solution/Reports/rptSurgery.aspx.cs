using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace solution.Reports
{
    public partial class rptSurgery : System.Web.UI.Page
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
            else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.ReadOnly).ToString())
            {
                Response.Redirect("/Reserve/Views");

                Response.End();
            }
            if (!IsPostBack)
            {
                if (Session["ORDate"] != null)
                {
                    hdDateFrom.Value = Session["ORDate"].ToString();
                    hdDateTo.Value = Session["ORDate"].ToString();
                }
                else
                {
                    hdDateFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }

                // Cxl Check Flag Status
                ddlCxlCheck.DataSource = EnumOR.GetAllName<EnumOR.CxlCheckFlag>();
                ddlCxlCheck.DataTextField = "Value";
                ddlCxlCheck.DataValueField = "Key";
                ddlCxlCheck.DataBind();
                ddlCxlCheck.SelectedIndex = (int)EnumOR.CxlCheckFlag.Without;

                // Cxl Check Flag Status
                ddlCxlConfirm.DataSource = EnumOR.GetAllName<EnumOR.CxlCheckFlag>();
                ddlCxlConfirm.DataTextField = "Value";
                ddlCxlConfirm.DataValueField = "Key";
                ddlCxlConfirm.DataBind();
                ddlCxlConfirm.SelectedIndex = (int)EnumOR.CxlCheckFlag.Without;

                loadvalue();
            }
        }

        private void loadvalue()
        {
            try
            {
                List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
                int iSeq = 0;
                DateTime DateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                DateTime DateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchrptSurgery(DateFrom, DateTo, ddlCxlCheck.SelectedValue, ddlCxlConfirm.SelectedValue,txtHN.Text);
                foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
                {
                    APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                    APPOINTMENTVO.AppointmentNo = ORHEADERVO.AppointmentNo;
                    List<APPOINTMENTVO> _lstAPPOINTMENTVO = new List<APPOINTMENTVO>();

                    //Original
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

                    //New
                    try
                    {
                        if (!string.IsNullOrEmpty(APPOINTMENTVO.AppointmentNo))
                        {
                            _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                            if (_lstAPPOINTMENTVO.Count > 0)
                            {
                                if (_lstAPPOINTMENTVO[0].ConfirmStatusType == 6)
                                {
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
                                }
                                else
                                {
                                    APPOINTMENTVO.ConfirmStatusType = 0;
                                    _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                                }
                            }
                            else
                            {
                                APPOINTMENTVO.ConfirmStatusType = 0;
                                _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                            }
                        }
                        else
                        {
                            APPOINTMENTVO.ConfirmStatusType = 0;
                            _lstAPPOINTMENTVO.Add(APPOINTMENTVO);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    
                    //if (_lstAPPOINTMENTVO[0].ConfirmStatusType != 6)
                       if (APPOINTMENTVO.ConfirmStatusType != 6)
                        {
                        ORHEADERVO.iSeq = iSeq + 1;
                        iSeq = iSeq + 1;
                        ORHEADERVO.SurgeonName = ORHEADERVO.SurgeonName;
                        ORHEADERVO.AnesDoctorName = ORHEADERVO.AnesDoctorName;
                        ORHEADERVO.ElectiveCase = ORHEADERVO.ElectiveCase;
                        ORHEADERVO.UrgencyCase = ORHEADERVO.UrgencyCase;
                        ORHEADERVO.CxlReason = ORHEADERVO.CxlReason;
                        //OR Time
                        try
                        {
                            if (ORHEADERVO.StartORDateTime != null)
                            {
                                ORHEADERVO.StartTime = ORHEADERVO.StartORDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.StartORDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try
                        {
                            if (ORHEADERVO.FinishORDateTime != null)
                            {
                                ORHEADERVO.FinishTime = ORHEADERVO.FinishORDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.FinishORDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        //Anes Time
                        try
                        {
                            if (ORHEADERVO.StartAnesDateTime != null)
                            {
                                ORHEADERVO.StartAnesTime = ORHEADERVO.StartAnesDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.StartAnesDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try
                        {
                            if (ORHEADERVO.FinishAnesDateTime != null)
                            {
                                ORHEADERVO.FinishAnesTime = ORHEADERVO.FinishAnesDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.FinishAnesDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        //Block Time
                        try
                        {
                            if (ORHEADERVO.StartBlockDateTime != null)
                            {
                                ORHEADERVO.StartBlockTime = ORHEADERVO.StartBlockDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.StartBlockDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try
                        {
                            if (ORHEADERVO.FinishBlockDateTime != null)
                            {
                                ORHEADERVO.FinishBlockTime = ORHEADERVO.FinishBlockDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.FinishBlockDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        //Recovery
                        try
                        {
                            if (ORHEADERVO.StartRecoveryDateTime != null)
                            {
                                ORHEADERVO.StartRecoveryTime = ORHEADERVO.StartRecoveryDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.StartRecoveryDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        try
                        {
                            if (ORHEADERVO.FinishRecoveryDateTime != null)
                            {
                                ORHEADERVO.FinishRecoveryTime = ORHEADERVO.FinishRecoveryDateTime.Value.Hour.ToString() + ":" + ORHEADERVO.FinishRecoveryDateTime.Value.Minute.ToString("0#");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        ORHEADERVO.HN = ORHEADERVO.HN;
                        try
                        {
                            if (ORHEADERVO.OperationTime != null && ORHEADERVO.OperationTime != "")
                            {

                                TimeSpan span = TimeSpan.FromMinutes(double.Parse(ORHEADERVO.OperationTime));
                                //string label = span.ToString(@"hh\:mm\:ss");
                                ORHEADERVO.OperationTime = span.ToString(@"h\:mm");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        ORHEADERVO.Remark = ORHEADERVO.Remark;

                        //Nurse

                        string strAnesNurse = string.Empty;
                        string strScrubNurse = string.Empty;
                        string strCriNurse = string.Empty;

                        try
                        {
                            POSTORNURSEVO POSTORNURSEVO = new POSTORNURSEVO();
                            POSTORNURSEVO.ORID = ORHEADERVO.ORID;
                            //POSTORNURSEVO.NurseType = int.Parse(hdSuffixNurse.Value);
                            List<POSTORNURSEVO> lstPOSTORNURSEVO = new BLPOSTORNURSE(dbInfo).SearchByKey(POSTORNURSEVO);
                            foreach (POSTORNURSEVO xx in lstPOSTORNURSEVO)
                            {
                                if (xx.NurseType == 1)
                                {
                                    strScrubNurse += xx.Nurse.ToString() + "<Br>";
                                }
                                if (xx.NurseType == 2)
                                {
                                    strCriNurse += xx.Nurse.ToString() + "<Br>";
                                }
                                if (xx.NurseType == 3)
                                {
                                    strAnesNurse += xx.Nurse.ToString() + "<Br>";
                                }
                                //ddlNurseType.SelectedValue = xx.NurseType.ToString();
                                //ddlNurseCode.SelectedValue = xx.NurseCode;
                                //txtNurseRemark.Text = xx.Remark;
                            }
                        }
                        catch { }

                        ORHEADERVO.strAnesNurse = strAnesNurse;
                        ORHEADERVO.strCriNurse = strCriNurse;
                        ORHEADERVO.strScrubNurse = strScrubNurse;


                        string r = string.Empty;
                        string l = string.Empty;
                        string b = string.Empty;
                        string None = string.Empty;
                        string NA = string.Empty;
                        List<POSTOROPERATIONVO> lstOROPERATIONVO = new BLPOSTOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                        foreach (POSTOROPERATIONVO op1 in lstOROPERATIONVO)
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
                        //string Surgeon1 = string.Empty;
                        //string Surgeon2 = string.Empty;
                        //string Surgeon3 = string.Empty;

                        //ORHEADERVO.Surgeon = Surgeon1;
                        //if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon2))
                        //{
                        //    ORHEADERVO.Surgeon += "<br/>" + Surgeon2;
                        //}
                        //if (!string.IsNullOrEmpty(ORHEADERVO.Surgeon3))
                        //{
                        //    ORHEADERVO.Surgeon += "<br/>" + Surgeon3;
                        //}

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

                        Boolean vbl = false;

                        VT_PATIENT_ANVO vl = new VT_PATIENT_ANVO();
                        vl.HN = ORHEADERVO.HN;
                        try
                        {
                            vl.ORDateTime = DateTime.Parse(ORHEADERVO.ORDate.Value.ToString("yyyy/MM/dd") + " " + ORHEADERVO.ORDate);
                        }
                        catch
                        {
                            vl.ORDateTime = DateTime.Parse(ORHEADERVO.ORDate.Value.ToString("yyyy/MM/dd") + " 23:59:59");
                        }

                        List<VT_PATIENT_ANVO> lstVT_PATIENT_ANVO = new BLVT_PATIENT_AN(dbInfo).SearchAN(vl);
                        if (lstVT_PATIENT_ANVO.Count > 0)
                        {
                            foreach (VT_PATIENT_ANVO xx in lstVT_PATIENT_ANVO)
                            {
                                ORHEADERVO.VisitID = xx.AN.ToString();
                            }
                            vbl = true;
                        }

                        if (vbl == false)
                        {

                            VT_PATIENT_VNVO vlVN = new VT_PATIENT_VNVO();
                            vlVN.HN = ORHEADERVO.HN;
                            vlVN.ORDateTime = ORHEADERVO.ORDate;
                            List<VT_PATIENT_VNVO> lstVT_PATIENT_VNVO = new BLVT_PATIENT_VN(dbInfo).SearchVN(vlVN);
                            if (lstVT_PATIENT_VNVO.Count > 0)
                            {
                                foreach (VT_PATIENT_VNVO xx in lstVT_PATIENT_VNVO)
                                {
                                    ORHEADERVO.VisitID = xx.VN.ToString();
                                }
                                vbl = true;
                            }

                        }
                        vbl = false;

                        lstORHEADERVO.Add(ORHEADERVO);

                    }                    
                }
                gvData.DataSource = lstORHEADERVO;
                gvData.DataBind();
                Session["gvrptSurgery"] = lstORHEADERVO;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reports/pSurgery/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=รายงานทะเบียนการผ่าตัด.xls");
            Response.ContentType = "application/ms-excel";
            Response.ContentEncoding = System.Text.Encoding.Unicode;
            Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                gvData.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in gvData.HeaderRow.Cells)
                {
                    cell.BackColor = gvData.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in gvData.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = gvData.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = gvData.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                gvData.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
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
                    vl.ORDateTime = DateTime.Parse(hd.ORDate.Value.ToString("yyyy/MM/dd") + " " + hd.ORDate);
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