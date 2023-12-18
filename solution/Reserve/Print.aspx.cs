using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace solution.Reserve
{
    public partial class Print : System.Web.UI.Page
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
            if (Request.QueryString["d"] != null)
            {
                string _date = Request.QueryString["d"];
                DateTime ordate = DateTime.Parse(_date);
                loadvalue(ordate);
            }
        }

        private void loadvalue(DateTime ordate)
        {
            try
            {
                loadAnesthesia(ordate);

                SETUPORROOMVO SETUPORROOMVO = new SETUPORROOMVO();
                List<SETUPORROOMVO> lstorromm = new BLSETUPORROOM(dbInfo).SearchByKey(SETUPORROOMVO);
                int i = 1;
                foreach (SETUPORROOMVO xx in lstorromm)
                {
                    if (i == 1)
                    {
                        pnORRoom1.Visible = true;
                        lblORRoom1.Text = xx.Name;
                        gvORRoom1.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom1.DataBind();
                    }
                    else if (i == 2)
                    {
                        pnORRoom2.Visible = true;
                        lblORRoom2.Text = xx.Name;
                        gvORRoom2.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom2.DataBind();
                    }
                    else if (i == 3)
                    {
                        pnORRoom3.Visible = true;
                        lblORRoom3.Text = xx.Name;
                        gvORRoom3.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom3.DataBind();
                    }
                    else if (i == 4)
                    {
                        pnORRoom4.Visible = true;
                        lblORRoom4.Text = xx.Name;
                        gvORRoom4.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom4.DataBind();
                    }
                    else if (i == 5)
                    {
                        pnORRoom5.Visible = true;
                        lblORRoom5.Text = xx.Name;
                        gvORRoom5.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom5.DataBind();
                    }
                    else if (i == 6)
                    {
                        pnORRoom6.Visible = true;
                        lblORRoom6.Text = xx.Name;
                        gvORRoom6.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom6.DataBind();
                    }
                    else if (i == 7)
                    {
                        pnORRoom7.Visible = true;
                        lblORRoom7.Text = xx.Name;
                        gvORRoom7.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom7.DataBind();
                    }
                    else if (i == 8)
                    {
                        pnORRoom8.Visible = true;
                        lblORRoom8.Text = xx.Name;
                        gvORRoom8.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom8.DataBind();
                    }
                    else if (i == 9)
                    {
                        pnORRoom9.Visible = true;
                        lblORRoom9.Text = xx.Name;
                        gvORRoom9.DataSource = loadORHeader(xx.CODE, ordate);
                        gvORRoom9.DataBind();
                    }
                    i++;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private List<ORHEADERVO> loadORHeader(string CODE, DateTime ordate)
        {
            List<ORHEADERVO> lstORHEADERVO = new List<ORHEADERVO>();
            List<ORHEADERVO> templstORHEADERVO = new BLORHEADER(dbInfo).SearchByRoom(CODE, ordate);
            foreach (ORHEADERVO ORHEADERVO in templstORHEADERVO)
            {
                //ORHEADERVO.ORPATIENTVO = new ORPATIENTVO();
                //ORPATIENTVO _ORPATIENTVO = new BLORPATIENT(dbInfo).SearchByHN(ORHEADERVO.HN);
                //ORHEADERVO.ORPATIENTVO.PatientName = _ORPATIENTVO.PatientName;
                //ORHEADERVO.ORPATIENTVO.BirthDateTime = _ORPATIENTVO.BirthDateTime;
                //ORHEADERVO.ORPATIENTVO.Age = ORUtils.getAge(_ORPATIENTVO.BirthDateTime);
                //ORHEADERVO.ORPATIENTVO.Sex = _ORPATIENTVO.Sex;
                //ORHEADERVO.ORPATIENTVO.Ref = _ORPATIENTVO.Ref;
                //ORHEADERVO.ORPATIENTVO.Nationality = _ORPATIENTVO.Nationality;
                //ORHEADERVO.ORPATIENTVO.Initial = _ORPATIENTVO.Initial;

                //ORHEADERVO.PatientName = _ORPATIENTVO.Initial + " " + _ORPATIENTVO.FirstName + " " + _ORPATIENTVO.LastName;

                string r = string.Empty;
                string l = string.Empty;
                string b = string.Empty;
                List<OROPERATIONVO> lstOROPERATIONVO = new BLOROPERATION(dbInfo).SearchByORID(ORHEADERVO.ORID);
                foreach (OROPERATIONVO op1 in lstOROPERATIONVO)
                {
                    if (op1.Side == (int)EnumOR.ORSide.RE)
                    {
                        if (op1.SubMark == "1")
                            r = r + "+" + op1.SubName;
                        else if (op1.SubMark == "2")
                            r = r + "+-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            r = r + "/" + op1.SubName;
                        else
                        {
                            if (r == "")
                                r = r + op1.SubName;
                            else
                                r = r + "," + op1.SubName;
                        }
                    }
                    if (op1.Side == (int)EnumOR.ORSide.LE)
                    {
                        if (op1.SubMark == "1")
                            l = l + "+" + op1.SubName;
                        else if (op1.SubMark == "2")
                            l = l + "+-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            l = l + "/" + op1.SubName;
                        else
                        {
                            if (l == "")
                                l = l + op1.SubName;
                            else
                                l = l + "," + op1.SubName;
                        }
                    }
                    if (op1.Side == (int)EnumOR.ORSide.BE)
                    {
                        if (op1.SubMark == "1")
                            b = b + "+" + op1.SubName;
                        else if (op1.SubMark == "2")
                            b = b + "+-" + op1.SubName;
                        else if (op1.SubMark == "3")
                            b = b + "/" + op1.SubName;
                        else
                        {
                            if (b == "")
                                b = b + op1.SubName;
                            else
                                b = b + "," + op1.SubName;
                        }
                    }
                }
                DOCTORMASTERVO DOCTORMASTERVO = new BLDOCTORMASTER(dbInfo).SearchByCode(ORHEADERVO.Surgeon1);
                ORHEADERVO.Surgeon1 = DOCTORMASTERVO.DoctorName;
                if (!string.IsNullOrEmpty(r))
                    r = " Right : " + r;
                if (!string.IsNullOrEmpty(l))
                    l = " Left : " + l;
                if (!string.IsNullOrEmpty(b))
                    b = " Boht : " + b;
                OROPERATIONVO OROPERATIONVO = new OROPERATIONVO();
                OROPERATIONVO.strSide = r + l + b;
                ORHEADERVO.OROPERATIONVO = OROPERATIONVO;

                ANESTHESIAVO ANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType1);
                ORHEADERVO.AnesthesiaType = ANESTHESIAVO.NAME;
                if (ORHEADERVO.AnesthesiaSign == "1")
                    ORHEADERVO.AnesthesiaType += "+";
                else if (ORHEADERVO.AnesthesiaSign == "2")
                    ORHEADERVO.AnesthesiaType += "-";
                else
                    ORHEADERVO.AnesthesiaType += " ";

                ANESTHESIAVO = new BLANESTHESIA(dbInfo).SearchByCode(ORHEADERVO.AnesthesiaType2);
                ORHEADERVO.AnesthesiaType += ANESTHESIAVO.NAME;

                lstORHEADERVO.Add(ORHEADERVO);
            }

            return lstORHEADERVO;
        }

        private void loadAnesthesia(DateTime ordate)
        {
            int i = 1;
            lblAnesthesiaDoctor.Text = string.Empty;
            ORANESTHESIADOCTORSCHEDULEVO ORANESTHESIADOCTORSCHEDULEVO = new ORANESTHESIADOCTORSCHEDULEVO();
            ORANESTHESIADOCTORSCHEDULEVO.StartAnesthesiaDateTime = ordate;
            List<ORANESTHESIADOCTORSCHEDULEVO> lstORANESTHESIADOCTORSCHEDULEVO = new BLORANESTHESIADOCTORSCHEDULE(dbInfo).SearchByKey(ORANESTHESIADOCTORSCHEDULEVO);
            foreach (ORANESTHESIADOCTORSCHEDULEVO xx in lstORANESTHESIADOCTORSCHEDULEVO)
            {
                if (!string.IsNullOrEmpty(lblAnesthesiaDoctor.Text))
                {
                    lblAnesthesiaDoctor.Text += "<br/>" + i + ". ";
                }
                else
                {
                    lblAnesthesiaDoctor.Text += i + ". ";
                }
                lblAnesthesiaDoctor.Text += xx.DoctorName;
                i++;
            }

            ORANESTHESIANURSESCHEDULEVO ORANESTHESIANURSESCHEDULEVO = new ORANESTHESIANURSESCHEDULEVO();
            ORANESTHESIANURSESCHEDULEVO.StartAnesthesiaDateTime = ordate;
            List<ORANESTHESIANURSESCHEDULEVO> lstORANESTHESIANURSESCHEDULEVO = new BLORANESTHESIANURSESCHEDULE(dbInfo).SearchByKey(ORANESTHESIANURSESCHEDULEVO);
            lblAnesthesiaNurse.Text = string.Empty;
            i = 1;
            foreach (ORANESTHESIANURSESCHEDULEVO xx in lstORANESTHESIANURSESCHEDULEVO)
            {
                if (!string.IsNullOrEmpty(lblAnesthesiaNurse.Text))
                {
                    lblAnesthesiaNurse.Text += "<br/>" + i + ". ";
                }
                else
                {
                    lblAnesthesiaNurse.Text += i + ". ";
                }
                lblAnesthesiaNurse.Text += xx.Name;
                i++;
            }
        }
    }
}