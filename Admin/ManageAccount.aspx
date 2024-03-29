﻿<%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageAccount.aspx.cs" Inherits="Item_Bidding_System.Admin.ManageAccount" %>
<%@ Register TagPrefix="Master" TagName="Side" Src="~/SideMenuAdmin.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Manage Account Roles</div> <!--Row 1-->
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="filter-option"> <!--Row 2-->
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
                <div id="btnAddRoles" class="small-left-gap btn-medium-white">
                    <i class="bi bi-plus-circle-fill"></i>
                    <div class="filter-text">Add New Roles</div>
                </div>
            </div>
            <div id="radioContainer" class="filter-content">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                    <asp:ListItem Value="">Recently added</asp:ListItem>
                    <asp:ListItem Value="accId">AccountId</asp:ListItem> 
                    <asp:ListItem Value="username">Username</asp:ListItem>
                    <asp:ListItem Value="email">Email</asp:ListItem> 
                    <asp:ListItem Value="RoleName">RoleName</asp:ListItem> 
                    
                </asp:RadioButtonList>
            </div>
            <div>
                <div ID="Panel1" class="small-top-gap small-top-inner-gap small-bottom-inner-gap small-left-inner-gap border-black displayLess">
                        <asp:Label ID="lblRoles" runat="server" Text="Roles:" style="align-self: center;"></asp:Label>
                        &nbsp;<asp:TextBox ID="txtRoles" CssClass="textBox border-black" runat="server"></asp:TextBox>
                        &nbsp;<asp:Button ID="roleSubmitBtn" runat="server" CssClass="btn-small-golden-custom btnHover" Text="Add Role" OnClick="roleSubmitBtn_Click" />
                        &nbsp;<button ID="btnCancel" type="button" class="btn-small-lightgray btnHover">Cancel</button>
                </div>
            </div>
             </div>

            <div id="SubStoreCon" class="substore-container" runat="server">
               
            </div>
            <div class="content-subcontainer" style="border:none;">
                <asp:GridView ID="userGrid" runat="server" class="role-display"  CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1000px" AllowSorting="True" AutoGenerateColumns="False" Height="147px" OnRowDataBound="userGrid_RowDataBound" OnRowDeleting="userGrid_RowDeleting" OnRowEditing="userGrid_RowEditing" OnRowUpdating="userGrid_RowUpdating" OnRowCancelingEdit="userGrid_RowCancelingEdit" OnSelectedIndexChanged="userGrid_SelectedIndexChanged">
                
                <Columns>
                    <asp:CommandField />
                    <asp:TemplateField Visible = "False">
                        <HeaderTemplate>
                            <asp:CheckBox ID = "chkHeader" runat="server" OnCheckedChanged="chkHeader_CheckedChanged"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID = "chkAccAll" runat="server"  />
                        </ItemTemplate>
                        <ControlStyle BorderStyle = "None" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblCustID" runat="server" Text="CustID"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="accId" runat="server" Text='<%# Eval("accId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblusername" runat="server" Text="Username"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="username" runat="server" Text='<%# Eval("username") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblEmail" runat="server" Text="Email"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="email" runat="server" Text='<%# Eval("email") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblSellerRole" runat="server" Text="Seller Registration Status"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <div>
                                 <asp:Label ID="sellerRole" runat="server" Text='<%# Eval("sellerStatus") == DBNull.Value?"NULL":Eval("sellerStatus") %>'></asp:Label>
                             </div>
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>
                        
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblRoles" runat="server" Text="Roles"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <div>
                                 <asp:DropDownList ID ="ddlRoles" runat="server" AutoPostBack="True" Width="152px" OnSelectedIndexChanged="ddlRoles_SelectedIndexChanged">
                            </asp:DropDownList>
                                 <asp:HiddenField ID="hfRole" Value='<%# Eval("RoleName") %>' runat="server" />
                             </div>
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblStatus" runat="server" Text="Status"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <div>
                                 <div>
                                <label class="switch" runat="server">
                                    <asp:CheckBox ID="CheckBox1" type="checkbox" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="" AutoPostBack="True" Checked="False" ViewStateMode="Inherit" TextAlign="Right" ClientIDMode="Static" EnableViewState="True" />
                                    <span id="btnToggleRound" class="slider round" style="--transformValue:0px;" runat="server"></span>
                                </label>
                                </div>
                                 <div>
                                     <asp:Label ID="lblStatusContent" runat="server" Text='<%#  Eval("accStatus") %>'></asp:Label>
                                 </div>
                             </div>
                             <asp:HiddenField ID="hfRowNo" Value='<%# Container.DataItemIndex %>' runat="server" />
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>
                    
                </Columns>
                
                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                <SortedDescendingHeaderStyle BackColor="#242121" />
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

        var panel = document.getElementById("Panel1");
        var btnAddRoles = document.getElementById("btnAddRoles");
        btnAddRoles.addEventListener("click", function () {
            panel.classList.remove("displayLess");
            panel.style.display = "block";
        });
            
        

        btnCancel = document.getElementById("btnCancel");
        btnCancel.addEventListener("click", function () {
            panel.style.display = "none";
            
        });
    </script>
    </asp:Content>