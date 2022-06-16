<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrderList.ascx.cs" Inherits="Item_Bidding_System.Seller.orderList" %>
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />


<div class="content-container">
    <div class="title2-black-bold content-title">Manage Order - ToShipped</div>
    <div class="btn-medium-white">
        <i class="bi bi-funnel-fill medium-size"></i>
        <div class="filter-text">Filter</div>
    </div>
    <div class="content-subcontainer">
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