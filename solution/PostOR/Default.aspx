<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.PostOR.Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/PostOR/">PostOR</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">Post OR</span></h4>

            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-2 text-right">
                <label class="pull-right">OR Date : </label>
            </div>
<%--            <div class="col-md-2">
                <div class="input-group">
                    <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                    <span class="input-group-addon">
                        <i class="fa fa-calendar-o"></i>
                    </span>
                </div>
                <asp:HiddenField ID="hdDate" runat="server" />
            </div>--%>

              <div class="form-inline col-md-2">
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

            <div class="col-md-1 text-right">
                <label class="pull-right">HN : </label>
            </div>
            <div class="col-md-4">
                <asp:TextBox runat="server" ID="txtHN" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2 text-right">
                <label>OR Room : </label>
            </div>
            <div class="col-md-2">
                <asp:DropDownList runat="server" ID="ddlORRoom" CssClass="form-control">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-1 text-right">
                <label>Doctor : </label>
            </div>
            <div class="col-md-4">
                <asp:DropDownList runat="server" ID="ddlSurgeon" CssClass="form-control">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-md-3 text-right">
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-info m-1" OnClick="btnSearch_Click"><i class="fa fa-2x fa-search" aria-hidden="true"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkbtnPrint" Visible="false" runat="server" CssClass="btn btn-sm btn-primary m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
            </div>
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <div class="scrolly">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvPostOR" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                DataKeyNames="ORID"
                                OnRowDataBound="gvPostOR_RowDataBound"
                                OnRowCommand="gvPostOR_RowCommand">
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="text-center" />
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>--%>
                                    <asp:BoundField HeaderText="OR Date" DataField="strORDate" HeaderStyle-HorizontalAlign="Right">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Time" DataField="ORTime">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime" Visible="false">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="OR Case">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdgvCancelPostOR" runat="server" Value='<%# Eval("CxlPostOR") %>' />
                                            <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                            <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                            <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                            <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                            <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                            <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />

                                            <asp:HiddenField ID="hdSurgeon1" runat="server" Value='<%# Eval("Surgeon1") %>' />
                                            <asp:HiddenField ID="hdSurgeon2" runat="server" Value='<%# Eval("Surgeon2") %>' />
                                            <asp:HiddenField ID="hdSurgeon3" runat="server" Value='<%# Eval("Surgeon3") %>' />
                                            <asp:Label ID="lblgvORCase" runat="server" Text='<%# Eval("ORCase") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Patient Name">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Eval("PatientName_IPPU")%>'></asp:HyperLink>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="HN">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                            <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Age" DataField="ORPATIENTVO.Age">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Gender" DataField="ORPATIENTVO.Sex">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="OR Room" DataField="strORRoom">
                                        <ItemStyle CssClass="word-break"  />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Operation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Surgeon">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSurgeon" runat="server" Text='<%# Eval("Surgeon") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Wrap="false" CssClass="text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Anesthesia Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>

                                    <asp:ButtonField CommandName="ed" Text="Post OR" ControlStyle-CssClass="btn btn-success btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-success btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:ButtonField CommandName="display" Text="Display">
                                        <ControlStyle CssClass="btn btn-info btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>

                                    <asp:TemplateField HeaderText="N/S/R">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="word-break text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Visit Type" DataField="strORStatus">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>                                   
                                    
                                    <asp:BoundField HeaderText="Remark" DataField="Remark">
                                        <ItemStyle CssClass="word-break" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    
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
                    <h5 class="modal-title">ข้อมูลการจองห้อง</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%" >--%>
                                    <asp:Image runat="server" ID="imgPatient" ImageUrl="~/Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
                                </div>
                                <div class="col-md-10">
                                    <div class="form-inline">
                                        <asp:HiddenField ID="hdORID" runat="server" />
                                        <label for="lblHN" class="p-1">HN : </label>
                                        <asp:Label ID="lblHN" runat="server" Font-Bold="true" CssClass="badge badge-info p-1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Patient Name : </label>
                                        <asp:Label ID="lblPatientName" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <label class="p-1">Gender : </label>
                                        <asp:Label ID="lblGender" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 20px"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Age : </label>
                                        <asp:Label ID="lblAge" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 20px"></asp:Label>
                                        <label class="p-1">Year</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Birth Date : </label>
                                        <asp:Label ID="lblBirthDateTime" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 100px"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <label class="p-1">ID Card :&nbsp;</label>
                                        <asp:Label ID="lblIDCARD" runat="server" Font-Bold="true" CssClass="badge badge-info p-1" Style="min-width: 130px"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Nationality :&nbsp;</label>
                                        <asp:Label ID="lblNationality" runat="server" Font-Bold="true" CssClass="badge badge-info p-1" Style="min-width: 80px"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <asp:CheckBox ID="chbPatientInfection" runat="server" Text="&nbsp;Infection" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                            
                                        <asp:CheckBox ID="chbPatientType1" runat="server" Text="&nbsp;Patient Type 1(**)" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chbPatientType2" runat="server" Text="&nbsp;Patient Type 2(***)" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                            
                                        <asp:CheckBox ID="chbPatientUP" runat="server" Text="&nbsp;Up" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:Label ID="lblPatientallegic" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
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
                                    <asp:Label ID="lblPatientDiag" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
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
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <label>OR Case : </label>
                                                <asp:Label ID="lblORCASE" Font-Bold="true" runat="server"></asp:Label>
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
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ID="chbORTimeFollow" Text="&nbsp;T F" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ID="chbORStatCase" Text="&nbsp;Stat Case" />
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
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <div class="form-inline">
                                                <asp:Label ID="lblAdmitTimeType" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
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
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:GridView ID="gvOROperation" runat="server"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover table-responsive"
                                        DataKeyNames="SubName,strSide">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="word-break" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Side">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <%--End Modal--%>

    <!-- Modal -->
    <div class="modal fade bd-example-modal-lg" id="modalDisplay" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">ข้อมูลการจองห้อง</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-md-2">
                                    <%--<img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%" >--%>
                                    <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/17241-200.png" CssClass="img-thumbnail" Style="width: 70%" />
                                </div>
                                <div class="col-md-10">
                                    <div class="form-inline">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                        <label for="lblHN" class="p-1">HN : </label>
                                        <asp:Label ID="Label1" runat="server" Font-Bold="true" CssClass="badge badge-info p-1"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Patient Name : </label>
                                        <asp:Label ID="Label2" Font-Bold="true" runat="server"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <label class="p-1">Gender : </label>
                                        <asp:Label ID="Label3" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 20px"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Age : </label>
                                        <asp:Label ID="Label4" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 20px"></asp:Label>
                                        <label class="p-1">Year</label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Birth Date : </label>
                                        <asp:Label ID="Label5" Font-Bold="true" runat="server" CssClass="badge badge-info p-1" Style="min-width: 100px"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <label class="p-1">ID Card :&nbsp;</label>
                                        <asp:Label ID="Label6" runat="server" Font-Bold="true" CssClass="badge badge-info p-1" Style="min-width: 130px"></asp:Label>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <label class="p-1">Nationality :&nbsp;</label>
                                        <asp:Label ID="Label7" runat="server" Font-Bold="true" CssClass="badge badge-info p-1" Style="min-width: 80px"></asp:Label>
                                    </div>
                                    <div class="form-inline">
                                        <asp:CheckBox ID="CheckBox1" runat="server" Text="&nbsp;Infection" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                            
                                        <asp:CheckBox ID="CheckBox2" runat="server" Text="&nbsp;Patient Type 1(**)" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="CheckBox3" runat="server" Text="&nbsp;Patient Type 2(***)" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                            
                                        <asp:CheckBox ID="CheckBox4" runat="server" Text="&nbsp;Up" />
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:Label ID="Label8" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:GridView ID="GridView1" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
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
                                    <asp:Label ID="Label9" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                    <asp:GridView ID="GridView2" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
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
                                                <asp:Label ID="Label10" runat="server" Font-Bold="true"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <label>OR Case : </label>
                                                <asp:Label ID="Label11" Font-Bold="true" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label CssClass="pull-right" ID="Label12" runat="server">OR Time : </asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="true"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ID="CheckBox5" Text="&nbsp;T F" />
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox runat="server" ID="CheckBox6" Text="&nbsp;Stat Case" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4">
                                            <asp:Label CssClass="pull-right" ID="Label14" runat="server">Arrival : </asp:Label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Specific Type : </label>
                                        </div>
                                        <div class="col-8">

                                            <asp:Label ID="Label16" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>

                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Status : </label>
                                        </div>
                                        <div class="col-4">
                                            <div class="form-inline">
                                                <asp:Label ID="Label17" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-4">
                                            <div class="form-inline">
                                                <asp:Label ID="Label18" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="div1" runat="server">
                                        <div class="col-4">
                                            <label class="pull-right">Room Type : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label19" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">OR Room : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label20" runat="server" CssClass="word-break" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anesthesia Type : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label21" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" id="div2" runat="server">
                                        <div class="col-4">
                                            <label class="pull-right">Anesthesia Type : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label22" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                                <asp:Label ID="Label23" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Remark : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label24" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
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
                                                <asp:Label ID="Label25" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Surgeon (2) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label26" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Surgeon (3) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label27" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Doctor (1) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label28" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Doctor (2) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label29" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Doctor (3) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label30" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Nurse (1) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label31" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Nurse (2) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label32" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Anes Nurse (3) : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="Label33" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>
                            <hr />
                            <div class="row">
                                <div class="col-2"></div>
                                <div class="col-8">
                                    <asp:GridView ID="GridView3" runat="server"
                                        ShowHeaderWhenEmpty="True" EmptyDataText="No records Found"
                                        AutoGenerateColumns="False"
                                        CssClass="table table-striped table-bordered table-hover table-responsive"
                                        DataKeyNames="SubName,strSide">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Operation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("SubName") %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle CssClass="word-break" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Side">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSide" runat="server" Text='<%# Eval("strSide") %>'></asp:Label>
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

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>
    <%--End Modal--%>

    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <%--<script>
        $(document).ready(function () {

            var ordate = document.getElementById('<%=hdDateFrom.ClientID %>').value;
            $('#txtDate').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',
                thaiyear: false 
            }).datepicker("setDate", ordate);
            var xdate = $('#txtDate').val();
            document.getElementById('<%=hdDateFrom.ClientID %>').value = xdate;
           <%-- if (ordate) {
                var ordate = document.getElementById('<%=hdDate.ClientID %>').value;
                console.log('ordateEn', ordateEn);
                $('#txtDate').datepicker({
                    format: 'dd/mm/yyyy',
                    todayBtn: true,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: false              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", ordate);  //กำหนดเป็นวันปัจุบัน
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
            document.getElementById('<%=hdDateFrom.ClientID %>').value = xdate;

        });
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>--%>

    <script>
        $(document).ready(function () {
            //Strat Date From=====================>
            var ordateFrom = document.getElementById('<%=hdDateFrom.ClientID %>').value;
            $('#txtDateFrom').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: false,
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
                todayBtn: false,
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
