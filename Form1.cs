using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ComboBox = System.Windows.Forms.ComboBox;

namespace TpCalculette
{
    public partial class Bbliothèque : Form
    {
        private Class1 dbOperations;
        private MySqlConnection cnx = new MySqlConnection("server=localhost;userid=root;password=azsqaqwxsz;database=biblio");
        private MySqlCommand cmd = new MySqlCommand();
        private MySqlDataAdapter adapter = new MySqlDataAdapter();
        private ComboBox comboBox;
        private bool addClicked = false;
        private bool modifyClicked = false;
        private bool deleteClicked = false;
        private bool rechercheClicked = false;
        private string oldTitle = string.Empty;
        private string oldAuteur = string.Empty;
        private string oldPrice = string.Empty;
        private string oldEdition = string.Empty;
        private string newTitle = string.Empty;

        public Bbliothèque()
        {
            dbOperations = new Class1();
            InitializeComponent();
            rechercher.Enabled = true;
            Créer.Enabled = true;
            Modify.Enabled = false;
            Annuler.Enabled = false;
            Valider.Enabled = false;
            Supprimer.Enabled = false;
            textBoxTitre.Enabled = false;
            textBoxAuteur.Enabled = false;
            textBoxPrix.Enabled = false;
            textBoxEdition.Enabled = false;
            cmd.Connection = cnx;
            adapter.SelectCommand = cmd;

            comboBox = new ComboBox();

            textBoxTitre.TextChanged += textBoxTitre_TextChanged;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rechercher.Enabled = true;
            Créer.Enabled = true;
            Modify.Enabled = false;
            Annuler.Enabled = false;
            Valider.Enabled = false;
            Supprimer.Enabled = false;
            textBoxTitre.Enabled = false;
            textBoxAuteur.Enabled = false;
            textBoxPrix.Enabled = false;
            textBoxEdition.Enabled = false;
        }

        private void stockValues()
        {
            oldTitle = textBoxTitre.Text;
            oldAuteur = textBoxAuteur.Text;
            oldPrice = textBoxPrix.Text;
            oldEdition = textBoxEdition.Text;
        }

        private void viderZones()
        {
            textBoxTitre.Text = "";
            textBoxAuteur.Text = "";
            textBoxPrix.Text = "";
            textBoxEdition.Text = "";
        }

        private void Créer_Click(object sender, EventArgs e)
        {
            addClicked = true;
            rechercher.Enabled = false;
            Créer.Enabled = false;
            Modify.Enabled = false;
            Supprimer.Enabled = false;
            Annuler.Enabled = true;
            Valider.Enabled = true;
            textBoxTitre.Enabled = true;
            textBoxAuteur.Enabled = true;
            textBoxPrix.Enabled = true;
            textBoxEdition.Enabled = true;
        }

        private void Modify_Click(object sender, EventArgs e)
        {
            modifyClicked = true;
            rechercher.Enabled = true;
            Créer.Enabled = false;
            Modify.Enabled = false;
            Supprimer.Enabled = false;
            Annuler.Enabled = true;
            Valider.Enabled = true;
            textBoxTitre.Enabled = true;
            textBoxAuteur.Enabled = true;
            textBoxPrix.Enabled = true;
            textBoxEdition.Enabled = true;
            Modify.Enabled = false;

            stockValues();
        }

        private void Supprimer_Click(object sender, EventArgs e)
        {
            Modify.Enabled = false;
            Supprimer.Enabled = false;
            textBoxTitre.Enabled = false;
            textBoxAuteur.Enabled = false;
            textBoxPrix.Enabled = false;
            textBoxEdition.Enabled = false;
            Annuler.Enabled = true;
            Valider.Enabled = true;
            rechercher.Enabled = true;
            Créer.Enabled = false;
            deleteClicked = true;
            stockValues();
        }

        private void Valider_Click(object sender, EventArgs e)
        {
            if (addClicked)
            {
                bool isValid = CheckConditions();
                if (isValid)
                {
                    try
                    {
                        dbOperations.InsertData(textBoxTitre.Text, textBoxAuteur.Text, textBoxPrix.Text, textBoxEdition.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur : " + ex.Message);
                    }

                    ResetTextBoxColors();

                    addClicked = false;
                    rechercher.Enabled = true;
                    Créer.Enabled = true;
                    Modify.Enabled = false;
                    Supprimer.Enabled = false;
                    Annuler.Enabled = false;
                    Valider.Enabled = false;
                    textBoxTitre.Enabled = false;
                    textBoxAuteur.Enabled = false;
                    textBoxPrix.Enabled = false;
                    textBoxEdition.Enabled = false;
                    viderZones();

                }
                else
                {
                    
                }
            }

            if (modifyClicked)
            {
                bool isValid = CheckConditions();
                if (isValid)
                {
                    dbOperations.UpdateData(oldTitle, textBoxTitre.Text, textBoxAuteur.Text, textBoxPrix.Text, textBoxEdition.Text);
                    rechercher.Enabled = true;
                    Créer.Enabled = true;
                    Modify.Enabled = false;
                    Supprimer.Enabled = false;
                    Annuler.Enabled = false;
                    Valider.Enabled = false;
                    textBoxTitre.Enabled = false;
                    textBoxAuteur.Enabled = false;
                    textBoxPrix.Enabled = false;
                    textBoxEdition.Enabled = false;
                    viderZones();
                }
                else
                {
                  
                }
            }

            if (deleteClicked)
            {
                dbOperations.DeleteData(textBoxTitre.Text);
                rechercher.Enabled = true;
                Créer.Enabled = true;
                Modify.Enabled = false;
                Annuler.Enabled = false;
                Valider.Enabled = false;
                Supprimer.Enabled = false;
                textBoxTitre.Enabled = false;
                textBoxAuteur.Enabled = false;
                textBoxPrix.Enabled = false;
                textBoxEdition.Enabled = false;
                viderZones();
            }
        }


        private bool CheckConditions()
        {
            bool isValid = true;

            if (string.IsNullOrEmpty(textBoxTitre.Text) || textBoxTitre.Text.Length <= 3)
            {
                textBoxTitre.BackColor = System.Drawing.Color.Red;
                isValid = false;
            }
            else
            {
                textBoxTitre.BackColor = System.Drawing.SystemColors.Window;
            }

            if (string.IsNullOrEmpty(textBoxAuteur.Text) || textBoxAuteur.Text.Length <= 3)
            {
                textBoxAuteur.BackColor = System.Drawing.Color.Red;
                isValid = false;
            }
            else
            {
                textBoxAuteur.BackColor = System.Drawing.SystemColors.Window;
            }

            if (!decimal.TryParse(textBoxPrix.Text, out _))
            {
                textBoxPrix.BackColor = System.Drawing.Color.Red;
                isValid = false;
            }
            else
            {
                textBoxPrix.BackColor = System.Drawing.SystemColors.Window;
            }

            

            return isValid;
        }

        private void ResetTextBoxColors()
        {
            textBoxTitre.BackColor = System.Drawing.SystemColors.Window;
            textBoxAuteur.BackColor = System.Drawing.SystemColors.Window;
            textBoxPrix.BackColor = System.Drawing.SystemColors.Window;
            
        }


        private void Annuler_Click(object sender, EventArgs e)
        {


            if (addClicked || rechercheClicked)
            {
                viderZones();
                rechercher.Enabled = true;
                Créer.Enabled = true;
                Modify.Enabled = false;
                Annuler.Enabled = false;
                Valider.Enabled = false;
                Supprimer.Enabled = false;
                textBoxTitre.Enabled = false;
                textBoxAuteur.Enabled = false;
                textBoxPrix.Enabled = false;
                textBoxEdition.Enabled = false;
                Controls.Remove(comboBox);
                Controls.Add(textBoxTitre);
                textBoxTitre.Enabled = false;
                ResetTextBoxColors();
                viderZones();

                if (cnx.State == ConnectionState.Open)
                {
                    cnx.Close();
                }
            } addClicked = false;

            if (modifyClicked || deleteClicked)
            {
                textBoxTitre.Text = oldTitle;
                textBoxAuteur.Text = oldAuteur;
                textBoxPrix.Text = oldPrice;
                textBoxEdition.Text = oldEdition;

                Supprimer.Enabled = false;
                textBoxTitre.Enabled = false;
                textBoxAuteur.Enabled = false;
                textBoxPrix.Enabled = false;
                textBoxEdition.Enabled = false;
                Valider.Enabled = false;
                Créer.Enabled = false;
                Modify.Enabled = true;
                Supprimer.Enabled = true;
                Annuler.Enabled = true;
                rechercher.Enabled = true;
                modifyClicked = false;
                deleteClicked = false;
                ResetTextBoxColors();
                viderZones();
            }
            modifyClicked = false;

        }

        private void textBoxTitre_TextChanged(object sender, EventArgs e)
        {
            string currentInput = textBoxTitre.Text;

            if (dbOperations != null)
            {
                if (!string.Equals(currentInput, oldTitle, StringComparison.OrdinalIgnoreCase))
                {
                    newTitle = currentInput;
                }
                else
                {
                    newTitle = oldTitle;
                }
            }
        }

        private void rechercher_Click(object sender, EventArgs e)
        {
            try
            {
                rechercheClicked = true;
                Valider.Enabled = false;
                Supprimer.Enabled = false;
                textBoxTitre.Enabled = false;
                textBoxAuteur.Enabled = false;
                textBoxPrix.Enabled = false;
                textBoxEdition.Enabled = false;
                rechercher.Enabled = false;
                Créer.Enabled = false;
                Modify.Enabled = true;
                Supprimer.Enabled = true;
                Annuler.Enabled = true;

                cnx.Open();

                comboBox.Items.Clear();

                cmd.CommandText = "SELECT titre FROM livre";
                adapter.SelectCommand = cmd;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string titre = reader.GetString(0);
                            comboBox.Items.Add(titre);
                        }
                    }
                }

                comboBox.Location = textBoxTitre.Location;
                comboBox.Size = textBoxAuteur.Size;

                Controls.Remove(textBoxTitre);
                Controls.Add(comboBox);
                comboBox.SelectedIndexChanged -= ComboBox_SelectedIndexChanged;
                comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
                comboBox.Focus();
                comboBox.DroppedDown = true;
                textBoxTitre.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }
        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox.SelectedIndex >= 0)
            {
                textBoxTitre.Text = comboBox.SelectedItem.ToString();
                Controls.Remove(comboBox);
                Controls.Add(textBoxTitre);
                textBoxTitre.Enabled = true;

                try
                {
                    Class1 dbOperations = new Class1();
                    DataTable dt = dbOperations.GetLData(textBoxTitre.Text);

                    if (dt.Rows.Count > 0)
                    {
                        textBoxAuteur.Text = dt.Rows[0]["auteur"].ToString();
                        textBoxPrix.Text = dt.Rows[0]["prix"].ToString();
                        textBoxEdition.Text = dt.Rows[0]["Edition"].ToString();

                        textBoxAuteur.Enabled = false;
                        textBoxPrix.Enabled = false;
                        textBoxEdition.Enabled = false;
                    }
                    else
                    {
                        textBoxAuteur.Text = string.Empty;
                        textBoxPrix.Text = string.Empty;
                        textBoxEdition.Text = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erreur : " + ex.Message);
                }
            }

            Modify.Enabled = true;
            Supprimer.Enabled = true;
        }

        private void lBbliothèque_Click(object sender, EventArgs e)
        {

        }

        private void Ltittre_Click(object sender, EventArgs e)
        {

        }

        private void Lauteur_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Ledition_Click(object sender, EventArgs e)
        {

        }

        private void Bbliothèque_Load(object sender, EventArgs e)
        {

        }

        private void textBoxAuteur_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPrix_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEdition_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
