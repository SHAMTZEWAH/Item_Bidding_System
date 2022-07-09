<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageOrder.aspx.cs" Inherits="Item_Bidding_System.Seller.ManageOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Manage Orders</div>
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="filter-option">
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
            </div>
            <div id="radioContainer" class="filter-content">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Awaiting Payment</asp:ListItem> <!-- yet payemnt, accepted by seller-->
                    <asp:ListItem>To Shipped</asp:ListItem> <!--payment success-->
                    <asp:ListItem>Product Received</asp:ListItem> <!-- shipped success-->
                </asp:RadioButtonList>
            </div>
             </div>
            <div id="SubStoreCon" class="substore-container" runat="server">
               
            </div>
            <div class="content-subcontainer">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>Order ID</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblOrderId" runat="server" Text='<%# Eval("orderId") %>'></asp:Label>
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
                    <HeaderTemplate>Price</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <div class="displayLess" runat="server">
                                <div>Open Bid:</div>
                                <div>
                                    <asp:Label ID="lblOpenBid" runat="server" Text='<%# Eval("totalPrice") %>'></asp:Label>
                                </div>
                            </div>
                            <div class="displayLess" runat="server">
                                <div>Fixed Price:</div>
                                <div>
                                    <asp:Label ID="lblFixedPrice" runat="server" Text='<%# Eval("totalPrice") %>'></asp:Label>
                                </div>
                            </div> <!--If it is not applicable, invisible (display:none)-->
                            <div class="displayLess" runat="server">
                                <div>Sealed Bid:</div>
                                <div>
                                    <asp:Label ID="lblSealedBid" runat="server" Text='<%# Eval("totalPrice") %>'></asp:Label>
                                </div>
                            </div>
                        </div><!--Eval, Depends on the type of order, if pending then have drop down list, deal with fixed, sealed bid and open bid price -->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Address</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("street").ToString() + Eval("poscode").ToString() + Eval("city").ToString() + Eval("state").ToString() + Eval("country").ToString() %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Date</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblDate" runat="server" Text='<%#Eval("createOrderDateTime") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />

                </asp:TemplateField>

                <asp:TemplateField>
                    <HeaderTemplate>Status</HeaderTemplate>
                    <ItemTemplate>
                        <div>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# Eval("orderStatus") %>'></asp:Label>
                        </div><!--Eval-->
                    </ItemTemplate>
                    <ControlStyle BorderStyle = "None" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
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
        

