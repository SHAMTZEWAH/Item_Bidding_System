<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="Item_Bidding_System.Seller.ManageOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <style>
        .content-title{
            font-family:'Century Schoolbook';
            font-size: 25px;
            font-weight: bold; 
            margin: 20px 0px;
            margin-left: 20px;
        }
        .filter-option{
            display:flex;
            flex-flow: row wrap;
            justify-content: flex-start;
            align-items: center;
        }
        .filter-content{
            display: none;
            border: 1px solid black;
            margin-left: 45px;
            margin-top: 10px;
            padding: 5px;
            width: fit-content;
        }
        .btn-filter, .btn-upload-product, .btnCreateStore{
            border: 1px solid gray;
            border-radius: 4px;
            background-color: white;
            font-size: 15px;
            color: black;
            text-align:center;
            padding: 10px 0px;
            cursor: pointer;
            margin-left: 40px;
            display:flex;
            flex-flow: column wrap;
            justify-content: center;
            align-items: center;
            width: 150px;
        }
        .btn-filter:hover, .btn-upload-product:hover, .btnCreateStore:hover{
            opacity: 0.8;
            background-color:mintcream;
        }
    </style>
    
    <div class="content-container">
        <div class="top-filter">
            <div class="content-title">Manage Orders</div>
            <div class="filter-option">
                <div class="btn-filter">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
                <div class="btn-upload-product">
                    <i class="bi bi-plus-circle-fill"></i>
                    <div class="filter-text">Upload New Products</div>
                </div>
                <div class="btn-create-store">
                    <i class="bi bi-shop"></i>
                    <asp:Button ID="btnCreateStore" CssClass="btnCreateStore" runat="server" Text="Button" OnClick="btnCreateStore_Click" />
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
        

