<%@ Page EnableEventValidation="false" Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="InjectionRoom.aspx.cs" Inherits="solution.InjectionRoom.InjectionRoom" %>

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

        .modal-body {
            padding-left: 30%;
        }
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/InjectionRoom/">List Injection Room</a>
            </li>
            <li class="breadcrumb-item active">Injection Room</li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">Injection Room</span></h4>
            </div>
            <div class="col-md-6">
            </div>
        </div>
        <hr />
    </div>
    <div class="row">
        <div class="col-md-2">
            <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%" >--%>
            <asp:Image runat="server" ID="imgPatient" ImageUrl="~/Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
        </div>
        <div class="col-md-10">
            <div class="form-inline">
                <label for="lblHN" class="">HN : </label>
                <asp:Label ID="lblHN" runat="server" Font-Bold="true" CssClass="badge badge-info ml-1"></asp:Label>
                <label class="ml-5">Patient Name : </label>
                <asp:Label ID="lblPatientName" Font-Bold="true" runat="server" CssClass="ml-1"></asp:Label>
            </div>
            <div class="form-inline">
                <label class="">Gender : </label>
                <asp:Label ID="lblGender" Font-Bold="true" runat="server" CssClass="badge badge-info ml-1" Style="min-width: 20px"></asp:Label>
                <label class="ml-3">Age : </label>
                <asp:Label ID="lblAge" Font-Bold="true" runat="server" CssClass="badge badge-info ml-1" Style="min-width: 20px"></asp:Label>
                <label class="ml-1">Year</label>
                <label class="ml-3">Birth Date : </label>
                <asp:Label ID="lblBirthDateTime" Font-Bold="true" runat="server" CssClass="badge badge-info ml-1" Style="min-width: 100px"></asp:Label>
            </div>
            <div class="form-inline">
                <label class="">ID Card :&nbsp;</label>
                <asp:Label ID="lblIDCARD" runat="server" Font-Bold="true" CssClass="badge badge-info ml-1" Style="min-width: 130px"></asp:Label>
                <label class="ml-3">Nationality :</label>
                <asp:Label ID="lblNationality" runat="server" Font-Bold="true" CssClass="badge badge-info ml-1" Style="min-width: 80px"></asp:Label>
            </div>
            <div class="form-inline">

                <i runat="server" id="ifInfection" class="fa fa-times-rectangle mr-1" style="color: #c82333"></i>
                <i runat="server" id="itInfection" class="fa fa-check-square mr-1" style="color: #28a745"></i>
                <span>Infection</span>

                <i runat="server" id="ifPatientType1" class="fa fa-times-rectangle mr-1 ml-3" style="color: #c82333"></i>
                <i runat="server" id="itPatientType1" class="fa fa-check-square mr-1 ml-3" style="color: #28a745"></i>
                <span>Patient Type 1(**)</span>

                <i runat="server" id="ifPatientType2" class="fa fa-times-rectangle mr-1 ml-3" style="color: #c82333"></i>
                <i runat="server" id="itPatientType2" class="fa fa-check-square mr-1 ml-3" style="color: #28a745"></i>
                <span>Patient Type 2(***)</span>

                <i runat="server" id="ifUp" class="fa fa-times-rectangle mr-1 ml-3" style="color: #c82333"></i>
                <i runat="server" id="itUp" class="fa fa-check-square mr-1 ml-3" style="color: #28a745"></i>
                <span>Up</span>

            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-2"></div>
        <div class="col-8">
            <asp:Label ID="lblPatientallegic" runat="server" CssClass="mr-3"></asp:Label>
            <asp:GridView ID="gvPatientallegic" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="แพ้ยา" DataField="allegicname" HtmlEncode="false">
                        <ItemStyle CssClass="word-break" />
                        <HeaderStyle CssClass="text-center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="อาการ" DataField="Reaction" HtmlEncode="false">
                        <ItemStyle CssClass="word-break" />
                        <HeaderStyle CssClass="text-center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
            </asp:GridView>
        </div>
    </div>
    <div class="row">
        <div class="col-2"></div>
        <div class="col-8">
            <asp:Label ID="lblPatientDiag" runat="server" CssClass="mr-3"></asp:Label>
            <asp:GridView ID="gvPatientDiag" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="โรคประจำ" DataField="diagname" HtmlEncode="false">
                        <ItemStyle CssClass="word-break" />
                        <HeaderStyle CssClass="text-center" />
                    </asp:BoundField>
                </Columns>
                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                <HeaderStyle CssClass="table-info" Font-Size="9pt" />
            </asp:GridView>
        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-6">
            <div class="row">
                <div class="col-4 ">
                    <label class="pull-right">OR Date : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblORDate" runat="server" Font-Bold="true"></asp:Label>
                        <label class="ml-3">OR Case : </label>
                        <asp:Label ID="lblORCASE" CssClass="mr-1" Font-Bold="true" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <asp:Label CssClass="pull-right" ID="lblORTimeH" runat="server">OR Time : </asp:Label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblORTime" runat="server" Font-Bold="true"></asp:Label>
                        <span class="ml-3">Time Follow :
                            <i runat="server" id="iTFFlase" class="fa fa-times-rectangle ml-1" style="color: #c82333"></i>
                            <i runat="server" id="iTFTrue" class="fa fa-check-square ml-1" style="color: #28a745"></i>
                        </span>
                        <span class="ml-3">Stat Case :
                            <i runat="server" id="iORStatCaseFlase" class="fa fa-times-rectangle ml-1" style="color: #c82333"></i>
                            <i runat="server" id="iORStatCaseTrue" class="fa fa-check-square ml-1" style="color: #28a745"></i>
                        </span>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <asp:Label CssClass="pull-right" ID="lblArrivalTimeH" runat="server">Arrival : </asp:Label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblArrivalTime" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Specific Type : </label>
                </div>
                <div class="col-8">

                    <asp:Label ID="lblORSpecificType" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>

                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Status : </label>
                </div>
                <div class="col-4">
                    <div class="form-inline">
                        <asp:Label ID="lblORStatus" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblAdmitTimeType" CssClass="word-break ml-5" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row" id="divRoomType" runat="server">
                <div class="col-4">
                    <label class="pull-right">Room Type : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblRoomType" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">OR Room : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblORRoom" runat="server" CssClass="word-break" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anesthesia Type : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaType1" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row" id="divAnesthesiaType2" runat="server">
                <div class="col-4">
                    <label class="pull-right">Anesthesia Type : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaSign" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblAnesthesiaType2" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Remark : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblRemark" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-6">
            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Surgeon (1) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblSurgeon1" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Surgeon (2) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblSurgeon2" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Surgeon (3) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblSurgeon3" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Doctor (1) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaDoctor1" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Doctor (2) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaDoctor2" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Doctor (3) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaDoctor3" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Nurse (1) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaNurse1" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Nurse (2) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaNurse2" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-4">
                    <label class="pull-right">Anes Nurse (3) : </label>
                </div>
                <div class="col-8">
                    <div class="form-inline">
                        <asp:Label ID="lblAnesthesiaNurse3" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <hr />
    <div class="row">
        <div class="col-2 text-right">Operation :</div>
        <div class="col-8">
            <asp:GridView ID="gvOROperation" runat="server"
                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover table-responsive"
                DataKeyNames="SubName,strSide">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Side">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="word-break" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Operation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="word-break" />
                    </asp:TemplateField>

                </Columns>
                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                <HeaderStyle CssClass="table-success" />
            </asp:GridView>
        </div>
        <div class="col-2 pull-right">
        </div>
    </div>

    <hr />
    <div class="row">
        <div class="col-2 text-right">Previous Operation :</div>
        <div class="col-8">
            <asp:GridView ID="gvPostOROperation" runat="server"
                ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                AutoGenerateColumns="False"
                CssClass="table table-striped table-bordered table-hover table-responsive"
                DataKeyNames="SubName,strSide">
                <Columns>
                    
                    <asp:TemplateField HeaderText="Side">
                        <ItemTemplate>
                            <asp:Label ID="lblgvSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="word-break" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Operation">
                        <ItemTemplate>
                            <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="word-break" />
                    </asp:TemplateField>

                </Columns>
                <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                <HeaderStyle CssClass="table-success" />
            </asp:GridView>
        </div>
        <div class="col-2 pull-right">
        </div>
    </div>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {


    </script>
</asp:Content>
