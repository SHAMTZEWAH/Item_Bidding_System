<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="AccountManagement.aspx.cs" Inherits="Item_Bidding_System.User.AccountManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../General/General.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />

    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
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
                <tr>
                <td class="lbl">Country: </td>
                <td>
                    <div class="medium-top-inner-gap content-adjust flex-column">
                        <asp:DropDownList ID="ddlCountry" CssClass="textBox" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                            <asp:ListItem Value="0">--Select Country--</asp:ListItem>
                            <asp:ListItem>Malaysia</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlCountry" ErrorMessage="Please select a country." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                        <div>
                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" Text="Default Address" />
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">State:</td>
                <td>
                    <div class="medium-top-inner-gap content-adjust">
                        <asp:DropDownList ID="ddlState" CssClass="textBox" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="0">--Select State--</asp:ListItem>
                            <%--<asp:ListItem>Johor</asp:ListItem>
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
                            <asp:ListItem>Putrajaya</asp:ListItem>--%>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="ddlState" ErrorMessage="Please select a state." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">City:</td>
                <td>
                    <div class="medium-top-inner-gap content-adjust">
                        <asp:DropDownList ID="ddlCity" CssClass="textBox" runat="server" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="0">--Select City--</asp:ListItem>
                        </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="ddlCity" ErrorMessage="Please select a city." Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Zip Code:</td>
                <td>
                     <div class="medium-top-inner-gap content-adjust">
                         <asp:TextBox ID="txtZip" CssClass="textBox" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtZip" ErrorMessage="Require to fill in."></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtZip" ErrorMessage="Must have 5 digit value" ValidationExpression="\d{5}"></asp:RegularExpressionValidator>
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Address:</td>
                <td>
                     <div class="medium-top-inner-gap content-adjust">
                         <asp:TextBox ID="txtAddress" CssClass="textBox" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" ErrorMessage="Require to fill in."></asp:RequiredFieldValidator>
                    </div>
                </td>
            </tr>
            </table>
        </div>
            <div class="right-container">
                <div class="image-container">
                    <div class="small-top-gap title2-black content-title" style="text-align:center;">Profile Photo</div>
                        <div class="small-top-gap flex-center-center">
                        <!--display image-->
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Vertical" RepeatColumns="1" >
                                    <ItemTemplate>
                                        <div class="flex-column small-top-gap" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value) || !Eval("accPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                            <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                                <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("accPhotoURL") %>' runat="server" Height="100px" />
                                            </div>
                                            <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                                                <%# Convert.ToBase64String((byte[])Eval("accPhoto")).Trim() != string.Empty ?true:false %>'>
                                                <asp:Image ID="Image2" ImageUrl='<%# String.Concat("~/User/ProcessPhoto.ashx?accId=",Eval("accId")) %>' Width="200px" runat="server" Height="100px" />
                                            </div>
                                            <asp:HiddenField ID="hfRowAccId" Value='<%# Eval("accId") %>' runat="server" />
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlId="btnSubmitURL" EventName="Click" />
                                <asp:PostBackTrigger ControlId="btnSubmitPhoto" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>

                    <div class="medium-top-inner-gap">
                    <div class="flex-center-center">
                        <!--Button goes here-->
                        <div id="btnInsertURL" class="btn-medium-white-custom border-black btn-blue-hover">
                            <i class="bi bi-link-45deg"></i>
                            <div>Insert URL</div>
                        </div>
                        <div class="or-text-margin">OR</div>
                        <div id="btnUploadPhoto" class="btn-medium-white-custom border-black btn-blue-hover"> 
                            <i class="bi bi-upload"></i>
                            <div>Upload Photo</div>
                        </div>
                    </div>
                     <div id="insertURLContainer" class="displayLess medium-top-inner-gap">
                         <!--Insert upload url-->
                         <div>Paste URL</div>
                         <div class="flex-flow flex-row">
                             <div>
                                 <asp:TextBox ID="txtInsertURL" runat="server"></asp:TextBox>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitURL" runat="server" Text="Submit URL" OnClick="btnSubmitURL_Click" /> <!--Submit URL and display preview image-->
                            </div>
                         </div>
                    </div>
                     <div id="uploadPhotoContainer" class="displayLess medium-top-inner-gap">
                         <div>Upload Images</div>
                         <div>
                         <!--Insert upload file-->
                             <div>
                                 <asp:FileUpload ID="txtUploadPhoto" runat="server" AllowMultiple="True" />
                             </div>
                             <div>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only .jpg, .png, bitmap image is supported" Text="*" ValidationExpression="/^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.JPG|.jpg|.bitmap|.BITMAP)$/" ControlToValidate="txtUploadPhoto" ValidationGroup="photoValidate"></asp:RegularExpressionValidator>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitPhoto" runat="server" Text="Submit Photo" OnClick="btnSubmitPhoto_Click" /> <!--Submit URL and display preview image-->
                            </div>
                        </div>
                     </div>
                    </div>
                </div>
            </div>
            <div id="errorMsg" style="display:none" runat="server">
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            </div>
        <div class="btn-container">
                <asp:Button ID="btnSave" CssClass="btn-small-golden" runat="server" Text="Save" OnClick="btnSave_Click" />
            </div>
    </div>
    <script>
    document.getElementById("btnInsertURL").addEventListener("click", function () {
        displayURL();
    });

    document.getElementById("btnUploadPhoto").addEventListener("click", function () {
        displayPhoto();
    });

    function displayURL() {
        var urlContainer = document.getElementById("insertURLContainer");
        var photoContainer = document.getElementById("uploadPhotoContainer");

        if (!photoContainer.classList.contains("displayLess")) {
            photoContainer.classList.add("displayLess");
        }
        if (urlContainer.classList.contains("displayLess")) {
            urlContainer.classList.remove("displayLess");
        }
        else {
            urlContainer.classList.add("displayLess");
        }
    }

    function displayPhoto() {
        var urlContainer = document.getElementById("insertURLContainer");
        var photoContainer = document.getElementById("uploadPhotoContainer");

        if (!urlContainer.classList.contains("displayLess")) {
            urlContainer.classList.add("displayLess");
        }
        if (photoContainer.classList.contains("displayLess")) {
            photoContainer.classList.remove("displayLess"); 
        }
        else {
            photoContainer.classList.add("displayLess");
        }
    }

    
    </script>

</asp:Content>
