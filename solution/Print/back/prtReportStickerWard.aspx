<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prtReportStickerWard.aspx.cs" Inherits="solution.Print.prtReportStickerWard" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 482px">
    <form id="form1" runat="server">
        <div>
       
            <CR:CrystalReportViewer ID="CRReportView" runat="server" AutoDataBind="true" HasCrystalLogo="False" 
                    HasSearchButton="False" Height="50px" PrintMode="ActiveX" RenderingDPI="100" 
                    ToolPanelView="None" ToolPanelWidth="0px" Width="350px" PageZoomFactor="100" ReportSourceID="CrystalReportSource1" />
            <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                <Report FileName="Z:\#ORProject\REPORT\Report\ORROOM.rpt">
                </Report>
            </CR:CrystalReportSource>
            <br />
            </div>
    </form>

</body>
</html>
