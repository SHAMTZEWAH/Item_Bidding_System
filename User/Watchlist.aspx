<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Watchlist.aspx.cs" Inherits="Item_Bidding_System.User.Watchlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
     <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">

        <div class="title1-black title1-bold">Watchlist:</div>

        <div class="btn-medium-white"></div>

        <div class="content-subcontainer">
            <div class="title1-black title1-bold">Results:</div>
        <div class="content-subcontainer">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
                <div class="content-subcontainer" >
                        <div>
                            <img class="medium-image" alt=""  /> <!--src="# Eval("") "-->
                        </div>
                        <div>
                            <asp:Label ID="prodName" runat="server"  /> <!--Text='# Eval("") '-->
                            <asp:Label ID="currentBidPrice" runat="server"  /><!--Text='# Eval("") '-->
                            <asp:Label ID="fixedPrice" runat="server"  /><!--Text='# Eval("") '-->
                        </div>
                        <div>
                            <asp:Label ID="stock" runat="server"  /> <!--Text='# Eval("") '-->
                            <asp:Label ID="yourBid" runat="server"  /> <!--Text='# Eval("") '-->
                        </div>
                        <div>
                            <button class="btn-medium-blue" onclick="/General/Product.aspx?prodName=">View</button> <!--URL need to add-->
                            <asp:Button ID="btnAddCart" CssClass="btn-medium-golden" runat="server" Text="Add to cart" />
                        </div>
                </div>
                </ItemTemplate>
            </asp:Repeater>

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ItemBidDB.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM"></asp:SqlDataSource>
        </div>
            
  
        </div>
    </div>
</asp:Content>

