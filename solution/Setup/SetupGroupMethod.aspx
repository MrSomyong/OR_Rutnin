<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetupGroupMethod.aspx.cs" Inherits="solution.Setup.SetupGroupMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/css/select2.min.css" rel="stylesheet" />

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
        #MainContent_gvTreatmentList tr td ,
        #MainContent_gvTreatmentList tr th ,
        #MainContent_gvSetupGroupMethodList tr td ,
        #MainContent_gvSetupGroupMethodList tr th 
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
        .selected  {
            background-color: #7f8fa6;
            color:#fff;
        }
        .selected  tr:hover {
            color: #fff;
            background-color: #000 ;
           
        }

        .selected tbody tr:hover td {
            background-color: transparent;
        }
        .table-striped tbody tr.selected:nth-of-type(odd) {
                background-color: #7f8fa6;
                color:#fff;
        }
    
        label[for*=cbTMAutoTick] , label[for*=cbTMEditAutoTick],
        label[for*=cbMedAutoTick] , label[for*=cbMedEditAutoTick]{ display: inline-block; margin-left : 5px;}
        .col-8 .form-check-input {
            margin-left:unset;
        }
        .wrap {
           white-space: normal !important;  
           word-break: break-all !important;
        }

    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs--> 
        <ol class="breadcrumb">
            
            <li class="breadcrumb-item active">Setup Group Method</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-cogs"></i><span class="nav-link-text">Setup Group Method</span></h4>
            </div>
            <div class="col-md-6">

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
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="control-group row-fluid form-inline alert alert-primary">
                                <div class="text-right">
                                    <label class="pull-right">Group Method Code : </label>
                                </div>
                                <div class="col-md-2 input-group">
                                    <asp:TextBox ID="txtGroupMethodCode" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>

                                <div class="text-right">
                                    <label class="pull-right">Group Method Name : </label>
                                </div>
                                <div class="col-md-4 input-group">
                                    <asp:TextBox ID="txtGroupMethodName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-success btn-sm mr-2" OnClick="btnAdd_Click" />
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-warning btn-sm" Visible="false"  OnClick="btnClear_Click"/>
                                </div>

                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList"   />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <hr />

        <div class="row" style="padding-top: 10px;">

            <div class="col-md-5">
                <div style="height: 350px;" class="table-responsive text-nowrap">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                        <asp:GridView ID="gvSetupGroupMethodList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered pre-scrollable"
                            DataKeyNames="GroupMethodID,GroupMethodCode,GroupMethodName"
                            OnRowCommand="gvSetupGroupMethodList_RowCommand" >
                            <columns>
                                <asp:TemplateField>
                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteGroupMethod" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 
                                    <asp:ButtonField  CommandName="EditGroupMethod" Text="Edit" ControlStyle-CssClass="btn  btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField HeaderText="Code" DataField="GroupMethodCode">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Name" DataField="GroupMethodName">
                                        <HeaderStyle CssClass="text-center" Width="100%" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                </columns>
                            <emptydatarowstyle cssclass="alert-secondary text-center" />
                            <headerstyle cssclass="table-info" />
                        </asp:GridView>
                            </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-7">
                 <div class="panel">
                <ul class="nav nav-tabs">
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#treatment" style="width: 120px">Treatment</a>
                    </li>
                    
                      <li class="nav-item">
                        <a class="nav-link text-center" href="#medicine" style="width: 120px">Medicine</a>
                    </li>
                     <li class="nav-item ">
                        <a class="nav-link text-center" href="#doctor" style="width: 120px">Doctor</a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#computer" style="width: 120px">Computer</a>
                    </li>
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#clinic" style="width: 120px">Clinic</a>
                    </li>
                </ul>
                <div class="tab-content">
                    <div id="treatment" class="tab-pane fade in active pt-3 pb-3">
                      <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>--%>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                        <%--    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" >
                                                <ContentTemplate>--%>
                                            <div class="alert alert-primary py-3">
                                                <div class="row">
                                                    <div class="col-md-12">
                                                        <div class="form-horizontal">
                                                            <div class="control-group row-fluid form-inline">
                                                                <div class="col-auto" style="width:120px">
                                                                    <label class="control-label pull-right">Treatment : </label>
                                                                </div>
                                                                <div class="col">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlTreatment" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnClearTreatment" EventName="Click" />

                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                    
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>  
                                                <div class="row">
                                                     <div class="col-md-12">
                                                           <div class="form-horizontal">
                                                                 <div class="control-group row-fluid form-inline">
                                                                  <div class="col-auto" style="width:120px"></div>
                                                                  <div class="col mt-1">
                                                                      <asp:CheckBox ID="cbTMAutoTick" runat="server" Text="Auto Check" CssClass="form-check-input"/>

                                                                  </div>
                                                                 </div>
                                                           </div>
                                                         </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 mt-3">
                                                        <div class="control-group">
                                                            <div class="col-md-12 text-center">
                                                                <asp:Button ID="btnAddTreatment" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2" OnClick="btnAddTreatment_Click" />
                                                                <asp:Button ID="btnClearTreatment" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnClearTreatment_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                                    <div class="row" style="padding-bottom: 50px;">
                                                        <div class="col-md-12">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="table-responsive text-nowrap">
                                                                        <asp:HiddenField runat="server" ID="hfGroupMethodID" />
                                                                        <asp:GridView ID="gvTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                            CssClass="table table-striped table-bordered pre-scrollable"
                                                                            DataKeyNames="GroupMethodID,TreatmentCode,TreatmentName"
                                                                            OnRowCommand="gvTreatmentList_RowCommand"
                                                                            OnRowDataBound="gvTreatmentList_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteTreatmentItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:ButtonField CommandName="EditTreatmentItem" Text="Edit" ControlStyle-CssClass="btn btn-info btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                                                                    <ControlStyle CssClass="btn  btn-info btn-sm"></ControlStyle>
                                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                                                </asp:ButtonField>
                                                                                <asp:BoundField HeaderText="Item Code" DataField="TreatmentCode">
                                                                                    <HeaderStyle CssClass="text-left" Width="20%" />
                                                                                    <ItemStyle CssClass="text-left" />
                                                                                </asp:BoundField>
                                                                                <asp:BoundField HeaderText="Item Name" DataField="TreatmentName">
                                                                                    <HeaderStyle CssClass="text-left" Width="70%" />
                                                                                    <ItemStyle CssClass="text-left" />
                                                                                </asp:BoundField>
                                                                                  <asp:templatefield>   
                                                                                    <HeaderStyle CssClass="text-center" />
                                                                                    <headertemplate>
                                                                                      <asp:Label ID="Label1" runat="server" Text="Auto Check"></asp:Label>
                                                                                    </headertemplate>
                                                                                    <itemtemplate>
                                                                                      <asp:CheckBox ID="chkSelect" cssclass="chkItem" runat="server" Checked='<%#Eval("AutoTick") %>'  Enabled="false" OnClientClick="javascript:putCookie(); return false"  />
                                                                                    </itemtemplate>
                                                                                      <ItemStyle CssClass="text-center" />
                                                                                    
                                                                                   </asp:templatefield>


                                                                            </Columns>
                                                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                            <HeaderStyle CssClass="table-warning" />
                                                                        </asp:GridView>

                                                                    </div>


                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddTreatment" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </div>
                                                    </div>
                                              <%--  </ContentTemplate>
                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddTreatment" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                                                                </Triggers>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                        
                                    </div>


                                </div>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                    <div id="medicine" class="tab-pane fade pt-3 pb-3">
                         <%-- <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                              <ContentTemplate>--%>
                                  <div class="row">
                                      <div class="col-md-12">
                                          <div class="alert alert-primary">
                                              <div class="row mt-2 mb-2">
                                                  <div class="col-md-12 px-0">
                                                      <div class="form-horizontal">
                                                          <div class="control-group row-fluid form-inline">
                                                              <div class="col-auto px-0" style="min-width: 100px;">
                                                                  <label class="control-label pull-right">Medicine : </label>
                                                              </div>
                                                              <div class="col">
                                                                  <asp:HiddenField ID="hfMedicine" runat="server" /> 
                                                                  <select name="ddlMedicine" id="ddlMedicine" ></select>
                                                              </div>
                                                              
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>



                                              <div class="row mb-2">
                                                  <div class="col-md-6 px-0">

                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">QTY : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:TextBox ID="txtMedQTY" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                          </div>

                                                      </div>
                                                  </div>
                                                  <div class="col-md-6 px-0">
                                                      <div class="control-group row-fluid form-inline ">

                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right" >Unit : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                          </div>
                                                       
                                                      </div>
                                                  </div>
                                              </div>
                                                        
                                              <div class="row mb-2">
                                                  <div class="col-md-6 px-0">

                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">Unit Price : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:TextBox ID="txtMedUnitPrice" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                               <asp:HiddenField runat="server" ID="hdMedChargeCode" />
                                                          </div>

                                                      </div>
                                                  </div>
                                                  <div class="col-md-6 px-0">
                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">AMT : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:TextBox ID="txtMedAMT" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                          </div>
                                                      </div>
                                                  </div>

                                              </div>

                                              <div class="row mb-2">
                                                  <div class="col-md-12 px-0">
                                                      <div class="control-group row-fluid form-inline ">
                                                           <div class="col-auto px-0"  style="min-width: 100px;">
                                                          <label class="control-label pull-right">Dose Type : </label>
                                                           </div>
                                                          <div class="col w-75">
                                                              <asp:DropDownList ID="ddlDoseType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <div class="row mb-2">
                                                  <div class="col-md-12 px-0">
                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">Dose QTY : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:DropDownList ID="ddlDoseQTY" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                          </div>

                                                      </div>
                                                  </div>
                                              </div>
                                              <div class="row mb-2">
                                                  <div class="col-md-12 px-0">
                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto  px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">Dose Unit : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:DropDownList ID="ddlDoseUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                              <div class="row mb-2">
                                                  <div class="col-md-12 px-0">
                                                      <div class="control-group row-fluid form-inline ">
                                                          <div class="col-auto px-0" style="min-width: 100px;">
                                                              <label class="control-label pull-right">Dose Code : </label>
                                                          </div>
                                                          <div class="col">
                                                              <asp:DropDownList ID="ddlDoseCode" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                               <div class="row">
                                                                <div class="col-md-6">
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12 px-0">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto px-0" style="min-width:100px;">
                                                                                    <label class="control-label pull-right">Aux Label : </label>
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel1" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12 px-0">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto px-0" style="min-width:100px;">
                                                                                    
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel2" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12 px-0">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto px-0" style="min-width:100px;">
                                                                                   
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel3" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12 px-0">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto px-0" style="min-width:100px;">
                                                                                   
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:CheckBox ID="cbMedAutoTick" runat="server" Text="Auto Check" CssClass="form-check-input"/>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="row mb-2">
                                                                        <div class="col-md-12 px-0">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto  px-0" style="min-width:100px;">
                                                                                    <label class="control-label pull-right">Remark : </label>
                                                                                </div>
                                                                                <div class="col">
                                                                                    <asp:TextBox ID="txtMedRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm w-100" Rows="3"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                </div>
                                              <div class="row mt-3 mb-2">
                                                  <div class="col-md-12">
                                                      <div class="control-group">
                                                          <div class="col-md-12 text-center">
                                                              <asp:Button ID="btnAddMedicine" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2" OnClick="btnAddMedicine_Click" />
                                                              <asp:Button ID="btnClearMedicine" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4"  />
                                                          </div>
                                                      </div>
                                                  </div>
                                              </div>
                                          </div>
                                      </div>
                                  </div>
                               <%--</ContentTemplate>
                             <Triggers>
                                  <asp:AsyncPostBackTrigger ControlID="ddlMedicine" EventName="SelectedIndexChanged" />
                                  <asp:AsyncPostBackTrigger ControlID="btnAddMedicine" EventName="Click" />
                                  <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                              </Triggers>
                          </asp:UpdatePanel>--%>
                          <div class="row" style="padding-bottom: 50px;">
                              <div class="col-md-12">
                                 <%-- <asp:UpdatePanel ID="UpdatePanel7" runat="server" >--%>
                                  <asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                                      <ContentTemplate>
                                          <div class="table-responsive text-nowrap">
                                              <asp:HiddenField runat="server" ID="HiddenField1" />
                                              <asp:GridView ID="gvMedicineList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                  CssClass="table table-striped table-bordered pre-scrollable"
                                                  DataKeyNames="GroupMethodID,MedicineCode,MedicineName"
                                                  OnRowCommand="gvMedicineList_RowCommand"
                                                  OnRowDataBound="gvMedicineList_RowDataBound">
                                                  <Columns>
                                                      <asp:TemplateField>
                                                          <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                          <HeaderStyle Width="10%"></HeaderStyle>
                                                          <ItemStyle CssClass="text-center"></ItemStyle>
                                                          <ItemTemplate>
                                                              <asp:Button ID="btnDeleteMedicineItem" runat="server" Text="Del" CommandName="DeleteMedicineItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                          </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:ButtonField CommandName="EditMedicineItem" Text="Edit" ControlStyle-CssClass="btn btn-info btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                                          <ControlStyle CssClass="btn  btn-info btn-sm"></ControlStyle>
                                                          <HeaderStyle Width="10%"></HeaderStyle>
                                                          <ItemStyle CssClass="text-center"></ItemStyle>
                                                      </asp:ButtonField>
                                                      <asp:BoundField HeaderText="Item Code" DataField="MedicineCode">
                                                          <HeaderStyle CssClass="text-left" Width="20%" />
                                                          <ItemStyle CssClass="text-left" />
                                                      </asp:BoundField>
                                                      <asp:BoundField HeaderText="Item Name" DataField="MedicineName">

                                                          <HeaderStyle CssClass="text-left"  />
                                                          <ItemStyle CssClass="text-left wrap" Width="70%" />
                                                      </asp:BoundField>

                                                        <asp:templatefield>   
                                                        <HeaderStyle CssClass="text-center" />
                                                        <headertemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Auto Check"></asp:Label>
                                                        </headertemplate>
                                                        <itemtemplate>
                                                            <asp:CheckBox ID="chkSelect" cssclass="chkItem" runat="server" Checked='<%#Eval("AutoTick") %>'  Enabled="false" OnClientClick="javascript:putCookie(); return false"  />
                                                        </itemtemplate>
                                                            <ItemStyle CssClass="text-center" />
                                                                                    
                                                        </asp:templatefield>

                                                  </Columns>
                                                  <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                  <HeaderStyle CssClass="table-warning" />
                                              </asp:GridView>

                                          </div>


                                      </ContentTemplate>
                                      <Triggers>
                                          <asp:AsyncPostBackTrigger ControlID="btnAddMedicine" EventName="Click" />
                                          <asp:AsyncPostBackTrigger ControlID="btnUpdateMedicine" EventName="Click" />
                                       
                                      </Triggers>
                                  </asp:UpdatePanel>
                              </div>
                          </div>
                    </div>
                    <div id="doctor" class="tab-pane fade pt-3 pb-3">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-primary py-3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="control-group row-fluid form-inline">
                                                        <div class="col-auto" style="width: 120px">
                                                            <label class="control-label pull-right">Doctor : </label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddDoctor" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnClearDoctor" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvDoctorList" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 mt-3">
                                                <div class="control-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnAddDoctor" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2" OnClick="btnAddDoctor_Click" />
                                                        <asp:Button ID="btnClearDoctor" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnClearDoctor_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-bottom: 50px;">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="table-responsive text-nowrap">
                                                        <asp:GridView ID="gvDoctorList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                            CssClass="table table-striped table-bordered pre-scrollable"
                                                            DataKeyNames="GroupMethodID,DoctorCode,DoctorName"
                                                            OnRowCommand="gvDoctorList_RowCommand"
                                                            OnRowDataBound="gvTreatmentList_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteDoctorItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Item Code" DataField="DoctorCode">
                                                                    <HeaderStyle CssClass="text-left" Width="20%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Item Name" DataField="DoctorName">
                                                                    <HeaderStyle CssClass="text-left" Width="70%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                            <HeaderStyle CssClass="table-warning" />
                                                        </asp:GridView>

                                                    </div>


                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddDoctor" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClearDoctor" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="computer" class="tab-pane fade pt-3 pb-3">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-primary py-3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="control-group row-fluid form-inline">
                                                        <div class="col-auto" style="width: 120px">
                                                            <label class="control-label pull-right">Computer : </label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlComputer" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddComputer" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnClearComputer" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvComputerList" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 mt-3">
                                                <div class="control-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnAddComputer" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2" OnClick="btnAddComputer_Click" />
                                                        <asp:Button ID="btnClearComputer" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnClearComputer_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-bottom: 50px;">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="table-responsive text-nowrap">
                                                        <asp:GridView ID="gvComputerList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                            CssClass="table table-striped table-bordered pre-scrollable"
                                                            DataKeyNames="GroupMethodID,ComputerCode,ComputerName"
                                                            OnRowCommand="gvComputerList_RowCommand"
                                                            OnRowDataBound="gvComputerList_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteComputerItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Item Code" DataField="ComputerCode">
                                                                    <HeaderStyle CssClass="text-left" Width="20%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Item Name" DataField="ComputerName">
                                                                    <HeaderStyle CssClass="text-left" Width="70%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                            <HeaderStyle CssClass="table-warning" />
                                                        </asp:GridView>

                                                    </div>


                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddComputer" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClearComputer" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="clinic" class="tab-pane fade pt-3 pb-3">
                         <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-primary py-3">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-horizontal">
                                                    <div class="control-group row-fluid form-inline">
                                                        <div class="col-auto" style="width: 120px">
                                                            <label class="control-label pull-right">Clinic : </label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlClinic" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAddClinic" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="btnClearClinic" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
                                                                    <asp:AsyncPostBackTrigger ControlID="gvClinicList" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-12 mt-3">
                                                <div class="control-group">
                                                    <div class="col-md-12 text-center">
                                                        <asp:Button ID="btnAddClinic" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2" OnClick="btnAddClinic_Click" />
                                                        <asp:Button ID="btnClearClinic" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnClearClinic_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" style="padding-bottom: 50px;">
                                        <div class="col-md-12">
                                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <div class="table-responsive text-nowrap">
                                                        <asp:GridView ID="gvClinicList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                            CssClass="table table-striped table-bordered pre-scrollable"
                                                            DataKeyNames="GroupMethodID,ClinicCode,ClinicName"
                                                            OnRowCommand="gvClinicList_RowCommand"
                                                            OnRowDataBound="gvClinicList_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                    <HeaderStyle Width="10%"></HeaderStyle>
                                                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteClinicItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Item Code" DataField="ClinicCode">
                                                                    <HeaderStyle CssClass="text-left" Width="20%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField HeaderText="Item Name" DataField="ClinicName">
                                                                    <HeaderStyle CssClass="text-left" Width="70%" />
                                                                    <ItemStyle CssClass="text-left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                            <HeaderStyle CssClass="table-warning" />
                                                        </asp:GridView>

                                                    </div>


                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddClinic" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClearClinic" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />
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

        <div class="form-group">

            <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
            </div>

           


           






        </div>

    </div>
    <div class="modal fade bd-example-modal-md" id="modalEditMedicine" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width:800px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title">Edit Medicine Item</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row my-2">
                                        <div class="col-md-12 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="pull-right">Medicine : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:Label runat="server" ID="lblMedicineName"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdMedicineCode" />
                                                    <asp:HiddenField runat="server" ID="hdMedicineGroupMethodID" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-8 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">QTY : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox ID="txtEditMedQTY" runat="server" CssClass="form-control form-control-sm rightText w-100" onFocus="this.select()" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                </div>

                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Unit : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:DropDownList ID="ddlEditUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-8 px-0">

                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Unit Price : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox ID="txtEditUnitPrice" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                </div>
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">AMT : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox ID="txtEditMedAMT" runat="server" CssClass="form-control form-control-sm rightText w-100" onFocus="this.select()" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-12 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Dose Type : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:DropDownList ID="ddlEditDoseType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-12 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Dose QTY : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:DropDownList ID="ddlEditDoseQTY" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-12 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Dose Unit : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:DropDownList ID="ddlEditDoseUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row mb-2">
                                        <div class="col-md-12 px-0">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto px-0" style="min-width: 100px;">
                                                    <label class="control-label pull-right">Dose Code : </label>
                                                </div>
                                                <div class="col">
                                                   <asp:DropDownList ID="ddlEditDoseCode" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row mb-2">
                                                <div class="col-md-12 px-0">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto px-0" style="min-width: 100px;">
                                                            <label class="control-label pull-right">Aux Label : </label>
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel1" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12 px-0">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto px-0" style="min-width: 100px;">
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel2" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12 px-0">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto px-0" style="min-width: 100px;">
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel3" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                             <div class="row mb-2">
                                                <div class="col-md-12 px-0">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto px-0" style="min-width: 100px;">
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:CheckBox ID="cbMedEditAutoTick" runat="server" Text="Auto check" CssClass="form-check-input"/>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row mb-2">
                                                <div class="col-md-12 px-0">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto px-0" style="min-width: 100px;">
                                                            <label class="control-label pull-right">Remark : </label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:TextBox ID="txtEditMedRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm w-100" Rows="3"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                   
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateMedicine" runat="server" CssClass="btn btn-primary btn-sm"  Text="Update"  OnClick="btnUpdateMedicine_Click" ></asp:Button>
                    <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>


            <div class="modal fade bd-example-modal-md" id="modalEditTreatment" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
            <div class="modal-dialog modal-md" role="document">
                <div class="modal-content">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-header">
                                <h6 class="modal-title" id="htmlTitleUpdateTreatment" runat="server">Edit Treatment Item</h6>
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
                                                    <div class="col-4">
                                                        <label class="pull-right">Treatment Item : </label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:Label runat="server" ID="lblTreatmentName"></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdTreatmentCode" />
                                                        <asp:HiddenField runat="server" ID="hdChargeCode" />
                                                        <asp:HiddenField runat="server" ID="hdGroupMethodID" />
                                                        <asp:HiddenField runat="server" ID="hdTreatmentEntryStyle" />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-4">
                                                        <label class="pull-right">Qty : </label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:TextBox ID="txtQty" runat="server" CssClass="form-control form-control-sm" onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"></asp:TextBox>

                                                    </div>
                                                    <div class="col-2">
                                                        <label class="pull-right">Amt. : </label>
                                                    </div>
                                                    <div class="col-3">
                                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control form-control-sm rightText" onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-4">
                                                        <label class="pull-right">Remark : </label>
                                                    </div>
                                                    <div class="col-8">
                                                        <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-4">
                                                        <label class="pull-right"></label>
                                                    </div>
                                                    <div class="col-8 mt-1">
                                                        <asp:CheckBox ID="cbTMEditAutoTick" runat="server" Text="Auto check" CssClass="form-check-input" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdateTreatment" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnUpdateTreatment_Click"></asp:Button>
                                <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancel</button>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAddTreatment" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvTreatmentList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
      
    <script src="https://code.jquery.com/jquery-3.4.0.js"></script>
    <script src="../../Scripts/select2.min.js"></script>
    <script src="../../Scripts/notify.js"></script>

    <script type="text/javascript">
        function CalculateMedPrice() {

            var msg = '';
            var qty = $('#<%=txtMedQTY.ClientID%>').length ? $('#<%=txtMedQTY.ClientID%>').val() : 0;
             var amt = $('#<%=txtMedAMT.ClientID%>').length ? $('#<%=txtMedAMT.ClientID%>').val() : 0;
             var chartAmt = $('#<%=txtMedUnitPrice.ClientID%>').length ? $('#<%=txtMedUnitPrice.ClientID%>').val() : 0;
             var totalAmt = qty * chartAmt;
             $('#<%=txtMedAMT.ClientID%>').val(totalAmt);

        }

        function CalculateEditMedPrice() {

            var msg = '';
            var qty = $('#<%=txtEditMedQTY.ClientID%>').length ? $('#<%=txtEditMedQTY.ClientID%>').val() : 0;
              var amt = $('#<%=txtEditMedAMT.ClientID%>').length ? $('#<%=txtEditMedAMT.ClientID%>').val() : 0;
              var chartAmt = $('#<%=txtEditUnitPrice.ClientID%>').length ? $('#<%=txtEditUnitPrice.ClientID%>').val() : 0;
              var totalAmt = qty * chartAmt;
              $('#<%=txtEditMedAMT.ClientID%>').val(totalAmt);

        }

        function showModalTreatment() {
            $("#modalEditTreatment").modal('show');
        }

        function showModalMedicine() {
            $("#modalEditMedicine").modal('show');
        }
        function isNumberOneDecimal(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var index = $(element).val().indexOf('.');
            var len = $(element).val().length;
            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode != 46 || $(element).val().indexOf('.') != -1) &&      // “.” CHECK DOT, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;
            else if (index > 0) {
                var CharAfterdot = (len + 1) - index;
                if (CharAfterdot > 2) {
                    return false;
                }
            }

            return true;
        }
        function isNumberOnly(evt, element) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            var index = $(element).val().indexOf('.');
            var len = $(element).val().length;
            if (
                (charCode != 45 || $(element).val().indexOf('-') != -1) &&      // “-” CHECK MINUS, AND ONLY ONE.
                (charCode < 48 || charCode > 57))
                return false;
            else if (index > 0) {
                var CharAfterdot = (len + 1) - index;
                if (CharAfterdot > 2) {
                    return false;
                }
            }

            return true;
        }

        function GetStockMasterByKey(stockCode) {

            var dataObject = JSON.stringify({
                "stockCode": stockCode 
            });

            $.ajax({
                type: "POST",
                url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("SetupGroupMethod.aspx/GetStockMasterByKey")%>',
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 30000,
                success: function (response, textStatus, jqXHR) {
                    var response = $.parseJSON(response.d);
                    var obj = (typeof response.d) == 'string' ? eval('(' + response + ')') : response;

                    
                    if (obj.MedicineCode != null && obj.MedicineCode != '') {

                        $("#<%=txtMedQTY.ClientID%>").val(obj.QTY);
                        $("#<%=txtMedAMT.ClientID%>").val(obj.AMT);
                        $("#<%=txtMedUnitPrice.ClientID%>").val(obj.AMT);
                        $("#<%=ddlUnit.ClientID%>").val(obj.UnitCode).change(); 
                        //$("#ddlUnit").val(obj.UnitCode).change();
                        $("#<%=ddlDoseType.ClientID%>").val(obj.DoseTypeCode).change();
                        $("#<%=ddlDoseQTY.ClientID%>").val(obj.DoseQTY).change();
                        $("#<%=ddlDoseUnit.ClientID%>").val(obj.DoseUnitCode).change();
                        $("#<%=ddlDoseCode.ClientID%>").val(obj.DoseCode).change();
                        $("#<%=ddlAuxLabel1.ClientID%>").val(obj.AUXLABEL1).change();
                        $("#<%=ddlAuxLabel2.ClientID%>").val(obj.AUXLABEL2).change();
                        $("#<%=ddlAuxLabel3.ClientID%>").val(obj.AUXLABEL3).change();
                        $("#<%=txtRemark.ClientID%>").val('');
                        
                        //return;
                    }
                },

                error: function (jqXHR, textStatus, errorThrown) {

                    $.notify("Error : " +jqXHR.statusText,
                        {
                            className: 'error',
                            position: 'bottom right',
                            clickToHide: true
                        }
                    );
                }
            });
        }

        $(document).ready(function () {

            $('.nav-tabs li:eq(0) a').tab('show'); 
            //$('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');
            $('#<%=cbTMAutoTick.ClientID%> , #<%=cbMedAutoTick.ClientID%>').prop('disabled', true);
            $('a[href*="EditGroupMethod"]').click(function () {
                $('#<%=btnClearTreatment.ClientID%>,#<%=btnClearMedicine.ClientID%>,#<%=btnClearDoctor.ClientID%>,#<%=btnClearComputer.ClientID%>,#<%=btnClearClinic.ClientID%>').prop('disabled', false);
                $("[id*='cbTMAutoTick'],[id*='cbMedAutoTick']").prop('checked', false);
                $('#<%=cbTMAutoTick.ClientID%>,#<%=cbMedAutoTick.ClientID%>').prop('disabled', false);
            });
            $("#<%=ddlTreatment.ClientID%>").select2({

              placeholder: "Select Treatment",
              width: '100%',
                allowClear: true,
                disabled: true,
              

            });

           $("#<%=ddlDoctor.ClientID%>").select2({

              placeholder: "Select Doctor",
              width: '100%',
              allowClear: true,
              disabled: true,
              

            });

            $("#<%=ddlComputer.ClientID%>").select2({

              placeholder: "Select Computer",
              width: '100%',
              allowClear: true,
              disabled: true,

            });

             $("#<%=ddlClinic.ClientID%>").select2({

              placeholder: "Select Clinic",
              width: '100%',
              allowClear: true,
              disabled: true,

            });

            $("#ddlMedicine").select2({
                ajax: {
                    delay: 150,
                    type: "POST",
                    url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("SetupGroupMethod.aspx/SearchMedicine")%>',
                    contentType: "application/json; charset=utf-8",
                    data: function (params) {
                        return JSON.stringify({
                            'textSearch': params.term,
                            'startPage': parseInt(params.page || 1),
                            'per_page': parseInt(10)
                        });
                    },
                  
                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        var data = $.parseJSON(data.d);
                        var models = (typeof data.Item1) == 'string' ? eval('(' + data.Item1 + ')') : data.Item1;

                        return {

                            results: $.map(models, function (item) {
                                return {
                                    id: item.StockCode,
                                    text: item.MedicineName,
                                };
                            })
                            ,
                            pagination: { more: (params.page * 10) < data.Item2 }

                        };
                    }
                },
                cache: true,
                placeholder: "Select Medicine",
                width: '100%',
                minimumInputLength: 3,
                allowClear: true,
                disabled: true

            });

            $('#ddlMedicine').on('change', function () {
                var stockCode = $(this).val();
                if (stockCode != null && stockCode != "") {
                    $('#<%=hfMedicine.ClientID%>').val(stockCode);
                    GetStockMasterByKey(stockCode);
                }
            });


            $("#<%=ddlDoseUnit.ClientID%>,#<%=ddlUnit.ClientID%>,#<%=ddlDoseType.ClientID%>,#<%=ddlDoseQTY.ClientID%>,#<%=ddlDoseCode.ClientID%>,#<%=ddlAuxLabel1.ClientID%>,#<%=ddlAuxLabel2.ClientID%>,#<%=ddlAuxLabel3.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true

            });

            $("#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true

            });
           

          // Set option selected onchange
         $('#<%=ddlTreatment.ClientID%>').change(function () {
              if ($(this).val() != "" && $(this).val() != null) 
                  $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
              else
                  $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');
             $('#<%=ddlTreatment.ClientID%>').select2('enable');
          });


       

            $('#ddlMedicine').change(function () {
               
                    if ($(this).val() != "" && $(this).val() != null) {
                        $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                    }
                    else {
                        
                        const stockCode = $('#<%=hfMedicine.ClientID%>').val();
                        if (stockCode != null && stockCode != "") {
                            $("#ddlMedicine").val(stockCode).change();
                        }
                        else
                        $('#<%=btnAddMedicine.ClientID%>').attr('disabled', 'disabled');
                    }
                

            });


            $('#<%=ddlDoctor.ClientID%>').change(function () {
              if ($(this).val() != "" && $(this).val() != null) 
                  $('#<%=btnAddDoctor.ClientID%>').removeAttr('disabled');
              else
                  $('#<%=btnAddDoctor.ClientID%>').attr('disabled', 'disabled');
             $('#<%=ddlDoctor.ClientID%>').select2('enable');
            });

            $('#<%=ddlComputer.ClientID%>').change(function () {
                if ($(this).val() != "" && $(this).val() != null) 
                    $('#<%=btnAddComputer.ClientID%>').removeAttr('disabled');
                else
                    $('#<%=btnAddComputer.ClientID%>').attr('disabled', 'disabled');
                $('#<%=ddlComputer.ClientID%>').select2('enable');
            });

             $('#<%=ddlClinic.ClientID%>').change(function () {
                if ($(this).val() != "" && $(this).val() != null) 
                    $('#<%=btnAddClinic.ClientID%>').removeAttr('disabled');
                else
                    $('#<%=btnAddClinic.ClientID%>').attr('disabled', 'disabled');
                $('#<%=ddlClinic.ClientID%>').select2('enable');
            });

            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            //$('.nav-tabs li:eq(0) a').tab('show');



          $('#<%=btnClearMedicine.ClientID%>').click(function (e) {
                e.preventDefault();
                $("[id*='txtMedQTY'],[id*='txtMedAMT'],[id*='txtMedUnitPrice'],[id*='txtMedRemark'],[id*='hfMedicine']").val('').prop('disabled', false); 
                $("[id *= 'ddlMedicine'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3'] ").val(null).trigger('change');
                $("[id*='ddlMedicine'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").prop('disabled', false).trigger('change');
                $("[id*='cbMedAutoTick']").prop('checked', false);
            });
     
            var gvid = '<%= gvSetupGroupMethodList.ClientID %>';
            $('#' + gvid + ' input[id*=btnEditGroupMethod]').click(function () {
                $('#<%=ddlTreatment.ClientID%>,#ddlMedicine').select2('enable');
                $('#<%=ddlTreatment.ClientID%>,#ddlMedicine').val(null).trigger('change');


            });

         

            $("#<%=txtMedQTY.ClientID%> ").change(function () {
                CalculateMedPrice();
            });

            $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                CalculateEditMedPrice();
            });

             $("#<%=txtMedUnitPrice.ClientID%> ").change(function () {
                CalculateMedPrice();
            });

             $("#<%=txtEditUnitPrice.ClientID%> ").change(function () {
                CalculateEditMedPrice();
            });
            
            
          
            $('#<%=btnAddMedicine.ClientID%>').click(function (e) {

                if ($("[id *= 'ddlMedicine']").val() != "" && $("[id *= 'ddlMedicine']").val() != null) {
                    //setTimeout(function () {
                        
                            $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                        
                   // }, 500);
                }
            });

            $('#<%=btnAddTreatment.ClientID%>').click(function (e) {

                if ($("[id *= 'ddlTreatment']").val() != "" && $("[id *= 'ddlTreatment']").val() != null) {
                     //setTimeout(function () {
                        $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                     //}, 500);
                 }
            });

          

        

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                //$('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');
                $('a[href*="EditGroupMethod"]').click(function () {
                    $('#<%=btnClearTreatment.ClientID%>,#<%=btnClearMedicine.ClientID%>,#<%=btnClearDoctor.ClientID%>,#<%=btnClearComputer.ClientID%>,#<%=btnClearClinic.ClientID%>').prop('disabled', false);
                    $("[id*='cbTMAutoTick'],[id*='cbMedAutoTick']").prop('checked', false);
                    $('#<%=cbTMAutoTick.ClientID%>,#<%=cbMedAutoTick.ClientID%>').prop('disabled', false);
                });

              <%-- $("#<%=ddlTreatment.ClientID%>").select2({

                    placeholder: "Select Treatment",
                    width: '100%',
                    allowClear: true,
                    disabled : false,

                });--%>
              




               <%-- $("#ddlMedicine").select2({
                    ajax: {
                        delay: 150,
                        type: "POST",
                        url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("SetupGroupMethod.aspx/SearchMedicine")%>',
                    contentType: "application/json; charset=utf-8",
                    data: function (params) {
                        return JSON.stringify({
                            'textSearch': params.term,
                            'startPage': parseInt(params.page || 1),
                            'per_page': parseInt(10)
                        });
                    },

                    processResults: function (data, params) {
                        params.page = params.page || 1;
                        var data = $.parseJSON(data.d);
                        var models = (typeof data.Item1) == 'string' ? eval('(' + data.Item1 + ')') : data.Item1;
                        return {

                            results: $.map(models, function (item) {
                                return {
                                    id: item.StockCode,
                                    text: item.MedicineName,
                                };
                            })
                            ,
                            pagination: { more: (params.page * 10) < data.Item2 }

                        };
                    }
                },
                cache: true,
                placeholder: "Select Medicine",
                width: '100%',
                minimumInputLength: 3,
                allowClear: true,
                disabled: true

                });--%>


                $('#ddlMedicine').on('change', function () {
                    var stockCode = $(this).val();
                    if (stockCode != null && stockCode != "") {
                        $('#<%=hfMedicine.ClientID%>').val(stockCode);
                         GetStockMasterByKey(stockCode);
                     }
                });

                
                $("#<%=ddlDoctor.ClientID%>").select2({

                    placeholder: "Select Doctor",
                    width: '100%',
                    allowClear: true
                    


                });

                $("#<%=ddlComputer.ClientID%>").select2({

                    placeholder: "Select Computer",
                    width: '100%',
                    allowClear: true
                    


                });
                
                $("#<%=ddlClinic.ClientID%>").select2({

                    placeholder: "Select Clinic",
                    width: '100%',
                    allowClear: true
                    


                });

                $("#<%=ddlDoseUnit.ClientID%>,#<%=ddlUnit.ClientID%>,#<%=ddlDoseType.ClientID%>,#<%=ddlDoseQTY.ClientID%>,#<%=ddlDoseCode.ClientID%>,#<%=ddlAuxLabel1.ClientID%>,#<%=ddlAuxLabel2.ClientID%>,#<%=ddlAuxLabel3.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    width: '100%',
                    allowClear: true

                });

                $("#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    width: '100%',
                    allowClear: true

                });
               
                // Set option selected onchange
               $('#<%=ddlTreatment.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddTreatment.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddTreatment.ClientID%>').attr('disabled', 'disabled');

                });

                $('#ddlMedicine').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) {
                        $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                    }
                    else {
                        const stockCode = $('#<%=hfMedicine.ClientID%>').val();
                        if (stockCode != null && stockCode != "") {
                            $("#ddlMedicine").val(stockCode).change();
                        }
                        else
                        $('#<%=btnAddMedicine.ClientID%>').attr('disabled', 'disabled');
                    }

                 });

                
                 $('#<%=ddlDoctor.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddDoctor.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddDoctor.ClientID%>').attr('disabled', 'disabled');
                 });

                 $('#<%=ddlComputer.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddComputer.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddComputer.ClientID%>').attr('disabled', 'disabled');
                });

                 $('#<%=ddlClinic.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddClinic.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddClinic.ClientID%>').attr('disabled', 'disabled');
                 });

                $(".nav-tabs a").click(function () {
                    $(this).tab('show');
                });

             

                $("#<%=btnClear.ClientID%>").click(function (e) {
                    //e.preventDefault();
                    $("[id *= 'txtMedQTY'], [id *= 'txtMedAMT'],[id*='txtMedUnitPrice'],[id*='txtMedRemark'],[id*='hfMedicine']").val('').prop('disabled', true);
                    $("[id *= 'ddlClinic'],[id *= 'ddlComputer'],[id *= 'ddlDoctor'],[id *= 'ddlTreatment'], [id *= 'ddlMedicine'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").val(null).trigger('change');
                    $("[id *= 'ddlClinic'],[id *= 'ddlComputer'],[id *= 'ddlDoctor'],[id *= 'ddlTreatment'], [id *= 'ddlMedicine'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").prop('disabled', true).trigger('change');
                    $("[id *= 'ddlClinic'],[id *= 'ddlComputer'],[id *= 'ddlDoctor'],[id *= 'ddlTreatment'], [id *= 'ddlMedicine']").attr('disabled', 'disabled');       
                    $('#<%=btnClearClinic.ClientID%>,#<%=btnClearComputer.ClientID%>,#<%=btnClearDoctor.ClientID%>,#<%=btnClearTreatment.ClientID%>, #<%=btnClearMedicine.ClientID%>').prop('disabled', true);
                    $("[id*='cbTMAutoTick'],[id*='cbMedAutoTick']").prop('checked', false);
                    $('#<%=cbTMAutoTick.ClientID%>,#<%=cbMedAutoTick.ClientID%>').prop('disabled', true);
                });


                $("#<%=txtMedQTY.ClientID%> ").change(function () {
                    CalculateMedPrice();
                });

                $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                    CalculateEditMedPrice();
                });

                $("#<%=txtMedUnitPrice.ClientID%> ").change(function () {
                    CalculateMedPrice();
                });

                $("#<%=txtEditUnitPrice.ClientID%> ").change(function () {
                    CalculateEditMedPrice();
                });


<%--                $('#<%=btnAddMedicine.ClientID%>').click(function (e) {

                    if ($("[id *= 'ddlMedicine']").val() != "" && $("[id *= 'ddlMedicine']").val() != null) {
                        setTimeout(function () {
                                
                           $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                            
                        }, 500);
                        alert("btnAddMedicine 2");
                    }
                });--%>

           


            });
        });

    </script>
</asp:Content>

