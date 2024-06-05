 <%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Reteta.aspx.cs" Inherits="licenta_test.Reteta" %>

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
           
            <ul class="navbar-nav ms-auto ">
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
			
	            <form action="#">
	              <div class="row">
	                  <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Judet</label>
					  <asp:TextBox id="JudetUnitate" runat="server" type="text" class="form-control" placeholder="Judetul unitatii sanitare" />
                    
                  </div>
                </div>

                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Numele medicului</label>
                    <asp:TextBox id="NumeMedic" runat="server" type="text" class="form-control" placeholder="Numele medicului"/>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Localitate</label>
					  <asp:TextBox id="LocalitateUnitate" runat="server" type="text" class="form-control" placeholder="Localitatea unitatii sanitare" />
                   
                  </div>
                </div>

                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Prenumele medicului</label>
                    <asp:TextBox id="PrenumeMedic" runat="server" type="text" class="form-control" placeholder="Prenumele medicului"/>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Unitate sanitara</label>
                    <asp:TextBox id="UnitateSanitara" runat="server" type="text" class="form-control" placeholder="Unitate sanitara"/>
                  </div>
                </div>
                      
                      
                <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Data</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="data" placeholder="Data emiterii retetei"/>
                  </div>
                </div>
                    
                      <h3 class="mb-4"><br><br></h3>
                        <h1 align="center" class="mt-4">Reteta medicala</h1>

                <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Nume</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="nume" placeholder="Numele pacientului"/>
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Prenume</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="prenume" placeholder="Prenumele pacientului"/>
                  </div>
                </div>
               
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">CNP</label>
					  <asp:TextBox id="cnp" runat="server" type="text" class="form-control" placeholder="CNP-ul pacientului" />
                    
                  </div>
                </div>
                                 <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Varsta</label>
					  <asp:TextBox id="Varsta" runat="server" type="text" class="form-control" placeholder="Varsta pacientului" />
                    
                  </div>
                </div>
		            <h5 class="mb-4"><br><br></h5>
		          
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Judet</label>
					  <asp:TextBox id="Judet" runat="server" type="text" class="form-control" placeholder="Judetul pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Localitate</label>
					  <asp:TextBox id="Localitate" runat="server" type="text" class="form-control" placeholder="Localitatea pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Strada</label>
					  <asp:TextBox id="Strada" runat="server" type="text" class="form-control" placeholder="Strada pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Bloc</label>
					  <asp:TextBox id="Bloc" runat="server" type="text" class="form-control" placeholder="Blocul pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Scara</label>
					  <asp:TextBox id="Scara" runat="server" type="text" class="form-control" placeholder="Scara pacientului" />
                    
                  </div>
                </div>
                      
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Apartament</label>
					  <asp:TextBox id="Apartament" runat="server" type="text" class="form-control" placeholder="Apartamentul pacientului" />
                    
                  </div>
                </div><div class="col-md-6">
                  <div class="form-group">
                    <label for="">Etaj</label>
					  <asp:TextBox id="Etaj" runat="server" type="text" class="form-control" placeholder="Etajul pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Numar</label>
					  <asp:TextBox id="Numar" runat="server" type="text" class="form-control" placeholder="Numarul casei pacientului" />
                    
                  </div>
                </div>
                       <h3 class="mb-4"><br><br></h3>

                       <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Numar reteta</label>
					  <asp:TextBox id="NrReteta" runat="server" type="text" class="form-control" placeholder="Numarul retetei" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Diagnostic</label>
					  <asp:TextBox id="Diagnostic" runat="server" type="text" class="form-control" placeholder="Diagnosticul pacientului" />
                    
                  </div>
                </div>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Rp</label>
					  <asp:TextBox id="Rp" runat="server" type="text" class="form-control" placeholder="Rp." />
                    
                  </div>
                </div>

                      <br />
                      <br />


                <div class="col-md-12 mt-3">
                  <div class="form-group text-center">
                    <asp:Button ID="SubmitReteta" runat="server" Text="Salvare reteta" type="submit" value="Salvare reteta" class="btn btn-primary py-3 px-5" OnClick="SalvareReteta_Click"/>
                  </div>
                </div>
	              </div>
	            </form>
	          </div>
          </div>
         
        </div>
			</div>
		</section>     
                     
                    </div>
                </main>
            </div>
        </div>
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
