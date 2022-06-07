<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuCategory.ascx.cs" Inherits="Item_Bidding_System.TopMenuCategory" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-category flex-subcontainer-top">
            <div id="fashion" class="flex-item-category">
                Fashion Apparel <!--URL to fashion page-->
            </div>
            <div id='art' class="flex-item-category">
                Artworks <!--URL to Artwork page-->
            </div>
            <div id='modern' class="flex-item-category">
                Modern Collectible <!--URL to Modern c page-->
            </div>
            <div id="old" class="flex-item-category">
                Old Collectible <!--URL to Decoration c page-->
            </div>
            <div id="luxury" class="flex-item-category">
                Luxury Items <!--URL to Luxury Items page (might use button) -->
            </div>
 </div>

<script>
             document.getElementById('fashion').addEventListener("click", function () {
                 window.location.href = "/General/Product.aspx?category=Fashion Apparel";
             });

             document.getElementById('art').addEventListener("click", function () {
                 window.location.href = "/General/Product.aspx?category=Artworks";
             });

             document.getElementById('modern').addEventListener("click", function () {
                 window.location.href = "/General/Product.aspx?category=Modern Collectible";
             });

             document.getElementById('old').addEventListener("click", function () {
                 window.location.href = "/General/Product.aspx?category=Old Collectible";
             });

             document.getElementById('luxury').addEventListener("click", function () {
                 window.location.href = "/General/Product.aspx?category=Luxury Items";
             });

             
</script>