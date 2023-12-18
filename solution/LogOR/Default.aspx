<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.LogOR.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/LogOR/">Audit Log</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-eye"></i><span class="nav-link-text">Audit Log</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <div class="form-inline">
                    <label class="pull-right">วันที่&nbsp;&nbsp;:&nbsp;&nbsp;</label>
                    <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                        <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                        <span class="input-group-btn">
                            <asp:Button CssClass="btn btn-sm btn-info" ID="btnSearch" Text="ค้นหา" runat="server" OnClick="btnSearch_Click" />
                        </span>
                    </div>
                    <asp:HiddenField ID="hdDate" runat="server" />
                </div>
            </div>

        </div>
        <div class="row">
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <div class="row">
                    <div class="col-10">
                    </div>
                    <div class="col-2 pull-right">
                        <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvLogOR" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                OnRowDataBound="gvLogOR_RowDataBound"
                                OnRowCommand="gvLogOR_RowCommand">
                                <Columns>
                                    <asp:BoundField HeaderText="Date" DataField="UpdateDate" dataformatstring="{0:d MMMM yyyy}" htmlencode="false">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Time" DataField="UpdateDate" dataformatstring="{0:HH:mm}" htmlencode="false">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField HeaderText="ORID" DataField="ORID">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>--%>
                                    <asp:BoundField HeaderText="HN" DataField="HN">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Patient Name" DataField="PatientName">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="รายละเอียด" DataField="Detail" htmlencode="false">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <%--<asp:BoundField HeaderText="โดย" DataField="FirstName">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>--%>
                                </Columns>
                                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

    <!-- Modal -->
    <div class="modal fade bd-example-modal-lg" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">ข้อมูลการจองห้อง</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            var ordate = document.getElementById('<%=hdDate.ClientID %>').value;
            $('#txtDate').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                thaiyear: false              //Set เป็นปี พ.ศ.
            }).datepicker("setDate", ordate);  //กำหนดเป็นวันปัจุบัน

            var xdate = $('#txtDate').val();
            document.getElementById('<%=hdDate.ClientID %>').value = xdate;


        });

        $("#txtDate").on("change", function () {

            var xdate = $(this).val();
            document.getElementById('<%=hdDate.ClientID %>').value = xdate;

        });
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>
</asp:Content>
