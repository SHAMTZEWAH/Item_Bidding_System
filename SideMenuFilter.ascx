<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuFilter.ascx.cs" Inherits="Item_Bidding_System.SideMenuFilter" %>
<link type="text/css" rel="stylesheet" href="MasterCSS.css" />
<link type="text/css" rel="stylesheet" href="SideMenu.css" />


<div class="filter-container side-menu-body">
    <div class="filter-title">Filter</div>
    <div class="filter-subcontainer">
        <div class="filter-subtitle">Selection:</div>
        <asp:RadioButtonList ID="radioSelect" CssClass="filterSelect" runat="server" OnSelectedIndexChanged="radioSelect_SelectedIndexChanged" AutoPostBack="True" ViewStateMode="Enabled">
            <asp:ListItem>Hots</asp:ListItem>
            <asp:ListItem>Newly Added</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div class="filter-subcontainer">
        <div class="filter-subtitle">Category:</div>
        <asp:CheckBoxList CssClass="filterCategory" ID="chkBoxCategory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkBoxCategory_SelectedIndexChanged" ViewStateMode="Enabled">
            <asp:ListItem>Fashion Apparel</asp:ListItem>
            <asp:ListItem>Artworks</asp:ListItem>
            <asp:ListItem>Modern Collectible</asp:ListItem>
            <asp:ListItem>Old Collectible</asp:ListItem>
            <asp:ListItem>Luxury Item</asp:ListItem>

        </asp:CheckBoxList>
    </div>
    <div class="filter-subcontainer">
        <div class="filter-subtitle">Price Range:</div>
        <div class="filter-price-subcontainer">
            <div>
            <asp:TextBox ID="txtMinPrice" CssClass="txtPrice" placeholder="RM" runat="server" OnTextChanged="txtMinPrice_TextChanged"></asp:TextBox>
            </div>
            <div class="line-price-range"></div>
            <div>
                <asp:TextBox ID="txtMaxPrice" CssClass="txtPrice" placeholder="RM" runat="server" OnTextChanged="txtMaxPrice_TextChanged"></asp:TextBox>
            </div>
        </div>
        
    </div>
    <div class="filter-subcontainer">
        <div class="filter-subtitle">States:</div>
        <asp:CheckBoxList CssClass="filterState" ID="chkBoxState" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkBoxState_SelectedIndexChanged">
            <asp:ListItem>Johor</asp:ListItem>
            <asp:ListItem>Kedah</asp:ListItem>
            <asp:ListItem>Kelantan</asp:ListItem>
            <asp:ListItem>Melaka</asp:ListItem>
            <asp:ListItem>Negeri Sembilan</asp:ListItem>
            <asp:ListItem>Pahang</asp:ListItem>
            <asp:ListItem>Penang</asp:ListItem>
            <asp:ListItem>Perak</asp:ListItem>
            <asp:ListItem>Perlis</asp:ListItem>
            <asp:ListItem>Sabah</asp:ListItem>
            <asp:ListItem>Sarawak</asp:ListItem>
            <asp:ListItem>Selangor</asp:ListItem>
            <asp:ListItem>Terengganu</asp:ListItem>
            <asp:ListItem>Kuala Lumpur</asp:ListItem>
            <asp:ListItem>Labuan</asp:ListItem>
            <asp:ListItem>Putrajaya</asp:ListItem>

        </asp:CheckBoxList>
    </div>
    <div class="filter-subcontainer">
        <div class="filter-subtitle">Selling Options:</div>
        <asp:CheckBoxList CssClass="sellingOption" ID="chkBoxSellOption" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chkBoxSellOption_SelectedIndexChanged">
            <asp:ListItem>Fixed price</asp:ListItem>
            <asp:ListItem>Open Bid Auction</asp:ListItem>
            <asp:ListItem>Sealed bid auction</asp:ListItem>
        </asp:CheckBoxList>
    </div>
</div>