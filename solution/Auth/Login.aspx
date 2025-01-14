﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="solution.Auth.Login" %>

<!DOCTYPE html>

<html>
<head runat="server">
      <meta charset="utf-8">
      <meta http-equiv="X-UA-Compatible" content="IE=edge">
      <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
      <meta name="description" content="">
      <meta name="author" content="">
      <title>RUTNIN OPERATION ROOM</title>
      <!-- Bootstrap core CSS-->
      <link href="/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet">
      <!-- Custom fonts for this template-->
      <link href="/vendor/font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css">
      <!-- Custom styles for this template-->
      <link href="/css/sb-admin.css" rel="stylesheet">
</head>
<body class="bg-dark">
  <div class="container">
    <div class="card card-login mx-auto mt-5">
      <div class="card-header">Login</div>
      <div class="card-body">
        <form runat="server">
            <div class="form-group">
                <div runat="server" id="divError" visible="false" class="alert alert-warning alert-dismissible fade show" role="alert">
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                    <asp:Label ID="lblMessageError" runat="server" Text="Message Error **" />
                </div>
            </div>
          <div class="form-group">
            <label for="exampleInputEmail1">UserID</label>
              <asp:TextBox runat="server" ID="txtUsername" CssClass="form-control" placeholder="Enter UserID" required></asp:TextBox>
          </div>
          <div class="form-group">
            <label for="exampleInputPassword1">Password</label>
              <asp:TextBox runat="server" ID="txtpassword" CssClass="form-control" TextMode="Password" placeholder="Enter Password" required></asp:TextBox>
          </div>
          <%--<div class="form-group">
            <div class="form-check">
              <label class="form-check-label">
                <input class="form-check-input" type="checkbox"> Remember Password</label>
            </div>
          </div>--%>
          <%--<a class="btn btn-primary btn-block" href="index.html">Login</a>--%>
            <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" />
        </form>
        <div class="text-center">
          <%--<a class="d-block small mt-3" href="register.html">Register an Account</a>
          <a class="d-block small" href="forgot-password.html">Forgot Password?</a>--%>
        </div>
      </div>
    </div>
  </div>
  <!-- Bootstrap core JavaScript-->
  <script src="/vendor/jquery/jquery.min.js"></script>
  <script src="/vendor/popper/popper.min.js"></script>
  <script src="/vendor/bootstrap/js/bootstrap.min.js"></script>
  <!-- Core plugin JavaScript-->
  <script src="/vendor/jquery-easing/jquery.easing.min.js"></script>
</body>
</html>
