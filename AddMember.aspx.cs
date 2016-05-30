using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using Devart.Common;
using System.Data.SqlClient;


/// <summary>
/// For Adding a person not already in the provided in the employee database
/// </summary>
public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// This event function runs when the add user button is pressed on the UI screen. It collects the data the user has inputted into the fields
    /// and then creates an SQL statement from that data. It then checks if the user already exitst and then runs the SQL function to 
    /// add it to the database.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void addMemberSubmitButton_Click(object sender, EventArgs e)
    {

        //Get all employee details
        string firstName = firstNameTextBox.Text;
        string lastName = lastNameTextBox.Text;
        string DOB = dateOfBirthTextBox.Text;
        string salary = salaryTextBox.Text;
        string gender = genderDDL.Text;

        //Create the appropriate SQL statements
        string sqlInsert = createInsertString(firstName, lastName, DOB, salary, gender);
        string selectString = createSelectString(firstName, lastName);
        bool doesEmployeeExists = SQLAdapter.checkIfRecordExists(selectString);
        //If employee already exists in our database let user know
        if (doesEmployeeExists)
        {
            ((Label)Master.FindControl("validationLablel")).Text = "This user already exists in our Database";
        }
        else
        {
            try
            {
                //Try insert data into the database and provide a message to let the user know
                SQLAdapter.ChangeDBData(sqlInsert);
                ((Label)Master.FindControl("validationLablel")).Text = "Member added successfully";
                UpdateMembershipClass.updateMembershipClass();
                firstNameTextBox.Text = "";
                lastNameTextBox.Text = "";
                dateOfBirthTextBox.Text = "";
                salaryTextBox.Text = "";
                genderDDL.Text = "";
            }
            catch (SqlException ex)
            {
                ((Label)Master.FindControl("validationLablel")).Text = ex.Errors.ToString();
            }
            catch (Exception)
            {
                ((Label)Master.FindControl("validationLablel")).Text = "An Unexpected error has occured adding a member";
            }
            finally
            {

            }
        }

    }

    /// <summary>
    /// Create a SQL insert string from the parameters passed into the function
    /// </summary>
    /// <param name="fname">First name</param>
    /// <param name="lname">Last name</param>
    /// <param name="dob">Date of Birth</param>
    /// <param name="salary">Salary</param>
    /// <param name="gender">Gender</param>
    /// <returns></returns>
    private string createInsertString(string fname, string lname, string dob, string salary, string gender)
    {
        string now = DateTime.Now.ToString("yyyy-MM-dd");
        string sqlInsert = @"INSERT INTO " + SQLAdapter.TABLENAME + "(" + SQLAdapter.FIRSTNAME + ", " + SQLAdapter.LASTNAME + ", " + SQLAdapter.DATEJOINED + ", " + SQLAdapter.DOB + ", " + SQLAdapter.SALARY + ", " + SQLAdapter.GENDER + ") VALUES('" + fname + "', '" + lname + "','" + now + "', EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + dob + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + salary + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + gender + "'))";
        return sqlInsert;
    }
    /// <summary>
    /// Create a SQL insert string only based on first name and last name. Overload funtion
    /// </summary>
    /// <param name="fname">first name</param>
    /// <param name="lname">Last name</param>
    /// <returns>SQL string</returns>
    private static string createSelectString(string fname, string lname)
    {
        string sqlSelect = @"SELECT TOP 1 " + SQLAdapter.FIRSTNAME + "," + SQLAdapter.LASTNAME + "," + SQLAdapter.DOB + " FROM " + SQLAdapter.TABLENAME + " WHERE " + "" + SQLAdapter.FIRSTNAME + "='" + fname + "' AND " + SQLAdapter.LASTNAME + "='" + lname + "'";
        return sqlSelect;
    }
}