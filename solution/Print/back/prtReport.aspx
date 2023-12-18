<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prtReport.aspx.cs" Inherits="solution.Print.prtReport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 482px">
    <form id="form1" runat="server">
        <div>
<%--            <rsweb:ReportViewer ID="ReportViewer1" runat="server" BackColor="" ClientIDMode="AutoID" Height="443px" HighlightBackgroundColor="" InternalBorderColor="204, 204, 204" InternalBorderStyle="Solid" InternalBorderWidth="1px" LinkActiveColor="" LinkActiveHoverColor="" LinkDisabledColor="" PrimaryButtonBackgroundColor="" PrimaryButtonForegroundColor="" PrimaryButtonHoverBackgroundColor="" PrimaryButtonHoverForegroundColor="" SecondaryButtonBackgroundColor="" SecondaryButtonForegroundColor="" SecondaryButtonHoverBackgroundColor="" SecondaryButtonHoverForegroundColor="" SplitterBackColor="" ToolbarDividerColor="" ToolbarForegroundColor="" ToolbarForegroundDisabledColor="" ToolbarHoverBackgroundColor="" ToolbarHoverForegroundColor="" ToolBarItemBorderColor="" ToolBarItemBorderStyle="Solid" ToolBarItemBorderWidth="1px" ToolBarItemHoverBackColor="" ToolBarItemPressedBorderColor="51, 102, 153" ToolBarItemPressedBorderStyle="Solid" ToolBarItemPressedBorderWidth="1px" ToolBarItemPressedHoverBackColor="153, 187, 226" Width="1003px">
                <LocalReport ReportPath="Report\Report\orroom.rdlc">
                </LocalReport>
            </rsweb:ReportViewer>--%>
            <%--<CR:CrystalReportViewer ID="crvReport" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl="" Height="815px" ToolbarImagesFolderUrl="" ToolPanelWidth="200px" Width="37px" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" ReportSourceID="CrystalReportSource1" />--%>
            
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
