<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Survey.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            UserID<asp:TextBox ID="textBoxUserID" runat="server"></asp:TextBox>
            <asp:Button ID="btnLogin" runat="server" OnClick="onBtnLoginClick" Text="Login" />
        </div>        
    </form>
</body>
</html>
