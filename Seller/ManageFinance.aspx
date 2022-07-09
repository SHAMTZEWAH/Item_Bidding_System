<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageFinance.aspx.cs" Inherits="Item_Bidding_System.Seller.ManageFinance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black content-title">Seller Wallet</div>
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="flex-row flex-between-center border-black"> <!--Row 2-->
                <div class="flex-column small-left-gap"> <!--left part-->
                    <div class="">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </div>
                    <div class="">
                        Total Balance
                    </div>
                </div>
                <div class="flex-column small-right-gap small-top-gap"> <!--Right part-->
                    <div class="flex-row">
                        <div class="small-right-gap">
                            <asp:TextBox ID="TextBox1" CssClass="textBox" runat="server" placeholder="RM"></asp:TextBox></div>
                        <div>
                            <asp:Button ID="btnChange" CssClass="btn-medium-golden-custom" runat="server" Text="Button" />
                        </div>
                    </div>
                    <div class="small-top-gap small-bottom-gap" style="align-self: flex-end;">
                        <button id="" class="btn-to-link" type="button">Change Payment</button>
                    </div>
                </div>
            </div>
             </div>
            <div id="SubStoreCon" class="substore-container" runat="server">
               
            </div>
            <div class="content-subcontainer">
                <div class="title2-black-bold content-title">
                    Transaction History
                </div>
                <div>
                    
                </div>
            </div>
       
        
        </div>

</asp:Content>
