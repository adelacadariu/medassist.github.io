using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZXing;
using MySql.Data.MySqlClient;
namespace licenta_test
{
    public partial class Trimitere : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                if (!string.IsNullOrEmpty(username))
                {
                    IncarcaDateMedic(username);
                    txtDataTrimiterii.Text = DateTime.Today.ToString("dd-MM-yyyy");
                }
                string cnp = Request.QueryString["pacient"];
                if (!string.IsNullOrEmpty(cnp))
                {
                    LoadPacientData(cnp);
                   
                }
               
                string userRole = GetUserRole(username);
                if (userRole != "admin")
                {
                    HideAdminSections();
                }
            }
        }
        private string GetUserRole(string username)
        {
            string role = string.Empty;

            string query = "SELECT Rol FROM Users WHERE Utilizator = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    role = result.ToString();
                }
            }

            return role;
        }
        private void HideAdminSections()
        {
            adaugareDiv.Visible = false;
            linkScrisoare.Visible = false;
            linkPacient.Visible = false;
            linkReteta.Visible = false;
            linkConsultatie.Visible = false;
            linkTrimitere.Visible = false;
            linkAlerte.Visible = false;
            linkPACIENTI.Visible = false;
        }
        private void LoadDateOfBirth(string pacientId)
        {
            string anNastere = "";
            if (pacientId.Substring(0, 1) == "1" || pacientId.Substring(0, 1) == "2")
            {  anNastere = "19" + pacientId.Substring(1, 2); }
            else if (pacientId.Substring(0, 1) == "5" || pacientId.Substring(0, 1) == "6")
            {  anNastere = "20" + pacientId.Substring(1, 2); }

            string lunaNastere= pacientId.Substring(3, 2);
            string ziNastere = pacientId.Substring(5, 2);
            string dataN = ziNastere + "-" + lunaNastere + "-" + anNastere;

        }

        private void LoadPacientData(string pacientId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Nume, Prenume, Varsta, CNP, Telefon, Email, Profesie, LocMunca, Judet, Localitate, Strada, Bloc, Scara, Etaj, Apartament, Numar, IstoricMedical, Alergii, ConsultatiiCardiologice FROM Pacienti WHERE CNP = @PacientId";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PacientId", pacientId);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtNume.Text = reader["Nume"].ToString();
                            txtPrenume.Text = reader["Prenume"].ToString(); 
                            txtCNP.Text = reader["CNP"].ToString();
                            txtAdresa.Text = reader["Judet"].ToString() + ", " + reader["Localitate"].ToString();
                            if (reader["Strada"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Strada"].ToString(); }
                            if (reader["Bloc"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Bloc"].ToString(); }
                            if (reader["Scara"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Scara"].ToString(); }
                            if (reader["Etaj"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Etaj"].ToString(); }
                            if (reader["Apartament"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Apartament"].ToString(); }
                            if (reader["Numar"].ToString() != null) { txtAdresa.Text = txtAdresa.Text + ", " + reader["Numar"].ToString(); }
                           
                        }
                    }
                }
            }
        }
        private void IncarcaDateMedic(string username)
        {
            
            string query = "SELECT Nume, Prenume FROM Users WHERE Utilizator = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    MySqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        string nume = reader["Nume"].ToString();
                        string prenume = reader["Prenume"].ToString();
                        txtSemnaturaMedicului.Text = $"{nume} {prenume}";
                    }
                }
            }
        }
        protected void SalvareTrimitere_Click(object sender, EventArgs e)
        {
            string codBare = "";  
            string serieBiletTrimitere = SerieBilet.Text;  
            string numarBiletTrimitere = NrBilet.Text;  
            string cui = txtCUI.Text;  
            string furnizorServiciiMedicale = ddlFurnizor.Text;  
            string adresaSediu = txtSediuUM.Text;  
            string judetSediu = txtJudet.Text;  
            string casaAsigurari = txtCasaAsigurari.Text;  
            string nrContract = txtNrContract.Text;  
            string prioritate = ddlNivelPrioritate.Text;  
            string casaAsigurariPacient = txtAsiguratCAS.Text;  
            string nrDinRegistruConsultatii = txtRC.Text;  
            string categorieAsigurat = ddlStatut.Text;  
            string cnpAsigurat = txtCNP.Text;  
            string numeAsigurat = txtNume.Text;  
            string prenumeAsigurat = txtPrenume.Text;  
            string adresaAsigurat = txtAdresa.Text;  
            string cetatenie = txtCetatenie.Text;  
            string diagnostic1 = txtDiagnostic.Text;  
            string diagnostic2 = txtDiagnostic2.Text;  
            string codDiagnostic1 = txtCodDiagnostic1.Text;  
            string codDiagnostic2 = txtCodDiagnostic2.Text;  
            string accidenteMunca = chkAccidente.Value.ToString();  
            string data = txtDataTrimiterii.Text;  
            string semnaturaMedic = txtSemnaturaMedicului.Text;  

            string[] codInvestigatii = new string[15];
            string[] investigatiiRecomandate = new string[15];
            string[] investigatiiEfectuate = new string[15];

            for (int i = 1; i <= 15; i++)
            {
                codInvestigatii[i - 1] = Request.Form["CodInvestigatie" + i];
                investigatiiRecomandate[i - 1] = Request.Form["InvestigatiiRecomandate" + i];
                investigatiiEfectuate[i - 1] = Request.Form["InvestigatiiEfectuate" + i];
            }
            byte[] CodBare = ViewState["BarcodeBytes"] as byte[];

            if (CodBare != null)
            {
                int codBareSize = CodBare.Length;
                if (codBareSize > 5000)
                {
                   
                    return;
                }
            }
            if (IsFormValid())
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO BiletDeTrimitere (CodBare, SerieBiletTrimitere, NumarBiletTrimitere, CUI, FurnizorServiciiMedicale, AdresaSediu, JudetSediu, CasaAsigurari, NrContract, Prioritate, CasaAsigurariPacient, NrDinRegistruConsultatii, CategorieAsigurat, CNPAsigurat, NumeAsigurat, PrenumeAsigurat, AdresaAsigurat, Cetatenie, Diagnostic1, Diagnostic2, CodDiagnostic1, CodDiagnostic2, AccidenteMunca, Data, SemnaturaMedic) " +
                         "VALUES (@CodBare, @SerieBiletTrimitere, @NumarBiletTrimitere, @CUI, @FurnizorServiciiMedicale, @AdresaSediu, @JudetSediu, @CasaAsigurari, @NrContract, @Prioritate, @CasaAsigurariPacient, @NrDinRegistruConsultatii, @CategorieAsigurat, @CNPAsigurat, @NumeAsigurat, @PrenumeAsigurat, @AdresaAsigurat, @Cetatenie, @Diagnostic1, @Diagnostic2, @CodDiagnostic1, @CodDiagnostic2, @AccidenteMunca, @Data, @SemnaturaMedic); " +
                         "SELECT SCOPE_IDENTITY() AS InsertedID";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CodBare", CodBare);
                        command.Parameters.AddWithValue("@SerieBiletTrimitere", serieBiletTrimitere);
                        command.Parameters.AddWithValue("@NumarBiletTrimitere", numarBiletTrimitere);
                        command.Parameters.AddWithValue("@CUI", cui);
                        command.Parameters.AddWithValue("@FurnizorServiciiMedicale", furnizorServiciiMedicale);
                        command.Parameters.AddWithValue("@AdresaSediu", adresaSediu);
                        command.Parameters.AddWithValue("@JudetSediu", judetSediu);
                        command.Parameters.AddWithValue("@CasaAsigurari", casaAsigurari);
                        command.Parameters.AddWithValue("@NrContract", nrContract);
                        command.Parameters.AddWithValue("@Prioritate", prioritate);
                        command.Parameters.AddWithValue("@CasaAsigurariPacient", casaAsigurariPacient);
                        command.Parameters.AddWithValue("@NrDinRegistruConsultatii", nrDinRegistruConsultatii);
                        command.Parameters.AddWithValue("@CategorieAsigurat", categorieAsigurat);
                        command.Parameters.AddWithValue("@CNPAsigurat", cnpAsigurat);
                        command.Parameters.AddWithValue("@NumeAsigurat", numeAsigurat);
                        command.Parameters.AddWithValue("@PrenumeAsigurat", prenumeAsigurat);
                        command.Parameters.AddWithValue("@AdresaAsigurat", adresaAsigurat);
                        command.Parameters.AddWithValue("@Cetatenie", cetatenie);
                        command.Parameters.AddWithValue("@Diagnostic1", diagnostic1);
                        command.Parameters.AddWithValue("@Diagnostic2", diagnostic2);
                        command.Parameters.AddWithValue("@CodDiagnostic1", codDiagnostic1);
                        command.Parameters.AddWithValue("@CodDiagnostic2", codDiagnostic2);
                        command.Parameters.AddWithValue("@AccidenteMunca", accidenteMunca);
                        command.Parameters.AddWithValue("@Data", data);
                        command.Parameters.AddWithValue("@SemnaturaMedic", semnaturaMedic);


                        connection.Open();

                        for (int i = 0; i < 15; i++)
                        {
                            SaveInvestigationData(CodBare, i + 1, codInvestigatii[i], investigatiiRecomandate[i], investigatiiEfectuate[i], connection);
                        }
                    }
                }
            }
            else { ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați toate câmpurile!');", true); }
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrEmpty(CodBareImage.ImageUrl) &&
                   !string.IsNullOrEmpty(SerieBilet.Text) &&
                   !string.IsNullOrEmpty(NrBilet.Text) &&
                   !string.IsNullOrEmpty(txtCUI.Text) &&
                   !string.IsNullOrEmpty(ddlFurnizor.Text) &&
                   !string.IsNullOrEmpty(txtAdresa.Text) &&
                   !string.IsNullOrEmpty(txtJudet.Text) &&
                   !string.IsNullOrEmpty(txtCasaAsigurari.Text) &&
                    !string.IsNullOrEmpty(txtNrContract.Text) &&
                    !string.IsNullOrEmpty(ddlNivelPrioritate.Text) &&
                    !string.IsNullOrEmpty(ddlStatut.Text) &&
                   !string.IsNullOrEmpty(txtCNP.Text) &&

                   !string.IsNullOrEmpty(txtNume.Text) &&
                   !string.IsNullOrEmpty(txtPrenume.Text) &&
                   !string.IsNullOrEmpty(txtSediuUM.Text) &&
                   !string.IsNullOrEmpty(txtDataTrimiterii.Text) &&
                   !string.IsNullOrEmpty(txtSemnaturaMedicului.Text) &&
                   !string.IsNullOrEmpty(txtCetatenie.Text);
        }
        private void SaveInvestigationData(byte[] codBare, int position, string codInvestigatie, string investigatiiRecomandate, string investigatiiEfectuate, MySqlConnection connection)
        {
            string query = "UPDATE BiletDeTrimitere SET " +
                           $"CodInvestigatie{position} = @CodInvestigatie, " +
                           $"InvestigatiiRecomandate{position} = @InvestigatiiRecomandate, " +
                           $"InvestigatiiEfectuate{position} = @InvestigatiiEfectuate " +
                           "WHERE CodBare = @CodBare";

            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CodBare", codBare);
                command.Parameters.AddWithValue("@CodInvestigatie", codInvestigatie ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@InvestigatiiRecomandate", investigatiiRecomandate ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@InvestigatiiEfectuate", investigatiiEfectuate ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }
        protected void GenerareCod_Click(object sender, EventArgs e)
        {
              
        string serie = SerieBilet.Text;
        string numar = NrBilet.Text;

        // Concatenați seria și numărul trimiterii pentru a obține datele pe baza cărora se generează codul de bare
        string datePentruCod = serie + numar;

        // Generați codul de bare
        BarcodeWriter writer = new BarcodeWriter();
        writer.Format = BarcodeFormat.CODE_128; // Alegeți formatul codului de bare (în acest caz, CODE_128)
        Bitmap barcodeBitmap = writer.Write(datePentruCod);

        // Convertiți imaginea Bitmap într-un array de bytes și salvați-l în ViewState pentru utilizare ulterioară
        ViewState["BarcodeBytes"] = BitmapToByteArray(barcodeBitmap);

        // Setează imaginea codului de bare în elementul <asp:Image>
        CodBareImage.ImageUrl = BitmapToBase64ImageSrc(barcodeBitmap);
        }
        private byte[] BitmapToByteArray(Bitmap bitmap)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Convertiți imaginea Bitmap într-o imagine PNG și salvați-o în memorie
                bitmap.Save(memoryStream, ImageFormat.Png);

                // Obțineți șirul de octeți corespunzător imaginii PNG
                return memoryStream.ToArray();
            }
        }


        private string BitmapToBase64ImageSrc(Bitmap bitmap)
        {
            byte[] imageBytes = BitmapToByteArray(bitmap);

            // Convertiți șirul de octeți într-un șir base64
            string base64String = Convert.ToBase64String(imageBytes);

            // Construiți URL-ul imaginii base64 (utilizând prefixul "data:image/png;base64,")
            return "data:image/png;base64," + base64String;
        }
    }
}