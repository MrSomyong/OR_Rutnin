﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="prtReportStickerOR.aspx.cs" Inherits="solution.Print.prtReportStickerOR" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title>RUTNIN OPERATION ROOM</title>
    <style type="text/css">
        #btnPrint {
            width: 123px;
        }
    </style>
    <!-- Bootstrap core CSS-->
    <link href="/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom fonts for this template-->
    <link href="/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- Custom styles for this template-->
    <link href="/css/sb-admin.css" rel="stylesheet">

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <%--<link href="/css/datepicker.css" rel="stylesheet" />--%>
    <link href="/css/bootstrap-datepicker.css" rel="stylesheet" />

    <link href="/Content/Site.css" rel="stylesheet" />

    <script src="/vendor/jquery/jquery.min.js"></script>
    <style type="text/css">
        body {
            padding-top: 10px;
        }
    </style>
</head>
<body style="height: 482px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-3">
                <asp:UpdatePanel runat="server" ID="upreport">
                    <ContentTemplate>
                        <div class="form-group p-2">
                            <asp:DropDownList ID="cboCurrentPrinters" runat="server" CssClass="form-control p-3" AutoPostBack="True">
                            </asp:DropDownList>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-2">
                <div class="form-group p-2">
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-md btn-primary mousecursor" OnClick="pntPrint_Click" Text="Print" />
                </div>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <CR:CrystalReportViewer ID="CRReportView" runat="server" AutoDataBind="true" HasCrystalLogo="False"
                    HasSearchButton="False" Height="50px" PrintMode="ActiveX" RenderingDPI="100"
                    ToolPanelView="None" ToolPanelWidth="0px" Width="350px" PageZoomFactor="100" ReportSourceID="CrystalReportSource1" HasDrilldownTabs="False" HasDrillUpButton="False" HasExportButton="False" HasToggleGroupTreeButton="False" HasToggleParameterPanelButton="False" />
                <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                    <Report FileName="Z:\#ORProject\REPORT\Report\ORROOM.rpt">
                    </Report>
                </CR:CrystalReportSource>
            </div>
        </div>
    </form>

</body>
</html>
