<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Chart.aspx.cs" Inherits="Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <form id="form1" runat="server">
        <asp:button id="createHistorgramForSalary" text="Create Salary Historgram" onclick="createHistorgramForSalary_Click" runat="server" />
        <asp:button id="createhistorgramforage" runat="server" onclick="createhistorgramforage_Click" text="Create Historgram for Age" />
        <asp:button id="createHistorgramFromgoldClassaverageSalary" onclick="createHistorgramFromgoldClassaverageSalary_Click" text="Earn above average salary Gold Class" runat="server" />
        <asp:button id="gettop5percentoldies" onclick="gettop5percentoldies_Click" runat="server" text="get top 5% of oldest employees" />
        <br />
        <br />
        <div>
            I want to see:
            <asp:dropdownlist onselectedindexchanged="ChangePicture" id="imagedll" runat="server" autopostback="true">
                <asp:ListItem Value="">No Choice</asp:ListItem>
                <asp:ListItem Value="~/Images/SalaryImg.PNG">Salary Histogram</asp:ListItem>
                <asp:ListItem Value="~/Images/ageDistribution.jpg">Age Histogram</asp:ListItem>
                <asp:ListItem Value="~/Images/oldestEmployees.PNG">Top 5% of oldest employees</asp:ListItem>
                <asp:ListItem Value="~/Images/averageTop5percentSalary.PNG">Employees who earn more than the GOLD average </asp:ListItem>
            </asp:dropdownlist>
            <br />
            <br />
            <asp:image id="Image1" runat="server" imageurl="" />
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" runat="Server">
</asp:Content>

