using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using DAL;

namespace solution.Auth
{
    public partial class Login : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            checkLogin();
        }

        private void checkLogin()
        {
            try
            {
                SETUPLOGONVO _SETUPLOGONVO = new SETUPLOGONVO();
                _SETUPLOGONVO.Username = txtUsername.Text.Trim().ToUpper();
                _SETUPLOGONVO.Password = txtpassword.Text;
                SETUPLOGONVO SETUPLOGONVO = new BLSETUPLOGON(dbInfo).CheckLogin(_SETUPLOGONVO);
                if (!string.IsNullOrEmpty(SETUPLOGONVO.UserID))
                {

                    //Session["USERID"] = SETUPLOGONVO.UserID;

                    SETUPLOGONVO SETUPLOGONV1 = new SETUPLOGONVO();
                    SETUPLOGONVO.AccessID = SETUPLOGONVO.AccessID;
                    List<SETUPLOGONVO> lstSETUPLOGONV1 = new BLSETUPLOGON(dbInfo).SearchByKeyAccessMenuCode(SETUPLOGONVO);

                    if (lstSETUPLOGONV1[0].UseReserve == true)
                    { Response.Redirect("/Reserve/", false); }
                    else if (lstSETUPLOGONV1[0].UseReserveReadOnly == true)
                    { Response.Redirect("/Reserve/Views", false); }
                    else if (lstSETUPLOGONV1[0].UseAppointment == true)
                    { Response.Redirect("/Appointment/", false); }
                    else if (lstSETUPLOGONV1[0].UseViewBooking == true)
                    { Response.Redirect("/DoctorSchedule/", false); }
                    else if (lstSETUPLOGONV1[0].UseOperation == true)
                    { Response.Redirect("/PostOR/", false); }
                    else if (lstSETUPLOGONV1[0].UsePostTreatment == true)
                    { Response.Redirect("/PostTreatment/", false); }
                    else if (lstSETUPLOGONV1[0].UseEnquiryPrice == true)
                    { Response.Redirect("/EnquirePrice/", false); }
                    else if (lstSETUPLOGONV1[0].UseInjectionRoom == true)
                    { Response.Redirect("/InjectionRoom/", false); }
                    else
                    { Response.Redirect("/Reserve/", false); }

                    Session["USERID"] = SETUPLOGONVO.UserID;
                    Session["USERNANME"] = SETUPLOGONVO.Username;
                    Session["NAME"] = SETUPLOGONVO.FirstName + " " + SETUPLOGONVO.LastName;
                    Session["USERTYPE"] = SETUPLOGONVO.UseType;

                    //if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.Adminstrator).ToString())
                    //{
                    //    Response.Redirect("/Reserve/", false);
                    //}
                    //else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.IT).ToString())
                    //{
                    //    Response.Redirect("/Reserve/", false);
                    //}
                    //else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.ReadOnly).ToString())
                    //{
                    //    Response.Redirect("/Reserve/Views", false);
                    //}
                    //else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.EnquireAndPostCharge).ToString())
                    //{
                    //    Response.Redirect("/PostTreatment/", false);
                    //}
                    //else if (Session["USERTYPE"].ToString() == ((int)EnumOR.UserType.User).ToString())
                    //{
                    //    if (SETUPLOGONVO.UseReserve == true)
                    //    { Response.Redirect("/Reserve/", false); }
                    //    else if (SETUPLOGONVO.UseReserveReadOnly == true)
                    //    { Response.Redirect("/Reserve/Views", false); }
                    //    else if (SETUPLOGONVO.UseAppointment == true)
                    //    { Response.Redirect("/Appointment/", false); }
                    //    else if (SETUPLOGONVO.UseViewBooking == true)
                    //    { Response.Redirect("/DoctorSchedule/", false); }
                    //    else if (SETUPLOGONVO.UseOperation == true)
                    //    { Response.Redirect("/PostOR/", false); }
                    //    else if (SETUPLOGONVO.UsePostTreatment == true)
                    //    { Response.Redirect("/PostTreatment/", false); }
                    //    else if (SETUPLOGONVO.UseEnquiryPrice == true)
                    //    { Response.Redirect("/EnquirePrice/", false); }
                    //    else if (SETUPLOGONVO.UseInjectionRoom == true)
                    //    { Response.Redirect("/InjectionRoom/", false); }
                    //    else
                    //    { Response.Redirect("/PostTreatment/", false); }                        
                    //}
                    //else
                    //{
                    //    Response.Redirect("/Reserve/Views", false);
                    //}                                    

                    try
                    {
                        string ComName;
                        string IP;
                        string clientMachineName;
                        clientMachineName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);

                        List<string> names = clientMachineName.ToUpper().Split('.').ToList();
                        string HostName = names[0];
                        ComName = HostName;
                        string clientAddress = HttpContext.Current.Request.UserHostAddress;
                        string userRequest = System.Web.HttpContext.Current.Request.UserHostAddress;
                        IP = Request.ServerVariables["remote_addr"].ToString();

                        var host = Dns.GetHostEntry(clientMachineName);
                        foreach (var ip in host.AddressList)
                        {
                            if (ip.AddressFamily == AddressFamily.InterNetwork)
                            {
                                IP = ip.ToString();
                            }
                        }

                        Session["IP"] = IP;
                        Session["COMNAME"] = ComName;

                    }
                    catch
                    { }

                }
                else
                {
                    divError.Visible = true;
                    lblMessageError.Text = "UserID or Password invalid. Please try again.";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}