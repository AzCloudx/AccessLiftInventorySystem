    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using System.Data.SqlClient;

    namespace AccessLift
    {
        public partial class StockInTransactionForm : Form
        {
            public StockInTransactionForm()
            {
                InitializeComponent();
            }

            private void StockInTransactionForm_Load(object sender, EventArgs e)
            {
                PopulateHouseCodes();
            }

            private void PopulateHouseCodes()
            {
                // Clear existing items before repopulating
                comboBox1.Items.Clear();

                // Connect to the database and retrieve housecodes
                ServerAccessClass linkObject = new ServerAccessClass();
                string localServerString = linkObject.Server;
                string localDBString = linkObject.DB;
                string connectionString = "Data Source = " + localServerString + ";" + "Initial Catalog = " + localDBString + ";" + "Integrated Security = True;";
                string query = "SELECT HouseCode FROM tblItem";

                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        SqlCommand command = new SqlCommand(query, connection);
                        SqlDataReader reader = command.ExecuteReader();

                        // Populate the ComboBox with retrieved housecodes
                        while (reader.Read())
                        {
                            comboBox1.Items.Add(reader["HouseCode"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            private void button1_Click(object sender, EventArgs e)
            {
                // Get data from form controls
                string houseCode = comboBox1.SelectedItem.ToString();
                DateTime transactionDate = dateTimePicker1.Value;
                int quantityIn = 0;
                if (!int.TryParse(textBox3.Text, out quantityIn))
                {
                    MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Insert into the database
                try
                {
                ServerAccessClass linkObject = new ServerAccessClass();
                string localServerString = linkObject.Server;
                string localDBString = linkObject.DB;
                string connectionString = "Data Source = " + localServerString + ";" + "Initial Catalog = " + localDBString + ";" + "Integrated Security = True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "INSERT INTO tblTransaction (TrDate, HouseCode, TrType, Quantity) " +
                                       "VALUES (@TrDate, @HouseCode, 'O', @Quantity)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@TrDate", transactionDate);
                        command.Parameters.AddWithValue("@HouseCode", houseCode);
                        command.Parameters.AddWithValue("@Quantity", quantityIn);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Transaction submitted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
