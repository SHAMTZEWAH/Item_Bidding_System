 <%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditProductDetails.ascx.cs" Inherits="Item_Bidding_System.Seller.EditProductDetails" %>

<link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
<link type="text/css" rel="stylesheet" href="../Content.css" />


         <div id="Title" class="title2-black-bold content-title" runat="server">Upload Product</div>
        <div>
            <asp:Label ID="lblMessage" CssClass="filter-content" runat="server" Text="*"></asp:Label></div>
        <div class="content-subcontainer">
            <div class="title2-black content-title">Product Details</div>
            <table style="width: 100%;">
            <tr>
                <td class="lbl">Product Name:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtProdName" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Category:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="ddlProdCategory" CssClass="textBox" runat="server">
                            <asp:ListItem>--Select Category--</asp:ListItem>
                            <asp:ListItem>Fashion Apparel</asp:ListItem>
                            <asp:ListItem>Artworks</asp:ListItem>
                            <asp:ListItem>Modern Collectible</asp:ListItem>
                            <asp:ListItem>Old Collectible</asp:ListItem>
                            <asp:ListItem>Luxury Item</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Type:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtType" CssClass="textBox" runat="server" placeholder="eg. Toys, Watches,..."></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Brand:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtBrand" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Model:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtModel" CssClass="textBox" runat="server"></asp:TextBox>
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
                                 <asp:TextBox ID="txtInsertURL" runat="server"></asp:TextBox>
                             </div>
                            <div>
                                <asp:Button ID="btnSubmitURL" runat="server" Text="Submit URL" /> <!--Submit URL and display preview image-->
                            </div>
                         </div>
                    </div>
                     <div id="uploadPhotoContainer" class="displayLess medium-top-inner-gap">
                         <div>Upload Images</div>
                         <div>
                         <!--Display upload file-->
                             <div>
                                 <asp:FileUpload ID="txtUploadPhoto" runat="server" />
                             </div>
                            <div>
                                <asp:Button ID="Button1" runat="server" Text="Submit Photo" /> <!--Submit URL and display preview image-->
                            </div>
                        </div>
                     </div>
                    
                    <div>
                        <!--display image-->
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
                                <div>
                                    <img src='Eval("productPhotoURL")' class="small-image" alt="URL" runat="server"/>
                                    <img src='Eval("productPhoto")' class="small-image" alt="image" runat="server"/>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
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
        <div class="content-subcontainer">
            <div class="title2-black content-title">Selling Details</div>
            <table style="width: 100%;">
            <tr>
                <td class="lbl">Selling Format:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:CheckBoxList ID="chkSellOption" CssClass="textBox" runat="server">
                            <asp:ListItem>Fixed Price</asp:ListItem>
                            <asp:ListItem>Open Bid Auction</asp:ListItem>
                            <asp:ListItem>Sealed Bid Auction</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Selling Duration:</td> <!--Duration Purpose -->
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="ddlDuration" CssClass="textBox" runat="server">
                            <asp:ListItem Value="1 ">1 days</asp:ListItem>
                            <asp:ListItem Value="3 ">3 days</asp:ListItem>
                            <asp:ListItem Value="5 ">5 days</asp:ListItem>
                            <asp:ListItem Value="7 ">7 days</asp:ListItem>
                            <asp:ListItem Value="10 ">10 days</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Fixed Price:</td>
                <td>
                    <div class="medium-top-inner-gap">
                         <asp:TextBox ID="txtFixedPrice" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Starting Price (for auction only):</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtStartPrice" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Stock:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="txtStock" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
        </table>
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
    