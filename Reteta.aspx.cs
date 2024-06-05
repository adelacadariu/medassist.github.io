using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace licenta_test
{
    public partial class Reteta : System.Web.UI.Page
    {

        private static readonly HttpClient httpClient = new HttpClient();
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cnp = Request.QueryString["pacient"];
                string username = Request.QueryString["username"];
                
                string userRole = GetUserRole(username);
                if (userRole != "admin")
                {
                    HideAdminSections();
                }
                if (!string.IsNullOrEmpty(username))
                {
                    LoadDoctorInfo(username);
                }
                if (!string.IsNullOrEmpty(cnp))
                {
                    LoadPacientData(cnp);
                }
                CalculeazaNrReteta();
                data.Text = DateTime.Today.ToString("dd-MM-yyyy");
            }

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
                            nume.Text = reader["Nume"].ToString();
                            prenume.Text = reader["Prenume"].ToString();
                            Varsta.Text = reader["Varsta"].ToString();
                            cnp.Text = reader["CNP"].ToString();
                            Judet.Text = reader["Judet"].ToString();
                            Localitate.Text = reader["Localitate"].ToString();
                            Strada.Text = reader["Strada"].ToString();
                            Bloc.Text = reader["Bloc"].ToString();
                            Scara.Text = reader["Scara"].ToString();
                            Etaj.Text = reader["Etaj"].ToString();
                            Apartament.Text = reader["Apartament"].ToString();
                            Numar.Text = reader["Numar"].ToString();
                        }
                    }
                }
            }
        }
        private void CalculeazaNrReteta()
        {
            string query = "SELECT MAX(CAST(NumarReteta AS UNSIGNED)) FROM Reteta";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        if (int.TryParse(result.ToString(), out int numarRetete))
                        {
                            int urmatorulNumarReteta = numarRetete + 1;
                            NrReteta.Text = urmatorulNumarReteta.ToString();
                        }
                    }
                    else
                    {
                        NrReteta.Text = "1"; 
                    }
                }
            }
        }

        private void LoadDoctorInfo(string username)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                string query = "SELECT Nume, Prenume FROM Users WHERE Utilizator = @Username";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        NumeMedic.Text = reader["Nume"].ToString();
                        PrenumeMedic.Text = reader["Prenume"].ToString();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        private bool IsFormValid()
        {
            return !string.IsNullOrEmpty(JudetUnitate.Text) &&
                   !string.IsNullOrEmpty(LocalitateUnitate.Text) &&
                   !string.IsNullOrEmpty(UnitateSanitara.Text) &&
                   !string.IsNullOrEmpty(NumeMedic.Text) &&
                   !string.IsNullOrEmpty(PrenumeMedic.Text) &&
                   !string.IsNullOrEmpty(data.Text) &&
                   !string.IsNullOrEmpty(nume.Text) &&
                   !string.IsNullOrEmpty(prenume.Text) &&
                    !string.IsNullOrEmpty(cnp.Text)&&
                    !string.IsNullOrEmpty(Varsta.Text)&&
                    !string.IsNullOrEmpty(Judet.Text) &&
                   !string.IsNullOrEmpty(Localitate.Text) &&
                   !string.IsNullOrEmpty(NrReteta.Text) &&
                   !string.IsNullOrEmpty(Diagnostic.Text) &&
                   !string.IsNullOrEmpty(Rp.Text);
        }
        protected async void SalvareReteta_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "INSERT INTO Reteta (JudetUnitateSanitara, LocalitateUnitateSanitara, NumeUnitateSanitara, NumeMedic, PrenumeMedic, DataEmiteriiRetetei, NumePacient, PrenumePacient, CNP, Varsta, Judet, Localitate, Strada, Bloc, Scara, Apartament, Etaj, Numar, NumarReteta, Diagnostic, Tratament) " +
                                   "VALUES (@JudetUnitateSanitara, @LocalitateUnitateSanitara, @NumeUnitateSanitara, @NumeMedic, @PrenumeMedic, @DataEmiteriiRetetei, @NumePacient, @PrenumePacient, @CNP, @Varsta, @Judet, @Localitate, @Strada, @Bloc, @Scara, @Apartament, @Etaj, @Numar, @NumarReteta, @Diagnostic, @Tratament)";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@JudetUnitateSanitara", JudetUnitate.Text);
                    command.Parameters.AddWithValue("@LocalitateUnitateSanitara", LocalitateUnitate.Text);
                    command.Parameters.AddWithValue("@NumeUnitateSanitara", UnitateSanitara.Text);
                    command.Parameters.AddWithValue("@NumeMedic", NumeMedic.Text);
                    command.Parameters.AddWithValue("@PrenumeMedic", PrenumeMedic.Text);
                    command.Parameters.AddWithValue("@DataEmiteriiRetetei", data.Text);
                    command.Parameters.AddWithValue("@NumePacient", nume.Text);
                    command.Parameters.AddWithValue("@PrenumePacient", prenume.Text);
                    command.Parameters.AddWithValue("@CNP", cnp.Text);
                    command.Parameters.AddWithValue("@Varsta", Varsta.Text);
                    command.Parameters.AddWithValue("@Judet", Judet.Text);
                    command.Parameters.AddWithValue("@Localitate", Localitate.Text);
                    command.Parameters.AddWithValue("@Strada", Strada.Text);
                    command.Parameters.AddWithValue("@Bloc", Bloc.Text);
                    command.Parameters.AddWithValue("@Scara", Scara.Text);
                    command.Parameters.AddWithValue("@Apartament", Apartament.Text);
                    command.Parameters.AddWithValue("@Etaj", Etaj.Text);
                    command.Parameters.AddWithValue("@Numar", Numar.Text);
                    command.Parameters.AddWithValue("@NumarReteta", NrReteta.Text);
                    command.Parameters.AddWithValue("@Diagnostic", Diagnostic.Text);
                    command.Parameters.AddWithValue("@Tratament", Rp.Text);

                    connection.Open();
                    command.ExecuteNonQuery();
                    string cnpGasit = await GetCnpFromDatabase(nume.Text, prenume.Text);
                    if (!string.IsNullOrEmpty(cnpGasit))
                    {
                        var medication = new
                        {
                            userId = cnpGasit,
                            name = Rp.Text,
                            dosage = GenerateRandomDosage(),
                            observations = Diagnostic.Text
                        };

                        string baseUrl = "https://medassistapimanagement.azure-api.net/api";
                        string apiKey = "";
                        string url = $"{baseUrl}/Medication";
                        try
                        {
                            var response = await SendPostRequest(url, medication, apiKey);

                            if (response.IsSuccessStatusCode)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.href = 'Pacienti.aspx?username=" + Request.QueryString["username"] + "'", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la salvarea consultatiei!');", true);
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la trimiterea datelor către server!');", true);
                        }
                        ResetFields();
                        CalculeazaNrReteta();
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați toate câmpurile!');", true);
            }
        }
        private static string GenerateRandomDosage()
        {
            Random random = new Random();

            double morningDose = random.Next(0, 3) / 2.0;
            double noonDose = random.Next(0, 3) / 2.0;   
            double eveningDose = random.Next(0, 3) / 2.0; 
            int days = random.Next(1, 91);             

            return $"{morningDose} dimineata, {noonDose} pranz, {eveningDose} seara, {days} zile";
        }
        private async Task<string> GetCnpFromDatabase(string nume, string prenume)
        {
            string cnp = null;
            string query = "SELECT CNP FROM Pacienti WHERE Nume = @Nume AND Prenume = @Prenume";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nume", nume);
                command.Parameters.AddWithValue("@Prenume", prenume);

                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null)
                {
                    cnp = result.ToString();
                }
            }
            return cnp;
        }
        private async Task<HttpResponseMessage> SendPostRequest(string url, object data, string apiKey)
        {
            var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = jsonContent
            };

            if (!string.IsNullOrEmpty(apiKey))
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);
            }

            return await httpClient.SendAsync(request);
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
        protected void ResetFields()
        {
            nume.Text = null;
            prenume.Text = null;
            cnp.Text = null;
            Varsta.Text = null;
            Judet.Text = null;
            Localitate.Text = null;
            Strada.Text = null;
            Bloc.Text = null;
            Scara.Text = null;
            Apartament.Text = null;
            Etaj.Text = null;
            Numar.Text = null;
            Diagnostic.Text = null;
            Rp.Text = null;
            JudetUnitate.Text = null;
            UnitateSanitara.Text = null;
            LocalitateUnitate.Text = null;
        }
    }
}