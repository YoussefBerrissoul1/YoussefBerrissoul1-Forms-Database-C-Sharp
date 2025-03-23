using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Windows.Forms;

namespace TpCalculette
{
    public class Class1
    {
        private MySqlConnection cnx;

        public Class1()
        {
            cnx = new MySqlConnection("server=localhost;userid=root;password=azsqaqwxsz;database=biblio");
        }

        public void InsertData(string title, string author, string price, string edition)
        {
            try
            {
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "INSERT INTO livre (titre, auteur, prix, Edition) VALUES (@title, @author, @price, @edition)";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
        }

        public void UpdateData(string oldTitle, string newTitle, string author, string price, string edition)
        {
            try
            {
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "UPDATE livre SET titre=@newTitle, auteur=@author, prix=@price, Edition=@edition WHERE titre=@oldTitle";
                cmd.Parameters.AddWithValue("@newTitle", newTitle);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.Parameters.AddWithValue("@oldTitle", oldTitle);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
        }

        public void DeleteData(string title)
        {
            try
            {
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "DELETE FROM livre WHERE titre=@title";
                cmd.Parameters.AddWithValue("@title", title);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
        }

        public DataTable GetLData(string title)
        {
            DataTable dt = new DataTable();

            try
            {
                cnx.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = cnx;
                cmd.CommandText = "SELECT auteur, prix, Edition FROM livre WHERE titre=@title";
                cmd.Parameters.AddWithValue("@title", title);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
            finally
            {
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }

            return dt;
        }
    }
}
