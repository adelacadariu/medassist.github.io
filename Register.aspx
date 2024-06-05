<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Register.aspx.cs" Inherits="licenta_test.Register" %>

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
    <title>MedAssist - Utilizator nou</title>
</head>
<body>
    <form runat="server" id="f1">
    <div class="container">
      <div class="forms-container">
        <div class="signin-signup">

          <form  class="sign-in-form">
            <h2 class="title">Utilizator nou</h2>
            <div class="input-field">
              <i class="fas fa-user"></i>
               <asp:TextBox id="Username2" runat="server" type="text" aria-describedby="usernameHelp"
                                                placeholder="Introduceti numele de utilizator" />
            </div>
              <div class="input-field">
              <i class="fas fa-user"></i>
               <asp:TextBox id="Name" runat="server" type="text" aria-describedby="usernameHelp"
                                                placeholder="Introduceti numele" />
            </div>
              <div class="input-field">
              <i class="fas fa-user"></i>
               <asp:TextBox id="Prenume" runat="server" type="text" aria-describedby="usernameHelp"
                                                placeholder="Introduceti prenumele" />
            </div>
            <div class="input-field">
              <i class="fas fa-envelope"></i>
               <asp:TextBox id="Email" runat="server" type="email" placeholder="Introduceti emailul"/>
            </div>
            <div class="input-field">
              <i class="fas fa-lock"></i>
              <asp:TextBox id="Password2" runat="server" type="password" placeholder="Introduceti parola" />
            </div>
            <asp:Button href="Login.aspx" id="bRegister" Text="Cont nou" runat="server" type="submit" class="btn" value="Sign up" OnClick="bRegister_Click" />
            
          </form>
          
        </div>
      </div>

       <div class="panels-container">
        
        <div class="panel left-panel">
          <div class="content">
            <h3>Ai un cont deja?</h3>
            <p>
              
            </p>
            <a href="Login.aspx"  class="btn transparent" id="sign-in-btn">
              Conectare
            </a>
          </div>
          <img src="auth/img/Stress.svg" class="image" alt="" />
        </div>
      </div>
    </div>

     <div>
       <center><asp:Label ID="FirebaseConnection2" runat="server"></asp:Label></center>
     </div>

    <script src="auth/app.js"></script>
</form>        
</body>
</html>
