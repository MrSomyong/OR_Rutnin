<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditAccessMenu.aspx.cs" Inherits="solution.AccessMenu.EditAccessMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/AccessMenu/ListAccessMenu">Access Menu Setup</a>
            </li>
            <li class="breadcrumb-item active">Edit Access Menu Setup</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-user"></i><span class="nav-link-text">แก้ไขรายการกลุ่มผู้ใช้งานระบบ</span></h4>
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

        <div class="card card-body mx-auto mt-2">
            <%--<div class="card-header">Register an Account</div>--%>
            <div class="card-header">
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
                            <asp:HiddenField runat="server" ID="hdAccessID" />
                            <label for="exampleInputName">Access Code</label>
                            <asp:TextBox ID="txtAccessCode" CssClass="form-control" runat="server" MaxLength="49" placeholder="Access Code" pattern=".{3,}" required title="4 characters minimum"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="form-row">
                        <div class="col-md-4">
                            <label for="exampleInputName">Access Name</label>
                            <asp:TextBox ID="txtAccessName" CssClass="form-control" runat="server" placeholder="Access Name" required></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            
            <div class="form-group">
                <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                        </label>
                        </div>
            </div>

            <div class="form-group">
                <div class="col-md-12">
                    <div class="form-horizontal alert alert-info" style="padding:15px;">
                        <div class="control-group row-fluid form-inline" >
                            <div class="row">
                                <label class="control-label pull-right font-weight-bold" >&nbsp;รายการจองห้อง : </label>
                            <div class="col-md-6">
                                <asp:DropDownList ID="ddlReserveOR" runat="server" AutoPostBack="false" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                    <asp:ListItem Value="1" Selected ="True" Text="Write"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Read Only"></asp:ListItem>
                                </asp:DropDownList>
                        </div>
                            </div>
                        </div>
                    </div>
                 </div>
            </div>

            <div class="form-group">
                <div class="col-md-12">
                    <div class="form-horizontal alert alert-info" style="padding:15px;">
                        <div class="control-group row-fluid form-inline" >
                            <div class="row">
                                <label class="control-label pull-right font-weight-bold" >&nbsp;รายการอื่น ๆ : </label>
                            </div>
                        </div>
                        <div class="control-group row-fluid form-inline" >
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbAppointment" runat="server" Text="&nbsp;นัดหมาย" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbViewBooking" runat="server" Text="&nbsp;View Booking" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbPostOR" runat="server" Text="&nbsp;Post OR" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbReport01" runat="server" Text="&nbsp;รายงานการส่งผ่าตัด" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbPostTreatment" runat="server" Text="&nbsp;Post Treatment" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbEnquiryPrice" runat="server" Text="&nbsp;Enquiry Price" />
                                </label>
                            </div>
                        </div>
                        <div class="control-group row-fluid form-inline" >
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbInjectionRoom" runat="server" Text="&nbsp;Injection Room" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbReport02" runat="server" Text="&nbsp;Report Post OP" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbSetupgroupMethod" runat="server" Text="&nbsp;Setup Group Method" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbSetupAll" runat="server" Text="&nbsp;Setup All" />
                                </label>
                            </div>    
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbSetupDoctor" runat="server" Text="&nbsp;Setup Doctor" />
                                </label>
                            </div>
                            <div class="col-md-2">
                                <label class="form-check-label pull-left">
                                            <asp:CheckBox ID="chbSetupNurse" runat="server" Text="&nbsp;Setup Nurse" />
                                </label>
                            </div>
                        </div>
                    </div>
                    </div>
                 </div>
            </div>

            <div class="form-group">
                <div class="form-row text-center p-2">
                    <div class="col-md-12">
                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="Save" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    <%--</div>--%>
</asp:Content>
