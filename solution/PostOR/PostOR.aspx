<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostOR.aspx.cs" Inherits="solution.PostOR.PostOR" %>

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

        input, select, textarea {
            max-width: 100%;
        }

        .loader {
            border: 16px solid #f3f3f3; /* Light grey */
            border-top: 16px solid #3498db; /* Blue */
            border-radius: 50%;
            width: 200px;
            height: 200px;
            animation: spin 2s linear infinite;
            position: sticky;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        /*.modal-body {
            padding-left: 30%;
        }*/
        body .popover {
            max-width: 830px;
        }
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/PostOR/">List Post OR</a>
            </li>
            <li class="breadcrumb-item active">Post OR</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">Post OR</span></h4>
            </div>
            <div class="col-md-6">

                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <asp:Button ID="btnPostTreatment" class="btn btn-info pull-right" runat="server" Text="Post Treatment" OnClick="PostTreatment_Click" data-toggle="modal" data-target="#exampleModalCenter" />
                    </div>
                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <asp:Button ID="btnSave" class="btn btn-info pull-right" runat="server" Text="Save" OnClick="Save_Click" data-toggle="modal" data-target="#exampleModalCenter" />
                    </div>
                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <a href="/Print/StickerORpost/?o=<%= hdORID.Value %>" style="width: 130px" target="_blank" class="btn btn-info pull-right"><i class="fa fa-1x fa-print" aria-hidden="true"></i>  Sticker OR</a>
                    </div>                    
                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <asp:Button ID="btnClear" class="btn btn-info pull-right" runat="server" Text="Refresh" OnClick="Clear_Click" />

                    </div>
                    <!-- Modal -->
                    <div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
                        <div class="modal-dialog modal-dialog-centered" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalCenterTitle">In Process....</h5>
                                </div>
                                <div class="modal-body" style="padding-left: 30%;">
                                    <div class="loader"></div>
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
                    <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%">--%>
                    <asp:Image runat="server" ID="imgPatient" ImageUrl="../Images/17241-200.png" CssClass="img-thumbnail" Style="width: 200px" />
                </div>
                <div class="col-md-10">
                    <div class="row">
                        <div class="col-3 form-inline">
                            <label>HN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblHN" Font-Bold="true" runat="server"></asp:Label>
                            <asp:HiddenField runat="server" ID="hdORID" />
                            <asp:HiddenField ID="hdVisitDate" runat="server" />
                            <asp:HiddenField ID="hdVN" runat="server" />
                            <asp:HiddenField ID="hdHN" runat="server" />
                        </div>
                        <div class="col-3 form-inline" runat="server" id="divAN" visible="false">
                            <label>AN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblAN" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-6 form-inline" runat="server" id="divVN" visible="false">
                            <label>VN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblVN" Font-Bold="true" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <label>Visit Date&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblVN_VisitDate" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-6 form-inline">
                            <label>Patient Name&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblPatientName" Font-Bold="true" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-2 form-inline">
                            <label>Gender&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblGender" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                        <div class="col-2 form-inline">

                            <label>Age&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblAge" Font-Bold="true" runat="server"></asp:Label>&nbsp;
                            <label>Year</label>

                        </div>
                        <div class="col-4 form-inline">
                            <label>Birth Date&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblBirthDateTime" Font-Bold="true" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-4 form-inline">
                            <label>ID Card&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblIDCARD" runat="server" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-6 form-inline">
                            <label>Nationality&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblNationality" runat="server" Font-Bold="true"></asp:Label>
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
                    <div class="row mb-1">
                        <div class="col-3">
                            <asp:CheckBox runat="server" ID="chORWoundType1" Text="&nbsp;Re operation" />
                        </div>
                        <div class="col-6">
                            <asp:Label ID="lblHeadWarning" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnHeadWarningMore" class="btn btn-sm btn-outline-success" Style="cursor: pointer; font-size: .3rem;" runat="server" Text="More.." data-toggle="modal" data-target="#modalHeadWarningMore" />
                            <!-- Modal -->
                            <div class="modal fade bd-example-modal-md" id="modalHeadWarningMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                                <div class="modal-dialog modal-md" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Warning</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                                <ContentTemplate>
                                                    <asp:GridView ID="gvHeadWarningMore" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                        CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Warning" DataField="Warning" HtmlEncode="false">
                                                                <ItemStyle CssClass="word-break" />
                                                                <HeaderStyle CssClass="text-center" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                                    </asp:GridView>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnHeadWarningMore" EventName="Click" />
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
                            <div class="modal fade bd-example-modal-lg" id="modalPatientalDiagMore" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Underlying disease</h5>
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
                                                            <asp:BoundField HeaderText="โรค" DataField="icdname" HtmlEncode="false">
                                                                <ItemStyle CssClass="word-break" />
                                                                <HeaderStyle CssClass="text-center" />
                                                            </asp:BoundField>
                                                        </Columns>
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

                    <div class="row">
                        <div class="col-8">
                            <asp:TextBox runat="server" ID="txtUnderlyingtext" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row mb-1 mt-1">
                        <div class="col-2">
                            <asp:CheckBox runat="server" ID="chOnmed" AutoPostBack="true" Text="&nbsp;ON med" OnCheckedChanged="chOnmed_CheckedChanged" />
                        </div>
                        <div class="col-6">
                            <asp:UpdatePanel ID="upOnmed" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox TextMode="MultiLine" Rows="2" MaxLength="120" runat="server" ID="txtOnmed" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="chOnmed" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <asp:Panel ID="pnORHEADER" runat="server" CssClass="bd-example">

        <div class="panel">
            <ul class="nav nav-tabs">
                <li class="nav-item ">
                    <a class="nav-link text-center" href="#Detail" style="min-width: 150px">Pre-OP</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#Operation" style="min-width: 150px">Post-OP & Procedure</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#ICD" style="min-width: 150px">ICD</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#PostORDetail" style="min-width: 150px">Time OR</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#Nurse" style="min-width: 150px">Nurse</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#ORSummary" style="min-width: 150px">OR Summary</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#UnderPatient" style="min-width: 150px">Status Patient</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#Complication" style="min-width: 150px">Indicator</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#Warning" style="min-width: 150px">Warning</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-center" href="#Migration" style="min-width: 150px">Migration Data</a>
                </li>
            </ul>
            <div class="tab-content">

                <div id="Detail" class="tab-pane fade in active">
                    <br />
                    <div class="row">
                        <div class="col-7">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">OR Date : </label>
                                    </div>
                                    <div class="col-md-5">
                                        <div class="input-group" data-date="12/02/2560" data-date-format="dd-mm-yyyy">
                                            <input id="txtORDate" name="txtORDate" class="datepicker form-control input-group-sm" data-date-format="dd-mm-yyyy">
                                            <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>
                                        </div>
                                        <asp:HiddenField ID="hdORDate" runat="server" />
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
                                        <asp:DropDownList ID="ddlORRoom" runat="server" AutoPostBack="false" CssClass="form-control">
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
                                        <asp:TextBox Rows="3" ID="txtRemark" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                    <div class="col-md-3 p-0">
                                        <label class="pull-right">Prediag&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtPrediag" runat="server" AutoPostBack="false" TextMode="MultiLine" Rows="3" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                        </div>
                        <div class="col-5">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="pull-left">Surgeon</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(1)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchSurgeon1" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchSurgeon1_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-6 pl-1 pl-1">
                                        <asp:UpdatePanel ID="upSearchSurgeon1" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSurgeon1" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchSurgeon1" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon1" GroupName="rbSurgeon" Checked="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(2)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchSurgeon2" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchSurgeon2_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-6 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSurgeon2" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchSurgeon2" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon2" GroupName="rbSurgeon" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(3)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchSurgeon3" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchSurgeon3_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-6 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlSurgeon3" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchSurgeon3" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                    <div class="col-1 p-0">
                                        <asp:RadioButton runat="server" ID="rbSurgeon3" GroupName="rbSurgeon" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <label class="pull-left">Anes Doctor</label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(1)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchAnesDoctor1" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchAnesDoctor1_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-7 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAnesthesiaDoctor1" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchAnesDoctor1" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(2)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchAnesDoctor2" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchAnesDoctor2_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-7 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAnesthesiaDoctor2" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchAnesDoctor2" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 pr-1">
                                        <label class="pull-right">(3)</label>
                                    </div>
                                    <div class="col-md-3 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtSearchAnesDoctor3" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchAnesDoctor3_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-7 pl-1 pl-1">
                                        <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlAnesthesiaDoctor3" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtSearchAnesDoctor3" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" hidden>
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(1) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse1" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" hidden>
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(2) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse2" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" hidden>
                                <div class="row">
                                    <div class="col-md-5 p-0">
                                        <label class="pull-right">Anes Nurse(3) : </label>
                                    </div>
                                    <div class="col-md-7">
                                        <asp:DropDownList ID="ddlAnesthesiaNurse3" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="pull-left">Request By</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="txtRequestByUser" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                             <div class="form-group">
                                <div class="row">
                                    <div class="col-md-3">
                                        <label class="pull-left">Suggest By</label>
                                    </div>
                                    <div class="col-md-9">
                                        <asp:TextBox CssClass="form-control form-control-sm" runat="server" ID="txtSuggestByUser" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="PostORDetail" class="tab-pane fade">
                    <br />
                    <div class="form-group">

                        <div class="row">
                            <div class="col-md-2 text-right">
                                <label class="pull-right">Block DateTime:</label>
                                <asp:DropDownList ID="ddlSBlockTimeH" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSBlockTimeM" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:HiddenField ID="hdSBlockDate" runat="server" />
                                <asp:HiddenField ID="hdSBlockDateEn" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtSBlockDate" name="txtSBlockDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSBlockTime" runat="server" CssClass="form-control" onfocus="reSBlock(this.value);" onblur="setSBlockTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                            <span class="input-group-addon">-</span>
                                            <asp:TextBox ID="txtFBlockTime" runat="server" CssClass="form-control" onfocus="reFBlock(this.value);" onblur="setFBlockTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1 text-center" hidden>
                                <label class="pull-right">To </label>
                            </div>
                            <div class="col-md-3" hidden>
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtFBlockDate" name="txtFBlockDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                    <asp:HiddenField ID="hdFBlockDate" runat="server" />
                                    <asp:HiddenField ID="hdFBlockDateEn" runat="server" />
                                    <asp:DropDownList ID="ddlFBlockTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlFBlockTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2 text-right">
                                <label class="pull-right">Anes DateTime:</label>
                                <asp:DropDownList ID="ddlSAnesTimeH" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSAnesTimeM" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:HiddenField ID="hdSAnesDate" runat="server" />
                                <asp:HiddenField ID="hdSAnesDateEn" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtSAnesDate" name="txtSAnesDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSAnesTime" runat="server" CssClass="form-control" onfocus="reSAnes(this.value);" onblur="setSAnesTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                            <span class="input-group-addon">-</span>
                                            <asp:TextBox ID="txtFAnesTime" runat="server" CssClass="form-control" onfocus="reFAnes(this.value);" onblur="setFAnesTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1 text-center" hidden>
                                <label class="pull-right">To </label>
                            </div>
                            <div class="col-md-3" hidden>
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtFAnesDate" name="txtFAnesDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                    <asp:HiddenField ID="hdFAnesDate" runat="server" />
                                    <asp:HiddenField ID="hdFAnesDateEn" runat="server" />
                                    <asp:DropDownList ID="ddlFAnesTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlFAnesTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2 text-right">
                                <label class="pull-right">OR DateTime:</label>
                                <asp:DropDownList ID="ddlSORTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0" Visible="false"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSORTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0" Visible="false"></asp:DropDownList>
                                <asp:HiddenField ID="hdSORDate" runat="server" />
                                <asp:HiddenField ID="hdSORDateEn" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtSORDate" name="txtSORDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="upTime" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                            <asp:TextBox ID="txtSORTime" onblur="setSORTime(this.value);" onfocus="reSOR(this.value);" onkeypress="return isDigit(event,this.value);" runat="server" CssClass="form-control"></asp:TextBox>
                                            <span class="input-group-addon">-</span>
                                            <asp:TextBox ID="txtFORTime" onblur="setFORTime(this.value);" onfocus="reFOR(this.value);" onkeypress="return isDigit(event,this.value);" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1 text-center" hidden>
                                <label class="pull-right">To </label>
                            </div>
                            <div class="col-md-3" hidden>
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtFORDate" name="txtFORDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                    <asp:DropDownList ID="ddlFORTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlFORTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                    <asp:HiddenField ID="hdFORDate" runat="server" />
                                    <asp:HiddenField ID="hdFORDateEn" runat="server" />
                                </div>
                            </div>
                        </div>                      
                        
                        <div class="row">
                            <div class="col-md-2 text-right">
                                <label class="pull-right">Recovery DateTime:</label>
                                <asp:DropDownList ID="ddlSRecoveryTimeH" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:DropDownList ID="ddlSRecoveryTimeM" Visible="false" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                <asp:HiddenField ID="hdSRecoveryDate" runat="server" />
                                <asp:HiddenField ID="hdSRecoveryDateEn" runat="server" />
                            </div>
                            <div class="col-md-2">
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtSRecoveryDate" name="txtSRecoveryDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                    <ContentTemplate>
                                        <div class="input-group">
                                            <asp:TextBox ID="txtSRecoveryTime" runat="server" CssClass="form-control" onfocus="reSRecovery(this.value);" onblur="setSRecoveryTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                            <span class="input-group-addon">-</span>
                                            <asp:TextBox ID="txtFRecoveryTime" runat="server" CssClass="form-control" onfocus="reFRecovery(this.value);" onblur="setFRecoveryTime(this.value);" onkeypress="return isDigit(event,this.value);"></asp:TextBox>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-1 text-center" hidden>
                                <label class="pull-right">To </label>
                            </div>
                            <div class="col-md-3" hidden>
                                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                    <input id="txtFRecoveryDate" name="txtFRecoveryDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" style="width: 70px">
                                    <span class="input-group-addon">
                                        <i class="fa fa-calendar-o"></i>
                                    </span>
                                    <asp:HiddenField ID="hdFRecoveryDate" runat="server" />
                                    <asp:HiddenField ID="hdFRecoveryDateEn" runat="server" />
                                    <asp:DropDownList ID="ddlFRecoveryTimeH" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                    <asp:DropDownList ID="ddlFRecoveryTimeM" runat="server" AutoPostBack="false" CssClass="form-control p-0"></asp:DropDownList>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-2 text-right">
                                <label class="pull-right">OR Case Type:</label>
                            </div>
                            <div class="col-md-4">
                                <div class="input-group">
                                    <asp:DropDownList ID="ddlORCaseType" runat="server" AutoPostBack="false" CssClass="form-control">
                                        <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="Elective Case"></asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="input-group-addon">
                                        <i class="fa fa-question-circle" data-toggle="tooltip" data-placement="right" title="&nbsp;&nbsp;&nbsp;ประเภทการ Set ผ่าตัด เป็นแบบ Elective (การจองผ่าตัดล่วงหน้า) และ Urgency (การ Set ผ่าตัดหลังจากที่มีการออกใบ Set แล้ว หลัง 15.30 น. ของทุกวัน) และอาจเพิ่ม Emergency case (Stat case เช่น RRD , Endophthalmitis , Corneal Emergency , Peroration & Rupture Infection , Trauma / F.B. ,REH post op complication & Revision , Glaucoma Emergency)"></i>
                                    </span>
                                </div>
                            </div>
                        </div>

                        <div class="row" hidden>
                            <div class="col-md-2 text-right">
                                <label class="pull-right">OR Wrong Case:</label>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlORWrongCase" runat="server" AutoPostBack="false" CssClass="form-control">
                                    <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>
                </div>

                <div id="Operation" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-7">
                                        <div class="row" style="height: 60px">
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
                                                        <HeaderStyle Width="300px" />
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
                                        <div class="row" style="height: 60px">
                                            <div class="col-md-12 m-2 text-right">
                                                <asp:Button CssClass="btn btn-xs btn-info" ID="btnCopyFromReserve" runat="server" Text="Copy From Reserve" Visible="false" />
                                                <asp:UpdatePanel ID="udpConfirmPostOR" runat="server">
                                                    <ContentTemplate>
                                                        <div class="btn-group mr-2" role="group" aria-label="First group">
                                                            <asp:Button ID="btnConfirm" class="btn btn-primary pull-right" runat="server" Text="Confirm" OnClick="Confirm_Click" />
                                                            <asp:Button ID="btnCancel" class="btn btn-warning pull-right" runat="server" Text="Cxl confirm" OnClick="Cancel_Click" data-toggle="modal" data-target="#ModalCancelReason" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                
                                            </div>
                                        </div>
                                        <div style="overflow-y: scroll; height: 300px;">
                                            <asp:GridView ID="gvOROperation" runat="server"
                                                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                AutoGenerateColumns="False"
                                                CssClass="table table-striped table-bordered table-hover table-responsive"
                                                DataKeyNames="ID,MainCode,Name,SubCode,SubName,Side,strSide,SubMark,strSubmark"
                                                OnRowDeleting="gvOROperation_RowDeleting"
                                                OnRowCommand="gvOROperation_OnRowCommand" OnRowDataBound="gvOROperation_RowDataBound">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Type Of Operation">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                            <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                            <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                            <asp:HiddenField ID="hdgvSide" runat="server" Value='<%# Eval("Side") %>' />
                                                            <asp:HiddenField ID="hdgvSubMark" runat="server" Value='<%# Eval("SubMark") %>' />
                                                            <asp:HiddenField ID="hdgvSurgeon1" runat="server" Value='<%# Eval("Surgeon1") %>' />
                                                            <asp:HiddenField ID="hdgvSurgeon2" runat="server" Value='<%# Eval("Surgeon2") %>' />
                                                            <asp:HiddenField ID="hdgvSurgeon3" runat="server" Value='<%# Eval("Surgeon3") %>' />
                                                            <asp:HiddenField ID="hdgvComplication" runat="server" Value='<%# Eval("Complication") %>' />
                                                            <asp:HiddenField ID="hdgvICD" runat="server" Value='<%# Eval("ICD") %>' />
                                                            <asp:HiddenField ID="hdgvOrgan" runat="server" Value='<%# Eval("Organ") %>' />
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
                                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="ลบ" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <asp:Button ID="btnSetup" runat="server" CssClass="btn btn-info btn-sm mousecursor" Text="Doctor" CommandName="setup" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <asp:Button ID="btnImplant" runat="server" CssClass="btn btn-primary btn-sm mousecursor" Text="Implant" CommandName="implant" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            <%--<asp:LinkButton ID="lnkbtnUp" runat="server" CommandName="Up" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-arrow-up mousecursor p-0" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnDown" runat="server" CommandName="Up" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"><i class="fa fa-arrow-up mousecursor p-0" aria-hidden="true"></i></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="text-left" Width="180px"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                <HeaderStyle CssClass="table-success" />
                                            </asp:GridView>
                                        </div>
                                        <br />
                                        <div class="card">
                                            <div class="card-body">
                                                <div class="row" style="margin-left: 0px;">
                                                    <asp:Label ID="lblProcedureRE" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left: 0px;">
                                                    <asp:Label ID="lblProcedureLE" runat="server"></asp:Label>
                                                </div>
                                                <div class="row" style="margin-left: 0px;">
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

                <div id="Nurse" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Nurse Type&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlNurseType" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdSuffixNurse" runat="server" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Nurse&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlNurseCode" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Remark&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtNurseRemark" runat="server" AutoPostBack="false" TextMode="MultiLine" Rows="3" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAddNurse" OnClick="btnAddNurse_Click" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClearNurse" OnClick="btnClearNurse_Click" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-9">
                                        <asp:GridView ID="gvPostORNurse" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover table-responsive"
                                            DataKeyNames="ORID,Suffix,NurseType,NurseTypeDesc,Nurse,NurseCode,Remark"
                                            OnRowCommand="gvPostORNurse_RowCommand"
                                            OnRowEditing="gvPostORNurse_RowEditing"
                                            OnRowDeleting="gvPostORNurse_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nurse Type" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNurseType" runat="server" Text='<%# Eval("NurseTypeDesc") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdgvNurseType" runat="server" Value='<%# Eval("NurseType") %>' />
                                                        <asp:HiddenField ID="hdgvSuffix" runat="server" Value='<%# Eval("Suffix") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Nurse" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNurse" runat="server" Text='<%# Eval("Nurse") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdgvNurseCode" runat="server" Value='<%# Eval("NurseCode") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                            <HeaderStyle CssClass="table-success" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="ICD" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="form-group">

                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICD</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDSearch" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDSearch_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICD" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>  
                                                <asp:HiddenField ID="hdPOSTORICD_ID" runat="server" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDSearch" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICDCM1</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM1Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM1Search_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICDCM1" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM1Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Doctor 1/2/3/4&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm1doc1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm1doc2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm1doc3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm1doc4" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICDCM2</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM2Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM2Search_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICDCM2" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM2Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Doctor 1/2/3/4&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm2doc1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm2doc2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm2doc3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm2doc4" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICDCM3</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM3Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM3Search_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICDCM3" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM3Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Doctor 1/2/3/4&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm3doc1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm3doc2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm3doc3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm3doc4" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                
                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICDCM4</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM4Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM4Search_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICDCM4" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM4Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Doctor 1/2/3/4&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm4doc1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm4doc2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm4doc3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm4doc4" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                

                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ICDCM5</label>
                                    </div>
                                    <div class="col-md-1 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtICDCM5Search" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." 
                                            OnTextChanged="txtICDCM5Search_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlICDCM5" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList> 
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtICDCM5Search" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Doctor 1/2/3/4&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm5doc1" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm5doc2" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm5doc3" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:DropDownList ID="ddlcm5doc4" runat="server" AutoPostBack="false" CssClass="form-control">
                                            <asp:ListItem Value="0" Text="None"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="mb-2"></div>
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Remark&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtICD_Remark" runat="server" AutoPostBack="false" TextMode="MultiLine" Rows="3" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>                                
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAddICD" OnClick="btnAddICD_Click" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClearICD" OnClick="btnClearICD_Click" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-1"></div>
                                    <div class="col-10">
                                        <asp:GridView ID="gvPostORICD" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover table-responsive"
                                            DataKeyNames="ID,ORID,ICD,ICDCM1,ICDCM2,ICDCM3,ICDCM4,ICDCM5,Remark,MakeDatetime"
                                            OnRowCommand="gvPostORICD_RowCommand"
                                            OnRowEditing="gvPostORICD_RowEditing"
                                            OnRowDeleting="gvPostORICD_RowDeleting">
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Code" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="ICD" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICD_Name" runat="server" Text='<%# Eval("ICD_Name") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hdgvICD" runat="server" Value='<%# Eval("ICD") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICDCM1" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICDCM1" runat="server" Text='<%# Eval("ICDCM1_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICDCM2" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICDCM2" runat="server" Text='<%# Eval("ICDCM2_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICDCM3" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICDCM3" runat="server" Text='<%# Eval("ICDCM3_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICDCM4" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICDCM4" runat="server" Text='<%# Eval("ICDCM4_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ICDCM5" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvICDCM5" runat="server" Text='<%# Eval("ICDCM5_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Make Datetime" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMakeDatetime" runat="server" Text='<%# Eval("MakeDatetime") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                            <HeaderStyle CssClass="table-success" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="UnderPatient" class="tab-pane fade">
                    <asp:GridView ID="gvUnderPatient" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                        CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                        DataKeyNames="ORID">
                        <Columns>
                            <asp:BoundField HeaderText="OR Date" DataField="strORDate" HeaderStyle-HorizontalAlign="Right">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Time" DataField="ORTime">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="แพทย์" DataField="SurgeonName">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="OR Room" DataField="strORRoom">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Operation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("POSTOROPERATION") %>'></asp:Label>
                                    <asp:HiddenField ID="hdORDate" runat="server" Value='<%# Eval("ORDate") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdstrSide" runat="server" Value='<%# Eval("strSide") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdNote" runat="server" Value='<%# Eval("Note") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hdSurgeonName" runat="server" Value='<%# Eval("SurgeonName") %>'></asp:HiddenField>
                                </ItemTemplate>
                                <ItemStyle CssClass="word-break" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>

                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                    </asp:GridView>
                </div>

                <div id="ORSummary" class="tab-pane fade">
                    <br />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-3 text-right">
                                Diagnosis ::
                            </div>
                            <div class="col-7">
                                <asp:Label runat="server" ID="lblDiage"></asp:Label>
                            </div>
                            <div class="col-2">
                                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                                    <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click">Download PDF</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                วันที่ผ่าตัด ::
                            </div>
                            <div class="col-3">
                                <asp:Label runat="server" ID="lblORDate"></asp:Label>
                            </div>

                            <div class="col-6" hidden>
                                เวลาผ่าตัด  เริ่ม ::
                                <asp:Label runat="server" ID="lblORTimeS"></asp:Label>
                                สิ้นสุด ::
                                <asp:Label runat="server" ID="lblORTimeF"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                การผ่าตัด ::
                            </div>
                            <div class="col-9">
                                <asp:Label runat="server" ID="lblProcedureSummary"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                ICDCM ::
                            </div>
                            <div class="col-9">
                                <asp:Label runat="server" ID="lblICDCM"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                ทีมผ่าตัด Scrub ::
                            </div>
                            <div class="col-6">
                                <asp:Label runat="server" ID="lblScrub"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                ทีมผ่าตัด Circulate ::
                            </div>
                            <div class="col-6">
                                <asp:Label runat="server" ID="lblCircalate"></asp:Label>
                            </div>
                        </div>
                        <hr>
                        <div class="row">
                            <div class="col-3 text-right">
                                วันที่ดมยา ::
                            </div>
                            <div class="col-3">
                                <asp:Label runat="server" Font-Bold="true" ID="lblAnesDate"></asp:Label>
                            </div>

                            <div class="col-6" hidden>
                                เวลา เริ่ม ::
                                <asp:Label runat="server" Font-Bold="true" ID="lblStartAnesTime"></asp:Label>
                                สิ้นสุด ::
                                <asp:Label runat="server" Font-Bold="true" ID="lblFinishAnesTime"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                ประเภทยา ::
                            </div>
                            <div class="col-9">
                                <asp:Label runat="server" Font-Bold="true" ID="lblAnesthesiaType"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                Anes Doctor ::
                            </div>
                            <div class="col-9">
                                <asp:Label runat="server" Font-Bold="true" ID="lblAnesthesiaDoctor"></asp:Label>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-3 text-right">
                                Anes Nurse ::
                            </div>
                            <div class="col-9">
                                <asp:Label runat="server" Font-Bold="true" ID="lblAnesthesiaNurse"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <div id="Complication" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row mb-2">
                                    <div class="col-md-2 text-right">
                                        <asp:Label ID="lblIndicator" runat="server" Text="Indicator"></asp:Label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:TextBox ID="txtIndicator" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chORWoundType2" Text="&nbsp;ผิดคน" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chChangOperation" Text="&nbsp;Change Operation" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chORWoundType3" Text="&nbsp;ผิดข้าง" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chHR48" Text="&nbsp;ติดเชื้อภายใน 48 ชม." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chORWoundType4" Text="&nbsp;ผิดชนิดการผ่าตัด" />
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chDay30" Text="&nbsp;ติดเชื้อภายใน 30 วัน หลังผ่าตัด" />
                                    </div>
                                </div>
                                <div class="row" hidden>
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chExternal" Text="&nbsp;External" />
                                    </div>
                                </div>
                                <div class="row" hidden>
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chAnterior" Text="&nbsp;Anterior" />
                                    </div>
                                </div>
                                <div class="row" hidden>
                                    <div class="col-md-2 text-right">
                                    </div>
                                    <div class="col-md-3">
                                        <asp:CheckBox runat="server" ID="chPosterior" Text="&nbsp;Posterior" />
                                    </div>
                                </div>
                                <hr />
                                <asp:UpdatePanel ID="upComplication" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-md-2 text-right">
                                                <label class="pull-right">หัวข้อ&nbsp:&nbsp</label>
                                            </div>
                                            <div class="col-md-3">
                                                <asp:TextBox ID="txtComplicationHeader" runat="server" Visible="false" CssClass="form-control"></asp:TextBox>
                                                <asp:DropDownList runat="server" ID="ddlComplicationID" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlComplicationID_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:HiddenField ID="hdComplicationID" runat="server" />
                                            </div>
                                            <div class="col-md-3">
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-md-2 text-right">
                                                <label class="pull-right">รายละเอียด&nbsp:&nbsp</label>
                                            </div>
                                            <div class="col-md-5">
                                                <asp:TextBox TextMode="MultiLine" Rows="2" ID="txtComplicationDetail" runat="server" AutoPostBack="false" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAddComplication" OnClick="btnAddComplication_Click" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClearComplication" OnClick="btnClearComplication_Click" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-9">
                                        <asp:GridView ID="gvComplication" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover table-responsive"
                                            DataKeyNames="ORID,ID,ComplicationHeader,ComplicationDetail"
                                            OnRowCommand="gvComplication_RowCommand"
                                            OnRowEditing="gvComplication_RowEditing"
                                            OnRowDeleting="gvComplication_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="หัวข้อ" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComplicationHeader" runat="server" Text='<%# Eval("ComplicationHeader") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hdORID" runat="server" Value='<%# Eval("ORID") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="รายละเอียด" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblComplicationDetail" runat="server" Text='<%# Eval("ComplicationDetail") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                            <HeaderStyle CssClass="table-success" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="Warning" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Warning&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtWarning" runat="server" AutoPostBack="false" CssClass="form-control">                                            
                                        </asp:TextBox>
                                        <asp:HiddenField ID="hdWarningID" runat="server" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAddWarning" OnClick="btnAddWarning_Click" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClearWarning" OnClick="btnClearWarning_Click" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-9">
                                        <asp:GridView ID="gvWarning" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover table-responsive"
                                            DataKeyNames="ID,ORID,Warning,CreateDateTime"
                                            OnRowCommand="gvWarning_RowCommand"
                                            OnRowEditing="gvWarning_RowEditing"
                                            OnRowDeleting="gvWarning_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Warning" HeaderStyle-CssClass="text-left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWarning" runat="server" Text='<%# Eval("Warning") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hdORID" runat="server" Value='<%# Eval("ORID") %>' />
                                                        <asp:HiddenField ID="hdCreateDateTime" runat="server" Value='<%# Eval("CreateDateTime") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                            <HeaderStyle CssClass="table-success" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div id="Migration" class="tab-pane fade">
                    <br />
                    <asp:UpdatePanel ID="UpdatePanel28" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">ORDate&nbsp:&nbsp</label>
                                        <asp:HiddenField ID="hdMigrationID" runat="server" />
                                    </div>
                                    <div class="col-md-3">
                                        <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                                            <input id="txtMigrationORDate" name="txtMigrationORDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy">
                                            <span class="input-group-addon"><i class="fa fa-calendar-o"></i></span>
                                        </div>
                                        <asp:HiddenField ID="hdORMigrationDate" runat="server" />
                                        <asp:HiddenField ID="hdORMigrationDateEn" runat="server" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Side&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="ddlORMigrationSide" runat="server" AutoPostBack="false" CssClass="form-control d-block">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-3">
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Procedure Memo&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtProcedureMemo" runat="server" AutoPostBack="false" TextMode="MultiLine" Rows="3" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Note&nbsp:&nbsp</label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:TextBox ID="txtNote" runat="server" AutoPostBack="false" TextMode="MultiLine" Rows="3" CssClass="form-control">                                            
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="row mt-1">
                                    <div class="col-md-2 text-right">
                                        <label class="pull-right">Surgeon</label>
                                    </div>
                                    <div class="col-md-2 pr-0">
                                        <asp:TextBox AutoPostBack="true" ID="txtORMigrationSurgeonSearch" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtORMigrationSurgeonSearch_TextChanged"></asp:TextBox>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlORMigrationSurgeon" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm">
                                                    <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                </asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="txtORMigrationSurgeonSearch" EventName="TextChanged" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <label class="pull-right">OR Room : </label>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlORMigrationORRoom" runat="server" AutoPostBack="false" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </div>
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-success btn-sm" Width="80px" Text="Save" runat="server" ID="btnAddMigration" OnClick="btnAddMigration_Click" />
                                        </div>
                                    </div>
                                    <div class="col-1">
                                        <div class="btn-group mt-2" role="group" aria-label="Second group">
                                            <asp:Button CssClass="btn btn-secondary btn-sm" Width="80px" Text="Clear" runat="server" ID="btnClearMigration" OnClick="btnClearMigration_Click" />
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-2"></div>
                                    <div class="col-9">
                                        <asp:GridView ID="gvORMigration" runat="server"
                                            ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                            AutoGenerateColumns="False"
                                            CssClass="table table-bordered table-hover table-responsive"
                                            OnRowCommand="gvORMigration_RowCommand"
                                            OnRowEditing="gvORMigration_RowEditing"
                                            OnRowDeleting="gvORMigration_RowDeleting">
                                            <Columns>
                                                <asp:TemplateField HeaderText="ORDate" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvORDate" runat="server" Text='<%# Eval("strORDate") %>'></asp:Label>
                                                        <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                        <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break text-center" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Side" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="hdgvSide" runat="server" Value='<%# Eval("Side") %>'></asp:HiddenField>
                                                        <asp:Label ID="lblgvstrSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break text-center" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ProcedureMemo" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvProcedureMemo" runat="server" Text='<%# Eval("ProcedureMemo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Note" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvNote" runat="server" Text='<%# Eval("Note") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Surgeon" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSurgeon" runat="server" Text='<%# Eval("SurgeonName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ORRoom" HeaderStyle-CssClass="text-center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvORRoom" runat="server" Text='<%# Eval("ORRoomName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="word-break" Font-Size="Small" />
                                                    <HeaderStyle Font-Size="Small" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Edit" CommandName="edit" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        <asp:Button ID="Button1" Style="min-width: 60px" runat="server" CssClass="btn btn-danger btn-sm mousecursor" Text="Delete" CommandName="delete" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                            <HeaderStyle CssClass="table-success" />
                                        </asp:GridView>
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
    <div class="modal fade bd-example-modal-md" id="modalSetupProcedure" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Doctor</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>

                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Side : </label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Label runat="server" ID="lblSide"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdPostOperation_ID" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Type Of Operation : </label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Label runat="server" ID="lblTypeOfOperation"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Procedure : </label>
                                        </div>
                                        <div class="col-5">
                                            <asp:Label runat="server" ID="lblProcedure"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Surgeon (1) : </label>
                                        </div>
                                        <div class="col-md-2 pr-0">
                                            <asp:TextBox AutoPostBack="true" ID="txtSearchProSurgeon1" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchProSurgeon1_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-4 pl-1 pl-1">
                                            <asp:UpdatePanel ID="upSearchProSurgeon1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlProSurgeon1" runat="server" AutoPostBack="false" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtSearchProSurgeon1" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Surgeon (2) : </label>
                                        </div>
                                        <div class="col-md-2 pr-0">
                                            <asp:TextBox AutoPostBack="true" ID="txtSearchProSurgeon2" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchProSurgeon2_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-4 pl-1 pl-1">
                                            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlProSurgeon2" runat="server" AutoPostBack="false" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtSearchProSurgeon2" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Surgeon (3) : </label>
                                        </div>
                                        <div class="col-md-2 pr-0">
                                            <asp:TextBox AutoPostBack="true" ID="txtSearchProSurgeon3" runat="server" CssClass="form-control form-control-sm" placeholder="ค้นหา.." OnTextChanged="txtSearchProSurgeon3_TextChanged"></asp:TextBox>
                                        </div>
                                        <div class="col-4 pl-1 pl-1">
                                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlProSurgeon3" runat="server" AutoPostBack="false" CssClass="form-control">
                                                        <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="txtSearchProSurgeon3" EventName="TextChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">Complication : </label>
                                        </div>
                                        <div class="col-5">
                                            <asp:DropDownList ID="ddlComplication" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlComplication_SelectedIndexChanged">
                                                <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upComplicationDoctor" runat="server">
                                        <ContentTemplate>
                                            <div class="row" id="divComplicationDoctor" runat="server">
                                                <div class="col-3">
                                                </div>
                                                <div class="col-5">
                                                    <asp:TextBox TextMode="MultiLine" Rows="3" ID="txtComplication" Visible="false" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddlComplication" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                <asp:UpdatePanel ID="upOrgan" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-3">
                                                    <label class="pull-right">Organ : </label>
                                                </div>
                                                <div class="col-5">
                                                    <asp:DropDownList ID="ddlOrgan" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlOrgan_SelectedIndexChanged">
                                                        <asp:ListItem Value="" Selected="True" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <div class="row">
                                                <div class="col-3">
                                                    <label class="pull-right">Organ Position : </label>
                                                </div>
                                                <div class="col-5">
                                                    <asp:DropDownList ID="ddlOrganPosition" runat="server" AutoPostBack="false" CssClass="form-control">
                                                        <asp:ListItem Value="" Selected="True" Text="None"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlOrgan" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-3">
                                            <label class="pull-right">OR Procedure Type : </label>
                                        </div>
                                        <div class="col-5">
                                            <asp:DropDownList ID="ddlORProcedureType" runat="server" AutoPostBack="false" CssClass="form-control">
                                                <asp:ListItem Value="0" Selected="True" Text="None"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnSaveSetupProcedure" runat="server" CssClass="btn btn-primary" OnClick="btnSaveSetupProcedure_Click" Text="Save" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

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
                                                OnRowDeleting="gvImplant_RowDeleting"
                                                OnRowEditing="gvImplant_RowEditing">
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
                                                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-info btn-sm mousecursor" Text="รูป" CommandName="image" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="text-left" HorizontalAlign="Center" Width="150px"></ItemStyle>
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

    <div class="modal fade bd-example-modal-lg" id="modalImgImplant" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Set Image</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <br />
                    <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                        <ContentTemplate>
                            <table class="table table-bordered">
                                <thead>
                                    <tr>
                                        <th scope="col" class="text-center" style="width: 30%">Original</th>
                                        <th scope="col" class="text-center" style="width: 30%">New Image</th>
                                        <th scope="col" class="text-center" style="width: 20%">Upload</th>
                                        <th scope="col" class="text-center" style="width: 20%">Delete</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr class="text-center">
                                        <th>
                                            <asp:Image runat="server" Width="100%" ID="img1" CssClass="mousecursor" onclick="showimg1();" />

                                        </th>
                                        <td>
                                            <img id="imgpreview1" width="100" src="http://placehold.it/100x100" style="border-width: 0px; visibility: hidden;" /></td>
                                        <td>
                                            <asp:HiddenField ID="hdPOSTORIMPLANT_ID" runat="server" />
                                            <asp:FileUpload CssClass="btn btn-sm btn-success" ID="FileUpload1" accept='image/jpeg,image/jpg,image/png' runat="server" onchange="showpreview(this);" /></td>
                                        <td>
                                            <asp:CheckBox ID="chimg1" runat="server" /></td>
                                    </tr>

                                    <tr class="text-center">
                                        <th>
                                            <asp:Image runat="server" Width="100%" ID="img2" CssClass="mousecursor" onclick="showimg2();" /></th>
                                        <td>
                                            <img id="imgpreview2" width="100" src="http://placehold.it/100x100" style="border-width: 0px; visibility: hidden;" /></td>
                                        <td>
                                            <asp:FileUpload CssClass="btn btn-sm btn-success" ID="FileUpload2" accept='image/jpeg,image/jpg,image/png' runat="server" onchange="showpreview(this);" /></td>
                                        <td>
                                            <asp:CheckBox ID="chimg2" runat="server" /></td>
                                    </tr>

                                    <tr class="text-center">
                                        <th>
                                            <asp:Image runat="server" Width="100%" ID="img3" CssClass="mousecursor" onclick="showimg3();" /></th>
                                        <td>
                                            <img id="imgpreview3" width="100" src="http://placehold.it/100x100" style="border-width: 0px; visibility: hidden;" /></td>
                                        <td>
                                            <asp:FileUpload CssClass="btn btn-sm btn-success" ID="FileUpload3" accept='image/jpeg,image/jpg,image/png' runat="server" onchange="showpreview(this);" /></td>
                                        <td>
                                            <asp:CheckBox ID="chimg3" runat="server" /></td>
                                    </tr>
                                    <tr class="text-center">
                                        <th>
                                            <asp:Image runat="server" Width="100%" ID="img4" CssClass="mousecursor" onclick="showimg4();" /></th>
                                        <td>
                                            <img id="imgpreview4" width="100" src="http://placehold.it/100x100" style="border-width: 0px; visibility: hidden;" /></td>
                                        <td>
                                            <asp:FileUpload CssClass="btn btn-sm btn-success" ID="FileUpload4" accept='image/jpeg,image/jpg,image/png' runat="server" onchange="showpreview(this);" /></td>
                                        <td>
                                            <asp:CheckBox ID="chimg4" runat="server" /></td>
                                    </tr>
                                    <tr class="text-center">
                                        <th>
                                            <asp:Image runat="server" Width="100%" ID="img5" CssClass="mousecursor" onclick="showimg5();" /></th>
                                        <td>
                                            <img id="imgpreview5" width="100" src="http://placehold.it/100x100" style="border-width: 0px; visibility: hidden;" /></td>
                                        <td>
                                            <asp:FileUpload CssClass="btn btn-sm btn-success" ID="FileUpload5" accept='image/jpeg,image/jpg,image/png' runat="server" onchange="showpreview(this);" /></td>
                                        <td>
                                            <asp:CheckBox ID="chimg5" runat="server" /></td>
                                    </tr>
                                </tbody>
                            </table>
                        </ContentTemplate>
                        <%--<Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSaveImageImplant" EventName="Click" />
                        </Triggers>--%>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button CssClass="btn btn-primary btn-sm mousecursor" ID="btnSaveImageImplant" Text="Save" runat="server" OnClick="btnSaveImageImplant_Click" />
                    <button type="button" class="btn btn-secondary btn-sm mousecursor" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="ModalCancelReason">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">ยกเลิก....</h5>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-4 text-right">เหตุผลการยกเลิก</div>
                            <div class="col-8">
                                <asp:UpdatePanel ID="upReason" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="ddlREASON" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlREASON_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtReason" Visible="false" runat="server" CssClass="form-control mt-3"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div class="form-group">
                        <div class="row">
                            <div class="col-4"></div>
                            <div class="col-8">
                                <asp:LinkButton ID="btnCanCelReason" class="btn btn-warning" ClientIDMode="Static" runat="Server" Text="บันทึก" CausesValidation="false" OnClick="btnCanCelReason_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="upModalCheckTime" runat="server">
        <ContentTemplate>
            <!-- Modal -->
            <div class="modal fade" id="ModalCheckTime">
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
                                        <asp:Label runat="server" ID="txtStrCheckTime" Text="เวลาเริ่มห้ามมากกว่าเวลาสิ้นสุด"></asp:Label>
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

    <asp:UpdatePanel ID="UpdatePanel19" runat="server">
        <ContentTemplate>
            <!-- Modal -->
            <div class="modal fade" id="ModalCheckDigi">
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
                                        <asp:Label runat="server" ID="Label2" Text="กรุณากรอกข้อมูลให้ถูกต้อง"></asp:Label>
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

            $(function () {
                $(document).on("focusin", ".datepicker", function () {
                    $(this).datepicker();
                });
            });

            //-------------Open ORDate-------------//

            var ordate = document.getElementById('<%=hdORDate.ClientID %>').value;
            $('#txtORDate').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',
                thaiyear: false
            }).datepicker("setDate", ordate);
            var xdate = $('#txtORDate').val();
            document.getElementById('<%=hdORDate.ClientID %>').value = xdate;

            var ordate = document.getElementById('<%=hdORDate.ClientID %>').value;

            $("#txtORDate").on("change", function () {
                console.log("txtORDate");
                var xdate = $(this).val();
                document.getElementById('<%=hdORDate.ClientID %>').value = xdate;

            });
            //-------------Close ORDate-------------//

            //-------------Open MIGRATIONORDate-------------//
            var ordate = document.getElementById('<%=hdORMigrationDate.ClientID %>').value;
            // console.log('ORMigrationDate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdORMigrationDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtMigrationORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtMigrationORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

                <%--var xdate = $('#txtORDate').val();
                document.getElementById('<%=hdORDate.ClientID %>').value = xdate;--%>
            }
            $("#txtMigrationORDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdORMigrationDate.ClientID %>').value = xdate;
                console.log("hdORMigrationDate", document.getElementById('<%=hdORMigrationDate.ClientID %>').value);

            });
            //-------------Close MIGRATIONORDate-------------//

            //-------------Open StartORDate-------------//
            var ordate = document.getElementById('<%=hdSORDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdSORDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtSORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtSORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

                <%--var xdate = $('#txtSORDate').val();
                console.log('xdate', xdate);
                document.getElementById('<%=hdSORDate.ClientID %>').value = xdate;--%>
            }
            $("#txtSORDate").on("change", function () {

                var xdate = $(this).val();
                $("#txtFORDate").val(xdate);
                document.getElementById('<%=hdSORDate.ClientID %>').value = xdate;
                document.getElementById('<%=hdFORDate.ClientID %>').value = xdate;
            });
            //-------------Close StartORDate-------------//

            //-------------Open FinishORDate-------------//
            var ordate = document.getElementById('<%=hdFORDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdFORDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtFORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtFORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtFORDate').val();
                document.getElementById('<%=hdFORDate.ClientID %>').value = xdate;--%>
            }
            $("#txtFORDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdFORDate.ClientID %>').value = xdate;
            });
            //-------------Close FinishORDate-------------//

            //-------------Open StartAnesDate-------------//
            var ordate = document.getElementById('<%=hdSAnesDate.ClientID %>').value;
            //console.log('hdSAnesDate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdSAnesDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtSAnesDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtSAnesDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtSAnesDate').val();
                document.getElementById('<%=hdSAnesDate.ClientID %>').value = xdate;--%>
            }
            $("#txtSAnesDate").on("change", function () {

                var xdate = $(this).val();
                $("#txtFAnesDate").val(xdate);
                document.getElementById('<%=hdSAnesDate.ClientID %>').value = xdate;
                document.getElementById('<%=hdFAnesDate.ClientID %>').value = xdate;
            });
            //-------------Close StartAnesDate-------------//

            //-------------Open FinishAnesDate-------------//
            var ordate = document.getElementById('<%=hdFAnesDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdFAnesDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtFAnesDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtFAnesDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

                <%--var xdate = $('#txtFAnesDate').val();
                document.getElementById('<%=hdFAnesDate.ClientID %>').value = xdate;--%>
            }
            $("#txtFAnesDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdFAnesDate.ClientID %>').value = xdate;

            });
            //-------------Close FinishAnesDate-------------//

            //-------------Open Start BlockDate-------------//
            var ordate = document.getElementById('<%=hdSBlockDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdSBlockDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtSBlockDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtSBlockDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtSBlockDate').val();
                document.getElementById('<%=hdSBlockDate.ClientID %>').value = xdate;--%>
            }
            $("#txtSBlockDate").on("change", function () {

                var xdate = $(this).val();
                $("#txtFBlockDate").val(xdate);
                document.getElementById('<%=hdSBlockDate.ClientID %>').value = xdate;
                document.getElementById('<%=hdFBlockDate.ClientID %>').value = xdate;
            });
            //-------------Close Start BlockDate-------------//

            //-------------Open FinishBlockDate-------------//
            var ordate = document.getElementById('<%=hdFBlockDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdFBlockDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtFBlockDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtFBlockDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtFBlockDate').val();
                document.getElementById('<%=hdFBlockDate.ClientID %>').value = xdate;--%>
            }
            $("#txtFBlockDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdFBlockDate.ClientID %>').value = xdate;

            });
            //-------------Close FinishBlockDate-------------//

            //-------------Open Start RecoveryDate-------------//
            var ordate = document.getElementById('<%=hdSRecoveryDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdSRecoveryDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtSRecoveryDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtSRecoveryDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtSRecoveryDate').val();
                document.getElementById('<%=hdSRecoveryDate.ClientID %>').value = xdate;--%>
            }
            $("#txtSRecoveryDate").on("change", function () {

                var xdate = $(this).val();
                $("#txtFRecoveryDate").val(xdate);
                document.getElementById('<%=hdSRecoveryDate.ClientID %>').value = xdate;
                document.getElementById('<%=hdFRecoveryDate.ClientID %>').value = xdate;
            });
            //-------------Close Start RecoveryDate-------------//

            //-------------Open Finish RecoveryDate-------------//
            var ordate = document.getElementById('<%=hdFRecoveryDate.ClientID %>').value;
            //console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdFRecoveryDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtFRecoveryDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtFRecoveryDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

<%--                var xdate = $('#txtFRecoveryDate').val();
                document.getElementById('<%=hdFRecoveryDate.ClientID %>').value = xdate;--%>
            }
            $("#txtFRecoveryDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdFRecoveryDate.ClientID %>').value = xdate;

            });
            //-------------Close Finish RecoveryDate-------------//

            if ($("#<%=chbORTimeFollow.ClientID %>").is(':checked')) {
                $("#<%=divORTime.ClientID %>").hide();
                $("#<%=lblORTimeH.ClientID %>").hide();
            } else {
                $("#<%=divORTime.ClientID %>").show();
                $("#<%=lblORTimeH.ClientID %>").show();
            }

            //console.log("ORstatus", $("#<%=ddlORStatus.ClientID %>").val());
            //IPD=2, Reserve=4
            if ($("#<%=ddlORStatus.ClientID %>").val() == 2 || $("#<%=ddlORStatus.ClientID %>").val() == 4) {
                $("#<%=ddlAdmitTimeType.ClientID %>").show();
                $("#divroomtype").show();
            }
            else {
                $("#<%=ddlAdmitTimeType.ClientID %>").hide();
                $("#divroomtype").hide();
            }
            //
            if ($("#<%=ddlAnesthesiaSign.ClientID %>").val() == "1" || $("#<%=ddlAnesthesiaSign.ClientID %>").val() == "2") {
                $("#<%=ddlAnesthesiaType2.ClientID %>").show();
            }
            else {
                $("#<%=ddlAnesthesiaType2.ClientID %>").hide();
            }


            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            $('.nav-tabs li:eq(0) a').tab('show');

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



    </script>
    <script type="text/javascript">

        function showModalCheckTime() {
            $("#ModalCheckTime").modal('show');
        }
        function showModalCheckDigi() {
            $("#ModalCheckDigi").modal('show');
        }
        function showModalSide() {
            $("#ModalSide").modal('show');
        }

        function showModalProcedure() {
            $("#modalSetupProcedure").modal('show');
        }
        function closeModalProcedure() {
            $("#modalSetupProcedure").modal('hide');
        }
        function showModalPopProcedureSelection() {
            $("#modalPopProcedureSelection").modal('show');
        }
        function closeModalPopProcedureSelection() {
            $("#modalPopProcedureSelection").modal('hide');
        }        
        function showModalImplant() {
            $("#modalImplant").modal('show');
        }
        function closeModalImplant() {
            $("#modalImplant").modal('hide');
        }

        function showModalImplantImage() {
            $("#modalImgImplant").modal('show');
        }
        function closeModalImplantImage() {
            $("#modalImgImplant").modal('hide');
        }

        function showpreview(input) {

            var lastChar = input.id.substr(input.id.length - 1);
            if (input.files && input.files[0]) {
                console.log('files', input.files[0]);
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#imgpreview' + lastChar).css('visibility', 'visible');
                    $('#imgpreview' + lastChar).attr('src', e.target.result);
                }
                reader.readAsDataURL(input.files[0]);
            }

        }

    </script>
    <script>

        function showimg1() {
            var img = document.getElementById('<%= img1.ClientID %>').src;


            html = "<HTML><HEAD><TITLE>Photo</TITLE>"
                + "</HEAD><BODY LEFTMARGIN=0 "
                + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                + "<IMG src='"
                + img
                + "' BORDER=0 NAME=image "
                + "onload='window.resizeTo(document.image.width,document.image.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function showimg2() {
            var img = document.getElementById('<%= img2.ClientID %>').src;


            html = "<HTML><HEAD><TITLE>Photo</TITLE>"
                + "</HEAD><BODY LEFTMARGIN=0 "
                + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                + "<IMG src='"
                + img
                + "' BORDER=0 NAME=image "
                + "onload='window.resizeTo(document.image.width,document.image.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }
        function showimg3() {
            var img = document.getElementById('<%= img3.ClientID %>').src;


            html = "<HTML><HEAD><TITLE>Photo</TITLE>"
                + "</HEAD><BODY LEFTMARGIN=0 "
                + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                + "<IMG src='"
                + img
                + "' BORDER=0 NAME=image "
                + "onload='window.resizeTo(document.image.width,document.image.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }
        function showimg4() {
            var img = document.getElementById('<%= img4.ClientID %>').src;


            html = "<HTML><HEAD><TITLE>Photo</TITLE>"
                + "</HEAD><BODY LEFTMARGIN=0 "
                + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                + "<IMG src='"
                + img
                + "' BORDER=0 NAME=image "
                + "onload='window.resizeTo(document.image.width,document.image.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }
        function showimg5() {
            var img = document.getElementById('<%= img5.ClientID %>').src;


            html = "<HTML><HEAD><TITLE>Photo</TITLE>"
                + "</HEAD><BODY LEFTMARGIN=0 "
                + "MARGINWIDTH=0 TOPMARGIN=0 MARGINHEIGHT=0><CENTER>"
                + "<IMG src='"
                + img
                + "' BORDER=0 NAME=image "
                + "onload='window.resizeTo(document.image.width,document.image.height)'>"
                + "</CENTER>"
                + "</BODY></HTML>";
            popup = window.open('', 'image', 'toolbar=0,location=0,directories=0,menuBar=0,scrollbars=0,resizable=1');
            popup.document.open();
            popup.document.write(html);
            popup.document.focus();
            popup.document.close();
        }

        function isDigit(evt, txt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            var c = String.fromCharCode(charCode);
            //console.log('evt', evt);

            if (txt.length > 3) {
                return false;
            }
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function setSORTime(timeStart) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeStart = timeStart.replace(':', '');
            if (timeStart != "" && checkDigit(timeStart)) {
                $('#' + '<%=txtSORTime.ClientID %>').val("");
                return;
            }
            var itimeStart = formatted_string('0000', timeStart, 'r')
            var ires = itimeStart.substring(0, 2) + ":" + itimeStart.substring(2, 4);
            var timeEnd = $("#<%=txtFORTime.ClientID%>").val();
            timeEnd = timeEnd.replace(':', '');
            if (checkTime(itimeStart, timeEnd)) {
                $('#' + '<%=txtSORTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtSORTime.ClientID %>').val(ires); }
        }
        function setSAnesTime(timeStart) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeStart = timeStart.replace(':', '');
            if (timeStart != "" && checkDigit(timeStart)) {
                $('#' + '<%=txtSAnesTime.ClientID %>').val("");
                return;
            }
            var itimeStart = formatted_string('0000', timeStart, 'r')
            var ires = itimeStart.substring(0, 2) + ":" + itimeStart.substring(2, 4);
            var timeEnd = $("#<%=txtFAnesTime.ClientID%>").val();
            timeEnd = timeEnd.replace(':', '');
            if (checkTime(itimeStart, timeEnd)) {
                $('#' + '<%=txtSAnesTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtSAnesTime.ClientID %>').val(ires); }
        }

        function setSBlockTime(timeStart) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeStart = timeStart.replace(':', '');
            if (timeStart != "" && checkDigit(timeStart)) {
                $('#' + '<%=txtSBlockTime.ClientID %>').val("");
                return;
            }
            var itimeStart = formatted_string('0000', timeStart, 'r')
            var ires = itimeStart.substring(0, 2) + ":" + itimeStart.substring(2, 4);
            var timeEnd = $("#<%=txtFBlockTime.ClientID%>").val();
            timeEnd = timeEnd.replace(':', '');
            if (checkTime(itimeStart, timeEnd)) {
                $('#' + '<%=txtSBlockTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtSBlockTime.ClientID %>').val(ires); }
        }

        function setSRecoveryTime(timeStart) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeStart = timeStart.replace(':', '');
            if (timeStart != "" && checkDigit(timeStart)) {
                $('#' + '<%=txtSRecoveryTime.ClientID %>').val("");
                return;
            }
            var itimeStart = formatted_string('0000', timeStart, 'r')
            var ires = itimeStart.substring(0, 2) + ":" + itimeStart.substring(2, 4);
            var timeEnd = $("#<%=txtFRecoveryTime.ClientID%>").val();
            timeEnd = timeEnd.replace(':', '');
            if (checkTime(itimeStart, timeEnd)) {
                $('#' + '<%=txtSRecoveryTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtSRecoveryTime.ClientID %>').val(ires); }
        }

        function setFORTime(timeEnd) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeEnd = timeEnd.replace(':', '');
            if (timeEnd != "" && checkDigit(timeEnd)) {
                $('#' + '<%=txtFORTime.ClientID %>').val("");
                return;
            }
            var itimeEnd = formatted_string('0000', timeEnd, 'r')
            var ires = itimeEnd.substring(0, 2) + ":" + itimeEnd.substring(2, 4);
            var timeStart = $("#<%=txtSORTime.ClientID%>").val();
            timeStart = timeStart.replace(':', '');
            if (checkTime(timeStart, itimeEnd)) {
                $('#' + '<%=txtFORTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtFORTime.ClientID %>').val(ires); }
        }
        function setFAnesTime(timeEnd) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeEnd = timeEnd.replace(':', '');
            if (timeEnd != "" && checkDigit(timeEnd)) {
                $('#' + '<%=txtFAnesTime.ClientID %>').val("");
                return;
            }
            var itimeEnd = formatted_string('0000', timeEnd, 'r')
            var ires = itimeEnd.substring(0, 2) + ":" + itimeEnd.substring(2, 4);
            var timeStart = $("#<%=txtSAnesTime.ClientID%>").val();
            timeStart = timeStart.replace(':', '');
            if (checkTime(timeStart, itimeEnd)) {
                $('#' + '<%=txtFAnesTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtFAnesTime.ClientID %>').val(ires); }
        }
        function setFBlockTime(timeEnd) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeEnd = timeEnd.replace(':', '');
            if (timeEnd != "" && checkDigit(timeEnd)) {
                $('#' + '<%=txtFBlockTime.ClientID %>').val("");
                return;
            }
            var itimeEnd = formatted_string('0000', timeEnd, 'r')
            var ires = itimeEnd.substring(0, 2) + ":" + itimeEnd.substring(2, 4);
            var timeStart = $("#<%=txtSBlockTime.ClientID%>").val();
            timeStart = timeStart.replace(':', '');
            if (checkTime(timeStart, itimeEnd)) {
                $('#' + '<%=txtFBlockTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtFBlockTime.ClientID %>').val(ires); }
        }
        function setFRecoveryTime(timeEnd) {
            if (timeStart == "" || timeEnd == "")
                return;
            timeEnd = timeEnd.replace(':', '');
            if (timeEnd != "" && checkDigit(timeEnd)) {
                $('#' + '<%=txtFRecoveryTime.ClientID %>').val("");
                return;
            }
            var itimeEnd = formatted_string('0000', timeEnd, 'r')
            var ires = itimeEnd.substring(0, 2) + ":" + itimeEnd.substring(2, 4);
            var timeStart = $("#<%=txtSRecoveryTime.ClientID%>").val();
            timeStart = timeStart.replace(':', '');
            if (checkTime(timeStart, itimeEnd)) {
                $('#' + '<%=txtFRecoveryTime.ClientID %>').val("");
            }
            else { $('#' + '<%=txtFRecoveryTime.ClientID %>').val(ires); }
        }
        function checkTime(timestart, timeend) {
            if (timestart == "" || timeend == "") {
                return false;
            }
            else if (timestart > timeend) {
                //เวลาเริ่มห้ามมากกว่าเวลาสิ้นสุด
                showModalCheckTime();
                return true;
            }
        }
        function checkDigit(time) {

            if (!(Math.floor(time) == time && $.isNumeric(time))) {
                showModalCheckDigi();
                return true;
            }
        }
        function formatted_string(pad, user_str, pad_pos) {
            if (typeof user_str === 'undefined')
                return pad;
            if (pad_pos == 'l') {
                return (pad + user_str).slice(-pad.length);
            }
            else {
                return (user_str + pad).substring(0, pad.length);
            }
        }
        function reSOR(time) {
            $('#' + '<%=txtSORTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtSORTime.ClientID %>').select();
        }
        function reSAnes(time) {
            $('#' + '<%=txtSAnesTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtSAnesTime.ClientID %>').select();
        }
        function reSBlock(time) {
            $('#' + '<%=txtSBlockTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtSBlockTime.ClientID %>').select();
        }
        function reSRecovery(time) {
            $('#' + '<%=txtSRecoveryTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtSRecoveryTime.ClientID %>').select();
        }

        function reFOR(time) {
            $('#' + '<%=txtFORTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtFORTime.ClientID %>').select();
        }
        function reFAnes(time) {
            $('#' + '<%=txtFAnesTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtFAnesTime.ClientID %>').select();
        }
        function reFBlock(time) {
            $('#' + '<%=txtFBlockTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtFBlockTime.ClientID %>').select();
        }
        function reFRecovery(time) {
            $('#' + '<%=txtFRecoveryTime.ClientID %>').val(time.replace(':', ''));
            $('#' + '<%=txtFRecoveryTime.ClientID %>').select();
        }
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <script>
        function SetMigationORDate() {
            //-------------Open MIGRATIONORDate-------------//
            var ordate = document.getElementById('<%=hdORMigrationDate.ClientID %>').value;
            // console.log('ORMigrationDate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdORMigrationDateEn.ClientID %>').value;
                //console.log('ordateEn', ordateEn);
                $('#txtMigrationORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtMigrationORDate').datepicker({
                    format: 'd/m/yyyy',
                    todayBtn: false,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker();  //กำหนดเป็นวันปัจุบัน

                <%--var xdate = $('#txtORDate').val();
                document.getElementById('<%=hdORDate.ClientID %>').value = xdate;--%>
            }
            $("#txtMigrationORDate").on("change", function () {

                var xdate = $(this).val();
                document.getElementById('<%=hdORMigrationDate.ClientID %>').value = xdate;
                //console.log("hdORMigrationDate", document.getElementById('<%=hdORMigrationDate.ClientID %>').value);

            });
        }
    </script>
</asp:Content>
