<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <div>
            <div>
                <asp:Label ID="Label2" runat="server" Text="Enter you Mobile Number"></asp:Label>
                <asp:TextBox ID="txtEnterMobile" runat="server"></asp:TextBox>
            </div>
            <div>
                <asp:Button ID="btnEnter" runat="server" Text="Enter" OnClick="btnEnter_Click" />
            </div>
            <br />
            <br />

            <div>
                 <asp:GridView ID="gvUserProfile" AutoGenerateColumns="true" runat="server"></asp:GridView>
            </div>
        </div>
        <br />
        <br />
    <div>
        <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblEmail" runat="server" Text="Email"></asp:Label>
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblMobile" runat="server" Text="Mobile"></asp:Label>
        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblAddress" runat="server" Text="Address"></asp:Label>
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        <br />

        <asp:Button ID="btnSignUp" runat="server" Text="Add" OnClick="btnSignUp_Click" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
