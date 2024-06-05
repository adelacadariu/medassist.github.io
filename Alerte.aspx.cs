using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace licenta_test
{
    public partial class Alerte : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";

        private static readonly HttpClient httpClient = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                string cnp = Request.QueryString["pacient"];
                string userRole = GetUserRole(username);
                if (userRole != "admin")
                {
                    HideAdminSections();
                }
                if (!string.IsNullOrEmpty(cnp))
                {
                    LoadPacientData(cnp);
                }
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
                            Pacient.Text = reader["Nume"].ToString() + " " + reader["Prenume"].ToString();

                        }
                    }
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


        protected void SalvareParametrii_Click(object sender, EventArgs e)
        {
            string username = Request.QueryString["username"];
            string pacientId = GetUserCNP(username);

            float valMinTemp = Convert.ToSingle(txtValMinTemp.Text);
            float valMaxTemp = Convert.ToSingle(txtValMaxTemp.Text);
            int valMinPuls = Convert.ToInt32(txtValMinPuls.Text);
            int valMaxPuls = Convert.ToInt32(txtValMaxPuls.Text);
            int valMinUmiditate = Convert.ToInt32(txtValMinUmiditate.Text);
            int valMaxUmiditate = Convert.ToInt32(txtValMaxUmiditate.Text);
            float valMinEkg = Convert.ToSingle(txtValMinEKG.Text);
            float valMaxEkg = Convert.ToSingle(txtValMaxEKG.Text);
            int valMinRitmCardiac = Convert.ToInt32(txtValMinRitmCardiac.Text);
            int valMaxRitmCardiac = Convert.ToInt32(txtValMaxRitmCardiac.Text);
            // Adaugă și alte valori pentru ceilalți parametrii

            SaveParametrii(pacientId, valMinTemp, valMaxTemp, valMinPuls, valMaxPuls, valMinUmiditate, valMaxUmiditate, valMinEkg, valMaxEkg, valMaxRitmCardiac, valMinRitmCardiac);

            // Dacă dorești să faci ceva după salvarea parametrilor, adaugă codul aici

        }

        protected void SalvareAlerta_Click(object sender, EventArgs e)
        {
            string username = Request.QueryString["username"];
            string cnp = Request.QueryString["pacient"];
            string pacientId = GetUserCNP(cnp);

            string alerta = Alerta.Text;
            string parametru = Parametru.SelectedValue;

            SaveAlertaAsync(cnp, alerta, parametru);
        }
        private string GetUserCNP(string username)
        {
            string cnp = "";

            // Interogare pentru a obține numele și prenumele din tabela Users
            string queryGetUserData = "SELECT Nume, Prenume FROM Users WHERE Utilizator = @Username";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand commandGetUserData = new MySqlCommand(queryGetUserData, connection);
                commandGetUserData.Parameters.AddWithValue("@Username", username);
                connection.Open();
                MySqlDataReader reader = commandGetUserData.ExecuteReader();

                if (reader.Read())
                {
                    // Extragem numele și prenumele din rezultatele interogării
                    string nume = reader["Nume"].ToString();
                    string prenume = reader["Prenume"].ToString();
                    reader.Close();

                    // Interogare pentru a obține CNP-ul din tabela Pacienti folosind numele și prenumele
                    string queryGetCNP = "SELECT CNP FROM Pacienti WHERE Nume = @Nume AND Prenume = @Prenume";
                    MySqlCommand commandGetCNP = new MySqlCommand(queryGetCNP, connection);
                    commandGetCNP.Parameters.AddWithValue("@Nume", nume);
                    commandGetCNP.Parameters.AddWithValue("@Prenume", prenume);

                    // Obținem CNP-ul
                    object resultCNP = commandGetCNP.ExecuteScalar();
                    if (resultCNP != null && resultCNP != DBNull.Value)
                    {
                        cnp = resultCNP.ToString();
                    }
                }
            }

            return cnp;
        }
        private async Task SaveAlertaAsync(string pacientId, string alerta, string parametru)
        {
            string cnp = Request.QueryString["pacient"];
            string query = "INSERT INTO Alerta (CNP, Alerta, Parametru) VALUES (@CNP, @Alerta, @Parametru)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CNP", cnp);
                command.Parameters.AddWithValue("@Alerta", alerta);
                command.Parameters.AddWithValue("@Parametru", parametru);
                connection.Open();
                command.ExecuteNonQuery();
            }
            string url = "https://medassistapimanagement.azure-api.net/api/Notification";
            string apiKey = "";

            try
            {
                var response = await SendNotification(url, cnp, parametru, alerta, apiKey);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Notificare trimisă și salvată cu succes!");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la trimiterea notificării!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la trimiterea datelor către server!');", true);
            }

            Alerta.Text = null;
            Parametru.SelectedValue = null;
        }
        private async Task<HttpResponseMessage> SendNotification(string url, string userId, string parameter, string message, string apiKey)
        {
            var notificationData = new
            {
                userId = userId,
                parameter = parameter,
                message = message
            };

            var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(notificationData), Encoding.UTF8, "application/json");

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


        private async void SaveParametrii(string pacientId, float valMinTemp, float valMaxTemp, int valMinPuls, int valMaxPuls, int valMinUmiditate, int valMaxUmiditate, float EkgMax, float EgkMin , int RitmCMax, int RitmCMin)
        {
            string cnp = Request.QueryString["pacient"];
            string query = "INSERT INTO Parametrii  (CNP, TemperaturaMinima, TemperaturaMaxima, UmiditateMinima, UmiditateMaxima, EKGminim, EKGmaxim, RitmCardiacMinim, RitmCardiacMaxim, PulsMinim, PulsMaxim) " +
                  "VALUES (@CNP, @ValMinTemp, @ValMaxTemp, @ValMinUmiditate, @ValMaxUmiditate, @EgkMin, @EkgMax, @RitmCMin, @RitmCMax, @PulsMinim, @PulsMaxim)";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@CNP", cnp);
                command.Parameters.AddWithValue("@ValMinTemp", valMinTemp);
                command.Parameters.AddWithValue("@ValMaxTemp", valMaxTemp);
                command.Parameters.AddWithValue("@PulsMinim", valMinPuls);
                command.Parameters.AddWithValue("@PulsMaxim", valMaxPuls);
                command.Parameters.AddWithValue("@ValMinUmiditate", valMinUmiditate);
                command.Parameters.AddWithValue("@ValMaxUmiditate", valMaxUmiditate);
                command.Parameters.AddWithValue("@EgkMin", EgkMin);
                command.Parameters.AddWithValue("@EkgMax", EkgMax);
                command.Parameters.AddWithValue("@RitmCMax", RitmCMax);
                command.Parameters.AddWithValue("@RitmCMin", RitmCMin);
                connection.Open();
                command.ExecuteNonQuery();
            }

            string url = "https://medassistapimanagement.azure-api.net/api/PatientRule";
            string apiKey = ""; // Adauga cheia API aici

            try
            {
                var response = await SendPatientRule(url, cnp, valMaxPuls, valMinPuls, valMaxTemp, valMinTemp, valMaxUmiditate, valMaxUmiditate, EgkMin, EkgMax, RitmCMax, RitmCMax, apiKey);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Reguli pacient salvate cu succes!");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la salvarea regulilor pacientului!');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la trimiterea datelor către server!');", true);
            }
            txtValMaxEKG.Text = "";
            txtValMinEKG.Text = "";
            txtValMinPuls.Text = "";
            txtValMaxPuls.Text = "";
            txtValMinTemp.Text = "";
            txtValMaxTemp.Text = "";
            txtValMinUmiditate.Text = "";
            txtValMaxUmiditate.Text = "";
            txtValMaxRitmCardiac.Text = "";
            txtValMinRitmCardiac.Text = "";


        }
        private async Task<HttpResponseMessage> SendPatientRule(string url, string userId, int maxPulse, int minPulse, float maxTemperature, float minTemperature, int maxHumidity, int minHumidity, float maxEkgMv, float minEkgMv, int maxHeartRate, int minHeartRate, string apiKey)
        {
            var ruleData = new
            {
                userId = (string)userId,
                maxPulse = (int)maxPulse,
                minPulse = (int)minPulse,
                maxTemperature = (int)maxTemperature,
                minTemperature = (int)minTemperature,
                maxHumidity = (int)maxHumidity,
                minHumidity = (int)minHumidity,
                maxEkgMv = (int)maxEkgMv,
                minEkgMv = (int)minEkgMv,
                maxHeartRate = (int)maxHeartRate,
                minHeartRate = (int)minHeartRate
            };

            var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(ruleData), Encoding.UTF8, "application/json");

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
    }
}
   