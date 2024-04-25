using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessLift
{

    public partial class ItemRegistrationForm : Form
    {
        public ItemRegistrationForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ItemRegistrationForm_Load(object sender, EventArgs e)
        {
            ServerAccessClass linkObject = new ServerAccessClass();
            string localServerString = linkObject.Server;
            string localDBString = linkObject.DB;
            string connectionString = "Data Source = " + localServerString + ";" + "Initial Catalog = " + localDBString + ";" + "Integrated Security = True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            listView1.Items.Clear();
            List<Item> listItems = new List<Item>();
            List<Supplier> listSuppliers = new List<Supplier>();
            string queryString = "SELECT * FROM tblItem ORDER BY HouseCode;";
            SqlCommand command = new SqlCommand(queryString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Item item = new Item();
                item.HouseCode = reader["HouseCode"].ToString();
                item.ItemName = reader["ItemName"].ToString();
                item.Description = reader["Description"].ToString();
                item.SupplierName = reader["SupplierName"].ToString();
                listItems.Add(item);
                ListViewItem lvi = new ListViewItem(item.HouseCode);
                lvi.SubItems.Add(item.ItemName);
                lvi.SubItems.Add(item.Description);
                lvi.SubItems.Add(item.SupplierName);
                listView1.Items.Add(lvi);
            }
            textBox1.Text = textBox2.Text = textBox3.Text = string.Empty;
            textBox1.Focus();
            reader.Close();
            queryString = "SELECT * FROM tblSupplier ORDER BY SupplierName;";
            comboBox1.Items.Clear();
            command = new SqlCommand(queryString, connection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.SupplierName = reader["SupplierName"].ToString();
                supplier.Address = reader["Address"].ToString();
                listSuppliers.Add(supplier);
                comboBox1.Items.Add(supplier.SupplierName);
            }
            reader.Close();
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Get the values from textboxes and combobox
            string houseCode = textBox1.Text;
            string itemName = textBox2.Text;
            string description = textBox3.Text;
            string supplierName = comboBox1.SelectedItem.ToString(); // Assuming an item is selected

            // Validate if all necessary fields are filled
            if (string.IsNullOrWhiteSpace(houseCode) || string.IsNullOrWhiteSpace(itemName) ||
                string.IsNullOrWhiteSpace(description) || string.IsNullOrWhiteSpace(supplierName))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }

            // Insert into the database
            try
            {
                ServerAccessClass linkObject = new ServerAccessClass();
                string localServerString = linkObject.Server;
                string localDBString = linkObject.DB;
                string connectionString = "Data Source = " + localServerString + ";" +
                                          "Initial Catalog = " + localDBString + ";" +
                                          "Integrated Security = True;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string queryString = "INSERT INTO tblItem (HouseCode, ItemName, Description, SupplierName) " +
                                         "VALUES (@HouseCode, @ItemName, @Description, @SupplierName);";
                    using (SqlCommand command = new SqlCommand(queryString, connection))
                    {
                        command.Parameters.AddWithValue("@HouseCode", houseCode);
                        command.Parameters.AddWithValue("@ItemName", itemName);
                        command.Parameters.AddWithValue("@Description", description);
                        command.Parameters.AddWithValue("@SupplierName", supplierName);
                        command.ExecuteNonQuery();
                    }
                }

                // Refresh the listview
                ItemRegistrationForm_Load(sender, e);

                MessageBox.Show("Item registered successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }



        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
