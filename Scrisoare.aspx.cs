using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Text;
//using System.Threading.Tasks;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Rest;
using MySql.Data.MySqlClient;


namespace licenta_test
{
    public partial class Scrisoare : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                string cnp = Request.QueryString["pacient"];
                if (!string.IsNullOrEmpty(username))
                {
                    IncarcaDateMedic(username);
                }
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
            txtDataScrisorii.Text = DateTime.Today.ToString("dd-MM-yyyy");
           
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
                            txtPacient.Text = reader["Nume"].ToString()+ " " + reader["Prenume"].ToString();
                            txtCNP.Text = reader["CNP"].ToString();
                            LoadDateOfBirth(pacientId);
                        }
                    }
                }
            }
        }
        private void LoadDateOfBirth(string pacientId)
        {
            string anNastere = "";
            if (pacientId.Substring(0, 1) == "1" || pacientId.Substring(0, 1) == "2")
            { anNastere = "19" + pacientId.Substring(1, 2); }
            else if (pacientId.Substring(0, 1) == "5" || pacientId.Substring(0, 1) == "6")
            { anNastere = "20" + pacientId.Substring(1, 2); }

            string lunaNastere = pacientId.Substring(3, 2);
            string ziNastere = pacientId.Substring(5, 2);
            txtDataNastere.Text = ziNastere + "-" + lunaNastere + "-" + anNastere;

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
                        Medic.Text = $"{nume} {prenume}";
                    }
                }
            }
           
        }

        protected void SalvareScrisoare_Click(object sender, EventArgs e)
        {
            // Calea unde va fi salvat PDF-ul generat
            string filePath = Server.MapPath("~/Scrisoare_Medicala.pdf");
            // Crearea documentului PDF
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Font-uri pentru text
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12f);

            // Adăugarea conținutului în document
            document.Add(new Paragraph("Furnizor servicii medicale:", boldFont));
            document.Add(new Phrase(FurnizorServiciiMedicale.Text, normalFont));

            document.Add(new Paragraph("\nMedic:", boldFont));
            document.Add(new Phrase(Medic.Text, normalFont));

            document.Add(new Paragraph("\nSpecialitatea:", boldFont));
            document.Add(new Phrase(Specialitatea.Text, normalFont));

            document.Add(new Paragraph("\nContract CAS:", boldFont));
            document.Add(new Phrase(ContractCAS.Text, normalFont));

            document.Add(new Paragraph("\n\nSCRISOARE MEDICALA", titleFont));

            document.Add(new Paragraph("\nDomnului/Doamnei dr. ", boldFont));
            document.Add(new Phrase(txtDoctor.Text, normalFont));
            document.Add(new Chunk(",\nStimate(a) coleg(a), vă informăm că pacientul dvs. ", normalFont));
            document.Add(new Phrase(txtPacient.Text, boldFont));
            document.Add(new Chunk(" născut la data de ", normalFont));
            document.Add(new Phrase(txtDataNastere.Text, boldFont));
            document.Add(new Chunk(", CNP ", normalFont));
            document.Add(new Phrase(txtCNP.Text, boldFont));
            document.Add(new Chunk(", a fost consultat în serviciul nostru la data de ", normalFont));
            document.Add(new Phrase(txtDataScrisorii.Text, boldFont));

            document.Add(new Paragraph("\n\nDiagnosticul:", boldFont));
            document.Add(new Paragraph(txtDiagnostic.Text, normalFont));

            document.Add(new Paragraph("\nTratamentul recomandat:", boldFont));
            document.Add(new Paragraph(txtTratamentRecomandat.Text, normalFont));

            // Închiderea documentului
            document.Close();

            // Citirea conținutului PDF-ului într-un array de bytes
            byte[] pdfContent = File.ReadAllBytes(filePath);

            if (IsFormValid())
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string insertQuery = "INSERT INTO ScrisoareMedicala (PDFContent, FurnizotiServiciiMedicale, Medic, Specialitate, ContractCAS, DoctorDestinatar, Pacient, DataNasteriiPacient, CNP_Pacient, Diagnostic, Tratament, Data) " +
                   "VALUES ( @PDFContent, @FurnizoriServiciiMedicale, @Medic, @Specialitate, @ContractCAS, @DoctorDestinatar, @Pacient, @DataNasteriiPacient, @CNP_Pacient, @Diagnostic, @Tratament, @Data)"; ;
                    using (MySqlCommand command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@PDFContent", pdfContent);
                        command.Parameters.AddWithValue("@FurnizoriServiciiMedicale", FurnizorServiciiMedicale.Text);
                        command.Parameters.AddWithValue("@Medic", Medic.Text);
                        command.Parameters.AddWithValue("@Specialitate", Specialitatea.Text);
                        command.Parameters.AddWithValue("@ContractCAS", ContractCAS.Text);
                        command.Parameters.AddWithValue("@DoctorDestinatar", txtDoctor.Text);
                        command.Parameters.AddWithValue("@Pacient", txtPacient.Text);
                        command.Parameters.AddWithValue("@DataNasteriiPacient", txtDataNastere.Text);
                        command.Parameters.AddWithValue("@CNP_Pacient", txtCNP.Text);
                        command.Parameters.AddWithValue("@Diagnostic", txtDiagnostic.Text);
                        command.Parameters.AddWithValue("@Tratament", txtTratamentRecomandat.Text);
                        command.Parameters.AddWithValue("@Data", txtDataScrisorii.Text);
                        command.ExecuteNonQuery();
                    }
                }
                ResetFields();
            }
            
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați toate câmpurile!');", true);
            }
        }
        private bool IsFormValid()
        {
            return !string.IsNullOrEmpty(GenerareScrisoarePDF.Text) &&
                   !string.IsNullOrEmpty(FurnizorServiciiMedicale.Text) &&
                   !string.IsNullOrEmpty(Medic.Text) &&
                   !string.IsNullOrEmpty(Specialitatea.Text) &&
                   !string.IsNullOrEmpty(ContractCAS.Text) &&
                   !string.IsNullOrEmpty(txtDoctor.Text) &&
                   !string.IsNullOrEmpty(txtPacient.Text) &&
                   !string.IsNullOrEmpty(txtDataNastere.Text) &&
                    !string.IsNullOrEmpty(txtCNP.Text) &&
                    !string.IsNullOrEmpty(txtDiagnostic.Text) &&
                    !string.IsNullOrEmpty(txtTratamentRecomandat.Text) &&
                   !string.IsNullOrEmpty(txtDataScrisorii.Text);
        }

        protected void GenerareScrisoarePDF_Click(object sender, EventArgs e)
        { // Calea unde va fi salvat PDF-ul generat
            string filePath = Server.MapPath("~/Scrisoare_Medicala.pdf");

            // Crearea documentului PDF
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(filePath, FileMode.Create));
            document.Open();

            // Font-uri pentru text
            Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 14f);
            Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f);
            Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 12f);

            // Adăugarea conținutului în document
            document.Add(new Paragraph("Furnizor servicii medicale:", boldFont));
            document.Add(new Phrase(FurnizorServiciiMedicale.Text, normalFont));

            document.Add(new Paragraph("\nMedic:", boldFont));
            document.Add(new Phrase(Medic.Text, normalFont));

            document.Add(new Paragraph("\nSpecialitatea:", boldFont));
            document.Add(new Phrase(Specialitatea.Text, normalFont));

            document.Add(new Paragraph("\nContract CAS:", boldFont));
            document.Add(new Phrase(ContractCAS.Text, normalFont));

            document.Add(new Paragraph("\n\nSCRISOARE MEDICALA", titleFont));

            document.Add(new Paragraph("\nDomnului/Doamnei dr. ", boldFont));
            document.Add(new Phrase(txtDoctor.Text, normalFont));
            document.Add(new Chunk(",\nStimate(a) coleg(a), vă informăm că pacientul dvs. ", normalFont));
            document.Add(new Phrase(txtPacient.Text, boldFont));
            document.Add(new Chunk(" născut la data de ", normalFont));
            document.Add(new Phrase(txtDataNastere.Text, boldFont));
            document.Add(new Chunk(", CNP ", normalFont));
            document.Add(new Phrase(txtCNP.Text, boldFont));
            document.Add(new Chunk(", a fost consultat în serviciul nostru la data de ", normalFont));
            document.Add(new Phrase(txtDataScrisorii.Text, boldFont));

            document.Add(new Paragraph("\n\nDiagnosticul:", boldFont));
            document.Add(new Paragraph(txtDiagnostic.Text, normalFont));

            document.Add(new Paragraph("\nTratamentul recomandat:", boldFont));
            document.Add(new Paragraph(txtTratamentRecomandat.Text, normalFont));

            // Închiderea documentului
            document.Close();

            // Citirea conținutului PDF-ului într-un array de bytes
            byte[] pdfContent = File.ReadAllBytes(filePath);

            

            Response.Redirect("~/Scrisoare_Medicala.pdf");
        }

        private void ResetFields()
        {
            FurnizorServiciiMedicale.Text = string.Empty;
            Medic.Text = string.Empty;
            Specialitatea.Text = string.Empty;
            ContractCAS.Text = string.Empty;
            txtDoctor.Text = string.Empty;
            txtPacient.Text = string.Empty;
            txtDataNastere.Text = string.Empty;
            txtCNP.Text = string.Empty;
            txtDiagnostic.Text = string.Empty;
            txtTratamentRecomandat.Text = string.Empty;
            txtDataScrisorii.Text = string.Empty;
        }

        protected async void TrimiteScrisoareMedicala_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> jsonScrisoareMedicala = new Dictionary<string, string>();
            jsonScrisoareMedicala.Add("FurnizorServiciiMedicale", FurnizorServiciiMedicale.Text);
            jsonScrisoareMedicala.Add("Medic", Medic.Text);
            jsonScrisoareMedicala.Add("Specialitatea", Specialitatea.Text);
            jsonScrisoareMedicala.Add("ContractCAS", ContractCAS.Text);
            jsonScrisoareMedicala.Add("Titlu", "SCRISOARE MEDICALA");
            jsonScrisoareMedicala.Add("Doctor", txtDoctor.Text);
            jsonScrisoareMedicala.Add("Pacient", txtPacient.Text);
            jsonScrisoareMedicala.Add("DataNasterii", txtDataNastere.Text);
            jsonScrisoareMedicala.Add("CNP", txtCNP.Text);
            jsonScrisoareMedicala.Add("DataConsultatiei", txtDataScrisorii.Text);
            jsonScrisoareMedicala.Add("Diagnostic", txtDiagnostic.Text);
            jsonScrisoareMedicala.Add("TratamentRecomandat", txtTratamentRecomandat.Text);

            string jsonString = JsonConvert.SerializeObject(jsonScrisoareMedicala);

            var composition = new Composition
            {
                Status = CompositionStatus.Final,
                Type = new CodeableConcept("http://loinc.org", "42349-1"),
                Subject = new ResourceReference($"Patient/{txtCNP.Text}"),
                Date = new FhirDateTime(DateTimeOffset.Now).ToString(),
                Title = "SCRISOARE MEDICALA",
                Author = new List<ResourceReference>
        {
            new ResourceReference($"Practitioner/{txtDoctor.Text}") 
        },
                Section = new List<Composition.SectionComponent>
        {
            new Composition.SectionComponent
            {
                Title = "Diagnosticul",
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns='http://www.w3.org/1999/xhtml'>{txtDiagnostic.Text}</div>"
                }
            },
            new Composition.SectionComponent
            {
                Title = "Tratamentul recomandat",
                Text = new Narrative
                {
                    Status = Narrative.NarrativeStatus.Generated,
                    Div = $"<div xmlns='http://www.w3.org/1999/xhtml'>{txtTratamentRecomandat.Text}</div>"
                }
            }
        }
            };

            // Trimite resursa Composition către HAPI Test Server (R4 FHIR)
            var fhirClient = new Hl7.Fhir.Rest.FhirClient("http://hapi.fhir.org/baseR4");
            try
            {
                var createdComposition = await fhirClient.CreateAsync(composition);

                if (createdComposition != null)
                {
                    successMessageDiv.InnerHtml = "<p>Datele au fost trimise cu succes!</p>";
                }
                else
                {
                    errorMessageDiv.InnerHtml = "<p>A apărut o eroare în timpul trimiterii datelor. Răspunsul primit de la server este nul.</p>";
                }
            }
            catch (Hl7.Fhir.Rest.FhirOperationException ex)
            {
                // A apărut o excepție specifică legată de operațiunea FHIR
                errorMessageDiv.InnerHtml = $"<p>A apărut o eroare în timpul trimiterii datelor:</p>";
            }
            catch (Exception ex)
            {
                // A apărut o altă excepție
                errorMessageDiv.InnerHtml = $"<p>A apărut o eroare neprevăzută în timpul trimiterii datelor</p>";
            }

        }

    }
}