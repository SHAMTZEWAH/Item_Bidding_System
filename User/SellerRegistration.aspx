<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="SellerRegistration.aspx.cs" Inherits="Item_Bidding_System.User.SellerRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
        <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
        <link type="text/css" rel="stylesheet" href="../General/General.css" />
        <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">
        <div class="title2-black-bold content-title">Seller Registration</div>
        <div class="">
            <table style="width: 100%;border:1px solid black;">
            <tr>
                <td class="lbl">Name:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtName" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Email Address:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtEmail" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Mobile Phone No:</td>
                <td class="phone-container">
                    <div class="medium-top-inner-gap">
                        <div class="textBox flex-center-center phone-container">
                            <div class="phone-prefix">(601)</div>
                            <div><asp:TextBox ID="PhoneNo" CssClass="phone-subcontainer textBox-custom" runat="server" placeholder="Phone No" MaxLength="16"></asp:TextBox></div>
                            <div></div>
                        </div>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td class="lbl">Country: <asp:Button ID="BtnDefAddress" CssClass="textBox" runat="server" Text="Select default address" Font-Underline="True" OnClick="BtnDefAddress_Click" ViewStateMode="Enabled" /></td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="ddlCountry" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select Country--</asp:ListItem>
                            <asp:ListItem>Malaysia</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Please select a country." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">State:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="ddlState" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select State--</asp:ListItem>
                            <asp:ListItem>Johor</asp:ListItem>
                            <asp:ListItem>Kedah</asp:ListItem>
                            <asp:ListItem>Kelantan</asp:ListItem>
                            <asp:ListItem>Melaka</asp:ListItem>
                            <asp:ListItem>Negeri Sembilan</asp:ListItem>
                            <asp:ListItem>Pahang</asp:ListItem>
                            <asp:ListItem>Penang</asp:ListItem>
                            <asp:ListItem>Perak</asp:ListItem>
                            <asp:ListItem>Perlis</asp:ListItem>
                            <asp:ListItem>Sabah</asp:ListItem>
                            <asp:ListItem>Sarawak</asp:ListItem>
                            <asp:ListItem>Selangor</asp:ListItem>
                            <asp:ListItem>Terengganu</asp:ListItem>
                            <asp:ListItem>Kuala Lumpur</asp:ListItem>
                            <asp:ListItem>Labuan</asp:ListItem>
                            <asp:ListItem>Putrajaya</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlState" ErrorMessage="Please select a state." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">City:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="ddlCity" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select City--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCity" ErrorMessage="Please select a city." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Zip Code:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtZip" CssClass="textBox" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtZip" ErrorMessage="Require to fill in."></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtZip" ErrorMessage="Must have 5 digit value" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Address:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtAddress" CssClass="textBox" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAddress" ErrorMessage="Require to fill in."></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Business/Company Name:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtBusiness" CssClass="textBox" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtBusiness" ErrorMessage="Require to fill in."></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Merchant ID (optional):</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtMerchant" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="btn-container">
                        <asp:Button ID="btnRegister" CssClass="btn-small-golden btn-register" runat="server" Text="Register" OnClick="btnRegister_Click" />
                    </div>
                </td>
            </tr>
        </table>
            <div id="errorMsg" style="display:none" runat="server">
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
                </div>
        </div>
        
    </div>
</asp:Content>