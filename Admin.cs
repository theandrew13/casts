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
    public partial class Admin : Form
    {
        private SqlCommand command;
        SqlConnection con;
        //String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014";
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true";

        public Admin()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
        }


        private void btnNewSemester(object sender, EventArgs e)
        {
            //con.Open();

            String sqlHoursWorked = "INSERT INTO HOURS_WORKED_ARCHIVE (WORK_HR_ID,EVENT_ID,USER_ID,START_DATE,START_TIME,END_DATE,END_TIME,TOTAL_HOURS) SELECT * FROM HOURS_WORKED";
            SqlCommand commandHoursWorked = new SqlCommand(sqlHoursWorked, con);

            String sqlEvents = "INSERT INTO EVENTS_ARCHIVE (EVENTID,EVENTID_MASK,EVENT_TITLE,START_DATE,START_TIME,END_DATE,END_TIME,BLDG_ID,DEPT_ID,COMMENTS) SELECT * FROM EVENTS";
            SqlCommand commandEvents = new SqlCommand(sqlEvents, con);

            String sqlUsers = "INSERT INTO USERS_ARCHIVE (USER_ID,NAME,ACCESS_LEVEL,CONTRACT_HOURS,HIRE_DATE,SEMESTER) SELECT * FROM USERS";
            SqlCommand commandUsers = new SqlCommand(sqlUsers, con);

            String sqlHoursWorkedDelete = "DELETE FROM HOURS_WORKED";
            SqlCommand commandHoursWorkedDelete = new SqlCommand(sqlHoursWorkedDelete, con);

            String sqlEventsDelete = "DELETE FROM EVENTS";
            SqlCommand commandEventsDelete = new SqlCommand(sqlEventsDelete, con);

            String sqlUsersDelete = "DELETE FROM USERS";
            SqlCommand commandUsersDelete = new SqlCommand(sqlUsersDelete, con);

            commandHoursWorked.ExecuteNonQuery();
            commandEvents.ExecuteNonQuery();
            commandUsers.ExecuteNonQuery();
            commandHoursWorkedDelete.ExecuteNonQuery();
            commandEventsDelete.ExecuteNonQuery();
            commandUsersDelete.ExecuteNonQuery();

            MessageBox.Show("Archive complete. Please reboot program to continue.");
        }
    }
}
