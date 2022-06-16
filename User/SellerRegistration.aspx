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
                        <asp:TextBox ID="TextBox1" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Email Address:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="TextBox2" CssClass="textBox" runat="server"></asp:TextBox>
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
                            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PhoneNo" ErrorMessage="Phone No is required.">*</asp:RequiredFieldValidator></div>
                        </div>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td class="lbl">Country:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="DropDownList1" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select Country--</asp:ListItem>
                            <asp:ListItem>Malaysia</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">State:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="DropDownList2" CssClass="textBox" runat="server">
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
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">City:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="DropDownList3" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select City--</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Zip Code:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="TextBox4" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Address:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="TextBox6" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Business/Company Name:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="TextBox7" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Merchant ID (optional):</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="TextBox8" CssClass="textBox" runat="server"></asp:TextBox>
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
        </div>
        
    </div>
</asp:Content>