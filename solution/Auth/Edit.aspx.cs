using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Auth
{

    public partial class Edit : System.Web.UI.Page
    {

        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
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
                if (Request.QueryString["id"] != null)
                {
                    string userid = Request.QueryString["id"];
                    loadvalue(userid);
                }
            }
        }

        private void loadvalue(string userid)
        {

            ListItem lit = new ListItem();
            lit.Text = "None";
            lit.Value = "";

            DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
            //DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
            ddlDoctor.DataSource = lstDOCTORMASTERVO;
            ddlDoctor.DataValueField = "DOCTOR";
            ddlDoctor.DataTextField = "DoctorName";
            ddlDoctor.DataBind();
            ddlDoctor.Items.Insert(0, lit);

            SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
            List<SETUPLOGONVO> lstSETUPACCESSMENUVO = new BLSETUPLOGON(dbInfo).SearchByKeyAccessMenuCode(_SETUPLOGONVO);
            ddlAccessMenu.DataSource = lstSETUPACCESSMENUVO;
            ddlAccessMenu.DataValueField = "AccessCode";
            ddlAccessMenu.DataTextField = "AccessName";
            ddlAccessMenu.DataBind();
            ddlAccessMenu.Items.Insert(0, lit);

            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = userid;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(SETUPLOGONVO);
            txtUsername.Text = lstSETUPLOGONVO[0].Username;
            txtfirstname.Text = lstSETUPLOGONVO[0].FirstName;
            txtlastname.Text = lstSETUPLOGONVO[0].LastName;
            chActive.Checked = lstSETUPLOGONVO[0].Active;
            ddlDoctor.SelectedValue = lstSETUPLOGONVO[0].Doctor;
            ddlAccessMenu.SelectedValue = lstSETUPLOGONVO[0].AccessCode;
            hdUserID.Value = userid;
            ddlUserType.SelectedValue = lstSETUPLOGONVO[0].UseType;
            txtUsername.ReadOnly = true;

            //if (lstSETUPLOGONVO[0].UseReserve == true)
            //{
            //    ddlReserveOR.SelectedValue = "1";
            //}
            //else if (lstSETUPLOGONVO[0].UseReserveReadOnly == true)
            //{
            //    ddlReserveOR.SelectedValue = "2";
            //}
            //else
            //{
            //    ddlReserveOR.SelectedValue = "0";
            //}

            //chbAppointment.Checked = lstSETUPLOGONVO[0].UseAppointment;
            //chbViewBooking.Checked = lstSETUPLOGONVO[0].UseViewBooking;
            //chbPostOR.Checked = lstSETUPLOGONVO[0].UseOperation;
            //chbReport01.Checked = lstSETUPLOGONVO[0].UseReport;
            //chbPostTreatment.Checked = lstSETUPLOGONVO[0].UsePostTreatment;
            //chbEnquiryPrice.Checked = lstSETUPLOGONVO[0].UseEnquiryPrice;
            //chbInjectionRoom.Checked = lstSETUPLOGONVO[0].UseInjectionRoom;
            //chbReport02.Checked = lstSETUPLOGONVO[0].UseReportPostOP;
            //chbSetupgroupMethod.Checked = lstSETUPLOGONVO[0].UseSetupGroupMethod;
            //chbSetupAll.Checked = lstSETUPLOGONVO[0].UseSetup;
                                 
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strError = string.Empty;

            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = hdUserID.Value;
            SETUPLOGONVO.Username = txtUsername.Text.Trim().ToUpper();
            SETUPLOGONVO.FirstName = txtfirstname.Text.Trim();
            SETUPLOGONVO.LastName = txtlastname.Text.Trim();
            SETUPLOGONVO.UseType = ddlUserType.SelectedValue;
            SETUPLOGONVO.Active = chActive.Checked;
            SETUPLOGONVO.AdminType = false;
            SETUPLOGONVO.Doctor = ddlDoctor.SelectedValue;
            SETUPLOGONVO.AccessCode = ddlAccessMenu.SelectedValue;

            //SETUPLOGONVO.Active = true;
            //SETUPLOGONVO.AdminType = false;
            //SETUPLOGONVO.UseAppointment = true;
            //SETUPLOGONVO.UseOperation = true;
            //SETUPLOGONVO.UseReport = true;
            //SETUPLOGONVO.UseReserve = true;
            //SETUPLOGONVO.UseSetup = true;

            //SETUPLOGONVO.UseViewBooking = true;
            //SETUPLOGONVO.UsePostTreatment = true;
            //SETUPLOGONVO.UseEnquiryPrice = true;
            //SETUPLOGONVO.UseInjectionRoom = true;
            //SETUPLOGONVO.UseReportPostOP = true;
            //SETUPLOGONVO.UseSetupGroupMethod = true;
            //SETUPLOGONVO.UseReserveReadOnly = true;

            //Reserve
            //if (ddlReserveOR.SelectedValue == "0")
            //{
            //    SETUPLOGONVO.UseReserve = false;
            //    SETUPLOGONVO.UseReserveReadOnly = false;
            //}
            //else if (ddlReserveOR.SelectedValue == "1")
            //{
            //    SETUPLOGONVO.UseReserve = true;
            //    SETUPLOGONVO.UseReserveReadOnly = false;
            //}
            //else
            //{
            //    SETUPLOGONVO.UseReserve = false;
            //    SETUPLOGONVO.UseReserveReadOnly = true;
            //}

            ////Appointment
            //if (chbAppointment.Checked == false)
            //{
            //    SETUPLOGONVO.UseAppointment = false;
            //}

            ////View Booking
            //if (chbViewBooking.Checked == false)
            //{
            //    SETUPLOGONVO.UseViewBooking = false;
            //}

            ////Post OR
            //if (chbPostOR.Checked == false)
            //{
            //    SETUPLOGONVO.UseOperation = false;
            //}

            ////รายงานการส่งผ่าตัด
            //if (chbReport01.Checked == false)
            //{
            //    SETUPLOGONVO.UseReport = false;
            //}

            ////Post Treatment
            //if (chbPostTreatment.Checked == false)
            //{
            //    SETUPLOGONVO.UsePostTreatment = false;
            //}

            ////Enquiry Price
            //if (chbEnquiryPrice.Checked == false)
            //{
            //    SETUPLOGONVO.UseEnquiryPrice = false;
            //}

            ////Injection Room
            //if (chbInjectionRoom.Checked == false)
            //{
            //    SETUPLOGONVO.UseInjectionRoom = false;
            //}

            ////Report Post OP
            //if (chbReport02.Checked == false)
            //{
            //    SETUPLOGONVO.UseReportPostOP = false;
            //}

            ////Setup Group Method
            //if (chbSetupgroupMethod.Checked == false)
            //{
            //    SETUPLOGONVO.UseSetupGroupMethod = false;
            //}

            ////Setup All
            //if (chbSetupAll.Checked == false)
            //{
            //    SETUPLOGONVO.UseSetup = false;
            //}

            ReturnValue rtv = new BLSETUPLOGON(dbInfo).Update(SETUPLOGONVO);

            if (rtv.Value)
            {
                AlertMessage(true, "Update Success.");
                divError.Attributes["class"] = "alert alert-success alert - dismissible fade show";

            }
            else
            {
                AlertMessage(true, rtv.Exception.Message);
            }

        }
        private void AlertMessage(bool hidmsg, string msg)
        {
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }
    }
}