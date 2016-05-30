using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //Choose the grid size depending on the value of the drop down list.
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.PageSize = Convert.ToInt32(ListSizeDDL.SelectedValue);
    }

    protected void findUserButton_Click(object sender, EventArgs e)
    {
    }
}