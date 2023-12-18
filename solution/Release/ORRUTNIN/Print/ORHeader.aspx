<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ORHeader.aspx.cs" Inherits="solution.Print.ORHeader" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title></title>

    <%--<asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />--%>

    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="author" content="">
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
    <style>
        .blankrow {
            height: 50px;
            vertical-align: top;
            min-height: 50px;
            background: linear-gradient(180deg, rgba(0,0,0,0) calc(50% - 1px), rgba(192,192,192,1) calc(50%), rgba(0,0,0,0) calc(50% + 1px) );
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-1 text-center"><a class="btn btn-primary" href="#" role="button" onclick="myFunction(divORHeader)"><i class="fa fa-1x fa-print" aria-hidden="true"></i></a></div>

        </div>
        <div id="divORHeader">

               <div class="row">
                <div class="col-5 text-left">
                    <asp:Label runat="server" Font-Size="10pt" Font-Bold="False" ID="Label1" Text ="RV01 (01/07/2565)"></asp:Label>
                </div>
                <%--<div class="col-4 text-center">
                    <asp:Label runat="server" Font-Size="14pt" Font-Bold="true" ID="Label2" Text ="Test" ></asp:Label>
                </div>--%>
                <div class="col-md-3">
                   <asp:Image runat="server" ImageAlign="NotSet" ID="imglogo" ImageUrl="../Images/logo.jpg" CssClass="img-fluid" Style="width: 200px" />
                </div>
                <div class="col-4 text-right">
                    <asp:Label runat="server" Font-Size="10pt" Font-Bold="false" ID="Label3" Text ="FM-ADC-032"></asp:Label>
                </div>
            </div>

            <div class="row">
                <div class="col-12 text-center">
                    <asp:Label runat="server" Font-Bold="true" Font-Size="14pt" ID="lblORHearder"></asp:Label><br />
                </div>
            </div>
            <div class="row">
                <div class="col-12 text-center">
                    <asp:Label runat="server" Font-Size="12pt" Font-Bold="true" ID="lblORDate"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <asp:GridView ID="gvORHeader" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                        OnRowDataBound="gvORHeader_RowDataBound"  ShowFooter="true"
                        Width="100%">
                        <Columns>
                            <%--<asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>--%>
                            <asp:BoundField HeaderText="ประเภทห้อง" DataField="RoomType">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ORStatus" DataField="strORStatus">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="ช่วงเวลา" DataField="AdmitTimeType">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Patient Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvPatientName_IPPU" runat="server" Text='<%# Eval("PatientName_IPPU") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HN">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Age" DataField="ORPATIENTVO.Age">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Sex" DataField="ORPATIENTVO.Sex">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Operation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="word-break" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="OR Date" DataField="strORDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                            <asp:TemplateField HeaderText="Anesthesia<br/>Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle CssClass="word-break text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Surgeon">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSurgeon" runat="server" Text='<%# Eval("Surgeon") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Prediag" DataField="Prediag">
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                            <asp:BoundField HeaderText="Remark" DataField="Remark">
                                <ItemStyle CssClass="word-break" Width="150px" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:BoundField>
                            <%--<asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>--%>
                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle Font-Size="9pt" Font-Bold="true" />
                        <RowStyle Font-Size="9pt" />
                        <FooterStyle Height="25px" />
                    </asp:GridView>
                </div>
            </div>

            <div class="row">
                <div class="col-12 text-center">
                    <asp:Label runat="server" Font-Size="9pt" ID="lblRoomType" Font-Bold="true"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-3"></div>
                <div class="col-6">
                    <asp:GridView ID="gvRoomType" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                        Width="100%">
                        <Columns>
                            <asp:BoundField HeaderText="ชื่อห้องพัก" DataField="RoomTypeName">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="เช้า" DataField="morning">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="บ่าย" DataField="evening">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle Font-Bold="true" Font-Size="9pt" />
                        <RowStyle Font-Size="9pt" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </form>
    <script>
        function myFunction(divID) {
            //console.log("divID", divID);
            var divElements = divID;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements.innerHTML + "</body>";
            window.print();
            document.body.innerHTML = oldstr;
        }
    </script>
</body>
</html>
