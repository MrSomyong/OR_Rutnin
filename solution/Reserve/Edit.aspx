<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="solution.Reserve.Edit" %>

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
            <li class="breadcrumb-item active">แก้ไขจองห้อง</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">บันทึกการจองห้อง</span></h4>
            </div>
            <div class="col-md-6">

                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <asp:Button ID="btnSave" class="btn btn-info pull-right" runat="server" Text="Save" OnClick="Save_Click" />
                    </div>

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <asp:Button ID="btnClear" class="btn btn-secondary pull-right" runat="server" Text="Refresh" OnClick="Clear_Click" />

                    </div>
                    <div class="btn-group mr-2" role="group" aria-label="First group">
                        <asp:Button ID="btnCancel" class="btn btn-warning pull-right" Style="cursor: pointer;" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                    </div>
                    <div class="btn-group mr-2" role="group" aria-label="First group">
                        <asp:Button ID="btnViewLog" class="btn btn-secondary pull-right" Style="cursor: pointer;" runat="server" Text="View Log" data-toggle="modal" data-target="#modalViewLog" />
                        <!-- Modal -->
                        <div class="modal fade bd-example-modal-lg" id="modalViewLog" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                            <div class="modal-dialog modal-lg" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">View Log</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvLogOR" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                                                    <Columns>
                                                        <asp:BoundField HeaderText="Date" DataField="UpdateDate" DataFormatString="{0:d MMMM yyyy}" HtmlEncode="false">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Time" DataField="UpdateDate" DataFormatString="{0:HH:mm}" HtmlEncode="false">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="รายละเอียด" DataField="Detail" HtmlEncode="false">
                                                            <ItemStyle CssClass="word-break" />
                                                            <HeaderStyle CssClass="text-center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="โดย" DataField="UpdateName">
                                                            <ItemStyle CssClass="word-break" />
                                                            <HeaderStyle CssClass="text-center" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                    <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnViewLog" EventName="Click" />
                                            </Triggers>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                        </div>
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

            <div class="row">
                <div class="col-md-2">
                    <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=Session["PicPatient"]%>" width="100%">--%>                    
                    <asp:Image runat="server" ImageUrl="~/Images/17241-200.png" ID="imgPatient" CssClass="img-thumbnail" Style="width: 70%" />
                </div>
                <div class="col-md-10">
                    <div class="row">
                        <div class="col-md-1">
                            <label for="ddlHN">HN :</label>
                        </div>
                        <div class="col-md-11">
                            <asp:TextBox ID="txtHN" ReadOnly="true" runat="server" CssClass="form-control input-group-sm"></asp:TextBox>
                            <asp:HiddenField runat="server" ID="hdORID" />
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
                    <div class="row">
                        <div class="col-12">
                            <div class="row">
                                <div class="col-4">
                                    <div class="form-inline">
                                        <label>ID Card :&nbsp;</label>
                                        <asp:Label ID="lblIDCARD" runat="server" Font-Bold="true" CssClass="badge badge-secondary" Style="min-width: 130px"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-4">
                                    <div class="form-inline">
                                        <label>Nationality :&nbsp;</label>
                                        <asp:Label ID="lblNationality" runat="server" Font-Bold="true" CssClass="badge badge-secondary" Style="min-width: 80px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12" style="padding-left: 32px">
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="chbPatientInfection" runat="server" CssClass="form-check-input" Text="&nbsp;Infection" />
                                        </label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="chbPatientType1" runat="server" Text="&nbsp;Universal Precaution 1(**)" />
                                        </label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="chbPatientType2" runat="server" Text="&nbsp;Universal Precaution 2(***)" />
                                        </label>
                                    </div>
                                    <div class="form-check form-check-inline">
                                        <label class="form-check-label">
                                            <asp:CheckBox ID="chbPatientUP" runat="server" Text="&nbsp;Up" />
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="lblPatientallegic" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPatientallegicMore" class="btn btn-sm btn-outline-success" Style="cursor: pointer; font-size: .3rem;" runat="server" Text="More.." data-toggle="modal" data-target="#modalPatientallegicMore" />
                                    <!-- Modal -->
                                    <div class="modal fade bd-example-modal-md" id="modalPatientallegicMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                                        <div class="modal-dialog modal-md" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Patient Allegic</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvPatientallegic" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="แพ้ยา" DataField="allegicname" HtmlEncode="false">
                                                                        <ItemStyle CssClass="word-break" />
                                                                        <HeaderStyle CssClass="text-center" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField HeaderText="อาการ" DataField="Reaction" HtmlEncode="false">
                                                                        <ItemStyle CssClass="word-break" />
                                                                        <HeaderStyle CssClass="text-center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnPatientallegicMore" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12">
                                    <asp:Label ID="lblPatientalDiag" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnPatientalDiagMore" class="btn btn-sm btn-outline-success" Style="cursor: pointer; font-size: .3rem;" runat="server" Text="More.." data-toggle="modal" data-target="#modalPatientalDiagMore" />
                                    <!-- Modal -->
                                    <div class="modal fade bd-example-modal-md" id="modalPatientalDiagMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                                        <div class="modal-dialog modal-md" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h5 class="modal-title">Patient Diag</h5>
                                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                        <span aria-hidden="true">&times;</span>
                                                    </button>
                                                </div>
                                                <div class="modal-body">
                                                    <asp:UpdatePanel ID="UpdatePanelDiag" runat="server">
                                                        <ContentTemplate>
                                                            <asp:GridView ID="gvPatientalDiag" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                                                                <Columns>
                                                                    <asp:BoundField HeaderText="โรคประจำตัว" DataField="diagname" HtmlEncode="false">
                                                                        <ItemStyle CssClass="word-break" />
                                                                        <HeaderStyle CssClass="text-center" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                                            </asp:GridView>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="btnPatientalDiagMore" EventName="Click" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnORHEADER" runat="server" CssClass="bd-example" Enabled="false">

        <div class="panel">
            <ul class="nav nav-tabs">
                <li class="nav-item ">
                    <a class="nav-link text-center" href="#detail" style="width: 150px">Detail</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#operation" style="width: 150px">Operation</a>
                </li>
            </ul>
            <div class="tab-content">
                <div id="detail" class="tab-pane fade in active">
                    <br />
                    <div class="row">
                        <div class="col-7">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">OR Date : </label>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                            <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                                            <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>
                                        </div>
                                        <asp:HiddenField ID="hdDate" runat="server" />
                                        <asp:HiddenField ID="hdDateEn" runat="server" />
                                    </div>
                                    <div class="col-md-2 pad-l-0">
                                        <label class="pull-right">OR Case : </label>
                                    </div>
                                    <div class="col-md-2 pad-l-0">
                                        <asp:DropDownList ID="ddlORCASE" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                            <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                            <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                            <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                            <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                            <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                            <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                            <asp:ListItem Value="32" Text="32"></asp:ListItem>
                                            <asp:ListItem Value="33" Text="33"></asp:ListItem>
                                            <asp:ListItem Value="34" Text="34"></asp:ListItem>
                                            <asp:ListItem Value="35" Text="35"></asp:ListItem>
                                            <asp:ListItem Value="36" Text="36"></asp:ListItem>
                                            <asp:ListItem Value="37" Text="37"></asp:ListItem>
                                            <asp:ListItem Value="38" Text="38"></asp:ListItem>
                                            <asp:ListItem Value="39" Text="39"></asp:ListItem>
                                            <asp:ListItem Value="40" Text="40"></asp:ListItem>
                                            <asp:ListItem Value="41" Text="41"></asp:ListItem>
                                            <asp:ListItem Value="42" Text="42"></asp:ListItem>
                                            <asp:ListItem Value="43" Text="43"></asp:ListItem>
                                            <asp:ListItem Value="44" Text="44"></asp:ListItem>
                                            <asp:ListItem Value="45" Text="45"></asp:ListItem>
                                            <asp:ListItem Value="46" Text="46"></asp:ListItem>
                                            <asp:ListItem Value="47" Text="47"></asp:ListItem>
                                            <asp:ListItem Value="48" Text="48"></asp:ListItem>
                                            <asp:ListItem Value="49" Text="49"></asp:ListItem>
                                            <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <asp:Label CssClass="pull-right" ID="lblORTimeH" runat="server">OR Time : </asp:Label>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="controls form-inline">
                                            <div id="divORTime" runat="server">
                                                <asp:DropDownList ID="ddlORTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0">
                                                </asp:DropDownList>&nbsp;:&nbsp;
                                            <asp:DropDownList ID="ddlORTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0">
                                            </asp:DropDownList>
                                            </div>
                                            &nbsp;&nbsp;
                                            <label class="form-check-label">
                                                <asp:CheckBox runat="server" ID="chbORTimeFollow" Text="&nbsp;T F" />
                                            </label>
                                            &nbsp;&nbsp;
                                            <label class="form-check-label">
                                                <asp:CheckBox runat="server" ID="chbORStatCase" Text="&nbsp;Stat Case" />
                                            </label>
                                            <label>
                                                &nbsp;&nbsp;&nbsp;Arrival&nbsp;&nbsp;
                                            </label>
                                            <asp:DropDownList ID="ddlArrivalTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0">
                                            </asp:DropDownList>&nbsp;:&nbsp;
                                            <asp:DropDownList ID="ddlArrivalTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Specific Type : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlORSpecificType" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <%--<asp:ListItem Value="0" Selected="True" Text="None Specific"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="Specific"></asp:ListItem>
                                            <asp:ListItem Value="3" Text="Refer"></asp:ListItem>--%>
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Status : </label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:DropDownList ID="ddlORStatus" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-4 pad-l-0">
                                        <asp:DropDownList ID="ddlAdmitTimeType" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div id="divroomtype" class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Room Type : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlRoomType" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">OR Room : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlORRoom" runat="server" AutoPostBack="false" CssClass="form-control" OnSelectedIndexChanged="ddlORRoom_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Anesthesia Type : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlAnesthesiaType1" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Anesthesia Type : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <div class="controls form-inline">
                                            <asp:DropDownList ID="ddlAnesthesiaSign" runat="server" AutoPostBack="false" CssClass="form-control">
                                                <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="+"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="+-"></asp:ListItem>
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;
                                            <asp:DropDownList ID="ddlAnesthesiaType2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Remarks : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox Rows="3" MaxLength="500" ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Pre diag : </label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox Rows="3" MaxLength="500" ID="txtPrediag" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-5">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Surgeon (1) : </label>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList ID="ddlSurgeon1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon1" GroupName="rbSurgeon" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Surgeon (2) : </label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlSurgeon2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon2" GroupName="rbSurgeon" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Surgeon (3) : </label>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList ID="ddlSurgeon3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon3" GroupName="rbSurgeon" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Doctor(1) : </label>
                                    </div>
                                    <div class="col-6">
                                        <asp:DropDownList ID="ddlAnesthesiaDoctor1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Doctor(2) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaDoctor2" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Doctor(3) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaDoctor3" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(1) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse1" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(2) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse2" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(3) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse3" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            

                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 pr-1">
                                        <label class="pull-right">Request By</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchRequestByUser" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchRequestByUser_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-6 pl-1 pl-1">
                                        <asp:UpdatePanel ID="upSearchRequestByUser" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlRequestByUser" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchRequestByUser" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                            
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 pr-1">
                                        <label class="pull-right">Suggest By</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchSuggestByUser" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchSuggestByUser_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-6 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSuggestByUser" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchSuggestByUser" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
                <div id="operation" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-7">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="control-group row-fluid form-inline alert alert-primary">
                                                        <label class="pull-right">Side : &nbsp;&nbsp;</label>
                                                        <asp:DropDownList ID="ddlORSide" runat="server" AutoPostBack="false" CssClass="form-control d-block">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-5">
                                                <div style="overflow-y: scroll; height: 350px;">
                                                    <asp:GridView ID="gvOROperationMain" runat="server" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No records Found" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover"
                                                        DataKeyNames="MainCode, Name"
                                                        OnRowDataBound="gvOROperationMain_RowDataBound"
                                                        OnRowEditing="gvOROperationMain_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Type Of Operation">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Bind("MainCode") %>' />
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="word-break" />
                                                                <HeaderStyle Font-Size="Small" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-7">
                                                <div style="overflow-y: scroll; height: 350px;">
                                                    <asp:HiddenField ID="hdMainCode" runat="server" />
                                                    <asp:HiddenField ID="hdMainName" runat="server" />
                                                    <asp:GridView ID="gvOROperationSub" runat="server"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                        ShowFooter="true"
                                                        AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover table-responsive"
                                                        DataKeyNames="MainCode,Name,SubCode,SubName"
                                                        OnRowCommand="gvOROperationSub_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Procedure">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="hdgvddlSubMask" runat="server" AutoPostBack="false" CssClass="form-control">
                                                                        <asp:ListItem Value="0" Text=""></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="+"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="+-"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="/"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="90px" Font-Size="Small"></ItemStyle>
                                                                <HeaderStyle Font-Size="Small" />
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lblgvProcedure" runat="server" Text="Procedure"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle Font-Size="Small" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                                    <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                                    <asp:HiddenField ID="hdgvName" runat="server" Value='<%# Eval("Name") %>' />
                                                                    <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtgvSubNameFt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <FooterStyle Font-Size="Small" />
                                                                <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btngvSubNew" runat="server" Width="45px" CssClass="btn btn-success btn-sm" Text="Add" CommandName="Add" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="btngvSubNewFt" runat="server" Width="45px" CssClass="btn btn-success btn-sm" Text="Add" CommandName="Add" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-5">
                                        <div style="overflow-y: scroll; height: 420px;">
                                            <asp:GridView ID="gvOROperation" runat="server"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover table-responsive"
                                                DataKeyNames="ID,MainCode,Name,SubCode,SubName,Side,strSide,SubMark,strSubmark"
                                                OnRowCommand="gvOROperation_OnRowCommand" 
                                                OnRowDeleting="gvOROperation_RowDeleting">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Type Of Operation">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                            <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                            <asp:HiddenField ID="hdgvSide" runat="server" Value='<%# Eval("Side") %>' />
                                                            <asp:HiddenField ID="hdgvSubMark" runat="server" Value='<%# Eval("SubMark") %>' />
                                                            <asp:Label ID="lblgvName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Procedure">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvstrSubMark" runat="server" Text='<%# Eval("strSubMark") %>'></asp:Label>
                                                            <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Side">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button1" runat="server" Text="ลบ" CssClass="btn btn-danger btn-sm mousecursor" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <asp:Button ID="btnImplant" runat="server" CssClass="btn btn-primary btn-sm mousecursor" Text="Implant" CommandName="implant" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            
                                                        </ItemTemplate>
                        
                                                        <ItemStyle CssClass="text-left" Width="120px"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                <HeaderStyle CssClass="table-success" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row" style="margin-left:0px">
                                                    <asp:Label ID="lblProcedureRE" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left:0px">
                                                    <asp:Label ID="lblProcedureLE" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left:0px">
                                                    <asp:Label ID="lblProcedureBE" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left: 0px;">
                                                    <asp:Label ID="lblProcedureUnknown" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left: 0px;">
                                                    <asp:Label ID="lblProcedureNone" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </asp:Panel>

    <hr />

    <div class="modal fade bd-example-modal-md" id="modalImplant" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" style="max-width: 1150px;" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Implant</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:HiddenField ID="hdPostOROperationID" runat="server" />
                                                <div style="overflow-y: scroll; height: 350px;">
                                                    <asp:GridView ID="gvImplantMain" runat="server" ShowHeaderWhenEmpty="True"
                                                        EmptyDataText="No records Found" AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover"
                                                        DataKeyNames="MainCode, Name" OnRowDataBound="gvImplantMain_RowDataBound" OnRowEditing="gvImplantMain_RowEditing">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Type Of Implant">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Bind("MainCode") %>' />
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle CssClass="word-break" />
                                                                <HeaderStyle Font-Size="Small" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                        <HeaderStyle Width="300px" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div style="overflow-y: scroll; height: 350px;">
                                                    <asp:HiddenField ID="hdImplantMainCode" runat="server" />
                                                    <asp:HiddenField ID="hdImplantMainName" runat="server" />
                                                    <asp:GridView ID="gvImplantSub" runat="server"
                                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                        ShowFooter="true"
                                                        AutoGenerateColumns="False"
                                                        CssClass="table table-striped table-bordered table-hover table-responsive"
                                                        DataKeyNames="MainCode,Name,SubCode,SubName" OnRowCommand="gvImplantSub_RowCommand">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Implant">
                                                                <ItemTemplate>
                                                                    <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                                    <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                                    <asp:HiddenField ID="hdgvName" runat="server" Value='<%# Eval("Name") %>' />
                                                                    <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txtgvSubNameFt" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <FooterStyle Font-Size="Small" />
                                                                <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btngvSubNew" runat="server" Width="45px" CssClass="btn btn-success btn-sm" Text="Add" CommandName="Add" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
                                                                <FooterTemplate>
                                                                    <asp:Button ID="btngvSubNewFt" runat="server" Width="45px" CssClass="btn btn-success btn-sm" Text="Add" CommandName="Add" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                </FooterTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div style="overflow-y: scroll; height: 300px;">
                                            <asp:GridView ID="gvImplant" runat="server"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover table-responsive"
                                                DataKeyNames="ID,PostOperation_ID,MainCode,SubCode,SubName" 
                                                OnRowCommand="gvImplant_RowCommand"
                                                OnRowDeleting="gvImplant_RowDeleting">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Type Of Implant">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                            <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                            <asp:Label ID="lblgvName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Implant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Remark">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtRemark" CssClass="form-control" runat="server" Text='<%# Eval("Remark") %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                        <HeaderStyle Font-Size="Small" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="Button4" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="บันทึก" CommandName="remark" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="ลบ" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="text-left" HorizontalAlign="Center" Width="110px"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                <HeaderStyle CssClass="table-success" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <asp:UpdatePanel ID="UpdatePanel20" runat="server">
        <ContentTemplate>
            <!-- Modal -->
            <div class="modal fade" id="ModalSide">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title text-primary">คำเตือน!!</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 text-center">
                                        <i class="fa fa-4x fa-exclamation-circle text-warning"></i>
                                        <br />
                                        <asp:Label runat="server" ID="Label3" Text="กรุณาระบุข้างก่อน"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            var ordate = document.getElementById('<%=hdDate.ClientID %>').value;
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdDateEn.ClientID %>').value;

                $('#txtDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", "0");  //กำหนดเป็นวันปัจุบัน

                var xdate = $('#txtDate').val();
                document.getElementById('<%=hdDate.ClientID %>').value = xdate;
            }
            if ($("#<%=chbORTimeFollow.ClientID %>").is(':checked')) {
                $("#<%=divORTime.ClientID %>").hide();
                $("#<%=lblORTimeH.ClientID %>").hide();
            } else {
                $("#<%=divORTime.ClientID %>").show();
                $("#<%=lblORTimeH.ClientID %>").show();
            }
            //console.log("ORstatus", $("#<%=ddlORStatus.ClientID %>").val());
            if ($("#<%=ddlORStatus.ClientID %>").val() == 2 || $("#<%=ddlORStatus.ClientID %>").val() == 4) {
                $("#<%=ddlAdmitTimeType.ClientID %>").show();
                $("#divroomtype").show();
            }
            else {
                $("#<%=ddlAdmitTimeType.ClientID %>").hide();
                $("#divroomtype").hide();
            }
            //
            if ($("#<%=ddlAnesthesiaSign.ClientID %>").val() == 1 || $("#<%=ddlAnesthesiaSign.ClientID %>").val() == 2) {
                $("#<%=ddlAnesthesiaType2.ClientID %>").show();
            }
            else {
                $("#<%=ddlAnesthesiaType2.ClientID %>").hide();
            }
            //
            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            $('.nav-tabs li:eq(0) a').tab('show');


        });

        $("#txtDate").on("change", function () {

            var xdate = $(this).val();
            document.getElementById('<%=hdDate.ClientID %>').value = xdate;

        });

        $(function () {
            $("#<%=chbORTimeFollow.ClientID %>").on("change", function () {

                if ($(this).is(':checked')) {
                    $("#<%=divORTime.ClientID %>").hide();
                    $("#<%=lblORTimeH.ClientID %>").hide();
                } else {
                    $("#<%=divORTime.ClientID %>").show();
                    $("#<%=lblORTimeH.ClientID %>").show();
                }

            });
            $("#<%=ddlAnesthesiaSign.ClientID %>").on("change", function () {

                var xdata = $(this).val();
                if (xdata != 0) {
                    $("#<%=ddlAnesthesiaType2.ClientID %>").show();
                }
                else {
                    $("#<%=ddlAnesthesiaType2.ClientID %>").hide();
                }

            });

            $("#<%=ddlORStatus.ClientID %>").on("change", function () {

                var xdata = $(this).val();
                if (xdata == 2 || xdata == 4) {
                    $("#<%=ddlAdmitTimeType.ClientID %>").show();
                    $("#divroomtype").show();
                }
                else {
                    $("#<%=ddlAdmitTimeType.ClientID %>").hide();
                    $("#divroomtype").hide();
                }

            });

        });
        function showModalImplant() {
            $("#modalImplant").modal('show');
        }
        function showModalSide() {
            $("#ModalSide").modal('show');
        }
    </script>
    <script>
       <%-- $(document).ready(function () {


            $('#txtDate').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                thaiyear: true              //Set เป็นปี พ.ศ.
            }).datepicker("setDate", "0");  //กำหนดเป็นวันปัจุบัน

        });--%>
</script>
</asp:Content>
