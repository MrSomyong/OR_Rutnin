<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Implant.aspx.cs" Inherits="solution.Setup.Implant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">Setup Implant Preset</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-tachometer"></i><span class="nav-link-text">Setup Implant Preset</span></h4>
            </div>
        </div>
        <hr />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-4">
                        <div style="overflow-y: hidden">
                            <asp:GridView ID="gvImplant" runat="server" ShowHeaderWhenEmpty="True" EmptyDataText="No records Found" AutoGenerateColumns="False"
                                CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvImplant_RowCommand">
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

                                <div class="card card-register mx-auto mt-3">
                                    <div class="card-header badge-warning">Implant</div>
                                    <div class="card-body" id="divSub" runat="server">

                                        <div class="form-group">
                                            <div class="form-row">
                                                <div class="col-md-2">
                                                    <label for="txtSubName">Implant</label>
                                                </div>
                                                <div class="col-md-6">
                                                    <asp:HiddenField ID="hdSubCode" runat="server" />
                                                    <asp:TextBox ID="txtSubName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-md-4">
                                                    <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                                                        <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                            <asp:Button ID="btnAdd" class="btn btn-info pull-right btn-sm" runat="server" Text="Add" OnClick="btnAdd_Click" />
                                                        </div>
                                                        <div class="btn-group mr-2" role="group" aria-label="Second group">
                                                            <asp:Button ID="btnClearSub" class="btn btn-warning pull-right btn-sm" runat="server" Text="Clear" OnClick="btnClearSub_Click" />

                                                        </div>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>--%>
                                            <asp:GridView ID="gvImplantSub" runat="server"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover"
                                                DataKeyNames="SubCode,SubName" OnRowCommand="gvImplantSub_RowCommand" OnRowDeleting="gvImplantSub_RowDeleting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Topping">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                            <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
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
                <asp:AsyncPostBackTrigger ControlID="txtMainCode" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
