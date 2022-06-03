<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Product.aspx.cs" Inherits="Item_Bidding_System.General.Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <style>
        .product-container{
            border: 1px solid black;
        }
        .content-title{
            font-size: 20px;
        }
    </style>
    <div class="product-container">
        <div class="content-title">Results:</div>
        <div class="product">
            <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                <ItemTemplate>
            <div style="border: solid 1px black; width: 95%; margin: 5px; height:fit-content; padding: 5px;">
                <img alt="" src="<%# Eval("artURL") %>" style="float: left; width: 70px; height:90px; padding-right: 10px;"/>
                
                <asp:Label ID="prodName" runat="server" Text='<%# Eval("") %>' />
                <br />
                
                <asp:Label ID="currentBidPrice" runat="server" Text='<%# Eval("") %>' />
                <br />
                
                <asp:Label ID="fixedPrice" runat="server" Text='<%# Eval("") %>' />
                <br />
                
                <asp:Label ID="stock" runat="server" Text='<%# Eval("quantity") %>' />
                <br />
                 
                <asp:Label ID="yourBid" runat="server" Text='<%# Eval("custName") %>' />
            </div>
        </ItemTemplate>
            </asp:Repeater>
            

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\ItemBidDB.mdf;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT * FROM"></asp:SqlDataSource>
            

        </div>
    </div>
</asp:Content>