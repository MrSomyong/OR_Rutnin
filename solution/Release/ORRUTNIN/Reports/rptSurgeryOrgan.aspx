<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptSurgeryOrgan.aspx.cs" Inherits="solution.Reports.rptSurgeryOrgan" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">รายงานการผ่าตัดแยกตาม-Organ
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-bar-chart"></i><span class="nav-link-text">รายงานการผ่าตัดแยกตาม-Organ.</span></h4>
            </div>
        </div>
        <hr />
        <div class="row">

            <div class="col-2 text-left">
                <label>วันที่จอง :</label>
            </div>
            <div class="col-2">
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
        <div class="row">
                <div class="col-2 text-left">
                <label>Cxl Check :</label>
            </div>
            <div class="col-2">                
                <asp:DropDownList ID="ddlCxlCheck" runat="server" CssClass="form-control input-group-sm"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
                <div class="col-2 text-left">
                <label>Cxl Confirm :</label>
            </div>
            <div class="col-2">                
                <asp:DropDownList ID="ddlCxlConfirm" runat="server" CssClass="form-control input-group-sm"></asp:DropDownList>
            </div>
        </div>
        <div class="row">
            <div class="col-12 pull-right">                
                <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()">Print</asp:LinkButton>
                <asp:Button ID="btnExportExcel" runat="server" CssClass="btn btn-sm btn-success mousecursor fa-pull-right m-1" Text="Export Excel" OnClick="btnExportExcel_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="btnSearch_Click" />
            </div>
            </div>
        <hr />
        <div>
            <div class="row" style="overflow-x: auto; height: 680px">
                <div class="col-12">
                    <asp:GridView ID="gvData" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                        CssClass="table table-bordered table-hover pre-scrollable">
                        <Columns>
                            <asp:BoundField HeaderText="OR Date" DataField="strORDate">
                                <HeaderStyle CssClass="text-center" />
                                <ItemStyle CssClass="text-center" />
                            </asp:BoundField>
                            <%--<asp:templatefield headertext="organ">
                                <itemtemplate>
                                    <asp:label id="lblorgan" runat="server" text='<%# eval("organ") %>'></asp:label>
                                </itemtemplate>
                                <itemstyle wrap="false" cssclass="text-center" />
                                <headerstyle cssclass="text-center" />
                            </asp:templatefield>--%>
                            <asp:TemplateField HeaderText="Organ">
                                <ItemTemplate>
                                    <asp:Label ID="lblOrganName" runat="server" Text='<%# Eval("OrganName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operation">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperation" runat="server" Text='<%# Eval("Operation") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Procedure">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcedure" runat="server" Text='<%# Eval("Procedure") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="จำนวนการผ่าตัด">
                                <ItemTemplate>
                                    <asp:Label ID="lblAmtSurgeon" runat="server" Text='<%# Eval("AmtSurgeon") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>                            
                        </Columns>
                        <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                        <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                    </asp:GridView>
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
