<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuAdmin.ascx.cs" Inherits="Item_Bidding_System.SideMenuAdmin" %>
<link type="text/css" rel="stylesheet" href="SideMenu.css" />

<!--class="side-menu-admin-body"-->
<nav class="side-menu-body">
    <div class="side-menu-container">
        <div class="side-menu-item menu-top">
            Admin Hub
        </div>
        <div class="side-menu-item">
            <a class="side-menu" href="Admin/ManageAccount.aspx">Manage Account Role</a> <!--Url to manage acc role page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu" href="Admin/ManageProduct.aspx">Monitor Product</a> <!--Url to monitor product page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu" href="Admin/ManageComplaint.aspx">Monitor Complaint Report</a> <!--Url to monitor complaint page-->
        </div>
    </div>
</nav>
