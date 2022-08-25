<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Item_Bidding_System.General.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">
        <div class="title1-black title1-bold">Results:<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProductPhoto.productPhotoURL, ProductDetails.productName, MAX(BidTable.bidPrice) AS maxBid, FixedPriceProduct.productPrice, Product.productStock, MAX(BidTable.bidPrice) AS yourBid FROM ProductPhoto INNER JOIN Product ON ProductPhoto.productId = Product.productId INNER JOIN BidTable ON Product.productId = BidTable.productId INNER JOIN ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN FixedPriceProduct ON Product.productId = FixedPriceProduct.productId INNER JOIN Account ON BidTable.accId = Account.accId INNER JOIN ProductPhoto AS ProductPhoto_1 ON Product.productId = ProductPhoto.productId GROUP BY ProductPhoto.productPhotoURL, ProductDetails.productName, FixedPriceProduct.productPrice, Product.productStock"></asp:SqlDataSource>
        </div>
        <div class="content-subcontainer">
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                <div class="content-subcontainer content-subcontainer-adjust">
                        <div>
                            <div id="productPhoto">
                                <!--display image-->
                                <div class="flex-column small-top-gap" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value) || !Eval("productPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                    <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                        <asp:ImageButton ID="Image1" Width="200px" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" Height="100px" Onclick="Image1_Click"/>
                                    </div>
                                    <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                                    <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                        <asp:ImageButton ID="Image2" ImageUrl='<%# String.Concat("~/User/ProcessPhoto.ashx?prodId=",Eval("productId")) %>' Width="200px" runat="server" Height="100px" OnClick="Image2_Click" />
                                    </div>
                                    <asp:HiddenField ID="hfRowProdId" Value='<%# Eval("productId") %>' runat="server" />
                                 </div>
                                <asp:HiddenField ID="hfRow" Value='<%# Container.ItemIndex %>' runat="server" />
                                <asp:HiddenField ID="hfProductPhotoURL" Value='<%# Eval("productPhotoURL") %>' runat="server" />
                                <asp:HiddenField ID="hfProductPhoto" Value='<%# Eval("productPhoto") %>' runat="server" />
                            </div>
                        </div>
                        <div class="flex-column flex-start">
                            <div>
                                <asp:Label ID="prodName" CssClass="title3-black-bold" runat="server" Text='<%# Eval("productName") %>'/> <!--Text='# Eval("") '-->
                            </div>
                            <div class="flex-row flex-self-end">
                                <div>Current max bid: RM</div>
                                <div>
                                    <asp:Label ID="currentBidPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"maxBid","{0:0.00}") %>' /><!--Text='# Eval("") '-->
                                </div>
                                
                            </div>
                            <div class="flex-row flex-self-end">
                                <div>Buy Now: RM</div>
                                <div>
                                    <asp:Label ID="fixedPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"productPrice","{0:0.00}") %>' /><!--Text='# Eval("") '-->
                                </div>
                            </div>   
                        </div>
                        <div class="flex-column flex-around">
                            <div class="flex-row">
                                <div>Stock: </div>
                                <div>
                                    <asp:Label ID="stock" runat="server" Text='<%# Eval("productStock") %>' /> <!--Text='# Eval("") '-->
                                </div>
                            </div>
                            <div class="flex-row" style="color: blue;display:none;">
                                <div>Your bid: RM</div>
                                <div>
                                    <asp:Label ID="yourBid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"yourBid","{0:0.00}") %>' Visible="false" /> <!--Text='# Eval("") '-->
                                </div>
                            </div>
                           
                        </div>
                        <div class="flex-column flex-around">
                            <button class="btn-medium-blue btnView" onclick="/General/Product.aspx?prodName=">View</button> <!--URL need to add-->
                            <asp:Button ID="btnAddCart" CssClass="btn-medium-golden btnAddCart" runat="server" Text="Add to cart" />
                        </div>
                </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>

        </div> 
    </div>
</asp:Content>