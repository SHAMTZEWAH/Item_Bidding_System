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
                            <asp:TextBox ID="txtUsername" CssClass="textBox" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Email:</td>
                    <td>
                        <div class="content-adjust">
                            <asp:TextBox ID="txtEmail" CssClass="textBox" runat="server"></asp:TextBox>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="lbl">Mobile Phone No:</td>
                    <td>
                        <div class="content-adjust">
                            <div class="textBox flex-center-center phone-container">
                                <div class="phone-prefix">(601)</div>
                                <div><asp:TextBox ID="txtPhoneNo" CssClass="phone-subcontainer textBox-custom" runat="server" placeholder="Phone No" MaxLength="16" OnTextChanged="txtPhoneNo_TextChanged"></asp:TextBox></div>
                                <div><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPhoneNo" ErrorMessage="Phone No is required.">*</asp:RequiredFieldValidator></div>
                            </div>
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
                <div id="errorMsg" style="display:none" runat="server">
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
                </div>
            </div>
            <div class="right-container">
                <div class="image-container">
                    <div>
                        <asp:Image src='<% Eval("") %>' ID="Image1" runat="server" />
                    </div>
                    <div>
                        <asp:Button ID="btnChange" runat="server" CssClass="btn-large-golden" Text="Change Image" OnClick="btnChange_Click" />
                    </div>
                    <div>Or</div>
                    <div>
                        <div><asp:TextBox ID="txtImageUrl" runat="server"></asp:TextBox></div>
                        <div><asp:Button ID="btnSubmitURL" runat="server" Text="Button" /></div>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="btn-container">
                <asp:Button ID="btnSave" CssClass="btn-small-golden" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
    </div>
</asp:Content>
