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
    public partial class Departments : Form
    {
        private SqlCommand command;
        SqlConnection con;
       // String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014"; //client
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true"; //mine

        public Departments()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void Departments_Load(object sender, EventArgs e)
        {
            richDepartmentsForm.Clear();

            using (var searchCmd = con.CreateCommand())
            {
                searchCmd.CommandText = "SELECT DEPARTMENT_NAME,DEPARTMENT_DESC FROM DEPARTMENTS ORDER BY DEPARTMENT_NAME";
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    richDepartmentsForm.AppendText("Code\t\tDepartment Name\r\n");
                    richDepartmentsForm.AppendText("----------------------------------------\r\n");
                    while (searchReader.Read())
                    {
                        richDepartmentsForm.AppendText(searchReader["DEPARTMENT_NAME"].ToString() + "\t\t" + searchReader["DEPARTMENT_DESC"].ToString() + "\r\n");
                    }
                }
            }
        }

        private void btnDepartmentsSave_Click(object sender, EventArgs e)
        {
            string Department = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\Departments.txt";
            //Try to write to file
            using (SqlCommand departmentCmd = con.CreateCommand())
            {
                departmentCmd.CommandText = "SELECT DEPARTMENT_NAME,DEPARTMENT_DESC FROM DEPARTMENTS ORDER BY DEPARTMENT_NAME";
                using (SqlDataReader departmentReader = departmentCmd.ExecuteReader())
                {
                    using (StreamWriter departmentWriter = new StreamWriter(File.Create(Department)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        DataTable schema = departmentReader.GetSchemaTable();
                        List<int> ordinals = new List<int>();

                        foreach (DataRow row in schema.Rows)
                        {
                            outputLine.AppendFormat("{0},", row["ColumnName"]);
                            ordinals.Add((int)row["ColumnOrdinal"]);
                        }

                        //This line adds the column names
                        departmentWriter.WriteLine("\tDEPARTMENTS");
                        departmentWriter.WriteLine();
                        departmentWriter.WriteLine("CODE\t   DESCRIPTION");
                        departmentWriter.WriteLine("------------------------");

                        while (departmentReader.Read())
                        {
                            outputLine.Clear();
                            foreach (int ordinal in ordinals)
                            {
                                outputLine.AppendFormat("{0}\t   ", departmentReader[ordinal]);
                            }
                            departmentWriter.WriteLine(outputLine.ToString());
                        }
                        DateTime currentDate = DateTime.Now;
                        departmentWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                    }
                }
                MessageBox.Show("Report saved at: " + Department);
            }
        }
    }
}
