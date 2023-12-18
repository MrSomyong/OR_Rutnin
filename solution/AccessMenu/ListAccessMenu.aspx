<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListAccessMenu.aspx.cs" Inherits="solution.AccessMenu.ListAccessMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/AccessMenu/ListAccessMenu">Access Menu Setup</a>
            </li>
            <li class="breadcrumb-item active">List Access Menu</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-users"></i><span class="nav-link-text">รายการกลุ่มผู้ใช้งานระบบ</span></h4>
            </div>
            <div class="col-md-6">
                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <a href="/AccessMenu/NewAccessMenu" class="btn btn-info pull-right">กลุ่มผู้ใช้งานใหม่</a>
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
            <div class="col-md-2"></div>
            <div class="col-md-8">
                <asp:GridView ID="gvSetupAccessMenu" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Orange"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                    DataKeyNames="UserID, Username, Name, strActive"
                    CssClass="table table-striped table-bordered table-hover"
                    OnRowDeleting="gvSetupAccessMenu_RowDeleting" OnRowEditing="gvSetupAccessMenu_RowEditing" OnRowCommand="gvSetupAccessMenu_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="รหัสรายการ">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdgvAccessID" runat="server" Value='<%# Bind("AccessID") %>' />
                                <asp:Label ID="lblAccessCode" runat="server" Text='<%# Bind("AccessCode") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" Width="150px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ชื่อกลุ่มการเข้าใช้งาน">
                            <ItemTemplate>
                                <asp:Label ID="lblAccessName" runat="server" Text='<%# Bind("AccessName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="การจัดการกลุ่มผู้ใช้งาน">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit" ControlStyle-CssClass="btn btn-success btn-sm"></asp:LinkButton>
                               
                                <%--<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="ChangePass" Text="Change Pass" ControlStyle-CssClass="btn btn-warning btn-sm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"></asp:LinkButton>--%>
                                
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ControlStyle-CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Do you want to delete?')"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="200px" />
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
