<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="rptSurgeryProcedrue.aspx.cs" Inherits="solution.Reports.rptSurgeryProcedrue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">รายงานรายละเอียดการผ่าตัดแยก Procedure 
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-bar-chart"></i><span class="nav-link-text">รายงานรายละเอียดการผ่าตัดแยก Procedure </span></h4>
            </div>
        </div>
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
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Doctor">
                                <ItemTemplate>
                                    <asp:Label ID="lblSurgeon" runat="server" Text='<%# Eval("SurgeonName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HN">
                                <ItemTemplate>
                                    <asp:Label ID="lblHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OPD/IPD">
                                <ItemTemplate>
                                    <asp:Label ID="lblOPDIPD" runat="server" Text='<%# Eval("IPDOPD") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ชื่อ-สกุล">
                                <ItemTemplate>
                                    <asp:Label ID="lblPatientName" runat="server" Text='<%# Eval("PatientName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="อายุ">
                                <ItemTemplate>
                                    <asp:Label ID="lblAge" runat="server" Text='<%# Eval("Age") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="เพศ">
                                <ItemTemplate>
                                    <asp:Label ID="lblGender" runat="server" Text='<%# Eval("Gender") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Anes Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Operation Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblORDate" runat="server" Text='<%# Eval("strOperationDate") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-center" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type Of Operation">
                                <ItemTemplate>
                                    <asp:Label ID="lblOperation" runat="server" Text='<%# Eval("Operation") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Procedure">
                                <ItemTemplate>
                                    <asp:Label ID="lblProcedrue" runat="server" Text='<%# Eval("Procedrue") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="OR Operation Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblOROperationType" runat="server" Text='<%# Eval("OROperationType") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="รหัสโรค">
                                <ItemTemplate>
                                    <asp:Label ID="lblDiagnosis" runat="server" Text='<%# Eval("Diagnosis") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CxlReason ">
                                <ItemTemplate>
                                    <asp:Label ID="lblCxlReason" runat="server" Text='<%# Eval("CxlReason") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
                                <HeaderStyle CssClass="text-center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CxlConfirmReason ">
                                <ItemTemplate>
                                    <asp:Label ID="lblCxlConfirmReason" runat="server" Text='<%# Eval("CxlPostORReason") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Wrap="false" CssClass="text-left" />
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


