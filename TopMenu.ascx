<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="Item_Bidding_System.TopMenu" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-top-menu">
            <div class="flex-item-top-menu">
               <button class="btn-watchlist" onclick="~/User/Watchlist.aspx">Watchlist
                   <i class="bi bi-bookmark-fill"></i>
               </button>
            </div>
            <div class="flex-item-top-menu login-status-container">
                <asp:LoginStatus ID="LoginStatus1" runat="server" />
            </div>
</div>