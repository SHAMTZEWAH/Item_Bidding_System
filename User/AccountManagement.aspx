<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs" Inherits="Item_Bidding_System.User.AccountManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../General/General.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />


    <div class="content-container">
        <div class="title2-black-bold content-title">My Profile</div>
        <div class="title2-black content-title">Manage and Protect your account.</div>

        <div class="flex-row">
            <div class="left-container">
                <table style="width:100%;">
                <tr>
                    <td class="lbl">Username:</td>
                    <td>
                        <div class="content-adjust">
                            <asp:TextBox ID="TextBox1" CssClass="textBox" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Email:</td>
                    <td>
                        <div class="content-adjust">
                            <asp:TextBox ID="TextBox2" CssClass="textBox" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Mobile Phone No:</td>
                    <td>
                        <div class="textBox flex-center-center phone-container" style="margin-left: 60px;margin-top:8%;">
                            <div class="phone-prefix">(601)</div>
                            <div><asp:TextBox ID="PhoneNo" CssClass="phone-subcontainer textBox-custom" runat="server" placeholder="Phone No" MaxLength="16"></asp:TextBox></div>
                            <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PhoneNo" ErrorMessage="Phone No is required.">*</asp:RequiredFieldValidator></div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Gender:</td>
                    <td>
                        <div class="content-adjust">
                            <asp:RadioButtonList CssClass="" ID="RadioButtonList1" runat="server">
                            <asp:ListItem>Male</asp:ListItem>
                            <asp:ListItem>Female</asp:ListItem>
                        </asp:RadioButtonList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Date of Birth:</td>
                    <td>
                        <div class="content-adjust">
                            <asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
                        </div>
                    </td>
                </tr>
            </table>
            </div>
            <div class="right-container">
                <div class="image-container">
                <!--<asp:Image src="" ID="Image1" runat="server" />-->
                    <asp:Button ID="btnChange" runat="server" CssClass="btn-large-golden" Text="Change Image" />
                </div>
            </div>
            
        </div>
        <div class="btn-container">
                <asp:Button ID="btnSave" CssClass="btn-small-golden" runat="server" Text="Save" />
            </div>
    </div>
</asp:Content>
