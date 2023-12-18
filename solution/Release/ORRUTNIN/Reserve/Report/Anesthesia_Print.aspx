<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Anesthesia_Print.aspx.cs" Inherits="solution.Print.Anesthesia_Print" %>

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
    <style>
        body {
            padding-top: 0px;
            padding-bottom: 0px;
            font-size: 13px;
        }

        .row {
            margin-bottom: 5px;
        }
    </style>
    <script src="/vendor/jquery/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-1 text-center p-2"><a class="btn btn-primary" href="#" role="button" onclick="myFunction(divORHeader)"><i class="fa fa-1x fa-print" aria-hidden="true"></i></a></div>

        </div>
        <div class="row">
            <div class="col-12">
                <div id="divORHeader">
                    <div class="row">
                    <div class="col-md-4">
                        <asp:GridView ID="gvAnesthesia" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia_RowCreated" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="Anesthesia Type" DataField="AnesthesiaTypeName">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                    <div class="col-md-4">
                        <asp:GridView ID="gvAnesthesia1" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia1_RowCreated" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="(+)Anesthesia Type" DataField="AnesthesiaTypeName">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                    <div class="col-md-4">
                        <asp:GridView ID="gvAnesthesia2" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia2_RowCreated" ShowFooter="True">
                            <Columns>
                                <asp:BoundField HeaderText="(+-)Anesthesia Type" DataField="AnesthesiaTypeName">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </div>
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
