<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderList.ascx.cs" Inherits="Item_Bidding_System.Seller.orderList" %>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
<style>
    .content-title{
            font-family:'Century Schoolbook';
            font-size: 25px;
            font-weight: bold; 
            margin: 20px 0px;
            margin-left: 20px;
    }
    .btn-filter{
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
        .btn-filter:hover{
            opacity: 0.8;
            background-color:mintcream;
        }
</style>


<div class="content-container">
    <div class="content-title">Manage Order - ToShipped</div>
    <div class="btn-filter">
        <i class="bi bi-funnel-fill"></i>
        <div class="filter-text">Filter</div>
    </div>
    <div class="content-only-container">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>Order ID</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Photos</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Product Name</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Brand</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Model</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>SKU</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Price</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Address</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Date</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Status</HeaderTemplate>
                    <ItemTemplate>
                        <div></div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
    </div>
</div>