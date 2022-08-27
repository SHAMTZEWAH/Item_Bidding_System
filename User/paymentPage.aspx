<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="paymentPage.aspx.cs" Inherits="Item_Bidding_System.User.paymentPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <script src="https://kit.fontawesome.com/0b8285e667.js" crossorigin="anonymous"></script>
    <title></title>
    
    <style type="text/css">
        .box {
            margin: 70px;
            display:grid;
            grid-template-areas: 
                "header   image"
                "header2   image2"
                "header3   image2";
            grid-template-rows: 300px 300px 480px;
        }
        .mainBox {
            margin-top: 50px;
            border-radius: 25px;
            text-align: center;
            border: 1px solid black;
            grid-area: header;
        }
        .mainBox2 {
            position: relative;
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 25px;
            grid-area: header2;
        }
        .subBox2 {
            position: absolute;
            width: 30%;
            left: 15%;
            height: 150px;
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 25px;
        }
       .subBox2:hover {
           border-color:aqua;
           color: aqua;
       }
        .subBox3 {
            position: absolute;
            width: 30%;
            left: 55%;
            height: 150px;
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 25px;
        }
        .subBox3:hover {
           border-color:aqua;
           color: aqua;
       }
        .mainBox3 {
            margin-top: 50px;
            border: 1px solid black;
            text-align: center;
            border-radius: 25px;
            grid-area: header3;
        }
        .mainBox4 {
            margin-left: 50px;
            margin-top: 50px;
            grid-area: image;
        }
        .mainBox5 {
            margin-left: 50px;
            margin-top: 50px;
            border: 1px solid black;
            grid-area: image2;
            border-radius: 25px;
            text-align: center;
        }
        .title {
            font-size:30px;
        }
        .word {
            margin-top: 40px;
            margin-left:70px;
        }
        .word2 {
            text-align: left;
        }
        .address {
            text-align: left;
        }
        .remark {
            font-size: 15px;
            color: grey;
        }
        .auto-style2 {
            width: 50px;
            height: 30px;
            border : 1px solid black;
            margin-top : 10px;
            margin-right : 10px;
        }
        .paymentWords {
            font-size: 20px;
            text-align: left;
            margin-left: 5px;
            
        }
        .paymentOption {
            text-align: left;
            margin-left: 10px;
        }
        .showVoucher {
            margin-left: 20px;
            margin-top: 5px;
            display: inline-block;
            border: 1px solid black;
            border-radius: 10px;
            width: 200px;
            text-align: center;
        }
        .showOutput {
            float: right;
            margin-right: 20px;
        }
        .showOutput1 {
            float: right;
            width:60px;
            font-size: 18px;
            text-align: center;
            margin-right: 20px;
        }
        .confirmBtn {
            float:right;
            margin-right: 10px;
  
        }
        .auto-style3 {
            width: 380px;
        }
        .checkBox {
            text-align:left;
        }
        .proceed {
            text-align: center;
            display: inline-block;
            width:30%;
            height:40px;
            float: right;
            background-color: forestgreen;
            color: white;
            border: none;
            margin-right: 20px;
        }
        .cancel {
            text-align: right;
            margin-right: 40px;
        }
        .information {    
            text-align: left;
        }
        .edit {
            width:30%;
        }
        .edit:hover {
            color:blue;
            cursor: pointer;
        }
        .haha {
            margin-top: 10px;
            float: right;
            margin-right: 35%;
        }
        .haha:hover {
            color:blue;
            cursor: pointer;
        }
        .auto-style4 {
            height: 26px;
        }
        .auto-style5 {
            width: 143px;
        }
        .auto-style6 {
            width: 136px;
        }
        .auto-style7 {
            width: 8px;
        }
    </style>
</head>
<body style="height: 1574px">
    <form id="form1" runat="server">
        

        <div class="title">CheckOut Page</div>
 
        <div class ="box">
        <div class="mainBox"> Personal Information
        <br /><br /><div class="auto-style3">Please do ensure that the following information is correct</div><br><br>
        <div class="information">    
            <asp:Label ID="Name" runat="server" Text="Label"></asp:Label>
            &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
            <asp:Label ID="Contact" runat ="server" Text="Label"></asp:Label><br><br>
            <asp:Label ID="Address" runat="server" Text="Label"></asp:Label><br><br>
            <div class="edit"><u>Click here to edit profile </u></div>  
        </div>
           
        </div>

        <div class ="mainBox4"> The following payment methods are supported by our website<br>
            <img src="https://tap2pay.me/wp-content/uploads/2018/12/PayPal-Header-720x480-1.jpg" alt="paypal" class="auto-style2">
            <img src="https://seeklogo.com/images/T/touch-n-go-ewallet-logo-CFCE2E1540-seeklogo.com.png" alt="paypal" class="auto-style2">
            <img src="https://seeklogo.com/images/V/VISA-logo-62D5B26FE1-seeklogo.com.png" alt="paypal" class="auto-style2">
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRCrJSkcbC3UE3b5lNZTvYCWJVqzWhcXIhPMA&usqp=CAU" alt="paypal" class="auto-style2">
            <img src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ1tAGIHfSuIdGLRPFYIbRbR0XmlRiWMn9C6g&usqp=CAU" alt="paypal" class="auto-style2">

            <br><br><br><div class="note"><b><mark>Note: The term points are equivalent to voucher</mark></b></div>
        </div>

            <div class="mainBox2">     
                Delivery Option
           
                <div class="subBox2" runat="server" id="div1">
                    Standard Shipping<br>
                    <br>
                    <i class="fa-solid fa-truck fa-3x"></i>
                    <br>
                    <br>
                    6 - 9 working days   
                </div>
                <div class="subBox3" runat="server" id="div2">
                    Express Shipping<br>
                    <br>
                    <i class="fa-solid fa-truck-fast fa-3x"></i>
                    <br>
                    <br>
                    2 - 5 working days 
                 </div>
                
                <br /><br /><div class="word2">*Note External factor such as weather condition will affect the shipping</div>
                <br />

            </div>
        <div class="mainBox5">Payment Detail<br /><br /><br />
            <div class="paymentWords">
                <b>Subtotal</b>
                <asp:Label ID="subtotal" CssClass="showOutput" runat="server" Text="Label"></asp:Label><div class="remark">*The total Price of selected items</div>
                <b><br>Shipping</b>
                <asp:Label ID="shipping" CssClass="showOutput" runat="server" Text="Label"></asp:Label><br><asp:Label ID="shippingSelected" runat="server" Font-Size="15px" ForeColor="#999999"></asp:Label><br>
                <b><br>Points</b>
                <asp:TextBox ID="TextBoxVoucher" CssClass="showOutput1" runat="server" AutoPostBack="True" >0</asp:TextBox><div class="remark">*There will be no points earned if points are used</div>
                <div class="showVoucher">Available:<asp:Label ID="LabelPoint" runat="server" Text="label"></asp:Label></div>
                <asp:Button ID="btnVoucher" runat="server" CssClass="haha" Text="Get More" OnClick="btnVoucher_Click"  />

                <br>
                <b><br>SalesTax</b>
                <asp:Label ID="tax" CssClass="showOutput" runat="server" Text="Label"></asp:Label><div class="remark">*There will be a 6% of government tax</div>
                <br><hr>
                <b>Total:</b><asp:Label ID="finalTotal" CssClass="showOutput" runat="server" Text="Label"></asp:Label><br><br>
                <b>Points Earned:</b><asp:Label ID="pointsEarned" CssClass="showOutput" runat="server" Text="Label"></asp:Label><br>

            </div> 
            <br><br><br>
            <div class="checkBox"><asp:CheckBox ID="CheckBox1" runat="server" />I agree to the payment Term & Condition </div></div>
        
            
        <div class="mainBox3">Payment Option

            <br><br><div class="paymentOption">Please select a payment method<br>
            <asp:DropDownList ID="DropDownList1" runat="server" Height="23px" Width="147px" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="1" Text="Paypal">Paypal</asp:ListItem>
                <asp:ListItem Value="2" Text="Touch And Go" >TouchAndGo Ewallet</asp:ListItem>
                <asp:ListItem Value="3" Text="Visa Card" >Visa Card</asp:ListItem>
                <asp:ListItem Value="4" Text="Crypto" >Crypto</asp:ListItem>
            </asp:DropDownList>
                <br>
                <table>
                    <tr>
                        <td><asp:Label ID="paypal1" runat="server" Text="Email: " Visible="False" ></asp:Label></td>
                        <td class="auto-style6"><asp:TextBox ID="paypal3" runat="server" Visible="False"></asp:TextBox></td>
                        <td><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Invalid Email" ControlToValidate="paypal3" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red">*</asp:RegularExpressionValidator></td></tr>
                    <tr>
                        <td><asp:Label ID="paypal2" runat="server" Text="Password: " Visible="False"></asp:Label></td>
                        <td class="auto-style6"><asp:TextBox ID="paypal4" runat="server" Visible="False"></asp:TextBox></td>
                        <td>&nbsp;</td></tr>
                </table>

                <table>
                    <tr>
                    <td><asp:Label ID="touchAndGo1" runat="server" Text="Phone Number: " Visible="False"></asp:Label></td>
                    <td><asp:TextBox ID="touchAndGo2" runat="server" Visible="False"></asp:TextBox></td>
                    <td><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="Invalid Phone Number" ControlToValidate="touchAndGo2" ForeColor="Red" ValidationExpression="^\d{10}$">*</asp:RegularExpressionValidator></td>
                    </tr>
                </table>

                <table>
                    <tr>
                        <td><asp:Label ID="visa1" runat="server" Text="Card Number: " Visible="False"></asp:Label></td>
                        <td class="auto-style5"><asp:TextBox ID="visa5" runat="server" Visible="False"></asp:TextBox></td>
                        <td><asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Invalid Card Number" ControlToValidate="visa5" ForeColor="Red" ValidationExpression="^\d{16}$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="visa2" runat="server" Text="Card Holder's Name: " Visible="False"></asp:Label></td>
                        <td class="auto-style5"><asp:TextBox ID="visa6" runat="server" Visible="False"></asp:TextBox></td>
                        <td><asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Invalid Name" ControlToValidate="visa6" ForeColor="Red" ValidationExpression="^[a-zA-Z]+$">*</asp:RegularExpressionValidator></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="visa3" runat="server" Text="Expiry Date: " Visible="False"></asp:Label> </td>
                        <td class="auto-style5"><asp:DropDownList ID="visaMonth" runat="server" AutoPostBack="True" DataTextField="Month" DataValueField="Month" Visible="False">
                            <asp:ListItem>January</asp:ListItem>
                            <asp:ListItem>February</asp:ListItem>
                            <asp:ListItem>March</asp:ListItem>
                            <asp:ListItem>April</asp:ListItem>
                            <asp:ListItem>May</asp:ListItem>
                            <asp:ListItem>June</asp:ListItem>
                            <asp:ListItem>July</asp:ListItem>
                            <asp:ListItem>August</asp:ListItem>
                            <asp:ListItem>September</asp:ListItem>
                            <asp:ListItem>October</asp:ListItem>
                            <asp:ListItem>November</asp:ListItem>
                            <asp:ListItem>December</asp:ListItem>
                    </asp:DropDownList>
                            <asp:DropDownList ID="visaYear" runat="server" AutoPostBack="True" DataTextField="Year" DataValueField="Year" Visible="False">
                            <asp:ListItem>2022</asp:ListItem>
                            <asp:ListItem>2023</asp:ListItem>
                            <asp:ListItem>2024</asp:ListItem>
                            <asp:ListItem>2025</asp:ListItem>
                            <asp:ListItem>2026</asp:ListItem>
                    </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="visa4" runat="server" Text="CVV: " Visible="False"></asp:Label></td>
                        <td class="auto-style5"><asp:TextBox ID="visa7" runat="server" Visible="False"></asp:TextBox></td>
                        <td><asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Invalid CVV" ControlToValidate="visa7" ForeColor="Red" ValidationExpression="^\d{3}$">*</asp:RegularExpressionValidator></td>
                    </tr>
            </table>

            <table>
                <tr>
                    <td class="auto-style4"><asp:Label ID="crypto1" runat="server" Text="Wallet Address: " Visible="False"></asp:Label></td>
                    <td class="auto-style4"><asp:TextBox ID="crypto3" runat="server" Visible="False"></asp:TextBox></td>
                    <td class="auto-style7"><asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ErrorMessage="Invalid Address" ControlToValidate="crypto3" ForeColor="Red" ValidationExpression="^[a-zA-Z0-9]{20,30}$">*</asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td><asp:Label ID="crypto2" runat="server" Text="Password: " Visible="False"></asp:Label></td>
                    <td><asp:TextBox ID="crypto4" runat="server" Visible="False"></asp:TextBox></td>
                    <td>&nbsp;</td>
                </tr>
            </table>

            <table>
                <tr>
                    <td>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
                </tr>
            </table>
            
            <div><asp:Button ID="Proceed" CssClass="proceed" runat="server" Text="Proceed" OnClick="Proceed_Click" /></div><br><br>
            <br><div class="cancel"><u aria-dropeffect="move">Cancel Payment</u></div>
             
            </div>
        </div>
        </div>

    </form>
    <form action="https://www.sandbox.paypal.com/cgi-bin/webscr" method="post" target="_top">
    <input type="hidden" name="cmd" value="_s-xclick" />
    <input type="hidden" name="hosted_button_id" value="G5NLK3UDPEZSJ" />
    <input type="hidden" name="item_name" value="Total Product" />
    <input type="image" src="https://www.sandbox.paypal.com/en_US/i/btn/btn_buynowCC_LG.gif" border="0" name="submit" alt="PayPal - The safer, easier way to pay online!" />
    <img alt="" border="0" src="https://www.sandbox.paypal.com/ms_MY/i/scr/pixel.gif" width="1" height="1" />
    </form>

</body>
</html>
