﻿<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Item_Bidding_System.General.HomePage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="SlideShow.css" />


<div class="week-new-text">Today's New</div>
    
<div class="slideshow-big-container"> <!--include dot and button -->
    <div class="slideshow-container" style="border:none;"> <!--exclude dot-->
        <div style="align-self: flex-end;">
            <a href="Product.aspx?selection=NewlyAdded&keyword=">View More</a>
        </div>
        <div id="slideSubcontainer" class="slide-only-container" style="border:1px solid black;"> <!--exclude previous, next button-->
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                    <!-- Full-width images with number and price, name -->
                    <div id="productContainer" class="mySlide slide-only-subcontainer fade" runat="server" CommandName="select" Onclick="productContainer_Click">
                        <div class="">
                            <%--<asp:ImageButton ID="ImageButton1" CssClass="slideshow-image" runat="server" ImageUrl='<%# Eval("productPhotoURL") %>' CommandName="select" />--%>
                            <div class="flex-row small-top-gap" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value) || !Eval("productPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                <div id="imgCon1" class="" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                    <asp:ImageButton ID="Image1" CssClass="slideshow-image" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" CommandName="select" Onclick="Image1_Click"/>
                                </div>
                                <div id="imgCon2" class="" runat="server" visible='
                                <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                    <asp:ImageButton ID="Image2" CssClass="slideshow-image" ImageUrl='<%# String.Concat("~/General/ProcessPhoto.ashx?photoId=",Eval("productPhotoId")) %>' runat="server" CommandName="select" OnClick="Image2_Click"/>
                                </div>
                                <asp:HiddenField ID="hfRowProdId" Value='<%# Eval("productId") %>' runat="server" />
                                <asp:HiddenField ID="hfRow" Value='<%# Eval("productPhotoId") %>' runat="server" />
                            </div>
                        </div>
                        <div class="">
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                        </div>
                        <div class="flex-row"> 
                            <div>Buy Now:RM</div>
                            <div>
                                <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"productPrice","{0:0.00}") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="flex-row"> <!--If it is auction-->
                            <div>Current Bid:RM</div>
                            <div>
                                <!--<asp:Label ID="lblMaxBid" runat="server" Text= 'DataBinder.Eval(Container.DataItem,"maxBid","{0:0.00}") %>'> </asp:Label>-->
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProductPhoto.productPhotoId, ProductPhoto.productPhotoURL, ProductPhoto.productPhoto, Product.productId, ProductDetails.productName, FixedPriceProduct.productPrice, OrderProduct.quantity AS CountSales 
FROM ProductPhoto INNER JOIN 
FixedPriceProduct ON ProductPhoto.productId = FixedPriceProduct.productId INNER JOIN 
Product ON ProductPhoto.productId = Product.productId AND FixedPriceProduct.productId = Product.productId INNER JOIN 
ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId FULL JOIN 
OrderProduct ON Product.productId = OrderProduct.productId 
WHERE ProductPhoto.photoStatus = 'Main' AND Product.productStatus = 'Unflagged'
ORDER BY CountSales
">
            </asp:SqlDataSource>
        </div>

      <!-- Next and previous buttons -->
      <a class="prev" onclick="move(-1,0)">&#10094;</a>
      <a class="next" onclick="move(1,0)">&#10095;</a>
    </div>
    <br>
</div>

<!--Slideshow 2-->
<div class="week-new-text">Today's Hot</div>
    
<div class="slideshow-big-container2"> <!--include dot and button -->
    <div class="slideshow-container2" style="border:none;"> <!--exclude dot-->
        <div style="align-self: flex-end;">
            <a href="Product.aspx?selection=Hots&keyword=">View More</a>
        </div>
        <div class="slide-only-container2" style="border:1px solid black;"> <!--exclude previous, next button-->
            <asp:Repeater ID="Repeater2" runat="server" OnItemDataBound="Repeater2_ItemDataBound" OnItemCommand="Repeater2_ItemCommand" DataSourceID="SqlDataSource2">
                <ItemTemplate>
                    <!-- Full-width images with number and price, name -->
                    <div id="productContainer2" class="mySlide slide-only-subcontainer2 fade" runat="server" CommandName="select" Onclick="productContainer2_Click">
                        <div class="">
<%--                            <asp:ImageButton ID="ImageButton2" CssClass="slideshow-image" runat="server" ImageUrl='<%# Eval("productPhotoURL") %>' CommandName="select" />--%>
                            <div class="flex-row small-top-gap" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value) || !Eval("productPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                <div id="imgCon1_1" class="" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                    <asp:ImageButton ID="Image1_1" CssClass="slideshow-image" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" CommandName="select" OnClick="Image1_1_Click"/>
                                </div>
                                <div id="imgCon2_1" class="" runat="server" visible='
                                <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                    <asp:ImageButton ID="Image2_1" CssClass="slideshow-image" ImageUrl='<%# String.Concat("~/General/ProcessPhoto.ashx?photoId=",Eval("productPhotoId")) %>' runat="server" CommandName="select" OnClick="Image2_1_Click"/>
                                </div>
                                <asp:HiddenField ID="hfRowProdId" Value='<%# Eval("productId") %>' runat="server" />
                                <asp:HiddenField ID="hfRow" Value='<%# Eval("productPhotoId") %>' runat="server" />
                            </div>
                        </div>
                        <div class="">
                            <asp:Label ID="lblName2" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                        </div>
                        <div class="flex-row"> 
                            <div>Buy Now:RM</div>
                            <div>
                                <asp:Label ID="lblPrice2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"productPrice","{0:0.00}") %>'></asp:Label>
                            </div>
                        </div>
                        <div class="flex-row"> <!--If it is auction-->
                            <div>Current Bid:RM</div>
                            <div>
                                <!--<asp:Label ID="lblMaxBid2" runat="server" Text= 'DataBinder.Eval(Container.DataItem,"maxBid","{0:0.00}") %>'> </asp:Label>-->
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoData2" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProductPhoto.productPhotoId, ProductPhoto.productPhotoURL, ProductPhoto.productPhoto, Product.productId, ProductDetails.productName, FixedPriceProduct.productPrice, OrderProduct.quantity AS CountSales 
FROM ProductPhoto INNER JOIN 
FixedPriceProduct ON ProductPhoto.productId = FixedPriceProduct.productId INNER JOIN 
Product ON ProductPhoto.productId = Product.productId AND FixedPriceProduct.productId = Product.productId INNER JOIN 
ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId FULL JOIN 
OrderProduct ON Product.productId = OrderProduct.productId 
WHERE ProductPhoto.photoStatus = 'Main' AND Product.productStatus = 'Unflagged'
ORDER BY Product.addDateTime DESC

">
            </asp:SqlDataSource>
        </div>

      <!-- Next and previous buttons -->
      <a class="prev" onclick="move(-1,1)">&#10094;</a>
      <a class="next" onclick="move(1,1)">&#10095;</a>
    </div>
    <br>
</div>

<script>

    //function for animation
    function move(value, slideshowNo) {

        var slideshowArr = ["slide-only-container", "slide-only-container2"]
        var prodArr = ["slide-only-subcontainer", "slide-only-subcontainer2"]
        var con = document.getElementsByClassName(slideshowArr[slideshowNo])[0];
        var prodCon = document.getElementsByClassName(prodArr[slideshowNo])[0];
        var prodStyle = getComputedStyle(prodCon);

        //get content width
        var prodWidth = prodStyle.width;
        var prodWidthNum = parseInt(prodWidth);

        //get content margin right
        var prodMarginRight = prodStyle.marginRight;
        var prodMarginRightNum = parseInt(prodMarginRight);

        //get total width
        var totalWidth = prodWidthNum + prodMarginRightNum
        totalWidth = totalWidth * 3 * value;

        //alert(prodWidthNum + " + " + prodMarginRightNum + " = " + totalWidth);

        window.setInterval(
            con.scrollTop += totalWidth, 10
        ); 
    }

    <%--function clickDiv() {
        if (__doPostBack) {
            __doPostBack('<%=btnHideToGo.UniqueID %>', '');
        }
        else {
            var theForm = document.forms['aspnetForm'];
            if (!theForm) {
                theForm = document.aspnetForm;
            }
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                    theForm.__EVENTTARGET.value = '<%=btnHideToGo.UniqueID %>';
                    theForm.__EVENTARGUMENT.value = '';
                    theForm.submit();
                }
            }
    }--%>
</script>
</asp:Content>
