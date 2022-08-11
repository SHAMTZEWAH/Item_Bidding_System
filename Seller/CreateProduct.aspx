<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="CreateProduct.aspx.cs" Inherits="Item_Bidding_System.Seller.CreateProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

<link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
<link type="text/css" rel="stylesheet" href="../Content.css" />

    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
         <div id="Title" class="title2-black-bold content-title" runat="server">Upload Product</div>
        <div>
            <asp:Label ID="lblMessage" CssClass="filter-content" runat="server" Text="*"></asp:Label></div>
        <div class="content-subcontainer">
            <div class="title2-black content-title">Product Details</div>
            <table style="width: 100%;">
            <tr>
                <td class="lbl">Product Name:</td>
                <td>
                    <div>
                        <div class="medium-top-inner-gap">
                            <asp:TextBox ID="txtProdName" CssClass="textBox" runat="server"></asp:TextBox>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the product name." ControlToValidate="txtProdName" ValidationGroup="ProductDetails">*</asp:RequiredFieldValidator>
                        </div>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Category:</td>
                <td>
                    <div>
                        <div class="medium-top-inner-gap">
                            <asp:DropDownList ID="ddlProdCategory" CssClass="textBox" runat="server" AutoPostBack="True">
                                <asp:ListItem Value="-1">--Select Category--</asp:ListItem>
                                <asp:ListItem Value="Fashion Apparel">Fashion Apparel</asp:ListItem>
                                <asp:ListItem Value="Artworks">Artworks</asp:ListItem>
                                <asp:ListItem Value="Modern Collectible">Modern Collectible</asp:ListItem>
                                <asp:ListItem Value="Old Collectible">Old Collectible</asp:ListItem>
                                <asp:ListItem Value="LuxuryItem">Luxury Item</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="CompareValidator" ControlToValidate="ddlProdCategory" CultureInvariantValues="False" ClientIDMode="Inherit" ValidationGroup="ProductDetails" ValueToCompare="-1" Operator="NotEqual" Text="*"></asp:CompareValidator>
                        </div>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td class="lbl">Type:</td>
                <td>
                    <div>
                        <div class="medium-top-inner-gap">
                            <asp:TextBox ID="txtType" CssClass="textBox" runat="server" placeholder="eg. Toys, Watches,..." ToolTip="eg. Watch, Toys, Stamps"></asp:TextBox>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter the type of the product." ValidationGroup="ProductDetails" Text="*" ControlToValidate="txtType"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Brand:</td>
                <td>
                    <div>
                        <div class="medium-top-inner-gap">
                            <asp:TextBox ID="txtBrand" CssClass="textBox" runat="server" ToolTip='Enter "No Brand" if don&apos;t have'></asp:TextBox>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter product brand." Text="*" ValidationGroup="ProductDetails" ControlToValidate="txtBrand"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                </td>
            </tr>
            <tr tooltip='Enter "No Brand" if do'>
                <td class="lbl">Model:</td>
                <td>
                    <div>
                        <div class="medium-top-inner-gap">
                            <asp:TextBox ID="txtModel" CssClass="textBox" runat="server" ToolTip='Enter "No Model" if don&apos;t have'></asp:TextBox>
                        </div>
                        <div>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please enter product model" ControlToValidate="txtModel" Enabled="True" ValidationGroup="ProductDetails" Text="*"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    
                </td>
            </tr>
            <tr>
                <td class="lbl">Photos:</td>
                <td>
                    <div class="medium-top-inner-gap">
                    <div class="flex-row">
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
                         <!--Display upload url-->
                         <div>Paste URL</div>
                         <div class="flex-flow flex-row">
                             <div>
                                 <asp:TextBox ID="txtInsertURL" runat="server" ValidationGroup="urlValidate"></asp:TextBox>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitURL" runat="server" Text="Submit URL" OnClick="btnSubmitURL_Click" ValidationGroup="urlValidate" CausesValidation="True" /> <!--Submit URL and display preview image-->
                            </div>
                         </div>
                    </div>
                     <div id="uploadPhotoContainer" class="displayLess medium-top-inner-gap">
                         <div>Upload Images</div>
                         <div>
                         <!--Display upload file-->
                             <div>
                                 <asp:FileUpload ID="txtUploadPhoto" runat="server" AllowMultiple="True" />
                             </div>
                             <div>
                                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Only .jpg, .png, bitmap image is supported" Text="*" ValidationExpression="/^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpeg|.JPEG|.gif|.GIF|.png|.PNG|.JPG|.jpg|.bitmap|.BITMAP)$/" ControlToValidate="txtUploadPhoto" ValidationGroup="photoValidate" SetFocusOnError="True"></asp:RegularExpressionValidator>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitPhoto" runat="server" Text="Submit Photo" OnClick="btnSubmitPhoto_Click" /> <!--Submit URL and display preview image-->
                            </div>
                        </div>
                     </div>
                    <div class="small-top-gap">
                        <!--display image-->
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" OnDataBinding="UpdatePanel1_DataBinding">
                            <ContentTemplate>
                                <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="3" >
                                    <ItemTemplate>
                                        <div class="flex-row small-top-gap" runat="server">
                                            <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                                <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" Height="100px" />
                                                <asp:Button ID="btnRemoveImg1" cssClass="btn-small-lightgray" Width="110px"  runat="server" Text="Remove" OnClick="btnRemoveImg1_Click" />
                                            </div>
                                            <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                                                <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                                <asp:Image ID="Image2" Width="200px" ImageUrl="~/Seller/ProcessPhoto.ashx" runat="server" Height="100px" />
                                                <asp:Button ID="btnRemoveImg2" cssClass="btn-small-lightgray" Width="110px"  runat="server" Text="Remove" OnClick="btnRemoveImg2_Click" />
                                            </div>
                                            
                                            <asp:HiddenField ID="hfRow" Value='<%# Eval("id") %>' runat="server" />
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
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">SubStore:</td>
                <td>
                    <div class="medium-top-inner-gap">
                            <asp:DropDownList ID="ddlSubStore" CssClass="textBox" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                    </div>
                    <div>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ErrorMessage="CompareValidator" ControlToValidate="ddlSubStore" CultureInvariantValues="False" ClientIDMode="Inherit" ValidationGroup="ProductDetails" ValueToCompare="-1" Operator="NotEqual" Text="*"></asp:CompareValidator>
                    </div>
                </td>
             </tr>
             <tr>
                <td class="lbl">Description:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtDesc" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
        </div>
        <div class="content-subcontainer flex-column">
            <div class="title2-black content-title">Selling Details</div>
            <table style="width: 100%;">
            <tr>
                <td class="lbl">Selling Format:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:CheckBoxList ID="chkSellOption" CssClass="textBox" runat="server" OnSelectedIndexChanged="chkSellOption_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="FixedPrice" Enabled="False" Selected="True">Fixed Price</asp:ListItem>
                            <asp:ListItem Value="OpenBidAuction">Open Bid Auction</asp:ListItem>
                            <asp:ListItem Value="SealedBidAuction">Sealed Bid Auction</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
            </tr>
                </table>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="chkSellOption" EventName="SelectedIndexChanged" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 72%;">      
                    <tr>
                        <td class="lbl">Selling Duration:</td> <!--Duration Purpose -->
                        <td>
                            <div id="durationCon" runat="server">
                                <div class="medium-top-inner-gap">
                                    <asp:DropDownList ID="ddlDuration" CssClass="textBox" runat="server">
                                        <asp:ListItem Value="-1 ">--Select Duration--</asp:ListItem>
                                        <asp:ListItem Value="1 ">1 days</asp:ListItem>
                                        <asp:ListItem Value="3 ">3 days</asp:ListItem>
                                        <asp:ListItem Value="5 ">5 days</asp:ListItem>
                                        <asp:ListItem Value="7 ">7 days</asp:ListItem>
                                        <asp:ListItem Value="10 ">10 days</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Please select a duration" ControlToValidate="ddlDuration" Text="*" ValidationGroup="ProductDetails" Operator="NotEqual" ValueToCompare="-1"></asp:CompareValidator>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl">Fixed Price:</td>
                        <td>
                            <div class="medium-top-inner-gap">
                                 <asp:TextBox ID="txtFixedPrice" CssClass="textBox" runat="server" ToolTip="The selling price which accepted by the seller"></asp:TextBox>
                            </div>
                            <div>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Please enter digit and maximum 1 dot only." Enabled="false" ControlToValidate="txtFixedPrice" ValidationGroup="ProductDetails" Text="*"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Enabled="false" ValidationGroup="ProductDetails" Text="*" ValidationExpression="^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*\.[0-9]{2}$" ErrorMessage="Please enter digit only." ControlToValidate="txtFixedPrice"></asp:RegularExpressionValidator>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl">Starting Price (for auction only):</td>
                        <td>
                            <div>
                                <div class="medium-top-inner-gap">
                                    <asp:TextBox ID="txtStartPrice" CssClass="textBox" runat="server"></asp:TextBox>
                                </div>
                                <div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please enter digit and maximum 1 dot only." Enabled="false" ControlToValidate="txtStartPrice" ValidationGroup="ProductDetails" Text="*"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Enabled="false" ValidationGroup="ProductDetails" Text="*" ValidationExpression="^[+-]?[0-9]{1,3}(?:,?[0-9]{3})*\.[0-9]{2}$" ErrorMessage="Please enter digit only." ControlToValidate="txtStartPrice"></asp:RegularExpressionValidator>
                                </div>
                            </div>
                        </td>
                    </tr>
                     <tr>
                        <td id="reservePriceHeader" class="lbl" style="display:none;" runat="server">Reserve Price:</td>
                        <td>
                            <div id="reservePriceContainer" class="medium-top-inner-gap" style="display:none;" runat="server">
                                <asp:TextBox ID="txtReservePrice" CssClass="textBox" runat="server"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="lbl" style="display:none;">Stock:</td>
                        <td>
                            <div class="medium-top-inner-gap" style="display:none;">
                                <asp:TextBox ID="txtStock" CssClass="textBox" runat="server"></asp:TextBox>
                            </div>
                        </td>
                    </tr>
                        </table>
                        </asp:Panel>
                        </ContentTemplate>
                </asp:UpdatePanel>
        </div>
        <div class="btn-container">
                <asp:Button ID="btnConfirm" CssClass="btn-small-golden" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
        </div>
        <div id="errorMsg" style="display:none" runat="server">
            <asp:Label ID="lblErrorMsg" runat="server" Text="" Visible="false"></asp:Label>
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
