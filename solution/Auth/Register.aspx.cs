using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Auth
{

    public partial class Register : System.Web.UI.Page
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
                ListItem lit = new ListItem();
                lit.Text = "None";
                lit.Value = "";

                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                ddlDoctor.DataSource = lstDOCTORMASTERVO;
                ddlDoctor.DataValueField = "DOCTOR";
                ddlDoctor.DataTextField = "DoctorName";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, lit);

                //lit = new ListItem();
                //lit.Text = "None";
                //lit.Value = "";

                SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
                List<SETUPLOGONVO> lstSETUPACCESSMENUVO = new BLSETUPLOGON(dbInfo).SearchByKeyAccessMenuCode(_SETUPLOGONVO);
                ddlAccessMenu.DataSource = lstSETUPACCESSMENUVO;
                ddlAccessMenu.DataValueField = "AccessCode";
                ddlAccessMenu.DataTextField = "AccessName";
                ddlAccessMenu.DataBind();
                ddlAccessMenu.Items.Insert(0, lit);
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
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

            string strError = string.Empty;

            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = Guid.NewGuid().ToString();
            SETUPLOGONVO.Username = txtUsername.Text.Trim().ToUpper();
            SETUPLOGONVO.Password = txtpassword.Text.Trim();
            SETUPLOGONVO.FirstName = txtfirstname.Text.Trim();
            SETUPLOGONVO.LastName = txtlastname.Text.Trim();
            SETUPLOGONVO.UseType = ddlUserType.SelectedValue;
            SETUPLOGONVO.Doctor = ddlDoctor.SelectedValue;

            SETUPLOGONVO.Active = true;
            SETUPLOGONVO.AdminType = false;
            SETUPLOGONVO.AccessCode = ddlAccessMenu.SelectedValue;

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

            //Appointment
            //if (chbAppointment.Checked == false)
            //{
            //    SETUPLOGONVO.UseAppointment = false;
            //}

            //View Booking
            //if (chbViewBooking.Checked == false)
            //{
            //    SETUPLOGONVO.UseViewBooking = false;
            //}

            //Post OR
            //if (chbPostOR.Checked == false)
            //{
            //    SETUPLOGONVO.UseOperation = false;
            //}

            //รายงานการส่งผ่าตัด
            //if (chbReport01.Checked == false)
            //{
            //    SETUPLOGONVO.UseReport = false;
            //}

            //Post Treatment
            //if (chbPostTreatment.Checked == false)
            //{
            //    SETUPLOGONVO.UsePostTreatment = false;
            //}

            //Enquiry Price
            //if (chbEnquiryPrice.Checked == false)
            //{
            //    SETUPLOGONVO.UseEnquiryPrice = false;
            //}

            //Injection Room
            //if (chbInjectionRoom.Checked == false)
            //{
            //    SETUPLOGONVO.UseInjectionRoom = false;
            //}

            //Report Post OP
            //if (chbReport02.Checked == false)
            //{
            //    SETUPLOGONVO.UseReportPostOP = false;
            //}

            //Setup Group Method
            //if (chbSetupgroupMethod.Checked == false)
            //{
            //    SETUPLOGONVO.UseSetupGroupMethod = false;
            //}

            //Setup All
            //if (chbSetupAll.Checked == false)
            //{
            //    SETUPLOGONVO.UseSetup = false;
            //}

            ReturnValue rtvcheck = new BLSETUPLOGON(dbInfo).Checkdup(SETUPLOGONVO.Username);
            if (rtvcheck.Value)
            {
                AlertMessage(true, "UserID have already.");
            }
            else
            {                
                ReturnValue rtv = new BLSETUPLOGON(dbInfo).Insert(SETUPLOGONVO);

                if (rtv.Value)
                {
                    AlertMessage(true, string.Empty);
                    Response.Redirect("/Auth/UserList", false);

                }
                else
                {
                    AlertMessage(true, rtv.Exception.Message);
                    txtUsername.CssClass = "";
                }
            }
        }
        private void AlertMessage(bool hidmsg, string msg)
        {
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }
    }
}