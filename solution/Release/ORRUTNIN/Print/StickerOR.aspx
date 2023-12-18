<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StickerOR.aspx.cs" Inherits="solution.Print.StickerOR" %>

<!DOCTYPE html>

<html lang="en">
<head runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title></title>

    <%--<asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:bundlereference runat="server" path="~/Content/css" />--%>

    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="description" content="">
    <meta name="author" content="">
    <!-- Bootstrap core CSS-->
    <link href="/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
    <!-- Custom fonts for this template-->
    <link href="/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
    <!-- Custom styles for this template-->
    <link href="/css/sb-admin.css" rel="stylesheet">

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <%--<link href="/css/datepicker.css" rel="stylesheet" />--%>
    <link href="/css/bootstrap-datepicker.css" rel="stylesheet" />

    <link href="/Content/Site.css" rel="stylesheet" />
    <style>
        body {
            padding-top: 0px;
            padding-bottom: 0px;
            font-size: 13px;
        }

        .row {
            margin-bottom: 0px;
        }

        .column {
          float: left;
          width: 50%;
        }
        .column1 {
          float: left;
          width: 20px;
        }
        .column2 {
          float: left;
          width: 380px;
        }

        /* Clear floats after the columns */
        .row:after {
          content: "";
          display: table;
          clear: both;
        }
    </style>
    <script src="/vendor/jquery/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-1 text-center p-2"><a class="btn btn-primary" href="#" role="button" onclick="myFunction(divORHeader)"><i class="fa fa-1x fa-print" aria-hidden="true"></i></a></div>

        </div>
        <div class="alert" role="alert">
            <div class="row">
            <div id="divORHeader" style="width: 400px">  
                <div class="row">
                    <div class="column1">
                        </div>
                    <div class="column2">
                          <div id="divORHeader02" style="width: 380px">                                        
                            <div class="row">                    
                                <div class="col-6 text-left">
                                     <asp:Label runat="server" ID="lblRV" Text ="RV03 (14/06/65)" Font-Size="9pt"></asp:Label>
                            </div>
                                <div class="col-6 text-right">
                                    <div class="col-12 text-right">
                                <asp:Label runat="server" ID="lblFM" Text ="FM-ADC-030" Font-Size="9pt"></asp:Label>
                                        </div>
                            </div>
                            </div>
                            <div class="row">
                                <div class="col-5 text-left">
                                    OR Date : 
                                    <asp:Label Font-Bold="False" runat="server" ID="lblORDate" Font-Size="10pt" ></asp:Label>
                                </div>

                                 <div class="col-7 text-left">
                                    Surgeon : 
                                    <asp:Label Font-Bold="True" runat="server" ID="lblSurgeon" Font-Size="14pt" ></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-left">
                                   Name :
                                    <asp:Label Font-Bold="True" runat="server" ID="lblPatientName" Font-Size="13pt"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-left">
                                    HN#
                                <asp:Label Font-Bold="True" runat="server" ID="lblHN" Font-Size="13pt" ></asp:Label>
                                อายุ :
                                <asp:Label Font-Bold="False" runat="server" ID="lblAge" Font-Size="13pt"></asp:Label>
                                    ปี/<asp:Label Font-Bold="False" runat="server" ID="lblGender" Font-Size="13pt"></asp:Label>
                                <%--</div>--%>

                                 <%--<div class="col-5 text-left">--%>
                        
                                <%--</div>--%>
                                    </div>
                                </div>
                            <div class="row">
                                <div class="col-6 text-left">
                                    Under : 
                                    <asp:Label Font-Bold="False" runat="server" ID="lblAnesthesiaType" Font-Size="12pt" ></asp:Label>
                                </div>
                                <div class="col-6 text-left">
                                    Status : 
                                    <asp:Label Font-Bold="False" runat="server" ID="lblStatus" Font-Size="10pt"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-left">
                                    Dx : 
                                    <asp:Label Font-Bold="False" runat="server" ID="lblPrediag" Font-Size="13pt" ></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-12 text-left">
                                    Operation : 
                                    <asp:Label Font-Bold="True" runat="server" ID="lblOperation" Font-Size="14pt" Font-Italic="False" ></asp:Label>
                                </div>
                    
                            </div>
                          </div>
                    </div>
               </div>
           </div>
           </div>
        </div>



            <%--<!-- Modal -->
            <div class="modal fade" id="ModalSide">
                <div class="modal-dialog modal-dialog-centered">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title text-primary">คำเตือน!!</h5>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <div class="row">
                                    <div class="col-12 text-center">
                                        <i class="fa fa-4x fa-exclamation-circle text-warning"></i>
                                        <br />
                                        <asp:Label runat="server" ID="Label3" Text="กรุณาระบุข้างก่อน"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <br />
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>--%>

    </form>

    <script>
        function myFunction(divID) {
            //console.log("divID", divID);
            var divElements = divID;
            var oldstr = document.body.innerHTML;
            document.body.innerHTML = "<html><head><title></title></head><body>" + divElements.innerHTML + "</body>";
            window.print();
            document.body.innerHTML = oldstr;
        }

        function showModalSide() {
            $("#ModalSide").modal('show');
        }

    </script>
</body>
</html>
