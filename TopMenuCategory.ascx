<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuCategory.ascx.cs" Inherits="Item_Bidding_System.TopMenuCategory" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-top-menu3">
            <div id="fashion" class="flex-item-top-menu">
                Fashion Apparel <!--URL to fashion page-->
            </div>
            <div id='art' class="flex-item-top-menu">
                Artworks <!--URL to Artwork page-->
            </div>
            <div id='modern' class="flex-item-top-menu">
                Modern Collectible <!--URL to Modern c page-->
            </div>
            <div id="old" class="flex-item-top-menu">
                Old Collectible <!--URL to Decoration c page-->
            </div>
            <div id="luxury" class="flex-item-top-menu">
                Luxury Items <!--URL to Luxury Items page (might use button) -->
            </div>
 </div>

<script>
             document.getElementById('fashion').addEventListener("click", function () {
                 console.log("Fashion")
                 directProductPage("Fashion Apparel")
             });

             document.getElementById('art').addEventListener("click", function () {
                 directProductPage("Artworks")
             });

             document.getElementById('modern').addEventListener("click", function () {
                 directProductPage("Modern Collectible")
             });

             document.getElementById('old').addEventListener("click", function () {
                 directProductPage("Old Collectible")
             });

             document.getElementById('luxury').addEventListener("click", function () {
                 directProductPage("Luxury Items")
             });

             function directProductPage(page = "Artworks") {
                 category = "";
                 console.log("You have done it right!");
                 if (page === "Fashion Apparel") {
                     category = "Fashion Apparel";
                 }
                 else if (page === "Artworks") {
                     category = "Artworks";
                 }
                 else if (page === "Modern Collectible") {
                     category = "Modern Collectible";
                 }
                 else if (page === "Old Collectible") {
                     category = "Old Collectible";
                 }
                 else if (page === "Luxury Items") {
                     category = "Luxury Items";
                 }
                
                 window.location.href = "/General/Product.aspx?category=" + category; //put aspx page url
             }
</script>