<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.Doctor.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Doctor/Doctor List">Doctor Setup</a>
            </li>
            <li class="breadcrumb-item active">Doctor List</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-user-circle-o"></i><span class="nav-link-text">Anesthesia Doctor</span></h4>
            </div>
            <div class="col-md-6">
                <%--<div class="btn-toolbar pull-right" role="toolbar" aria-label="Toolbar with button groups">

                    <div class="btn-group mr-2" role="group" aria-label="Second group">
                        <a href="/Doctor/AddEdit" class="btn btn-info pull-right">Register</a>
                    </div>
                </div>--%>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-5">

                <div class="form-inline">
                    <label class="pull-left p-1">วันที่ : </label>
                    <div class="input-group p-1" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                        <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                        <span class="input-group-btn">
                            <asp:Button CssClass="btn btn-sm btn-info" ID="btnSearch" Text="ค้นหา" runat="server" OnClick="btnSearch_Click" />
                        </span>
                    </div>
                    <asp:HiddenField ID="hdDate" runat="server" />
                </div>

            </div>
            <div class="col-md-7">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-2">
                                <label class="pull-right p-0">Doctor : </label>
                            </div>
                            <div class="col-10">
                                <asp:DropDownList ID="ddlDoctor" runat="server" AutoPostBack="true" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <label class="pull-right p-0">Time : </label>
                            </div>
                            <div class="col-10">
                                <div class="form-inline">
                                    <asp:DropDownList ID="ddlHH" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMM" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-2">
                                <label class="pull-right p-0">Reamrk : </label>
                            </div>
                            <div class="col-10">
                                <asp:TextBox TextMode="MultiLine" Rows="5" runat="server" ID="txtReamrk" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 p-1 text-center">
                                <asp:Button CssClass="btn btn-sm btn-success mousecursor" ID="btnAdd" Width="150px" Text="เพิ่ม" runat="server" OnClick="btnAdd_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                    <span aria-hidden="true">&times;</span>
                </button>
                <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <asp:GridView ID="gvDoctor" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="Orange"
                    ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                    DataKeyNames="ID, Doctor, DoctorName, StartAnesthesiaDateTime, Reamrk, strStartAnesthesiaDateTime"
                    CssClass="table table-striped table-bordered table-hover" OnRowDeleting="gvDoctor_RowDeleting">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Anesthesia DateTime">
                            <ItemTemplate>
                                <asp:Label ID="lblgvStartAnesthesiaDateTime" runat="server" Text='<%# Bind("strStartAnesthesiaDateTime") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="text-center" />
                            <HeaderStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Doctor Name">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdgvID" runat="server" Value='<%# Bind("ID") %>' />
                                <asp:Label ID="lblgvDoctorName" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="word-break" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reamrk">
                            <ItemTemplate>
                                <asp:Label ID="lblgvReamrk" runat="server" Text='<%# Bind("Reamrk") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle CssClass="text-center" />
                            <ItemStyle CssClass="word-break" />
                        </asp:TemplateField>

                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" ControlStyle-CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Do you want to delete?')"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle CssClass="text-center" />
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                    <HeaderStyle CssClass="table-info" />
                </asp:GridView>
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

    </script>
</asp:Content>
