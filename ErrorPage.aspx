<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="Item_Bidding_System.ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <style>
        .img-error{
            position:relative;
            width: 400px;
            height: 350px;
            overflow: visible;
            aspect-ratio: 1 / 1;
            background-image: url(https://yellowkazoo.files.wordpress.com/2014/04/oops.png);
            background-size: cover;
            background-repeat: no-repeat;
            background-position: center;
            margin: 20px 0px;
        }
    </style>
    
    <div style="font-size:30px;color:black;text-align:center;position:relative;margin:0 auto;margin-top:5%;">
           <div>Error 404.</div> 
            <div class="img-error"></div>
           <div>There is error available.</div>
    </div>
</asp:Content>
