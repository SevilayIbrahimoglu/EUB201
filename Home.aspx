<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="FinalProject.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .table_style {
            width: 100%;
            text-align: center;
        }
        .center {
            text-align: center;
        }
        #TextArea1 {
            height: 281px;
            width: 506px;
        }
        .auto-style2 {
            width: 100%;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="table_style">
            <asp:Button runat="server" Text="Yeni Not Ekle" OnClick="addNewNoteClicked" />
           
        </div>
       
        <div class="table_style">
            <br />
            <asp:Table ID="myTable" runat="server"> </asp:Table>  
            <br />
        </div>
        
        <div class="table_style">
            <asp:Table ID="inputTable" Width="70%" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>
                        <asp:Label ID="NewNoteTitleLabel" runat="server" Text="Yeni Not Ekle"></asp:Label>
                    </asp:TableHeaderCell>
                    <asp:TableCell>
                        <asp:ImageButton ID="closeButton" ImageUrl="Images/cancel.png" Height="15" Width="15" runat="server" OnClick="closeButton_Click" />
                    </asp:TableCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Label runat="server" Width="20%" Text="Başlık"></asp:Label>
                        
                        <asp:TextBox ID="titleTextBox" Width="60%" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="bodyTextBox" TextMode="multiline" Rows="20" Width="100%" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>

                <asp:TableRow>
                    <asp:TableCell>
                        <asp:Button ID="saveButton" runat="server" Text="Kaydet" OnClick="saveButton_Click" />
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>

        </div>
    </form>
</body>
</html>
