<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
	<meta name="viewport" content="width=1,initial-scale=1,user-scalable=1" />
	<title>Academia</title>
	
	<link href="http://fonts.googleapis.com/css?family=Lato:100italic,100,300italic,300,400italic,400,700italic,700,900italic,900" rel="stylesheet" type="text/css"/>
	<link rel="stylesheet" type="text/css" href="assets/bootstrap/css/bootstrap.min.css" />
	<link rel="stylesheet" type="text/css" href="assets/css/styles.css" />
	 <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet"/>
    
    <style type="text/css">
        .auto-style1 {
            width: 152px;
        }
    </style>
</head>
<body>
    
        <div>
        </div>
       <section class="container">
       <section class="login-form">
         <form id="form1" runat="server">
       
                    <div>
                        <font color="white">
					<i class="material-icons">school</i>
                            </font>
					</div>
					<div>
						
						<font size=5 color="white">Academia</font>
					    
					</div>
    
            
                <asp:TextBox ID="txtUsuario" placeholder="Usuario" required class="form-control input-lg" runat="server"></asp:TextBox>
                <asp:TextBox ID="txtpassword" placeholder="Clave" required class="form-control input-lg" runat="server" TextMode="Password"></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server">No recuerdo mi clave</asp:LinkButton>
                <asp:Button ID="btnLogin" class="btn btn-lg btn-info btn-block" runat="server" OnClick="btnLogin_Click" Text="Iniciar sesion" />
               </form>
    
      </section>
	</section>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
	<script src="assets/bootstrap/js/bootstrap.min.js"></script>
    </body>
</html>
