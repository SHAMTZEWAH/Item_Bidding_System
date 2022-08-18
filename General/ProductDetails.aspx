<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ProductDetails.aspx.cs" Inherits="Item_Bidding_System.General.ProductDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="General.css" />
    <link type="text/css" rel="stylesheet" href="SlideShow.css" />
    <link type="text/css" rel="stylesheet" href="../Dialog.css" />
 
    <div class="content-container-signUp container">
        <div class="content-subcontainer"> <!--left part-->
            <div class="multiple-image-container"> <!--top part / image container-->
                <div class="multiple-image-left-container"> <!--left small image container and up down-->
                    <div class="up-container">
                        <a class="up" onclick="move(-1,0)">&#10094;</a>
                    </div>
                    <div class="repeater-container"> <!--left small image container exclude up down-->
                        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
                            <ItemTemplate>
                                <div class="small-image-container"> <!--small image-->
                                    <img class="medium-image image-position" src='<%# Eval("productPhotoURL") %>' alt="Product image" onmouseover="displayImage(<%# Container.ItemIndex %>)" />
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT ProductPhoto.productPhoto, ProductPhoto.productPhotoURL FROM Product INNER JOIN ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId AND ProductDetails.productName = @prodName INNER JOIN ProductPhoto ON Product.productId = ProductPhoto.productId">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="prodName" QueryStringField="prodName" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <div class="down-container">
                        <a class="down" onclick="move(1,0)">&#10095;</a>
                    </div>
                </div>
                <div class="image-display"> <!--selected image display-->
                    <img id="bigImage" class="medium-large-image" src="#" alt="display image in bigger size" />
                </div>
            </div>
            <div class="desc-container"> <!--Description-->
                <details>
                    <summary>Description:</summary>
                    <asp:Label ID="lblDesc" runat="server" Text=""></asp:Label>
                </details>
            </div>
        </div>

        <div class="content-subcontainer" style="overflow: initial;"><!--right part-->
            <div class="datalist-container"> <!---->
                <asp:DataList ID="DataList1" runat="server" OnItemDataBound="DataList1_ItemDataBound">
                     <ItemTemplate>
                        <table width="100%">
                            <tr> <!--Row 1-->
                                <td class="cell-styling"> <!--Cell 1-->
                                    <div class="flex-row flex-between-center">
                                        <div>
                                            <asp:Label ID="lblProdDesc" runat="server" Text='<%# Eval("productDesc") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblProdName" runat="server" CssClass="title3-black-bold" Text='<%# Eval("productName") %>' />
                                        </div>
                                        <div class="flex-row medium-left-gap btn-report">
                                            <i class="bi bi-exclamation-triangle-fill"></i>
                                            <div>Report</div>
                                        </div>
                                    </div>
                                </td>
                                <td class="cell-styling2">

                                </td>
                            </tr>
                            <tr> <!--Row 2-->
                                <td class="cell-styling"> <!--Cell 1-->
                                    <div class="flex-column">
                                        <div class="flex-row">
                                            Time Left:&nbsp;
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
                                        </div>

                                        <div class="flex-row">
                                            Reference Price: RM
                                            <asp:Label ID="lblRefLowPrice" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"productLowestPrice","{0:0.00}") %>' /> - RM
                                            <asp:Label ID="lblRefHighPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"productHighestPrice","{0:0.00}") %>' />
                                        </div>
                                    </div>
                                </td>
                                <td> <!--Cell 2-->

                                </td>
                            </tr>
                            <tr> <!--Row 3-->
                                <td class="cell-styling"> <!--Cell 1-->
                                    <div class="flex-row flex-between-center"> <!--big container-->
                                        <div class="flex-column"> <!--left subcontainer-->
                                            <div class="flex-row">
                                                Current Bid: RM
                                                <asp:Label ID="lblCurrentBid" CssClass="title4-blue-bold" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"currentBid","{0:0.00}") %>' />
                                            </div>
                                            <div>
                                                <asp:TextBox ID="txtBid" CssClass="small-textBox" runat="server"></asp:TextBox>
                                            </div>
                                            <div class="placeholder-size">
                                                Enter RM<asp:Label ID="lblRecommendPrice" runat="server" Text=""></asp:Label> or more
                                            </div>
                                        </div>

                                        <div class="flex-column flex-center medium-left-gap"><!--right subcontainer-->
                                            <div class="small-bottom-gap">
                                                <button id="viewBid" class="view-bid" value="viewBid" type="button">View Bid</button> <!--jQuery-->
                                            </div>
                                            <div>
                                                <asp:Button ID="btnPlaceBid" CssClass="btn-medium-golden-custom" runat="server" Text="Place Bid" OnClick="btnPlaceBid_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                                <td class="cell-styling2"> <!--Cell 2-->
                                    
                                </td>
                            </tr>
                            <tr> <!--Row 4-->
                                <td class="cell-styling"> <!--Cell 1-->
                                    <div class="flex-row flex-between-center">
                                        <div class="flex-column">
                                            Best Offer:
                                            <asp:TextBox ID="TextBox1" CssClass="small-textBox" runat="server" placeholder="Private Bid"></asp:TextBox>
                                            <div class="placeholder-size small-textBox-width" >Submit good offer privately once.</div>
                                        </div>
                                        <div class="flex-column medium-left-gap">
                                            <asp:Button ID="btnMakeOffer" CssClass="btn-medium-golden-custom" runat="server" Text="Make Offer" OnClick="btnMakeOffer_Click" />
                                        </div>
                                    </div>
                                </td>
                                <td class="cell-styling2"> <!--Cell 2-->

                                </td>
                            </tr>
                            <tr> <!--Row 5-->
                                <td class="cell-styling"> <!--Cell 1-->
                                    <div class="flex-row flex-between-center">
                                        <div class="flex-row">
                                            Buy Now Price: RM 
                                            <asp:Label ID="Label1" runat="server" CssClass="title4-blue-bold" Text='<%# DataBinder.Eval(Container.DataItem,"productPrice","{0:0.00}") %>' />
                                        </div>
                                        <div class="flex-column flex-between-center medium-left-gap">
                                            <asp:Button ID="Button1" CssClass="btn-medium-golden-custom small-bottom-gap" runat="server" Text="Add to Cart" />
                                            <asp:Button ID="btnAddToWatchlist" CssClass="btn-medium-golden-custom btn-padding small-bottom-gap" runat="server" Text="Add to Watchlist" OnClick="btnAddToWatchlist_Click" />
                                        </div>
                                    </div>
                                </td>
                                <td class="cell-styling2"> <!--Cell 2-->

                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterTemplate>
                    <asp:Label ID="lblNoData" runat="server" Text="No Data To Display" Visible="false"></asp:Label>
                </FooterTemplate>
                </asp:DataList>
            </div>
            <div id="bidTable" class="bid-table"><!--Table for View bid-->
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
            <Columns>
                <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
                <asp:BoundField DataField="Bid Amount" HeaderText="Bid Amount" SortExpression="Bid Amount" ReadOnly="True" />
                <asp:BoundField DataField="Bid Time" HeaderText="Bid Time" SortExpression="Bid Time" />
            </Columns>
        </asp:GridView>
        <!--Bid Table-->
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Account.username AS Username, CAST(BidTable.bidPrice AS Numeric(10,2)) AS [Bid Amount], BidTable.bidDateTime AS [Bid Time] FROM Account INNER JOIN BidTable ON Account.accId = BidTable.accId INNER JOIN Product ON BidTable.productId = Product.productId INNER JOIN ProductDetails ON Product.productDetailsId = ProductDetails.productDetailsId WHERE (BidTable.bidType = 'Open') AND (ProductDetails.productName = @prodName)">
            <SelectParameters>
                <asp:QueryStringParameter Name="prodName" QueryStringField="prodName" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
        </div>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Seller.businessName, Product.productId FROM ProductDetails INNER JOIN Product ON ProductDetails.productDetailsId = Product.productDetailsId INNER JOIN SubStore ON Product.subStoreId = SubStore.subStoreId INNER JOIN Seller ON SubStore.sellerId = Seller.sellerId WHERE (ProductDetails.productName = @prodName)">
            <SelectParameters>
                <asp:QueryStringParameter Name="prodName" QueryStringField="prodName" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>

    <!--Dialog for Report-->
    <input id="userAuth" type="hidden" value="<%= Request.IsAuthenticated %>" />
    <div id="myModal" class="modal">
      <!-- Modal content -->
      <div class="modal-content">
        <span class="close">&times;</span>
        <div class="modal-container">
            <asp:FormView ID="FormView1" runat="server" DataSourceID="SqlDataSource3" OnDataBound="FormView1_DataBound">
                <ItemTemplate>
                    <table id="reportTable">
                        <tr>
                            <th colspan="2">Report Form</th>
                        </tr>
                        <tr>
                            <td class="field-name all-cell">
                                <div class="field-name-text">
                                    <div>Account Name:</div>

                                </div>
                            </td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblAccName" runat="server" CssClass="textBox textBox-size" Text=''></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name all-cell"><div class="field-name-text">Product Name:</div></td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblProductName" runat="server" CssClass="textBox textBox-size" Text='<%# Request.QueryString["prodName"].ToString() %>'></asp:Label>
                                    <asp:Label ID="lblProdId" runat="server" Text='<%# Eval("productId") %>' Visible="false"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name all-cell"><div class="field-name-text">Seller Name:</div></td>
                            <td class="all-cell">
                                <div class="textBox textBox-container textBox-all-container">
                                    <asp:Label ID="lblSellerName" CssClass="textBox textBox-size" runat="server" Text='<%# Eval("businessName") %>'></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="field-name report-title all-cell">
                                <div class="field-name-text">Title:</div>
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
                                    Description:
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
                                <asp:Button ID="btnReport" CssClass="btn-medium-golden-custom" runat="server" Text="Report" OnClick="btnReport_Click" />
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

        // Get the button that opens the modal
        var btn = document.getElementsByClassName("btn-report")[0];

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        //Get the value whether it is authenticated
        var authenticate = document.getElementById("userAuth");

        // When the user clicks on the button, open the modal
        btn.onclick = function () {
            if (authenticate.value === "True") {
                modal.style.display = "block";
            }
            else {
                window.location.href = "/General/LoginPage.aspx";
            }
        }

        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }

        //function for image move up and down
        function move(value, no) {

            var slideshowArr = ["repeater-container"]
            var prodArr = ["small-image-container"]
            var con = document.getElementsByClassName(slideshowArr[no])[0];
            var prodCon = document.getElementsByClassName(prodArr[no])[0];
            var prodStyle = getComputedStyle(prodCon);

            //get content width
            var prodHeight = prodStyle.height;
            var prodHeightNum = parseInt(prodHeight);

            //get content margin right
            var prodMarginBtm = prodStyle.marginBottom;
            var prodMarginBtmNum = parseInt(prodMarginBtm);

            //get total width
            var totalHeight = prodHeightNum + prodMarginBtmNum
            totalHeight = totalHeight * 3 * value;

            window.setInterval(
                con.scrollTop += totalHeight, 10
            );
        }

        //show on bigger display
        function displayImage(index) {
            var imageCon = document.getElementsByClassName("image-position")[index];
            document.getElementById("bigImage").src = imageCon.src;
        }
        displayImage(0);
        
    </script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#viewBid").click(function () {
                if ($("#bidTable").css("display") == 'none')
                    $("#bidTable").css("display", "block");
                else
                    $("#bidTable").css("display", "none");
            });
        });
    </script>
</asp:Content>
