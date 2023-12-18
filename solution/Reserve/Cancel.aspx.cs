using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Reserve
{
    public partial class Cancel : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;

        System.Globalization.CultureInfo cultureinfo_us = new System.Globalization.CultureInfo("en-US");
        System.Globalization.CultureInfo cultureinfo_th = new System.Globalization.CultureInfo("th-TH");
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
                setdefaultvalue();
                MapDDLREASON();
                if (Request.QueryString["d"] != null)
                {
                    hdORID.Value = Request.QueryString["d"];
                    loadvalue(hdORID.Value);
                }
                else
                {
                    Response.Redirect("/Reserve/", false);
                }
            }
            

        }

        protected void Clear_Click(object sender, EventArgs e)
        {
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
            divError.Visible = false;

        }

        public void loadvalue(string orid)
        {
            try
            {
                ClearSelection();
                ORHEADERVO xx = new ORHEADERVO();
                xx.ORID = orid;

                List<ORHEADERVO> lsth = new BLORHEADER(dbInfo).SearchByKey(xx);

                foreach (ORHEADERVO hd in lsth)
                {
                    ORPATIENTVO ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(hd.HN);
                    if (!string.IsNullOrEmpty(ORPATIENTVO.HN))
                    {
                        txtHN.Text = ORPATIENTVO.HN;
                        lblPatientName.Text = ORPATIENTVO.PatientName;
                        lblGender.Text = ORPATIENTVO.Sex;
                        lblBirthDateTime.Text = CultureInfo.GetDatetime(ORPATIENTVO.BirthDateTime.Value, YearType.Thai).ToString("dd MMM yyyy");
                        lblAge.Text = ORUtils.getAge(ORPATIENTVO.BirthDateTime);
                        divError.Visible = false;

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ClearSelection()
        {
            
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
        }
        private List<ORPATIENTVO> getORPatient(string strSearch)
        {
            List<ORPATIENTVO> lstOR = new List<ORPATIENTVO>();
            lstOR = new BLORPATIENT(dbInfo).SearchByKey(strSearch);

            return lstOR;
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

        protected void btnCancelApp_Click(object sender, EventArgs e)
        {
            ORHEADERVO ORHEADERVO = new ORHEADERVO();
            ORHEADERVO.ORID = hdORID.Value;
            ORHEADERVO.CxlByUser = Session["USERID"].ToString();
            ORHEADERVO.CxlReason = ddlREASON.SelectedValue;
            ReturnValue rv = new BLORHEADER(dbInfo).CancelApp(ORHEADERVO);
            if (rv.Value)
            {
                ORLogVO orlog = new ORLogVO();
                orlog.ORID = ORHEADERVO.ORID;
                orlog.HN = ORHEADERVO.HN;
                orlog.PatientName = ORHEADERVO.PatientName;
                orlog.Detail = "ยกเลิก : " + ddlREASON.SelectedItem.Text;
                orlog.UpdateBy = Session["USERID"].ToString();
                ReturnValue rv1 = new BLORLog(dbInfo).Insert(orlog);
                if (rv.Value)
                {
                    //Happy
                }
            }
            Response.Redirect("/Reserve/", false);
        }

        protected void ddlREASON_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hdREASON.Value = ddlREASON.SelectedValue;
        }
    }
}