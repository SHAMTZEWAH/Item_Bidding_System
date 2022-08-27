<%@ Page Title="" Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="Item_Bidding_System.User.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <style type = "text/css">
        .TableLabel {
            width: 14.1%;
            display: inline-block;
            float:left;
            margin-top: 10px;
            border: 1px solid black;
            text-align: center;
        }
        h6 {
            font-size: 20px;
            padding-left: 80%;
        }
        .auto-style12 {
            width: 100%;
        }
        .itemTemplate {
            width:14%;
            text-align: center;
        }
        .delete {
            width:100px;
            text-align: center;
            background-color:red;
            color:white;
        }
        .checkOutButton {
            display: inline-block;
            text-align: right;
            background-color: green;
            color: white;
            border: 1px solid green;
            height: 40px;
            float: right;
            margin-right: 100px;
            padding-right: 40px;
            width: 100px;
        }
    </style>



        <h1>Cart</h1>
    <div class="TableLabel">Product</div>
    <div class="TableLabel">Seller</div>
    <div class="TableLabel">Detail</div>
    <div class="TableLabel">Unit Quantity</div>
    <div class="TableLabel">Unit Price</div>
    <div class="TableLabel">Total</div>
    <div class="TableLabel">Actions</div>

    <br />

    <asp:DataList ID="DataList1" runat="server" Height="144px" Width="100%" DataSourceID="SqlDataSource1">
        <ItemTemplate>
            <table class="auto-style12">
                <tr>
                    <td class="itemTemplate">
                        <asp:HiddenField ID="HiddenField1" Value='<%# Eval("productId") %>' runat="server" />
            <asp:Label ID="productName" runat="server" Text='<%# Eval("productName") %>' /><br />
                        
                    </td>
                    <td class="itemTemplate">
            <asp:Label ID="sellerLabel" runat="server" Text='<%# Eval("seller") %>' />
                        <br />
                    </td>
                    <td class="itemTemplate">Desc:
            <asp:Label ID="productDesclabel" runat="server" Text='<%# Eval("productDesc") %>' /><br />
            </td>
                    <td class="itemTemplate">
                        <asp:Button ID="BtnMinus" runat="server" Text="-" OnClick="BtnMinus_Click"/>
                        <asp:Label ID="quantity" runat="server" Text='<%# Eval("quantity") %>'></asp:Label>
                        <asp:Button ID="BtnAdd" runat="server" Text="+" OnClick="BtnAdd_Click" /></td>
                    <td class="itemTemplate">&nbsp;<asp:Label ID="productPrice" runat="server" Text='<%#"RM " + Eval("price") %>' />&nbsp;</td>
                    
                    <td class="itemTemplate">
                        <asp:Label ID="totalPrice" runat="server" Text='<%# "RM " +((int.Parse)(Eval("quantity").ToString()) * (int.Parse)(Eval("price").ToString())) %>'></asp:Label>
                    </td>
                    <td class="itemTemplate">
                        <asp:Button CssClass="delete" ID="delete" runat="server" Text="delete" OnClick="delete_Click" />
                    </td>
                </tr>
            </table>
            <hr>
        </ItemTemplate>
    </asp:DataList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Product.productId, Product.productDesc, ProductDetails.productName, CartTable.quantity, Seller.businessName, (OrderProduct.totalPrice/OrderProduct.quantity) FROM CartTable INNER JOIN Product ON CartTable.productId = Product.productId INNER JOIN ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId INNER JOIN SubStore ON SubStore.subStoreId = Product.subStoreId INNER JOIN Seller ON Seller.sellerId = SubStore.sellerId INNER JOIN OrderProduct ON Product.productId = OrderProduct.productId"></asp:SqlDataSource>
    <h6>
        <asp:Label ID="Label2" runat="server" Text="Total Amount RM: "></asp:Label>

        <asp:Label ID="Label1" runat="server"></asp:Label>
    </h6>
    <div>
        <asp:Button ID="Button1" CssClass="checkOutButton" runat="server" Text="Check Out" OnClick="Button1_Click" />

    </div>

    <br />
</asp:Content>
