using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace licenta_test
{
    public partial class Pacient : System.Web.UI.Page
    {

        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string cnp = Request.QueryString["pacient"];
                string username = Request.QueryString["username"];
                string userRole = GetUserRole(username);
                SalvareModificari.Enabled = !string.IsNullOrEmpty(cnp);
                Submit.Enabled = string.IsNullOrEmpty(cnp);
                if (!string.IsNullOrEmpty(cnp))
                {
                    LoadPacientData(cnp);
                    
                }
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
                            Nume.Text = reader["Nume"].ToString();
                            Prenume.Text = reader["Prenume"].ToString();
                            Varsta.Text = reader["Varsta"].ToString();
                            cnp.Text = reader["CNP"].ToString();
                            Telefon.Text = reader["Telefon"].ToString();
                            email.Text = reader["Email"].ToString();
                            Profesie.Text = reader["Profesie"].ToString();
                            LocMunca.Text = reader["LocMunca"].ToString();
                            Judet.Text = reader["Judet"].ToString();
                            Localitate.Text = reader["Localitate"].ToString();
                            Strada.Text = reader["Strada"].ToString();
                            Bloc.Text = reader["Bloc"].ToString();
                            Scara.Text = reader["Scara"].ToString();
                            Etaj.Text = reader["Etaj"].ToString();
                            Apartament.Text = reader["Apartament"].ToString();
                            Numar.Text = reader["Numar"].ToString();
                            IstoricMedical.Text = reader["IstoricMedical"].ToString();
                            Alergii.Text = reader["Alergii"].ToString();
                            ConsultatiiCardiologice.Text = reader["ConsultatiiCardiologice"].ToString();
                        }
                    }
                }
            }
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            if (IsFormValid())
            {
                if (!PacientExists(cnp.Text))
                {
                    if (InsertPacient(Nume.Text, Prenume.Text, Varsta.Text, cnp.Text, Telefon.Text, email.Text, Profesie.Text, LocMunca.Text, Judet.Text, Localitate.Text, Strada.Text, Bloc.Text, Scara.Text, Apartament.Text, Etaj.Text, Numar.Text, IstoricMedical.Text, Alergii.Text, ConsultatiiCardiologice.Text))
                    {
                        ResetFields();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Eroare la salvarea datelor pacientului!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Pacientul există deja în baza de date!');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați toate câmpurile!');", true);
            }
        }
        protected bool PacientExists(string cnp)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM Pacienti WHERE CNP = @CNP";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CNP", cnp);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        protected bool InsertPacient(string nume, string prenume, string varsta, string cnp, string telefon, string email, string profesie, string locMunca, string judet, string localitate, string strada, string bloc, string scara, string apartament, string etaj, string numar, string istoricMedical, string alergii, string consultatiiCardiologice)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Pacienti (Nume, Prenume, Varsta, CNP, Telefon, Email, Profesie, LocMunca, Judet, Localitate, Strada, Bloc, Scara, Apartament, Etaj, Numar, IstoricMedical, Alergii, ConsultatiiCardiologice) VALUES (@Nume, @Prenume, @Varsta, @CNP, @Telefon, @Email, @Profesie, @LocMunca, @Judet, @Localitate, @Strada, @Bloc, @Scara, @Apartament, @Etaj, @Numar, @IstoricMedical, @Alergii, @ConsultatiiCardiologice)";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Nume", nume);
                    command.Parameters.AddWithValue("@Prenume", prenume);
                    command.Parameters.AddWithValue("@Varsta", varsta);
                    command.Parameters.AddWithValue("@CNP", cnp);
                    command.Parameters.AddWithValue("@Telefon", telefon);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Profesie", profesie);
                    command.Parameters.AddWithValue("@LocMunca", locMunca);
                    command.Parameters.AddWithValue("@Judet", judet);
                    command.Parameters.AddWithValue("@Localitate", localitate);
                    command.Parameters.AddWithValue("@Strada", strada);
                    command.Parameters.AddWithValue("@Bloc", bloc);
                    command.Parameters.AddWithValue("@Scara", scara);
                    command.Parameters.AddWithValue("@Apartament", apartament);
                    command.Parameters.AddWithValue("@Etaj", etaj);
                    command.Parameters.AddWithValue("@Numar", numar);
                    command.Parameters.AddWithValue("@IstoricMedical", istoricMedical);
                    command.Parameters.AddWithValue("@Alergii", alergii);
                    command.Parameters.AddWithValue("@ConsultatiiCardiologice", consultatiiCardiologice);
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        protected void ResetFields()
        {
            Nume.Text = "";
            Prenume.Text = "";
            Varsta.Text = "";
            cnp.Text = "";
            Telefon.Text = "";
            email.Text = "";
            Profesie.Text = "";
            LocMunca.Text = "";
            Judet.Text = "";
            Localitate.Text = "";
            Strada.Text = "";
            Bloc.Text = "";
            Scara.Text = "";
            Apartament.Text = "";
            Etaj.Text = "";
            Numar.Text = "";
            IstoricMedical.Text = "";
            Alergii.Text = "";
            ConsultatiiCardiologice.Text = "";
        }
        private bool IsFormValid()
        {
            return !string.IsNullOrEmpty(Nume.Text) &&
                   !string.IsNullOrEmpty(Prenume.Text) &&
                   !string.IsNullOrEmpty(cnp.Text) &&
                   !string.IsNullOrEmpty(Varsta.Text) &&
                   !string.IsNullOrEmpty(Telefon.Text) &&
                   !string.IsNullOrEmpty(email.Text) &&
                   !string.IsNullOrEmpty(Judet.Text) &&
                   !string.IsNullOrEmpty(Localitate.Text);
        }
        protected void SalvareModificariPacient_Click(object sender, EventArgs e)
        {
            string cnpId = Request.QueryString["pacient"];
            if (!string.IsNullOrEmpty(cnpId))
            { 
                if(IsFormValid())
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "UPDATE Pacienti SET Nume = @Nume, Prenume = @Prenume, Varsta = @Varsta, CNP = @CNP, Telefon = @Telefon, Email = @Email, Profesie = @Profesie, LocMunca = @LocMunca, Judet = @Judet, Localitate = @Localitate, Strada = @Strada, Bloc = @Bloc, Scara = @Scara, Apartament = @Apartament, Etaj = @Etaj, Numar = @Numar, IstoricMedical = @IstoricMedical, Alergii = @Alergii, ConsultatiiCardiologice = @ConsultatiiCardiologice WHERE CNP = @PacientId";
                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Nume", Nume.Text);
                            command.Parameters.AddWithValue("@Prenume", Prenume.Text);
                            command.Parameters.AddWithValue("@Varsta", Varsta.Text);
                            command.Parameters.AddWithValue("@CNP", cnp.Text);
                            command.Parameters.AddWithValue("@Telefon", Telefon.Text);
                            command.Parameters.AddWithValue("@Email", email.Text);
                            command.Parameters.AddWithValue("@Profesie", Profesie.Text);
                            command.Parameters.AddWithValue("@LocMunca", LocMunca.Text);
                            command.Parameters.AddWithValue("@Judet", Judet.Text);
                            command.Parameters.AddWithValue("@Localitate", Localitate.Text);
                            command.Parameters.AddWithValue("@Strada", Strada.Text);
                            command.Parameters.AddWithValue("@Bloc", Bloc.Text);
                            command.Parameters.AddWithValue("@Scara", Scara.Text);
                            command.Parameters.AddWithValue("@Apartament", Apartament.Text);
                            command.Parameters.AddWithValue("@Etaj", Etaj.Text);
                            command.Parameters.AddWithValue("@Numar", Numar.Text);
                            command.Parameters.AddWithValue("@IstoricMedical", IstoricMedical.Text);
                            command.Parameters.AddWithValue("@Alergii", Alergii.Text);
                            command.Parameters.AddWithValue("@ConsultatiiCardiologice", ConsultatiiCardiologice.Text);
                            command.Parameters.AddWithValue("@PacientId", cnpId);

                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                                ResetFields();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Vă rugăm completați toate câmpurile!');", true);
                }
            }
        }
    }
}