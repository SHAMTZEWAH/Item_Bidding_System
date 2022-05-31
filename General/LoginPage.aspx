<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Item_Bidding_System.General.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <style>
        .login-container{
            height: 75%;
            width: 75%;
            margin: auto;
            margin-top:5%;
            display:flex;
            flex-flow: row wrap;
            justify-content: space-between;
            align-items: center;
            border: 1px solid lightgray;
            border-radius:4px;
        }
        
        .login-only-container{
            position: relative;
            width:48%;
            height:auto;
            display:flex;
            justify-content: center;
            align-items: center;
        }
        .login-image-container{
            position: relative;
            width:50%;
            padding: 20px 0px;
            background-color: aliceblue;
            display:flex;
            justify-content: center;
            align-items: center;
        }
        .image {
            position:relative;
            width: 500px;
            height: 500px;
            overflow: visible;
            aspect-ratio: 1 / 1;
            background-image: url(https://www.jumblebee.co.uk/site/wp-content/uploads/2014/06/JB-FE-Shop_10.png);
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
        }
        td{
            height:40px;
        }
        td.cell-fail{
            height: 5px !important;
        }
        .row-title{
            height:100px !important;
        }
        #LoginTitle{
            font-family:'Century Schoolbook';
            font-size: 25px;
            font-weight: bold;
            color: midnightblue;
            vertical-align:top;
        }
        .text{
            border:1px solid #F5F5F5;
            border-radius: 4px;
            background-color: #F5F5F5;
            width: 200px;
            height: 30px;
            text-align: center;
        }
        ::placeholder{
            color:#AAAAAA;
        }
        .btn-login{
            border: 1px solid goldenrod;
            border-radius: 4px;
            background-color: goldenrod;
            font-size: 15px;
            color: white;
            text-align:center;
            padding: 10px 50px;
            cursor: pointer;
        }
        .btn-login:hover{
            opacity: 0.8;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <div class="login-only-container">
                <asp:Login ID="Login1" runat="server">
            <LayoutTemplate>
                <table cellpadding="1" cellspacing="0" style="border-collapse:collapse;">
                    <tr>
                        <td>
                            <table cellpadding="0">
                                <tr class="row-title">
                                    <td id="LoginTitle" align="center">Login</td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="UserName" runat="server" CssClass="text" placeholder="  username">  </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:TextBox ID="Password" runat="server" CssClass="text" placeholder="  password" TextMode="Password">  </asp:TextBox>
                                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
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
                                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="btn-login" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>

        </asp:Login>
            </div>
            <div class="login-image-container">
                <div class="image"></div>
            </div>
        </div>
    </form>
</body>
</html>
