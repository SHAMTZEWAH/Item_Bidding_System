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
                            <asp:RadioButtonList CssClass="" ID="radioGender" runat="server">
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
                    <div class="flex-row small-top-gap" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value) || !Eval("accPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                        <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value)?true:false %>'>
                            <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("accPhotoURL") %>' runat="server" Height="100px" />
                        </div>
                        <div id="imgCon2" class="border-black flex-column" runat="server" visible='<%# !Eval("accPhoto").Equals(DBNull.Value) ?true:false %>'>
                            <asp:Image ID="Image2" ImageUrl='<%# String.Concat("~/User/ProcessPhoto.ashx?accId=",Eval("accId")) %>' Width="200px" runat="server" Height="100px" />
                        </div>
                        <asp:HiddenField ID="hfRowAccId" Value='<%# Eval("accId") %>' runat="server" />
                    </div>
                    <div class="displayLess medium-top-inner-gap">
                         <div>Upload Images</div>
                         <div>
                         <!--Insert upload file-->
                             <div>
                                 <asp:FileUpload ID="txtUploadPhoto" runat="server" AllowMultiple="false" />
                             </div>
                             <div>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only .jpg, .png, bitmap image is supported" Text="*" ValidationExpression="/^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.JPG|.jpg|.bitmap|.BITMAP)$/" ControlToValidate="txtUploadPhoto" ValidationGroup="photoValidate"></asp:RegularExpressionValidator>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitPhoto" runat="server" CssClass="btn-large-golden" Text="Change Image" OnClick="btnSubmitPhoto_Click" /> <!--Submit URL and display preview image-->
                            </div>
                        </div>
                     </div>
                    <div>Or</div>
                    <div class="flex-center-end">
                        <div><asp:TextBox ID="txtImageUrl" runat="server"></asp:TextBox></div>
                        <div><asp:Button ID="btnSubmitURL" CssClass="btn-large-golden" runat="server" Text="Submit URL" OnClick="btnSubmitURL_Click" /></div>
                    </div>
                </div>
            </div>
            
        </div>
        <div class="btn-container">
                <asp:Button ID="btnSave" CssClass="btn-small-golden" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
    </div>
</asp:Content>
