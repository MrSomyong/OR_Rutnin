<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostGroupMethod.aspx.cs" Inherits="solution.PostTreatment.PostGroupMethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/css/select2.min.css" rel="stylesheet" />
    <link href="../assets/plugins/datetimepicker/css/redmond/jquery-ui.css" rel="stylesheet" />
    <%--<link href="../assets/plugins/datetimepicker/css/redmond/jquery-ui-timepicker-addon.css" rel="stylesheet" />--%>
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
       
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs--> 
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/PostTreatment/">Visit Search</a>
            </li>
            <li class="breadcrumb-item active">Post Treatment</li>
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
                                <asp:HiddenField ID="hdMedicinePriceType" runat="server" />
                                <asp:HiddenField ID="hdTreatmentPriceType" runat="server" />
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
                        <div class="menu-button">
                <div class="row justify-content-end my-1 float-right">
                    <div class="col-12">
                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/Main.aspx">
                             <span class="text">Main</span>
                            </asp:HyperLink>
                        <asp:HyperLink ID="lnkTreatment" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostTreatment.aspx">
                             <span class="text">Treatment</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkDF" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostDF.aspx">
                             <span class="text">DF</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostMedicine.aspx">
                             <span class="text">Medicine</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostGroupMethod.aspx">
                             <span class="text">Group</span>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>

                        </div>
                </div>
            </div>
           <%--<div class="menu-button">
                <div class="row justify-content-end my-3 float-right">
                    <div class="col-12">
                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/Main.aspx">
                             <span class="text">Main</span>
                            </asp:HyperLink>
                        <asp:HyperLink ID="lnkTreatment" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostTreatment.aspx">
                             <span class="text">Treatment</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkDF" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostDF.aspx">
                             <span class="text">DF</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostMedicine.aspx">
                             <span class="text">Medicine</span>
                        </asp:HyperLink>
                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostGroupMethod.aspx">
                             <span class="text">Group</span>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>--%>
            <div class="clearfix"></div>
            <div class="panel">
                <ul class="nav nav-tabs">
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#treatment" style="width: 150px">Group Method</a>
                    </li>
                    <%--  <li class="nav-item">
                        <a class="nav-link text-center" href="#operation" style="width: 150px">Operation</a>
                    </li>--%>
                </ul>
                <div class="tab-content">
                    <div id="treatment" class="tab-pane fade in active">
                        <%--<br />--%>
                        
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-7">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                   <%-- <div class="row">
                                                        <div class="col-md-12">
                                                            <div class="form-horizontal">
                                                                <div class="control-group row-fluid form-inline alert alert-primary">
                                                                    <label class="control-label pull-right">Treatment : </label>
                                                                    <div class="col-md-9">
                                                                        <asp:DropDownList ID="ddlTreatment" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                    </div>
                                                                    <asp:Button ID="btnAddTreatment" runat="server" Text="Add" CssClass="btn btn-success btn-sm" OnClick="btnAddTreatment_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>
                                                    <div class="row" style="padding-bottom: 50px;">
                                                        <div class="col-md-12">
                                                            <div style="overflow-y: scroll; overflow-x: auto; height: 550px;" class="table-responsive text-nowrap">

                                                                <asp:GridView ID="gvOPDTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX,ITEMCODE,GroupMethodCode,GroupType,CHARGECODE"
                                                                    OnRowCommand="gvOPDTreatmentList_RowCommand"
                                                                    OnRowDataBound="gvOPDTreatmentList_RowDataBound">
                                                                    <Columns>

                                                                        <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteTreatment" runat="server" Text="Del" CommandName="delTreatment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn  btn-primary btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnEditTreatment" runat="server" Text="Edit" CommandName="editTreatment" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                     
                                                                        <asp:BoundField HeaderText="Presc#" DataField="SUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Item Name" DataField="ITEMNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                     <asp:BoundField HeaderText="Charge" DataField="ActivityName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Qty" DataField="QTY">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                           <asp:BoundField HeaderText="Amount" DataField="AMT" DataFormatString="{0:#,##0}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Group Method" DataField="GroupMethodInfo.GroupMethodName">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                      <%--  <asp:BoundField HeaderText="Doctor" DataField="DOCTORNAME">
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
                                                                        </asp:BoundField>--%>
                                                                       
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                    <HeaderStyle CssClass="table-warning" />
                                                                </asp:GridView>

                                                            </div>
                                                        </div>

                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                     <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                                                     <asp:AsyncPostBackTrigger ControlID="btnUpdateMedicine" EventName="Click" />
                                                    
                                                     <asp:AsyncPostBackTrigger ControlID="gvTreatmentList" />
                                                     <asp:AsyncPostBackTrigger ControlID="gvMedicineList" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                     
                                        <div class="col-md-5">
                                             <div class="row">
                                                 <div class="container-fluid">
                                                     <div class="form-group alert alert-success mb-1">
                                                         <div class="row">
                                                             <div class="col-auto">
                                                                 <label class="pull-right">Store : </label>
                                                             </div>
                                                             <div class="col">
                                                                  <asp:DropDownList ID="ddlStore" runat="server"  CssClass="form-control d-block" ></asp:DropDownList>
                                                             </div>
                                                         </div>
                                                          <div class="row mt-2">
                                                             <div class="col-auto">
                                                                 <label class="pull-right">Group Method : </label>
                                                             </div>
                                                             <div class="col">
                                                                  <asp:DropDownList ID="ddlGroupMethod" runat="server" AutoPostBack="true" CssClass="form-control d-block" OnSelectedIndexChanged="ddlGroupMethod_SelectedIndexChanged"></asp:DropDownList>
                                                             </div>
                                                         </div>
                                                     </div>
                                                 </div>                                                   
                                                    </div>

                                             <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                <ContentTemplate>
                                                     <div class="row" style="padding-bottom: 50px;">
                                                                <div class="col-md-12">
                                                                    <div style="overflow-y: scroll; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">
                                                                        <div class="panel">
                                                                            <ul class="nav nav-tabs">
                                                                                <li class="nav-item">
                                                                                    <a class="nav-link text-center" href="#treatmentTab" style="width: 150px">Treatment</a>
                                                                                </li>                    
                                                                                  <li class="nav-item">
                                                                                    <a class="nav-link text-center" href="#medicineTab" style="width: 150px">Medicine</a>
                                                                                </li>
                                                                            </ul>
                                                                            <div class="tab-content">
                                                                                <div id="treatmentTab" class="tab-pane fade in active pt-1 pb-3">
                                                                                
                                                                                            <div class="form-group">
                                                                                                <div class="row">
                                                                                                    <div class="col-md-12">
                                       
                                                                                                                <div class="row" style="padding-bottom: 50px;">
                                                                                                                    <div class="col-md-12">
                                                                                                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                                                                                                            <ContentTemplate>
                                                                                                                                <div class="table-responsive text-nowrap">
                                                                                                                                    <asp:HiddenField runat="server" ID="hfGroupMethodID" />
                                                                                                                                    <asp:GridView ID="gvTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                                                                                        CssClass="table table-striped table-bordered pre-scrollable"
                                                                                                                                        DataKeyNames="GroupMethodID,GroupMethodCode,TreatmentCode,TreatmentName"
                                                                                                                                        OnRowCommand="gvTreatmentList_RowCommand"
                                                                                                                                        OnRowDataBound="gvTreatmentList_RowDataBound"
                                                                                                                                         OnRowCreated="gvTreatmentList_RowCreated"
                                                                                                                                         ShowFooter="true" >
                                                                                                                                        <Columns>
                                                                                                                                             <asp:templatefield>   
                                                                                                                                                <HeaderStyle CssClass="text-center" />
                                                                                                                                                <headertemplate>
                                                                                                                                                  <asp:CheckBox ID="chkSelectAll" cssclass="chkHeader" runat="server" />
                                                                                                                                                </headertemplate>
                                                                                                                                                <itemtemplate>
                                                                                                                                                  <asp:CheckBox ID="chkSelect" cssclass="chkItem" runat="server"/>
                                                                                                                                                </itemtemplate>
                                                                                                                                                  <ItemStyle CssClass="text-center" />
                                                                                                                                                 <FooterTemplate>
                                                                                                                                                     <asp:CheckBox ID="chkftSelectAll" cssclass="chkFooter" runat="server" />
                                                                                                                                                   <asp:Button ID="btnSubmitTreatmentSelect" runat="server"  Text="Add Selected"  CssClass="btn btn-success btn-sm" OnClick="btnSubmitTreatmentSelect_Click" />
                                                                                                                                                 </FooterTemplate>
                                                                                                                                               </asp:templatefield>
                                                                                                                                             <asp:ButtonField CommandName="Add" Text="Add" ControlStyle-CssClass="btn btn-success btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                                                                                                                                <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                                                                                                                                <HeaderStyle Width="80px"></HeaderStyle>
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
                                                                                                                                        </Columns>
                                                                                                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                                                                                        <HeaderStyle CssClass="table-warning" />
                                                                                                                                    </asp:GridView>

                                                                                                                                </div>
                                                                                                                            </ContentTemplate>
                                                                                                                            <Triggers>
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="btnAddTreatment" EventName="Click" />--%>
                                                                                                                               <%-- <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />--%>
                                                                                                                                <%--<asp:AsyncPostBackTrigger ControlID="gvSetupGroupMethodList" />--%>
                                                                                                                                <asp:AsyncPostBackTrigger ControlID="btnUpdateTreatment" EventName="Click" />
                                                                                                                                <asp:asyncpostbacktrigger controlid="ddlgroupmethod" eventname="selectedindexchanged" />
                                                                                                                            </Triggers>
                                                                                                                        </asp:UpdatePanel>
                                                                                                                    </div>
                                                                                                                </div>
                                                                                                      
                                                                                                    </div>
                                        
                                                                                                </div>
                                                                                            </div>
                                                                                    
                                                                                </div>
                                                                                <div id="medicineTab" class="tab-pane fade pt-3 pb-3">
                        
                                                                                      <div class="row" style="padding-bottom: 50px;">
                                                                                          <div class="col-md-12">
                                                                                            
                                                                                              <asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                                                                                                  <ContentTemplate>
                                                                                                      <div class="table-responsive text-nowrap">
                                                                                                          <asp:HiddenField runat="server" ID="HiddenField1" />
                                                                                                          <asp:GridView ID="gvMedicineList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                                                              CssClass="table table-striped table-bordered pre-scrollable"
                                                                                                              DataKeyNames="GroupMethodID,GroupMethodCode,MedicineCode,MedicineName"
                                                                                                              OnRowCommand="gvMedicineList_RowCommand"
                                                                                                               OnRowCreated="gvMedicineList_RowCreated"
                                                                                                              OnRowDataBound="gvMedicineList_RowDataBound" ShowFooter="true">
                                                                                                              <Columns>
                                                                                                                   <asp:templatefield>   
                                                                                                                        <HeaderStyle CssClass="text-center" />
                                                                                                                        <headertemplate>
                                                                                                                            <asp:CheckBox ID="chkMedSelectAll" cssclass="chkMedHeader" runat="server" />
                                                                                                                        </headertemplate>
                                                                                                                        <itemtemplate>
                                                                                                                            <asp:CheckBox ID="chkMedSelect" cssclass="chkMedItem" runat="server"/>
                                                                                                                        </itemtemplate>
                                                                                                                            <ItemStyle CssClass="text-center" />
                                                                                                                            <FooterTemplate>
                                                                                                                                <asp:CheckBox ID="chkftMedSelectAll" cssclass="chkMedFooter" runat="server" />
                                                                                                                            <asp:Button ID="btnSubmitMedSelect" runat="server"  Text="Add Selected"  CssClass="btn btn-success btn-sm" OnClick="btnSubmitMedSelect_Click"  />
                                                                                                                            </FooterTemplate>
                                                                                                                        </asp:templatefield>
                                                                                                                   <asp:ButtonField CommandName="Add" Text="Add" ControlStyle-CssClass="btn btn-success btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                                                                                                        <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                                                                                                        <HeaderStyle Width="80px"></HeaderStyle>
                                                                                                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                                                                                                    </asp:ButtonField>
                                                                                                                  <asp:BoundField HeaderText="Item Code" DataField="MedicineCode">
                                                                                                                      <HeaderStyle CssClass="text-left" Width="20%" />
                                                                                                                      <ItemStyle CssClass="text-left" />
                                                                                                                  </asp:BoundField>
                                                                                                                  <asp:BoundField HeaderText="Item Name" DataField="MedicineName">
                                                                                                                      <HeaderStyle CssClass="text-left" Width="50%" />
                                                                                                                      <ItemStyle CssClass="text-left" />
                                                                                                                  </asp:BoundField>
                                                                                                                  <asp:BoundField HeaderText="Unit Name" DataField="UnitName">
                                                                                                                      <HeaderStyle CssClass="text-left" Width="30%" />
                                                                                                                      <ItemStyle CssClass="text-left" />
                                                                                                                  </asp:BoundField>

                                                                                                              </Columns>
                                                                                                              <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                                                              <HeaderStyle CssClass="table-warning" />
                                                                                                          </asp:GridView>

                                                                                                      </div>


                                                                                                  </ContentTemplate>
                                                                                                  <Triggers>
                                                                                                      <%--<asp:AsyncPostBackTrigger ControlID="btnAddMedicine" EventName="Click" />
                                                                                                      <asp:AsyncPostBackTrigger ControlID="btnUpdateMedicine" EventName="Click" />--%>
                                                                                                      <asp:asyncpostbacktrigger controlid="ddlgroupmethod" eventname="selectedindexchanged" />
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
                                                </contenttemplate>
                                                <triggers>
<%--                                                    <asp:asyncpostbacktrigger controlid="ddlgroupmethod" eventname="selectedindexchanged" />--%>
                                                </triggers>
                                             </asp:updatepanel>
                                                    
                                       
                                    </div>
                                    </div>
                                </div>
                           
                    </div>
                    <%--  <div id="operation" class="tab-pane fade">
                        <br />
                    </div>--%>
                </div>
            </div>






        </div>

    </div>
    <div class="modal fade bd-example-modal-md" id="modalEditTreatment" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width:700px;">
            <div class="modal-content"> 
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-header">
                            <h6 class="modal-title" id="htmlTitleUpdateTreatment" runat="server">Edit Treatment</h6>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div> 
               
                        <div class="modal-body">

                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row mb-2">
                                        <div class="col-4">
                                            <label class="pull-right">Treatment Item : </label>
                                        </div>
                                        <div class="col-8">
                                            <asp:Label runat="server" ID="lblTreatmentName"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdTreatmentCode" />
                                            <asp:HiddenField runat="server" ID="hdChargeCode" />
                                            <asp:HiddenField runat="server" ID="hdTreatmentStyle" />
                                            <asp:HiddenField runat="server" ID="hdSubSuffix" />
                                            <asp:HiddenField runat="server" ID="hdGroupRequestCode" />
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
                                            <asp:TextBox ID="txtChargeAMT" runat="server" CssClass="form-control form-control-sm" onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)" Enabled="false"></asp:TextBox>

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

                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnUpdateTreatment" runat="server" CssClass="btn btn-primary btn-sm" Text="Update" OnClick="btnUpdateTreatment_Click"></asp:Button>
                            <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancel</button>
                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>


<%--    <div class="modal fade bd-example-modal-md" id="modalEditTreatment" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title">Edit Treatment</h6>
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
                                            <asp:HiddenField runat="server" ID="hdSubSuffix" />
                                            <asp:HiddenField runat="server" ID="hdGroupRequestCode" />
                                            <asp:HiddenField runat="server" ID="hdChargeAMT" />
                                        </div>
                                    </div>
                                     <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Order Type : </label>
                                        </div>
                                        <div class="col-3">
                                             <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                        </div>
                                      
                                    </div>
                                     <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Charge AMT : </label>
                                        </div>
                                        <div class="col-3">
                                             <asp:TextBox ID="txtChargeAMT" runat="server" CssClass="form-control form-control-sm"  onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"  Enabled="false" ></asp:TextBox>
                                            
                                        </div>
                                      
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Qty : </label>
                                        </div>
                                        <div class="col-3">
                                             <asp:TextBox ID="txtQty" runat="server" CssClass="form-control form-control-sm"  onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)" ></asp:TextBox>
                                            
                                        </div>
                                         <div class="col-2">
                                            <label class="pull-right">Amt. : </label>
                                        </div>
                                        <div class="col-3">
                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control form-control-sm rightText"  onFocus="this.select()" onkeypress="return isNumberOneDecimal(event,this)"></asp:TextBox>
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
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnUpdateTreatment" runat="server" CssClass="btn btn-primary btn-sm"   Text="Update"  OnClick="btnUpdateTreatment_Click"  ></asp:Button>
                    <button type="button" class="btn btn-secondary btn-sm" data-dismiss="modal">Cancel</button>
                </div>
            </div>
        </div>
    </div>--%>
  
    <div class="modal fade bd-example-modal-md" id="modalEditMedicine" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width:900px;">
            <div class="modal-content">
                <div class="modal-header">
                    <h6 class="modal-title">Edit Medicine Item</h6>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row my-2">
                                        <div class="col-md-12">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width: 112px;">
                                                    <label class="pull-right">Medicine : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:Label runat="server" ID="lblMedicineName"></asp:Label>
                                                    <asp:HiddenField runat="server" ID="hdMedicineCode" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row mb-2">
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width:112px;">
                                                    <label class="control-label pull-right">Store : </label>
                                                </div>
                                                <div class="col">
                                                      <asp:DropDownList ID="ddlEditStore" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="row mb-2">
                                        <div class="col-md-6">

                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width: 112px;">
                                                    <label class="control-label pull-right">QTY : </label>
                                                </div>
                                                <div class="col">
                                                   <asp:TextBox ID="txtEditMedQTY" runat="server" CssClass="form-control form-control-sm rightText w-100"  onFocus="this.select()" onkeypress="return isNumberOnly(event,this)" ></asp:TextBox>
                                                </div>

                                                <div class="col-auto">
                                                    <label class="control-label pull-right">Unit : </label>
                                                </div>
                                                <div class="col">
                                                     <asp:DropDownList ID="ddlEditUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">

                                                <div class="col-auto" style="min-width: 117px;">
                                                    <label class="control-label pull-right">Order Type : </label>
                                                </div>
                                                <div class="col">
                                                     <asp:DropDownList ID="ddlEditOrderType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-6">

                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width: 112px;">
                                                    <label class="control-label pull-right">Unit Price : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox ID="txtEditUnitPrice" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width: 117px;">
                                                    <label class="control-label pull-right">AMT : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:TextBox ID="txtEditMedAMT" runat="server" CssClass="form-control form-control-sm rightText w-100"  onFocus="this.select()" onkeypress="return isNumberOnly(event,this)" ></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto">
                                                    <label class="control-label pull-right">Dose Type : </label>
                                                </div>
                                                <div class="col">
                                                    <asp:DropDownList ID="ddlEditDoseType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width: 112px;">
                                                    <label class="control-label pull-right">Dose QTY : </label>
                                                </div>
                                                <div class="col">
                                                     <asp:DropDownList ID="ddlEditDoseQTY" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-6">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width:112px;">
                                                    <label class="control-label pull-right">Dose Unit : </label>
                                                </div>
                                                <div class="col">
                                                     <asp:DropDownList ID="ddlEditDoseUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-md-12">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto">
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
                                                <div class="col-md-12">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto" style="min-width: 112px;">
                                                            <label class="control-label pull-right">Aux Label : </label>
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel1" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto" style="min-width: 112px;">
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel2" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto" style="min-width: 112px;">
                                                        </div>
                                                        <div class="col pr-0" style="max-width:280px">
                                                            <asp:DropDownList ID="ddlEditAuxLabel3" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row mb-2">
                                                <div class="col-md-12">
                                                    <div class="control-group row-fluid form-inline ">
                                                        <div class="col-auto" style="min-width: 112px;">
                                                            <label class="control-label pull-right">Remark : </label>
                                                        </div>
                                                        <div class="col">
                                                            <asp:TextBox ID="txtEditRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm w-100" Rows="3"></asp:TextBox>
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

    <script src="https://code.jquery.com/jquery-3.4.0.js"></script>
    <script src="../../Scripts/select2.min.js"></script>
    <script src="../../Scripts/notify.js"></script>
    <script src="../assets/plugins/moment/js/moment.min.js"></script>
    <script src="../assets/plugins/moment/js/moment-with-locales.min.js"></script>
     <script src="../assets/plugins/datetimepicker/scripts/jquery-1.6.min.js"></script>
    <script src="../assets/plugins/datetimepicker/scripts/jquery-ui.min.js"></script>
    <script src="../assets/plugins/datetimepicker/scripts/jquery.ui.datepicker-th.js"></script>
    <script src="../assets/plugins/jquery-ui-timepicker-addon/1.6.3/jquery-ui-timepicker-addon.min.js"></script>
    <script src="../assets/plugins/datetimepicker/scripts/jquery.ui.datepicker.ext.be.js"></script>
    
    <script type="text/javascript">
		var jQuery_1_6 = $.noConflict(true);
	</script>
    <script type="text/javascript">

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

        function CalculateTreatPrice() {

            var msg = '';
            var qty = $('#<%=txtQty.ClientID%>').length ? $('#<%=txtQty.ClientID%>').val() : 0;
            var amt = $('#<%=txtAmt.ClientID%>').length ? $('#<%=txtAmt.ClientID%>').val() : 0;
            var chartAmt = $('#<%=txtChargeAMT.ClientID%>').length ? $('#<%=txtChargeAMT.ClientID%>').val() : 0;
            var totalAmt = qty * chartAmt;
            $('#<%=txtAmt.ClientID%>').val(totalAmt);
            
        }


        function CalculateMedPrice() {

            var msg = '';
            var qty = $('#<%=txtEditMedQTY.ClientID%>').length ? $('#<%=txtEditMedQTY.ClientID%>').val() : 0;
              var amt = $('#<%=txtEditMedAMT.ClientID%>').length ? $('#<%=txtEditMedAMT.ClientID%>').val() : 0;
              var chartAmt = $('#<%=txtEditUnitPrice.ClientID%>').length ? $('#<%=txtEditUnitPrice.ClientID%>').val() : 0;
              var totalAmt = qty * chartAmt;
              $('#<%=txtEditMedAMT.ClientID%>').val(totalAmt);

          }

   
        //(function($){
        $(document).ready(function () {

                var headerChk = $(".chkHeader input");
                var itemChk = $(".chkItem input");
                var footerChk = $(".chkFooter input");
                var itemChk2 = $(".chkItem input");

            headerChk.click(function () {

                    itemChk.each(function () {
                        this.checked = headerChk[0].checked;
                        if (this.checked == true) {
                            $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().removeClass('highlightRow');
                        }
                    })
                });

                itemChk.each(function () {
                    $(this).click(function () {
                        if (this.checked == false) { headerChk[0].checked = false; }
                    })
                });

                footerChk.click(function () {
                    itemChk2.each(function () {
                        this.checked = footerChk[0].checked;
                        if (this.checked == true) {
                            $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().removeClass('highlightRow');
                        }
                    })
                });

                itemChk2.each(function () {
                    $(this).click(function () {
                        if (this.checked == false) { footerChk[0].checked = false; }
                    })
                });

                $("#<%=gvTreatmentList.ClientID%> input[id*='chkSelect']:checkbox").click(function () {
                    if ($(this).is(':checked')) {
                        $(this).parent().parent().parent().addClass('highlightRow');
                    }
                    else {
                        $(this).parent().parent().parent().removeClass('highlightRow');
                    }
                });








            

                var headerMedChk = $(".chkMedHeader input");
                var itemMedChk = $(".chkMedItem input");
                var footerMedChk = $(".chkMedFooter input");
                var itemMedChk2 = $(".chkMedItem input");

                headerMedChk.click(function () {
                   
                    itemMedChk.each(function () {
                        this.checked = headerMedChk[0].checked;
                        if (this.checked == true) {
                            $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().removeClass('highlightRow');
                        }
                    })
                });

                itemMedChk.each(function () {
                    $(this).click(function () {
                        if (this.checked == false) { headerMedChk[0].checked = false; }
                    })
                });

                footerMedChk.click(function () {
                    itemMedChk2.each(function () {
                        this.checked = footerMedChk[0].checked;
                        if (this.checked == true) {
                            $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().removeClass('highlightRow');
                        }
                    })
                });

                itemMedChk2.each(function () {
                    $(this).click(function () {
                        if (this.checked == false) { footerMedChk[0].checked = false; }
                    })
                });

                $("#<%=gvMedicineList.ClientID%> input[id*='chkMedSelect']:checkbox").click(function () {
                    if ($(this).is(':checked')) {
                        $(this).parent().parent().parent().addClass('highlightRow');
                    }
                    else {
                        $(this).parent().parent().parent().removeClass('highlightRow');
                    }
                });







            $('.nav-tabs li:eq(1) a').tab('show');
           
            $("#<%=ddlStore.ClientID%>,#<%=ddlGroupMethod.ClientID%>").select2({
                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true
            });
         

            $(".nav-tabs a").click(function () {
                  $(this).tab('show');
            });

            $('.nav-tabs li:eq(0) a').tab('show');
          

            $("#<%=txtQty.ClientID%> ").change(function () {
                CalculateTreatPrice();
            });

            $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                CalculateMedPrice();
            });

            $("#<%=ddlEditOrderType.ClientID%>,#<%=ddlEditStore.ClientID%>,#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true

            });
             
            $("#<%=ddlEditDoctorList.ClientID%>").select2({

                placeholder: "-Select Doctor-",
                width: '100%',
                allowClear: true

            });
           
        });
        //})(jQuery_1_6);
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

                        const dateTimeStart = moment($("#<%=txtStartDateTime.ClientID%>").val() + " " + $("#<%=txtStartTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
                        const dateTimeEnd = moment($("#<%=txtEndDateTime.ClientID%>").val() + " " + $("#<%=txtEndTime.ClientID%>").val(), 'DD/MM/YYYY hh:mm');
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


            $(document).ready(function () {
                
                    var headerChk = $(".chkHeader input");
                    var itemChk = $(".chkItem input");
                    var footerChk = $(".chkFooter input");
                    var itemChk2 = $(".chkItem input");

                    headerChk.click(function () {
                    
                        itemChk.each(function () {
                            this.checked = headerChk[0].checked;
                            if (this.checked == true) {
                                $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().addClass('highlightRow');
                            }
                            else {
                                $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().removeClass('highlightRow');
                            }
                        })
                    });

                    itemChk.each(function () {
                        $(this).click(function () {
                            if (this.checked == false) { headerChk[0].checked = false; }
                        })
                    });

                    footerChk.click(function () {
                        itemChk2.each(function () {
                            this.checked = footerChk[0].checked;
                            if (this.checked == true) {
                                $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().addClass('highlightRow');
                            }
                            else {
                                $('#<%=gvTreatmentList.ClientID %>').find("input:checkbox[Id*=chkSelect]").parent().parent().parent().removeClass('highlightRow');
                            }
                        })
                    });

                    itemChk2.each(function () {
                        $(this).click(function () {
                            if (this.checked == false) { footerChk[0].checked = false; }
                        })
                    });

                    $("#<%=gvTreatmentList.ClientID%> input[id*='chkSelect']:checkbox").click(function () {
                        if ($(this).is(':checked')) {
                            $(this).parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $(this).parent().parent().parent().removeClass('highlightRow');
                        }
                    });






                    var headerMedChk = $(".chkMedHeader input");
                    var itemMedChk = $(".chkMedItem input");
                    var footerMedChk = $(".chkMedFooter input");
                    var itemMedChk2 = $(".chkMedItem input");

                    headerMedChk.click(function () {
                        itemMedChk.each(function () {
                            this.checked = headerMedChk[0].checked;
                            if (this.checked == true) {
                                $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().addClass('highlightRow');
                            }
                            else {
                                $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().removeClass('highlightRow');
                            }
                        })
                    });

                    itemMedChk.each(function () {
                        $(this).click(function () {
                            if (this.checked == false) { headerMedChk[0].checked = false; }
                        })
                    });

                    footerMedChk.click(function () {
                        itemMedChk2.each(function () {
                            this.checked = footerMedChk[0].checked;
                            if (this.checked == true) {
                                $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().addClass('highlightRow');
                            }
                            else {
                                $('#<%=gvMedicineList.ClientID %>').find("input:checkbox[Id*=chkMedSelect]").parent().parent().parent().removeClass('highlightRow');
                            }
                        })
                    });

                    itemMedChk2.each(function () {
                        $(this).click(function () {
                            if (this.checked == false) { footerMedChk[0].checked = false; }
                        })
                    });

                    $("#<%=gvMedicineList.ClientID%> input[id*='chkMedSelect']:checkbox").click(function () {
                        if ($(this).is(':checked')) {
                            $(this).parent().parent().parent().addClass('highlightRow');
                        }
                        else {
                            $(this).parent().parent().parent().removeClass('highlightRow');
                        }
                    });




                 
                    $("#<%=ddlEditDoctorList.ClientID%>").select2({

                        placeholder: "-Select Doctor-",
                        width: '100%',
                        allowClear: true

                    });




                    $("#<%=ddlStore.ClientID%>,#<%=ddlGroupMethod.ClientID%>").select2({
                        placeholder: "-Please Select-",
                        width: '100%',
                        allowClear: true
                    });

               

                    $(".nav-tabs a").click(function () {
                        $(this).tab('show');
                    });

                    $("#<%=txtQty.ClientID%> ").change(function () {
                        CalculateTreatPrice();
                    });

                    $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                        CalculateMedPrice();
                    });

                    $("#<%=ddlEditOrderType.ClientID%>,#<%=ddlEditStore.ClientID%>,#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                        placeholder: "-Please Select-",
                        width: '100%',
                        allowClear: true

                    });

            });
        });

    </script>
</asp:Content>

