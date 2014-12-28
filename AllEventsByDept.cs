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
    public partial class AllEventsByDept : Form
    {
        public string MyProperty { get; set; }
        private SqlCommand command;
        SqlConnection con;
        //String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014";
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true";

        public AllEventsByDept()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void AllEventsByDept_Load(object sender, EventArgs e)
        {
            richAllEventsByDeptForm.Clear();

            using (var searchCmd = con.CreateCommand())
            {
                searchCmd.CommandText = MyProperty;
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    richAllEventsByDeptForm.AppendText("Event ID\tDept Name            Start Date\t\t       Event Title\r\n");
                    richAllEventsByDeptForm.AppendText("-------------------------------------------------------------------------------\r\n");
                    while (searchReader.Read())
                    {
                        richAllEventsByDeptForm.AppendText("  " + searchReader["EVENTID_MASK"].ToString() + "\t   " + searchReader["DEPARTMENT_NAME"].ToString() + "\t     " + searchReader["START_DATE"].ToString() + "\t    " + searchReader["EVENT_TITLE"].ToString()
                             + "\r\n");
                    }
                }
            }
        }

        private void btnAllEventsByDeptSave_Click(object sender, EventArgs e)
        {
            string AllEventsByDept = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\AllEventsByDept.txt";
            //Try to write to file
            using (SqlCommand AllEventsByDeptCmd = con.CreateCommand())
            {
                AllEventsByDeptCmd.CommandText = MyProperty;
                using (SqlDataReader AllEventsByDeptReader = AllEventsByDeptCmd.ExecuteReader())
                {
                    using (StreamWriter departmentWriter = new StreamWriter(File.Create(AllEventsByDept)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        DataTable schema = AllEventsByDeptReader.GetSchemaTable();
                        List<int> ordinals = new List<int>();

                        foreach (DataRow row in schema.Rows)
                        {
                            outputLine.AppendFormat("{0},", row["ColumnName"]);
                            ordinals.Add((int)row["ColumnOrdinal"]);
                        }

                        //This line adds the column names
                        departmentWriter.WriteLine("\tEvent ID");
                        departmentWriter.WriteLine();
                        departmentWriter.WriteLine("CODE\t   DESCRIPTION");
                        departmentWriter.WriteLine("------------------------");

                        while (AllEventsByDeptReader.Read())
                        {
                            outputLine.Clear();
                            foreach (int ordinal in ordinals)
                            {
                                outputLine.AppendFormat("{0}\t   ", AllEventsByDeptReader[ordinal]);
                            }
                            departmentWriter.WriteLine(outputLine.ToString());
                        }
                        DateTime currentDate = DateTime.Now;
                        departmentWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                    }
                }
                MessageBox.Show("Report saved at: " + AllEventsByDept);
            }
        }
    }
}
