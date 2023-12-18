using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Text;

namespace solution.Reserve
{
    public partial class ImageServer : System.Web.UI.Page
    {
        protected DatabaseInfo dbInfo = GParameters.dbInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-US");
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;
            //string url = Request.QueryString["HN"];
            //Response.ContentType = "image/jpeg"; // for JPEG file
            ////Response.ContentType = "text/html";
            ////string physicalFileName = url;
            //if (!File.Exists(url))
            //{
            //    url = Server.MapPath("../Images/17241-200.png");
            //    //Response.End();
            //}
            //url = @"E:\Pitures\test.jpg";
            //FileStream fs = new FileStream(url, FileMode.Open);
            //////fs.SetLength(1000);
            //byte[] bt = new Byte[fs.Length];
            //fs.Read(bt, 0, 2000);
            //fs.Close();
            //Response.ContentType = "image/jpeg";
            //Response.BinaryWrite(bt);

            string HN = Request.QueryString["HN"];
            try
            {
                //Response.ContentType = "image/jpeg"; // for JPEG file
                HN = "6002766";
                byte [] bytes = new BLDOCUMENT_ITEM(dbInfo).SearchByHN(HN);
                //if (lstDOCUMENT_ITEMVO.Count > 0)
                //{
                    //byte[] bytes = Encoding.ASCII.GetBytes(lstDOCUMENT_ITEMVO[0].Documentdata);
                    //Response.BinaryWrite(bytes);

                    //byte[] bytes = (byte[])GetData("SELECT Data FROM tblFiles WHERE Id =" + id).Rows[0]["Data"];
                    string base64String = Convert.ToBase64String(bytes);
                    imgesp.ImageUrl = "data:image/png;base64," + base64String;
                //}

            }
            catch (Exception ex)
            {

            }
        }
    }
}