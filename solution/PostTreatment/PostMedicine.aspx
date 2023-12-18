<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PostMedicine.aspx.cs" Inherits="solution.PostTreatment.PostMedicine" %>

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
        #MainContent_gvMedicineList tr td ,
        #MainContent_gvMedicineList tr th ,
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
            <li class="breadcrumb-item active">Post Medicine</li>
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
                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostMedicine.aspx">
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
                        <asp:HyperLink ID="lnkMedicine" runat="server"  CssClass="btn btn-info btn-sm px-3 mr-2"  NavigateUrl="~/PostTreatment/PostMedicine.aspx">
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
                        <a class="nav-link text-center" href="#medicine" style="width: 175px">Medicine</a>
                    </li>
              
                </ul>
                <div class="tab-content">
                    <div id="medicine" class="tab-pane fade in active">
                        <%--<br />--%>
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-12">
                                            
                                                    <div class="row">
                                                        <div class="col-md-12">

                                                            <div class="alert alert-primary">
                                                                <div class="row mt-2 mb-2">
                                                                    <div class="col-md-6">
                                                                      
                                                                    </div>
                                                                    
                                                                    <div class="col-md-6" hidden="hidden">
                                                                        <div class="form-horizontal alert alert-danger" style="padding:15px;">
                                                                            <div class="control-group row-fluid form-inline" >
                                                                                 <div class="col-auto" style="min-width:117px;">
                                                                                    <label class="control-label pull-right font-weight-bold" >Add Item to Group Method : </label>
                                                                                </div>
                                                                                <div class="col">
                                                                                      <asp:DropDownList ID="ddlSetupGroupMethod" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="row mt-2 mb-2">
                                                                    <div class="col-md-6">
                                                                        <div class="form-horizontal">
                                                                            <div class="control-group row-fluid form-inline">
                                                                                    <div class="col-auto" style="min-width:112px;">
                                                                                        <label class="control-label pull-right">Store : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:DropDownList ID="ddlStore" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                    </div>
                                                                               

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-md-6">
                                                                        <div class="form-horizontal">
                                                                            <div class="control-group row-fluid form-inline">
                                                                                 <div class="col-auto" style="min-width:117px;">
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
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                    <ContentTemplate>
                                                                        <div class="row mb-2">
                                                                            <div class="col-md-6">

                                                                                <div class="control-group row-fluid form-inline ">
                                                                                    <div class="col-auto" style="min-width:112px;">
                                                                                        <label class="control-label pull-right">QTY : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:TextBox ID="txtMedQTY" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                                                    </div>
                                                                                   
                                                                                    <div class="col-auto">
                                                                                        <label class="control-label pull-right">Unit : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:DropDownList ID="ddlUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="control-group row-fluid form-inline ">
                                                                                   
                                                                                     <div class="col-auto" style="min-width:117px;">
                                                                                        <label class="control-label pull-right">Order Type : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:DropDownList ID="ddlOrderType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                        
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row mb-2">
                                                                            <div class="col-md-6">

                                                                                <div class="control-group row-fluid form-inline ">
                                                                                    <div class="col-auto" style="min-width:112px;">
                                                                                        <label class="control-label pull-right">Unit Price : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:TextBox ID="txtMedUnitPrice" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
                                                                                        <asp:HiddenField runat="server" ID="hdChargeCode" />
                                                                                    </div>
                                                                                   
                                                                                </div>
                                                                            </div>
                                                                            <div class="col-md-6">
                                                                                <div class="control-group row-fluid form-inline ">
                                                                                    <div class="col-auto" style="min-width:117px;">
                                                                                        <label class="control-label pull-right">AMT : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:TextBox ID="txtMedAMT" runat="server" CssClass="form-control form-control-sm rightText w-100" onkeypress="return isNumberOnly(event,this)"></asp:TextBox>
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
                                                                                        <asp:DropDownList ID="ddlDoseType" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="row mb-2">
                                                                            <div class="col-md-6">
                                                                                <div class="control-group row-fluid form-inline ">
                                                                                    <div class="col-auto" style="min-width:112px;">
                                                                                        <label class="control-label pull-right">Dose QTY : </label>
                                                                                    </div>
                                                                                    <div class="col">
                                                                                        <asp:DropDownList ID="ddlDoseQTY" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
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
                                                                                        <asp:DropDownList ID="ddlDoseUnit" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                       
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                       <%-- <asp:AsyncPostBackTrigger ControlID="ddlMedicine" EventName="SelectedIndexChanged" />--%>
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                                <div class="row mb-2">
                                                                    <div class="col-md-12">
                                                                        <div class="control-group row-fluid form-inline ">
                                                                            <div class="col-auto">
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
                                                                        <div class="col-md-12">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto" style="min-width:112px;">
                                                                                    <label class="control-label pull-right">Aux Label : </label>
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel1" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto" style="min-width:112px;">
                                                                                    
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel2" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                     <div class="row mb-2">
                                                                        <div class="col-md-12">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto" style="min-width:112px;">
                                                                                   
                                                                                </div>
                                                                                <div class="col w-75">
                                                                                    <asp:DropDownList ID="ddlAuxLabel3" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-6">
                                                                    <div class="row mb-2">
                                                                        <div class="col-md-12">
                                                                            <div class="control-group row-fluid form-inline ">
                                                                                <div class="col-auto" style="min-width:112px;">
                                                                                    <label class="control-label pull-right">Remark : </label>
                                                                                </div>
                                                                                <div class="col">
                                                                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm w-100" Rows="3"></asp:TextBox>
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
                                                                                <asp:Button ID="btnClearMedicine" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" />
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <div class="row" style="padding-bottom: 50px;">
                                                        <div class="col-md-12">
                                                            <div style="overflow-y: scroll; overflow-x: auto; height: 350px;" class="table-responsive text-nowrap">

                                                                <asp:GridView ID="gvMedicineList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                                    CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                                                    DataKeyNames="VN,VISITDATE,SUFFIX,SUBSUFFIX"
                                                                    OnRowCommand="gvMedicineList_RowCommand"
                                                                    OnRowDataBound="gvMedicineList_RowDataBound">
                                                                    <Columns>
                                                                         <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnDeleteMedicine" runat="server" Text="Del" CommandName="DeleteMedicineItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                         <asp:TemplateField>
                                                                            <ControlStyle CssClass="btn  btn-primary btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="80px"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="btnEditMedicine" runat="server" Text="Edit" CommandName="EditMedicineItem" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:ButtonField CommandName="EditMedicineItem" Text="Edit" ControlStyle-CssClass="btn btn-info btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                                                            <ControlStyle CssClass="btn  btn-info btn-sm"></ControlStyle>
                                                                            <HeaderStyle Width="10%"></HeaderStyle>
                                                                            <ItemStyle CssClass="text-center"></ItemStyle>
                                                                        </asp:ButtonField>--%>
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
                                                                          <asp:BoundField HeaderText="Amt" DataField="AMT" DataFormatString="{0:#,##0.00}">
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
                                                                        <asp:BoundField HeaderText="Dose Type" DataField="DOSETYPEINFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Dose Code" DataField="DOSECODEINFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Dose Qty" DataField="DOSEQTYINFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-right" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Dose Unit" DataField="DOSEUNITINFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Dose Memo" DataField="DOSEMEMO">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Charge#" DataField="ACTIVITYCODEINFO.ActivityName">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Aux1" DataField="AUXLABEL1INFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Aux2" DataField="AUXLABEL2INFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Aux3" DataField="AUXLABEL3INFO.Name">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Entry by" DataField="ENTRYBYUSERNAME">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Entry Date" DataField="MAKEDATETIME" DataFormatString="{0:dd/MM/yyyy HH:mm}">
                                                                            <HeaderStyle CssClass="text-center" />
                                                                            <ItemStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField HeaderText="Order Type" DataField="TYPEOFCHARGENAME">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <asp:BoundField HeaderText="Remark" DataField="REMARK">
                                                                            <HeaderStyle CssClass="text-left" />
                                                                        </asp:BoundField>
                                                                         <%--<asp:BoundField HeaderText="Group Code" DataField="GROUPCODE">
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
                                                    <asp:AsyncPostBackTrigger ControlID="btnAddMedicine" EventName="Click" />
                                                     <asp:AsyncPostBackTrigger ControlID="btnUpdateMedicine" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        
                                    </div>
                                </div>
                          <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                  
                </div>
            </div>






        </div>

    </div>
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
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <div class="container-fluid">
                                <div class="form-group">
                                    <div class="row mt-2 mb-2">
                                        <div class="col-md-4">
                                                                      
                                        </div>
                                                                    
                                        <div class="col-md-8">
                                            <div class="form-horizontal alert alert-danger" style="padding:15px;">
                                                <div class="control-group row-fluid form-inline" >
                                                        <div class="col-auto" style="min-width:117px;">
                                                        <label class="control-label pull-right font-weight-bold" >Add Item to Group Method : </label>
                                                    </div>
                                                    <div class="col">
                                                            <asp:DropDownList ID="ddlEditSetupGroupMethod" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row my-2">
                                        <div class="col-md-12">
                                            <div class="control-group row-fluid form-inline ">
                                                <div class="col-auto" style="min-width:112px;">
                                                    <label class="pull-right">Medicine : </label>
                                                </div>
                                                <div class="col">
                                                      <asp:Label runat="server" ID="lblMedicineName"></asp:Label>
                                            <asp:HiddenField runat="server" ID="hdMedicineCode" />
                                            <%--<asp:HiddenField runat="server" ID="hdMedicineGroupMethodID" />--%>
                                             <asp:HiddenField runat="server" ID="hdSubSuffix" />
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

    <script type="text/javascript">

        function showModalMedicine() {
            $("#modalEditMedicine").modal('show');
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
     
        function GetStockMasterByKey(stockCode,store,medicinePriceType) {
            var dataObject = JSON.stringify({
                "stockCode": stockCode,
                "store": store,
                "medicinePriceType" : medicinePriceType
            });

            $.ajax({
                type: "POST",
                url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("Ajax.aspx/GetStockMasterByKey")%>',
                data: dataObject,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                timeout: 30000,
                success: function (response, textStatus, jqXHR) {
                    var response = $.parseJSON(response.d);
                    var obj = (typeof response.d) == 'string' ? eval('(' + response + ')') : response;

                    if (obj.MedicineCode != null && obj.MedicineCode != '') {

                        $("#<%=ddlStore.ClientID%>").val(obj.STORE).change();
                        $("#<%=txtMedQTY.ClientID%>").val(obj.QTY);
                        $("#<%=txtMedAMT.ClientID%>").val(obj.AMT);
                        $("#<%=txtMedUnitPrice.ClientID%>").val(obj.UnitPrice);
                        $("#<%=hdChargeCode.ClientID%>").val(obj.ChargeCode);
                        $("#<%=ddlUnit.ClientID%>").val(obj.UnitCode).change(); 
                        $("#<%=ddlOrderType.ClientID%>").val(0).change();
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

                    $.notify("Error : " + jqXHR.statusText,
                        {
                            className: 'error',
                            position: 'bottom right',
                            clickToHide: true
                        }
                    );
                }
            });
         }


        function InitialControl() {

            $("#<%=txtMedQTY.ClientID%>").val('');
            $("#<%=txtMedAMT.ClientID%>").val('');
            $("#<%=txtMedUnitPrice.ClientID%>").val('');
            $("#<%=hdChargeCode.ClientID%>").val('');
            $("#<%=ddlUnit.ClientID%>").val(null).change();
            $("#<%=ddlOrderType.ClientID%>").val(0).change();
            $("#<%=ddlDoseType.ClientID%>").val(null).change();
            $("#<%=ddlDoseQTY.ClientID%>").val(null).change();
            $("#<%=ddlDoseUnit.ClientID%>").val(null).change();
            $("#<%=ddlDoseCode.ClientID%>").val(null).change();
            $("#<%=ddlAuxLabel1.ClientID%>").val(null).change();
            $("#<%=ddlAuxLabel2.ClientID%>").val(null).change();
            $("#<%=ddlAuxLabel3.ClientID%>").val(null).change();
            $("#<%=txtRemark.ClientID%>").val('');

        }

        $(document).ready(function () {

           // $('#<%=btnAddMedicine.ClientID%>').attr('disabled', 'disabled');

            $("#ddlMedicine").select2({
                ajax: {
                    delay: 200,
                    type: "POST",
                    url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("Ajax.aspx/SearchMedicine")%>',
                    contentType: "application/json; charset=utf-8",
                    data: function (params) {
                        return JSON.stringify({
                            'storeCode': $("[id*='ddlStore']").val(),
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
                                    store:item.STORE,
                                };
                            })
                            ,
                            pagination: { more: (params.page * 10) < data.Item2 }

                        };
                    }
                    , error: function (jqXHR, status, error) {
                        console.log(error + ": " + jqXHR.responseText);
                        return { results: [] }; // Return dataset to load after error
                    }
                },
                cache: true,
                placeholder: "Select Medicine",
                width: '100%',
                minimumInputLength: 4,
                allowClear: true,
            

            });



            $('#ddlMedicine').on('change', function () {
                var stockCode = $(this).val();
                data = $("#ddlMedicine").select2('data')[0];
                if (stockCode != null && stockCode != "") {
                    $('#<%=hfMedicine.ClientID%>').val(stockCode);
                    GetStockMasterByKey(stockCode,$("[id*='ddlStore']").val(), $("[id*='hdMedicinePriceType']").val());
                  }
              });



            $("#<%=ddlOrderType.ClientID%>,#<%=ddlStore.ClientID%>,#<%=ddlDoseUnit.ClientID%>,#<%=ddlUnit.ClientID%>,#<%=ddlDoseType.ClientID%>,#<%=ddlDoseQTY.ClientID%>,#<%=ddlDoseCode.ClientID%>,#<%=ddlAuxLabel1.ClientID%>,#<%=ddlAuxLabel2.ClientID%>,#<%=ddlAuxLabel3.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true

            });

            $("#<%=ddlSetupGroupMethod.ClientID%> , #<%=ddlEditSetupGroupMethod.ClientID%>").select2({

                placeholder: "-Setup Group Method-",
                width: '100%',
                allowClear: true

            });

            $("#<%=ddlEditOrderType.ClientID%>,#<%=ddlEditStore.ClientID%>,#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true

            });

            $('#ddlMedicine').change(function () {
                if ($(this).val() != "" && $(this).val() != null) {
                    $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                }
                else {
                    InitialControl();
                    $('#<%=btnAddMedicine.ClientID%>').attr('disabled', 'disabled');
                }
              });

            $('#<%=btnClearMedicine.ClientID%>').click(function (e) {
                e.preventDefault();
                //$("[id *= 'ddlMedicine'],[id *= 'ddlStore'], [id *= 'ddlOrderType'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").val(null).trigger('change');
                $("[id *= 'ddlMedicine'], [id *= 'ddlOrderType'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").val(null).trigger('change');
                $("[id *= 'txtMedQTY'],[id *= 'txtRemark']").val('');
            });



            $(".nav-tabs a").click(function () {
                $(this).tab('show');
            });

            $('.nav-tabs li:eq(0) a').tab('show');

            $("#<%=txtMedQTY.ClientID%> ").change(function () {
                CalculateMedPrice();
            });

            $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                CalculateEditMedPrice();
            });



            $("[id*='ddlStore']").change(function () {
                if ($(this).val() != "" && $(this).val() != null) {
                    //$("[id*='ddlMedicine']").val('').trigger('change')
                    InitialControl();
                }
            });

           

            $("#<%=btnAddMedicine.ClientID%>").click(function () {
                if ($("[id *= 'ddlMedicine']").val() == "" || $("[id *= 'ddlMedicine']").val() == null) {
                    setTimeout(function () {
                        {
                            $('[id*=ddlMedicine]').select2('open');
                        }
                    }, 500);
                }

                if ($("[id *= 'ddlSetupGroupMethod']").val() != "")
                {
                    return confirm('Confirm to add this item to "' + $("#<%=ddlSetupGroupMethod.ClientID%>").select2('data')[0].text+'" group method?');

                }
                
                
            }); 

            $("#<%=btnUpdateMedicine.ClientID%>").click(function () {


                if ($("[id *= 'ddlEditSetupGroupMethod']").val() != "") {
                    return confirm('Confirm to add this item to "' + $("[id *= 'ddlEditSetupGroupMethod']").find(":selected").text() + '" group method?');

                }


            }); 
            
            $("[id *= 'txtMedUnitPrice']").keydown(function (e) {
                return false;
            });

            //$('[id*=ddlMedicine]').attr('disabled', 'disabled');
            setTimeout( function() {
                if ($('[id*=ddlMedicine]').is(':disabled') == false) {
                       $('[id*=ddlMedicine]').select2('open');
                }
            }, 1000);
        });


        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {

                //$('#<%=btnAddMedicine.ClientID%>').attr('disabled', 'disabled');

                $("#ddlMedicine").select2({
                    ajax: {
                        delay: 200,
                        type: "POST",
                        url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("Ajax.aspx/SearchMedicine")%>',
                          contentType: "application/json; charset=utf-8",
                          data: function (params) {
                              return JSON.stringify({
                                  'storeCode': $("[id*='ddlStore']").val(),
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
                        , error: function (jqXHR, status, error) {
                            console.log(error + ": " + jqXHR.responseText);
                            return { results: [] }; // Return dataset to load after error
                        }
                      },
                      cache: true,
                      placeholder: "Select Medicine",
                      width: '100%',
                      minimumInputLength: 3,
                      allowClear: true,


                  });





                $('#ddlMedicine').on('change', function () {
                    var stockCode = $(this).val();
                    data = $("#ddlMedicine").select2('data')[0];
                    if (stockCode != null && stockCode != "") {
                        $('#<%=hfMedicine.ClientID%>').val(stockCode);
                        GetStockMasterByKey(stockCode,$("[id*='ddlStore']").val(),$("[id*='hdMedicinePriceType']").val());
                    }
                });


                $("#<%=ddlOrderType.ClientID%>,#<%=ddlStore.ClientID%>,#<%=ddlDoseUnit.ClientID%>,#<%=ddlUnit.ClientID%>,#<%=ddlDoseType.ClientID%>,#<%=ddlDoseQTY.ClientID%>,#<%=ddlDoseCode.ClientID%>,#<%=ddlAuxLabel1.ClientID%>,#<%=ddlAuxLabel2.ClientID%>,#<%=ddlAuxLabel3.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    width: '100%',
                    allowClear: true

                });

              
                $("#<%=ddlEditOrderType.ClientID%>,#<%=ddlEditStore.ClientID%>,#<%=ddlEditDoseUnit.ClientID%>,#<%=ddlEditUnit.ClientID%>,#<%=ddlEditDoseType.ClientID%>,#<%=ddlEditDoseQTY.ClientID%>,#<%=ddlEditDoseCode.ClientID%>,#<%=ddlEditAuxLabel1.ClientID%>,#<%=ddlEditAuxLabel2.ClientID%>,#<%=ddlEditAuxLabel3.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    width: '100%',
                    allowClear: true

                });

                $('#ddlMedicine').change(function () {

                    if ($(this).val() != "" && $(this).val() != null) {
                        $('#<%=btnAddMedicine.ClientID%>').removeAttr('disabled');
                    }
                    else {
                        InitialControl();
                    }
                   });


                $('#<%=btnClearMedicine.ClientID%>').click(function (e) {
                    e.preventDefault();
                    $("[id *= 'ddlMedicine'],[id *= 'ddlStore'], [id *= 'ddlOrderType'], [id *= 'ddlUnit'], [id *= 'ddlDoseType'], [id *= 'ddlDoseQTY'], [id *= 'ddlDoseUnit'], [id *= 'ddlDoseCode'], [id *= 'ddlAuxLabel1'], [id *= 'ddlAuxLabel2'], [id *= 'ddlAuxLabel3']").val(null).trigger('change');
                    $("[id *= 'txtMedQTY'],[id *= 'txtRemark']").val('');
                });


                $(".nav-tabs a").click(function () {
                    $(this).tab('show');
                });

                $('.nav-tabs li:eq(0) a').tab('show');

                $("#<%=txtMedQTY.ClientID%> ").change(function () {
                    CalculateMedPrice();
                });

                $("#<%=txtEditMedQTY.ClientID%> ").change(function () {
                    CalculateEditMedPrice();
                });

                $("[id*='ddlStore']").change(function () {
                    if ($(this).val() != "" && $(this).val() != null) {
                        //$("[id*='ddlMedicine']").val('').trigger('change')
                        InitialControl();
                    }
                });

                $("#<%=btnAddMedicine.ClientID%>").click(function () {
                    if ($("[id *= 'ddlMedicine']").val() == "" || $("[id *= 'ddlMedicine']").val() == null) {
                        setTimeout(function () {
                            {
                                $('[id*=ddlMedicine]').select2('open');
                            }
                        }, 500);
                    }
                }); 

               
                $("[id*='txtMedUnitPrice']").keydown(function (e) {
                    return false;
                });

            });
        });

      

    </script>
</asp:Content>