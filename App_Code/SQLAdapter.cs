using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Static class which will process all the sql function.
/// </summary>
public static class SQLAdapter
{
    #region declare constants
    //Declare constants
    public const string ID = "Id";
    public const string FIRSTNAME = "FirstName";
    public const string LASTNAME = "LastName";
    public const string MEMBERSHIPCLASS = "MemberShipClass";
    public const string DATEJOINED = "DateJoined";
    public const string DOB = "DOB";
    public const string SALARY = "Salary";
    public const string GENDER = "Gender";
    public const string TABLENAME = "UserAccount";
    public static string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Database.mdf;Integrated Security = True;MultipleActiveResultSets=true";
    public const string PASSWORD = "P@SSW04D";
    private static SqlConnection connection;
    private static SqlDataReader rdr;
    #endregion

    /// <summary>
    /// Will open a connection to the database and run a SQL command which is not a query Type.
    /// </summary>
    /// <param name="SQLInsertLine">string which has been structred to pass to the Database</param>
    public static void ChangeDBData(string SQLInsertLine)
    {
        connection = OpenConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(SQLInsertLine, connection);
        cmd.ExecuteNonQuery();
        CloseConnection(ref connection);
    }

    /// <summary>
    /// THis function will run and update the membership status of all the employees
    /// </summary>
    /// <param name="SQLSELECTString">SQL select statement</param>
    /// <param name="SQLUPDATEString">SQL update statement</param>
    public static void updateMembership(string SQLSELECTString, string SQLUPDATEString)
    {
        connection = OpenConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(SQLSELECTString, connection);
        rdr = cmd.ExecuteReader();
        SqlCommand cmd2 = new SqlCommand(SQLUPDATEString, connection);
        while (rdr.Read())
        {

            //This will give me the employee ID
            string id = rdr[0].ToString();
            SQLUPDATEString = SQLUPDATEString.Replace("_::_", id);
            cmd2.CommandText = SQLUPDATEString;
            SQLUPDATEString = SQLUPDATEString.Replace(id, "_::_");
            cmd2.ExecuteNonQuery();

        }
        CloseConnection(ref connection);
        rdr.Close();
    }

    /// <summary>
    /// This will check if a record already exists in the database depending on the Select statement given.
    /// </summary>
    /// <param name="SQLSelectString">SQL Select statement</param>
    /// <returns></returns>
    public static bool checkIfRecordExists(string SQLSelectString)
    {
        connection = OpenConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(SQLSelectString, connection);
        rdr = cmd.ExecuteReader();
        if (rdr.HasRows)
        {
            rdr.Close();
            return true;
        }
        //If there are not rows
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Return the number of rows that match the statement
    /// </summary>
    /// <param name="query">number of rows that match the statement as a string</param>
    /// <returns></returns>
    public static string getCount(string query)
    {
        string count = "0";
        connection = OpenConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(query, connection);
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            count = rdr[0].ToString();           
        }
        CloseConnection(ref connection);
        return count;
        
    }

    /// <summary>
    /// Find the employees that have higher than average wages and return a string of all of them
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public static string getEmployeesThatHaveHigherthanAverageSalary(string query)
    {
        string s = "";
        connection = OpenConnection(_connectionString);
        SqlCommand cmd = new SqlCommand(query, connection);
        rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {
            for(int i = 0; i < 4; i++)
            {
                s = s + rdr[i].ToString();
                if(i == 3)
                {
                    s = s + "\r\n";
                }
                else
                {
                    s = s + ",";
                }
                
            }
        }
        CloseConnection(ref connection);
        return s;
    }

    

    /// <summary>
    /// Open a connection to a database
    /// </summary>
    /// <param name="c">Name of the SqlConnection</param>
    /// <param name="connectionString">Connection String</param>
    private static SqlConnection OpenConnection(string connectionString)
    {
        SqlConnection conn = new SqlConnection(connectionString);
        conn.Open();
        return conn;
    }

    /// <summary>
    /// Close connection of the database
    /// </summary>
    /// <param name="c">SqlConnection</param>
    private static void CloseConnection(ref SqlConnection c)
    {
        c.Close();
    }
}