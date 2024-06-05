<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pacienti.aspx.cs" Inherits="licenta_test.Pacienti" %>

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
                        <h1 align="center" class="mt-4">Datele pacientului</h1>
                   <section class="ftco-section ftco-no-pt ftco-no-pb">
	            <form action="#">
	              <div class="row">
	                <div class="col-md-6">
                  <div class="form-group">
                   <asp:Label ID="lblPacienti" runat="server" Text="Selectează un pacient:"></asp:Label>
                            <asp:DropDownList ID="ddlPacienti" runat="server" CssClass="form-control"></asp:DropDownList>
                      <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                  </div>
                </div>
               <div class="col-md-6">
                  <div class="form-group text-center">
                    
                      <asp:Button ID="Modificare" runat="server" Text="Modificare" type="submit" value="Modificare date pacient" class="btn btn-primary py-3 px-5" OnClick="ModificaDatePacient_Click"/>
                       <asp:Button ID="Stergere" runat="server" Text="Stergere" type="submit" value="Stergere pacient" class="btn btn-primary py-3 px-5" OnClick="StergerePacient_Click"/>
                       <asp:Button ID="RetetaNoua" runat="server" Text="Reteta noua" type="submit" value="Reteta noua" class="btn btn-primary py-3 px-5" OnClick="CreareRetetaNoua_Click"/>
                      <br><br>
                      <asp:Button ID="ConsultatieNoua" runat="server" Text="Consultatie noua" type="submit" value="Consultatie noua" class="btn btn-primary py-3 px-5" OnClick="CreareConsultatieNoua_Click"/>
                      <asp:Button ID="BiletTrimitereNou" runat="server" Text="Bilet de trimitere nou" type="submit" value="Bilet de trimitere nou" class="btn btn-primary py-3 px-5" OnClick="TrimitereNoua_Click"/>
                      <br><br>
                      <asp:Button ID="SetareAlerte" runat="server" Text="Seteaza alertele" type="submit" value="Seteaza alertele" class="btn btn-primary py-3 px-5" OnClick="SetareAlerte_Click"/>
                      <asp:Button ID="ScrisoareMedicalaNoua" runat="server" Text="Scrisoare medicala noua" type="submit" value="Scrisoare medicala noua" class="btn btn-primary py-3 px-5" OnClick="CreareScrisoareNoua_Click"/>
                  </div>
                </div>
		            <h5 class="mb-4"><br><br></h5>
		          <div class="card mb-4">
                        <div class="card-header">
                            <i class="fas fa-table me-1"></i>
                            Pacienti
                        </div>
                        <div class="card-body" style="overflow-x: auto;">
                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                                    <Columns>

                                        <asp:BoundField DataField="Nume" HeaderText="Nume"  />
                                        <asp:BoundField DataField="Prenume" HeaderText="Prenume"  />
                                        <asp:BoundField DataField="Varsta" HeaderText="Varsta"  />
                                        <asp:BoundField DataField="CNP" HeaderText="CNP"  />
                                        <asp:BoundField DataField="Telefon" HeaderText="Telefon"   />
                                        <asp:BoundField DataField="Email" HeaderText="Email"   />
                                        <asp:BoundField DataField="Profesie" HeaderText="Profesie"  />
                                        <asp:BoundField DataField="LocMunca" HeaderText="Loc de munca"   />
                                        <asp:BoundField DataField="Judet" HeaderText="Judet"  />
                                        <asp:BoundField DataField="Localitate" HeaderText="Localitate"   />
                                        <asp:BoundField DataField="Strada" HeaderText="Strada"   />
                                        <asp:BoundField DataField="Bloc" HeaderText="Bloc"  />
                                        <asp:BoundField DataField="Scara" HeaderText="Scara"  />
                                        <asp:BoundField DataField="Etaj" HeaderText="Etaj"   />
                                        <asp:BoundField DataField="Apartament" HeaderText="Apartament"  />
                                        <asp:BoundField DataField="Numar" HeaderText="Numar"   />
                                        <asp:BoundField DataField="IstoricMedical" HeaderText="Istoric Medical"  />
                                        <asp:BoundField DataField="Alergii" HeaderText="Alergii"   />
                                        <asp:BoundField DataField="ConsultatiiCardiologice" HeaderText="Consultatii Cardiologice"  />
                                        
                                    </Columns>
                                </asp:GridView>
                             </div>
                        </div>
                    </div>
	              </div>
	           </form>
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
