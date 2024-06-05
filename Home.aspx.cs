using Hl7.Fhir.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
//using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace licenta_test
{
    public partial class Home : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string username = Request.QueryString["username"];
                string userRole = GetUserRole(username);

                if (!string.IsNullOrEmpty(username))
                {
                    if (userRole!= "admin")
                    {
                        HideAdminSections();
                        cardPacienti.Visible = false;
                    }
                }
                int numarRetete = GetNumarRetete(username, userRole);
                reteteCount.InnerText = numarRetete.ToString();
                int numarPacienti = GetNumarPacienti();
                PacientiCount.InnerText = numarPacienti.ToString();
                int numarConsultatii = GetNumarConsultatii(username,userRole);
                ConsultatiiCount.InnerText = numarConsultatii.ToString();
                //int numarTrimiteri = GetNumarTrimiterii();
                //TrimiteriCount.InnerText = numarTrimiteri.ToString();
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
        private int GetNumarRetete(string username, string userRole)
        {
            int count = 0;
            string query;

            if (userRole == "admin")
            {
                query = "SELECT COUNT(*) FROM Reteta";
            }
            else
            {
                query = @"
            SELECT COUNT(*) 
            FROM Reteta 
            WHERE NumePacient = (SELECT Nume FROM Users WHERE Utilizator = @Username) 
            AND PrenumePacient = (SELECT Prenume FROM Users WHERE Utilizator = @Username)";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                if (userRole != "admin")
                {
                    command.Parameters.AddWithValue("@Username", username);
                }

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }
            }

            return count;
        }
        private int GetNumarPacienti()
        {
            int count = 0;

            string query = "SELECT COUNT(*) FROM Pacienti";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }
            }

            return count;
        }
        private int GetNumarConsultatii(string username, string userRole)
        {
            int count = 0;
            string query;

            if (userRole == "admin")
            {
                query = "SELECT COUNT(*) FROM Consultatie";
            }
            else
            {
                query = @"
            SELECT COUNT(*) 
            FROM Consultatie 
            WHERE NumePacient = (SELECT Nume FROM Users WHERE Utilizator = @Username) 
            AND PrenumePacient = (SELECT Prenume FROM Users WHERE Utilizator = @Username)";
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                MySqlCommand command = new MySqlCommand(query, connection);

                if (userRole != "admin")
                {
                    command.Parameters.AddWithValue("@Username", username);
                }

                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    count = Convert.ToInt32(result);
                }
            }

            return count;
        }
        //private int GetNumarTrimiterii()
        //{
        //    int count = 0;

        //    string query = "SELECT COUNT(*) FROM BiletDeTrimitere ";

        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        MySqlCommand command = new MySqlCommand(query, connection);
        //        connection.Open();
        //        object result = command.ExecuteScalar();
        //        if (result != null && result != DBNull.Value)
        //        {
        //            count = Convert.ToInt32(result);
        //        }
        //    }

        //    return count;
        //}
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

        public Control FindControlRecursive(Control root, string id)
        {
            if (root.ID == id)
            {
                return root;
            }

            foreach (Control control in root.Controls)
            {
                Control foundControl = FindControlRecursive(control, id);
                if (foundControl != null)
                {
                    return foundControl;
                }
            }

            return null;
        }
    }
}