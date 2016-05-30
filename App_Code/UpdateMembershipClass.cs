using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UpdateMembershipClass
/// </summary>
public static class UpdateMembershipClass
{

    /// <summary>
    /// This method is run everytime someone new is added. It will assign 100% of the employees with a Regular membership.
    /// Then the top 30 percent with the silver membership
    /// Then the top 10 with Gold membership, and finally those who are not alreayd gold and silver with loyalty if they have been 
    /// with the compnay more than 25 years.
    /// </summary>
    public static void updateMembershipClass()
    {
        //Assign Regular
        updateMembership("100", "REGULAR");
        //Assign Silver
        updateMembership("30", "SILVER");
        //Update gold.
        updateMembership("10", "GOLD");
        updateMembership("LOYALTY");

    }

    /// <summary>
    /// Updates employee membership details depending on percentage.
    /// </summary>
    /// <param name="percent">Top percent that needs to be changed</param>
    /// <param name="membershipclass">Membership category to change them too</param>
    private static void updateMembership(string percent, string membershipclass)
    {
        string SQLSelect = createSelectString(percent);
        string SQLUpdate = createUpdateString(membershipclass);
        SQLAdapter.updateMembership(SQLSelect, SQLUpdate);
    }
    
    /// <summary>
    /// Overload method only used to update loyalty members
    /// </summary>
    /// <param name="membershipclass"></param>
    private static void updateMembership(string membershipclass)
    {
        string SQLSelect = createSelectStringForLoyalty();
        string SQLUpdate = createUpdateString(membershipclass);
        SQLAdapter.updateMembership(SQLSelect, SQLUpdate);
    }
    /// <summary>
    /// Create an SQL Update string
    /// </summary>
    /// <param name="membershipType">Membership type</param>
    /// <returns></returns>
    private static string createUpdateString(string membershipType)
    {
        return "UPDATE UserAccount SET MemberShipClass ='" + membershipType + "' WHERE Id = _::_";
    }
    /// <summary>
    /// Create an SQL Select string
    /// </summary>
    /// <param name="topPercent">Top percent</param>
    /// <returns></returns>
    private static string createSelectString(string topPercent)
    {
        string sqlString = @"SELECT TOP " + topPercent + " Percent Id,MemberShipClass,CAST(CONVERT(varchar(64), DECRYPTBYPASSPHRASE('P@SSW04D',Salary)) AS float) as Salary FROM UserAccount ORDER BY Salary DESC";
        return sqlString;
    }

    /// <summary>
    /// Create Select statement used to update the membership for loyalty customers.
    /// </summary>
    /// <returns></returns>
    private static string createSelectStringForLoyalty()
    {
        string sqlString = @"SELECT Id FROM UserAccount WHERE DATEDIFF(YEAR, UserAccount.DateJoined, GETDATE()) > 25 AND MemberShipClass = 'REGULAR'";
        return sqlString;
    }
}