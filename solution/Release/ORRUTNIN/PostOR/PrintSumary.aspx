<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintSumary.aspx.cs" Inherits="solution.PostOR.PrintSumary" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <title></title>
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
        <div class="row mb-3">

            <div class="col-12 text-center">
                <div class="btn-toolbar pull-right mr-5" role="toolbar" aria-label="Toolbar with button groups">
                    <a class="btn btn-primary" href="#" role="button" onclick="myFunction(divORHeader)"><i class="fa fa-2x fa-print" aria-hidden="true"></i></a>
                </div>
            </div>
        </div>
        <div id="divORHeader" style="font-size: 1.5rem;">

            <div class="row">
                <div class="col-md-6">
                    <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">ORDate :</span>
                        <asp:Label ID="lblORDate" runat="server"></asp:Label></h4>
                </div>
            </div>
            <div class="row">
                <%--<div class="col-12 text-center">
                    <h4><span class="nav-link-text">แพทย์ :</span>
                        <asp:Label ID="lblDoctor" runat="server" Font-Size=40></asp:Label></h4>
                </div>--%>
                <%--<div class="col-8 text-center">
            </div>--%>
            <div class="col-12 text-right">
                <%--<h4><span class="nav-link-text">แพทย์ :</span></h4>--%>
                    <h4><asp:Label ID="lblDoctor" runat="server" Font-Size=40></asp:Label></h4>
            </div>
            </div>
            <hr />
            <div class="form-group">
                <div class="row">
                    <div class="col-2">
                        <asp:Image runat="server" ID="imgPatient" ImageUrl="../Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
                    </div>
                    <div class="col-10">
                        <div class="row" style="font-size: 1.5rem;">
                        <div class="col-12 text-left">
                        <h4><span class="nav-link-text">ชื่อ-สกุล :</span>
                            <asp:Label ID="lblPatientName" runat="server" Font-Size=50></asp:Label></h4>
                    </div>
                    </div>
                        <div class="row">
                            <div class="col-3"  style="font-size: 1.5rem;">
                                <label>HN&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblHN" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                                <asp:HiddenField runat="server" ID="hdORID" />
                            </div>
                            <div class="col-5"  style="font-size: 1.5rem;">
                                <label>อายุ&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblAge" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                            </div>
                            <div class="col-4" style="font-size: 1.5rem;">
                                <label>Nationality&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblNationality" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                            </div>
                        </div>
                        <div class="row" id="divANVN" runat="server" visible="false" style="font-size: 1.5rem;">
                            <div class="col-12" id="divAN" runat="server" visible="false">
                                <label>AN&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblAN" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                            </div>
                            <div class="col-12" id="divVN" runat="server" visible="false"  style="font-size: 1.5rem;">
                                <label>VN&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblVN" Font-Bold="true" runat="server" Font-Size =25></asp:Label>&nbsp;&nbsp;&nbsp;
                                <label>Visit Date&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblVN_VisitDate" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </div>
            <hr />
            <div class="row">
                <div class="col-4" style="border-right: 1px solid #e9ecef; font-size: 1.5rem;">
                    <div class="row">
                        <div class="col-12">
                            <label>Underlying disease :&nbsp;</label>
                            <asp:Label ID="lblPatientalDiag" runat="server"></asp:Label>
                            <asp:Label ID="lblPatientalDiagDesc" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <i id="chOnmedTure" runat="server" class="fa fa-check" aria-hidden="true" style="color: darkgreen"></i>
                            <i id="chOnmedFalse" runat="server" class="fa fa-times" aria-hidden="true" style="color: brown"></i>
                            <asp:Label runat="server" ID="lblCheckOnmed">&nbsp;ON med&nbsp;</asp:Label>
                            <asp:Label runat="server" CssClass="word-break" ID="lblOnmed"></asp:Label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:GridView ID="gvHeadWarningMore" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                Width="90%">
                                <Columns>
                                    <asp:BoundField HeaderText="Warning" DataField="Warning" HtmlEncode="false">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                </Columns>
                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label2" runat="server">Allegic&nbsp;:&nbsp;</asp:Label>
                                <asp:Label ID="lblPatientallegic" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="Label3" runat="server">S/P : </asp:Label>
                                    <asp:Label ID="lblPrevOR" runat="server" Text="ไม่มี"></asp:Label>
                                    <div class="row col-12">
                                        <asp:Label ID="lblPrevORImplant" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-8" style="font-size: 1.5rem;">
                    <div class="form-group mt-2">
                        <div class="row mt-1" runat="server" id="div1">
                            <div class="col-12">
                        <div class="card">
                            <div class="card-body" id="Div2" runat="server">
                                <div class="row">
                                    <div class="col-6" style="border-right: 2px solid silver;">
                                        <asp:Label ID="Label1" runat="server" Text="Dx."></asp:Label>                                        
                                        <asp:Label ID="lblPrediag" runat="server" Font-Bold="true" Font-Size =20></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="Label4" runat="server" Text="Under :"></asp:Label>
                                        <asp:Label ID="lblAnesthesiaType" runat="server"  Font-Bold="true" Font-Size =20></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                        </div>
                        <div class="row mt-1" runat="server" id="divSite">
                            <div class="col-12">
                                <div class="card">
                                    <div class="card-body" id="Site" runat="server">
                                        <div class="row">
                                            <div class="col-12">
                                                <asp:Label ID="lblSite" runat="server" Font-Size=50></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row mt-1" id="divImplant" runat="server">
                            <div class="col-12">
                                <asp:GridView ID="gvImplant" runat="server"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                    AutoGenerateColumns="False" Width="90%"
                                    OnRowDataBound="gvImplant_RowDataBound"
                                    DataKeyNames="ID,PostOperation_ID,MainCode,SubCode,SubName,Used">
                                    <Columns>
                                        <%--<asp:TemplateField HeaderText="Implant" HeaderStyle-Width="70%">--%>
                                            <asp:TemplateField HeaderText="Implant">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                <asp:HiddenField ID="hdUsed" runat="server" Value='<%# Eval("Used") %>' />
                                                <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" Font-Size="Large" />
                                            <HeaderStyle Font-Size="Large" CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="word-break" Font-Size="Large" />
                                                            <HeaderStyle Font-Size="Large" CssClass="text-center" />
                                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lblUsed" runat="server" Text='<%# Eval("Used") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" HorizontalAlign="Center" Width="100px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-success" />
                                </asp:GridView>
                            </div>
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
