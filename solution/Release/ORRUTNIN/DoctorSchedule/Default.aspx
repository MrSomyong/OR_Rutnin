<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.DoctorSchedule.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">ตารางแพทย์เข้าเวรผ่าตัด</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-tachometer"></i><span class="nav-link-text">ตารางแพทย์เข้าเวรผ่าตัด</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <asp:Literal ID = "ltTable" runat = "server" />
            </div>
        </div>
    </div>
</asp:Content>
