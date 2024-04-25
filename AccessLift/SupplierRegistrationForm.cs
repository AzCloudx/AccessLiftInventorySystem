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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AccessLift
{
    public partial class SupplierRegistrationForm : Form
    {
        public SupplierRegistrationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Save Button
            string supplierString = textBox1.Text;
            string addressString = textBox2.Text;
            ServerAccessClass linkObject = new ServerAccessClass();
            string localServerString = linkObject.Server;
            string localDBString = linkObject.DB;
            string connectionString = "Data Source = " + localServerString + ";" + "Initial Catalog = " + localDBString + ";" + "Integrated Security = True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            string queryString = "INSERT INTO tblSupplier VALUES(@SupplierName,@Address);";
            SqlParameter param = new SqlParameter("@SupplierName", textBox1.Text);
            SqlParameter param1 = new SqlParameter("@Address", textBox2.Text);           
            var command = new SqlCommand(queryString, connection);
            command.Parameters.Add(param);
            command.Parameters.Add(param1);          
            command.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Supplier Registered","Registered",MessageBoxButtons.OK,MessageBoxIcon.Information);
            //Reload from database to populate listView1.
            connection.Open();
            listView1.Items.Clear();
            List<Supplier> listSuppliers = new List<Supplier>();
            queryString = "SELECT * FROM tblSupplier ORDER BY SupplierName;";
            command = new SqlCommand(queryString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.SupplierName = reader["SupplierName"].ToString();
                supplier.Address = reader["Address"].ToString();
                listSuppliers.Add(supplier);
                ListViewItem lvi = new ListViewItem(supplier.SupplierName);
                lvi.SubItems.Add(supplier.Address);
                listView1.Items.Add(lvi);
            }
            textBox1.Text = textBox2.Text = string.Empty;
            textBox1.Focus();
        }

        private void SupplierRegistrationForm_Load(object sender, EventArgs e)
        {
            ServerAccessClass linkObject = new ServerAccessClass();
            string localServerString = linkObject.Server;
            string localDBString = linkObject.DB;
            string connectionString = "Data Source = " + localServerString + ";" + "Initial Catalog = " + localDBString + ";" + "Integrated Security = True;";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            listView1.Items.Clear();
            List<Supplier> listSuppliers = new List<Supplier>();
            string queryString = "SELECT * FROM tblSupplier ORDER BY SupplierName;";
            SqlCommand command = new SqlCommand(queryString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Supplier supplier = new Supplier();
                supplier.SupplierName = reader["SupplierName"].ToString();
                supplier.Address = reader["Address"].ToString();               
                listSuppliers.Add(supplier);
                ListViewItem lvi = new ListViewItem(supplier.SupplierName);
                lvi.SubItems.Add(supplier.Address);               
                listView1.Items.Add(lvi);
            }
            textBox1.Text = textBox2.Text = string.Empty;
            textBox1.Focus();
        }
    }
}
