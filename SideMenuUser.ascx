<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuUser.ascx.cs" Inherits="Item_Bidding_System.SideMenuUser" %>

<style>
    nav.side-menu-seller-body{
        border: 3px solid black;
        width:200px;
        height: 1100px;
        position:relative;
    }
    div.side-menu-seller-container{
        list-style-type: none;
        display:flex;
        flex-flow: column wrap;
        align-items: center;
        justify-content: flex-start;
        align-content: center;
        height: 75%;
        padding-left:0px;
    }
    div.side-menu-item{
        width:100%;
        border-bottom:1px solid lightgray;
        text-align:center;
        padding: 20px 0;
    }
    div.menu-top{
        padding: 30px 0;
        font-family:Georgia;
        font-weight: bold;
    }
    a.side-menu-seller{
        color: black;
        text-decoration: none;
        margin: 0px;
    }
    div.side-menu-item:hover{
        background-color: aliceblue;
        cursor:pointer;
    }
    div.menu-top:hover{
        background-color: white;
        cursor:default;
    }
    #profilePic{
        height:50px;
        width:50px;
    }
    #username{
        margin-left:10px;
    }
    .side-menu-top{
        display:flex;
        flex-flow: row wrap;
        justify-content:center;
        align-items: center;
    }
    .side-menu-subcontainer{
        border-bottom: 1px solid lightgray;
        background-color: lightgray;
        padding: 0 0 !important;
        display:none;
        flex-flow:column wrap;
        justify-content:center;
        align-content:flex-start;
    }
    .side-menu-subcontainer:hover{
        background-color: lightgray !important;
        cursor:default !important;
    }
    .side-menu-subitem{
        width: 80%;
        margin-left:20%;
        margin-top:0;
        padding:10px 0;
        border-bottom:1px solid lightgray;
    }
    .side-menu-subitem:hover{
        background-color: aliceblue;
        cursor:pointer;
    }
</style>
<!--class="side-menu-admin-body"-->
<nav class="side-menu-seller-body">
    <div class="side-menu-seller-container">

        <div class="side-menu-item menu-top side-menu-top">
            <img id="profilePic" src=""/> <!--Url for profile pic-->
            <div id="username">Username</div>
        </div>

        <div id="acc" class="side-menu-item">
            <a class="side-menu-seller" href="">My Account</a> <!--Url to manage acc role page-->
        </div>

        <div id="accSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Profile</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Payment Cards</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Payment Cards</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Addresses</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Change Password</a>
            </div>
        </div>

        <div class="side-menu-item">
            <a class="side-menu-seller" href="">My Purchase</a> <!--Url to monitor product page-->
        </div>

        <div id="watchlist" class="side-menu-item">
            <a class="side-menu-seller" href="">My Watchlist</a> <!--Url to monitor product page-->
        </div>

        <div id="watchlistSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">My Watchlist</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">My Bid</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Watched History</a>
            </div>
        </div>

        <div id="voucher" class="side-menu-item">
            <a class="side-menu-seller" href="">My Vouchers</a> <!--Url to monitor complaint page-->
        </div>

        <div id="voucherSub" class="side-menu-item side-menu-subcontainer">
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Store Voucher</a>
            </div>
            <div class="side-menu-subitem">
                <a class="side-menu-seller" href="">Site Voucher</a>
            </div>
        </div>

        <div class="side-menu-item">
            <a class="side-menu-seller" href="">Seller Registration</a> <!--Url to monitor complaint page-->
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