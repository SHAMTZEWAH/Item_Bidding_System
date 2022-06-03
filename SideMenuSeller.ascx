<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuSeller.ascx.cs" Inherits="Item_Bidding_System.SideMenuSeller" %>

<style>
    nav.side-menu-seller-body{
        position:relative;
        border: 3px solid black;
        width:200px;
        height: 1100px;
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
        border-bottom:1px solid gray;
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
</style>
<!--class="side-menu-admin-body"-->
<nav class="side-menu-seller-body">
    <div class="side-menu-seller-container">
        <div class="side-menu-item menu-top">
            Seller Hub
        </div>
        <div class="side-menu-item">
            <a class="side-menu-seller" href="">Manage Order</a> <!--Url to manage acc role page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu-seller" href="">Manage Product</a> <!--Url to monitor product page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu-seller" href="">Sales Summary</a> <!--Url to monitor product page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu-seller" href="">My Finance</a> <!--Url to monitor complaint page-->
        </div>
    </div>
</nav>
