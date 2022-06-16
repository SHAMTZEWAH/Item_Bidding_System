<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="Item_Bidding_System.Seller.ManageOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Manage Orders</div>
            <div class="filter-option">
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
                <div class="btn-upload-product btn-medium-white">
                    <i class="bi bi-plus-circle-fill"></i>
                    <div class="filter-text">Upload New Products</div>
                </div>
                <div id="btnCreateStore" class="btn-create-store btn-medium-white btnCreateStore" runat="server"> <!--Event at C#-->
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
            <div id="SubStoreCon" class="substore-container" runat="server">
               
            </div>
            <div class="content-subcontainer">

            </div>
        </div>
        
        </div>
    <script>
        document.getElementsByClassName("btn-filter")[0].addEventListener("click", function () {
            elem = document.getElementById("radioContainer")
            if (window.getComputedStyle(elem).display === "none") {
                elem.style.display = "flex";
            }
            else {
                elem.style.display = "none";
            }
        });

        document.getElementsByClassName("btn-upload-product")[0].addEventListener("click", function () {
            window.location.href = "/UploadProduct.aspx";
        });
    </script>
</asp:Content>
        

