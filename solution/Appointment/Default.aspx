<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.Appointment.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Appointment/">นัดหมาย</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar-plus-o"></i><span class="nav-link-text">นัดหมาย</span></h4>
            </div>
        </div>
        <hr />
        <div class="form-group">
            <asp:UpdatePanel runat="server" ID="upDivDrror">
                <ContentTemplate>
                    <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>
                        <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnConfrim" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="row">
            <div class="col-12">
                <div class="panel">
                    <div class="row">
                        <div class="col-md-5">

                            <div class="row mb-3">
                                <div class="col-md-5">
                                    <label class="pull-right">วันที่นัดหมาย : </label>
                                </div>
                                <div class="col-md-7">
                                    <div class="input-group" data-date="12-02-2560" data-date-format="dd-mm-yyyy">
                                        <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd-mm-yyyy" required>
                                        <%--<span class="input-group-addon" id="basic-addon1"><i class="fa fa-1 fa-calendar" aria-hidden="true"></i></span>--%>
                                        <span class="input-group-btn">
                                            <asp:Button CssClass="btn btn-sm btn-info" ID="btnSearch" Text="ค้นหา" runat="server" OnClick="btnSearch_Click" data-toggle="modal" data-target="#ModalInProcess" />
                                        </span>
                                    </div>
                                    <asp:HiddenField ID="hdDate" runat="server" OnValueChanged="hdDate_ValueChanged" />
                                    <asp:HiddenField ID="hdDateEn" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-5">
                                    <label class="pull-right">Operation Room : </label>
                                </div>
                                <div class="col-md-7">
                                    <asp:DropDownList ID="ddlORRoom" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>

                            </div>
                        </div>

                        <div class="col-md-7">
                            <div class="card">
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="pull-right">Anesthesia Doctor : </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label runat="server" ID="lblAnesthesiaDoctor" CssClass="word-break"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="pull-right">Anesthesia Nurse : </label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Label runat="server" ID="lblAnesthesiaNurse" CssClass=""></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <hr />
                    <div class="p-1">
                        <asp:Button CssClass="btn btn-sm btn-success mousecursor" ID="btnConfrim" Text="Comfirm Multiple Booking" runat="server" OnClick="btnConfrim_Click" data-toggle="modal" data-target="#ModalInProcess" />
                    </div>
                    <asp:UpdatePanel runat="server" ID="upgvAppointment">
                        <ContentTemplate>
                            <asp:GridView ID="gvAppointment" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                DataKeyNames="HN,PatientName,AppointmentNo,AppointmentDateTime,ProcedureCode,ProcedureName,Doctor,DoctorName,RemarksMemo"
                                OnRowDataBound="gvAppointment_RowDataBound"
                                OnRowCommand="gvAppointment_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HiddenField runat="server" ID="hdgvAppointmentDateTime" Value='<%# Bind("AppointmentDateTime") %>' />
                                            <asp:CheckBox runat="server" ID="chbgvAppointment" CssClass="custom-checkbox" />
                                            <asp:Label runat="server" ID="lblIndex"></asp:Label>
                                            <%--<%# Container.DataItemIndex + 1 %>--%>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Width="55px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AppointmentNo">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" CssClass="text-primary" ID="hlAppointmentNo" Text='<%# Bind("AppointmentNo") %>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Width="130px" CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Date" DataField="strAppointmentDateTime">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Width="130px" CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="HN" DataField="HN">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Name" DataField="PatientName">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Wrap="false" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Doctor" DataField="DoctorName">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Width="200px" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Procedure" DataField="ProcedureName">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <%-- <asp:ButtonField CommandName="Confirm" Text="Confirm"  ControlStyle-CssClass="btn btn-success btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-success btn-sm"></ControlStyle>

                                        <HeaderStyle Width="80px"></HeaderStyle>

                                        <ItemStyle CssClass="text-center" ></ItemStyle>
                                    </asp:ButtonField>--%>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="Button2" Style="min-width: 60px" runat="server" CssClass="btn btn-success btn-sm mousecursor" Text="Confirm" CommandName="Confirm" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" data-toggle="modal" data-target="#ModalInProcess" />
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" Width="160px"></ItemStyle>
                                    </asp:TemplateField>
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
    <div class="modal fade bd-example-modal-md" id="exampleModalLong" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-md" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLongTitle">ข้อมูลการนัดหมาย</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-12">
                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">AppointmentNo : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblAppointmentNo" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Date : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblAppointmentDateTime" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">HN : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblHN" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Name : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblPatientName" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Doctor : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblDoctorName" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Procedure : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblProcedureName" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Memo : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblRemarksMemo" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="gvAppointment" EventName="RowCommand" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <asp:UpdatePanel runat="server" ID="upInProcess">
        <ContentTemplate>
            <div class="modal fade" id="ModalInProcess" tabindex="-1" role="dialog" aria-labelledby="ModalInProcessTitle" aria-hidden="true">
                <div class="modal-dialog modal-dialog-centered" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="ModalInProcessTitle">In Process....</h5>
                        </div>
                        <div class="modal-body" style="padding-left: 30%;">
                            <div class="loader"></div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            var ordate = document.getElementById('<%=hdDate.ClientID %>').value;
            console.log('ordate', ordate);
            if (ordate) {
                var ordateEn = document.getElementById('<%=hdDateEn.ClientID %>').value;
                console.log('ordateEn', ordateEn);
                $('#txtDate').datepicker({
                    format: 'dd/mm/yyyy',
                    todayBtn: true,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtDate').datepicker({
                    format: 'dd/mm/yyyy',
                    todayBtn: true,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", "0");  //กำหนดเป็นวันปัจุบัน

                var xdate = $('#txtDate').val();
                document.getElementById('<%=hdDate.ClientID %>').value = xdate;
            }


        });

        $("#txtDate").on("change", function () {

            var xdate = $(this).val();
            document.getElementById('<%=hdDate.ClientID %>').value = xdate;

        });
    </script>
</asp:Content>
