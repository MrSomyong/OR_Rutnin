using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Nurse
{
    public partial class Default : System.Web.UI.Page
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

                ORANESTHESIANURSESCHEDULEVO _ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
                _ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(DateFormat.dmy2ymd(hdDate.Value));
                List<ORANESTHESIANURSESCHEDULEVO> lstORANESTHESIANURSESCHEDULEVO = new BLORANESTHESIANURSESCHEDULE(dbInfo).SearchByKey(_ORANESTHESIANURSESCHEDULEVO);

                gvNurse.DataSource = lstORANESTHESIANURSESCHEDULEVO;
                gvNurse.DataBind();
                gvNurse.ShowHeaderWhenEmpty = true;

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
                ListItem lit = new ListItem();
                lit.Text = "None";
                lit.Value = "";

                ListItem litAnesthesiaNurse = new ListItem();
                litAnesthesiaNurse.Text = "None";
                litAnesthesiaNurse.Value = "";
                NURSEMASTERVO NURSEMASTERVO = new NURSEMASTERVO();
                NURSEMASTERVO.EDUCATIONSTANDARD = "AN";
                List<NURSEMASTERVO> lstNURSEMASTERVO = new BLNURSEMASTER(dbInfo).SearchByKey(NURSEMASTERVO);
                ddlNurse.DataSource = lstNURSEMASTERVO;
                ddlNurse.DataValueField = "CODE";
                ddlNurse.DataTextField = "NAME";
                ddlNurse.DataBind();
                ddlNurse.Items.Insert(0, lit);
                
                //List<NURSEMASTERVO> lstNURSEMASTERVO = new BLNURSEMASTER(dbInfo).SearchAll();
                //ddlNurse.DataSource = lstNURSEMASTERVO;
                //ddlNurse.DataValueField = "CODE";
                //ddlNurse.DataTextField = "NAME";
                //ddlNurse.DataBind();

                //ListItem lit = new ListItem();

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
            ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
            ORANESTHESIANURSESCHEDULEVO.ID = Guid.NewGuid().ToString();
            ORANESTHESIANURSESCHEDULEVO.NURSE = ddlNurse.SelectedValue;
            ORANESTHESIANURSESCHEDULEVO.Reamrk = txtReamrk.Text;
            string dd = DateFormat.dmy2ymd(hdDate.Value);
            string hh = ddlHH.SelectedValue;
            string mm = ddlMM.SelectedValue;
            string date = dd + " " + hh + ":" + mm;
            ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = DateTime.Parse(date);
            ReturnValue rv = new BLORANESTHESIANURSESCHEDULE(dbInfo).Insert(ORANESTHESIANURSESCHEDULEVO);
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

        protected void gvNurse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvNurse.Rows[e.RowIndex];
            string id = (row.FindControl("hdgvID") as HiddenField).Value;
            ReturnValue rv = new BLORANESTHESIANURSESCHEDULE(dbInfo).Delete(id);
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