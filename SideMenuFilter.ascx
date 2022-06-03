<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuFilter.ascx.cs" Inherits="Item_Bidding_System.SideMenuFilter" %>

<style>
    .filter-container{
        display:flex;
        flex-flow: column wrap;
        justify-content: flex-start;
        align-items: flex-start;
        width:200px;
        height: auto;
    }
    .filter-title{
        font-size: 20px;
        text-decoration:underline;
        text-align:center;
        align-self: center;
    }
    .filter-subtitle{
        font-weight:bold;
        margin-bottom:10px;
    }
    .filter-category-container, .filter-price-container, .filter-state-container, .selling-option-container{
        padding: 10px 0px;
        border-bottom: 1px solid lightgray;
    }
    .txtPrice{
        width:50px;
    }
    .filter-price-subcontainer{
        display:flex;
        flex-flow:row wrap;
        justify-content: space-between;
        align-items: center;
        margin: 19px 0px 15px 0px;
    }
    .line-price-range{
        height: 1px;
        width: 20px;
        background-color:gray;
        margin: 0px 5px;
    }
    .filterCategory{
        height:200px;
    }
    .filterState{
        height: 608px;
    }
    .sellingOption{
        height: 114px;
    }
</style>

<div class="filter-container">
    <div class="filter-title">Filter</div>
    <div class="filter-category-container">
        <div class="filter-subtitle">Category:</div>
        <asp:CheckBoxList CssClass="filterCategory" ID="chkBoxCategory" runat="server">
            <asp:ListItem>Fashion Apparel</asp:ListItem>
            <asp:ListItem>Artworks</asp:ListItem>
            <asp:ListItem>Modern Collectible</asp:ListItem>
            <asp:ListItem>Old Collectible</asp:ListItem>
            <asp:ListItem>Luxury Item</asp:ListItem>

        </asp:CheckBoxList>
    </div>
    <div class="filter-price-container">
        <div class="filter-subtitle">Price Range:</div>
        <div class="filter-price-subcontainer">
            <div>
            <asp:TextBox ID="txtMinPrice" CssClass="txtPrice" placeholder="RM" runat="server"></asp:TextBox>
            </div>
            <div class="line-price-range"></div>
            <div>
                <asp:TextBox ID="txtMaxPrice" CssClass="txtPrice" placeholder="RM" runat="server"></asp:TextBox>
            </div>
        </div>
        
    </div>
    <div class="filter-state-container">
        <div class="filter-subtitle">States:</div>
        <asp:CheckBoxList CssClass="filterState" ID="chkBoxState" runat="server">
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
    <div class="selling-option-container">
        <div class="filter-subtitle">Selling Options:</div>
        <asp:CheckBoxList CssClass="sellingOption" ID="chkBoxSellOption" runat="server">
            <asp:ListItem>Fixed price</asp:ListItem>
            <asp:ListItem>Open Bid Auction</asp:ListItem>
            <asp:ListItem>Sealed bid auction</asp:ListItem>
        </asp:CheckBoxList>
    </div>
</div>