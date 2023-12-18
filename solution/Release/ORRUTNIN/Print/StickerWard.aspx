<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StickerWard.aspx.cs" Inherits="solution.Print.StickerWard" %>

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

    <script src="/vendor/jquery/jquery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row">
            <div class="col-1 text-center"><a class="btn btn-primary" href="#" role="button" onclick="myFunction(divORHeader)"><i class="fa fa-1x fa-print" aria-hidden="true"></i></a></div>

        </div>
        <div id="divORHeader" style="width:500px">
            <div class="row">
                <div class="col-4 text-right">
                    วันที่ผ่าตัด
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblORDate"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-right">
                    HN
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblHN"></asp:Label>
                </div>
            </div>    
            <div class="row">
                <div class="col-4 text-right">
                    Patient Name
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblPatientName"></asp:Label>
                </div>
            </div>  
            <div class="row">
                <div class="col-6 text-right">
                    อายุ : <asp:Label runat="server" ID="lblAge"></asp:Label>
                </div>
                <div class="col-6 text-left">
                    เพศ : <asp:Label runat="server" ID="lblGender"></asp:Label>
                </div>
            </div> 
            <div class="row">
                <div class="col-4 text-right">
                    Operation : 
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblOperation"></asp:Label>
                </div>
            </div> 
            <div class="row">
                <div class="col-4 text-right">
                    Under : 
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblAnesthesiaType"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-4 text-right">
                    แพทย์(Surgeon) : 
                </div>
                <div class="col-8 text-left">
                    <asp:Label runat="server" ID="lblSurgeon"></asp:Label>
                </div>
            </div>            
            <div class="row" hidden>
                <div class="col-12 text-left">
                    <asp:Label runat="server" ID="lblRemark"></asp:Label>
                </div>
            </div>
        </div>
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
    </script>
</body>
</html>
