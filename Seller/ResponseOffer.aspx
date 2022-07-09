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
    <input id="userAuth" type="hidden" value="<%= Request.IsAuthenticated %>" />
    <div id="myModal" class="modal">
      <!-- Modal content -->
      <div class="modal-content">
        <span class="close">&times;</span>
        <div class="modal-container">
            <asp:FormView ID="FormView1" runat="server" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                    <table id="reportTable">
                        <tr>
                            <th colspan="2">Report Form</th>
                        </tr>
                        <tr>
                            <td class="field-name all-cell">
                                <div class="field-name-text">
                                    <div>Offer amount:</div>
                                </div>
                            </td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblAccName" runat="server" CssClass="textBox textBox-size" Text='Eval("bidOffer")'></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name all-cell"><div class="field-name-text">Quantity:</div></td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblProductName" runat="server" CssClass="textBox textBox-size" Text='<%# Request.QueryString["prodName"].ToString() %>'></asp:Label>
                                    <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("quantity") %>' Visible="false"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name all-cell"><div class="field-name-text">Offer expires In:</div></td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblAddTime" runat="server" Text='<%# Eval("addDateTime") %>' Visible="False"></asp:Label>
                                            <asp:Label ID="lblDuration" runat="server" Text='<%# Eval("auctionDuration") %>' Visible="false"></asp:Label>
                                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Label ID="lblTimeLeft" runat="server" Text='' />
                                                    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="1000"></asp:Timer>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                    <!--<asp:Label ID="lblSellerName" CssClass="textBox textBox-size" runat="server" Text=''></asp:Label>-->
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name report-title all-cell">
                                <div class="field-name-text">Your counteroffer:</div>
                            </td>
                            <td class="all-cell">
                                <div class="textBox textBox-all-container">
                                    <asp:TextBox ID="txtTitle" CssClass="textBox textBox-size textBox-title" TextMode="SingleLine" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name report-desc all-cell">
                                <div class="field-name-text">
                                    Message:
                                </div>
                            </td>
                            <td class="all-cell">
                                <div class="textBox textBox-all-container textBox-desc">
                                    <asp:TextBox ID="txtDesc" CssClass="textBox textBox-desc" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="all-cell"></td>
                            <td class="all-cell">
                                <div>
                                    <div>
                                        <asp:Button ID="btnReview" CssClass="btn-medium-golden-custom" runat="server" Text="Review counteroffer" Onclick="btnReview_Click"/>
                                    </div>
                                    <div>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return closeModal();"/>
                                    </div>
                                </div>
                                
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:FormView>
        </div>
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
