<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="SalesSummary.aspx.cs" Inherits="Item_Bidding_System.Seller.SalesSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container">
        <div class="top-filter"> <!--Row 1-->
            <div class="title2-black-bold content-title">Response Offer</div>
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
        </div>
        <div> <!--Row 2-->
            <div> <!--Row 2i-->
                <div>
                    Total Costs:
                </div>
                <div>
                    <asp:Label ID="LblTotalCost" runat="server" Text=''></asp:Label>
                </div>
            </div>
            <div> <!--Row 2ii-->
                <div>
                    Operating Fees:
                </div>
                <div>
                    <asp:Label ID="lblTotalOrder" runat="server" Text=''></asp:Label>
                </div>
                
            </div>
            <div>
                <div>
                    Total Profit:
                </div>
                <div>
                    <asp:Label ID="lblTotalProfit" runat="server" Text=''></asp:Label>
                </div>
            </div>
        </div>
        <div> <!--Row 3-->
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>Product ID</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("productId") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Photos</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Image ID="imgProduct" ImageUrl='<%# Eval("productPhotoURL") %>'  runat="server" />
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Product Name</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblProdName" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Brand</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("productBrand") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Model</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("productModel") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Quantity</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblQty" runat="server" Text='<%#Eval("quantity") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Product Selling Cost</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div class="displayLess" runat="server">
                                <div>Open Bid:</div>
                                <div>
                                    <asp:Label ID="lblOpenBid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"totalPrice","{0:0.00}") %>'></asp:Label>
                                </div>
                            </div>
                            <div class="displayLess" runat="server">
                                <div>Fixed Price:</div>
                                <div>
                                    <asp:Label ID="lblFixedPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"totalPrice","{0:0.00}") %>'></asp:Label>
                                </div>
                            </div> <!--If it is not applicable, invisible (display:none)-->
                            <div class="displayLess" runat="server">
                                <div>Sealed Bid:</div>
                                <div>
                                    <asp:Label ID="lblSealedBid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"totalPrice","{0:0.00}") %>'></asp:Label>
                                </div>
                            </div>
                        </div><!--Eval, Depends on the type of order, if pending then have drop down list, deal with fixed, sealed bid and open bid price -->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Operating Fees</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblIOperatingCost" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"someCost","{0:0.00}") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Total Profit</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblTotalProfit" runat="server" Visible="false" Text='<%#DataBinder.Eval(Container.DataItem,"profit","{0:0.00}") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

            </Columns>
            
        </asp:GridView>
        </div>
    </div>
</asp:Content>
