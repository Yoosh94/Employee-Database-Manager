<%@ Page Title="Migrate Member" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MigrateMember.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainBody" runat="Server">
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="memberShipIDTextBox" placeholder="ID of member to migrate" runat="server" />
            <br />
            <br />
            <asp:Button ID="migrateEmployee" runat="server" OnClick="migrate_employee" Text="Migrate Employee" />
            <%--<asp:Button id="Button1" runat="server" onclick="test" Text="Update Memberships"/>--%>
            <%--Button used to migrate first 500 employees--%>
            <%--<asp:Button ID="Button2" runat="server" OnClick="migrate" Text="Button" />--%>
        </div>

    </form>
</asp:Content>

