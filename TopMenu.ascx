<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="Item_Bidding_System.TopMenu" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />

<div class="flex-container-login">
            <div>
               <button id="btnWatchlist" class="btn-medium-lightgray btn-watchlist" onclick="/User/Watchlist.aspx">Watchlist
                   <i class="bi bi-bookmark-fill small-left-inner-gap"></i>
               </button>
            </div>
            <div class="larger-right-gap">
                <asp:LoginStatus ID="LoginStatus1" runat="server" />
            </div>
</div>

<!--
<script>
    document.getElementById("btnWatchlist").addEventListener("click", function () {
        window.location.href = '';
    });
</script>-->