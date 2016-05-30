<%@ Page Title="Manage Members" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SearchMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="App_Themes/Theme1/insidePages.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <h1>Search and Update Members</h1>
    Insert known information. All fields are not needed.
    <form id="form1" runat="server">
        <div>
            <%--Create the user input fields--%>
            <asp:TextBox ID="idTextBox" placeholder="Membership ID" runat="server" />
            <br />
            <br />
            <asp:TextBox ID="firstNameTextBox" placeholder="First Name" runat="server" />
            <br />
            <br />
            <asp:TextBox ID="lastNameTextBox" placeholder="Last Name" runat="server" />
            <br />
            <br />
            <asp:Button ID="findUserButton" Text="Search for User" runat="server" OnClick="findUserButton_Click" />
            <br />
            <br />
            <%--Create a dtop down list to limit how many employees the user wants to view--%>
            <asp:Label Text="Rows to view: " runat="server" />
            <asp:DropDownList ID="ListSizeDDL" runat="server" AutoPostBack="True">
                <asp:ListItem Text="5" Value="5" />
                <asp:ListItem Text="10" Value="10" />
                <asp:ListItem Text="15" Value="15" />
                <asp:ListItem Text="20" Value="20" />
                <asp:ListItem Text="25" Value="25" />
                <asp:ListItem Text="30" Value="30" />
            </asp:DropDownList>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None" AllowSorting="True" AllowPaging="True">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
                    <asp:BoundField DataField="Id" HeaderText="Member ID" InsertVisible="False" ReadOnly="True" SortExpression="Id" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
                    <asp:BoundField DataField="MemberShipClass" HeaderText="Membership Class" SortExpression="MemberShipClass" ReadOnly="True" />
                    <asp:BoundField DataField="DateJoined" HeaderText="Date Joined" SortExpression="DateJoined" ReadOnly="True" />
                    <asp:BoundField DataField="DOB" HeaderText="Date of Birth" SortExpression="DOB" ReadOnly="True" />
                    <asp:BoundField DataField="Salary" HeaderText="($)Salary" SortExpression="Salary" />
                    <asp:BoundField DataField="Gender" HeaderText="Gender" SortExpression="Gender" ReadOnly="true" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <br />
            <%--Select, Update and Delete functions are called here--%>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="SELECT [Id], [FirstName], [LastName], [DateJoined], [MemberShipClass],CONVERT(varchar(50),DECRYPTBYPASSPHRASE ('P@SSW04D',[DOB])) AS DOB,CONVERT(varchar(max), DECRYPTBYPASSPHRASE('P@SSW04D',Salary)) as Salary,Convert(varchar(50),DECRYPTBYPASSPHRASE('P@SSW04D', [Gender])) AS [Gender] FROM [UserAccount] WHERE (([Id] = @Id) OR ([FirstName] LIKE '%' + @FirstName + '%') OR ([LastName] LIKE '%' + @LastName + '%'))"
                UpdateCommand="UPDATE [UserAccount] SET [FirstName] = @FirstName, [LastName] = @LastName, [Salary] = ENCRYPTBYPASSPHRASE('P@SSW04D',Convert(varchar(10),@Salary)) WHERE [Id] = @Id"
                DeleteCommand="DELETE FROM [UserAccount] WHERE [Id] = @Id">
                <%--Parameters for update--%>
                <UpdateParameters>
                    <asp:Parameter Name="FirstName" Type="String" />
                    <asp:Parameter Name="LastName" Type="String" />
                    <asp:Parameter Name="MemberShipClass" Type="String" />
                    <asp:Parameter Name="DateJoined" Type="String" />
                    <asp:Parameter Name="DOB" Type="String" />
                    <asp:Parameter Name="Salary" Type="String" />
                    <asp:Parameter Name="Gender" Type="String" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
                <%--Parameters for delete--%>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
                <%--Parameters for Select--%>
                <SelectParameters>
                    <asp:ControlParameter Name="Id" Type="Int32" ControlID="idTextBox" DefaultValue="0" PropertyName="Text" />
                    <asp:ControlParameter Name="FirstName" Type="String" ControlID="firstNameTextBox" DefaultValue=" " PropertyName="Text" />
                    <asp:ControlParameter Name="LastName" Type="String" ControlID="lastNameTextBox" DefaultValue=" " PropertyName="Text" />
                </SelectParameters>
            </asp:SqlDataSource>
        </div>
    </form>
</asp:Content>

