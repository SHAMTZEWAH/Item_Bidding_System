<%@ Page Title="" Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="shippingPage.aspx.cs" Inherits="Item_Bidding_System.User.shippingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <style type="text/css">
        .box {
            margin: 70px;
            display:grid;
            grid-template-areas: 
                "header header header"
                "foot1 foot2 foot3"   ;
            grid-template-rows: 300px 200px;
        }
        .title {
            font-size:30px;
        }
         .main {
             text-align: center;             
             grid-area: header;
         }
         .sub {
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 10px;
            grid-area: foot1;
            margin-right: 20px;
            padding-top: 60px;
            font-size: 20px;
         }
         .sub2 {
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 10px;
            grid-area: foot2;
            margin-right: 20px;
            padding-top: 60px;
            font-size: 20px;
         }
         .sub3 {
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius:10px;
            grid-area: foot3;
            margin-right: 20px;
            padding-top: 60px;
            font-size: 20px;
         }
        .sub:hover {
            color:blue;
            cursor: pointer;
        }
        .sub2:hover {
            color:blue;
            cursor: pointer;
        }
        .sub3:hover {
            color:blue;
            cursor: pointer;
        }
        .auto-style1 {
             width: 60%;
        }
         .table2{
             width: 100%;
             
         }
         .table1, tr, td{
             width: 100%;
         }
    </style>

        <div class="title">Shipping Detail</div>
            
        <div class="box">
        <div class="main">
            <table class="table1">
            <tr>
                <td style="width: 20%">Shipping ID</td>
                <td style="width: 20%">Product</td>
                <td style="width: 20%">Shipping Type</td>
                <td style="width: 20%">TotalAmount</td>
                <td style="width: 20%">ShippingStatus</td>
            </tr>
            </table>
             <hr>   
                <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" Width="100%" >

                    <ItemTemplate>
                        <table class="table2">
                            <tr>
                                <td style="width: 20%"><asp:Label ID="Label1" runat="server" Text='<%# Eval("shippingId") %>'></asp:Label></td>
                                <td style="width: 20%"><asp:Label ID="Label2" runat="server" Text='<%# Eval("shippingDesc") %>'></asp:Label></td>
                                <td style="width: 20%"><asp:Label ID="Label4" runat="server" Text='<%# Eval("shippingInfo") %>'></asp:Label></td>
                                <td style="width: 20%"><asp:Label ID="Label3" runat="server" Text='<%# Eval("amount") %>'></asp:Label></td>
                                <td style="width: 20%"><asp:Label ID="Label5" runat="server" Text='<%# Eval("shippingStatus") %>'></asp:Label></td>
                            </tr>
                            
                        </table>
                        <hr>
                    </ItemTemplate>
                    </asp:DataList>

                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [shippingId], [shippingDesc], [shippingInfo], [amount], [shippingStatus] FROM [ShippingOrder]"></asp:SqlDataSource>

                 
        </div>
        
        <div class="sub">Contact Seller </div>
        <div class="sub2">Request Refund </div>
        <div class="sub3">Order Received</div>


        </div>

</asp:Content>
