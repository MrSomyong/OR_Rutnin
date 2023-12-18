using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
using System.Text;
using System.Data;

namespace solution.Reserve
{
    public partial class StatCase : System.Web.UI.Page
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
                MapDDL();

                if (Session["ORDateFrom"] != null)
                {
                    hdDateFrom.Value = Session["ORDateFrom"].ToString();
                    hdDateTo.Value = Session["ORDateTo"].ToString();
                }
                else
                {
                    hdDateFrom.Value = DateTime.Now.ToString("dd/MM/yyyy");
                    hdDateTo.Value = DateTime.Now.ToString("dd/MM/yyyy");
                }

                loadvalue();
            }
        }

        private void loadvalue()
        {
            try
            {
                ORHEADERVO ORHEADERVO = new ORHEADERVO();
                ORHEADERVO.ORDateFrom = DateTime.Parse(DateFormat.dmy2ymd(hdDateFrom.Value));
                ORHEADERVO.ORDateTo = DateTime.Parse(DateFormat.dmy2ymd(hdDateTo.Value));
                ORHEADERVO.Surgeon1 = ddlDoctor.SelectedValue;
                ORHEADERVO.AnesthesiaType1 = ddlAnesthesiaType.SelectedValue;
                ORHEADERVO.OROPERATIONVO = new OROPERATIONVO();
                ORHEADERVO.OROPERATIONVO.MainCode = ddlOperation.SelectedValue;
                ORHEADERVO.OROPERATIONVO.SubCode = ddlProcedure.SelectedValue;
                List<ORHEADERVO> lstORHEADERVO = new BLORHEADER(dbInfo).SearchStatCase(ORHEADERVO);

                gvStatCase.DataSource = lstORHEADERVO;

                int iSum = 0;
                foreach (ORHEADERVO lst in lstORHEADERVO)
                {
                    iSum += lst.OROPERATIONVO.QTY;
                }

                gvStatCase.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                gvStatCase.Columns[0].FooterStyle.Font.Bold = true;
                gvStatCase.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                gvStatCase.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                gvStatCase.DataBind();
                Session["gvStatCase"] = lstORHEADERVO;

                Session["ORDateFrom"] = hdDateFrom.Value;
                Session["ORDateTo"] = hdDateTo.Value;
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

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            //loadvalue();
            Response.Redirect("/Reserve/Report/StatCase_Print/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        private void MapDDL()
        {
            try
            {
                load_ddlDoctor();
                load_Operation();
                load_AnesthesiaType();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void load_ddlDoctor()
        {
            ListItem litddlDoctor = new ListItem();
            litddlDoctor.Text = "All";
            litddlDoctor.Value = "";
            DOCTORMASTERVO DOCTORMASTERVO = new DOCTORMASTERVO();
            List<DOCTORMASTERVO> lstDOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByKey(DOCTORMASTERVO);
            ddlDoctor.DataSource = lstDOCTORMASTERVO;
            ddlDoctor.DataValueField = "DOCTOR";
            ddlDoctor.DataTextField = "DoctorName";
            ddlDoctor.DataBind();
            ddlDoctor.Items.Insert(0, litddlDoctor);
        }

        private void load_Operation()
        {
            ListItem litOperation = new ListItem();
            litOperation.Text = "All";
            litOperation.Value = "";
            SETUPOPERATIONMAINVO SETUPOPERATIONMAINVO = new SETUPOPERATIONMAINVO();
            SETUPOPERATIONMAINVO.MainCode = ddlOperation.SelectedValue;
            List<SETUPOPERATIONMAINVO> lstSETUPOPERATIONMAINVO = new BLSETUPOPERATIONMAIN(dbInfo).SearchByKey(SETUPOPERATIONMAINVO);
            ddlOperation.DataSource = lstSETUPOPERATIONMAINVO;
            ddlOperation.DataValueField = "MainCode";
            ddlOperation.DataTextField = "Name";
            ddlOperation.DataBind();
            ddlOperation.Items.Insert(0, litOperation);
        }

        private void load_Procedure()
        {
            ListItem litProcedure = new ListItem();
            litProcedure.Text = "All";
            litProcedure.Value = "";
            SETUPOPERATIONSUBVO SETUPOPERATIONSUBVO = new SETUPOPERATIONSUBVO();
            SETUPOPERATIONSUBVO.MainCode = ddlOperation.SelectedValue;
            SETUPOPERATIONSUBVO.SubCode = ddlProcedure.SelectedValue;
            List<SETUPOPERATIONSUBVO> lstSETUPOPERATIONSUBVO = new BLSETUPOPERATIONSUB(dbInfo).SearchByKey(SETUPOPERATIONSUBVO);
            ddlProcedure.DataSource = lstSETUPOPERATIONSUBVO;
            ddlProcedure.DataValueField = "SubCode";
            ddlProcedure.DataTextField = "SubName";
            ddlProcedure.DataBind();
            ddlProcedure.Items.Insert(0, litProcedure);
        }

        private void load_AnesthesiaType()
        {
            ListItem litAnesthesiaType = new ListItem();
            litAnesthesiaType.Text = "All";
            litAnesthesiaType.Value = "";
            ANESTHESIAVO ANESTHESIAVO = new ANESTHESIAVO();
            ANESTHESIAVO.CODE = ddlAnesthesiaType.SelectedValue;
            List<ANESTHESIAVO> lstANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchByKey(ANESTHESIAVO);
            ddlAnesthesiaType.DataSource = lstANESTHESIAVO;
            ddlAnesthesiaType.DataValueField = "CODE";
            ddlAnesthesiaType.DataTextField = "NAME";
            ddlAnesthesiaType.DataBind();
            ddlAnesthesiaType.Items.Insert(0, litAnesthesiaType);
        }

        protected void ddlOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            load_Procedure();
        }

        protected void gvStatCase_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvStatCase.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvStatCase.Columns.Count;
                }
            }
        }
    }
}