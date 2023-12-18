<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Print.aspx.cs" Inherits="solution.Reserve.Print" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
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
            <div class="col-12">
                <asp:Panel runat="server" ID="pnORRoom1" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom1"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom1" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom2" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom2"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom2" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>

                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom3" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom3"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom3" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom4" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom4"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom4" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom5" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom5"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom5" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom6" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom6"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom6" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom7" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom7"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom7" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom8" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom8"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom8" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnORRoom9" Visible="false">
                    <h5>OR ROOM :
                        <asp:Label runat="server" ID="lblORRoom9"></asp:Label></h5>
                    <div style="overflow-y: hidden">
                        <asp:GridView ID="gvORRoom9" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                            CssClass="table table-striped table-bordered table-hover pre-scrollable" Width="100%"
                            DataKeyNames="ORID">
                            <Columns>
                                <asp:BoundField HeaderText="Date" DataField="strORDate" >
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Time" DataField="ORTime">
                                    <HeaderStyle CssClass="text-center" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Patient Name">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" CssClass="text-primary" ID="hlPatientName" Text='<%# Bind("ORPATIENTVO.PatientName") %>'></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="text-center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="HN">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdgvORID" runat="server" Value='<%# Eval("ORID") %>' />
                                        <asp:Label ID="lblgvHN" runat="server" Text='<%# Eval("HN") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
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
                                        <asp:Label ID="lblgvOperation" runat="server" Text='<%# Eval("OROPERATIONVO.Side") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Anesthesia Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAnesthesiaType" runat="server" Text='<%# Eval("AnesthesiaType") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle CssClass="word-break" />
                                    <HeaderStyle CssClass="text-left" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Surgeon" DataField="Surgeon1">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Remark" DataField="Remark">
                                    <HeaderStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <EmptyDataRowStyle CssClass="alert-secondary text-center" />
                            <HeaderStyle CssClass="table-info" Font-Size="9pt" />
                        </asp:GridView>
                    </div>
                </asp:Panel>

            </div>
        </div>
    </form>
</body>
</html>
