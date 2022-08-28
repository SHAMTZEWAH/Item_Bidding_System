<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ResponseOffer.aspx.cs" Inherits="Item_Bidding_System.Seller.ResponseOffer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
     <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    <link type="text/css" rel="stylesheet" href="../Dialog.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Response Offer</div>
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="filter-option">
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter Items Name</div>
                </div>
            </div>
            
            <div id="radioContainer" class="filter-content">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    
                </asp:RadioButtonList>
            </div>
             </div>
            <div id="SubStoreCon" class="substore-container" runat="server">
               
            </div>
            <div class="content-subcontainer">
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
                                <div id="productPhoto">
                                        <!--display image-->
                                        <div class="flex-column small-top-gap" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value) || !Eval("productPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                            <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                                <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" Height="100px" />
                                            </div>
                                            <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                                            <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                                <asp:Image ID="Image2" ImageUrl='<%# String.Concat("~/Seller/ProcessPhoto.ashx?prodId=",Eval("productId")) %>' Width="200px" runat="server" Height="100px" />
                                            </div>
                                            <asp:HiddenField ID="hfRowAccId" Value='<%# Eval("productId") %>' runat="server" />
                                         </div>

                                        <asp:HiddenField ID="hfProductPhotoURL" Value='<%# Eval("productPhotoURL") %>' runat="server" />
                                        <asp:HiddenField ID="hfProductPhoto" Value='<%# Eval("productPhoto") %>' runat="server" />
                                    </div>
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
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("street").ToString() + " " + Eval("poscode").ToString() + " " + Eval("city").ToString() + " " + Eval("state").ToString() + " " + Eval("country").ToString() %>'></asp:Label>
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
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btnCounterOffer" runat="server" Text="Make a counteroffer" OnClientClick="return openModal();" />
                                </div>
                            </ItemTemplate>
                            <ControlStyle BorderStyle = "None" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btnAcceptOffer" runat="server" Text="Accept Offer" OnClick="btnAcceptOffer_Click"></asp:Button>
                                </div>
                            </ItemTemplate>
                            <ControlStyle BorderStyle = "None" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <div>
                                    <asp:Button ID="btnRejectOffer" runat="server" Text="Reject Offer" OnClick="btnRejectOffer_Click"></asp:Button>
                                </div>
                            </ItemTemplate>
                            <ControlStyle BorderStyle = "None" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                        </asp:TemplateField>
                    </Columns>
            
                </asp:GridView>
            </div>
        </div>
    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        //Get the value whether it is authenticated
        var authenticate = document.getElementById("userAuth");

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            closeModal();
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                closeModal();
            }
        }

        function openModal() {
            if (authenticate.value === "True") {
                modal.style.display = "block";
            }
            else {
                window.location.href = "/General/LoginPage.aspx";
            }
            return false;
        }

        function closeModal() {
            modal.style.display = "none";
            return false;
        }

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
