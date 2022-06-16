<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuGeneral.ascx.cs" Inherits="Item_Bidding_System.TopMenuGeneral" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-login">
            <div>
               <button id="btnLogin" class="btn-medium-lightgray" onclick="redirectToNextPage()" type="button">
                   Login
               </button> <!--Url to login page-->
            </div>
            <div>
                <a class="flex-item" href="/General/SignUpPage.aspx">
                    Sign Up
                </a> <!--Url to sign up page-->
            </div>
</div>

<script>
    function redirectToNextPage() {
        window.location.href = '/General/LoginPage.aspx?ReturnURL=' + window.location.href;
    }
</script>