using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Setup
{
    public partial class Printer : System.Web.UI.Page
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
                loadvalue();
            }
        }

        private void loadvalue()
        {
            try
            {

                SETUPPRINTERVO _SETUPPRINTERVO = new SETUPPRINTERVO();
                List<SETUPPRINTERVO> lstSETUPPRINTERVO = new BLSETUPPRINTER(dbInfo).SearchByKey(_SETUPPRINTERVO);

                gvPrinter.DataSource = lstSETUPPRINTERVO;
                gvPrinter.DataBind();
                gvPrinter.ShowHeaderWhenEmpty = true;

                ClearValue();

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

        private void ClearValue()
        {
            btnAdd.Text = "เพิ่ม";
            hdEvent.Value = "Add";
            txtPrinterName.Text = string.Empty;
            txtPrinterPath.Text = string.Empty;
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            SETUPPRINTERVO SETUPPRINTERVO = new SETUPPRINTERVO();
            
            SETUPPRINTERVO.Name = txtPrinterName.Text;
            SETUPPRINTERVO.Path = txtPrinterPath.Text;
            if (hdEvent.Value == "Add")
            {
                SETUPPRINTERVO.ID = Guid.NewGuid().ToString();
                ReturnValue rv = new BLSETUPPRINTER(dbInfo).Insert(SETUPPRINTERVO);
                if (rv.Value)
                {
                    loadvalue();
                }
                else
                {
                    AlertMessage(true, rv.Exception.Message);
                }
            }
            else if (hdEvent.Value == "Edit")
            {
                SETUPPRINTERVO.ID = hdID.Value;
                ReturnValue rv = new BLSETUPPRINTER(dbInfo).Update(SETUPPRINTERVO);
                if (rv.Value)
                {
                    loadvalue();
                }
                else
                {
                    AlertMessage(true, rv.Exception.Message);
                }
            }
        }

        private void AlertMessage(bool hidmsg, string msg)
        {
            divError.Visible = hidmsg;
            lblMessageError.Text = msg;
        }

        protected void gvPrinter_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["ondblclick"] = Page.ClientScript.GetPostBackClientHyperlink(gvPrinter, "Edit$" + e.Row.RowIndex);
                e.Row.Attributes["style"] = "cursor:pointer";
            }
        }

        protected void gvPrinter_RowEditing(object sender, GridViewEditEventArgs e)
        {

            foreach (GridViewRow r in gvPrinter.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    r.BackColor = System.Drawing.Color.White;
                }
            }
            //loadgvmain();
            GridViewRow row = gvPrinter.Rows[e.NewEditIndex];
            row.BackColor = System.Drawing.Color.LightPink;
            SETUPPRINTERVO SETUPPRINTERVO = new SETUPPRINTERVO();
            SETUPPRINTERVO.ID = (row.FindControl("hdgvID") as HiddenField).Value;
            loadEdit(SETUPPRINTERVO);
        }

        private void loadEdit(SETUPPRINTERVO SETUPPRINTERVO)
        {
            List<SETUPPRINTERVO> lstSETUPPRINTERVO = new BLSETUPPRINTER(dbInfo).SearchByKey(SETUPPRINTERVO);
            if (lstSETUPPRINTERVO.Count > 0)
            {
                hdID.Value = lstSETUPPRINTERVO[0].ID;
                txtPrinterName.Text = lstSETUPPRINTERVO[0].Name;
                txtPrinterPath.Text = lstSETUPPRINTERVO[0].Path;
                hdEvent.Value = "Edit";
                btnAdd.Text  = "บันทึก";
            }
        }

        protected void gvPrinter_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridViewRow row = gvPrinter.Rows[e.RowIndex];
            string id = (row.FindControl("hdgvID") as HiddenField).Value;
            ReturnValue rv = new BLSETUPPRINTER(dbInfo).Delete(id);
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