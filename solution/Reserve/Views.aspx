<%@ Page Title="" EnableEventValidation="false" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Views.aspx.cs" Inherits="solution.Reserve.Views" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/Reserve/">รายการจองห้อง</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-6">
                <h4><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">รายการจองห้อง</span></h4>
                <button type="button" class="btn btn-warning" style="background-color: #ffd800; border-color: #808080"></button>
                &nbsp;แก้ไขหลังจาก 15.30&nbsp;&nbsp;
                <button type="button" class="btn btn-danger" style="background-color: #ffb6c1; border-color: #808080"></button>
                &nbsp;Stat Case
            </div>

            <%--<div class="col-6">
                <asp:LinkButton ID="lnkbtnPrint" runat="server" CssClass="btn btn-md btn-primary pull-right m-1" OnClick="lnkbtnPrint_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                <asp:LinkButton ID="lnkbtnAdd" runat="server" CssClass="btn btn-md btn-success pull-right m-1" OnClick="lnkbtnAdd_Click"><i class="fa fa-2x fa-plus" aria-hidden="true"></i></asp:LinkButton>
            </div>--%>
        </div>
        <hr />
        <div class="row">
            <div class="col-md-4">
                <%--<div class="form-inline">
                    <label class="pull-right">วันที่จอง : </label>
                    <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                        <input id="txtDate" name="txtDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                        <span class="input-group-btn">
                            <asp:Button CssClass="btn btn-sm btn-info" ID="btnSearch" Text="ค้นหา" runat="server" OnClick="btnSearch_Click" />
                        </span>
                    </div>
                    <asp:HiddenField ID="hdDate" runat="server" />
                    <asp:HiddenField ID="hdDateEn" runat="server" />
                </div>--%>
                <asp:Button CssClass="btn btn-sm btn-primary" Width="150px" ID="btnToday" Text="วันนี้" runat="server" OnClick="btnToday_Click" />
                <asp:Button CssClass="btn btn-sm btn-success" Width="150px" ID="btnTomorrow" Text="พรุ่งนี้" runat="server" OnClick="btnTomorrow_Click" />
                <asp:HiddenField ID="hdDate" runat="server" />
                <asp:HiddenField ID="hdDateEn" runat="server" />
            </div>

            <div class="col-md-8">
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
        <div class="row">
        </div>
        <hr />
        <div class="row">
            <div class="col-12">
                <asp:Panel runat="server" ID="pnORRoom1" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom1"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom1" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint1" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint1_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom1" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom1_RowDataBound"
                                    OnRowCommand="gvORRoom1_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom2" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom2"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom2" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint2" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint2_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom2" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom2_RowDataBound"
                                    OnRowCommand="gvORRoom2_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom3" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom3"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom3" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint3" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint3_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom3" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom3_RowDataBound"
                                    OnRowCommand="gvORRoom3_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom4" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom4"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom4" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint4" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint4_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom4" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom4_RowDataBound"
                                    OnRowCommand="gvORRoom4_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom5" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom5"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom5" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint5" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint5_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom5" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom5_RowDataBound"
                                    OnRowCommand="gvORRoom5_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom6" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom6"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom6" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint6" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint6_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom6" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom6_RowDataBound"
                                    OnRowCommand="gvORRoom6_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom7" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom7"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom7" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint7" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint7_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom7" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom7_RowDataBound"
                                    OnRowCommand="gvORRoom7_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom8" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom8"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom8" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint8" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint8_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom8" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom8_RowDataBound"
                                    OnRowCommand="gvORRoom8_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom9" Visible="false">
                    <div class="row">
                        <div class="col-10">
                            <h5>OR ROOM :
                                <asp:Label runat="server" ID="lblORRoom9"></asp:Label></h5>
                            <asp:HiddenField ID="hdORRoom9" runat="server" />
                        </div>
                        <div class="col-2 pull-right">
                            <asp:LinkButton ID="lnkbtnPrint9" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClick="lnkbtnPrint9_Click" OnClientClick="SetTarget()"><i class="fa fa-2x fa-print" aria-hidden="true"></i></asp:LinkButton>
                        </div>
                    </div>
                    <div style="overflow-y: scroll; height: 400px;">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvORRoom9" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                    CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                                    DataKeyNames="ORID"
                                    OnRowDataBound="gvORRoom9_RowDataBound"
                                    OnRowCommand="gvORRoom9_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <i runat="server" class="fa fa-warning faa-flash animated" style="color: red"></i>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Make Date" DataField="strCreateDate" HeaderStyle-HorizontalAlign="Right">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Time" DataField="ORTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Arrival" DataField="ArrivalTime">
                                            <HeaderStyle CssClass="text-center" />
                                            <ItemStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="OR Case">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdgvCancelDate" runat="server" Value='<%# Eval("CxlDateTime") %>' />
                                                <asp:HiddenField ID="hdgvORTime" runat="server" Value='<%# Eval("ORTime") %>' />
                                                <asp:HiddenField ID="hdgvORDate" runat="server" Value='<%# Eval("ORDate") %>' />
                                                <asp:HiddenField ID="hdgvCreateDate" runat="server" Value='<%# Eval("CreateDate") %>' />
                                                <asp:HiddenField ID="hdgvUpdateDate" runat="server" Value='<%# Eval("UpdateDate") %>' />
                                                <asp:HiddenField ID="hdgvORStatCase" runat="server" Value='<%# Eval("ORStatCase") %>' />
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
                                        <asp:TemplateField HeaderText="N/S/R">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvNSF" runat="server" Text='<%# Eval("NSR") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break text-center" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Visit Type" DataField="ORStatus">
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.strSide") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle CssClass="word-break" />
                                            <HeaderStyle CssClass="text-center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Anesthesia Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
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
                </asp:Panel>

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
                                    <img id="imgp" src="/Reserve/ImageServer.aspx?url=<%=PictureFileName%>" width="100%">
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

                                    <div class="row">
                                        <div class="col-4">
                                            <label class="pull-right">Pre diag : </label>
                                        </div>
                                        <div class="col-8">
                                            <div class="form-inline">
                                                <asp:Label ID="lblPrediag" CssClass="word-break" runat="server" Font-Bold="true"></asp:Label>
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
                                    <%--         <asp:LinkButton ID="btnStickerOR" runat="server" CssClass="btn btn-sm btn-primary pull-right m-1" OnClientClick="SetTarget()" Width="130px" OnClick="btnStickerOR_Click"><i class="fa fa-2x fa-print" aria-hidden="true"></i> Sticker OR</asp:LinkButton>
                                    <asp:LinkButton ID="btnStickerAdmint" runat="server" CssClass="btn btn-sm btn-success pull-right m-1" OnClientClick="SetTarget()" Width="130px"><i class="fa fa-2x fa-print" aria-hidden="true"></i> Sticker Admit</asp:LinkButton>
                                    <asp:LinkButton ID="btnStickerWard" runat="server" CssClass="btn btn-sm btn-warning pull-right m-1" OnClientClick="SetTarget()" Width="130px"><i class="fa fa-2x fa-print" aria-hidden="true"></i> Sticker Ward</asp:LinkButton>--%>

                                    <a href="/Print/StickerOR/?o=<%= hdORID.Value %>" style="width: 130px" target="_blank" class="btn btn-sm btn-primary pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i>Sticker OR</a>
                                    <a hidden href="/Print/StickerAdmit/?o=<%= hdORID.Value %>" style="width: 130px" target="_blank" class="btn btn-sm btn-success pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i>Sticker Admit</a>
                                    <a hidden href="/Print/StickerWard/?o=<%= hdORID.Value %>" style="width: 130px" target="_blank" class="btn btn-sm btn-warning pull-right m-1"><i class="fa fa-2x fa-print" aria-hidden="true"></i>Sticker Ward</a>
                                </div>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
    </div>

    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>

        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>
</asp:Content>
