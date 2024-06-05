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
    public partial class Consultatii : System.Web.UI.Page
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
                BindGridConsultatii(username, userRole);
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
        private void BindGridConsultatii(string username, string userRole)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                string query;

                if (userRole == "admin")
                {
                    query = "SELECT JudetUnitateSanitara, LocalitateUnitateSanitara, NumeUnitateSanitara, NumeMedic, PrenumeMedic, DataEmiteriiRetetei, NumePacient, PrenumePacient, CNP, Varsta, ZiuaNasterii, LunaNasterii, AnulNasterii, Profesie, LocMunca, Judet, Localitate, Strada, Apartament, Etaj, Numar, Antecedente, ConditiiDeMunca, Simptome, Diagnostic, CodDiagnostic, Recomandari FROM Consultatie";
                }
                else
                {
                    query = @"
                SELECT JudetUnitateSanitara, LocalitateUnitateSanitara, NumeUnitateSanitara, NumeMedic, PrenumeMedic, DataEmiteriiRetetei, NumePacient, PrenumePacient, CNP, Varsta, ZiuaNasterii, LunaNasterii, AnulNasterii, Profesie, LocMunca, Judet, Localitate, Strada, Apartament, Etaj, Numar, Antecedente, ConditiiDeMunca, Simptome, Diagnostic, CodDiagnostic, Recomandari 
                FROM Consultatie 
                WHERE NumePacient = (SELECT Nume FROM Users WHERE Utilizator = @Username) 
                AND PrenumePacient = (SELECT Prenume FROM Users WHERE Utilizator = @Username)";
                }

                MySqlCommand command = new MySqlCommand(query, connection);

                if (userRole != "admin")
                {
                    command.Parameters.AddWithValue("@Username", username);
                }

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

        }
    }
}