<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Anesthesia.aspx.cs" Inherits="solution.Reserve.Anesthesia" %>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">รายงานการส่งผ่าตัด
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-bar-chart"></i><span class="nav-link-text">Anesthesia</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-2 text-right">
                <label>วันที่จอง :&nbsp;&nbsp;</label>
            </div>
            <div class="col-md-3">
                <div class="form-inline">
                    <div class="input-group">
                        <input id="txtDateFrom" name="txtDateFrom" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy">
                        <span class="input-group-btn">
                            <label class="btn btn-sm btn-info">To</label>
                        </span>
                        <input id="txtDateTo" name="txtDateTo" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy">
                    </div>
                    <asp:HiddenField ID="hdDateFrom" runat="server" />
                    <asp:HiddenField ID="hdDateTo" runat="server" />
                </div>
            </div>
        </div>
        <asp:UpdatePanel runat="server" ID="upddl">
            <ContentTemplate>
                <div class="row">
                    <div class="col-md-2 text-right">
                        <%--<label>Doctor :</label>--%>
                    </div>
                    <div class="col-md-3">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="true" CssClass="form-control input-group-sm" Visible="false">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-5">
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-sm btn-primary mousecursor" Text="  ค้นหา  " OnClick="btnSearch_Click" />
                    </div>
                </div>
                <hr />
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-11 pull-right">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:GridView ID="gvAnesthesia" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia_RowCreated" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField HeaderText="Anesthesia Type" DataField="AnesthesiaTypeName">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-4">
                                <asp:GridView ID="gvAnesthesia1" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia1_RowCreated" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField HeaderText="(+)Anesthesia Type" DataField="AnesthesiaTypeName">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-4">
                                <asp:GridView ID="gvAnesthesia2" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvAnesthesia2_RowCreated" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField HeaderText="(+-)Anesthesia Type" DataField="AnesthesiaTypeName">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="QTY" DataField="AnesthesiaTypeQTY">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {
            //Strat Date From=====================>
            var ordateFrom = document.getElementById('<%=hdDateFrom.ClientID %>').value;
            $('#txtDateFrom').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                thaiyear: false              //Set เป็นปี พ.ศ.
            }).datepicker("setDate", ordateFrom);  //กำหนดเป็นวันปัจุบัน

            var xdate = $('#txtDateFrom').val();
            document.getElementById('<%=hdDateFrom.ClientID %>').value = xdate;
            //End Date From=====================>
            //Strat Date To=====================>
            var ordateTo = document.getElementById('<%=hdDateTo.ClientID %>').value;
            $('#txtDateTo').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                thaiyear: false              //Set เป็นปี พ.ศ.
            }).datepicker("setDate", ordateTo);  //กำหนดเป็นวันปัจุบัน

            var xdateTo = $('#txtDateTo').val();
            document.getElementById('<%=hdDateTo.ClientID %>').value = xdateTo;
            //End Date To=====================>
        });

        $("#txtDateFrom").on("change", function () {
            var xdateFrom = $(this).val();
            document.getElementById('<%=hdDateFrom.ClientID %>').value = xdateFrom;
        });

        $("#txtDateTo").on("change", function () {
            var xdateTo = $(this).val();
            document.getElementById('<%=hdDateTo.ClientID %>').value = xdateTo;
        });


        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>
</asp:Content>



