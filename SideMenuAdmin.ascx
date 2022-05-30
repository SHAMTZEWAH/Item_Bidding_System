<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SideMenuAdmin.ascx.cs" Inherits="Item_Bidding_System.SideMenuAdmin" %>


<style>
    nav.side-menu-admin-body{
        position:fixed;
        border: 3px solid black;
        margin-left:15%;
        width:200px;
        height: 85%;
    }
    div.side-menu-admin-container{
        list-style-type: none;
        display:flex;
        flex-flow: column wrap;
        align-items: center;
        justify-content: flex-start;
        align-content: center;
        height: 75%;
        padding-left:0px;
    }
    div.side-menu-item{
        width:100%;
        border-bottom:1px solid gray;
        text-align:center;
        padding: 20px 0;
    }
    div.menu-top{
        padding: 30px 0;
        font-family:Georgia;
        font-weight: bold;
    }
    a.side-menu-admin{
        color: black;
        text-decoration: none;
        margin: 0px;
    }
    div.side-menu-item:hover{
        background-color: aliceblue;
        cursor:pointer;
    }
    div.menu-top:hover{
        background-color: white;
        cursor:default;
    }
</style>
<!--class="side-menu-admin-body"-->
<nav class="side-menu-admin-body">
    <div class="side-menu-admin-container">
        <div class="side-menu-item menu-top">
            Admin Hub
        </div>
        <div class="side-menu-item">
            <a class="side-menu-admin" href="Admin/ManageAccount.aspx">Manage Account Role</a> <!--Url to manage acc role page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu-admin" href="Admin/ManageProduct.aspx">Monitor Product</a> <!--Url to monitor product page-->
        </div>
        <div class="side-menu-item">
            <a class="side-menu-admin" href="Admin/ManageComplaint.aspx">Monitor Complaint Report</a> <!--Url to monitor complaint page-->
        </div>
    </div>
</nav>
