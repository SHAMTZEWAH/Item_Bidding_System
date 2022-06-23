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
                        <asp:TextBox ID="TextBox1" CssClass="textBox" runat="server"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox2" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Model:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="TextBox3" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">SKU:</td>
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:TextBox ID="TextBox5" CssClass="textBox" runat="server"></asp:TextBox>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="lbl">Photos:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:FileUpload ID="FileUpload1" runat="server" />
                    </div>
                </td>
            </tr>
             <tr>
                <td class="lbl">Description:</td>
                <td>
                     <div class="medium-top-inner-gap">
                         <asp:TextBox ID="TextBox6" CssClass="textBox" runat="server"></asp:TextBox>
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
                        <asp:CheckBoxList ID="CheckBoxList1" CssClass="textBox" runat="server">
                            <asp:ListItem>Fixed Price</asp:ListItem>
                            <asp:ListItem>Open Bid Auction</asp:ListItem>
                            <asp:ListItem>Sealed Bid Auction</asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                </td>
                
            </tr>
            <tr>
                <td class="lbl">Selling End Date:</td> <!--Duration Purpose -->
                <td>
                    <div class="medium-top-inner-gap">
                        <asp:DropDownList ID="DropDownList1" CssClass="textBox" runat="server">
                            <asp:ListItem>1 days</asp:ListItem>
                            <asp:ListItem>3 days</asp:ListItem>
                            <asp:ListItem>5 days</asp:ListItem>
                            <asp:ListItem>7 days</asp:ListItem>
                            <asp:ListItem>10 days</asp:ListItem>
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
    