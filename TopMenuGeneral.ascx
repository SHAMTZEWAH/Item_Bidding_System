<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuGeneral.ascx.cs" Inherits="Item_Bidding_System.TopMenuGeneral" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-top-menu top-container">
            <div class="flex-item-top-menu">
               <button id="btnLogin" onclick="location.href='/General/LoginPage.aspx'" type="button">Login</button> <!--Url to login page-->
            </div>
            <div class="flex-item-top-menu">
                <a class="flex-item" href="/General/SignUpPage.aspx">Sign Up</a> <!--Url to sign up page-->
            </div>
</div>