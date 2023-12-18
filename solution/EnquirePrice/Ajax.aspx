<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ajax.aspx.cs" Inherits="solution.EnquirePostMedicine.Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../../Content/css/select2.min.css" rel="stylesheet" />


</head>
<body>
    <form id="form1" runat="server">
        <div>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
             <asp:HiddenField ID="hfMedicine" runat="server" /> 
                                                                  <select name="ddlMedicine" id="ddlMedicine" ></select>
        </div>
    </form>

     <script src="https://code.jquery.com/jquery-3.4.0.js"></script>
    <script src="../../Scripts/select2.min.js"></script>
    <script src="../../Scripts/notify.js"></script>

        <script type="text/javascript">
            $(document).ready(function () {


                $("#ddlMedicine").select2({
                    ajax: {
                        delay: 150,
                        type: "POST",
                        url: '<%=Microsoft.AspNet.FriendlyUrls.FriendlyUrl.Resolve("Ajax.aspx/SearchMedicine")%>',
                          contentType: "application/json; charset=utf-8",
                          data: function (params) {
                              return JSON.stringify({
                                  'storeCode': '',
                                  'textSearch': params.term,
                                  'startPage': parseInt(params.page || 1),
                                  'per_page': parseInt(10)
                              });
                          },

                          processResults: function (data, params) {
                              params.page = params.page || 1;
                              var data = $.parseJSON(data.d);
                              var models = (typeof data.Item1) == 'string' ? eval('(' + data.Item1 + ')') : data.Item1;

                              return {

                                  results: $.map(models, function (item) {
                                      return {
                                          id: item.StockCode,
                                          text: item.MedicineName,
                                      };
                                  })
                                  ,
                                  pagination: { more: (params.page * 10) < data.Item2 }

                              };
                          }
                      },
                      cache: true,
                      placeholder: "Select Medicine",
                      width: '100%',
                      minimumInputLength: 3,
                      allowClear: true,
                     // disabled: true

                  });


            });
        </script>
</body>
</html>
