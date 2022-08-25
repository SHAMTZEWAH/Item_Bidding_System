<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenuSeller.ascx.cs" Inherits="Item_Bidding_System.TopMenuSeller" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-login">
            <div>
               <button id="btnWatchlist" class="btn-medium-lightgray btn-watchlist" onclick="/Seller/ManageOrder.aspx">Notification
                   <i class="bi bi-bell-fill small-left-inner-gap"></i>
               </button>
            </div>
            <div class="larger-right-gap">
                <asp:LoginStatus ID="LoginStatus1" runat="server" />
            </div>
</div>