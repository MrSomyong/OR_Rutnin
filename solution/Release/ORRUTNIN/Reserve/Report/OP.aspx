<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OP.aspx.cs" Inherits="solution.Reserve.OP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">รายงานการส่งผ่าตัด
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-bar-chart"></i><span class="nav-link-text">Operation + Procedure</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-2 text-right">
                <label>วันที่จอง :&nbsp;&nbsp;</label>
            </div>
            <div class="col-3">
                <div class="form-inline">
                    <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
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
                    <div class="col-2 text-right">
                        <label>Doctor :</label>
                    </div>
                    <div class="col-3">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="true" CssClass="form-control input-group-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2 text-right">
                        <label>Operation :</label>
                    </div>
                    <div class="col-3">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlOperation" runat="server" AutoPostBack="true" CssClass="form-control input-group-sm" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2 text-right">
                        <label>Procedure :</label>
                    </div>
                    <div class="col-3">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlProcedure" runat="server" AutoPostBack="true" CssClass="form-control input-group-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-2 text-right">
                        <label>Anesthesia Type :</label>
                    </div>
                    <div class="col-3">
                        <div class="input-group">
                            <asp:DropDownList ID="ddlAnesthesiaType" runat="server" AutoPostBack="true" CssClass="form-control input-group-sm">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-5">
                        <asp:Button runat="server" ID="btnSearch" CssClass="btn btn-sm btn-primary mousecursor" Text="  ค้นหา  " OnClick="btnSearch_Click1" />
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <hr />
        <div class="row">
            <div class="col-12">
                <div class="row">
                    <div class="col-9 pull-right">
                        <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                    </div>
                    <div class="col-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvOperation" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%" OnRowCreated="gvOperation_RowCreated" ShowFooter="True">
                                    <Columns>
                                        <asp:BoundField HeaderText="Operation" DataField="OROPERATIONVO.Name">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Procedure" DataField="OROPERATIONVO.SubName">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-left" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Qty" DataField="OROPERATIONVO.QTY">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                    </Columns>
                                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                                    <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
            </div>
        </div>
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
