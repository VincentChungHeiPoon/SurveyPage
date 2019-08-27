<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Survey.Data.Survey" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:100%;text-align:center">
            
            UserID :
            <asp:Label ID="labelUserID" runat="server" Text="UserID"></asp:Label>
            
        </div>
        <div>
            <asp:Label ID="questionLable" runat="server" Text="Question: "></asp:Label>
            <asp:Label ID="questionText" runat="server" Text="No question were found"></asp:Label>
        </div>

        <div>
            <asp:TextBox ID="textBoxAnswer" runat="server" Width="632px"></asp:TextBox>
            <asp:Label ID="optOutLabel" runat="server" Text="Opt out"></asp:Label>
            <asp:CheckBox ID="checkBoxOptOut" runat="server" />
        </div>

        <asp:Button ID="btnSubmit" runat="server" OnClick="onBtnSubmitClick" Text="Submit" />
    </form>
</body>
</html>
