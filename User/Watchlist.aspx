<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Watchlist.aspx.cs" Inherits="Item_Bidding_System.User.Watchlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
     <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Watchlist:</div>
            <div class="filter-option">
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
            </div>
            <div id="radioContainer" class="filter-content">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Pending Orders</asp:ListItem>
                    <asp:ListItem>ToShipped</asp:ListItem>
                    <asp:ListItem>Product Received</asp:ListItem>
                </asp:RadioButtonList>
            </div>
         </div>
        <div>
        <div class="content-subcontainer">
            <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
                <ItemTemplate>
                <div class="content-subcontainer content-subcontainer-adjust" >
                        <div>
                            <img class="medium-image" alt="" runat="server" src='<%# Eval("productPhotoURL") %>' /> <!--src="# Eval("") "-->
                        </div>
                        <div class="flex-column flex-start">
                            <div>
                                <asp:Label ID="prodName" CssClass="title3-black-bold" runat="server" Text='<%# Eval("productName") %>'/> <!--Text='# Eval("") '-->
                            </div>
                            <div class="flex-row flex-self-end">
                                <div>Current max bid: RM</div>
                                <div>
                                    <asp:Label ID="currentBidPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"maxBid","{0:0.00}") %>' /><!--Text='# Eval("") '-->
                                </div>
                                
                            </div>
                            <div class="flex-row flex-self-end">
                                <div>Buy Now: RM</div>
                                <div>
                                    <asp:Label ID="fixedPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"productPrice","{0:0.00}") %>' /><!--Text='# Eval("") '-->
                                </div>
                            </div>   
                        </div>
                        <div class="flex-column flex-around">
                            <div class="flex-row">
                                <div>Stock: </div>
                                <div>
                                    <asp:Label ID="stock" runat="server" Text='<%# Eval("productStock") %>' /> <!--Text='# Eval("") '-->
                                </div>
                            </div>
                            <div class="flex-row" style="color: blue;display:none;">
                                <div>Your bid: RM</div>
                                <div>
                                    <asp:Label ID="yourBid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"yourBid","{0:0.00}") %>' Visible="false" /> <!--Text='# Eval("") '-->
                                </div>
                            </div>
                           
                        </div>
                        <div class="flex-column flex-around">
                            <button class="btn-medium-blue btnView" onclick="/General/Product.aspx?prodName=">View</button> <!--URL need to add-->
                            <asp:Button ID="btnAddCart" CssClass="btn-medium-golden btnAddCart" runat="server" Text="Add to cart" />
                            <asp:Button ID="btnRemove" runat="server" CssClass="btn-medium-red btnRemove" Text="Remove" />
                        </div>
                </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ItemBidDB.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM"></asp:SqlDataSource>
        </div>
  
        </div>
    </div>
</asp:Content>

