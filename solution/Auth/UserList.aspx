<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="solution.Auth.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Auth/UserList">User Setup</a>
            </li>
            <li class="breadcrumb-item active">User List</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-users"></i><span class="nav-link-text">รายการรายชื่อผู้ใช้งาน</span></h4>
            </div>
            <div class="col-md-6">
                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <a href="/Auth/Register" class="btn btn-info pull-right">ลงทะเบียนผู้ใช้งานใหม่</a>
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
            <div class="col-md-1"></div>
            <div class="col-md-10">
                <asp:GridView ID="gvSetupLogon" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Orange"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                    DataKeyNames="UserID, Username, Name, strActive"
                    CssClass="table table-striped table-bordered table-hover"
                    OnRowDeleting="gvSetupLogon_RowDeleting" OnRowEditing="gvSetupLogon_RowEditing" OnRowCommand="gvSetupLogon_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdgvUserID" runat="server" Value='<%# Bind("UserID") %>' />
                                <asp:Label ID="lblUsername" runat="server" Text='<%# Bind("Username") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                        </asp:TemplateField>
<%--                        <asp:TemplateField HeaderText="ประเภทผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:Label ID="lblUseTypeName" runat="server" Text='<%# Bind("UseTypeName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="กลุ่มการเข้าถึงข้อมูล">
                            <ItemTemplate>
                                <asp:Label ID="lblAccessMenu" runat="server" Text='<%# Bind("AccessName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="สถานะของผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:Label ID="lblstrAtive" runat="server" Text='<%# Bind("strActive") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" Width="150px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="การจัดการผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" ControlStyle-CssClass="btn btn-success btn-sm"></asp:LinkButton>
                               
                                <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="ChangePass" Text="Change Pass" ControlStyle-CssClass="btn btn-warning btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>
                                
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ControlStyle-CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Do you want to delete?')"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="250px" />
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="text-center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                    <HeaderStyle CssClass="table-info" />
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
