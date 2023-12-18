<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRoomType.aspx.cs" Inherits="solution.Setup.UserRoomType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Setup User & RoomType</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-tachometer"></i><span class="nav-link-text">Setup User & RoomType</span></h4>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
                </div>
                <div class="row">
                    <div class="col-md-4">
                        <div style="overflow-y: hidden">
                            <asp:GridView ID="gvSetup" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvSetup_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="UserID">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdUserID" runat="server" Value='<%# Bind("UserID") %>' />
                                            <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" Width="180px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:ButtonField CommandName="Select" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60px" Text="Edit" ButtonType="Button" ControlStyle-CssClass="btn btn-info btn-sm" />
                                </Columns>
                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                <HeaderStyle CssClass="table-info" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="col-md-8">
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                        <div class="row">
                            <div class="col-12">

                                <div class="card card-register mx-auto mt-3">
                                    <div class="card-header badge-warning">
                                        <asp:Label Font-Bold="true" ID="lblUserName" runat="server"></asp:Label>
                                        <asp:HiddenField ID="hdUserID" runat="server" />
                                    </div>
                                    <div class="card-body" id="divSub" runat="server">

                                        <div class="form-group">
                                            <div class="form-row">
                                                <div class="col-md-2">
                                                    <label>Room Type</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="form-control form-control-sm">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                                                        <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                            <asp:Button ID="btnAdd" class="btn btn-info pull-right btn-sm" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                                        </div>
                                                        <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                            <asp:Button ID="btnClear" class="btn btn-warning pull-right btn-sm" runat="server" Text="Clear" OnClick="btnClear_Click" />

                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:GridView ID="gvRoomType" runat="server"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="UserID,RoomType,RoomTypeName" OnRowCommand="gvRoomType_RowCommand" OnRowDeleting="gvRoomType_RowDeleting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="RoomType">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdRoomType" runat="server" Value='<%# Eval("RoomType") %>' />
                                                            <asp:Label ID="lblRoomTypeName" runat="server" Text='<%# Eval("RoomTypeName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" Text="Delete" OnClientClick="return confirm('Do you want to delete?')" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                        <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
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
            </ContentTemplate>
            <Triggers>
                <%--<asp:AsyncPostBackTrigger ControlID="txtMainCode" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />--%>
            </Triggers>
        </asp:UpdatePanel>
</asp:Content>
