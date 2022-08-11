<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageProduct.aspx.cs" Inherits="Item_Bidding_System.Seller.ManageProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">
        <div class="title1-black title1-bold">Results:</div>
        <div class="top-filter">
            <div class="title2-black-bold content-title">Manage Product</div>
            <div class="filter-option">
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
                <div class="btn-upload-product btn-medium-white">
                    <i class="bi bi-plus-circle-fill"></i>
                    <div class="filter-text">Upload New Products</div>
                </div>
                <div id="btnCreateStore" class="btn-create-store btn-medium-white btnCreateStore" runat="server" Onclick="btnCreateStore_Click"> <!--Event at C#-->
                    <i class="bi bi-shop"></i>
                    <div class="">
                        <div>Create New Substore</div>
                    </div>
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
        
        <div id="SubStoreCon" class="substore-container" runat="server">
               <asp:Panel ID="Panel1" CssClass="substore-container" runat="server">

               </asp:Panel>
            </div>
        <div class="content-subcontainer-no-marginTop" runat="server">
            
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
                                    <asp:Label ID="currentBidPrice" runat="server" Visible="false" Text='<%# Eval("Bid") %>'/><!--Text='# Eval("") '-->
                                    <asp:DropDownList ID="ddlBid" runat="server" CssClass="textBox">

                                    </asp:DropDownList>
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
                                <div>Total bid: RM</div>
                                <div>
                                    <asp:Label ID="totalBid" runat="server" Visible="false" /> <!--Text='# Eval("") '-->
                                </div>
                            </div>
                           
                        </div>
                        <div class="flex-column flex-around">
                            <button class="btn-medium-blue btnHover" onclick="/User/EditProduct.aspx?prodName=">Edit</button> <!--URL need to add-->
                        </div>
                </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
            </asp:Repeater>


        </div> 
    </div>
</asp:Content>


