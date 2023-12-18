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

namespace solution
{
    public partial class SiteMaster : MasterPage
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
            else
            {
                //string UserType = Session["USERTYPE"].ToString();
                //setMenu(UserType);
                setMenu(false);
                loadvalue(Session["USERID"].ToString());
                //IPAddress.Text= Session["IP"].ToString();
                //ComputerName.Text = Session["COMNAME"].ToString();

                try
                {

                    if (Request.QueryString["COMNAME"] != null)
                    {
                        IPAddress.Text = Session["IP"].ToString();
                        ComputerName.Text = Session["COMNAME"].ToString();
                    }
                    else
                    {
                        string clientMachineName;
                        clientMachineName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);

                        List<string> names = clientMachineName.ToUpper().Split('.').ToList();
                        string HostName = names[0];
                        ComputerName.Text = HostName;
                        string clientAddress = HttpContext.Current.Request.UserHostAddress;
                        string userRequest = System.Web.HttpContext.Current.Request.UserHostAddress;
                        IPAddress.Text = Request.ServerVariables["remote_addr"].ToString();

                        var host = Dns.GetHostEntry(clientMachineName);
                        foreach (var ip in host.AddressList)
                        {
                            if (ip.AddressFamily == AddressFamily.InterNetwork)
                            {
                                IPAddress.Text = ip.ToString();
                            }
                        }

                        Session["IP"] = IPAddress.Text;
                        Session["COMNAME"] = ComputerName.Text;
                    }
                }
                catch
                { }


            }
        }
        //private void setMenu(string UserType)
        //{
        //    setMenu(false);
        //    loadvalue(Session["USERID"].ToString());

        //    //if (UserType == ((int)EnumOR.UserType.ReadOnly).ToString())
        //    //{
        //    //    menuReserveReadOnly.Visible = true;
        //    //    menuAppointment.Visible = true;
        //    //}
        //    //else if (UserType == ((int)EnumOR.UserType.EnquireOnly).ToString())
        //    //{
        //    //    menuEnquiryPrice.Visible = true;
        //    //}
        //    //else if (UserType == ((int)EnumOR.UserType.EnquireAndPostCharge).ToString())
        //    //{
        //    //    menuPostTreatment.Visible = true;
        //    //    menuEnquiryPrice.Visible = true;
        //    //}
        //    //else
        //    //{
        //        //menuReserveReadOnly.Visible = false;
        //        //menuReserve.Visible = true;
        //        //menuReport.Visible = true;
        //        //menuAppointment.Visible = true;
        //        //menuSetup.Visible = true;
        //        //menuInjectionRoom.Visible = true;
        //        //menuEnquiryPrice.Visible = true;
        //        //menuViewBooking.Visible = true;
        //        //menuOperation.Visible = true;
        //        //menuPostTreatment.Visible = true;
        //        //menuReportPostOP.Visible = true;
        //        //menuSetupGroupMethod.Visible = true;
        //    //}
        //}
        private void setMenu(bool val)
        {
            menuReserveReadOnly.Visible = val;
            menuReserve.Visible = val;
            menuReport.Visible = val;
            menuAppointment.Visible = val;
            menuInjectionRoom.Visible = val;
            menuEnquiryPrice.Visible = val;
            menuViewBooking.Visible = val;
            menuOperation.Visible = val;
            menuPostTreatment.Visible = val;
            menuReportPostOP.Visible = val;
            menuSetupGroupMethod.Visible = val;
            menuChangePassword.Visible = val;

            menuSetup.Visible = val;
            menuSetupDoctor.Visible = val;
            menuSetupNurse.Visible = val;

        }
        
        private void loadvalue(string userid)
        {

            SETUPLOGONVO SETUPLOGONVO = new SETUPLOGONVO();
            SETUPLOGONVO.UserID = userid;
            List<SETUPLOGONVO> lstSETUPLOGONVO = new BLSETUPLOGON(dbInfo).SearchByKey(SETUPLOGONVO);
            if (lstSETUPLOGONVO.Count > 0)
            {

                User.Text = lstSETUPLOGONVO[0].Name;

                SETUPLOGONVO SETUPLOGONV1 = new SETUPLOGONVO();
                if (lstSETUPLOGONVO[0].AccessID == "")
                { SETUPLOGONVO.AccessID = "XXXX"; }
                else
                { SETUPLOGONVO.AccessID = lstSETUPLOGONVO[0].AccessID;}
                List<SETUPLOGONVO> lstSETUPLOGONV1 = new BLSETUPLOGON(dbInfo).SearchByKeyAccessMenuCode(SETUPLOGONVO);

                Session["USERID"] = userid;

                if (lstSETUPLOGONV1.Count > 0)
                {

                    if (lstSETUPLOGONV1[0].UseReserve == true)
                    {
                        menuReserveReadOnly.Visible = false;
                        menuReserve.Visible = true;
                    }
                    else if (lstSETUPLOGONV1[0].UseReserveReadOnly == true)
                    {
                        menuReserveReadOnly.Visible = true;
                        menuReserve.Visible = false;
                    }
                    else
                    {
                        menuReserveReadOnly.Visible = false;
                        menuReserve.Visible = false;
                    }

                    menuAppointment.Visible = lstSETUPLOGONV1[0].UseAppointment;
                    menuViewBooking.Visible = lstSETUPLOGONV1[0].UseViewBooking;
                    menuOperation.Visible = lstSETUPLOGONV1[0].UseOperation;
                    menuReport.Visible = lstSETUPLOGONV1[0].UseReport;
                    menuPostTreatment.Visible = lstSETUPLOGONV1[0].UsePostTreatment;
                    menuEnquiryPrice.Visible = lstSETUPLOGONV1[0].UseEnquiryPrice;
                    menuInjectionRoom.Visible = lstSETUPLOGONV1[0].UseInjectionRoom;
                    menuReportPostOP.Visible = lstSETUPLOGONV1[0].UseReportPostOP;
                    menuSetupGroupMethod.Visible = lstSETUPLOGONV1[0].UseSetupGroupMethod;
                    menuSetup.Visible = lstSETUPLOGONV1[0].UseSetup;
                    menuSetupDoctor.Visible = lstSETUPLOGONV1[0].UseSetupDoctor;
                    menuSetupNurse.Visible = lstSETUPLOGONV1[0].UseSetupNurse;
                    menuChangePassword.Visible = true;
                }
                //Session["USERID"] = lstSETUPLOGONVO[0].UserID;

                //if (lstSETUPLOGONVO[0].UseReserve == true)
                //{
                //    menuReserveReadOnly.Visible = false;
                //    menuReserve.Visible = true;
                //}
                //else if (lstSETUPLOGONVO[0].UseReserveReadOnly == true)
                //{
                //    menuReserveReadOnly.Visible = true;
                //    menuReserve.Visible = false;
                //}
                //else
                //{
                //    menuReserveReadOnly.Visible = false;
                //    menuReserve.Visible = false;
                //}

                //menuAppointment.Visible = lstSETUPLOGONVO[0].UseAppointment;
                //menuViewBooking.Visible = lstSETUPLOGONVO[0].UseViewBooking;
                //menuOperation.Visible = lstSETUPLOGONVO[0].UseOperation;
                //menuReport.Visible = lstSETUPLOGONVO[0].UseReport;
                //menuPostTreatment.Visible = lstSETUPLOGONVO[0].UsePostTreatment;
                //menuEnquiryPrice.Visible = lstSETUPLOGONVO[0].UseEnquiryPrice;
                //menuInjectionRoom.Visible = lstSETUPLOGONVO[0].UseInjectionRoom;
                //menuReportPostOP.Visible = lstSETUPLOGONVO[0].UseReportPostOP;
                //menuSetupGroupMethod.Visible = lstSETUPLOGONVO[0].UseSetupGroupMethod;
                //menuSetup.Visible = lstSETUPLOGONVO[0].UseSetup;
                //menuChangePassword.Visible = true;
            }
        }

    }
}