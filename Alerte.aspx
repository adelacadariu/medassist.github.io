<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Alerte.aspx.cs" Inherits="licenta_test.Alerte" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
    
    <link rel="icon" href="images/logo.jpeg" type="image/jpeg">
        <title>MedAssist</title>
        <link href="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/style.min.css" rel="stylesheet" />
        <link href="sbadmin/css/styles.css" rel="stylesheet" />
        <script src="https://use.fontawesome.com/releases/v6.3.0/js/all.js" crossorigin="anonymous"></script>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="sb-topnav navbar navbar-expand navbar-dark bg-dark">
           
           
            <a class="navbar-brand ps-3" href="Home.aspx">
                  <img src="images/logo.jpeg" width="150" height="60"  class="d-inline-block align-top" alt="">
            </a>
           
            <button class="btn btn-link btn-sm order-1 order-lg-0 me-4 me-lg-0" id="sidebarToggle" href="#!"><i class="fas fa-bars"></i></button>
           
            <ul class="navbar-nav ms-auto">
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false"><i class="fas fa-user fa-fw"></i></a>
                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                   
                        <li><a class="dropdown-item" href="Login.aspx">Iesire</a></li>
                    </ul>
                </li>
            </ul>
        </nav>
        <div id="layoutSidenav">
            <div id="layoutSidenav_nav">
                <nav class="sb-sidenav accordion sb-sidenav-dark" id="sidenavAccordion">
                    <div class="sb-sidenav-menu">
                        <div class="nav">
                            <a class="nav-link" href="Home.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                                Acasa
                            </a>
                            <div class="sb-sidenav-menu-heading" id="adaugareDiv" runat="server">Adaugare</div>

                            <div id="linkPacient" runat="server">
                                <a  class="nav-link" href="Pacient.aspx?username=<%=Request.QueryString["username"]%>">
                                    <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                                    Pacient
                                </a>
                            </div>
                            <div  id="linkReteta" runat="server">
                                <a class="nav-link" href="Reteta.aspx?username=<%=Request.QueryString["username"]%>">
                            <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                            Reteta
                             </a>
                            </div>
                        <div id="linkConsultatie" runat="server">
                            <a  class="nav-link" href="Consultatie.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                                Consultatie
                            </a>
                        </div>
                        <div id="linkTrimitere" runat="server">
                            <a class="nav-link" href="Trimitere.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                                Bilet de trimitere
                            </a>
                        </div>
                        <div id="linkAlerte" runat="server">
                            <a  class="nav-link" href="Alerte.aspx?username=<%=Request.QueryString["username"]%>">
                            <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                            Alerte
                        </a>
                        </div>

  <div id="linkScrisoare" runat="server">
      <a class="nav-link" href="Scrisoare.aspx?username=<%=Request.QueryString["username"]%>">
    <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
    Scrisoare medicala
</a></div>

                          
                          
                            <div class="sb-sidenav-menu-heading">Vizualizare</div>
                           <%-- <a class="nav-link" href="charts.html">
                                <div class="sb-nav-link-icon"><i class="fas fa-chart-area"></i></div>
                                Grafice
                            </a>--%>
                            <div id="linkPACIENTI" runat="server">
                                <a class="nav-link" href="Pacienti.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-table"></i></div>
                                Pacienti
                            </a>
                            </div>
                            
                            <a class="nav-link" href="Retete.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-table"></i></div>
                                Retete
                            </a>
                            <a class="nav-link" href="Consultatii.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-table"></i></div>
                                Consultatii
                            </a>
                        </div>
                    </div>
                    
                </nav>
            </div>
            <div id="layoutSidenav_content">
                <main>
                    <div class="container-fluid px-4">
                       
                   <section class="ftco-section ftco-no-pt ftco-no-pb">
			
	           <%-- <form action="#">--%>
	              <div class="row">
                       <h3 class="mb-4"><br><br></h3>
                        <h1 align="center" class="mt-4">Alerte</h1>

	                  <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Pacient</label>
					  <asp:TextBox id="Pacient" runat="server" type="text" class="form-control" placeholder="Pacientul pentru  care se seteaza alertele" />
                    
                  </div>
                </div>

                       <h5 class="mb-4"><br><br></h5>
                   <div class="col-md-6">
    <div class="form-group">
        <label for="">Alerta</label>
        <asp:TextBox ID="Alerta" runat="server" type="text" class="form-control" placeholder="Setati alerta " />
    </div>
</div>
<div class="col-md-6">
    <div class="form-group">
        <label for="">Parametru</label>
        <asp:DropDownList ID="Parametru" runat="server" CssClass="form-control">
            <asp:ListItem Text="Temperatura" Value="Temperatura" />
            <asp:ListItem Text="Puls" Value="Puls" />
            <asp:ListItem Text="Ritm cardiac" Value="RitmCardiac" />
            <asp:ListItem Text="Umiditate" Value="Umiditate" />
             
        </asp:DropDownList>
    </div>
</div>
<br />
<div class="col-md-12 mt-3">
    <div class="form-group text-center">
        <asp:Button ID="AdaugaAlertaNoua" runat="server" Text="Salvare alerta" type="submit" value="Salvare alerte" class="btn btn-primary py-3 px-5" OnClick="SalvareAlerta_Click"/>
    </div>
</div>
            
                         <h5 class="mb-4"><br><br></h5>
                       <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Valoare minima temperatura</label>
					  <asp:TextBox id="txtValMinTemp" runat="server" type="text" class="form-control" placeholder="Setati valoarea minima pentru temperatura a pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Valoare maxima temperatura</label>
					  <asp:TextBox id="txtValMaxTemp" runat="server" type="text" class="form-control" placeholder="Setati valoarea maxima pentru temperatura a pacientului" />
                    
                  </div>
                </div>

                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Valoare minima puls</label>
					  <asp:TextBox id="txtValMinPuls" runat="server" type="text" class="form-control" placeholder="Setati valoarea minima pentru puls a pacientului" />
                    
                  </div>
                </div>
                 
                 <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Valoare maxima puls</label>
					  <asp:TextBox id="txtValMaxPuls" runat="server" type="text" class="form-control" placeholder="Setati valoarea maxima pentru puls a pacientului" />
                    
                  </div>
                </div>
                     
<div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare minimă umiditate</label>
        <asp:TextBox ID="txtValMinUmiditate" runat="server" type="text" class="form-control" placeholder="Setați valoarea minimă pentru umiditate a pacientului" />
    </div>
</div>
                      <div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare maximă umiditate</label>
        <asp:TextBox ID="txtValMaxUmiditate" runat="server" type="text" class="form-control" placeholder="Setați valoarea maximă pentru umiditate a pacientului" />
    </div>
</div>


<div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare minimă EKG</label>
        <asp:TextBox ID="txtValMinEKG" runat="server" type="text" class="form-control" placeholder="Setați valoarea minimă pentru EKG a pacientului" />
    </div>
</div>

                  <div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare maximă EKG</label>
        <asp:TextBox ID="txtValMaxEKG" runat="server" type="text" class="form-control" placeholder="Setați valoarea maximă pentru EKG a pacientului" />
    </div>
</div>

                      <div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare minimă ritm cardiac</label>
        <asp:TextBox ID="txtValMinRitmCardiac" runat="server" type="text" class="form-control" placeholder="Setați valoarea minimă pentru ritmul cardiac a pacientului" />
    </div>
</div>

<div class="col-md-6">
    <div class="form-group">
        <label for="">Valoare maximă ritm cardiac</label>
        <asp:TextBox ID="txtValMaxRitmCardiac" runat="server" type="text" class="form-control" placeholder="Setați valoarea maximă pentru ritmul cardiac a pacientului" />
    </div>
</div>


		            <h5 class="mb-4"><br><br></h5>
                  </div>
                </div>
                      <br />
                      <br />

                <div class="col-md-12 mt-3">
                  <div class="form-group text-center">
                    <asp:Button ID="SalveazaParametrii" runat="server" Text="Salvare parametrii" type="submit" value="Salvare alerte" class="btn btn-primary py-3 px-5" OnClick="SalvareParametrii_Click"/>
                  </div>
                </div>
	              </div>
	            <%--</form>--%>
	       
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" crossorigin="anonymous"></script>
        <script src="sbadmin/js/scripts.js"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.8.0/Chart.min.js" crossorigin="anonymous"></script>
        <script src="sbadmin/assets/demo/chart-area-demo.js"></script>
        <script src="sbadmin/assets/demo/chart-bar-demo.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/simple-datatables@7.1.2/dist/umd/simple-datatables.min.js" crossorigin="anonymous"></script>
        <script src="sbadmin/js/datatables-simple-demo.js"></script>
    </form>
</body>
</html>
