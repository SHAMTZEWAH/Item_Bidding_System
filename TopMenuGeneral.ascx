<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuGeneral.ascx.cs" Inherits="Item_Bidding_System.TopMenuGeneral" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-top flex-subcontainer-top">
            <div>
               <button id="btnLogin" class="btn-medium-lightgray" onclick="location.href='/General/LoginPage.aspx'" type="button">
                   Login
               </button> <!--Url to login page-->
            </div>
            <div>
                <a class="flex-item" href="/General/SignUpPage.aspx">
                    Sign Up
                </a> <!--Url to sign up page-->
            </div>
</div>