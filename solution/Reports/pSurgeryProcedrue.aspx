<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pSurgeryProcedrue.aspx.cs" Inherits="solution.Reports.pSurgeryProcedrue" %>

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
            <div class="col-1 text-center p-2"><a class="btn btn-primary" href="#" role="button" onclick="myFunction(divData)"><i class="fa fa-1x fa-print" aria-hidden="true"></i></a></div>

        </div>
        <div class="row">
            <div class="col-12">
                <div id="divData">
                    <h2>รายงานรายละเอียดการผ่าตัด</h2>
                    <asp:GridView ID="gvData" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                        CssClass="table table-bordered table-hover pre-scrollable">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor">
                                <ItemTemplate>
                                    <asp:Label ID="lblSurgeon" runat="server" Text='<%# Eval("SurgeonName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HN">
                                <ItemTemplate>
                                    <asp:Label ID="lblHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OPD/IPD">
                                <ItemTemplate>
                                    <asp:Label ID="lblOPDIPD" runat="server" Text='<%# Eval("IPDOPD") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อ-สกุล">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อายุ">
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เพศ">
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Anes Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operation Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblORDate" runat="server" Text='<%# Eval("strOperationDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operation">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperation" runat="server" Text='<%# Eval("Operation") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Procedrue">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcedrue" runat="server" Text='<%# Eval("Procedrue") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OR Operation Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblOROperationType" runat="server" Text='<%# Eval("OROperationType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสโรค">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiagnosis" runat="server" Text='<%# Eval("Diagnosis") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle CssClass="table-info" Font-Size="9pt" />
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
