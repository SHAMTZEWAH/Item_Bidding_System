﻿<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="UploadProduct.aspx.cs" Inherits="Item_Bidding_System.Seller.UploadProduct" %>
<%@ Register TagPrefix="Upload" TagName="Products" Src="~/Seller/EditProductDetails.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />

    <div class="content-container" runat="server">
        <Upload:Products ID="uploadProduct" runat="server"></Upload:Products>
    </div>

</asp:Content>
