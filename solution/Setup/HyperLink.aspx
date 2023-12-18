<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HyperLink.aspx.cs" Inherits="solution.Setup.HyperLink" %>
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
        #MainContent_gvSetupHyperLinkList tr td ,
        #MainContent_gvSetupHyperLinkList tr th 
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

        ul.square-bullets {
            list-style-type: square;
            padding: 0; /* Remove padding */
            margin: 0 0 0 20px; 
        }
    </style>
    <div class="container-fluid">

        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-link"></i><span class="nav-link-text">Setup Hyperlink</span></h4>
            </div>
        </div>
        
        <hr />

        <div class="row" style="padding-top: 10px;">

            <div class="col-md-10">
                <div style="height: 350px;" class="table-responsive ">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                        <asp:HiddenField runat="server" ID="hfLinkCode" />
                        <asp:GridView ID="gvSetupHyperLinkList" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered pre-scrollable"
                            DataKeyNames="LinkCode,LinkName,LinkURL,IsShow"
                            OnRowCommand="gvSetupHyperLinkList_RowCommand"
                            OnRowDataBound="gvSetupHyperLinkList_RowDataBound"
                            OnRowCreated="gvSetupHyperLinkList_RowCreated"
                            ShowFooter="true">
                            <columns>
                                <asp:TemplateField>
                                    <ControlStyle CssClass="btn btn-danger btn-sm"></ControlStyle>
                                    <HeaderStyle Width="80px"></HeaderStyle>
                                    <ItemStyle CssClass="text-center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Button ID="btnDeleteLink" runat="server" Text="Del" CommandName="DeleteLink" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" OnClientClick="return confirm('Confirm to delete?');" />
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Button ID="btnAddLink" runat="server" Text="Add Link" CssClass="btn btn-info btn-sm" OnClick="btnAddLink_Click" />
                                    </FooterTemplate>
                                </asp:TemplateField>
                                
                                    <asp:ButtonField  CommandName="EditLink" Text="Edit" ControlStyle-CssClass="btn  btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:TemplateField HeaderText="Show">
                                        <HeaderStyle CssClass="text-center" Width="80px"/>
                                        <ItemStyle CssClass="text-center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblShow" runat="server"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Code" DataField="LinkCode">
                                        <HeaderStyle CssClass="text-center" Width="10%"/>
                                        <ItemStyle CssClass="text-left " />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Name" DataField="LinkName">
                                        <HeaderStyle CssClass="text-center" Width="20%" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                 <asp:BoundField HeaderText="Link" DataField="LinkURL">
                                        <HeaderStyle CssClass="text-center" Width="60%" />
                                        <ItemStyle CssClass="text-left" />
                                    </asp:BoundField>
                                
                                </columns>
                            <emptydatarowstyle cssclass="alert-secondary text-center" />
                            <headerstyle cssclass="table-info " />
                             
                        </asp:GridView>
                            </ContentTemplate>
                        <Triggers>
                           <%-- <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="gvSetupHyperLinkList" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
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


    <div class="modal fade bd-example-modal-md" id="modalHyperLink" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document" style="max-width:700px;">
            <div class="modal-content">
                 <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                <div class="modal-header">
                    <h6 class="modal-title" id="htmlTitleHyperLink" runat="server">Edit Hyperlink</h6>
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
                                            <label class="pull-right">Code : </label>
                                        </div>
                                        <div class="col-3">
                                            <asp:TextBox ID="txtLinkCode" runat="server" CssClass="form-control form-control-sm" onFocus="this.select()" ></asp:TextBox>
                                            <asp:HiddenField runat="server" ID="hdLinkCode" />
                                            <asp:HiddenField runat="server" ID="hdLinkName" />
                                            <asp:HiddenField runat="server" ID="hdURL" />
                                            <asp:HiddenField runat="server" ID="hdIsShow" />
                                        </div>
                                    </div>
                                    <div class="row mb-2">
                                        <div class="col-4">
                                            <label class="pull-right">Name : </label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtLinkName" runat="server" CssClass="form-control form-control-sm" ></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="row mb-2">
                                        <div class="col-4">
                                            <label class="pull-right">URL : </label>
                                        </div>
                                        <div class="col-8">
                                            <asp:TextBox ID="txtLinkURL" runat="server" TextMode="MultiLine" CssClass="form-control form-control-sm" Rows="3"></asp:TextBox>
                                        </div>

                                    </div>
                                      <div class="row mb-2">
                                        <div class="col-4">
                                           
                                        </div>
                                          <div class="col-8">
                                              <div class="form-check">
                                                  <span class="custom form-check-input">
                                                      <asp:CheckBox ID="cbIsShow" runat="server"/>
                                                      <asp:Label ID="lblShow" runat="server" Text="Show"  CssClass="form-check-label" AssociatedControlID="cbIsShow">
                                                      </asp:Label> 
                                                  </span>

                                              </div>
                                          </div>
                                    </div>
                                            
                                                 <div class="row mb-2">
                                                    <div class="col-4">
                                                        <label class="pull-right">Parameter : </label>
                                                    </div>
                                                    <div class="col-8">
                                                        <ul class="square-bullets">
                                                            <li>HN</li>
                                                            <li>VN</li>
                                                            <li>Visitdate</li>
                                                            <li>UserID</li>
                                                        </ul>
                                                    </div>
                                        
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnUpdateLink" runat="server" CssClass="btn btn-primary btn-sm"   Text="Update"  OnClick="btnUpdateLink_Click" ></asp:Button>
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
         function showModalHyperLink() {
            $("#modalHyperLink").modal('show');
        }
        $(document).ready(function () {


        });

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_endRequest(function () {

            $(document).ready(function () {

           
        
            

            });
        });

    </script>
    </asp:Content>
