using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace CASTSvFinal
{
    public partial class PayPeriod : Form
    {
        public string MyProperty { get; set; }

        private SqlCommand command;
        SqlConnection con;
       // String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014"; //Client's MS SQL instance
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true"; //my instance
        string startDate;


        public PayPeriod(string passedIN)
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
            startDate = passedIN;
        }

        private void btnPayPeriodSave_Click(object sender, EventArgs e)
        {
            CASTS saveReport = new CASTS(startDate);
            saveReport.savePayroll(startDate);
        }

        private void PayPeriod_Load(object sender, EventArgs e)
        {
            richPayPeriodForm.Clear();

            using (var searchCmd = con.CreateCommand())
            {
                richPayPeriodForm.AppendText("User Name\tEvent ID\tHours\t\tDate\r\n");
                richPayPeriodForm.AppendText("--------------------------------------\r\n");
                searchCmd.CommandText = MyProperty;
                //MessageBox.Show(MyProperty); //used for debugging
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    while (searchReader.Read())
                    {
                        //MessageBox.Show(MyProperty); //used for debugging
                        richPayPeriodForm.AppendText(searchReader["NAME"].ToString() + "\t" + searchReader["EVENTID_MASK"].ToString() + "\t\t" + searchReader["TOTAL_HOURS"].ToString() + "\t\t" + searchReader["START_DATE"].ToString() + "\t" + "\r\n");
                    }
                }
            }
        }
    }
}
