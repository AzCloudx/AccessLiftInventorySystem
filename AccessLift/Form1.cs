using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccessLift
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void newSupplierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupplierRegistrationForm aForm = new SupplierRegistrationForm();
            aForm.Show();
        }

        private void newItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ItemRegistrationForm aForm = new ItemRegistrationForm();
            aForm.Show();

        }

        private void stockInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockInTransactionForm aForm = new StockInTransactionForm();
            aForm.Show();
        }

        private void stockOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StockOutTransactionForm aForm = new StockOutTransactionForm();
            aForm.Show();
        }
    }
}
