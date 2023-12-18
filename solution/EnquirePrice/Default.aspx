<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="solution.EnquirePrice.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../../Content/css/select2.min.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style>
        .select2 {
            width: 100% !important;
        }
    </style>
    <div class="container-fluid">
        <!-- Breadcrumbs-->
        <ol class="breadcrumb">
            <li class="breadcrumb-item">
                <a href="/EnquirePrice/">Enquiry Price With Patient List</a>
            </li>
        </ol>
        <div class="row">
            <div class="col-md-2">
                <h5><i class="fa fa-fw fa-calendar"></i><span class="nav-link-text">Visit Search</span></h5>
            </div>

            <div class="col-md-1 text-right">
                <label class="pull-right">Visit Date : </label>
            </div>
            <div class="col-md-2">
                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                    <input id="txtVisitDate" name="txtVisitDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                    <span class="input-group-addon">
                        <i class="fa fa-calendar-o"></i>
                    </span>
                </div>
                <asp:HiddenField ID="hdVisitDate" runat="server" />
                <asp:HiddenField ID="hdVisitDateEn" runat="server" />
              
            </div>
            <div class="col-md-1 text-right">
                <label class="pull-right">VN : </label>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtVN" CssClass="form-control inputs"></asp:TextBox>
            </div>

            <div class="col-md-1 text-right">
                <label>HN : </label>
            </div>
            <div class="col-md-2">
                <asp:TextBox runat="server" ID="txtHN" CssClass="form-control inputs"></asp:TextBox>
            </div>

        </div>
        <hr />
<%--        <div class="row">
            <div class="col-md-2 text-right">
                <label class="pull-right">Visit Date : </label>
            </div>
            <div class="col-md-3">
                <div class="input-group" data-date="12/02/2560" data-date-format="dd/mm/yyyy">
                    <input id="txtVisitDate" name="txtVisitDate" class="datepicker form-control input-group-sm" data-date-format="dd/mm/yyyy" required>
                    <span class="input-group-addon">
                        <i class="fa fa-calendar-o"></i>
                    </span>
                </div>
                <asp:HiddenField ID="hdVisitDate" runat="server" />
                <asp:HiddenField ID="hdVisitDateEn" runat="server" />
              
            </div>
            <div class="col-md-2 text-right">
                <label class="pull-right">VN : </label>
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtVN" CssClass="form-control inputs"></asp:TextBox>
            </div>
        </div>--%>
        <div class="row my-2" hidden ="true">
<%--            <div class="col-md-2 text-right">
                <label>HN : </label>
            </div>
            <div class="col-md-3">
                <asp:TextBox runat="server" ID="txtHN" CssClass="form-control inputs"></asp:TextBox>
            </div>--%>
            <div class="col-md-2 text-right">
                <label>Doctor : </label>
            </div>
            <div class="col-md-3">
                <asp:DropDownList runat="server" ID="ddlSurgeon" CssClass="form-control">
                    <asp:ListItem></asp:ListItem>
                </asp:DropDownList>
            </div>
           
        </div>
         <div class="row my-2" hidden ="true">
            <div class="col-md-2 text-right">
                <label>Clinic : </label>
            </div>
            <div class="col-md-8">
                <div class="form-horizontal">
                    <div class="control-group form-inline ">
                        <asp:DropDownList ID="ddlClinic" runat="server" AutoPostBack="false" CssClass="form-control d-block" multiple="multiple" ></asp:DropDownList>
                        <asp:HiddenField ID="hfClinic" runat="server" />
                    </div>
                </div>
            </div>
           
        </div>

          <div class="row my-3" hidden ="true">
            <div class="col-md-2 text-right">
                <label>Paid out : </label>
            </div>
              <div class="col-md-3">
                  <div class="form-horizontal">
                      <div class="control-group form-inline ">
                          <asp:DropDownList ID="ddlPaidOut" runat="server" AutoPostBack="false" CssClass="form-control d-block" Visible ="false">
                              <asp:ListItem Text="All" Value=" " />
                              <asp:ListItem Text="Include" Value="0" />
                              <asp:ListItem Text="Exclude" Value="1" />
                          </asp:DropDownList>
                      </div>
                  </div>
              </div>
             <div class="col-md-2 text-right">
                <label>Close Visit :</label>
            </div>
              <div class="col-md-3">
                  <asp:DropDownList ID="ddlCloseVisit" runat="server" AutoPostBack="false" CssClass="form-control d-block"  Visible ="false">
                      <asp:ListItem Text="All" Value=" " />
                      <asp:ListItem Text="Include" Value="0" />
                      <asp:ListItem Text="Exclude" Value="1" />
                  </asp:DropDownList>
              </div>
              <div class="col-md-2 text-right">
                  <asp:LinkButton ID="lbtnOPDSearch" runat="server" CssClass="btn btn-sm btn-info m-1" OnClick="lbtnOPDSearch_Click"><i class="fa fa-2x fa-search" aria-hidden="true"></i></asp:LinkButton>
              </div>
        </div>
       
        <hr />
        <div class="row">
            <div class="col-12">
                <div class="scrolly">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvPostTreatment" ShowHeaderWhenEmpty="True" AutoGenerateColumns="False" EmptyDataText="No records Found" runat="server"
                                CssClass="table table-striped table-bordered table-hover pre-scrollable"
                                DataKeyNames="VN,HN,VISITDATE,SUFFIX" OnRowCommand="gvPostTreatment_RowCommand"
                               
                                >
                                <Columns>
                                   
                                    <asp:BoundField HeaderText="Visit Datetime" DataField="VISITDATE" HeaderStyle-HorizontalAlign="Right" DataFormatString="{0:dd/MM/yyyy}">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center"  />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="VN" DataField="VN">
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="HN" DataField="HN" >
                                        <HeaderStyle CssClass="text-center" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Patient Name" DataField="FullName">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Presc. No." DataField="SUFFIX">
                                         <ItemStyle CssClass="text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Doctor" DataField="DoctorName">
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Clinic" DataField="CLINICNAME">
                                        <HeaderStyle CssClass="text-left" />
                                    </asp:BoundField>
                                     <asp:BoundField HeaderText="Hold Bill" DataField="HoldBillDesc">
                                         <ItemStyle CssClass="text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Close" DataField="Close">
                                         <ItemStyle CssClass="text-center" />
                                        <HeaderStyle CssClass="text-center" />
                                    </asp:BoundField>
                                     <asp:ButtonField   CommandName="Select" Text="Select" ControlStyle-CssClass="btn btn-success btn-sm" HeaderStyle-Width="100px" ItemStyle-CssClass="text-center"  HeaderText="Select"  HeaderStyle-CssClass="text-center">
                                        <ControlStyle CssClass="btn btn-secondary btn-sm"></ControlStyle>
                                        <HeaderStyle Width="80px"></HeaderStyle>
                                        <ItemStyle CssClass="text-center"></ItemStyle>
                                    </asp:ButtonField>
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
    <script src="../../Scripts/select2.min.js"></script>
    <script src="/js/moment.min.js"></script>
    <script src="/js/bootstrap-datepicker-custom.js"></script>
    <script src="/js/locales/bootstrap-datepicker.th.min.js" charset="UTF-8"></script>

    <script>
        $(document).ready(function () {

            var visitdate = document.getElementById('<%=hdVisitDate.ClientID %>').value;
            console.log('visitdate', visitdate);
            if (visitdate) {
                var visitdateEn = document.getElementById('<%=hdVisitDateEn.ClientID %>').value;
                console.log('visitdateEn', visitdateEn);
                $('#txtVisitDate').datepicker({
                    format: 'dd/mm/yyyy',
                    todayBtn: true,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: true              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", visitdateEn);  //กำหนดเป็นวันปัจุบัน
            }
            else {
                $('#txtVisitDate').datepicker({
                    format: 'dd/mm/yyyy',
                    todayBtn: true,
                    language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                    thaiyear: true              //Set เป็นปี พ.ศ.
                }).datepicker("setDate", "0");  //กำหนดเป็นวันปัจุบัน

                var xdate = $('#txtVisitDate').val();
                document.getElementById('<%=hdVisitDate.ClientID %>').value = xdate;
               
            }

             $("#<%=ddlClinic.ClientID%>").select2({
                placeholder: "Select All",
                tags: true,
                tokenSeparators: [',', ' '],
                closeOnSelect:true,
                maximumSelectionLength: 6,
                allowClear: true,
            });

            $("#<%=ddlPaidOut.ClientID%> , #<%=ddlCloseVisit.ClientID%>").select2({

                placeholder: "-Please Select-",
                width: '50%',
                allowClear: false,
                minimumResultsForSearch: -1

            });

             $("#<%=ddlClinic.ClientID %>").change(function () {
                $("#<%=hfClinic.ClientID %>").val($(this).val());
            })

        });

        $("#txtVisitDate").on("change", function () {

            var xdate = $(this).val();
            document.getElementById('<%=hdVisitDate.ClientID %>').value = xdate;
            document.getElementById('<%=hdVisitDateEn.ClientID %>').value = moment($("#txtVisitDate").datepicker('getDate')).format('DD/MM/YYYY');
            //alert($("#txtVisitDate").datepicker('getDate'));

            //alert(moment($("#txtVisitDate").datepicker('getDate')).format('DD/MM/YYYY'));
        });

        $('.inputs').keydown(function (e) {
            if (e.which === 13) {
                $('#<%=lbtnOPDSearch.ClientID %>')[0].click();
            }
        });
        function SetTarget() {
            document.forms[0].target = "_blank";
        }

    </script>
</asp:Content>