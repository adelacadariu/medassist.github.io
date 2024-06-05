using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FireSharp.Config;
using FireSharp.Interfaces;
using System.Data;
using FireSharp.Response;
using licenta_test;
using System.Net.PeerToPeer;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace licenta_test
{
    public partial class Register : System.Web.UI.Page
    {
        private static readonly HttpClient httpClient = new HttpClient();

        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void bRegister_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Username2.Text) && !string.IsNullOrEmpty(Password2.Text) && !string.IsNullOrEmpty(Email.Text))
            {
                if (!UserExists(Username2.Text))
                {
                    if (ValidateEmailFormat(Email.Text))
                    {
                        if (InsertUser(Username2.Text, Password2.Text, Email.Text, Name.Text, Prenume.Text, "utilizator"))
                        {
                            string cnp = await GetCnpFromDatabase(Name.Text, Prenume.Text, Email.Text);
                            if (!string.IsNullOrEmpty(cnp))
                            {
                                var user = new
                                {
                                    id = cnp,
                                    name = Name.Text + " " + Prenume.Text,
                                    email = Email.Text,
                                    password = Password2.Text
                                };

                                string baseUrl = "https://medassistapimanagement.azure-api.net/api";
                                string apiKey = "";
                                string url = $"{baseUrl}/User/SignUp";
                                var response = await SendPostRequest(url, user, apiKey);

                                if (response.IsSuccessStatusCode)
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.href = 'Home.aspx?username=" + Username2.Text + "';", true);
                                }
                                else
                                {
                                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la salvarea datelor pe server!');", true);
                                }
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Nu s-a găsit niciun pacient cu aceste informații.');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la salvarea datelor în baza de date!');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Adresa de email invalidă!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Utilizatorul există deja!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați utilizatorul, parola și email-ul!');", true);
            }
        }

        private async Task<string> GetCnpFromDatabase(string nume, string prenume, string email)
        {
            string cnp = null;
            string query = "SELECT CNP FROM Pacienti WHERE Nume = @Nume AND Prenume = @Prenume AND Email = @Email";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Nume", nume);
                command.Parameters.AddWithValue("@Prenume", prenume);
                command.Parameters.AddWithValue("@Email", email);

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
            request.Headers.Add("Ocp-Apim-Subscription-Key", apiKey);

            return await httpClient.SendAsync(request);
        }

        protected bool UserExists(string username)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Users WHERE Utilizator = @Username";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        protected bool InsertUser(string username, string password, string email, string name, string prenume, string role)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Users (Utilizator, Parola, Email, Nume, Prenume, Rol) VALUES (@Username, @Password, @Email, @Name, @Prenume, @Role)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Prenume", prenume);
                    command.Parameters.AddWithValue("@Role", role);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        protected bool ValidateEmailFormat(string email)
        {
            string emailPattern = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, emailPattern);
        }

    }
}