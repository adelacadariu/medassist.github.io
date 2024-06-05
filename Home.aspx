<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="licenta_test.Home" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>

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
<body class="sb-nav-fixed">
    <form id="form1" runat="server">
           <nav class="sb-topnav navbar navbar-expand navbar-dark bg-dark">
          
            <a class="navbar-brand ps-3" href="Home.aspx">
                  <img src="images/logo.jpeg" width="150" height="60"  class="d-inline-block align-top" alt="">
            </a>
            
            <button class="btn btn-link btn-sm order-1 order-lg-0 me-4 me-lg-0" id="sidebarToggle" href="#!"><i class="fas fa-bars"></i></button>
           
            <ul class="navbar-nav ms-auto">
                <li class="nav-item dropdown">
                    <a class="nav-link dropdown-toggle" id="navbarDropdown" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                        <i class="fas fa-user fa-fw"></i>
                    </a>
                    <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="navbarDropdown">
                        <li><a class="dropdown-item" href="Login.aspx">Iesire</a></li>
                    </ul>
        </nav>
        <div id="layoutSidenav">
            <div id="layoutSidenav_nav">
                <nav class="sb-sidenav accordion sb-sidenav-dark" id="sidenavAccordion">
                    <div class="sb-sidenav-menu">
                        <div class="nav">
                            <a class="nav-link" href="Home.aspx?username=<%=Request.QueryString["username"]%>">
                                <div class="sb-nav-link-icon"><i class="fas fa-tachometer-alt"></i></div>
                                Pagina principala
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
                        <h1 class="mt-4"></h1>
                        <ol class="breadcrumb mb-4">
                            <li class="breadcrumb-item active"></li>
                        </ol>
                        <div class="row">
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-primary text-white mb-4">
                                    <div class="card-body">
            <span id="TrimiteriCount" runat="server">3</span>
        </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Trimitere.aspx">Bilete de trimitere</a>
                                        <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-warning text-white mb-4">
                                    <div class="card-body">
            <span id="ConsultatiiCount" runat="server">3</span>
        </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Consultatii.aspx">Consultatii</a>
                                        <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xl-3 col-md-6">
                                <div class="card bg-success text-white mb-4">
                                    <div class="card-body">
            <span id="reteteCount" runat="server">3</span>
        </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Retete.aspx">Retete</a>
                                        <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                            <div id= "cardPacienti" runat="server" class="col-xl-3 col-md-6">
                                <div class="card bg-danger text-white mb-4">
                                   <div class="card-body">
            <span id="PacientiCount" runat="server">3</span>
        </div>
                                    <div class="card-footer d-flex align-items-center justify-content-between">
                                        <a class="small text-white stretched-link" href="Pacienti.aspx?username=<%=Request.QueryString["username"]%>">Pacienti</a>
                                        <div class="small text-white"><i class="fas fa-angle-right"></i></div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h3 class="mb-4"><br><br></h3>
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
