<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hackWords.aspx.cs" Inherits="hackWords.hackWordsWeb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 421px;
        }
        .auto-style2 {
            width: 53%;
        }
        .auto-style3 {
            width: 20%;
        }
        .auto-style4 {
            width: 20%;
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="auto-style2" >
                <tr>
                    <td class="auto-style1">LETRAS</td>
                    <td>Tamaño de palabras</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtLetters" runat="server" Width="384px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGroupsize" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearchWords" runat="server" OnClick="btnSearchWords_Click" Text="Generar" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <table class="auto-style3" style="display: inline-block;">
                <tr>
                    <td>
                    Palabras:
                        <asp:Label ID="lblValidWords" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvValidWords" runat="server">
                        </asp:GridView>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
                </table>
            <table class="auto-style4">
                <tr>
                    <td>Permutaciones: <asp:Label ID="lblTotalPermutes" runat="server" Text="0"></asp:Label>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvTotalPermutations" runat="server" AllowPaging="True" OnPageIndexChanging="gvTotalPermutations_PageIndexChanging" PageSize="20">
                            <PagerSettings PageButtonCount="20" />
                        </asp:GridView>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                </table>
        </div>
    </form>
</body>
</html>
