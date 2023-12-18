<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="solution.PostTreatment.Main" %>

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
            <li class="breadcrumb-item active">Patient Visit Detail</li>
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

                        <div class="menu-button">
                <div class="row justify-content-end my-1 float-right">
                    <div class="col-12">
                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/Main.aspx">
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
                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostGroupMethod.aspx">
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
                        <asp:HyperLink ID="lnkMain" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/Main.aspx">
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
                        <asp:HyperLink ID="lnkGroup" runat="server"  CssClass="btn btn-secondary btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostGroupMethod.aspx">
                             <span class="text">Group</span>
                        </asp:HyperLink>
                    </div>
                </div>
            </div>--%>
            <div class="clearfix"></div>
            <div class="panel">
                <ul class="nav nav-tabs">
                                       
                    <li class="nav-item ">
                        <a class="nav-link text-center" href="#alltreatment" style="width: 150px">All Treatment</a>
                    </li>
                      <li class="nav-item">
                        <a class="nav-link text-center" href="#df" style="width: 150px">DF Treatment</a>
                    </li>
                     <li class="nav-item">
                        <a class="nav-link text-center" href="#medicine" style="width: 150px">Medicine</a>
                    </li>
                   <li class="nav-item">
                        <a class="nav-link text-center" href="#chargedetail" style="width: 150px">Charge Detail</a>
                    </li>
                    <li class="nav-item">
                        <a class="nav-link text-center" href="#chargedetailall" style="width: 200px">Charge Detail All</a>
                    </li>

                </ul>
                <div class="tab-content">
                    <div id="alltreatment" class="tab-pane fade in active">
                        <%--<br />--%>
                        
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                           
                                                 
                                                    <div class="row" style="padding-bottom: 50px;">
                                                        <div class="col-md-12">
                                                            <div style="overflow-y: scroll; overflow-x: auto; height: 550px;" class="table-responsive text-nowrap">

                                                                <asp:GridView ID="gvOPDTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX,CHARGECODE,ITEMCODE,IsDeleted"
                                                                     OnRowDataBound="gvOPDTreatmentList_RowDataBound"
                                                                    >
                                                                    <Columns>
                                                                        <asp:BoundField HeaderText="Presc#" DataField="SUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:TemplateField HeaderText="Type">
                                                                             <HeaderStyle CssClass="text-center" />
                                                                             <ItemStyle CssClass="text-center"/>
                                                                            <ItemTemplate> 
                                                                              <asp:Label ID="lblType" runat="server"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-leftr" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Item Name" DataField="ITEMNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                      <asp:BoundField HeaderText="Charge" DataField="ActivityName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Price" DataField="UNITPRICE" DataFormatString="{0:#,##0.00}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Qty" DataField="QTY">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                           <asp:BoundField HeaderText="Amount" DataField="AMT" DataFormatString="{0:#,##0}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                       
                                                                         <asp:BoundField HeaderText="Group Code" DataField="GroupMethodInfo.GroupMethodName">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                       
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                    <HeaderStyle CssClass="table-warning" />
                                                                </asp:GridView>

                                                            </div>
                                                        </div>

                                                    </div>
                                               
                                        </div>
                                       
                                    </div>
                                </div>
                            
                    </div>
                    <div id="df" class="tab-pane fade">
                       <%-- <br />--%>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="row" style="padding-bottom: 50px;">
                                        <div class="col-md-12">
                                            <div style="overflow-y: scroll; overflow-x: auto; height: 550px;" class="table-responsive text-nowrap">

                                                <asp:GridView ID="gvDFList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX,CHARGECODE,TREATMENTCODE,TREATMENTENTRYSTYLE,IsDeleted"
                                                     OnRowDataBound="gvDFList_RowDataBound"
                                                    >
                                                    <Columns>
                                                       
                                                        <asp:BoundField HeaderText="Presc#" DataField="SUFFIX">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-center" />
                                                        </asp:BoundField>
                                                         <asp:TemplateField HeaderText="Type">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-center"/>
                                                            <ItemTemplate> 
                                                                <asp:Label ID="lblType" runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Item Code" DataField="TREATMENTCODE">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Item Name" DataField="TREATMENTNAME">
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
                                                        <asp:BoundField HeaderText="Amount" DataField="AMT" DataFormatString="{0:#,##0}">
                                                            <HeaderStyle CssClass="text-center" />
                                                            <ItemStyle CssClass="text-right" />
                                                        </asp:BoundField>
                                                        <asp:BoundField HeaderText="Group Code" DataField="GroupMethodInfo.GroupMethodName">
                                                            <HeaderStyle CssClass="text-left" />
                                                        </asp:BoundField>

                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                    <HeaderStyle CssClass="table-warning" />
                                                </asp:GridView>

                                            </div>
                                        </div>

                                    </div>

                                </div>

                            </div>
                        </div>
                    </div>
                    <div id="medicine" class="tab-pane fade">
                        <%--<br />--%>
                        <div class="panel">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row" style="padding-bottom: 50px;">
                                            <div class="col-md-12">
                                                <div style="overflow-y: auto; overflow-x: auto; height: 550px;" class="table-responsive text-nowrap">
                                                

                                                      
                                                   
                                                                <asp:GridView ID="gvMedicineList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX"
                                                                    >
                                                                    <Columns>
                                                                        
                                                                        <asp:BoundField HeaderText="Presc#" DataField="SUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="No." DataField="SUBSUFFIX">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Item Code" DataField="MEDICINECODE">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                      <asp:BoundField HeaderText="Item Name" DataField="MEDICINENAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Charge" DataField="ActivityName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                          <asp:BoundField HeaderText="Price" DataField="UNITPRICE" DataFormatString="{0:#,##0.00}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Qty" DataField="QTY">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                          <asp:BoundField HeaderText="Amt" DataField="AMT" DataFormatString="{0:#,##0}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Unit" DataField="UnitName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>
                                                                          <asp:BoundField HeaderText="Store" DataField="STORENAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-center" />
                                                                        </asp:BoundField>

                                                                        <asp:BoundField HeaderText="Order Type" DataField="TYPEOFCHARGENAME">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Remark" DataField="REMARK">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                      
                                                                       
                                                                    </Columns>
                                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                                    <HeaderStyle CssClass="table-warning" />
                                                                </asp:GridView>

                                                           
                                                


                                                
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div id="chargedetail" class="tab-pane fade">
                      <%--  <br />--%>
                        <div class="panel">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row" style="padding-bottom: 50px;">
                                            <div class="col-md-12">
                                                <div style="overflow-y: auto; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">

                                                    <asp:GridView ID="gvItemChargeList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                        CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                        OnRowCreated="gvItemChargeList_RowCreated">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="" DataField="ITEMDETAIL">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                             <asp:BoundField HeaderText="" DataField="ActivityName">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="Total" DataFormatString="{0:#,##0}">
                                                                <ItemStyle CssClass="text-right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table table-striped table-bordered" cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                                    <tbody>
                                                        <tr class="table-footer">
                                                            <th class="text-left header">รวม</th>
                                                            <th class="text-right header">
                                                                <asp:Literal ID="ltlTotalTreatmentPrice" runat="server"></asp:Literal>
                                                            </th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-6">

                                        <div class="row" style="padding-bottom: 50px;">
                                            <div class="col-md-12">
                                                <div style="overflow-y: auto; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">
                                                    <asp:GridView ID="gvDFTreatmentCharge" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                        CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                        OnRowCreated="gvDFTreatmentCharge_RowCreated">
                                                        <Columns>
                                                            <asp:BoundField HeaderText="" DataField="TREATMENTDOCTORDETAIL">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="ActivityName">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="Total" DataFormatString="{0:#,##0}">
                                                                <ItemStyle CssClass="text-right" />
                                                            </asp:BoundField>
                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table table-striped table-bordered" cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                                    <tbody>
                                                        <tr class="table-footer">
                                                            <th class="text-left header">รวม</th>
                                                            <th class="text-right header">
                                                                <asp:Literal ID="ltlTotalDFPrice" runat="server"></asp:Literal>

                                                            </th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center pb-5">
                                <div class="title ">
                                    <h3>รวมทั้งหมด
                                        <asp:Literal ID="ltlAllTotalPrice" runat="server"></asp:Literal>
                                        บาท</h3>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="chargedetailall" class="tab-pane fade">
                       <%-- <br />--%>
                        <div class="panel">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="row" style="padding-bottom: 50px;">
                                            <div class="col-md-12">
                                                <div style="overflow-y: auto; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">

                                                    <asp:GridView ID="gvItemChargeAllList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                        CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                        OnRowCreated="gvItemChargeAllList_RowCreated">
                                                        <Columns>


                                                            <asp:BoundField HeaderText="" DataField="ITEMDETAIL">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                             <asp:BoundField HeaderText="" DataField="ActivityName">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="Total" DataFormatString="{0:#,##0}">

                                                                <ItemStyle CssClass="text-right" />
                                                            </asp:BoundField>

                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-warning" />
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table table-striped table-bordered" cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                                    <tbody>
                                                        <tr class="table-footer">
                                                            <th class="text-left header">รวม</th>
                                                            <th class="text-right header">
                                                                <asp:Literal ID="ltlTotalTreatmentPriceAll" runat="server"></asp:Literal>
                                                            </th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-6">

                                        <div class="row" style="padding-bottom: 50px;">
                                            <div class="col-md-12">
                                                <div style="overflow-y: auto; overflow-x: auto; height: 450px;" class="table-responsive text-nowrap">

                                                    <asp:GridView ID="gvDFTreatmentChargeAll" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                        CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                        OnRowCreated="gvDFTreatmentChargeAll_RowCreated">
                                                        <Columns>

                                                            <asp:BoundField HeaderText="" DataField="TREATMENTDOCTORDETAIL">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="ActivityName">
                                                                <ItemStyle CssClass="text-left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField HeaderText="" DataField="Total" DataFormatString="{0:#,##0}">
                                                                <ItemStyle CssClass="text-right" />
                                                            </asp:BoundField>


                                                        </Columns>
                                                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                        <HeaderStyle CssClass="table-info" />
                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-md-12">
                                                <table class="table table-striped table-bordered" cellspacing="0" rules="all" border="0" style="border-collapse: collapse;">
                                                    <tbody>
                                                        <tr class="table-footer">
                                                            <th class="text-left header">รวม</th>
                                                            <th class="text-right header">
                                                                <asp:Literal ID="ltlTotalDFPriceAll" runat="server"></asp:Literal>

                                                            </th>
                                                        </tr>
                                                    </tbody>
                                                </table>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="text-center pb-5">
                                <div class="title ">
                                    <h3>รวมทั้งหมด
                                        <asp:Literal ID="ltlTotalPriceAllClinic" runat="server"></asp:Literal>
                                        บาท</h3>
                                </div>
                            </div>
                        </div>
                    </div>


                </div>
            </div>






        </div>

    </div>
    <div class="modal fade bd-example-modal-md" id="modalEditTreatment" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
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
                                            <%--<asp:HiddenField runat="server" ID="hdVN" />--%>
                                            <asp:HiddenField runat="server" ID="hdSubSuffix" />
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
        $(document).ready(function () {
            
           

          $(".nav-tabs a").click(function () {
              $(this).tab('show');
          });

          $('.nav-tabs li:eq(0) a').tab('show');

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
               

                $(".nav-tabs a").click(function () {
                    $(this).tab('show');
                });

                $('.nav-tabs li:eq(0) a').tab('show');

            });
        });

    </script>
</asp:Content>
