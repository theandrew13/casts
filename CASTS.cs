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
using System.IO;

namespace CASTSvFinal
{
    public partial class CASTS : Form
    {
        private SqlCommand command;
        SqlConnection con, con2, con3;
       // String connectionString = "Data Source=(local)\\CIS411;Initial Catalog=CASTS;User ID=sa;Password=Carl2014";//instance on Carl's Computer
        String connectionString = "Data Source=(local)\\CIS4112;Initial Catalog=CASTS;Integrated Security=true"; //local instance
        string startDate;
        int userID;

        private int access_level;
        public CASTS()
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
              
        }

        public CASTS(string passedIn)
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
            startDate = passedIn;

        }
        public CASTS(string passedIn, int user)
        {
            InitializeComponent();
            con = new SqlConnection(connectionString);
            con.Open();
            startDate = passedIn;
            userID = user;

        }

        private void btnGroups_Click(object sender, EventArgs e)
        {
            //Reloads any new changes before displaying form
            this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
            this.dEPARTMENTSTableAdapter.Fill(this.cASTSDataSet.DEPARTMENTS);

            panelAddGroup.Visible = true;
            panelBillHours.Visible = false;
            panelEmployee.Visible = false;
            panelEvents.Visible = false;
            panelReports.Visible = false;
            pnlWelcomeScreen.Visible = false;
            
           

            this.Refresh();
        }


        private void btnAddGroupSave_Click(object sender, EventArgs e)
        {
            if (txtGroupCode.Modified == true && txtGroupCode.Text != null)
            {
                /**** Department (group) ****/
                try
                {
                    String sql = "INSERT INTO DEPARTMENTS(DEPARTMENT_NAME,DEPARTMENT_DESC) VALUES ('" + txtGroupCode.Text + "','" + txtGroupDesc.Text + "')";
                    SqlCommand command = new SqlCommand(sql, con);
                    //MessageBox.Show(sql);
                    //Executes the insert statement
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully.");
                    txtGroupCode.Clear();
                    txtGroupDesc.Clear();
                    this.dEPARTMENTSTableAdapter1.FillDepartments(this.departmentDataSet.DEPARTMENTS);
                }
                catch (SqlException ex)
                {
                    //Display the error to the user
                    MessageBox.Show("There is an error." + ex);
                }
            }
            else
                MessageBox.Show("There was no text changed.");
        }

        private void btnBill_Click(object sender, EventArgs e)
        {
            //Reloads any new changes before displaying form
            this.qryHoursWorkedWUserTableAdapter.FillHoursWorkedWUser(this.WOHoursDataSet.qryHoursWorkedWUser);
            this.eVENTSTableAdapter.Fill(this.cASTSDataSet.EVENTS);
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);


            this.Refresh();

            panelBillHours.Visible = true;
            panelEmployee.Visible = false;
            panelEvents.Visible = false;
            panelAddGroup.Visible = false;
            panelReports.Visible = false;
            pnlWelcomeScreen.Visible = false;

            
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            //Reloads any new changes before displaying form
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);

            panelEmployee.Visible = true;
            panelBillHours.Visible = false;
            panelEvents.Visible = false;
            panelAddGroup.Visible = false;
            panelReports.Visible = false;
            pnlWelcomeScreen.Visible = false;


            //txtUserID.Text = "251-";

            this.Refresh();
        }


        private void btnEvents_Click(object sender, EventArgs e)
        {
            this.addEventsDataGridView.Refresh();
            //Reloads any new changes before displaying form
            this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
            this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
            this.dEPARTMENTSTableAdapter.Fill(this.cASTSDataSet.DEPARTMENTS);

            panelEvents.Visible = true;
            panelEmployee.Visible = false;
            panelBillHours.Visible = false;
            panelAddGroup.Visible = false;
            panelReports.Visible = false;
            pnlWelcomeScreen.Visible = false;


            //Calculates day number and inserts into event mask
            int dayNumberOfYear = System.DateTime.UtcNow.DayOfYear;
            txtEventMask.Text = dayNumberOfYear.ToString() + "-";

            this.Refresh();
        }

        private void CASTS_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eventsDataSet.AddEvents' table. You can move, or remove it, as needed.
            this.addEventsTableAdapter.FillAddEvents(this.eventsDataSet.AddEvents);
            // TODO: This line of code loads data into the 'buildingsDataSet.BUILDING' table. You can move, or remove it, as needed.
            this.bUILDINGTableAdapter2.Fill(this.buildingsDataSet.BUILDING);
            // TODO: This line of code loads data into the 'buildingDataSet.BUILDING' table. You can move, or remove it, as needed.
            this.bUILDINGTableAdapter.Fill(this.buildingDataSet.BUILDING);
            // TODO: This line of code loads data into the 'departmentDataSet.DEPARTMENTS' table. You can move, or remove it, as needed.
            this.dEPARTMENTSTableAdapter1.FillDepartments(this.departmentDataSet.DEPARTMENTS);
            // TODO: This line of code loads data into the 'eventsDataSet.AddEvents' table. You can move, or remove it, as needed.
            //this.addEventsTableAdapter.FillAddEvents(this.eventsDataSet.AddEvents);
            this.qryHoursWorkedWUserTableAdapter.FillHoursWorkedWUser(this.WOHoursDataSet.qryHoursWorkedWUser);
            // TODO: This line of code loads data into the 'cASTSDataSet.HOURS_WORKED' table. You can move, or remove it, as needed.
            this.hOURS_WORKEDTableAdapter.Fill(this.cASTSDataSet.HOURS_WORKED);
            // TODO: This line of code loads data into the 'cASTSDataSet.BUILDING' table. You can move, or remove it, as needed.
            this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
            // TODO: This line of code loads data into the 'cASTSDataSet.EVENTS' table. You can move, or remove it, as needed.
            this.eVENTSTableAdapter.Fill(this.cASTSDataSet.EVENTS);
            // TODO: This line of code loads data into the 'cASTSDataSet.USERS' table. You can move, or remove it, as needed.
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);
            // TODO: This line of code loads data into the 'cASTSDataSet.DEPARTMENTS' table. You can move, or remove it, as needed.
            this.dEPARTMENTSTableAdapter.Fill(this.cASTSDataSet.DEPARTMENTS);

        }

        private void btnEmployeeSave_Click(object sender, EventArgs e)
        {
            int contractHrs = 0;
            //Error checking on 'Name' field using custom 'ValidateEmployeeName' method
            bool isNameValid = false;
            String EmployeeName = txtName.Text;
            isNameValid = ValidateEmployeeName(EmployeeName);

            if (isNameValid == false)
            {
                MessageBox.Show("Please enter a valid name.");
                txtName.Clear();
                txtName.Focus();
            }

            /**** Employee ****/
            if (Int32.TryParse(txtEmpContractHrs.Text, out contractHrs) && isNameValid == true && !String.IsNullOrEmpty(txtName.Text))
            {
                try
                {
                    string semester;
                    semester = "N/A";
                    if (radFall.Checked == true)
                    {
                        semester = "Fall";
                    }
                    else
                        if (radSpring.Checked == true)
                        {
                            semester = "Spring";
                        }

                    access_level = 1;

                    String sql = "INSERT INTO USERS(NAME,ACCESS_LEVEL,CONTRACT_HOURS,HIRE_DATE,SEMESTER) VALUES ('" + txtName.Text + "','"
                        + access_level + "','" + contractHrs + "','" + dateHireDate.Value + "','" + semester + "')";
                    SqlCommand command = new SqlCommand(sql, con);

                    //Executes the insert statement
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully.");
                    txtName.Text = "";
                    txtEmpContractHrs.Text = "";

                    this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);
                }

                catch (SqlException ex)
                {
                    //Display the error to the user
                    MessageBox.Show("There is an error." + ex);
                }
            }
            else
            {
                MessageBox.Show("You must enter a value for 'Contract Hours'.");
                txtEmpContractHrs.Clear();
                txtEmpContractHrs.Focus();
            }
        }
 

        private void btnSaveBuilding_Click(object sender, EventArgs e)
        {
            if (txtBuildingCode.Modified == true && txtBuildingCode.Text != null)
            {
                /**** Building ****/
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO BUILDING(BUILDING_NAME,BUILDING_DESC) VALUES ('" + txtBuildingCode.Text + "','" + txtBuildingDesc.Text + "')", con);

                    //Executes the insert statement
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully.");
                    txtBuildingCode.Clear();
                    txtBuildingDesc.Clear();

                    //This updates the table after user enters knew info to it
                    this.bUILDINGTableAdapter.Fill(this.buildingDataSet.BUILDING);
                }
                catch (SqlException ex)
                {
                    //Display the error to the user
                    MessageBox.Show("There is an error." + ex);
                }
            }
            else
                MessageBox.Show("There was no text changed.");
               
        }

        private void btnBillSave_Click(object sender, EventArgs e)
        {
            double hoursWorked = 0.0;


            /**** Employee ****/

            if (Double.TryParse(txtTotal.Text, out hoursWorked))
            {
                try
                {
                    String sql = "INSERT INTO HOURS_WORKED(EVENT_ID,USER_ID,START_DATE,START_TIME,END_DATE,END_TIME,TOTAL_HOURS) VALUES ('" + eventID_MaskComboBox.SelectedValue + "','" + stud_IDComboBox.SelectedValue + "','" + dtpStartDate.Value + "','" + dateWOStartTime.Value + "','" + dtpEndDate.Value + "','" + dateWOEndTime.Value + "','" + txtTotal.Text + "')";
                    SqlCommand command = new SqlCommand(sql, con);
                    //MessageBox.Show(sql);
                    //Executes the insert statement
                    command.ExecuteNonQuery();
                    MessageBox.Show("Saved successfully.");
                    txtBuildingCode.Text = "";

                    this.hOURS_WORKEDTableAdapter.Fill(this.cASTSDataSet.HOURS_WORKED);
                }
                catch (SqlException ex)
                {
                    //Display the error to the user
                    MessageBox.Show("There is an error." + ex);
                }
            }
            else
            {
                MessageBox.Show("There was an error with the total hours worked. Please enter a number between 0.25 and 99.75 and try again.");
                txtTotal.Clear();
                txtTotal.Focus();
            }
            this.qryHoursWorkedWUserTableAdapter.FillHoursWorkedWUser(this.WOHoursDataSet.qryHoursWorkedWUser);

        }

        private void btnEventsSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
               try
               {
                   con.Open();

                   String sql = "INSERT INTO EVENTS(EVENT_TITLE,BLDG_ID,DEPT_ID,EVENTID_MASK,COMMENTS,START_DATE,START_TIME) VALUES ('" + txtEventsTitle.Text + "'," + buildingComboBox.SelectedValue + "," + departmentComboBox.SelectedValue + ",'" + txtEventMask.Text + "','" + commentsTextBox.Text + "','" + dateEventsStartDate.Value + "','" + dateEventStartTime.Value + "')";
                   command = new SqlCommand(sql, con);
                  
                   //Executes the insert statement
                   command.ExecuteNonQuery();
                   MessageBox.Show("Event saved successfully.");

                   this.addEventsTableAdapter.FillAddEvents(this.eventsDataSet.AddEvents);
                   
               }
               catch (SqlException ex)
               {
                   //Display the error to the user
                   MessageBox.Show("There is an error Carl, sorry" + ex);
                   //command.ExecuteNonQuery();
                   
               }
               finally
               {
                   con.Close();//Close connection here
               }
        }

        private void btnEventsExit_Click(object sender, EventArgs e)
        {
            // result of dialog must be this type
            DialogResult result;

            // show a message box and get answer   
            result = MessageBox.Show("Are you sure you want to exit?",      // msg
                                      "Exit",                            // caption
                                      MessageBoxButtons.YesNo,           // buttons
                                      MessageBoxIcon.Hand);           // icon

            // exit if YES                             
            if (result == DialogResult.Yes)
                this.Dispose(); 
        }

        private void btnClearFrm_Click(object sender, EventArgs e)//Clear  values from the various controls
        {
            txtEventsTitle.Clear();
            commentsTextBox.Clear();
            buildingComboBox.Text = "";
            departmentComboBox.Text = "";
            txtEventMask.Clear();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            DateTime startTime = Convert.ToDateTime(dateWOStartTime.Value);
            DateTime endTime = Convert.ToDateTime(dateWOEndTime.Value);
            endTime.AddSeconds(1);

            TimeSpan span = endTime.Subtract(startTime);
            Double spanHours = span.Hours;
            Double spanMinutes = span.Minutes / 60.0;
            if (spanMinutes > .9)
            {
                spanMinutes = .00;
                spanHours++;

            }
            else if (spanMinutes < .9 && spanMinutes > .7)
                spanMinutes = .75;
            else if (spanMinutes < .7 && spanMinutes > .4)
                spanMinutes = .5;
            else if (spanMinutes < .4 && spanMinutes > .13)
                spanMinutes = .25;
            else if (spanMinutes < .12)
                spanMinutes = .00;



            Double totalHours = spanHours + spanMinutes;
            //MessageBox.Show(String.Format("{0:0.00}", totalHours));

            //This adds two decimal places
            txtTotal.Text = String.Format("{0:0.00}", totalHours);
        }

        //Method to make sure there is text (no digits & no blanks) in the 'Name' field when adding an employee
        private bool ValidateEmployeeName(String nameIsOk)
        {
            /*bool temp = false;
            if ((nameIsOk.All(char.IsLetter)) && (nameIsOk.All(char.IsSeparator))) 
                temp = true;*/
            bool temp = true;
            return temp;
        }
        
        //Makes changes to Employees
        private void btnEditEmployee_Click(object sender, EventArgs e)
        {
            int i;
            i = uSERSDataGridView.SelectedCells[0].RowIndex;
            txtName.Text = uSERSDataGridView.Rows[i].Cells[1].Value.ToString();
            txtEmpContractHrs.Text = uSERSDataGridView.Rows[i].Cells[2].Value.ToString();
            String date = uSERSDataGridView.Rows[i].Cells[3].Value.ToString();
            DateTime dt = Convert.ToDateTime(date);
            dateHireDate.Value = dt;

            string semester;
            semester = "N/A";
            if (uSERSDataGridView.Rows[i].Cells[4].Value.ToString() == "N/A       ")
            {
                radFall.Checked = false;
                radSpring.Checked = false;
            }
            else if (uSERSDataGridView.Rows[i].Cells[4].Value.ToString() == "Spring    ")
            {
                radSpring.Checked = true;
            }
            else if (uSERSDataGridView.Rows[i].Cells[4].Value.ToString() == "Fall      ")
            {
                radFall.Checked = true;
            }

        }

        //Makes changes to Employees
        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            string semester;
            semester = "N/A";
            if (radFall.Checked == true)
            {
                semester = "Fall";
            }
            else if (radSpring.Checked == true)
            {
                semester = "Spring";
            }
            string sql = "UPDATE USERS SET NAME=@Name,CONTRACT_HOURS=@ContractHours,HIRE_DATE=@HireDate,SEMESTER=@Semester WHERE(USER_ID=@UserID)";
            SqlCommand updateCommand = new SqlCommand(sql,con);
            updateCommand.Parameters.AddWithValue("@UserID", uSER_IDTextBox.Text);
            updateCommand.Parameters.AddWithValue("@Name", txtName.Text);
            updateCommand.Parameters.AddWithValue("@ContractHours", txtEmpContractHrs.Text);
            updateCommand.Parameters.AddWithValue("@HireDate", dateHireDate.Value);
            updateCommand.Parameters.AddWithValue("@Semester", semester);

            updateCommand.ExecuteNonQuery();
            MessageBox.Show("Updated successfully.");
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);

            txtName.Clear();
            txtEmpContractHrs.Clear();
            radSpring.Checked = false;
            radFall.Checked = false;
            dateHireDate.Value = DateTime.Now;
        }

        //Deletes rows from Employees
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            SqlCommand deleteCommand = new SqlCommand(); 
            if (uSERSDataGridView.Rows.Count > 1 && uSERSDataGridView.SelectedRows[0].Index != uSERSDataGridView.Rows.Count - 1)
            {
                deleteCommand.CommandText = "DELETE FROM USERS WHERE USER_ID=" + uSERSDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                deleteCommand.Connection = con;
                deleteCommand.ExecuteNonQuery();
                uSERSDataGridView.Rows.RemoveAt(uSERSDataGridView.SelectedRows[0].Index);
                MessageBox.Show("Deleted Sucessfully.");
            }
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);
        }

        //Deletes rows from WO Hours
        private void btnWODelete_Click(object sender, EventArgs e)
        {
            SqlCommand deleteCommand = new SqlCommand();
            if (qryHoursWorkedWUserDataGridView.Rows.Count > 1 && qryHoursWorkedWUserDataGridView.SelectedRows[0].Index != qryHoursWorkedWUserDataGridView.Rows.Count - 1)
            {
                //int i = qryHoursWorkedWUserDataGridView.SelectedCells[0].RowIndex;
                //lblWOID.Text = i.ToString();
                //deleteCommand.CommandText = "DELETE FROM HOURS_WORKED WHERE EVENT_ID=" + qryHoursWorkedWUserDataGridView.SelectedRows[0].Cells[0].Value + "";
                //deleteCommand.CommandText = "DELETE FROM HOURS_WORKED WHERE EVENT_ID=" + i + "";
                deleteCommand.CommandText = "DELETE FROM HOURS_WORKED WHERE EVENT_ID=" + qryHoursWorkedWUserDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                deleteCommand.Connection = con;
                deleteCommand.ExecuteNonQuery();
                qryHoursWorkedWUserDataGridView.Rows.RemoveAt(qryHoursWorkedWUserDataGridView.SelectedRows[0].Index);
                MessageBox.Show("Deleted Sucessfully.");
            }
            this.qryHoursWorkedWUserTableAdapter.FillHoursWorkedWUser(this.WOHoursDataSet.qryHoursWorkedWUser);
        }

        //Makes changes to WO Hours
        private void btnWOEdit_Click(object sender, EventArgs e)
        {
            int i;
            i = qryHoursWorkedWUserDataGridView.SelectedCells[0].RowIndex;
            //String eventID = qryHoursWorkedWUserDataGridView.Rows[i].Cells[0].Value.ToString();
            ////int eventIDInt = Convert.ToInt16(eventID);
            //eventID_MaskComboBox.SelectedValue = eventID;
            //stud_IDComboBox.SelectedValue = qryHoursWorkedWUserDataGridView.Rows[i].Cells[1].Value.ToString();
            String startDate = qryHoursWorkedWUserDataGridView.Rows[i].Cells[2].Value.ToString();
            DateTime startDT = Convert.ToDateTime(startDate);
            dtpStartDate.Value = startDT;
            String startTime = qryHoursWorkedWUserDataGridView.Rows[i].Cells[3].Value.ToString();
            DateTime st = Convert.ToDateTime(startTime);
            dateWOStartTime.Value = st;
            String endDate = qryHoursWorkedWUserDataGridView.Rows[i].Cells[4].Value.ToString();
            DateTime endDT = Convert.ToDateTime(endDate);
            dtpEndDate.Value = endDT;
            String endTime = qryHoursWorkedWUserDataGridView.Rows[i].Cells[5].Value.ToString();
            DateTime et = Convert.ToDateTime(endTime);
            dateWOEndTime.Value = et;
            txtTotal.Text = qryHoursWorkedWUserDataGridView.Rows[i].Cells[6].Value.ToString();
        }

        //Makes changes to WO Hours
        private void btnWOUpdate_Click(object sender, EventArgs e)
        {
            String sql = "UPDATE HOURS_WORKED SET EVENT_ID=@EventID,USER_ID=@UserID,START_DATE=@StartDate,START_TIME=@StartTime,END_DATE=@EndDate,END_TIME=@EndTime,TOTAL_HOURS=@TotalHours WHERE(EVENT_ID=@EventID)";
            SqlCommand updateCommand = new SqlCommand(sql, con);
            updateCommand.Parameters.AddWithValue("@EventID", eventID_MaskComboBox.SelectedValue);
            updateCommand.Parameters.AddWithValue("@UserID", stud_IDComboBox.SelectedValue);
            updateCommand.Parameters.AddWithValue("@StartDate", dtpStartDate.Value);
            updateCommand.Parameters.AddWithValue("@StartTime", dateWOStartTime.Value);
            updateCommand.Parameters.AddWithValue("@EndDate", dtpEndDate.Value);
            updateCommand.Parameters.AddWithValue("@EndTime", dateWOEndTime.Value);
            updateCommand.Parameters.AddWithValue("@TotalHours", txtTotal.Text);

            updateCommand.ExecuteNonQuery();
            MessageBox.Show("Updated successfully.");
            this.uSERSTableAdapter.Fill(this.cASTSDataSet.USERS);

            this.hOURS_WORKEDTableAdapter.Fill(this.cASTSDataSet.HOURS_WORKED);
        }

        //Makes changes to Groups
        private void btnGroupEdit_Click(object sender, EventArgs e)
        {
            int i;
            i = dEPARTMENTSDataGridView.SelectedCells[0].RowIndex;
            txtGroupCode.Text = dEPARTMENTSDataGridView.Rows[i].Cells[1].Value.ToString();
            txtGroupDesc.Text = dEPARTMENTSDataGridView.Rows[i].Cells[2].Value.ToString();
        }

        //Deletes items from Departments
        private void btnGroupDelete_Click(object sender, EventArgs e)
        {
            SqlCommand deleteCommand = new SqlCommand();
            if (dEPARTMENTSDataGridView.Rows.Count > 1 && dEPARTMENTSDataGridView.SelectedRows[0].Index != dEPARTMENTSDataGridView.Rows.Count - 1)
            {
                deleteCommand.CommandText = "DELETE FROM DEPARTMENTS WHERE DEPT_ID=" + dEPARTMENTSDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                deleteCommand.Connection = con;
                deleteCommand.ExecuteNonQuery();
                dEPARTMENTSDataGridView.Rows.RemoveAt(dEPARTMENTSDataGridView.SelectedRows[0].Index);
                MessageBox.Show("Deleted Sucessfully.");
            }
            this.dEPARTMENTSTableAdapter.Fill(this.cASTSDataSet.DEPARTMENTS);
        }

        //Updates Groups
        private void btnGroupUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE DEPARTMENTS SET DEPARTMENT_NAME=@DeptName,DEPARTMENT_DESC=@DeptDesc WHERE(DEPT_ID=@DeptID)", con);
            updateCommand.Parameters.AddWithValue("@DeptID", dEPT_IDLabel1.Text);
            updateCommand.Parameters.AddWithValue("@DeptName", txtGroupCode.Text);
            updateCommand.Parameters.AddWithValue("@DeptDesc", txtGroupDesc.Text);

            updateCommand.ExecuteNonQuery();
            MessageBox.Show("Updated successfully.");
            this.dEPARTMENTSTableAdapter1.FillDepartments(this.departmentDataSet.DEPARTMENTS);
        }

        //Edits Buildings
        private void btnBuildingEdit_Click(object sender, EventArgs e)
        {
            int i;
            i = bUILDINGDataGridView.SelectedCells[0].RowIndex;
            txtBuildingCode.Text = bUILDINGDataGridView.Rows[i].Cells[1].Value.ToString();
            txtBuildingDesc.Text = bUILDINGDataGridView.Rows[i].Cells[2].Value.ToString();
        }

        //Deletes Buildings
        private void btnBuildingDelete_Click(object sender, EventArgs e)
        {
            SqlCommand deleteCommand = new SqlCommand();
            if (bUILDINGDataGridView.Rows.Count > 1 && bUILDINGDataGridView.SelectedRows[0].Index != bUILDINGDataGridView.Rows.Count - 1)
            {
                deleteCommand.CommandText = "DELETE FROM BUILDING WHERE BLDG_ID=" + bUILDINGDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                deleteCommand.Connection = con;
                deleteCommand.ExecuteNonQuery();
                bUILDINGDataGridView.Rows.RemoveAt(bUILDINGDataGridView.SelectedRows[0].Index);
                MessageBox.Show("Deleted Sucessfully.");
            }
            this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
            
        }

        //Updates Buildings
        private void btnBuildingUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE BUILDING SET BUILDING_NAME=@BldgName,BUILDING_DESC=@BldgDesc WHERE(BLDG_ID=@BldgID)", con);
            updateCommand.Parameters.AddWithValue("@BldgID", bLDG_IDLabel1.Text);
            updateCommand.Parameters.AddWithValue("@BldgName", txtBuildingCode.Text);
            updateCommand.Parameters.AddWithValue("@BldgDesc", txtBuildingDesc.Text);

            updateCommand.ExecuteNonQuery();
            MessageBox.Show("Updated successfully.");
            this.bUILDINGTableAdapter.Fill(this.buildingDataSet.BUILDING);
            
        }

        //Edits Events
        private void btnEventEdit_Click(object sender, EventArgs e)
        {
            int i;
            i = addEventsDataGridView.SelectedCells[0].RowIndex;
            String date = addEventsDataGridView.Rows[i].Cells[3].Value.ToString();
            DateTime dtDate = Convert.ToDateTime(date);
            dateEventsStartDate.Value = dtDate;
            String time = addEventsDataGridView.Rows[i].Cells[4].Value.ToString();
            DateTime dtTime = Convert.ToDateTime(time);
            dateEventStartTime.Value = dtTime;
            txtEventMask.Text = addEventsDataGridView.Rows[i].Cells[1].Value.ToString();
            txtEventsTitle.Text = addEventsDataGridView.Rows[i].Cells[2].Value.ToString();
            commentsTextBox.Text = addEventsDataGridView.Rows[i].Cells[7].Value.ToString();
            buildingComboBox.Text = addEventsDataGridView.Rows[i].Cells[5].Value.ToString();
            departmentComboBox.Text = addEventsDataGridView.Rows[i].Cells[6].Value.ToString();
        }

        //Deletes Events
        private void btnEventDelete_Click(object sender, EventArgs e)
        {
            SqlCommand deleteCommand = new SqlCommand();
            if (addEventsDataGridView.Rows.Count > 1 && addEventsDataGridView.SelectedRows[0].Index != addEventsDataGridView.Rows.Count - 1)
            {
                deleteCommand.CommandText = "DELETE FROM EVENTS WHERE EVENTID=" + addEventsDataGridView.SelectedRows[0].Cells[0].Value.ToString() + "";
                deleteCommand.Connection = con;
                deleteCommand.ExecuteNonQuery();
                addEventsDataGridView.Rows.RemoveAt(addEventsDataGridView.SelectedRows[0].Index);
                MessageBox.Show("Deleted Sucessfully.");
                this.bUILDINGTableAdapter1.Fill(this.cASTSDataSet.BUILDING);
                this.dEPARTMENTSTableAdapter.Fill(this.cASTSDataSet.DEPARTMENTS);
            }
            
        }

        private void btnEventUpdate_Click(object sender, EventArgs e)
        {
            SqlCommand updateCommand = new SqlCommand("UPDATE EVENTS SET START_DATE=@StartDate,START_TIME=@StartTime,EVENTID_MASK=@EventMask,EVENT_TITLE=@Title,COMMENTS=@Comments WHERE(EVENTID=@EventID)", con);
            updateCommand.Parameters.AddWithValue("@EventID", eVENTIDLabel1.Text);
            updateCommand.Parameters.AddWithValue("@StartDate", dateEventsStartDate.Value);
            updateCommand.Parameters.AddWithValue("@StartTime", dateEventStartTime.Value);
            updateCommand.Parameters.AddWithValue("@EventMask", txtEventMask.Text);
            updateCommand.Parameters.AddWithValue("@EventTitle", txtEventsTitle.Text);
            updateCommand.Parameters.AddWithValue("@Comments", commentsTextBox.Text);
            updateCommand.Parameters.AddWithValue("@Title", txtEventsTitle.Text);

            updateCommand.ExecuteNonQuery();
            MessageBox.Show("Updated successfully.");
            this.addEventsTableAdapter.FillAddEvents(this.eventsDataSet.AddEvents);
        }

        private void dateEventsStartDate_ValueChanged(object sender, EventArgs e)
        {
            DateTime tempEvents = dateEventsStartDate.Value;
            int dayNumberOfYear = tempEvents.DayOfYear;
            txtEventMask.Text = dayNumberOfYear.ToString() + "-";
            this.Refresh();
        }


        private void btnReports_Click(object sender, EventArgs e)
        {
            panelAddGroup.Visible = false;
            panelBillHours.Visible = false;
            panelEmployee.Visible = false;
            panelEvents.Visible = false;
            panelReports.Visible = true;
            pnlWelcomeScreen.Visible = false;

        }

        private void btnDeptStats_Click(object sender, EventArgs e)
        {
            Form Departments = new Departments();
            Departments.ShowDialog();
        }

        private void btnEventsByDept_Click(object sender, EventArgs e)
        {
            string search = "SELECT EVENTID,DEPARTMENT_NAME,DEPARTMENTS.DEPT_ID,EVENTID_MASK,EVENT_TITLE,START_DATE FROM EVENTS INNER JOIN DEPARTMENTS ON (EVENTS.DEPT_ID = DEPARTMENTS.DEPT_ID) WHERE (EVENTS.DEPT_ID='" + dEPARTMENT_NAMEComboBox.SelectedValue + "' AND (START_DATE >= '" + dtpReportStart.Value + "' AND START_DATE <= '" + dtpReportEnd.Value + "')) ORDER BY EVENTID_MASK";
            AllEventsByDept AllEventsByDeptForm = new AllEventsByDept();
            AllEventsByDeptForm.MyProperty = search;
            AllEventsByDeptForm.ShowDialog();
            

            //txtResults.Clear();
            //SqlCommand EventsByDeptReport = new SqlCommand();
            DateTime ReportStart = Convert.ToDateTime(dtpReportStart.Value);
            DateTime ReportEnd = Convert.ToDateTime(dtpReportEnd.Value);
            TimeSpan time = ReportEnd - ReportStart;

            //EventsByDeptReport.CommandText ="SELECT EVENTID, EVENTID_MASK, EVENT_TITLE, START_DATE, DEPT_ID FROM EVENTS WHERE (DEPT_ID = " + dEPARTMENT_NAMEComboBox.SelectedValue + " ) AND ( START_DATE BETWEEN " + ReportStart +" AND " + ReportEnd +" ) ORDER BY START_DATE";
            //MessageBox.Show(EventsByDeptReport.CommandText.ToString());
            /*using (var searchCmd = con.CreateCommand())
            {
                searchCmd.CommandText = "SELECT EVENTID,DEPARTMENT_NAME,DEPARTMENTS.DEPT_ID,EVENTID_MASK,EVENT_TITLE,START_DATE FROM EVENTS INNER JOIN DEPARTMENTS ON (EVENTS.DEPT_ID = DEPARTMENTS.DEPT_ID) WHERE (EVENTS.DEPT_ID='" + dEPARTMENT_NAMEComboBox.SelectedValue + "' AND (START_DATE >= '" + dtpReportStart.Value + "' AND START_DATE <= '" + dtpReportEnd.Value + "')) ORDER BY EVENTID_MASK";
                //MessageBox.Show(searchCmd.CommandText);
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    txtResults.AppendText("Event ID\tDept Name            Start Date\t\t    Event Title\r\n");
                    txtResults.AppendText("------------------------------------------------------------------------------------------------------\r\n");
                    while(searchReader.Read())
                    {
                        txtResults.AppendText("  " + searchReader["EVENTID_MASK"].ToString() + "\t   " + searchReader["DEPARTMENT_NAME"].ToString() + "\t     " + searchReader["START_DATE"].ToString() + "\t    " + searchReader["EVENT_TITLE"].ToString()
                             + "\r\n");
                    }
                }
            }*/
            this.Refresh();
            dEPARTMENT_NAMEComboBox.Refresh();
            
            //needs an output
        }

        private void btnEventsByPers_Click(object sender, EventArgs e)
        {
            string search = "SELECT EVENTID_MASK,NAME,HOURS_WORKED.START_DATE,HOURS_WORKED.START_TIME,HOURS_WORKED.END_TIME,TOTAL_HOURS FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE (HOURS_WORKED.USER_ID = " + nAMEComboBox.SelectedValue + "AND (HOURS_WORKED.START_DATE >= '" + dtpReportStart.Value + "' AND HOURS_WORKED.START_DATE <= '" + dtpReportEnd.Value + "')) ORDER BY EVENTID_MASK";
            EventsWorkedByStudent EventsWorkedByStudentForm = new EventsWorkedByStudent();
            EventsWorkedByStudentForm.MyProperty = search;
            EventsWorkedByStudentForm.ShowDialog();

            string nameComboBox = nAMEComboBox.SelectedValue.ToString();
            EventsWorkedByStudentForm.nameComboBox = nameComboBox;

            string dtpReportStartString = dtpReportStart.ToString();
            EventsWorkedByStudentForm.dtpReportStartString = dtpReportStartString;

            string dtpReportEndString = dtpReportEnd.ToString();
            EventsWorkedByStudentForm.dtpReportEndString = dtpReportEndString;


            /*
            txtResults.Clear();
            using (var searchCmd = con.CreateCommand())
            {
                txtResults.AppendText("Event ID \t Name \t\tDate \t            Total Hours\r\n");
                txtResults.AppendText("-----------------------------------------------------------------------------------------------\r\n");
                searchCmd.CommandText = "SELECT EVENTID_MASK,NAME,HOURS_WORKED.START_DATE,HOURS_WORKED.START_TIME,HOURS_WORKED.END_TIME,TOTAL_HOURS FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE (HOURS_WORKED.USER_ID = " + nAMEComboBox.SelectedValue + "AND (HOURS_WORKED.START_DATE >= '" + dtpReportStart.Value + "' AND HOURS_WORKED.START_DATE <= '" + dtpReportEnd.Value + "')) ORDER BY EVENTID_MASK";
                //searchCmd.CommandText = "SELECT EVENTS.EVENTID_MASK FROM HOURS_WORKED INNER JOIN ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) WHERE (USER_ID = " + nAMEComboBox.SelectedValue + "AND (START_DATE >= '" + dtpReportStart.Value + "' AND START_DATE <= '" + dtpReportEnd.Value + "'))";
                //MessageBox.Show(searchCmd.CommandText);
                using (var searchReader = searchCmd.ExecuteReader())
                {
                    while (searchReader.Read())
                    {
                        txtResults.AppendText("  " + searchReader["EVENTID_MASK"].ToString() + "      " + searchReader["NAME"].ToString() + "     " + searchReader["START_DATE"].ToString() + "        " + searchReader["TOTAL_HOURS"].ToString() + "\r\n");
                    }
                }
            }*/
        }

        //Specific student with start and end date
        private void btnPayReport_Click(object sender, EventArgs e)
        {
            DateTime startDT = dtpReportStart.Value;
            startDate = startDT.ToString("MM.dd.yy");
            string endDate = startDT.AddDays(13).ToString("MM.dd.yy");
            string MyProperty = "SELECT HOURS_WORKED.USER_ID,EVENTID_MASK,NAME, TOTAL_HOURS, HOURS_WORKED.START_DATE FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE HOURS_WORKED.START_DATE BETWEEN '" + startDate + "' AND '" + endDate + "' ORDER BY HOURS_WORKED.USER_ID"; /* " + nAMEComboBox.SelectedValue + " ";*/
            savePayroll(startDate);
        }
     
        private void btnBuildingsReport_Click(object sender, EventArgs e)
        {
            Form Buildings = new Buildings();
            Buildings.ShowDialog();
        }

        private void btnIndivStudent_Click(object sender, EventArgs e)
        {
            DateTime startDT = dtpReportStart.Value;
            int user = Convert.ToInt32(nAMEComboBox.SelectedValue);
            //MessageBox.Show(user.ToString());
            startDate = startDT.ToString("MM.dd.yy");
            string endDate = startDT.AddDays(13).ToString("MM.dd.yy");
            string MyProperty = "SELECT HOURS_WORKED.USER_ID,EVENTID_MASK, NAME,TOTAL_HOURS, HOURS_WORKED.START_DATE FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE HOURS_WORKED.START_DATE BETWEEN '" + startDate + "' AND '" + endDate + "' AND HOURS_WORKED.USER_ID = '" + user + "' ORDER BY HOURS_WORKED.USER_ID"; /* " + nAMEComboBox.SelectedValue + " ";*/

            saveSingle(startDate, user);

            //txtResults.Clear();
            //using (var searchCmd = con.CreateCommand())
            //{
            //    txtResults.AppendText("Event ID \t Name \t\tDate \t            Total Hours\r\n");
            //    txtResults.AppendText("-----------------------------------------------------------------------------------------------\r\n");
            //    searchCmd.CommandText = "SELECT EVENTID_MASK,NAME,HOURS_WORKED.START_DATE,HOURS_WORKED.START_TIME,HOURS_WORKED.END_TIME,TOTAL_HOURS FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE (HOURS_WORKED.USER_ID = " + nAMEComboBox.SelectedValue + "AND (HOURS_WORKED.START_DATE >= '" + dtpReportStart.Value + "' AND HOURS_WORKED.START_DATE <= '" + dtpReportEnd.Value + "')) ORDER BY EVENTID_MASK";
            //    //searchCmd.CommandText = "SELECT EVENTS.EVENTID_MASK FROM HOURS_WORKED INNER JOIN ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) WHERE (USER_ID = " + nAMEComboBox.SelectedValue + "AND (START_DATE >= '" + dtpReportStart.Value + "' AND START_DATE <= '" + dtpReportEnd.Value + "'))";
            //    //MessageBox.Show(searchCmd.CommandText);
            //    using (var searchReader = searchCmd.ExecuteReader())
            //    {
            //        while (searchReader.Read())
            //        {
            //            txtResults.AppendText("  " + searchReader["EVENTID_MASK"].ToString() + "      " + searchReader["NAME"].ToString() + "     " + searchReader["START_DATE"].ToString() + "        " + searchReader["TOTAL_HOURS"].ToString() + "\r\n");
            //        }
            //    }
            //}
        }
        public void savePayroll(string passedIn)
        {
            startDate = passedIn;

            DateTime startDT = Convert.ToDateTime(startDate);
            /*string*/ startDate = startDT.ToString("MM.dd.yy");
            string endDate = startDT.AddDays(13).ToString("MM.dd.yy");
      //      MessageBox.Show(startDate + " " + endDate);

            string mon = "Monday";
            string tue = "Tuesday";
            string wed = "Wednesday";
            string thu = "Thursday";
            string fri = "Friday";
            string sat = "Saturday";
            string sun = "Sunday";
            int userid = 0;

            int[] userArray;
            con3 = new SqlConnection(connectionString);
            con3.Open();
            SqlCommand getID = new SqlCommand("select USER_ID from users", con3);
            using (var reader = getID.ExecuteReader())
            {
                var list = new List<int>();
                while (reader.Read())
                    list.Add(reader.GetInt32(0));
                userArray = list.ToArray();
            }
            con3.Close();
            int i = 0;


            var searchDate = con.CreateCommand();
            var SearchHours = con.CreateCommand();

            string Payroll = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\Payroll.txt";
            //Try to write to file
            using (SqlCommand payrollCmd = con.CreateCommand())
            {
                payrollCmd.CommandText = "SELECT HOURS_WORKED.USER_ID,EVENTID_MASK, NAME,TOTAL_HOURS, HOURS_WORKED.START_DATE FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE HOURS_WORKED.START_DATE BETWEEN '" + startDate + "' AND '" + endDate + "' ORDER BY HOURS_WORKED.USER_ID"; /* " + nAMEComboBox.SelectedValue + " ";*/



                using (SqlDataReader payrollReader = payrollCmd.ExecuteReader())
                {
                    using (StreamWriter payrollWriter = new StreamWriter(File.Create(Payroll)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        DataTable schema = payrollReader.GetSchemaTable();
                        List<int> ordinals = new List<int>();

                        foreach (DataRow row in schema.Rows)
                        {
                            outputLine.AppendFormat("{0},", row["ColumnName"]);
                            ordinals.Add((int)row["ColumnOrdinal"]);
                        }

                        foreach (DataRow row in schema.Rows)
                        {
                            while (i < userArray.Length)
                            {
                                userid = userArray[i];

                                //this puts the user name into the individual time sheets.
                                con2 = new SqlConnection(connectionString);
                                con2.Open();
                                SqlCommand searchName = new SqlCommand("SELECT NAME FROM USERS WHERE USER_ID ='" + userid + "'", con2);
                                //MessageBox.Show(searchName.CommandText);
                                string name = Convert.ToString(searchName.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();
                                string tempDate;
                                string hours;
                                string w1s, w1e, w2s, w2e;
                                SqlCommand sumHours;
                                //This line adds the column names
                                payrollWriter.WriteLine("\t\t\t\tClarion University");
                                payrollWriter.WriteLine("\t\tStudent Employment Verification Form (Time Sheet)");
                                payrollWriter.WriteLine();
                                payrollWriter.WriteLine("\t\t\t Student Name: " + name);
                                payrollWriter.WriteLine("\t\tPay Period " + startDate + " to " + endDate);
                                payrollWriter.WriteLine();
                                payrollWriter.WriteLine("\t\tDATE\t\tTIME PERIODS WORKED\t\tHOURS");
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//8
                                tempDate = startDT.AddDays(0).ToString("MM.dd.yy");
                                w1s = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sat + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//10
                                tempDate = startDT.AddDays(1).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sun + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//12
                                tempDate = startDT.AddDays(2).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(mon + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//14
                                tempDate = startDT.AddDays(3).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(tue + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//16
                                tempDate = startDT.AddDays(4).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(wed + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//18
                                tempDate = startDT.AddDays(5).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(thu + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//20
                                tempDate = startDT.AddDays(6).ToString("MM.dd.yy");
                                w1e = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(fri + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//22
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w1s + "' and '" + w1e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
               //                 MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";

                                payrollWriter.WriteLine("\t\t\t\t\t Weekly Subtotal = " + hours);//23


                                //week2
                                payrollWriter.WriteLine();//24
                                payrollWriter.WriteLine("\t\tDATE\t\tTIME PERIODS WORKED\t\tHOURS");//25
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//26
                                tempDate = startDT.AddDays(7).ToString("MM.dd.yy");
                                w2s = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sat + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//28
                                tempDate = startDT.AddDays(8).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sun + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//30
                                tempDate = startDT.AddDays(9).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(mon + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//32
                                tempDate = startDT.AddDays(10).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(tue + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//34
                                tempDate = startDT.AddDays(11).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(wed + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//36
                                tempDate = startDT.AddDays(12).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(thu + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//38
                                tempDate = startDT.AddDays(13).ToString("MM.dd.yy");
                                w2e = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(fri + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//40
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w2s + "' and '" + w2e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                   //             MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine("\t\t\t\t\t Weekly Subtotal = " + hours);//41
                                payrollWriter.WriteLine();//42  
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w1s + "' and '" + w2e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                //MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine("\t\t\t\t Pay Period Subtotal = " + hours);//43
                                payrollWriter.WriteLine();//44
                                payrollWriter.WriteLine("\t I certify the hours indicated above are true and accurate.");//45
                                payrollWriter.WriteLine();//46
                                payrollWriter.WriteLine();//47
                                payrollWriter.WriteLine();//48

                                payrollWriter.WriteLine();//49
                                payrollWriter.WriteLine("------------------------------                ------------------------------"); //50
                                payrollWriter.WriteLine("Signature of             Date                  Signature of Work      Date  ");//51
                                payrollWriter.WriteLine("Student Employee                               Site Supervisor"); //52
                                payrollWriter.WriteLine(); //53
                                payrollWriter.WriteLine();//54
                                payrollWriter.WriteLine();//55 
                                payrollWriter.WriteLine(); //56
                                payrollWriter.WriteLine();//57


                                i++;
                            }
                        }
                        DateTime currentDate = DateTime.Now;
                        payrollWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                    }
                    MessageBox.Show("Report saved at: " + Payroll);
                }

            }
        }

        public void saveSingle(string passedIn, int userIn)
        {
            startDate = passedIn;
            int userid = userIn;

            DateTime startDT = Convert.ToDateTime(startDate);
            startDate = startDT.ToString("MM.dd.yy");
            string endDate = startDT.AddDays(13).ToString("MM.dd.yy");
            //      MessageBox.Show(startDate + " " + endDate);

            string mon = "Monday";
            string tue = "Tuesday";
            string wed = "Wednesday";
            string thu = "Thursday";
            string fri = "Friday";
            string sat = "Saturday";
            string sun = "Sunday";


            var searchDate = con.CreateCommand();
            var SearchHours = con.CreateCommand();

            string Payroll = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\PayrollIndividual.txt";
            //Try to write to file
            using (SqlCommand payrollCmd = con.CreateCommand())
            {
                payrollCmd.CommandText = "SELECT HOURS_WORKED.USER_ID,EVENTID_MASK, NAME,TOTAL_HOURS, HOURS_WORKED.START_DATE FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE HOURS_WORKED.START_DATE BETWEEN '" + startDate + "' AND '" + endDate + "' AND HOURS_WORKED.USER_ID = '" + userid + "' ORDER BY HOURS_WORKED.USER_ID";
                //MessageBox.Show(payrollCmd.CommandText);

                using (SqlDataReader payrollReader = payrollCmd.ExecuteReader())
                {
                    using (StreamWriter payrollWriter = new StreamWriter(File.Create(Payroll)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        {
                                //this puts the user name into the individual time sheets.
                                con2 = new SqlConnection(connectionString);
                                con2.Open();
                                SqlCommand searchName = new SqlCommand("SELECT NAME FROM USERS WHERE USER_ID ='" + userid + "'", con2);
                                //MessageBox.Show(searchName.CommandText);
                                string name = Convert.ToString(searchName.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();
                                string tempDate;
                                string hours;
                                string w1s, w1e, w2s, w2e;
                                SqlCommand sumHours;
                                //This line adds the column names
                                payrollWriter.WriteLine("\t\t\t\tClarion University");
                                payrollWriter.WriteLine("\t\tStudent Employment Verification Form (Time Sheet)");
                                payrollWriter.WriteLine();
                                payrollWriter.WriteLine("\t\t\t Student Name: " + name);
                                payrollWriter.WriteLine("\t\tPay Period " + startDate + " to " + endDate);
                                payrollWriter.WriteLine();
                                payrollWriter.WriteLine("\t\tDATE\t\tTIME PERIODS WORKED\t\tHOURS");
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//8
                                tempDate = startDT.AddDays(0).ToString("MM.dd.yy");
                                w1s = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sat + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//10
                                tempDate = startDT.AddDays(1).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sun + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//12
                                tempDate = startDT.AddDays(2).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(mon + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//14
                                tempDate = startDT.AddDays(3).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(tue + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//16
                                tempDate = startDT.AddDays(4).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(wed + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//18
                                tempDate = startDT.AddDays(5).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(thu + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//20
                                tempDate = startDT.AddDays(6).ToString("MM.dd.yy");
                                w1e = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(fri + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//22
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w1s + "' and '" + w1e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                //                 MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";

                                payrollWriter.WriteLine("\t\t\t\t\t Weekly Subtotal = " + hours);//23


                                //week2
                                payrollWriter.WriteLine();//24
                                payrollWriter.WriteLine("\t\tDATE\t\tTIME PERIODS WORKED\t\tHOURS");//25
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//26
                                tempDate = startDT.AddDays(7).ToString("MM.dd.yy");
                                w2s = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sat + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//28
                                tempDate = startDT.AddDays(8).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(sun + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//30
                                tempDate = startDT.AddDays(9).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(mon + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//32
                                tempDate = startDT.AddDays(10).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(tue + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//34
                                tempDate = startDT.AddDays(11).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(wed + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//36
                                tempDate = startDT.AddDays(12).ToString("MM.dd.yy");
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(thu + "\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//38
                                tempDate = startDT.AddDays(13).ToString("MM.dd.yy");
                                w2e = tempDate;
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date = ' " + tempDate + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine(fri + "\t\t" + tempDate + "\t\t\t\t\t" + hours);
                                payrollWriter.WriteLine("----------------------------------------------------------------------------");//40
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w2s + "' and '" + w2e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                //             MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine("\t\t\t\t\t Weekly Subtotal = " + hours);//41
                                payrollWriter.WriteLine();//42  
                                con2.Open();
                                sumHours = new SqlCommand("select sum(total_hours) from HOURS_WORKED left outer join users on USERS.USER_ID = HOURS_WORKED.USER_ID where start_date between '" + w1s + "' and '" + w2e + "' and HOURS_WORKED.USER_ID = '" + userid + "'", con2);
                                //MessageBox.Show(sumHours.CommandText);
                                hours = Convert.ToString(sumHours.ExecuteScalar());
                                con2.Close();
                                if (hours == "")
                                    hours = "0.00";
                                payrollWriter.WriteLine("\t\t\t\t Pay Period Subtotal = " + hours);//43
                                payrollWriter.WriteLine();//44
                                payrollWriter.WriteLine("\t I certify the hours indicated above are true and accurate.");//45
                                payrollWriter.WriteLine();//46
                                payrollWriter.WriteLine();//47
                                payrollWriter.WriteLine();//48

                                payrollWriter.WriteLine();//49
                                payrollWriter.WriteLine("------------------------------                ------------------------------"); //50
                                payrollWriter.WriteLine("Signature of             Date                  Signature of Work      Date  ");//51
                                payrollWriter.WriteLine("Student Employee                               Site Supervisor"); //52
                                payrollWriter.WriteLine(); //53
                                payrollWriter.WriteLine();//54
                                payrollWriter.WriteLine();//55 
                                payrollWriter.WriteLine(); //56


                            }
                            DateTime currentDate = DateTime.Now;
                            payrollWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                        }
                        MessageBox.Show("Report saved at: " + Payroll);
                    }

            }
        }

        private void btnAdmin_Click(object sender, EventArgs e)
        {
            Password password = new Password();
            password.ShowDialog();

        }

        private void btnUpcomingEvents_Click(object sender, EventArgs e)
        {
            string pass;
            DateTime startDT = dtpReportStart.Value;
            pass = startDT.ToString("MM.dd.yy");
            upcomingEvents(pass);
        }

        public void upcomingEvents(string passedIn)
        {
            startDate = passedIn;

            DateTime startDT = Convert.ToDateTime(startDate);
            /*string*/
            startDate = startDT.ToString("MM.dd.yy");
            string endDate = startDT.AddDays(13).ToString("MM.dd.yy");
            //      MessageBox.Show(startDate + " " + endDate);

            int eventid = 0;

            int[] userArray;
            con3 = new SqlConnection(connectionString);
            con3.Open();
            SqlCommand getEvents = new SqlCommand("select eventid from events where START_DATE between ' " + startDate + "' and '" + endDate + "'", con3);
            using (var reader = getEvents.ExecuteReader())
            {
                var list = new List<int>();
                while (reader.Read())
                    list.Add(reader.GetInt32(0));
                userArray = list.ToArray();
            }
            con3.Close();
            int i = 0;


            var searchDate = con.CreateCommand();
            var SearchHours = con.CreateCommand();

            string Event = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\CASTS\\UpcomingEvents.txt";
            //Try to write to file
            using (SqlCommand payrollCmd = con.CreateCommand())
            {
                payrollCmd.CommandText = "SELECT HOURS_WORKED.USER_ID,EVENTID_MASK, NAME,TOTAL_HOURS, HOURS_WORKED.START_DATE FROM HOURS_WORKED INNER JOIN EVENTS ON (HOURS_WORKED.EVENT_ID = EVENTS.EVENTID) INNER JOIN USERS ON (HOURS_WORKED.USER_ID = USERS.USER_ID) WHERE HOURS_WORKED.START_DATE BETWEEN '" + startDate + "' AND '" + endDate + "' ORDER BY HOURS_WORKED.USER_ID";


                using (SqlDataReader eventReader = payrollCmd.ExecuteReader())
                {
                    using (StreamWriter eventWriter = new StreamWriter(File.Create(Event)))
                    {
                        StringBuilder outputLine = new StringBuilder();

                        DataTable schema = eventReader.GetSchemaTable();
                        List<int> ordinals = new List<int>();

                        foreach (DataRow row in schema.Rows)
                        {
                            outputLine.AppendFormat("{0},", row["ColumnName"]);
                            ordinals.Add((int)row["ColumnOrdinal"]);
                        }

                        foreach (DataRow row in schema.Rows)
                        {
                            while (i < userArray.Length)
                            {
                                eventid = userArray[i];

                                //this puts the user name into the individual time sheets.
                                con2 = new SqlConnection(connectionString);

                                con2.Open();
                                SqlCommand getWO = new SqlCommand("SELECT EVENTID_MASK FROM EVENTS WHERE EVENTID = " + eventid, con2);
                                //MessageBox.Show(searchName.CommandText);
                                string eventIDMask = Convert.ToString(getWO.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();

                                con2.Open();
                                SqlCommand getJob = new SqlCommand("SELECT EVENT_TITLE FROM EVENTS WHERE EVENTID =" + eventid, con2);
                                //MessageBox.Show(searchName.CommandText);
                                string title = Convert.ToString(getJob.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();

                                con2.Open();
                                SqlCommand getBldg = new SqlCommand("SELECT BUILDING_NAME FROM BUILDING INNER JOIN EVENTS ON BUILDING.BLDG_ID = EVENTS.BLDG_ID WHERE EVENTID =" + eventid, con2);
                                //MessageBox.Show(searchName.CommandText);
                                string bldg = Convert.ToString(getBldg.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();

                                con2.Open();
                                SqlCommand getDept = new SqlCommand("SELECT DEPARTMENT_NAME FROM DEPARTMENTS INNER JOIN EVENTS ON DEPARTMENTS.DEPT_ID = EVENTS.DEPT_ID WHERE EVENTID =" + eventid, con2);
                                //MessageBox.Show(searchName.CommandText);
                                string dept = Convert.ToString(getDept.ExecuteScalar());
                                //MessageBox.Show(name);
                                con2.Close();


                                string tempDate;

                                //This line adds the column names
                                con2.Open();
                                SqlCommand getDate = new SqlCommand("SELECT START_DATE FROM EVENTS WHERE EVENTID = " + eventid, con2);
                                string tmp;
                                tmp = Convert.ToString(getDate.ExecuteScalar());
                                DateTime swap = Convert.ToDateTime(tmp);
                                tempDate = swap.ToString("MM.dd.yy");

                                con2.Close();


                                eventWriter.WriteLine("\tWORK ORDER SHEET \t\t\t" + tempDate);
                                eventWriter.WriteLine();
                                eventWriter.WriteLine("\tW.O.#: " + eventIDMask + "\t\t" + tempDate);
                                eventWriter.WriteLine();
                                eventWriter.WriteLine("\tJOB: " + title + " \t\tBLDG: " + bldg + "\t\tDEPT CODE: " + dept);
                                eventWriter.WriteLine();
                                eventWriter.WriteLine("\t\tEMPLOYEE  :  DATE  :  START : STOP  : TOTAL  :       REQUESTOR:");
                                eventWriter.WriteLine("\t\t  NUMBER  :        :   TIME : TIME  : HOURS  :       ");

                                eventWriter.WriteLine("============================================================================");//8
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//10
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//12
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//14
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//16
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//18
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//20
                                eventWriter.WriteLine("\t\t          :        :        :       :        :       ");
                                eventWriter.WriteLine("----------------------------------------------------------------------------");//22
                                eventWriter.WriteLine("\t\t  TOTALS  :        :        :       :        :       ");
                                eventWriter.WriteLine();//24
                                eventWriter.WriteLine();
                                //eventWriter.WriteLine();//24
                                i++;
								//this is to add a extra line to every other item's output, so that they come out 2 to a page
                                int j = i % 2;
                                if (j == 1)
                                    eventWriter.WriteLine();
									

                            }
                        }
                        DateTime currentDate = DateTime.Now;
                        eventWriter.WriteLine("\r\n\r\n\r\n\r\nThis file was last modified on: " + currentDate);
                    }
                    MessageBox.Show("Report saved at: " + Event);
                }

            }
        }

    }



}