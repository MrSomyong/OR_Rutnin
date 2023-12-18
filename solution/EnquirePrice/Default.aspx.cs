using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


using DAL;
using DAL.Info;

namespace solution.EnquirePrice
{
    public partial class Default : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected DatabaseInfo appConnDBInfo = GParameters.AppConnDBInfo;
        protected DatabaseInfo extConnDBInfo = GParameters.ExtConnDBInfo;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["USERID"] == null)
            {
                Response.Redirect("/Auth/Login");

                Response.End();

            }
            //else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.ReadOnly).ToString())
            //{
            //    Response.Redirect("/Reserve/Views");

            //    Response.End();
            //}
            if (!IsPostBack)
            {
                if (Session["VisitDate"] != null)
                {
                    try
                    {
                        hdVisitDate.Value = DateTime.Parse(Session["VisitDate"].ToString()).ToString("dd/MM/yyyy");
                        hdVisitDateEn.Value = DateTime.Parse(Session["VisitDate"].ToString()).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        hdVisitDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                        hdVisitDateEn.Value = DateTime.Now.ToString("dd/MM/yyyy");
                        hdVisitDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);
                    }
                }
                else
                {
                    hdVisitDate.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdVisitDateEn.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdVisitDateEn.Value = CultureInfo.GetDateString(DateTime.Now, YearType.English);
                }
                //LoadClinic();
                MapDDL();
                //loadvalue();
                txtVN.Focus();
            }
        }
        private void LoadClinic()
        {
            //try
            //{
            //    ListItem litClinic = new ListItem();
            //    litClinic.Text = "None";
            //    litClinic.Value = "";
            //    VT_CLINIC VT_STORE = new VT_CLINIC();

            //    List<VT_CLINIC> lstClinic = new BLVT_CLINIC(extConnDBInfo).SearchAll();
            //    ddlClinic.DataSource = lstClinic;
            //    ddlClinic.DataValueField = "CLINIC_CODE";
            //    ddlClinic.DataTextField = "CLINIC_NAME_ENG";
            //    ddlClinic.DataBind();
            //    ddlClinic.Items.Insert(0, litClinic);


            //    string HostName = Dns.GetHostName();
            //    SETUPCOMPUTER SetupComputer = new BLSETUPCOMPUTER(appConnDBInfo).SearchByKey(HostName);

            //    ddlClinic.ClearSelection();
            //    if (!string.IsNullOrEmpty(SetupComputer.ComputerCode))
            //    {
            //        hfClinic.Value = SetupComputer.DefaultClinicCode;
            //        string strClinicCode = "'" + SetupComputer.DefaultClinicCode.Replace(",", "','") + "'";
            //        string alertScript = string.Format(@"javascript: $(document).ready(function(){{
            //                                $(""[id*='ddlClinic']"").val([{0}]).trigger('change');
            //                                }});   
            //    ", strClinicCode);
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
            //                            alertScript, true);
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void MapDDL()
        {
            try
            {
                ListItem litSurgeon = new ListItem();
                litSurgeon.Text = "Select All";
                litSurgeon.Value = "";
                DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
                DOCTORMASTERVO.EDUCATIONSTANDARD = "OD";
                List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(extConnDBInfo).SearchByKey(DOCTORMASTERVO);
                ddlSurgeon.DataSource = lstDOCTORMASTERVO;
                ddlSurgeon.DataValueField = "DOCTOR";
                ddlSurgeon.DataTextField = "DoctorName";
                ddlSurgeon.DataBind();
                ddlSurgeon.Items.Insert(0, litSurgeon);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void loadvalue()
        {
            try
            {
                               
                if (string.IsNullOrEmpty(hdVisitDate.Value))
                {

                    DateTime dd = DateTime.Now;
                    hdVisitDate.Value = dd.ToString("dd/MM/yyyy");
                    hdVisitDateEn.Value = dd.ToString("dd/MM/yyyy");

                }
                else
                {

                    DateTime dd = DateTime.ParseExact(hdVisitDateEn.Value, "dd/MM/yyyy", null);
                    hdVisitDate.Value = dd.ToString("dd/MM/yyyy");
                    hdVisitDateEn.Value = dd.ToString("dd/MM/yyyy");
                }

                //DateTime dtVisitdate = DateTime.Parse(hdVisitDateEn.Value);
                DateTime dtVisitdate = DateTime.ParseExact(hdVisitDateEn.Value, "dd/MM/yyyy", null);
                Session["VisitDate"] = dtVisitdate.ToString("dd/MM/yyyy");
                
                DateTime dtGetVisitdate = DateTime.ParseExact(hdVisitDateEn.Value, "dd/MM/yyyy", null);
                
                VT_VNMASTER _VT_VNMASTER = new VT_VNMASTER();
                _VT_VNMASTER.VISITDATE = dtGetVisitdate;
                _VT_VNMASTER.HN = txtHN.Text.Trim();
                _VT_VNMASTER.VN = txtVN.Text.Trim();
                _VT_VNMASTER.DOCTOR = ddlSurgeon.SelectedValue;
                _VT_VNMASTER.CLINIC = hfClinic.Value;
                _VT_VNMASTER.OutFlag = !string.IsNullOrWhiteSpace(ddlPaidOut.SelectedValue) ? Convert.ToBoolean(Convert.ToInt32(ddlPaidOut.SelectedValue)) : _VT_VNMASTER.OutFlag = null;
                _VT_VNMASTER.CloseVisitFlag = !string.IsNullOrWhiteSpace(ddlCloseVisit.SelectedValue) ? Convert.ToBoolean(Convert.ToInt32(ddlCloseVisit.SelectedValue)) : _VT_VNMASTER.CloseVisitFlag = null;
                List<VT_VNMASTER> lstVNMST = new BLVT_VNMASTER(extConnDBInfo).SearchVT_VNMASTERByKey(_VT_VNMASTER);
                gvPostTreatment.DataSource = lstVNMST;
                gvPostTreatment.DataBind();

                string strClinicCode = "'" + hfClinic.Value.Replace(",", "','") + "'";
                string alertScript = string.Format(@"javascript: $(document).ready(function(){{
                                            $(""[id*='ddlClinic']"").val([{0}]).trigger('change');
                                            }});   
                ", strClinicCode);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript",
                                    alertScript, true);
                txtVN.Focus();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtnOPDSearch_Click(object sender, EventArgs e)
        {
            loadvalue();
        }

        protected void gvPostTreatment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvPostTreatment.Rows[index];
                string HN = gvPostTreatment.DataKeys[row.RowIndex].Values["HN"].ToString();
                string VN = gvPostTreatment.DataKeys[row.RowIndex].Values["VN"].ToString();
                DateTime visitDate = DateTime.Parse(gvPostTreatment.DataKeys[row.RowIndex].Values["VISITDATE"].ToString());
                string suffix = gvPostTreatment.DataKeys[row.RowIndex].Values["SUFFIX"].ToString();
                string strVisitDate = Convert.ToDateTime(visitDate).ToString("yyyyMMdd");

                Response.Redirect(string.Format("/EnquirePrice/Main/?hn={0}&vn={1}&vndate={2}&suffix={3}", HN, VN, strVisitDate, suffix), false);
            }

           
        }
    }
}