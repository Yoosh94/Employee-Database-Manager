<%@ Page Title="Add Member" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <h1>Add Member</h1>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="firstNameTextBox" placeholder="First Name" runat="server" />
            <asp:RegularExpressionValidator runat="server" ID="regexFirstName" ControlToValidate="firstNameTextBox" Display="Dynamic" ValidationExpression="^[a-zA-Z''-'\s]{1,20}$" ErrorMessage="Incorrect Name" />
            <asp:RequiredFieldValidator runat="server" ID="reqName" ControlToValidate="firstNameTextBox" Display="Dynamic" ErrorMessage="Please enter your name!" />
            <br />
            <br />
            <asp:TextBox ID="lastNameTextBox" placeholder="Last Name" runat="server" />
            <asp:RegularExpressionValidator runat="server" ID="regexLastName" ControlToValidate="lastNameTextBox" Display="Dynamic" ValidationExpression="^[a-zA-Z''-'\s]{1,20}$" ErrorMessage="Please enter your Last name" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="lastNameTextBox" Display="Dynamic" ErrorMessage="Please enter your name!" />
            <br />
            <br />
            <asp:TextBox ID="dateOfBirthTextBox" placeholder="DOB (yyyy-mm-dd)" runat="server" />
            <asp:CompareValidator ID="dateValidator" runat="server"
                Type="Date" Operator="DataTypeCheck" ControlToValidate="dateOfBirthTextBox"
                ErrorMessage="Please enter a valid date.">
            </asp:CompareValidator>
            <br />
            <br />
            <asp:TextBox ID="salaryTextBox" placeholder="Salary (In whole dollars)" runat="server" />
            <asp:RegularExpressionValidator runat="server" ID="regexWeight" Display="Dynamic" ControlToValidate="salaryTextBox" ValidationExpression="^\d+$" ErrorMessage="Numeric numbers only (0-9)" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" Display="Dynamic" ControlToValidate="salaryTextBox" ErrorMessage="Please enter a Salary" />
            <br />
            <br />
            <asp:DropDownList ID="genderDDL" runat="server">
                <asp:ListItem Enabled="true" Text="Male" Value="Male"></asp:ListItem>
                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="addMemberSubmitButton" Text="Add Member" runat="server" OnClick="addMemberSubmitButton_Click" />
        </div>
    </form>
</asp:Content>

