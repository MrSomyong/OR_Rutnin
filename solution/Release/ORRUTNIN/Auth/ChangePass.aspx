<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChangePass.aspx.cs" Inherits="solution.Auth.ChangePass" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Auth/UserList">User Setup</a>
            </li>
            <li class="breadcrumb-item active">Change Password</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-key"></i><span class="nav-link-text">Change Password</span></h4>
            </div>
            <div class="col-md-6">

                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <%--<div class="btn-group mr-2" role="group" aria-label="Second group">
                    <a href="/Auth/Register" class="btn btn-info pull-right">Register</a>
                </div>--%>
                </div>
            </div>
        </div>
        <hr />

        <div class="card card-register mx-auto mt-5">
            <%--<div class="card-header">Register an Account</div>--%>
            <div class="card-body">
                <div class="form-group">
                    <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <asp:HiddenField runat="server" ID="hdUserID" />
                            <label for="exampleInputName">Name</label>
                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-6">
                            <label for="exampleInputPassword1">New Password</label>
                            <asp:TextBox ID="txtpassword" CssClass="form-control" runat="server" MaxLength="8" TextMode="Password" placeholder="New Password" pattern=".{0}|.{4,8}" required title="Either 0 OR (4 to 8 chars)"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row text-center p-2">
                        <div class="col-md-12">
                            <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Change Password" OnClick="btnSave_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
