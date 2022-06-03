<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpPage.aspx.cs" Inherits="Item_Bidding_System.General.SignUpPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style>
        .signUp-container{
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
        
        .signUp-only-container{
            position: relative;
            width:48%;
            height:auto;
            display:flex;
            align-self: flex-start;
            justify-content: center;
        }
        .signUp-image-container{
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
            display:flex;
            flex-flow: row wrap;
            justify-content: center;
            align-items: center;
            padding: 0px;
        }
        #signUpTitle{
            font-family:'Century Schoolbook';
            font-size: 25px;
            font-weight: bold;
            color: midnightblue;
            vertical-align:top;
            padding:30px 0px 60px 0px;
        }
        .text{
            border:1px solid #F5F5F5;
            border-radius: 4px;
            background-color: #F5F5F5;
            width: 250px;
            height: 35px;
            text-align: center;
        }
        ::placeholder{
            color:#AAAAAA;
        }
        .phone-container{
            position:relative;
            margin: 0 auto;
            display:flex;
            flex-flow: row wrap;
            justify-content: center;
            align-items: center;
        }
        .phone-prefix::before{
            content:"(601)";
            position:absolute;
            top:15%;
            left:20%;
        }

        #ContinueButton{
            border: 1px solid goldenrod;
            border-radius: 4px;
            background-color: goldenrod;
            font-size: 15px;
            color: white;
            text-align:center;
            padding: 10px 50px;
            cursor: pointer;
        }
        .btn-signUp-container{
            display:flex;
            justify-content: flex-start;
            align-items: flex-start;
            width:100%;
        }
        
        .btn-signUp{
            border: 1px solid goldenrod;
            border-radius: 4px;
            background-color: goldenrod;
            font-size: 15px;
            color: white;
            text-align:center;
            padding: 10px 0px;
            cursor: pointer;
            margin: auto;
            width: 40%;
            margin-right: 130px;
            margin-top: 5px;
        }
        .btn-signUp:hover{
            opacity: 0.8;
        }
         .adjust-gap{
             padding-bottom: 20px;
         }
    </style>

    
</head>
<body>
    <form id="form1" runat="server">
        <div class="signUp-container">
            <div class="signUp-only-container">
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server">
                    <CreateUserButtonStyle CssClass="btn-signUp" />
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                            <ContentTemplate>
                                <table class="table">
                                    <tr>
                                        <td id="signUpTitle" class="" align="center">Sign Up for Your New Account</td>
                                    </tr>
                                    <tr>
                                        <td class=" adjust-gap" align="center">
                                            <asp:TextBox ID="UserName" CssClass="text" runat="server" placeholder="Username"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="phone-container adjust-gap" align="center">
                                            <div class="phone-prefix"></div>
                                            <asp:TextBox ID="PhoneNo" CssClass="text phone-text" runat="server" placeholder="Phone No" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PhoneNo" ErrorMessage="Phone No is required.">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator" align="center">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneNo" Display="Dynamic" ErrorMessage="The format of Phone No should be digit only." ValidationExpression="/d{8,}"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="adjust-gap" align="center">
                                            <asp:TextBox ID="Email" CssClass="text" runat="server" placeholder="Email" MaxLength="60"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator" align="center">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="The email format is wrong. Correct format: exp1@gmail.com" ValidationExpression="[a-z0-9]+@[a-z]+\.[a-z]{2,3}"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="adjust-gap" align="center">
                                            <asp:TextBox ID="Password" CssClass="text" runat="server" TextMode="Password" placeholder="Password" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="adjust-gap" align="center">
                                            <asp:TextBox ID="ConfirmPassword" CssClass="text" runat="server" TextMode="Password" placeholder="Confirm Password" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="color:Red;">
                                            <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <CustomNavigationTemplate>
                                <div class="btn-signUp-container">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn-signUp" Text="Create User" />
                                </div>
                            </CustomNavigationTemplate>
                        </asp:CreateUserWizardStep>
                        <asp:WizardStep runat="server" Title="Email Confirmation">
                        </asp:WizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td>Your account has been successfully created.</td>
                                    </tr>
                                    <tr>
                                        <td align="center">Complete</td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Button ID="ContinueButton" runat="server" CausesValidation="False" CommandName="Continue" Text="Continue" ValidationGroup="CreateUserWizard1" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                </asp:CreateUserWizard>
            </div>
            <div class="signUp-image-container">
                <div class="image"></div>
            </div>
        </div>
    </form>
</body>
</html>
