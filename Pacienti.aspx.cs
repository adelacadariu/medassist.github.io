using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace licenta_test
{
    public partial class Pacienti : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                Session["Username"] = username;
                string userRole = GetUserRole(username);
                if (userRole != "admin")
                {
                    HideAdminSections();
                }
                BindGrid();
                LoadPacienti();
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
        private void LoadPacienti()
        {
            
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT CNP, Nume, Prenume FROM Pacienti"; 
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string id = reader["CNP"].ToString();
                                string fullName = reader["Nume"].ToString() + " " + reader["Prenume"].ToString();
                                ddlPacienti.Items.Add(new ListItem(fullName, id));
                            }
                        }
                    }
                }
            }
        }
        private void BindGrid()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand("SELECT Nume, Prenume, Varsta, CNP, Telefon, Email, Profesie, LocMunca, Judet, Localitate, Strada, Bloc, Scara, Apartament, Etaj, Numar, IstoricMedical, Alergii, ConsultatiiCardiologice FROM Pacienti", connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ModificaDatePacient_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Pacient.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }

        protected void StergerePacient_Click(object sender, EventArgs e)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string cnp = ddlPacienti.SelectedValue;
                MySqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    // Ștergerea rețelelor asociate
                    MySqlCommand deleteReteleCommand = new MySqlCommand("DELETE FROM Reteta WHERE CNP = @CNP", connection, transaction);
                    deleteReteleCommand.Parameters.AddWithValue("@CNP", cnp);
                    deleteReteleCommand.ExecuteNonQuery();

                    // Ștergerea consultațiilor asociate
                    MySqlCommand deleteConsultatiiCommand = new MySqlCommand("DELETE FROM Consultatie WHERE CNP = @CNP", connection, transaction);
                    deleteConsultatiiCommand.Parameters.AddWithValue("@CNP", cnp);
                    deleteConsultatiiCommand.ExecuteNonQuery();

                    //// Ștergerea alertelor asociate
                    //MySqlCommand deleteAlerteCommand = new MySqlCommand("DELETE FROM Alerte WHERE CNP = @CNP", connection, transaction);
                    //deleteAlerteCommand.Parameters.AddWithValue("@CNP", cnp);
                    //deleteAlerteCommand.ExecuteNonQuery();

                    // Ștergerea scrisorilor medicale asociate
                    MySqlCommand deleteScrisoriCommand = new MySqlCommand("DELETE FROM ScrisoareMedicala WHERE CNP_Pacient = @CNP", connection, transaction);
                    deleteScrisoriCommand.Parameters.AddWithValue("@CNP", cnp);
                    deleteScrisoriCommand.ExecuteNonQuery();

                    //// Ștergerea biletelor de trimitere asociate
                    //MySqlCommand deleteBileteCommand = new MySqlCommand("DELETE FROM BiletDeTrimitere WHERE CNPAsigurat = @CNP", connection, transaction);
                    //deleteBileteCommand.Parameters.AddWithValue("@CNP", cnp);
                    //deleteBileteCommand.ExecuteNonQuery();

                    // Ștergerea pacientului
                    MySqlCommand deletePacientCommand = new MySqlCommand("DELETE FROM Pacienti WHERE CNP = @CNP", connection, transaction);
                    deletePacientCommand.Parameters.AddWithValue("@CNP", cnp);
                    deletePacientCommand.ExecuteNonQuery();

                    // Commit the transaction
                    transaction.Commit();
                    BindGrid();
                    LoadPacienti();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction in case of error
                    transaction.Rollback();
                    // Handle the error (e.g., log the error, display a message to the user, etc.)
                }
            }
        }
        protected void CreareRetetaNoua_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Reteta.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }

        protected void CreareConsultatieNoua_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Consultatie.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }

        protected void TrimitereNoua_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Trimitere.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }

        protected void SetareAlerte_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Alerte.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }

        protected void CreareScrisoareNoua_Click(object sender, EventArgs e)
        {
            string selectedPatientId = ddlPacienti.SelectedValue;
            string usernameMedicului = Request.QueryString["username"];

            string url = $"Scrisoare.aspx?username={usernameMedicului}&pacient={selectedPatientId}";
            Response.Redirect(url);
        }
    }
}