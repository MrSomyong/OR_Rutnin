using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Doctor
{
    public partial class Default : System.Web.UI.Page
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
        }

        private void loadvalue()
        {
            try
            {

                ORANESTHESIADOCTORSCHEDULEVO _ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
                _ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                List<ORANESTHESIADOCTORSCHEDULEVO> lstORANESTHESIADOCTORSCHEDULEVO = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByKey(_ORANESTHESIADOCTORSCHEDULEVO);

                gvDoctor.DataSource = lstORANESTHESIADOCTORSCHEDULEVO;
                gvDoctor.DataBind();
                gvDoctor.ShowHeaderWhenEmpty = true;

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

        private void MapDDL()
        {
            try
            {

                //List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchAll();
                //ddlDoctor.DataSource = lstDOCTORMASTERVO;
                //ddlDoctor.DataValueField = "DOCTOR";
                //ddlDoctor.DataTextField = "DoctorName";
                //ddlDoctor.DataBind();

                //ListItem lit = new ListItem();

                ListItem lit = new ListItem();
                lit.Text = "None";
                lit.Value = "";

                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "AD";
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
                ddlDoctor.DataSource = lstDOCTORMASTERVO;
                ddlDoctor.DataValueField = "DOCTOR";
                ddlDoctor.DataTextField = "DoctorName";
                ddlDoctor.DataBind();
                ddlDoctor.Items.Insert(0, lit);

                for (int i = 0; i < 24; i++)
                {
                    lit = new ListItem();
                    lit.Value = i.ToString("0#");
                    lit.Text = i.ToString("0#");
                    ddlHH.Items.Insert(i, lit);
                }

                lit = new ListItem();

                for (int i = 0; i < 60; i++)
                {
                    lit = new ListItem();
                    lit.Value = i.ToString("0#");
                    lit.Text = i.ToString("0#");
                    ddlMM.Items.Insert(i, lit);
                }

            }
            catch (Exception ex)
            {
                AlertMessage(true, ex.Message);
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
            ORANESTHESIADOCTORSCHEDULEVO.ID = Guid.NewGuid().ToString();
            ORANESTHESIADOCTORSCHEDULEVO.Doctor = ddlDoctor.SelectedValue;
            ORANESTHESIADOCTORSCHEDULEVO.Reamrk = txtReamrk.Text;
            string dd = DateFormat.dmy2ymd(hdDate.Value);
            string hh = ddlHH.SelectedValue;
            string mm = ddlMM.SelectedValue;
            string date = dd + " " + hh + ":" + mm;
            ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(date);
            ReturnValue rv = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).Insert(ORANESTHESIADOCTORSCHEDULEVO);
            if (rv.Value)
            {
                loadvalue();
            }
            else
            {
                AlertMessage(true, rv.Exception.Message);
            }
        }
        private void AlertMessage(bool hidmsg, string msg)
        {
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }

        protected void gvDoctor_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvDoctor.Rows[e.RowIndex];
            string id = (row.FindControl("hdgvID") as HiddenField).Value;
            ReturnValue rv = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).Delete(id);
            if (!rv.Value)
            {
                AlertMessage(true, rv.Exception.Message);
            }
            else
            {

                loadvalue();

            }
        }
    }
}