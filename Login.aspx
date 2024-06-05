<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Login.aspx.cs" Inherits="licenta_test.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <script
      src="https://kit.fontawesome.com/64d58efce2.js"
      crossorigin="anonymous"
    ></script> 
    <link rel="stylesheet" href="auth/style.css" />
    <link rel="icon" href="images/logo.jpeg" type="image/jpeg">
    <title>MedAssist - Conectare</title>
</head>
<body>
   <form runat="server" id="f2">
    <div class="container">
      <div class="forms-container">
        <div class="signin-signup">
         
          <form class="sign-in-form">
            <h2 class="title">Conectare</h2>
            <div class="input-field">
              <i class="fas fa-user"></i>
         
                <asp:TextBox id="Username" runat="server" type="text"  aria-describedby="usernameHelp"
                                                placeholder="Introduceti numele de utilizator"/>
            </div>
            <div class="input-field">
              <i class="fas fa-lock"></i>
              <asp:TextBox id="Password" runat="server" type="password" placeholder="Introduceti parola"/>
            </div>
            <asp:Button href="Home.aspx" id="b_login" Text="Conectare" runat="server" type="submit" value="Login" class="btn solid" OnClick="b_login_Click" />
          </form>
        </div>
      </div>

      <div class="panels-container">
        <div class="panel left-panel">
          <div class="content">
            <h3>Esti nou aici?</h3>
            <p>
             
            </p>
            <a  href="Register.aspx"  class="btn transparent" id="sign-up-btn">
             Creaza cont
            </a>
          </div>
          <img src="auth/img/Psychology.svg" class="image" alt="" />
        </div>
       
      </div>
    </div>

     <hr>
       <center><asp:Label ID="FirebaseConnection" runat="server"></asp:Label></center>
     </hr>

   
       </form>
</body>
</html>
