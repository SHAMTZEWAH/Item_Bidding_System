<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuUser.ascx.cs" Inherits="Item_Bidding_System.SideMenuUser" %>
<link type="text/css" rel="stylesheet" href="SideMenu.css" />

<!--class="side-menu-admin-body"-->
<nav class="side-menu-body">
    <div class="side-menu-container">

        <div class="side-menu-item menu-top side-menu-top">
            <asp:DataList ID="DataList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="1" >
                <ItemTemplate>
                    <div class="flex-row small-top-gap" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value) || !Eval("accPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                        <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("accPhotoURL").Equals(DBNull.Value)?true:false %>'>
                            <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("accPhotoURL") %>' runat="server" Height="100px" />
                        </div>
                        <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                        <%# Convert.ToBase64String((byte[])Eval("accPhoto")).Trim() != string.Empty ?true:false %>'>
                            <asp:Image ID="Image2" ImageUrl='<%# String.Concat("~/User/ProcessPhoto.ashx?accId=",Eval("accId")) %>' Width="200px" runat="server" Height="100px" />
                        </div>
                        <asp:HiddenField ID="hfRowAccId" Value='<%# Eval("accId") %>' runat="server" />
                    </div>
                    <div id="username">
                        <asp:Label ID="lblUsername" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                    </div>
                 </ItemTemplate>
            </asp:DataList>
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