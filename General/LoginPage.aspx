<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Item_Bidding_System.General.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title>Login Page</title>
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="General.css" />
    
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-container-signUp">
            <div class="content-subcontainer-signUp">
                <asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_LoggedIn">
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
                                <tr class="row-title">
                                    <td id="LoginTitle" class="title1 title1-bold medium-bottom-inner-gap" align="center">Login</td>
                                </tr>
                                <tr>
                                    <td class="medium-bottom-inner-gap" align="right">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="textBox" placeholder="  username">  </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="medium-bottom-inner-gap" align="right">
                                        <asp:TextBox ID="Password" runat="server" CssClass="textBox" placeholder="  password" TextMode="Password">  </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="medium-bottom-inner-gap">
                                        <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time." />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="cell-fail" align="center" style="color:Red;">
                                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                    </td>
                                </tr>
                                <tr class="row-btn">
                                    <td clas="cell-btn" align="center">
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="btn-large-golden btn-login" OnClick="LoginButton_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>

        </asp:Login>
                <div>
                    <asp:Label ID="lblError" runat="server" Text="*"></asp:Label>
                </div>
            </div>
            <div class="image-container-signUp">
                <div class="larger-image"></div>
            </div>
        </div>
    </form>
</body>
</html>
