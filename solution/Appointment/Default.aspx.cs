using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Appointment
{
    public partial class Default : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        private int RowIndex = 0;
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
                //Response.Redirect("/Reserve/Views");
                //Response.End();
                btnConfrim.Visible = false;
                gvAppointment.Columns[gvAppointment.Columns.Count-1].Visible = false;
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
                MapDDL();
            }
            AlertMessage(false, true, string.Empty);
        }

        private void loadvalue()
        {
            try
            {
                if (string.IsNullOrEmpty(hdDate.Value))
                {
                    DateTime dd = DateTime.Now;
                    hdDate.Value = dd.ToString("dd/MM/yyyy");
                    hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }
                else
                {
                    DateTime dd = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                    hdDate.Value = dd.ToString("dd/MM/yyyy");
                    hdDateEn.Value = dd.ToString("dd/MM/yyyy");
                }
                Session["ORDate"] = hdDate.Value;

                string iUserID = Session["USERID"].ToString();

                List<SETUPORROOMTYPEVO> lstSETUPORROOMTYPEVO = new BLSETUPORROOMTYPE(dbInfo).SearchByUser(iUserID);

                string arProce = string.Empty;
                foreach (SETUPORROOMTYPEVO vl1 in lstSETUPORROOMTYPEVO)
                {
                    ROOMTYPEPROCEVO ROOMTYPEPROCEVO = new ROOMTYPEPROCEVO();
                    ROOMTYPEPROCEVO.RoomType = vl1.ID;
                    List<ROOMTYPEPROCEVO> lstROOMTYPEPROCEVO = new BLROOMTYPEPROCE(dbInfo).SearchByKey(ROOMTYPEPROCEVO);
                    foreach (ROOMTYPEPROCEVO vl2 in lstROOMTYPEPROCEVO)
                    {
                        if (arProce != string.Empty)
                            arProce += ",";
                        arProce += "'" + vl2.ProcedureCode + "'";
                    }
                }

                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentDateTime = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                APPOINTMENTVO.ConfirmStatusType = 6;
                if(Session["USERNANME"].ToString() != "ADMIN")
                    APPOINTMENTVO.ArProcedureCode = arProce;

                List<APPOINTMENTVO> lstAPPOINTMENTVO = new List<APPOINTMENTVO>();
                List<APPOINTMENTVO> templstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);

                //foreach (APPOINTMENTVO _APPOINTMENTVO in templstAPPOINTMENTVO)
                //{

                //    if (loadAN(_APPOINTMENTVO) == true)
                //    {
                //        lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                //    }
                //    else if (loadVN(_APPOINTMENTVO) == true)
                //    {
                //        lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                //    }
                //    else if (_APPOINTMENTVO.ConfirmStatusType != 6)
                //    {
                //        lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                //    }

                //}

                foreach (APPOINTMENTVO _APPOINTMENTVO in templstAPPOINTMENTVO)
                {

                    if (_APPOINTMENTVO.ConfirmStatusType != 6)
                    {
                        lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                    }
                    else {
                        if (loadAN(_APPOINTMENTVO) == true)
                        {
                            lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                        }
                        else if (loadVN(_APPOINTMENTVO) == true)
                        {
                            lstAPPOINTMENTVO.Add(_APPOINTMENTVO);
                        }
                    }

                }

                gvAppointment.DataSource = lstAPPOINTMENTVO;
                gvAppointment.DataBind();
                gvAppointment.ShowHeaderWhenEmpty = true;

                ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
                ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                List<ORANESTHESIADOCTORSCHEDULEVO> lstORANESTHESIADOCTORSCHEDULEVO = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByKey(ORANESTHESIADOCTORSCHEDULEVO);

                lblAnesthesiaDoctor.Text = string.Empty;
                foreach (ORANESTHESIADOCTORSCHEDULEVO xx in lstORANESTHESIADOCTORSCHEDULEVO)
                {
                    if (!string.IsNullOrEmpty(lblAnesthesiaDoctor.Text))
                    {
                        lblAnesthesiaDoctor.Text += "<br/>";
                    }
                    lblAnesthesiaDoctor.Text += " <code>" + xx.StartAnesthesiaDateTime.Value.ToShortTimeString() + "</code> " + xx.DoctorName + " : " + xx.Reamrk;
                }

                ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
                ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
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
            catch (Exception ex)
            {
                throw ex;
            }

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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void gvAppointment_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string AppointmentNo = (e.Row.FindControl("hlAppointmentNo") as HyperLink).Text;
                ReturnValue rv = new BLORHEADER(dbInfo).CheckCountByAppointment(AppointmentNo);
                if (rv.Value)
                {
                    e.Row.Visible = false;
                }
                else
                {
                    e.Row.Cells[1].Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvAppointment, "select$" + e.Row.RowIndex);
                    e.Row.Cells[1].Attributes["style"] = "cursor:pointer";
                    e.Row.Cells[1].Attributes["data-toggle"] = "modal";
                    e.Row.Cells[1].Attributes["data-target"] = "#exampleModalLong";

                    Label _rowindex = e.Row.FindControl("lblIndex") as Label;

                    _rowindex.Text = (RowIndex + 1).ToString();
                    RowIndex++;
                    //DropDownList ddlgvORRoom = e.Row.FindControl("ddlgvORRoom") as DropDownList;
                    //ddlgvORRoom.DataSource = new BLOPERATIONROOM(dbInfo).SearchAll();
                    //ddlgvORRoom.DataValueField = "CODE";
                    //ddlgvORRoom.DataValueField = "NAME";
                    //ddlgvORRoom.DataBind();
                }
            }
            else
            {
                RowIndex = 0;
            }
        }

        protected void gvAppointment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Confirm")
            {
                //ScriptManager.RegisterStartupScript(this, this.Page.GetType(), "Script", "ShowInProcess()", true);

                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvAppointment.Rows[rowIndex];
                string apno = (row.FindControl("hlAppointmentNo") as HyperLink).Text;
                ReturnValue rv = Confirm(apno);
                if (rv.Value)
                {
                    AlertMessage(true, false, "Confirm Complete.");
                    //loadvalue();
                }
                else
                {
                    AlertMessage(true, true, "Confirm Fail.");
                }
                Response.Redirect(Request.RawUrl, true);
            }
            else if (e.CommandName == "select")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvAppointment.Rows[rowIndex];
                loadvalueDetail(row);
            }
        }

        protected void hdDate_ValueChanged(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            AlertMessage(false, true, string.Empty);
            loadvalue();
        }

        public void loadvalueDetail(GridViewRow row)
        {
            try
            {
                ClearvalueDetail();
                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentNo = (row.FindControl("hlAppointmentNo") as HyperLink).Text;

                List<APPOINTMENTVO> _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                if (_lstAPPOINTMENTVO.Count > 0)
                {
                    lblHN.Text = _lstAPPOINTMENTVO[0].HN;
                    lblPatientName.Text = _lstAPPOINTMENTVO[0].PatientName;
                    lblAppointmentNo.Text = _lstAPPOINTMENTVO[0].AppointmentNo;
                    lblAppointmentDateTime.Text = _lstAPPOINTMENTVO[0].strAppointmentDateTime;
                    lblProcedureName.Text = _lstAPPOINTMENTVO[0].ProcedureName;
                    lblDoctorName.Text = _lstAPPOINTMENTVO[0].DoctorName;
                    lblRemarksMemo.Text = _lstAPPOINTMENTVO[0].RemarksMemo;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearvalueDetail()
        {
            lblAppointmentNo.Text = string.Empty;
            lblAppointmentDateTime.Text = string.Empty;
            lblHN.Text = string.Empty;
            lblPatientName.Text = string.Empty;
            lblDoctorName.Text = string.Empty;
            lblProcedureName.Text = string.Empty;
            lblRemarksMemo.Text = string.Empty;
        }

        private ReturnValue Confirm(string AppointmentNo)
        {
            ReturnValue rv = new ReturnValue();
            try
            {
                APPOINTMENTVO APPOINTMENTVO = new APPOINTMENTVO();
                APPOINTMENTVO.AppointmentNo = AppointmentNo;
                List<APPOINTMENTVO> _lstAPPOINTMENTVO = new BLAPPOINTMENT(dbInfo).SearchByKey(APPOINTMENTVO);
                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORID = Guid.NewGuid().ToString();
                ORHEADERVO.HN = _lstAPPOINTMENTVO[0].HN;
                ORHEADERVO.ORDate = _lstAPPOINTMENTVO[0].AppointmentDateTime;
                ORHEADERVO.ORTime = "00:00:00";
                ORHEADERVO.ArrivalTime = "00:00:00";

                ORHEADERVO.Surgeon1 = _lstAPPOINTMENTVO[0].Doctor;
                ORHEADERVO.SurgeonMaster = _lstAPPOINTMENTVO[0].Doctor;
                ORHEADERVO.AppointmentNo = _lstAPPOINTMENTVO[0].AppointmentNo;

                if (_lstAPPOINTMENTVO[0].ProcedureCode == "35" || _lstAPPOINTMENTVO[0].ProcedureCode == "61")
                    ORHEADERVO.ORStatus = ((int)EnumOR.ORStatus.OPD).ToString();
                else if (_lstAPPOINTMENTVO[0].ProcedureCode == "36" || _lstAPPOINTMENTVO[0].ProcedureCode == "62")
                    ORHEADERVO.ORStatus = ((int)EnumOR.ORStatus.IPD).ToString();
                else if (_lstAPPOINTMENTVO[0].ProcedureCode == "38" || _lstAPPOINTMENTVO[0].ProcedureCode == "63")
                    ORHEADERVO.ORStatus = ((int)EnumOR.ORStatus.Observe).ToString();
                else if (_lstAPPOINTMENTVO[0].ProcedureCode == "64")
                    ORHEADERVO.ORStatus = ((int)EnumOR.ORStatus.Reserve).ToString();
                else
                    ORHEADERVO.ORStatus = ((int)EnumOR.ORStatus.None).ToString();

                ORHEADERVO.ORRoom = ddlORRoom.SelectedValue;
                ORHEADERVO.CreateDate = _lstAPPOINTMENTVO[0].MakeDateTime;
                rv = new BLORHEADER(dbInfo).Insert(ORHEADERVO);
            }
            catch (Exception ex)
            {
                rv.Value = false;
                rv.Exception = ex;
            }
            return rv;
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

        protected void btnConfrim_Click(object sender, EventArgs e)
        {
            try
            {
                ReturnValue rv = new ReturnValue();
                foreach (GridViewRow row in gvAppointment.Rows)
                {

                    bool ch = (row.FindControl("chbgvAppointment") as CheckBox).Checked;
                    if (ch)
                    {
                        string AppointmentNo = (row.FindControl("hlAppointmentNo") as HyperLink).Text;
                        rv = Confirm(AppointmentNo);
                    }
                }
                if (rv.Value)
                {
                    AlertMessage(true, false, "Confirm Complete.");
                    //loadvalue();
                    Response.Redirect(Request.RawUrl, true);
                }
            }
            catch (Exception ex)
            {
                AlertMessage(true, true, ex.Message);
            }
        }

        public bool loadAN(APPOINTMENTVO hd)
        {
            bool rv = false;
            try
            {

                VT_PATIENT_ANVO vl = new VT_PATIENT_ANVO();
                vl.HN = hd.HN;
                try
                {
                    vl.ORDateTime = DateTime.Parse(hd.AppointmentDateTime.Value.ToString("yyyy/MM/dd") + " " + hd.AppointmentDateTime);
                }
                catch
                {
                    vl.ORDateTime = DateTime.Parse(hd.AppointmentDateTime.Value.ToString("yyyy/MM/dd") + " 23:59:59");
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

        public bool loadVN(APPOINTMENTVO hd)
        {
            bool rv = false;
            try
            {
                VT_PATIENT_VNVO vl = new VT_PATIENT_VNVO();
                vl.HN = hd.HN;
                vl.ORDateTime = hd.AppointmentDateTime;
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