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
                <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/General/LoginPage.aspx" OnCreatingUser="CreateUserWizard1_CreatingUser" OnCreatedUser="CreateUserWizard1_CreatedUser1" InstructionText="Please fill up the form before using our user-restricted features.">
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
                                            <div class="flex-column flex-center textBox">
                                                <div>
                                                <asp:TextBox ID="UserName" CssClass="textBox" runat="server" placeholder="Username"></asp:TextBox>
                                            </div>
                                            <div>
                                                <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ForeColor="Red" ToolTip="User Name is required." ValidationGroup="CreateUserWizard1" Display="Dynamic">*</asp:RequiredFieldValidator>
                                            </div>
                                            </div>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item" align="center">
                                            <div class="textBox flex-column" style="align-items: center; justify-content: center;">
                                                <div class="textBox flex-center-center phone-container">
                                                    <div class="phone-prefix">(601)</div>
                                                    <div>
                                                        <asp:TextBox ID="PhoneNo" CssClass="phone-subcontainer textBox-custom" runat="server" placeholder="Phone No" MaxLength="16"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="PhoneNoValidator" runat="server" ControlToValidate="PhoneNo" ErrorMessage="Phone No is required." ForeColor="Red" Display="Dynamic" ValidationGroup="CreateUserWizard1" ClientIDMode="Static">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator table-item" align="center">
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="PhoneNo" Display="Dynamic" ErrorMessage="The format of Phone No should be digit only." ForeColor="Red" ValidationGroup="CreateUserWizard1" ValidationExpression="\d{8,}"></asp:RegularExpressionValidator>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item container-block" align="center">
                                            <div class="flex-column flex-center textBox">
                                                <div>
                                                    <asp:TextBox ID="Email" CssClass="textBox" runat="server" placeholder="Email" MaxLength="60" OnTextChanged="Email_TextChanged"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Email" ErrorMessage="E-mail is required." ForeColor="Red" ToolTip="E-mail is required." ValidationGroup="CreateUserWizard1" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="Email" Display="Dynamic" ErrorMessage="The email format is wrong. Correct format: exp1@gmail.com" ForeColor="Red" ValidationGroup="CreateUserWizard1" ValidationExpression="[a-z0-9]+@[a-z]+\.[a-z]{2,3}"></asp:RegularExpressionValidator>
                                            <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="Email" ErrorMessage="Email Exists. Please use another email." ForeColor="Red" OnServerValidate="CustomValidator2_ServerValidate" Display="Dynamic" ValidateEmptyText="True" ValidationGroup="CreateUserWizard1"></asp:CustomValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="validator table-item" align="center">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item container-block" align="center">
                                            <div class="flex-column flex-center textBox">
                                                <div>
                                                    <asp:TextBox ID="Password" CssClass="textBox" runat="server" TextMode="Password" placeholder="Password" MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ForeColor="Red" ToolTip="Password is required." ValidationGroup="CreateUserWizard1" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="Password" ErrorMessage="The password must consists of digit, lower alphabet, upper alphabet and symbol." ForeColor="Red" Display="Dynamic" ValidationGroup="CreateUserWizard1" ValidationExpression='^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&,.|#^[])[A-Za-z\d[@$!%*?&,.|#^[]{8,30}$'></asp:RegularExpressionValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="Password" runat="server" ErrorMessage="The password must be at least 8 characters." ForeColor="Red" Display="Dynamic" ValidationGroup="CreateUserWizard1" ValidationExpression='^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&,.|#^[])[A-Za-z\d[@$!%*?&,.|#^[]{8,30}$'></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="medium-bottom-inner-gap table-item container-block" align="center">
                                            <div class="flex-column flex-center textBox">
                                                <div>
                                                    <asp:TextBox ID="ConfirmPassword" CssClass="textBox" runat="server" TextMode="Password" placeholder="Confirm Password" MaxLength="30"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <asp:RequiredFieldValidator ID="ConfirmPasswordRequired" runat="server" ControlToValidate="ConfirmPassword" ErrorMessage="Confirm Password is required." ForeColor="Red" ToolTip="Confirm Password is required." ValidationGroup="CreateUserWizard1" Display="Dynamic">*</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="table-item" align="center">
                                            <asp:CompareValidator ID="PasswordCompare" runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" Display="Dynamic" ErrorMessage="The Password and Confirmation Password must match." ForeColor="Red" ValidationGroup="CreateUserWizard1"></asp:CompareValidator>
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
                                        <td class="table-item" align="center">Check your email for confirmation link.Didn't get one? We can <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">send</asp:LinkButton> another. <br /> You can click <a href="Home.aspx">here</a> to go to home page.</td>
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
