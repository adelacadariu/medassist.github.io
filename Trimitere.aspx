<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Trimitere.aspx.cs" Inherits="licenta_test.Trimitere" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta charset="utf-8" />
        <meta http-equiv="X-UA-Compatible" content="IE=edge" />
        <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
        <meta name="description" content="" />
        <meta name="author" content="" />
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
			
	            <form action="#">
	              <div class="row">
                        <div align="center" class="col-md-6">
                 <div class="form-group">
        <label for="">Cod de bare</label>
        <asp:Image ID="CodBareImage" runat="server" CssClass="img-fluid" />
    </div>
                </div>
                      <div class="col-md-6 mt-2">
                  <div class="form-group text-center">
                    <asp:Button ID="btnGenerareCodBare" runat="server" Text="Generare cod de bare" type="submit" value="Generare cod" class="btn btn-primary py-3 px-5" OnClick="GenerareCod_Click"/>
                  </div>
                </div>

                      <h6 class="mb-4"><br><br></h6>
                      <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Serie</label>
                    <asp:TextBox id="SerieBilet" runat="server" type="text" class="form-control" placeholder="Seria biletului"/>
                  </div>
                </div>
                <div class="col-md-6">
                  <div class="form-group">
                    <label for="">Numar</label>
					  <asp:TextBox id="NrBilet" runat="server" type="text" class="form-control" placeholder="Numarul biletului" />
                   
                  </div>
                </div>

                       <h3 class="mb-4"><br><br></h3>
                        <h1 align="center" class="mt-4">BILET DE TRIMITERE pentru investigaţii paraclinice decontate de CAS</h1>

 <h5 class="mb-4"><br>Unitate medicala<br></h5>
                   <div class="row">
   
                       <div class="col-md-6">
        <div class="form-group">
            <label for="txtCUI">CUI</label>
            <asp:TextBox ID="txtCUI" runat="server" CssClass="form-control" placeholder="CUI"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="ddlFurnizor">Furnizor de servicii medicale</label>
            <asp:DropDownList ID="ddlFurnizor" runat="server" CssClass="form-control">
                <asp:ListItem Text="MF" Value="MF"></asp:ListItem>
                <asp:ListItem Text="Amb. Spec." Value="Amb. Spec."></asp:ListItem>
                <asp:ListItem Text="Unitate sanitara cu paturi" Value="Unitate sanitara cu paturi"></asp:ListItem>
                <asp:ListItem Text="Altele" Value="Altele"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>
<div class="row">
    
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtSpec">Sediu</label>
            <asp:TextBox ID="txtSediuUM" runat="server" CssClass="form-control" placeholder="Localitate, Strada, Numar"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtJudet">Judeţul</label>
            <asp:TextBox ID="txtJudet" runat="server" CssClass="form-control" placeholder="Judeţul"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtCasaAsigurari">Casa de asigurari</label>
            <asp:TextBox ID="txtCasaAsigurari" runat="server" CssClass="form-control" placeholder="Casa de asigurari"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtNrContract">Nr. Contract/Convenţie</label>
            <asp:TextBox ID="txtNrContract" runat="server" CssClass="form-control" placeholder="Nr. Contract/Convenţie"></asp:TextBox>
        </div>
    </div>
    
</div>
<div class="row">
    
    <div class="col-md-6">
        <div class="form-group">
            <label for="ddlNivelPrioritate">Nivel de prioritate</label>
            <asp:DropDownList ID="ddlNivelPrioritate" runat="server" CssClass="form-control">
                <asp:ListItem Text="Urgente" Value="Urgente"></asp:ListItem>
                <asp:ListItem Text="Curente" Value="Curente"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
</div>

                     
		            <h5 class="mb-4"><br>Date de identificare asigurat<br></h5>
		          
                 <div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtAsiguratCAS">Asigurat la CAS</label>
            <asp:TextBox ID="txtAsiguratCAS" runat="server" CssClass="form-control" placeholder="Casa de asigurări de sănătate în evidenţa căreia se află asiguratul"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtRC">RC</label>
            <asp:TextBox ID="txtRC" runat="server" CssClass="form-control" placeholder="Numărul din registrul de consultaţii"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="ddlStatut">Categorie de asigurat</label>
            <asp:DropDownList ID="ddlStatut" runat="server" CssClass="form-control">
                <asp:ListItem Text="Salariat" Value="Salariat"></asp:ListItem>
                <asp:ListItem Text="Veteran" Value="Veteran"></asp:ListItem>
                <asp:ListItem Text="Co-asigurat" Value="Co-asigurat"></asp:ListItem>
                <asp:ListItem Text="Revoluţionar" Value="Revoluţionar"></asp:ListItem>
                <asp:ListItem Text="Liber profesionist" Value="Liber profesionist"></asp:ListItem>
                <asp:ListItem Text="Handicap" Value="Handicap"></asp:ListItem>
                <asp:ListItem Text="Elev/Ucenic/Student social" Value="Elev/Ucenic/Student social"></asp:ListItem>
                <asp:ListItem Text="Ajutor social" Value="Ajutor social"></asp:ListItem>
                <asp:ListItem Text="Şomaj" Value="Şomaj"></asp:ListItem>
                <asp:ListItem Text="Gravidă/Lehuză" Value="Gravidă/Lehuză"></asp:ListItem>
                <asp:ListItem Text="Pensionar" Value="Pensionar"></asp:ListItem>
                <asp:ListItem Text="Alte categorii" Value="Alte categorii"></asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtCNP">CNP/CID/PASS/CE</label>
            <asp:TextBox ID="txtCNP" runat="server" CssClass="form-control" placeholder="CNP"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtNume">Nume</label>
            <asp:TextBox ID="txtNume" runat="server" CssClass="form-control" placeholder="Numele asiguratului"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtPrenume">Prenume</label>
            <asp:TextBox ID="txtPrenume" runat="server" CssClass="form-control" placeholder="Prenumele asiguratului"></asp:TextBox>
        </div>
    </div>
    
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtAdresa">Adresa</label>
            <asp:TextBox ID="txtAdresa" runat="server" CssClass="form-control" placeholder="Adresa asiguratului"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtCetatenie">Cetatenie</label>
            <asp:TextBox ID="txtCetatenie" runat="server" CssClass="form-control" placeholder="Cetatenia asiguratului"></asp:TextBox>
        </div>
    </div>
</div>

                       <h5 class="mb-4"><br>Diagnostic<br></h5>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtCodDiagnostic1">Cod diagnostic 1</label>
            <asp:TextBox ID="txtCodDiagnostic1" runat="server" CssClass="form-control" placeholder="Cod diagnostic 1"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtDiagnostic">Diagnostic</label>
            <asp:TextBox ID="txtDiagnostic" runat="server" CssClass="form-control" placeholder="Diagnostic"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
     <div class="col-md-6">
        <div class="form-group">
            <label for="txtCodDiagnostic2">Cod diagnostic 2</label>
            <asp:TextBox ID="txtCodDiagnostic2" runat="server" CssClass="form-control" placeholder="Cod diagnostic 2"></asp:TextBox>
        </div>
    </div>
    <div class="col-md-6">
        <div class="form-group">
            <label for="txtDiagnostic2">Diagnostic 2</label>
            <asp:TextBox ID="txtDiagnostic2" runat="server" CssClass="form-control" placeholder="Diagnostic 2"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
    
</div>
<div class="row">
    <div class="col-md-6">
        <div class="form-group">
           
            <div class="form-check">
                <input type="checkbox" id="chkAccidente" runat="server" class="form-check-input" />
                <label class="form-check-label" for="chkAccidente">Accidente de muncă/Boli profesionale/Daune</label>
            </div>
        </div>
    </div>
</div>
                      
                      <h5 class="mb-4"><br><br></h5> 
                       <div class="table-responsive">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Poziția</th>
                            <th>Cod investigație</th>
                            <th>Investigații recomandate</th>
                            <th>Investigații efectuate</th>
                        </tr>
                    </thead>
                    <tbody>
                        <% for (int i = 1; i <= 15; i++) { %>
                            <tr>
                                <td><%= i %></td>
                                <td><input id="Text1" runat="server" type="text" class="form-control" /></td>
                                <td><input id="Text2" runat="server" type="text" class="form-control" /></td>
                                <td><input id="Text3" runat="server" type="text" class="form-control" /></td>
                            </tr>
                        <% } %>
                    </tbody>
                </table>
            </div>
                      <div class="row">
    <div class="col-md-12">
        <div class="form-group">
            <label for="txtDataTrimiterii">Data trimiterii</label>
            <asp:TextBox ID="txtDataTrimiterii" runat="server" CssClass="form-control" placeholder="Data trimiterii"></asp:TextBox>
        </div>
    </div>
</div>
<div class="row">
    <div class="col-md-12">
        <div class="form-group">
            <label for="txtSemnaturaMedicului">Semnătura medicului</label>
            <asp:TextBox ID="txtSemnaturaMedicului" runat="server" CssClass="form-control" placeholder="Semnătura medicului"></asp:TextBox>
        </div>
    </div>
</div>


                <div class="col-md-12 mt-3">
                  <div class="form-group text-center">
                    <asp:Button ID="SubmitTrimitere" runat="server" Text="Salvare trimitere" type="submit" value="Salvare trimitere" class="btn btn-primary py-3 px-5" OnClick="SalvareTrimitere_Click"/>
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
