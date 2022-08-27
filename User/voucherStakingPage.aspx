<%@ Page Title="" Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="voucherStakingPage.aspx.cs" Inherits="Item_Bidding_System.User.voucherStakingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <style type="text/css">
        .box {
            margin :70px;
            display:grid;
            grid-template-rows: 300px 100px 200px;
            grid-template-areas:
                "box1 box1 box1 box1 . box5 box5 box5 box5"
                "box2 box2 box2 box2 . . . . . "
                "box3 box3 box4 box4 . box6 box6 box6 box6 ";
        }
        .auto-style1 {
            width: 100%;
            text-align: center;
            font-size: 30px;
        }
        .mainBox {
            position: relative;
            border-radius: 25px;
            grid-area: box1;
            border: 1px solid black;
        }
        .contentBox {
            position:absolute;
            left: 5%;
            top:20px;
            width: 35%;
            height:40%;
            background-color: grey;
            padding-top: 20px;
            padding-left: 20px;
        }
        .contentBox1 { 
            position: absolute;
            left: 53%;
            top:20px;
            width: 35%;
            height:40%;
            background-color: grey;
            padding-top: 20px;
            padding-left: 20px;
        }
        .stakeBtn {
            position: absolute;
            display: inline-block;
            width: 40%;
            height : 15%;
            left: 30%;
            top: 65%;
            background-color: lightblue;
            color:white;
            border: none;
            font-size: 15px;
 
        }
        .stakeBtn:hover {
            background-color: darkblue;
            cursor: pointer;
        }
        .stakeValue { 
            text-align: center;
            margin-top: 20px;
            margin-bottom: 20px;
            border-radius: 5px;
            grid-area: box2;
            border: 1px solid black;
            padding-bottom: 10px;
        }
        .stakeRate {  
            text-align: center;
            border: 1px solid black;
            border-radius: 15px;
            grid-area: box3;
            margin-right: 10px;
        }
        .circulate {
            text-align: center;
            border: 1px solid black;
            border-radius: 15px;
            grid-area: box4;
        }
        .reward {
            text-align: center;
            border: 1px solid black;
            border-radius: 25px;
            grid-area: box5;
        }
        .claimBtn {
            width: 30%;
            background-color: green;
            color:white;
            margin-right: 30px;
            height: 40px;
        }
        .restakeBtn {
            width: 30%;
            background-color: cornflowerblue;
            color:white;
            height: 40px;
 
        }
        .pointDistribution {
            border-radius: 25px;
            grid-area: box6;
            border: 1px solid black;
            text-align: center;
        }
        .rewardEarned {
            border: none;
            text-align: left;
            padding-left:100px;
            width:auto;
        }
        .labelPoint {
            float: right;
            padding-right: 100px;
        }
        .box1Message {
            padding-top:260px;
            padding-left: 30px;
        }
        .box5Message {
            text-align:left;
            padding-left: 20px;
            padding-top:100px;
        }
        .box2Message {
            text-align:left;
            padding-top:80px;
            padding-left: 20px;
        }
    </style>
<%--</head>
<body style="height: 900px">
    <form id="form1" runat="server" class="auto-style3">--%>
        <div class="auto-style1">
            Voucher Staking Dashboard</div>

        <div class="box">
        <div class ="reward"><b>Estimate Reward</b><br><br><br>
            
            <div class="rewardEarned">Potential Points Earned <asp:Label ID="pointReward" CssClass="labelPoint" runat="server"></asp:Label></div>
            <div class="rewardEarned">Total Points Earned <asp:Label ID="label2" CssClass="labelPoint" runat="server" Text="0"></asp:Label></div><br><br><br>
            <asp:Button ID="Button1" runat="server" CssClass="claimBtn" Text="Claim" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" CssClass="restakeBtn" Text="Restake" OnClick="Button2_Click" />
            <div class="box2Message">*The points will be given out daily at 0.00a.m.</div>
        </div>


        <div class="mainBox">
        <div class="contentBox"><b>Available Point</b><br><br><br> 
            <asp:Label ID="token" runat="server"></asp:Label>    
        </div>
            

        <div class="contentBox1"><b>Amount to Stake</b><br><br><br>
            <asp:TextBox ID="tokenStake" runat="server" Font-Size="Small" Font-Strikeout="False" ForeColor="#999999" Width="70%"></asp:TextBox></div>
        <asp:Button ID="stakeBtn" runat="server" Text="Stake" CssClass="stakeBtn" OnClick="stakeBtn_Click" />
        <div class="box1Message">*The points that invest into the pool will be locked for 30days!!!</div>
        </div>

        <div class ="stakeValue"><b>Total Stack Value</b>
            <br><br>
            
            <asp:Label ID="stakeValue" runat="server"></asp:Label>
        </div>
        
        <div class ="stakeRate"><b>Staking Rate</b><br><br><br>
            
            <asp:Label ID="rate" runat="server"></asp:Label></div>

        <div class ="circulate"><b>Total Circulating Token</b><br><br><br>
            
            <asp:Label ID="tokenCirculate" runat="server"></asp:Label></div>
            
            
        <div class ="pointDistribution"><b>Daily Point Distribution</b><br><br>

            <asp:Label ID="label1" runat="server"></asp:Label>
        <div class="box5Message">*The daily points will be adjusted every month</div>
        </div>
       </div>
</asp:Content>
