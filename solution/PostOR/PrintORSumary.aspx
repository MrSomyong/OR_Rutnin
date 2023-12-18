<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintORSumary.aspx.cs" Inherits="solution.PostOR.ORSumary" %>

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
                <div class="col-md-6">
                    <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">ORDate :</span>
                        <asp:Label ID="lblORDate" runat="server"></asp:Label></h4>
                </div>
                <div class="col-md-6">

                    <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                        <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-12 text-center">
                    <h4><span class="nav-link-text">แพทย์ :</span>
                        <asp:Label ID="lblDoctor" runat="server"></asp:Label></h4>
                </div>
            </div>
            <hr />
            <div class="form-group">
                <div class="row">
                    <div class="col-2">
                        <asp:Image runat="server" ID="imgPatient" ImageUrl="../Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
                    </div>
                    <div class="col-10">
                        <div class="row">
                            <div class="col-2">
                                <h4>ชื่อ-สกุล&nbsp;:&nbsp;</h4>
                            </div>
                            <div class="col-10">
                                <h4>
                                    <asp:Label ID="lblPatientName" Font-Bold="true" runat="server" Text=""></asp:Label></h4>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <label>HN&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblHN" Font-Bold="true" runat="server"></asp:Label>
                                <asp:HiddenField runat="server" ID="hdORID" />
                            </div>
                            <div class="col-8">
                                <label>อายุ&nbsp;:&nbsp;</label>
                                <asp:Label ID="lblAge" Font-Bold="true" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <asp:Label ID="Label1" runat="server">โรคประจำตัว&nbsp;:&nbsp;</asp:Label>
                            </div>
                            <div class="col-10">
                                <asp:Label ID="lblPatientalDiag" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <i id="chOnmedTure" runat="server" class="fa fa-check" aria-hidden="true" style="color: darkgreen"></i>
                                <i id="chOnmedFalse" runat="server" class="fa fa-times" aria-hidden="true" style="color: brown"></i>
                                <label>&nbsp;ON med&nbsp;</label>
                            </div>
                            <div class="col-6">
                                <asp:TextBox ReadOnly="true" TextMode="MultiLine" Rows="2" MaxLength="120" runat="server" ID="txtOnmed" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <hr />
            <div class="form-group">
                <div class="row">
                    <div class="col-1">
                        <asp:Label ID="Label2" runat="server">Allegic&nbsp;:&nbsp;</asp:Label>
                    </div>
                    <div class="col-11">
                        <asp:Label ID="lblPatientallegic" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="form-group mt-2">
                <div class="row">
                    <div class="col-3">
                        <asp:Label ID="Label3" runat="server">S/P : การผ่าตัดครั้งล่าสุด</asp:Label>
                    </div>
                    <div class="col-9">
                        <asp:Label ID="lblPrevOR" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body" id="Site" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblSite" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-1">
                    <div class="col-2"></div>
                    <div class="col-8">
                        <div class="card">
                            <div class="card-body" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:GridView ID="gvImplant" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-striped table-bordered table-hover table-responsive"
                                            DataKeyNames="ID,PostOperation_ID,MainCode,SubCode,SubName,Used"
                                            OnRowDataBound="gvImplant_RowDataBound"
                                            OnRowCommand="gvImplant_RowCommand"
                                            OnRowEditing="gvImplant_RowEditing">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Implant">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                        <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                        <asp:HiddenField ID="hdUsed" runat="server" Value='<%# Eval("Used") %>' />
                                                        <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" CssClass="text-center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnUsed" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Use" CommandName="select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
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
