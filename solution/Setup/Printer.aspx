<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Printer.aspx.cs" Inherits="solution.Setup.Printer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Setup/Printer">Printer Setup</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-user-circle"></i><span class="nav-link-text">Printer Setup</span></h4>
            </div>
            <div class="col-md-6">
                <%--<div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <a href="/Doctor/AddEdit" class="btn btn-info pull-right">Register</a>
                    </div>
                </div>--%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2">
                                <label class="pull-right">Printer Name : </label>
                                <asp:HiddenField ID="hdEvent" runat="server" />
                                <asp:HiddenField ID="hdID" runat="server" />
                            </div>
                            <div class="col-10">
                                <asp:TextBox runat="server" ID="txtPrinterName" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <label class="pull-right">Path : </label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox runat="server" ID="txtPrinterPath" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">                                
                            </div>
                            <div class="col-10 text-left">
                                <asp:Button CssClass="btn btn-sm btn-success mousecursor" ID="btnAdd" Width="150px" Text="เพิ่ม" runat="server" OnClick="btnAdd_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvPrinter" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Orange"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                    CssClass="table table-striped table-bordered table-hover"
                    OnRowDataBound="gvPrinter_RowDataBound" 
                    OnRowEditing="gvPrinter_RowEditing"
                    OnRowDeleting="gvPrinter_RowDeleting">
                    <Columns>                        
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblgvName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="text-center" />
                            <HeaderStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Path">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Bind("ID") %>' />
                                <asp:Label ID="lblgvPath" runat="server" Text='<%# Bind("Path") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="word-break" />
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ControlStyle-CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Do you want to delete?')"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle CssClass="text-center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                    <HeaderStyle CssClass="table-info" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            


        });

    </script>
</asp:Content>
