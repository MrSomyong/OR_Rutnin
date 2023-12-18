<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.PatientsSchedule.Default"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="#">ตารางจองห้องผ่าตัด</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-tachometer"></i><span class="nav-link-text">ตารางจองห้องผ่าตัด</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-5 text-right">
                <asp:Button ID="btnPrev" runat="server" CssClass="btn btn-primary btn-md" Text="กลับ" OnClick="btnPrev_Click" />
            </div>
            <div class="col-2 text-center">
                <asp:Label ID="lblmonth" runat="server" Font-Size="1.4em" />
                <asp:HiddenField ID="hMonth" runat="server" />
            </div>
            <div class="col-5 text-left">
                <asp:Button ID="btnNext" runat="server" CssClass="btn btn-primary btn-md" Text="ถัดไป" OnClick="btnNext_Click" />
            </div>
        </div>
        <div class="row">
            <div class="col-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="gvWeeklyCalender" runat="server" CssClass="table table-bordered table-bordered pre-scrollable"
                            OnSelectedIndexChanged="OnSelectedIndexChanged">
                            <HeaderStyle BackColor="#BEDDFE" HorizontalAlign="Center" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
     <!-- Modal -->
    <div class="modal fade bd-example-modal-lg" id="PatientDetail" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">ข้อมูลการจองห้อง</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <asp:Literal ID = "ltTablePatient" runat = "server" />

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
