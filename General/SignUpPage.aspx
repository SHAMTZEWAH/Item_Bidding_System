<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUpPage.aspx.cs" Inherits="Item_Bidding_System.General.SignUpPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up Page</title>
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="General.css" />
    
</head>
<body>
    <form id="form1" runat="server">
        <div class="content-container-signUp">
            <div class="content-subcontainer-signUp">
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/General/LoginPage.aspx" OnCreatingUser="CreateUserWizard1_CreatingUser">
                    <CreateUserButtonStyle CssClass="btn-large-golden btn-signUp" />
                    <WizardSteps>
                        <asp:CreateUserWizardStep ID="CreateUserWizardStep1" runat="server">
                            <ContentTemplate>
                                <table class="table-signUp">
                                    <tr>
                                        <td id="signUpTitle" class="title1 title1-bold table-item" align="center">Sign Up for Your New Account</td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <asp:TextBox ID="UserName" CssClass="textBox" runat="server" placeholder="Username"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <div class="textBox flex-center-center phone-container">
                                                <div class="phone-prefix">(601)</div>
                                                <div>
                                                    <asp:TextBox ID="PhoneNo" CssClass="phone-subcontainer textBox-custom" runat="server" placeholder="Phone No" MaxLength="16"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PhoneNo" ErrorMessage="Phone No is required.">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator table-item" align="center">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneNo" Display="Dynamic" ErrorMessage="The format of Phone No should be digit only." ValidationExpression="\d{8,}"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <asp:TextBox ID="Email" CssClass="textBox" runat="server" placeholder="Email" MaxLength="60"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="EmailRequired" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="Email" ErrorMessage="Duplicate email. Please use another email." OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator table-item" align="center">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="The email format is wrong. Correct format: exp1@gmail.com" ValidationExpression="[a-z0-9]+@[a-z]+\.[a-z]{2,3}"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <asp:TextBox ID="Password" CssClass="textBox" runat="server" TextMode="Password" placeholder="Password" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <asp:TextBox ID="ConfirmPassword" CssClass="textBox" runat="server" TextMode="Password" placeholder="Confirm Password" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table-item" align="center">
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table-item" align="center" style="color:Red;">
                                            <asp:Literal ID="ErrorMessage" runat="server" EnableViewState="False"></asp:Literal>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:CreateUserWizardStep>
                        <asp:CompleteWizardStep ID="CompleteWizardStep1" runat="server">
                            <ContentTemplate>
                                <table class="table-signUp">
                                    <tr>
                                        <td class="table-item"><div class="title1 title1-bold">Almost there.</div></td>
                                    </tr>
                                    <tr>
                                        <td class="table-item" align="center">Check your email for confirmation link.Didn't get one? We can <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">send</asp:LinkButton> another. </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:CompleteWizardStep>
                    </WizardSteps>
                </asp:CreateUserWizard>
            </div>
            <div class="image-container-signUp flex-center-center">
                <div class="larger-image"></div>
            </div>
        </div>
    </form>
    <script>
        document.getElementsByClassName("phone-subcontainer")[0].addEventListener("click", function () {
            document.getElementsByClassName("phone-container")[0].style.border = "3px solid black";
        });
        document.getElementsByClassName("phone-subcontainer")[0].addEventListener("focusout", function () {
            document.getElementsByClassName("phone-container")[0].style.border = "none";
        });
    </script>
</body>
</html>
