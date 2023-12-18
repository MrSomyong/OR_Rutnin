<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cancel.aspx.cs" Inherits="solution.Reserve.Cancel" %>

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
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Reserve/">รายการจองห้อง</a>
            </li>
            <li class="breadcrumb-item active">ยกเลิกการจองห้อง</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">ยกเลิกการจองห้อง</span></h4>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <hr />
        <div class="form-group" hidden>
            <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
            </div>

            <div class="row">
                <div class="col-md-2">
                    <img src="/Images/17241-200.png" alt="..." class="img-thumbnail" style="width: 70%">
                </div>
                <div class="col-md-10">
                    <div class="row">
                        <div class="col-md-1">
                            <label for="ddlHN">HN :</label>
                        </div>
                        <div class="col-md-11">
                            <asp:TextBox ID="txtHN" ReadOnly="true" runat="server" CssClass="form-control input-group-sm"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-12">
                            <label>Patient Name : </label>
                            <asp:Label ID="lblPatientName" Font-Bold="true" runat="server" Style="min-width: 220px" Text=""></asp:Label>

                            <div class="form-check form-check-inline">

                                <label>Gender : </label>
                                <asp:Label ID="lblGender" Font-Bold="true" runat="server" CssClass="badge badge-secondary" Style="min-width: 20px"></asp:Label>
                            </div>
                            <div class="form-check form-check-inline">

                                <label>Age : </label>
                                <asp:Label ID="lblAge" Font-Bold="true" runat="server" CssClass="badge badge-secondary" Style="min-width: 20px"></asp:Label>
                                <label>Year</label>

                            </div>
                            <div class="form-check form-check-inline">
                                <label>Birth Date : </label>
                                <asp:Label ID="lblBirthDateTime" Font-Bold="true" runat="server" CssClass="badge badge-secondary" Style="min-width: 100px"></asp:Label>
                            </div>
                        </div>
                    </div>


                </div>
            </div>

        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-2 text-right">เหตุผลการยกเลิก</div>
                <div class="col-10">
                    <asp:HiddenField ID="hdORID" runat="server" />
                    <asp:DropDownList ID="ddlREASON" runat="server" AutoPostBack="false" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
        <br/>
        <div class="form-group">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-10">
                    <asp:LinkButton ID="LinkButton1" class="btn btn-primary" ClientIDMode="Static" runat="Server" Text="บันทึก" CausesValidation="false" OnClick="btnCancelApp_Click" />
                </div>
            </div>
        </div>
    </div>


    <hr />

    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>
</asp:Content>
