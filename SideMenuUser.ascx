<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuUser.ascx.cs" Inherits="Item_Bidding_System.SideMenuUser" %>
<link type="text/css" rel="stylesheet" href="SideMenu.css" />

<!--class="side-menu-admin-body"-->
<nav class="side-menu-body">
    <div class="side-menu-container">

        <div class="side-menu-item menu-top side-menu-top">
            <img id="profilePic" src=""/> <!--Url for profile pic-->
            <div id="username">Username</div>
        </div>

        <div id="acc" class="side-menu-item">
            <a class="side-menu" href="">My Account</a> <!--Url to manage acc role page-->
        </div>

        <div id="accSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Profile</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Payment Cards</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Payment Cards</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Addresses</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Change Password</a>
            </div>
        </div>

        <div class="side-menu-item">
            <a class="side-menu" href="">My Purchase</a> <!--Url to monitor product page-->
        </div>

        <div id="watchlist" class="side-menu-item">
            <a class="side-menu" href="">My Watchlist</a> <!--Url to monitor product page-->
        </div>

        <div id="watchlistSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu" href="">My Watchlist</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">My Bid</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Watched History</a>
            </div>
        </div>

        <div id="voucher" class="side-menu-item">
            <a class="side-menu" href="">My Vouchers</a> <!--Url to monitor complaint page-->
        </div>

        <div id="voucherSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Store Voucher</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu" href="">Site Voucher</a>
            </div>
        </div>

        <div class="side-menu-item">
            <a class="side-menu" href="">Seller Registration</a> <!--Url to monitor complaint page-->
        </div>

    </div>
</nav>

<script>
    document.getElementById("acc").addEventListener("click", function () {
        elem1 = document.getElementById("accSub")
        if (window.getComputedStyle(elem1).display === "none") {
            elem1.style.display = "flex";
        }
        else {
            elem1.style.display = "none";
        }
        
    });

    document.getElementById("watchlist").addEventListener("click", function () {
        elem2 = document.getElementById("watchlistSub")
        if (window.getComputedStyle(elem2).display === "none") {
            elem2.style.display = "flex";
        }
        else {
            elem2.style.display = "none";
        }

    });

    document.getElementById("voucher").addEventListener("click", function () {
        elem3 = document.getElementById("voucherSub")
        if (window.getComputedStyle(elem3).display === "none") {
            elem3.style.display = "flex";
        }
        else {
            elem3.style.display = "none";
        }

    });
</script>