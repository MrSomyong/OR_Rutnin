<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Display.aspx.cs" Inherits="solution.PostOR.Display" %>

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
            <li class="breadcrumb-item active">Display</li>
        </ol>
        <br />
        <div class="row" style="font-size: 1.5rem;">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">ORDate :</span>
                    <asp:Label ID="lblORDate" runat="server"></asp:Label></h4>
            </div>
            <div class="col-md-6">
                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                    <a href="/Print/StickerORpost/?o=<%= hdORID.Value %>" style="width: 130px" target="_blank" class="btn btn-sm btn-primary pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i>  Sticker OR</a>
                </div>
                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                    <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i>  Print Display</asp:LinkButton>
                </div>
                <div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">
                    <a href="/Print/HNIdentify/?o=<%= hdORID.Value %>" style="width: 200px" target="_blank" class="btn btn-sm btn-primary pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i>  Print HN# Identification</a>
                </div>
            </div>
        </div>
        <div class="row" style="font-size: 1.5rem;">
            <%--<div class="col-8 text-center">
            </div>--%>
            <div class="col-12 text-right">
                <%--<h4><span class="nav-link-text">แพทย์ :</span></h4>--%>
                    <h4><asp:Label ID="lblDoctor" runat="server" Font-Size=40></asp:Label></h4>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <div class="row">
                <div class="col-2">
                    <asp:Image runat="server" ID="imgPatient" ImageUrl="../Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
                </div>
                <div class="col-10">
                    <div class="row" style="font-size: 1.5rem;">
                        <div class="col-12 text-left">
                        <h4><span class="nav-link-text">ชื่อ-สกุล :</span>
                            <asp:Label ID="lblPatientName" runat="server" Font-Size=50></asp:Label></h4>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-3" style="font-size: 1.5rem;">
                            <label>HN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblHN" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                            <asp:HiddenField runat="server" ID="hdORID" />
                        </div>
                        <div class="col-5" style="font-size: 1.5rem;">
                            <label>อายุ&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblAge" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                        </div>
                        <div class="col-4" style="font-size: 1.5rem;">
                            <label>Nationality&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblNationality" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                        </div>
                    </div>
                    <div class="row" id="divANVN" runat="server" visible="false">
                        <div class="col-12" style="font-size: 1.5rem;" id="divAN" runat="server" visible="false">
                            <label>AN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblAN" Font-Bold="true" runat="server"  Font-Size =25></asp:Label>
                        </div>
                        <div class="col-12" style="font-size: 1.5rem;" id="divVN" runat="server" visible="false">
                            <label>VN&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblVN" Font-Bold="true" runat="server" Font-Size =25></asp:Label>&nbsp;&nbsp;&nbsp;
                            <label>Visit Date&nbsp;:&nbsp;</label>
                            <asp:Label ID="lblVN_VisitDate" Font-Bold="true" runat="server" Font-Size =25></asp:Label>
                        </div>
                    </div>


                </div>

            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-4" style="border-right: 1px solid #e9ecef; font-size: 1.3rem;">
                <div class="row">
                    <div class="col-12">
                        <label>Underlying disease :&nbsp;</label>
                        <asp:Label ID="lblPatientalDiag" runat="server"></asp:Label>
                        <asp:Label ID="lblPatientalDiagDesc" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
                        <i id="chOnmedTure" runat="server" class="fa fa-check" aria-hidden="true" style="color: darkgreen"></i>
                        <i id="chOnmedFalse" runat="server" class="fa fa-times" aria-hidden="true" style="color: brown"></i>
                        <asp:Label runat="server" ID="lblCheckOnmed">&nbsp;ON med&nbsp;</asp:Label>
                        <asp:Label runat="server" CssClass="word-break" ID="lblOnmed"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-12">
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
                <div class="form-group">
                    <div class="row">
                        <div class="col-12">
                            <asp:Label ID="Label2" runat="server">Allegic&nbsp;:&nbsp;</asp:Label>
                            <asp:Label ID="lblPatientallegic" runat="server"></asp:Label>
                        </div>
                    </div>
                            <asp:Label ID="AllergicOtherLabel" runat="server">Allegic Other&nbsp;:&nbsp;</asp:Label>
                            <asp:Label ID="lblPatientallegicOther" runat="server"></asp:Label>
                    <br />
                    <div class="form-group mt-2">
                        <div class="row">
                            <div class="col-12">
                                <asp:Label ID="Label3" runat="server">S/P : </asp:Label>
                                <asp:Label ID="lblPrevOR" runat="server" Text="ไม่มี"></asp:Label>
                                <div class="row col-12">
                                    <asp:Label ID="lblPrevORImplant" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-8" style="font-size: 1.3rem;">
                <div class="row mt-1" runat="server" id="div1">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body" id="Div2" runat="server">
                                <div class="row">
                                    <div class="col-6" style="border-right: 2px solid silver;">
                                        <asp:Label ID="Label1" runat="server" Text="Dx."></asp:Label>                                        
                                        <asp:Label ID="lblPrediag" runat="server" Font-Bold="true" Font-Size =20></asp:Label>
                                    </div>
                                    <div class="col-6">
                                        <asp:Label ID="Label4" runat="server" Text="Under :"></asp:Label>
                                        <asp:Label ID="lblAnesthesiaType" runat="server" Font-Bold="true" Font-Size =20></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 <%--<div class="row mt-1" runat="server" id="divSelectOR">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body" id="Div4" runat="server">
                                <div class="row">
           <div class="col-2">
             <asp:Button ID="btnADC" class="btn btn-primary pull-right" runat="server" Text="Operation ADC"  OnClick="ADC_Click" />
               <asp:Button ID="btnADC2" class="btn btn-secondary pull-right" runat="server" Text="Operation ADC"  OnClick="ADC_Click" />
            </div>
            <div class="col-2">
             <asp:Button ID="btnPostOR" class="btn btn-primary pull-right" runat="server" Text="Operation Post OR"  OnClick="PostOR_Click" />
                <asp:Button ID="btnPostOR2" class="btn btn-secondary pull-right" runat="server" Text="Operation Post OR"  OnClick="PostOR_Click" />
            </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
                <div class="row mt-1" runat="server" id="divSite">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body" id="Site" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:Label ID="lblSite" runat="server" Font-Size=50></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row mt-1" runat="server" id="divImplant">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-body" runat="server">
                                <div class="row">
                                    <div class="col-12">
                                        <asp:UpdatePanel ID="upImplant" runat="server">
                                            <ContentTemplate>
                                                <asp:GridView ID="gvImplant" runat="server"
                                                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                                    AutoGenerateColumns="False"
                                                    CssClass="table table-bordered table-hover table-responsive"
                                                    DataKeyNames="ID,PostOperation_ID,MainCode,SubCode,SubName,Used"
                                                    OnRowDataBound="gvImplant_RowDataBound"
                                                    OnRowCommand="gvImplant_RowCommand"
                                                    OnRowEditing="gvImplant_RowEditing">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Implant">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Eval("ID") %>' />
                                                                <asp:HiddenField ID="hdgvMainCode" runat="server" Value='<%# Eval("MainCode") %>' />
                                                                <asp:HiddenField ID="hdgvSubCode" runat="server" Value='<%# Eval("SubCode") %>' />
                                                                <asp:HiddenField ID="hdUsed" runat="server" Value='<%# Eval("Used") %>' />
                                                                <asp:Label ID="lblgvSubName" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="word-break" Font-Size="Large" />
                                                            <HeaderStyle Font-Size="Large" CssClass="text-center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Remark">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="word-break" Font-Size="Large" />
                                                            <HeaderStyle Font-Size="Large" CssClass="text-center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnUsed" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Use" CommandName="select" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                                <asp:Button ID="btnImg" runat="server" CssClass="btn btn-info btn-sm mousecursor" Text="รูป" CommandName="img" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="text-center" HorizontalAlign="Center" Width="100px"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                                    <HeaderStyle CssClass="table-success" />
                                                </asp:GridView>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
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
                        <asp:UpdatePanel runat="server" ID="UpdatePanel13">
                            <ContentTemplate>
                                <br />
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
                        </asp:UpdatePanel>
                    </div>
                    <div class="modal-footer">
                        <asp:Button CssClass="btn btn-primary btn-sm mousecursor" ID="btnSaveImageImplant" Text="Save" runat="server" OnClick="btnSaveImageImplant_Click" />
                        <button type="button" class="btn btn-secondary btn-sm mousecursor" data-dismiss="modal">Cancel</button>
                    </div>
                </div>
            </div>
        </div>
        <hr />
    </div>

    <div class="modal fade" id="modalPopProcedureSelection" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Procedure Selection</h5>
                    <%--<button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>--%>
                </div>
                <div class="modal-body">
                    <div class="row">
           <div class="col-6">
             <asp:Button ID="btnADC" class="btn btn-primary  pull-right" runat="server" Text="Operation ADC"  OnClick="ADC_Click" />
               <asp:Button ID="btnADC2" class="btn btn-primary  pull-right" runat="server" Text="Operation ADC"  OnClick="ADC_Click" />
            </div>
            <div class="col-6">
             <asp:Button ID="btnPostOR" class="btn btn-primary" runat="server" Text="Operation Post OR"  OnClick="PostOR_Click" />
                <asp:Button ID="btnPostOR2" class="btn btn-primary" runat="server" Text="Operation Post OR"  OnClick="PostOR_Click" />
            </div>
                                </div>
                </div>
                <div class="modal-footer">
<%--                    <asp:Button ID="Button5" runat="server" CssClass="btn btn-primary" OnClick="btnSaveSetupProcedure_Click" Text="Save" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>--%>
                </div>
            </div>
        </div>
    </div>

    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>
    <script type="text/javascript">

        function showModalPopProcedureSelection() {
            $("#modalPopProcedureSelection").modal('show');
        }
        function closeModalPopProcedureSelection() {
            $("#modalPopProcedureSelection").modal('hide');
        }
  
    </script>

    <script>

        function SetTarget() {
            document.forms[0].target = "_blank";
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

    </script>
</asp:Content>
