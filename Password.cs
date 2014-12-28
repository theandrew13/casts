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

namespace CASTSvFinal
{
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
            txtPassword.Focus();
        }

        private void btnSubmitPassword_Click(object sender, EventArgs e)
        {
            string password = "Carl2014";
            
            if(txtPassword.Text == password)
            {
                Admin adminForm = new Admin();
                adminForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please type in the correct password.");
                txtPassword.Clear();
            }
        }

        private void Password_Load(object sender, EventArgs e)
        {
            txtPassword.Focus();
        }
    }
}
