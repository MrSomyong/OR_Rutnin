﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.Preset.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Setup Operation Preset</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-tachometer"></i><span class="nav-link-text">Setup Operation Preset</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <div style="overflow-y: hidden">
                    <asp:GridView ID="gvOROperation" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" AutoGenerateColumns="False"
                        CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvOROperation_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="MainCode" HeaderText="Code" />
                            <asp:BoundField HeaderText="Name" DataField="Name" />
                            <asp:ButtonField CommandName="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" Text="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-info btn-sm" />
                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle CssClass="table-info" />
                    </asp:GridView>
                </div>
            </div>
            <div class="col-md-8">

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div class="row">
                    <div class="col-md-7">
                        <div class="row">
                            <div class="col-2">
                                <label>Code</label>
                            </div>
                            <div class="col-3">

                                <asp:TextBox ID="txtMainCode" AutoPostBack="true" runat="server" CssClass="form-control" OnTextChanged="txtMainCode_TextChanged"></asp:TextBox>
                            </div>
                            <div class="col-7">
                                <asp:TextBox ID="txtName" runat="server" CssClass="form-control" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                            <div class="btn-group mr-2" role="group" aria-label="Second group">
                                <asp:Button ID="btnSave" class="btn btn-info pull-right" runat="server" Text="Save" OnClick="btnSave_Click" />
                            </div>

                            <div class="btn-group mr-2" role="group" aria-label="Second group">
                                <asp:Button ID="btnClear" class="btn btn-warning pull-right" runat="server" Text="Clear" OnClick="btnClear_Click" />

                            </div>

                            <div class="btn-group mr-2" role="group" aria-label="First group">
                                <asp:Button ID="btnDelete" class="btn btn-danger pull-right" runat="server" Text="Delete" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure?');" />

                            </div>

                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">

                        <div class="card card-register mx-auto mt-3" style="max-width: 100%">
                            <div class="card-header badge-warning">Operation</div>
                            <div class="card-body" id="divSub" runat="server">

                                <div class="form-group">
                                    <div class="form-row">
                                        <div class="col-md-2">
                                            <label for="txtSubName">Operation</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:HiddenField ID="hdSubCode" runat="server" />
                                            <asp:TextBox ID="txtSubName" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM1Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM1Search_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4">
                                           <%-- <asp:DropDownList ID="ddlicd" runat="server" AutoPostBack="false" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                            </asp:DropDownList>--%>
                                            <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlicd" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM1Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                        </div>
                                        <div class="col-md-2">
                                            <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                                                <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                    <asp:Button ID="btnAdd" class="btn btn-info pull-right btn-sm" runat="server" Text="Save" OnClick="btnAdd_Click" />
                                                </div>
                                                <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                    <asp:Button ID="btnClearSub" class="btn btn-warning pull-right btn-sm" runat="server" Text="Clear" OnClick="btnClearSub_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-2">
                                            <label for="txtSubName">OR Procedure Type</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlORProcedureType" runat="server" AutoPostBack="false" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-row">
                                        <div class="col-md-2">
                                            <label>ORGAN</label>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="ddlORGANMAIN" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlORGANMAIN_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:UpdatePanel ID="upORGANMAIN" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlORGANSUB" runat="server" AutoPostBack="false" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlORGANMAIN" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>--%>
                                    <asp:GridView ID="gvOROperationSub" runat="server"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover"
                                        DataKeyNames="MainCode,SubCode,SubName,ICDCM,ICDCMName"
                                        OnRowCommand="gvOROperationSub_RowCommand"
                                        OnRowDeleting="gvOROperationSub_RowDeleting" OnRowEditing="gvOROperationSub_RowEditing">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Topping">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                    <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                    <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ICDCM">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdICDCM" runat="server" Value='<%# Eval("ICDCM") %>' />
                                                    <asp:Label ID="lblICDCMName" runat="server" Text='<%# Eval("ICDCMName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Procedure Type">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdORProcedureType" runat="server" Value='<%# Eval("ORProcedureType") %>' />
                                                    <asp:Label ID="lblORProcedureTypeName" runat="server" Text='<%# Eval("ORProcedureTypeName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organ">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdOrgan" runat="server" Value='<%# Eval("ORGANMAIN") %>' />
                                                    <asp:Label ID="lblOrgan" runat="server" Text='<%# Eval("ORGANMAINName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Organ Position">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdOrganPosition" runat="server" Value='<%# Eval("ORGANSUB") %>' />
                                                    <asp:Label ID="lblOrganPosition" runat="server" Text='<%# Eval("ORGANSUBName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="Button2" CssClass="btn btn-info btn-sm" runat="server" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    <asp:Button ID="Button1" CssClass="btn btn-danger btn-sm" runat="server" Text="Delete" OnClientClick="return confirm('Do you want to delete?')" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />

                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="120px"></ItemStyle>
                                            </asp:TemplateField>

                                            <%--<asp:ButtonField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" CommandName="delete" Text="Delete" ButtonType="Button" ControlStyle-CssClass="btn btn-danger btn-sm">
                                                <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                            </asp:ButtonField>--%>
                                        </Columns>
                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                        <HeaderStyle CssClass="table-info" />
                                    </asp:GridView>
                                    <%--</ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="gvOROperationSub" />
                                        </Triggers>
                                    </asp:UpdatePanel>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <%--</ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="txtMainCode" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
            </div>
        </div>
    </div>
</asp:Content>
