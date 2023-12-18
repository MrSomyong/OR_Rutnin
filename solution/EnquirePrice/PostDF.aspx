<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostDF.aspx.cs" Inherits="solution.EnquirePricePostDF.PostDF" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/css/select2.min.css" rel="stylesheet" />
    <link href="../assets/plugins/datetimepicker/css/redmond/jquery-ui.css" rel="stylesheet" />
    <%--<link href="../assets/plugins/datetimepicker/css/redmond/jquery-ui-timepicker-addon.css" rel="stylesheet" />--%>
    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />--%>
    <link href="../assets/plugins/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.css" rel="stylesheet" />

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

        .select2 {
            width: 100% !important;
        }
        #MainContent_gvOPDTreatmentList tr td ,
        #MainContent_gvOPDTreatmentList tr th ,
        #MainContent_gvTreatmentItem tr td ,
        #MainContent_gvTreatmentItem tr th 
        {
            padding : 5px 8px ;
        }
        .title {
            border-bottom: 1pt solid #33b5e5;
            margin-bottom: 10px;
            margin-top:7px;
        }
        .title h5 {
            margin-bottom: .2rem;
        }
        .modal-header {
            background-color: #33b5e5;
            color:#fff;
            box-shadow: 0 2px 5px 0 rgba(0,0,0,.16), 0 2px 10px 0 rgba(0,0,0,.12);
            border: 0;
        }
        .close {
            float: right;
            font-size: 1.5rem;
            font-weight: 700;
            line-height: 1;
            color: #fff;
            text-shadow: 0 1px 0 #fff;
            opacity: .5;
        }
        .rightText {
            text-align: right;
        }
        a.aspNetDisabled {
            color : #fff !important;
        }

        body {
            line-height: 1;
        }

    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs--> 
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/EnquirePrice/">Enquiry Price With Patient List</a>
            </li>
            <li class="breadcrumb-item active">Enquiry Doctor Treatment</li>
        </ol>
         <div class="row">
            <div class="col-md-6">
                <h5><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">Patient Visit Detail/Patient Information</span></h5>
            </div>

                          <div class="col-md-6">
                              <div class="row justify-content-end float-right">
                                    <asp:PlaceHolder ID="phHyperLink" runat="server"></asp:PlaceHolder>
                                  </div>
                        </div>
<%--            <div class="col-md-6">

                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    
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
            </div>--%>
        </div>
        <hr />
        <div class="form-group">

            <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
            </div>
            <%--<div class="row float-right">
                <div class="menu-button mr-2">
                    <div class="row justify-content-end my-3 float-right">
                        <div class="col-12">
                            <asp:PlaceHolder ID="phHyperLink" runat="server"></asp:PlaceHolder>
                        </div>
                    </div>
                </div>
            </div>--%>
<%--            <div class="clearfix"></div>--%>
            <div class="row">
                <div class="col-md-2">
                    <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%">--%>
                    <asp:Image runat="server" ID="imgPatient" ImageUrl="../Images/17241-200.png" CssClass="img-thumbnail" Style="width: 150px" />
                </div>
                <div class="col-md-10">
                 
                    <div class="title">
                        <h8 class=""></h8>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>HN : </label>
                                <asp:Label ID="lblHN" runat="server" Font-Bold="true"></asp:Label>


                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label>Patient Name : </label>
                                <asp:Label ID="lblPatientName" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>

                          <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>ID Card : </label>
                                <asp:Label ID="lblIDCard" runat="server" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>Nationality : </label>
                                <asp:Label ID="lblNationality" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-2 pr-1">
                            <div class="form-group">
                                <label>Gender : </label>
                                <asp:Label ID="lblGender" runat="server" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-4 pr-1">
                            <div class="form-group">
                                <label>Date of Birth : </label>
                                <asp:Label ID="lblDateOfBirth" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>Age : </label>
                                <asp:Label ID="lblAge" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>Pateint Type : </label>
                                <asp:Label ID="lblPatientType" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>
                    </div>
                   
                    <%--<div class="title">
                        <h8>Visit Details</h8>
                    </div>--%>
                    <div class="row">
                        <div class="col-md-2 pr-1">
                            <div class="form-group">
                                <label>VN/Pres: </label>
                                <asp:Label ID="lblVN" runat="server" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-2 pr-1">
                            <div class="form-group">
                                <label>Visit Date: </label>
                                <asp:Label ID="lblVisitDate" runat="server" Font-Bold="true"></asp:Label>
                            </div>
                        </div>


                 
                        <div class="col-md-2 pr-1">
                            <div class="form-group">
                                <label>Clinic: </label>
                                <asp:Label ID="lblClinic" runat="server" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>Doctor: </label>
                                <asp:Label ID="lblDoctor" runat="server" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdDoctorCode" runat="server" />
                                <asp:HiddenField ID="hdRightCode" runat="server" />
                                <asp:HiddenField ID="hdFixRate" runat="server" />
                            </div>
                        </div>
                        <div class="col-md-3 pr-1">
                            <div class="form-group">
                                <label>Right: </label>
                                <asp:Label ID="lblRight" runat="server" Font-Bold="true"></asp:Label>

                            </div>
                        </div>
                    </div>

                    <div class="row">
                                <%--<div class="menu-button">
                                <div class="row justify-content-end my-1 float-right">--%>
                                    <div class="col-12">
                                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/Main.aspx">
                                             <span class="text">Main</span>
                                            </asp:HyperLink>
                                        <asp:HyperLink ID="lnkTreatment" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostTreatment.aspx">
                                             <span class="text">Treatment</span>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="lnkDF" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostDF.aspx">
                                             <span class="text">DF</span>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostMedicine.aspx">
                                             <span class="text">Medicine</span>
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostGroupMethod.aspx">
                                             <span class="text">Group</span>
                                        </asp:HyperLink>
                                    </div>
                                <%--</div>
                            </div>--%>
                </div>

                </div>
            </div>
<%--            <div class="menu-button">
                <div class="row justify-content-end my-3 float-right">
                    <div class="col-12">
                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/Main.aspx">
                             <span class="text">Main</span>
                            </asp:HyperLink>
                        <asp:HyperLink ID="lnkTreatment" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostTreatment.aspx">
                             <span class="text">Treatment</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkDF" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostDF.aspx">
                             <span class="text">DF</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostMedicine.aspx">
                             <span class="text">Medicine</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/EnquirePrice/PostGroupMethod.aspx">
                             <span class="text">Group</span>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>--%>
            <div class="clearfix"></div>
            <div class="panel">
                <ul class="nav nav-tabs">
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#treatment" style="width: 200px;background-color: lightyellow;">Enq : doctor treatment</a>
                    </li>
              
                </ul>
                <div class="tab-content">
                    <div id="treatment" class="tab-pane fade in active">
                        <%--<br />--%>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                    <div class="alert alert-primary">
                                                        <div class="row">
                                                            <div class="col-md-12">
                                                                <div class="form-horizontal">
                                                                    <div class="control-group row-fluid form-inline">

                                                                        <label class="control-label pull-right" style="min-width:100px;">DF : </label>
                                                                        <div class="col-md-9">
                                                                            <asp:DropDownList ID="ddlDF" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                        </div>
                                                                        <asp:Button ID="btnAddTreatment" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAddTreatment_Click" />
                                                                         <div class="row col-md-12 control-group row-fluid form-inline mb-0 mt-2">
                                                                         </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12">
                                                                <div class="control-group row-fluid form-inline ">
                                                                      <div class="col-auto" style="min-width:100px;">
                                                                                <label class="control-label pull-right">Doctor : </label>
                                                                            </div>
                                                                            <div class="col-md-9">
                                                                                <asp:DropDownList ID="ddlDoctorList" runat="server" CssClass="form-control d-block"></asp:DropDownList>
                                                                            </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row" style="padding-bottom: 50px;">
                                                        <div class="col-md-12">
                                                            <div style="overflow-y: scroll; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">

                                                                <asp:GridView ID="gvOPDTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX,CHARGECODE,TREATMENTCODE,TREATMENTENTRYSTYLE,IsDeleted"
                                                                    OnRowCommand="gvOPDTreatmentList_RowCommand"
                                                                    OnRowDataBound="gvOPDTreatmentList_RowDataBound">
                                                                    <Columns>
                                                                    
                                                                         <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteDF" runat="server" Text="Del" CommandName="del" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn  btn-primary btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnEditDF" runat="server" Text="Edit" CommandName="editTreatment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                          <asp:TemplateField HeaderText="Type">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center"/>
                                                                            <ItemTemplate> 
                                                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Presc#" DataField="SUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Item#" DataField="SUBSUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="TreatmentCode" DataField="TREATMENTINFO">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Charge" DataField="ActivityName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                       <asp:BoundField HeaderText="Price" DataField="CHARGEAMT" DataFormatString="{0:#,##0.00}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Qty" DataField="QTY"> 
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Amount" DataField="AMT" DataFormatString="{0:#,##0.00}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Doctor" DataField="DOCTORNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Clinic" DataField="CLINICNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Entry by" DataField="ENTRYBYUSERNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Entry Date" DataField="MAKEDATETIME" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Remark" DataField="REMARKS">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                       
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                    <HeaderStyle CssClass="table-warning" />
                                                                </asp:GridView>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddTreatment" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                                                    
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                  
                </div>
            </div>






        </div>

    </div>
    <div class="modal fade bd-example-modal-md" id="modalEditTreatment" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width:700px;">
            <div class="modal-content">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <div class="modal-header">
                    <h6 class="modal-title" id="htmlTitleUpdateTreatment" runat="server">Edit DF Item</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row mb-2">
                                        <div class="col-4">
                                            <label class="pull-right">DF Item : </label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblTreatmentName"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdTreatmentCode" />
                                            <asp:HiddenField runat="server" ID="hdChargeCode" />
                                            <%--<asp:HiddenField runat="server" ID="hdVN" />--%>
                                            <asp:HiddenField runat="server" ID="hdTreatmentStyle" />
                                            <asp:HiddenField runat="server" ID="hdSubSuffix" />
                                            <asp:HiddenField runat="server" ID="hdChargeAMT" />
                                            <asp:HiddenField runat="server" ID="hdZeroPrice" />
                                            <asp:HiddenField runat="server" ID="hdTime01" />
                                            <asp:HiddenField runat="server" ID="hdTime02" />
                                            <asp:HiddenField runat="server" ID="hdTime03" />
                                            <asp:HiddenField runat="server" ID="hdTime04" />
                                            <asp:HiddenField runat="server" ID="hdTime05" />
                                            <asp:HiddenField runat="server" ID="hdTime06" />
                                        </div>
                                    </div>
                                     <div class="row mb-2">
                                        <div class="col-4">
                                            <label class="pull-right">Doctor : </label>
                                        </div>
                                        <div class="col-8">
                                             <asp:DropDownList ID="ddlEditDoctorList" runat="server" CssClass="form-control d-block" AutoPostBack="false"></asp:DropDownList>
                                        </div>
                                     </div>
                                     <div class="row mb-2" runat="server" id="divOrderType">
                                        <div class="col-4">
                                            <label class="pull-right">Order Type : </label>
                                        </div>
                                        <div class="col-3">
                                             <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                        </div>
                                      
                                    </div>
                                    <div class="row mb-2" runat="server" id="divChargeAMT">
                                        <div class="col-4">
                                            <label class="pull-right">Charge AMT : </label>
                                        </div>
                                        <div class="col-3">
                                             <asp:TextBox ID="txtChargeAMT" runat="server" CssClass="form-control form-control-sm"  onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"  Enabled="false" ></asp:TextBox>
                                            
                                        </div>
                                      
                                    </div>
                                               
                                                <div class="row mb-2" runat="server" id="divQTY">
                                                    <div class="col-4">
                                                        <label class="pull-right">Qty : </label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control form-control-sm" onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row mb-2" runat="server" id="divTimeBetween">
                                                    <div class="col-4">
                                                        <label class="pull-right">Time Between : </label>
                                                    </div>
                                                    <div class="row  col">
                                                        <div class="col-6">
                                                            <div class="form-inline">
                                                                <asp:TextBox ID="txtStartDateTime" runat="server" CssClass="form-control form-control-sm datepicker col-8"></asp:TextBox>
                                                                <asp:TextBox ID="txtStartTime" runat="server" CssClass="form-control form-control-sm col-4"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="col-6">
                                                            <div class="form-inline">
                                                                <asp:HiddenField ID="hfTimeType" runat="server" />
                                                                <asp:TextBox ID="txtEndDateTime" runat="server" CssClass="form-control form-control-sm datepicker col-8" onFocus="this.select()"></asp:TextBox>
                                                                <asp:TextBox ID="txtEndTime" runat="server" CssClass="form-control form-control-sm col-4"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row mb-2" runat="server" id="divAMT">
                                                    <div class="col-4">
                                                        <label class="pull-right">Amt. : </label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control form-control-sm rightText" onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                 <div class="row mb-2">
                                                    <div class="col-4">
                                                        <label class="pull-right">Remark : </label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                                    </div>
                                        
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdateTreatment" runat="server" CssClass="btn btn-primary btn-sm"   Text="Update"  OnClick="btnUpdateTreatment_Click"  ></asp:Button>
                                <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancel</button>
                            </div>
                        </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <script src="https://code.jquery.com/jquery-3.4.0.js"></script>
    <script src="../../Scripts/select2.min.js"></script>
    <script src="../../Scripts/notify.js"></script>

    <script type="text/javascript">
        function showModalTreatment() {
            $("#modalEditTreatment").modal('show');
        }
        function isNumberOneDecimal(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var index = $(element).val().indexOf('.');
            var len = $(element).val().length;
            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57)) {

                if (event.keyCode === 13) {
                    //alert("Button code executed.");
                    //document.getElementById("btnUpdateTreatment").onclick();

                    var button = document.getElementById("<%= btnUpdateTreatment.ClientID %>");
                    button.click();

                }

                return false;
            }
            else if (index > 0) {
                var CharAfterdot = (len + 1) - index;
                if (CharAfterdot > 2) {

                    if (event.keyCode === 13) {
                    //alert("Button code executed.");
                    //document.getElementById("btnUpdateTreatment").onclick();

                    var button = document.getElementById("<%= btnUpdateTreatment.ClientID %>");
                    button.click();

                }

                    return false;
                }
            }

            return true;
        }
        $(document).ready(function () {
          
            $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');
            $("#<%=ddlDF.ClientID%>").select2({

              placeholder: "Select Treatment",
              width: '100%',
              allowClear: true

            });

            $("#<%=ddlDoctorList.ClientID%> , #<%=ddlEditDoctorList.ClientID%> ").select2({

                placeholder: "-Select Doctor-",
                width: '100%',
                allowClear: true

            });

            $('#<%=ddlDF.ClientID%>').on("click change keydown", function (e) {
                if (event.keyCode === 13 && $(this).val() != "") {
                    $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                    $('#<%=btnAddTreatment.ClientID %>')[0].click();
                }
                else if ($(this).val() != "") {
                    $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                }
                else
                    $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');

              });

              $(".nav-tabs a").click(function () {
                  $(this).tab('show');
              });

            $('.nav-tabs li:eq(0) a').tab('show');

            if ($('#<%=ddlDF.ClientID%>').is(':disabled') == false) {
                setTimeout( function() {
                   $('[id*=ddlDF]').select2('open');
                }, 1000);
            }

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');

                $("#<%=ddlDF.ClientID%>").select2({

                    placeholder: "Select DF",
                    width: '100%',
                    allowClear: true

                });

                $("#<%=ddlDoctorList.ClientID%> , #<%=ddlEditDoctorList.ClientID%> ").select2({

                    placeholder: "-Select Doctor-",
                    width: '100%',
                    allowClear: true

                });
              
                $('#<%=ddlDF.ClientID%>').on("click change keydown", function (e) {
                    if (event.keyCode === 13 && $(this).val() != "") {
                        $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                    $('#<%=btnAddTreatment.ClientID %>')[0].click();
                }
                else if ($(this).val() != "") {
                    $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                }
                else
                    $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');

                });

                $(".nav-tabs a").click(function () {
                    $(this).tab('show');
                });

                $('.nav-tabs li:eq(0) a').tab('show');

            });
        });

    </script>

    <script src="../assets/plugins/moment/js/moment.min.js"></script>
    <script src="../assets/plugins/moment/js/moment-with-locales.min.js"></script>

    <script src="../assets/plugins/datetimepicker/scripts/jquery-1.6.min.js"></script>
   
    <script src="../assets/plugins/datetimepicker/scripts/jquery-ui.min.js"></script>
    <script src="../assets/plugins/datetimepicker/scripts/jquery.ui.datepicker-th.js"></script>
    <%--<script src="../assets/plugins/datetimepicker/scripts/jquery.ui.datetimepicker.js"></script> --%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>--%>
    <script src="../assets/plugins/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>
    <script src="../assets/plugins/datetimepicker/scripts/jquery.ui.datepicker.ext.be.js"></script>

    <script type="text/javascript">
		var jQuery_1_6 = $.noConflict(true);
	</script>
    <script type="text/javascript">
       

        (function($){
             $(document).ready(function () {
         

                 $("#<%=txtStartDateTime.ClientID%> , #<%=txtEndDateTime.ClientID%> , #<%=txtStartTime.ClientID%> , #<%=txtEndTime.ClientID%>").change(function () {

                     const dateTimeStart = moment($("#<%=txtStartDateTime.ClientID%>").val() + " " + $("#<%=txtStartTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
                     const dateTimeEnd = moment($("#<%=txtEndDateTime.ClientID%>").val() + " " + $("#<%=txtEndTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
                     const timeType = +($("#<%=hfTimeType.ClientID%>").val());
                     const chargeAMT = +($("#<%=hdChargeAMT.ClientID%>").val());
                     var duration = moment.duration(dateTimeEnd.diff(dateTimeStart));
                     let durationTime = timeType == 1 ? Math.ceil(duration.asMinutes()) : timeType == 2 ? Math.ceil(duration.asHours()) : timeType == 3 ? Math.ceil(duration.asDays()) : 0;
                     let amt = 0.00;
                     delay(function () {
                         if (+($("#<%=hdTime01.ClientID%>").val()) == 0 &&
                             +($("#<%=hdTime02.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime03.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime04.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime05.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime06.ClientID%>").val()) == 0) {

                                amt = chargeAMT * durationTime;

                            }
                            else {
                                for (let i = 1; i <= durationTime; i++) {

                                    switch (i) {
                                        case 1: amt += +($("#<%=hdTime01.ClientID%>").val());
                                            break;
                                        case 2: amt += +($("#<%=hdTime02.ClientID%>").val());
                                            break;
                                        case 3: amt += +($("#<%=hdTime03.ClientID%>").val());
                                            break;
                                        case 4: amt += +($("#<%=hdTime04.ClientID%>").val());
                                            break;
                                        case 5: amt += +($("#<%=hdTime05.ClientID%>").val());
                                            break;
                                        default: amt += +($("#<%=hdTime06.ClientID%>").val());
                                         break;
                                 }
                             }
                         }

                         $("#<%=txtAmt.ClientID%>").val(amt);

                     }, 500, this);



                 });

                    var delay = (function () {
                        var timer = 0;
                        return function (callback, ms) {
                            clearTimeout(timer);
                            timer = setTimeout(callback, ms);
                        };
                    })();

               
            });
           

        })(jQuery_1_6);
         var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {
            (function ($) {
                $(document).ready(function () {
                    $(".datepicker").datetimepicker({
                        changeMonth: false,
                        changeYear: false,
                        showButtonPanel: true,
                        dateFormat: 'dd/mm/yy',
                        timeFormat: '',
                        showTime : false,
                        showHour: false,
	                    showMinute: false,
                        isBE: true,
                        autoConversionField: false
                    });
      

                    $("#<%=txtStartTime.ClientID%> , #<%=txtEndTime.ClientID%>").timepicker(
	                    {   
                        
                        timeFormat: "HH:mm",
                        controlType: 'select',
	                    oneLine: true
                            
	                    }
                    );


                    $("#<%=txtStartDateTime.ClientID%> , #<%=txtEndDateTime.ClientID%> , #<%=txtStartTime.ClientID%> , #<%=txtEndTime.ClientID%>").change(function () {
                        
                        const dateTimeStart = moment($("#<%=txtStartDateTime.ClientID%>").val()+" "+$("#<%=txtStartTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
                        const dateTimeEnd = moment($("#<%=txtEndDateTime.ClientID%>").val()+" "+$("#<%=txtEndTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
                        const timeType = +($("#<%=hfTimeType.ClientID%>").val());
                        const chargeAMT = +($("#<%=hdChargeAMT.ClientID%>").val());
                        var duration = moment.duration(dateTimeEnd.diff(dateTimeStart));
                        let durationTime = timeType == 1 ? Math.ceil(duration.asMinutes()) : timeType == 2 ? Math.ceil(duration.asHours()) : timeType == 3 ? Math.ceil(duration.asDays()) : 0 ;
                        let amt = 0.00;
                        delay(function () {
                            if (+($("#<%=hdTime01.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime02.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime03.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime04.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime05.ClientID%>").val()) == 0 &&
                                +($("#<%=hdTime06.ClientID%>").val()) == 0) {

                                amt = chargeAMT * durationTime;

                            }
                            else {
                                for (let i = 1; i <= durationTime; i++) {

                                    switch (i) {
                                        case 1: amt += +($("#<%=hdTime01.ClientID%>").val());
                                            break;
                                        case 2: amt += +($("#<%=hdTime02.ClientID%>").val());
                                            break;
                                        case 3: amt += +($("#<%=hdTime03.ClientID%>").val());
                                            break;
                                        case 4: amt += +($("#<%=hdTime04.ClientID%>").val());
                                            break;
                                        case 5: amt += +($("#<%=hdTime05.ClientID%>").val());
                                            break;
                                        default: amt += +($("#<%=hdTime06.ClientID%>").val());
                                            break;
                                    }
                                }
                            }
                            $("#<%=txtAmt.ClientID%>").val(amt);

                        }, 500, this);


                    });

                    var delay = (function () {
                        var timer = 0;
                        return function (callback, ms) {
                            clearTimeout(timer);
                            timer = setTimeout(callback, ms);
                        };
                    })();


                 });
             })(jQuery_1_6);
        });
        
    </script>
</asp:Content>