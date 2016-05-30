using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;

public partial class Chart : System.Web.UI.Page
{
    //create private variables
    private string filePathSalaryHistorgram = HttpContext.Current.Server.MapPath("~/App_Data/salaryHistogram5.csv");
    private string filePathAverageGoldSalary = HttpContext.Current.Server.MapPath("~/App_Data/averageSalaryHistogram.csv");
    private string filePath5PercentOldest = HttpContext.Current.Server.MapPath("~/App_Data/oldestEmployees.csv");
    private string filePathageHistogram = HttpContext.Current.Server.MapPath("~/App_Data/ageHistogram.csv");
    private string deliminator = ",";

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// Event handler from when the button to create a Salary historgram
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void createHistorgramForSalary_Click(object sender, EventArgs e)
    {
        //Open a file stream and stream writer
        FileStream fs = new FileStream(filePathSalaryHistorgram, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        //Create a new 2D array with the first array being the X coords of the graph, and the second array being the value calculated
        string[][] output = new string[][]{
                new string[]{"0k-10k", "10k-20k", "20k-30k", "30k-40k", "40k-50k", "50k-60k", "60k-70k","70k-80k", "80k-90k", "90k-100k", "100k+" },
                 new string[]{ getCountOfSalary("0","10000"), getCountOfSalary("10000", "20000"), getCountOfSalary("20000", "30000"), getCountOfSalary("30000", "40000"), getCountOfSalary("40000", "50000"), getCountOfSalary("50000", "60000"), getCountOfSalary("60000", "70000"), getCountOfSalary("70000", "80000"), getCountOfSalary("80000", "90000"), getCountOfSalary("90000", "100000"), getCountOfSalary("100000", "1000000") }
               };
        int length = output.GetLength(0);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(deliminator, output[index]));
        sw.WriteLine(sb.ToString());
        sw.Close();
        fs.Close();
    }

    /// <summary>
    /// Gets the amount of employees which fall within a specific salary range
    /// </summary>
    /// <param name="startSalary">Starting salary range</param>
    /// <param name="endSalary">Ending salary range</param>
    /// <returns></returns>
    private static string getCountOfSalary(string startSalary, string endSalary)
    {
        string SQLString = @"SELECT COUNT(*) FROM UserAccount WHERE CAST(CONVERT(VARCHAR(50), DECRYPTBYPASSPHRASE('P@SSW04D', Salary))AS INT) >=" + startSalary + " AND CAST(CONVERT(VARCHAR(50), DECRYPTBYPASSPHRASE('P@SSW04D', Salary))AS INT) <" + endSalary;
        return SQLAdapter.getCount(SQLString);
    }

    /// <summary>
    /// Event function called when the user clicks the button to create histogram of users who earn above the average gold income.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void createHistorgramFromgoldClassaverageSalary_Click(object sender, EventArgs e)
    {
        //Open file stream and stream writer
        FileStream fs = new FileStream(filePathAverageGoldSalary, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        //create a single array with all the data
        string[] output = new string[] { getEmployeesThatAreAboveGoldAverage() };
        int length = output.GetLength(0);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(deliminator, output[index]));
        }

        //File.Create(filePathSalaryHistorgram);
        sw.WriteLine(sb.ToString());
        sw.Close();
        fs.Close();
        //File.WriteAllText(filePathSalaryHistorgram, sb.ToString());
    }

    /// <summary>
    /// Function which will get the employees which earn above the average gold income.
    /// </summary>
    /// <returns>SQL string</returns>
    private static string getEmployeesThatAreAboveGoldAverage()
    {
        string sqlString = @"SELECT Id,FirstName,LastName,AVG(CAST(CONVERT(VARCHAR(50),DECRYPTBYPASSPHRASE('P@SSW04D', Salary))AS INT)) AS SALARY from UserAccount
                                where MemberShipClass ='GOLD'
                                GROUP BY Salary,Id,FirstName,LastName
                                having Salary > AVG(CAST(CONVERT(VARCHAR(50),DECRYPTBYPASSPHRASE('P@SSW04D', Salary))AS INT))
                                ORDER BY SALARY DESC;";
        return SQLAdapter.getEmployeesThatHaveHigherthanAverageSalary(sqlString);
    }

    /// <summary>
    /// This function will get the top 5 percent of the oldest employees that have worked in the company.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gettop5percentoldies_Click(object sender, EventArgs e)
    {
        //Open file stream and stream writer.
        FileStream fs = new FileStream(filePath5PercentOldest, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        string[] output = new string[] { getEmployeesThatAreTheOldest() };
        int length = output.GetLength(0);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(deliminator, output[index]));
        }

        sw.WriteLine(sb.ToString());
        sw.Close();
        fs.Close();
    }

    /// <summary>
    /// Function which will create an SQL string for getting the top 5 percent of oldest employeees.
    /// </summary>
    /// <returns></returns>
    private static string getEmployeesThatAreTheOldest()
    {
        string sqlString = @"select TOP 5 PERCENT Id,FirstName,LastName,DateJoined from UserAccount
                            ORDER BY DATEDIFF(DAY, UserAccount.DateJoined, GETDATE()) DESC;";
        return SQLAdapter.getEmployeesThatHaveHigherthanAverageSalary(sqlString);

    }

    /// <summary>
    /// This will create the historgram for the amount of people within an age bracket.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void createhistorgramforage_Click(object sender, EventArgs e)
    {
        FileStream fs = new FileStream(filePathageHistogram, FileMode.OpenOrCreate, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        string[][] output = new string[][]{
                new string[]{"0-10", "10-20", "20-30", "30-40", "40-50","50-51", "51-52", "52-53", "53-54", "54-55", "55-56", "56-57", "57-58", "58-59", "59-60", "60-61", "61-62", "62-63", "63-64", "64-65", "65-66", "66-67", "67-68", "68-69", "69-70", "70-80", "80-90", "90-100", "100+" },
                 new string[]{ getCountOfAge("0","10"), getCountOfAge("10", "20"), getCountOfAge("20", "30"), getCountOfAge("30", "40"), getCountOfAge("40", "50"), getCountOfAge("50", "51"), getCountOfAge("51", "52"), getCountOfAge("52", "53"), getCountOfAge("53", "54"), getCountOfAge("54", "55"), getCountOfAge("55", "56"), getCountOfAge("56", "57"), getCountOfAge("57", "58"), getCountOfAge("58", "59"), getCountOfAge("59", "60"), getCountOfAge("60", "61"), getCountOfAge("61", "62"), getCountOfAge("62", "63"), getCountOfAge("63", "64"), getCountOfAge("64", "65"), getCountOfAge("65", "66"), getCountOfAge("66", "67"), getCountOfAge("67", "68"), getCountOfAge("68", "69"), getCountOfAge("69", "70"), getCountOfAge("70", "80"), getCountOfAge("80", "90"), getCountOfAge("90", "100"), getCountOfAge("100", "1000") }
               };
        int length = output.GetLength(0);
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(deliminator, output[index]));
        sw.WriteLine(sb.ToString());
        sw.Close();
        fs.Close();
    }

    /// <summary>
    /// Create an SQL string of the number of people within a certain age bracket.
    /// </summary>
    /// <param name="startage"></param>
    /// <param name="endage"></param>
    /// <returns></returns>
    private static string getCountOfAge(string startage, string endage)
    {
        string sqlString = @"Select count(*) from UserAccount
WHERE Datediff(YEAR,CONVERT(DATETIME,CONVERT(VARCHAR(64), DECRYPTBYPASSPHRASE('P@SSW04D', UserAccount.DOB)),111),GETDATE()) >=" + startage + " AND  Datediff(YEAR,CONVERT(DATETIME,CONVERT(VARCHAR(64), DECRYPTBYPASSPHRASE('P@SSW04D', UserAccount.DOB)),111),GETDATE()) <" + endage;
        return SQLAdapter.getCount(sqlString);
    }

    protected void ChangePicture(object sender, EventArgs e)
    {
        Image1.ImageUrl = imagedll.SelectedValue;
    }
}