<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ORRoomType.aspx.cs" Inherits="solution.Setup.ORRoomType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        .pad-l-0 {
            padding-left: 0px;
        }

        .pad-r-0 {
            padding-left: 0px;
        }

        input, select, textarea {
            max-width: 100%;
        }

        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 200px;
            height: 200px;
            animation: spin 2s linear infinite;
            position: sticky;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        /*.modal-body {
            padding-left: 30%;
        }*/
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Setup/ORRoomType/">Room Type</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-cog"></i><span class="nav-link-text">Room Type</span></h4>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                <ContentTemplate>
                    <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
                    </div>
                    <div class="form-group">
                        <div class="row">
                            <div class="col-md-3 text-right">
                                <label class="pull-right">CODE :</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtCodeType" runat="server" CssClass="form-control form-control-sm">                                            
                                </asp:TextBox>
                                <asp:HiddenField ID="hdID" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3 text-right">
                                <label class="pull-right">Name :</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control form-control-sm">                                            
                                </asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="row">
                            <div class="col-md-3 text-right">
                                <label class="pull-right">Procedure Code :</label>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtProcedureCode" runat="server" CssClass="form-control form-control-sm">                                            
                                </asp:TextBox>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-3"></div>
                            <div class="col-1">
                                <div class="btn-group mt-2" role="group" aria-label="Second group">
                                    <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAdd" OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="col-1">
                                <div class="btn-group mt-2" role="group" aria-label="Second group">
                                    <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClear" OnClick="btnClear_Click" />
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="row">
                            <div class="col-2"></div>
                            <div class="col-8">
                                <asp:GridView ID="gvSetup" runat="server"
                                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                    AutoGenerateColumns="False"
                                    CssClass="table table-bordered table-hover table-responsive"
                                    DataKeyNames="ID,CodeType,Name"
                                    OnRowEditing="gvSetup_RowEditing"
                                    OnRowDeleting="gvSetup_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CodeType" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCodeType" runat="server" Text='<%# Eval("CodeType") %>'></asp:Label>
                                                <asp:HiddenField ID="hdID" runat ="server" Value='<%# Eval("ID") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" Font-Size="Small" />
                                            <HeaderStyle Font-Size="Small" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" Font-Size="Small" />
                                            <HeaderStyle Font-Size="Small" />
                                        </asp:TemplateField>
<%--                                        <asp:TemplateField HeaderText="ProcedureCode" HeaderStyle-CssClass="text-center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcedureCode" runat="server" Text='<%# Eval("ProcedureCode") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" Font-Size="Small" />
                                            <HeaderStyle Font-Size="Small" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-success" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <hr />
        </div>

    </div>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            $('.nav-tabs li:eq(0) a').tab('show');

        });
    </script>
</asp:Content>
