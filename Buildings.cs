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
    public partial class Buildings : Form
    {
        private SqlCommand command;
        SqlConnection con;
       // String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014";
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true";

        public Buildings()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void Buildings_Load(object sender, EventArgs e)
        {
            richBuildingsForm.Clear();

            using(var searchCmd = con.CreateCommand())
            {
                searchCmd.CommandText = "SELECT BUILDING_NAME,BUILDING_DESC FROM BUILDING ORDER BY BUILDING_NAME";
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    richBuildingsForm.AppendText("Code\t\tBuilding Name\r\n");
                    richBuildingsForm.AppendText("------------------------------------------\r\n");
                    while (searchReader.Read())
                    {
                        richBuildingsForm.AppendText(searchReader["BUILDING_NAME"].ToString() + "\t\t" + searchReader["BUILDING_DESC"].ToString() + "\r\n");
                    }
                }
            }

        }

        private void btnBuildingSave_Click(object sender, EventArgs e)
        {
            string Building = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\Buildings.txt";
            //Try to write to file
            using (SqlCommand buildingCmd = con.CreateCommand())
            {
                buildingCmd.CommandText = "SELECT BUILDING_NAME,BUILDING_DESC FROM BUILDING ORDER BY BUILDING_NAME";
                using (SqlDataReader buildingReader = buildingCmd.ExecuteReader())
                {
                    using (StreamWriter buildingWriter = new StreamWriter(File.Create(Building)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        DataTable schema = buildingReader.GetSchemaTable();
                        List<int> ordinals = new List<int>();

                        foreach (DataRow row in schema.Rows)
                        {
                            outputLine.AppendFormat("{0},", row["ColumnName"]);
                            ordinals.Add((int)row["ColumnOrdinal"]);
                        }

                        //This line adds the column names
                        buildingWriter.WriteLine("\tBUILDINGS");
                        buildingWriter.WriteLine();
                        buildingWriter.WriteLine("CODE\t   DESCRIPTION");
                        buildingWriter.WriteLine("------------------------");

                        while (buildingReader.Read())
                        {
                            outputLine.Clear();
                            foreach (int ordinal in ordinals)
                            {
                                outputLine.AppendFormat("{0}\t   ", buildingReader[ordinal]);
                            }
                            buildingWriter.WriteLine(outputLine.ToString());
                        }
                        DateTime currentDate = DateTime.Now;
                        buildingWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                    }
                    MessageBox.Show("Report saved at: " + Building);
                }
            }
        }
    }
}
