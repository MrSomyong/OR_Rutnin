<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Computer.aspx.cs" Inherits="solution.Setup.Computer" %>
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
        #MainContent_gvSetupComputerList tr td ,
        #MainContent_gvSetupComputerList tr th 
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
        [id*="DefaultClinic"] + span.select2 {
            line-height: 1.5 !important;
             margin-left: 0px;
             margin-right: -20px;
        }
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs--> 
        <ol class="breadcrumb">
            
            <li class="breadcrumb-item active">Setup ComputerName</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-cogs"></i><span class="nav-link-text">Setup Computer Name</span></h4>
            </div>
           
        </div>
        <div class="row">
            <div class="col-md-9">
                <div class="form-horizontal">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <div class="container alert alert-primary">

                                <div class="control-group ">
                                    <div class="row my-2">
                                        <div class="col-md-12">
                                            <div class="form-horizontal">
                                                <div class="control-group  form-inline ">
                                                    <div class="col-auto">
                                                        <label class="pull-right">Computer Code : </label>
                                                    </div>
                                                    <div class="col input-group">
                                                        <asp:TextBox ID="txtComputerCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>

                                                    <div class="col-auto text-right">
                                                        <label class="pull-right">Computer Name : </label>
                                                    </div>
                                                    <div class="col input-group">
                                                        <asp:TextBox ID="txtComputerName" runat="server" CssClass="form-control"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="form-horizontal">
                                                <div class="control-group form-inline ">
                                                    <div class="col-auto ">
                                                        <label class="pull-right">Default Store : </label>
                                                    </div>
                                                    <div class="col">
                                                        <asp:DropDownList ID="ddlDefaultStore" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                     <div class="row my-2">
                                        <div class="col-md-12">
                                            <div class="form-horizontal">
                                                <div class="control-group form-inline ">
                                                    <div class="col-auto ">
                                                        <label class="pull-right">Default Clinic : </label>
                                                    </div>
                                                    <div class="col">
                                                        <asp:DropDownList ID="ddlDefaultClinic" runat="server" AutoPostBack="false" CssClass="form-control d-block" multiple="multiple" ></asp:DropDownList>
                                                         <asp:HiddenField ID="hfClinic" runat="server" />
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-md-12 mt-3">
                                            <div class="control-group">
                                                <div class="col-md-12 text-center">
                                                    <asp:Button ID="btnAdd" runat="server" Text="Save" CssClass="btn btn-success btn-sm mr-2 px-4 " OnClick="btnAdd_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4" OnClick="btnClear_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvSetupComputerList"   />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <hr />

        <div class="row" style="padding-top: 10px;">

            <div class="col-md-7">
                <div style="overflow-y: scroll; overflow-x: auto; height: 500px;" class="table-responsive ">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                        <asp:HiddenField runat="server" ID="hfComputerCode" />
                        <asp:GridView ID="gvSetupComputerList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered pre-scrollable"
                            DataKeyNames="ComputerCode,ComputerName,DefaultStoreCode,DefaultClinicCode"
                            OnRowCommand="gvSetupComputerList_RowCommand"
                            OnRowDataBound="gvSetupComputerList_RowDataBound">
                            <columns>
                                <asp:TemplateField>
                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDeleteComputer" runat="server" Text="Del" CommandName="DeleteComputer" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                    <asp:ButtonField  CommandName="EditComputer" Text="Edit" ControlStyle-CssClass="btn  btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:BoundField HeaderText="Computer Code" DataField="ComputerCode">
                                        <HeaderStyle CssClass="text-center" Width="25%"/>
                                        <ItemStyle CssClass="text-left " />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Computer Name" DataField="ComputerName">
                                        <HeaderStyle CssClass="text-center" Width="30%" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                 <asp:BoundField HeaderText="Default Store" DataField="StoreInfo.StoreName">
                                        <HeaderStyle CssClass="text-center" Width="30%" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                
                                </columns>
                            <emptydatarowstyle cssclass="alert-secondary text-center" />
                            <headerstyle cssclass="table-info " />
                        </asp:GridView>
                            </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvSetupComputerList" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-md-5">
                 <div class="alert alert-primary py-3">
                      <div class="row">
                        <div class="col-md-12">
                            <div class="form-horizontal">
                                <div class="control-group row-fluid form-inline">
                                    <div class="col-auto" style="width:170px">
                                    <label class="control-label pull-right">Treatment Method : </label>
                                    </div>
                                    <div class="col">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DropDownList ID="ddlTreatmentMethod" runat="server" AutoPostBack="false" CssClass="form-control d-block"></asp:DropDownList>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="btnClearTreatmentMethod" EventName="Click" />
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
                                    <asp:Button ID="btnAddTreatmentMethod" runat="server" Text="Save" CssClass="btn btn-success btn-sm px-4 mr-2"  OnClick="btnAddTreatmentMethod_Click" />
                                    <asp:Button ID="btnClearTreatmentMethod" runat="server" Text="Clear" CssClass="btn btn-secondary btn-sm px-4"  OnClick="btnClearTreatmentMethod_Click"/>
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
                                           <%-- <asp:HiddenField runat="server" ID="hfGroupMethodID" />--%>
                                            <asp:GridView ID="gvTreatmentList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                                CssClass="table table-striped table-bordered pre-scrollable"
                                                DataKeyNames="ComputerMethodID,ComputerCode,MethodCode,MethodName"
                                                OnRowCommand="gvTreatmentList_RowCommand"
                                                OnRowDataBound="gvTreatmentList_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                                        <HeaderStyle Width="10%"></HeaderStyle>
                                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnDeleteGroupMethod" runat="server" Text="Del" CommandName="DeleteTreatmentMethod" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                    <asp:BoundField HeaderText="Method Code" DataField="MethodCode">
                                                        <HeaderStyle CssClass="text-left" Width="20%" />
                                                        <ItemStyle CssClass="text-left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="Method Name" DataField="MethodName">
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
                                        <asp:AsyncPostBackTrigger ControlID="btnAddTreatmentMethod" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="gvSetupComputerList" />
                                        <asp:AsyncPostBackTrigger ControlID="gvTreatmentList" />
                                        
                                    </Triggers>
                                </asp:UpdatePanel>
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
    <script src="https://code.jquery.com/jquery-3.4.0.js"></script>
    <script src="../../Scripts/select2.min.js"></script>
    <script src="../../Scripts/notify.js"></script>

    <script type="text/javascript">

        $(document).ready(function () {

            $('#<%=btnClearTreatmentMethod.ClientID%>').prop('disabled', true);
            $('#<%=btnAddTreatmentMethod.ClientID%>').attr('disabled', 'disabled');
            $("#<%=ddlDefaultStore.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '100%',
                allowClear: true
             
            });
            $("#<%=ddlDefaultClinic.ClientID%>").select2({

                placeholder: "-Please Select-",
                tags: true,
                tokenSeparators: [',', ' '],
                closeOnSelect:true,
                maximumSelectionLength: 6
            });

             $("#<%=ddlDefaultClinic.ClientID %>").change(function () {
                $("#<%=hfClinic.ClientID %>").val($(this).val());
            })
            $('#<%=btnClear.ClientID%>').click(function (e) {
                //e.preventDefault();
                $("[id *= 'ddlDefaultStore']").val(null).trigger('change');
                $("[id *= 'ddlDefaultClinic']").val(null).trigger('change');
                $("[id *= 'txtComputerCode'] , [id *= 'txtComputerName']").val('');
            });

            

            $('#<%=btnClear.ClientID%>').click(function (e) {
                e.preventDefault();
                $("[id *= 'ddlDefaultStore']").val(null).trigger('change');
                $("[id *= 'ddlDefaultClinic']").val(null).trigger('change');
                $("[id *= 'txtComputerCode'] , [id *= 'txtComputerName']").val('');
                $("[id *= 'ddlTreatmentMethod']").val(null).trigger('change');
                $("[id *= 'ddlTreatmentMethod']").prop('disabled', true).trigger('change');
                $("[id *= 'ddlTreatmentMethod']").attr('disabled', 'disabled'); 
                $('#<%=btnClearTreatmentMethod.ClientID%>').prop('disabled', true);

            });
           
              $("#<%=ddlTreatmentMethod.ClientID%>").select2({

                placeholder: "Select Method",
                width: '100%',
                allowClear: true,
                disabled: true,
              });
          
           $('#<%=ddlTreatmentMethod.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddTreatmentMethod.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddTreatmentMethod.ClientID%>').attr('disabled', 'disabled');

                });

          $('#<%=btnAddTreatmentMethod.ClientID%>').click(function (e) {

                if ($("[id *= 'ddlTreatmentMethod']").val() != "" && $("[id *= 'ddlTreatmentMethod']").val() != null) {
                     //setTimeout(function () {
                        $('#<%=btnAddTreatmentMethod.ClientID%>').removeAttr('disabled');
                     //}, 500);
                 }
            });

        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {
                $('#<%=btnAddTreatmentMethod.ClientID%>').attr('disabled', 'disabled');
                $("#<%=ddlDefaultStore.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    width: '100%',
                    allowClear: true

                });
                 $("#<%=ddlDefaultClinic.ClientID%>").select2({

                    placeholder: "-Please Select-",
                    tags: true,
                    tokenSeparators: [',', ' '],
                    closeOnSelect:true,
                    maximumSelectionLength: 6
                 });
             $("#<%=ddlDefaultClinic.ClientID %>").change(function () {
                $("#<%=hfClinic.ClientID %>").val($(this).val());
            })
                
                $('#<%=btnClear.ClientID%>').click(function (e) {
                   // e.preventDefault();
                    $("[id *= 'ddlDefaultStore']").val(null).trigger('change');
                    $("[id *= 'ddlDefaultClinic']").val(null).trigger('change');
                    $("[id *= 'txtComputerCode'] , [id *= 'txtComputerName']").val('');
                    $("[id *= 'ddlTreatmentMethod']").val(null).trigger('change');
                    $("[id *= 'ddlTreatmentMethod']").prop('disabled', true).trigger('change');
                    $("[id *= 'ddlTreatmentMethod']").attr('disabled', 'disabled'); 
                    $('#<%=btnClearTreatmentMethod.ClientID%>').prop('disabled', true);
                });
                 $('#<%=ddlTreatmentMethod.ClientID%>').change(function () {
                    if ($(this).val() != "" && $(this).val() != null) 
                        $('#<%=btnAddTreatmentMethod.ClientID%>').removeAttr('disabled');
                    else
                        $('#<%=btnAddTreatmentMethod.ClientID%>').attr('disabled', 'disabled');

                });
              

            });
        });

    </script>
    </asp:Content>
