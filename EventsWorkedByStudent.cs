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
    public partial class EventsWorkedByStudent : Form
    {
        private SqlCommand command;
        SqlConnection con;
        //String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014"; //clients connection
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true"; //my machine's connection

        public string MyProperty { get; set; }
        public string nameComboBox { get; set; }
        public string dtpReportStartString { get; set; }
        public string dtpReportEndString { get; set; }


        public EventsWorkedByStudent()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
        }

        private void EventsWorkedByStudent_Load(object sender, EventArgs e)
        {
            richEventsWorkedByStudentForm.Clear();

            using (var searchCmd = con.CreateCommand())
            {
                //MessageBox.Show(MyProperty); //for debugging
                richEventsWorkedByStudentForm.AppendText("Event ID \t Name \t\t      Date \t               Total Hours\r\n");
                richEventsWorkedByStudentForm.AppendText("-------------------------------------------------------------------------------\r\n");
                searchCmd.CommandText = MyProperty;
                //searchCmd.CommandText = "SELECT EVENTS.EVENTID_MASK FROM HOURS_WORKED INNER JOIN ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) WHERE (USER_ID = " + nAMEComboBox.SelectedValue + "AND (START_DATE >= '" + dtpReportStart.Value + "' AND START_DATE <= '" + dtpReportEnd.Value + "'))";
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    while (searchReader.Read())
                    {
                        richEventsWorkedByStudentForm.AppendText(searchReader["EVENTID_MASK"].ToString() + "      " + searchReader["NAME"].ToString() + "     " + searchReader["START_DATE"].ToString() + "        " + searchReader["TOTAL_HOURS"].ToString() + "\r\n");
                    }
                }
            }
        }
    }
}
