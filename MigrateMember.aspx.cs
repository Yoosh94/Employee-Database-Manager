using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

/// <summary>
/// This page is used to migrate members from the csv provided to our own database.
/// </summary>

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Run the migration of 500 emplopyees
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void migrate(object sender, EventArgs e)
    //{
    //    //First Add the first 500 employee
    //    csv.readEmployee();

    //}

    /// <summary>
    /// This will update the membership details for every employee. Only used when initially migrating the 500 employees
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    //protected void test(object sender, EventArgs e)
    //{
    //    UpdateMembershipClass.updateMembershipClass();
    //}

    /// <summary>
    /// Used to migrate members to our databse from engineering db using ID
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void migrate_employee(object sender, EventArgs e)
    {
        string id = memberShipIDTextBox.Text;
        try
        {
            string[] employeeDeatils = csv.getEmployeebyID(id);
            string selectString = createSelectString(employeeDeatils[2], employeeDeatils[3]);
            bool doesEmployeeExists = SQLAdapter.checkIfRecordExists(selectString);
            //If employee already exists in our database let user know
            if (doesEmployeeExists)
            {
                ((Label)Master.FindControl("validationLablel")).Text = "This user already exists in our Database";
            }
            else
            {

                //Get salary of employee by ID
                string salary = csv.FindSalaryByID(employeeDeatils[0]);
                //Create string to insert
                string sqlInsert = createInsertString(employeeDeatils[2], employeeDeatils[3], employeeDeatils[1], salary, employeeDeatils[4]);
                try
                {
                    SQLAdapter.ChangeDBData(sqlInsert);
                    ((Label)Master.FindControl("validationLablel")).Text = "User successfully added";

                }
                catch (Exception exception)
                {
                    ((Label)Master.FindControl("validationLablel")).Text = exception.Message.ToString();
                }
            }
        }
        catch (ArgumentOutOfRangeException exception)
        {
            ((Label)Master.FindControl("validationLablel")).Text = exception.Message.ToString();
        }
    }

    /// <summary>
    /// Create a SQL select string
    /// </summary>
    /// <param name="fname"></param>
    /// <param name="lname"></param>
    /// <returns></returns>
    private static string createSelectString(string fname, string lname)
    {
        string sqlSelect = @"SELECT TOP 1 " + SQLAdapter.FIRSTNAME + "," + SQLAdapter.LASTNAME + "," + SQLAdapter.DOB + " FROM " + SQLAdapter.TABLENAME + " WHERE " + "" + SQLAdapter.FIRSTNAME + "='" + fname + "' AND " + SQLAdapter.LASTNAME + "='" + lname + "'";
        return sqlSelect;
    }


    /// <summary>
    /// Create a SQL Insert string
    /// </summary>
    /// <param name="fname">First name</param>
    /// <param name="lname">Last Name</param>
    /// <param name="dob">Date of Birth</param>
    /// <param name="salary">Salary</param>
    /// <param name="gender">Gender</param>
    /// <returns>A string which can be used as a SQL functin</returns>
    private string createInsertString(string fname, string lname, string dob, string salary, string gender)
    {
        string now = DateTime.Now.ToShortDateString();
        string sqlInsert = @"INSERT INTO " + SQLAdapter.TABLENAME + "(" + SQLAdapter.FIRSTNAME + ", " + SQLAdapter.LASTNAME + ", " + SQLAdapter.DATEJOINED + ", " + SQLAdapter.DOB + ", " + SQLAdapter.SALARY + ", " + SQLAdapter.GENDER + ") VALUES('" + fname + "', '" + lname + "','" + now + "', EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + dob + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + salary + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + gender + "'))";
        return sqlInsert;
    }


}