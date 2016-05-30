using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

/// <summary>
/// This static class will be used to read the CSV files and process them
/// </summary>
public static class csv
{
    private const int ID = 0;
    private const int DOB = 1;
    private const int Fname = 2;
    private const int Lname = 3;
    private const int Gender = 4;
    private const int DateJoined = 5;

    /// <summary>
    /// Will migrate 500 employees to our database.
    /// </summary>
    public static void readEmployee()
    {
        //Read all the lines from the salaries file
        string[] allLinesSalaries = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/salaries.csv"));
        //Split the data and save it to a variable
        var salaries = from line in allLinesSalaries
                       let data = line.Split('|')
                       select data;
        //Read all the values from the employees file
        string[] allLinesEmployee = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/employees.csv"));
        //Split that data and save it in a variable
        var employees = from line in allLinesEmployee
                        let data = line.Split('|')
                        select data;

        //For the first 500 members in the employees file get the trimmed data and insert it into the database
        for (int i = 1; i <= 500; i++)
        {
            char[] charToTrim = { '\\', '\"' };
            string[] emp = employees.ElementAt(i);
            string id = emp[ID];
            string dob = emp[DOB].Trim(charToTrim);
            string firstName = emp[Fname].Trim(charToTrim);
            string lastName = emp[Lname].Trim(charToTrim);
            string gender = emp[Gender].Trim(charToTrim);
            if (gender == "M")
            {
                gender = "Male";
            }
            if (gender == "F")
            {
                gender = "Female";
            }
            string dateJoined = emp[DateJoined].Trim(charToTrim);
            string salary = getSalaryForID(id, allLinesSalaries);
            string sqlInsert = createInsertString(firstName, lastName, dob, salary, gender, dateJoined);
            SQLAdapter.ChangeDBData(sqlInsert);
        }
    }

    /// <summary>
    /// Find the salary of an employee by their ID number
    /// </summary>
    /// <param name="id">ID number of employee</param>
    /// <returns>Salary as a string</returns>
    public static string FindSalaryByID(string id)
    {
        //read all salaries and split the data
        string[] allLinesSalaries = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/salaries.csv"));
        var salaries = from line in allLinesSalaries
                       let data = line.Split('|')
                       select data;

        return getSalaryForID(id, allLinesSalaries);
    }

    /// <summary>
    /// Returns the salary
    /// </summary>
    /// <param name="id">ID of employee</param>
    /// <param name="allSalaries">array of all the salaries</param>
    /// <returns></returns>
    private static string getSalaryForID(string id, string[] allSalaries)
    {
        //Trim the data by removing all the unneccassary data.
        var result = from everyLine in allSalaries
                     let data = everyLine.Remove(10, everyLine.Length - 10)
                     where data.Contains(id)
                     select everyLine;
        //Get the last result as it will be the most upto date
        string lastSalary = result.Last();
        string[] res = lastSalary.Split('|');
        string salary = res[1].Trim('\\', '\"');
        return salary;
    }

    /// <summary>
    /// Finds the employee information by their ID
    /// </summary>
    /// <param name="id">ID of User</param>
    /// <returns></returns>
    public static string[] getEmployeebyID(string id)
    {
        char[] charToTrim = { '\\', '\"' };
        //Read all th data
        string[] allLinesEmployee = File.ReadAllLines(HttpContext.Current.Server.MapPath("~/App_Data/employees.csv"));
        //This will get the employee information.
        var employees = from line in allLinesEmployee
                        let data = line.Split('|')
                        where data[0].Trim(charToTrim) == id
                        select data;
        //If there is only one record found return that data
        if (employees.Count() == 1)
        {
            string[] employee = trimData(employees.First(), charToTrim);
            return employee;
        }
        //If more is found throw an error
        else
        {
            throw new System.ArgumentOutOfRangeException("More than one record found with this ID");
        }
    }

    /// <summary>
    /// This will create a string which will be used as an SQL statement.
    /// </summary>
    /// <param name="fname">First name</param>
    /// <param name="lname">Last name</param>
    /// <param name="dob">date of birth</param>
    /// <param name="salary">Salary</param>
    /// <param name="gender">Gender</param>
    /// <returns></returns>
    private static string createInsertString(string fname, string lname, string dob, string salary, string gender)
    {
        string now = DateTime.Now.ToShortDateString();
        string sqlInsert = @"INSERT INTO " + SQLAdapter.TABLENAME + "(" + SQLAdapter.FIRSTNAME + ", " + SQLAdapter.LASTNAME + ", " + SQLAdapter.DATEJOINED + ", " + SQLAdapter.DOB + ", " + SQLAdapter.SALARY + ", " + SQLAdapter.GENDER + ") VALUES('" + fname + "', '" + lname + "','" + now + "', EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + dob + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + salary + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + gender + "'))";
        return sqlInsert;
    }

    /// <summary>
    /// overload method with datejoined, used when migrating the 500 employees
    /// </summary>
    /// <param name="fname"></param>
    /// <param name="lname"></param>
    /// <param name="dob"></param>
    /// <param name="salary"></param>
    /// <param name="gender"></param>
    /// <param name="dateJoined"></param>
    /// <returns></returns>
    private static string createInsertString(string fname, string lname, string dob, string salary, string gender, string dateJoined)
    {
        string now = DateTime.Now.ToShortDateString();
        string sqlInsert = @"INSERT INTO " + SQLAdapter.TABLENAME + "(" + SQLAdapter.FIRSTNAME + ", " + SQLAdapter.LASTNAME + ", " + SQLAdapter.DATEJOINED + ", " + SQLAdapter.DOB + ", " + SQLAdapter.SALARY + ", " + SQLAdapter.GENDER + ") VALUES('" + fname + "', '" + lname + "','" + dateJoined + "', EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + dob + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + salary + "'), EncryptByPassPhrase ('" + SQLAdapter.PASSWORD + "','" + gender + "'))";
        return sqlInsert;
    }

    /// <summary>
    /// This will trim a string of data from the charToTrim parameter provided.
    /// </summary>
    /// <param name="employeeDetails">string of employee details</param>
    /// <param name="charToTrim">array of characters to trim from the stringt</param>
    /// <returns></returns>
    private static string[] trimData(string[] employeeDetails, char[] charToTrim)
    {
        string[] trimmedData = new string[6];
        trimmedData[ID] = employeeDetails[ID].Trim(charToTrim);
        trimmedData[DOB] = employeeDetails[DOB].Trim(charToTrim);
        trimmedData[Fname] = employeeDetails[Fname].Trim(charToTrim);
        trimmedData[Lname] = employeeDetails[Lname].Trim(charToTrim);
        trimmedData[Gender] = employeeDetails[Gender].Trim(charToTrim);
        trimmedData[DateJoined] = employeeDetails[DateJoined].Trim(charToTrim);
        return trimmedData;
    }
}
