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
    public partial class Anesthesia : System.Web.UI.Page
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

                if (Session["ORDateForm"] != null)
                {
                    hdDateFrom.Value = Session["ORDateForm"].ToString();
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

                List<ORHEADERVO> lstAnesthesiaType = new BLORHEADER(dbInfo).SearchAnesthesiaType1(ORHEADERVO);
                gvAnesthesia.DataSource = lstAnesthesiaType;

                int iSum = 0;
                foreach (ORHEADERVO lst in lstAnesthesiaType)
                {
                    iSum += lst.AnesthesiaTypeQTY;
                }

                gvAnesthesia.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                gvAnesthesia.Columns[0].FooterStyle.Font.Bold = true;
                gvAnesthesia.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                gvAnesthesia.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                gvAnesthesia.DataBind();
                Session["gvAnesthesia"] = lstAnesthesiaType;

                ORHEADERVO.AnesthesiaSign = "1";
                List<ORHEADERVO> lstAnesthesiaType1 = new BLORHEADER(dbInfo).SearchAnesthesiaType2(ORHEADERVO);
                gvAnesthesia1.DataSource = lstAnesthesiaType1;

                iSum = 0;
                foreach (ORHEADERVO lst in lstAnesthesiaType1)
                {
                    iSum += lst.AnesthesiaTypeQTY;
                }

                gvAnesthesia1.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                gvAnesthesia1.Columns[0].FooterStyle.Font.Bold = true;
                gvAnesthesia1.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                gvAnesthesia1.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                gvAnesthesia1.DataBind();
                Session["gvAnesthesia1"] = lstAnesthesiaType1;

                ORHEADERVO.AnesthesiaSign = "2";
                List<ORHEADERVO> lstAnesthesiaType2 = new BLORHEADER(dbInfo).SearchAnesthesiaType2(ORHEADERVO);
                gvAnesthesia2.DataSource = lstAnesthesiaType2;

                iSum = 0;
                foreach (ORHEADERVO lst in lstAnesthesiaType2)
                {
                    iSum += lst.AnesthesiaTypeQTY;
                }

                gvAnesthesia2.Columns[0].FooterText = "Total Case   :   " + iSum.ToString();
                gvAnesthesia2.Columns[0].FooterStyle.Font.Bold = true;
                gvAnesthesia2.Columns[0].FooterStyle.HorizontalAlign = HorizontalAlign.Center;
                gvAnesthesia2.Columns[0].FooterStyle.BackColor = System.Drawing.Color.LightPink;

                gvAnesthesia2.DataBind();

                Session["gvAnesthesia2"] = lstAnesthesiaType2;

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
            Response.Redirect("/Reserve/Report/Anesthesia_Print/", false);
            //Response.Redirect("/Print/ORHeader/?d=" + hdDate.Value, false);
        }

        private void MapDDL()
        {
            try
            {
                load_ddlDoctor();
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

        protected void gvAnesthesia_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia.Columns.Count;
                }
            }
        }

        protected void gvAnesthesia1_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia1.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia1.Columns.Count;
                }
            }
        }

        protected void gvAnesthesia2_RowCreated(object sender, GridViewRowEventArgs e)
        {
            {
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    for (int i = 0; i < gvAnesthesia2.Columns.Count - 1; i++)
                    {
                        e.Row.Cells.RemoveAt(1);
                    }
                    e.Row.Cells[0].ColumnSpan = gvAnesthesia2.Columns.Count;
                }
            }
        }
    }
}