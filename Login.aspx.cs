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
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace licenta_test
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = "server=rhx.h.filess.io;user=proiect_starputegg;database=proiect_starputegg;port=3307;password=7a30521a801fa1bba1b805ced37f8663519aaf81";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FirebaseConnection.Text = "Conexiunea la baza de date a fost realizata cu succes!";
            }
        }

        protected async void b_login_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Username.Text))
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT Parola FROM Users WHERE Utilizator = @Utilizator";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Utilizator", Username.Text.ToString());
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string passwordFromDatabase = result.ToString();
                            if (passwordFromDatabase == Password.Text.ToString())
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "redirect", "window.location.href = 'Home.aspx?username=" + Username.Text + "';", true);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Parola incorecta!');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Utilizatorul introdus nu a fost gasit!');", true);
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "alert('Te rog sa introduci un utilizator!');", true);
            }
        }
    }
}

