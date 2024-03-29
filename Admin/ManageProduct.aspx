﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/ItemBidding.Master" CodeBehind="ManageProduct.aspx.cs" Inherits="Item_Bidding_System.Admin.ManageProduct" %>
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
            <div class="flex-row">
                <div class="filter-option"> <!--Row 2-->
                    <div class="btn-filter btn-medium-white">
                        <i class="bi bi-funnel-fill"></i>
                        <div class="filter-text">Filter</div>
                    </div>
                </div>
                <div id="radioContainer" class="filter-content">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged1" AutoPostBack="True">
                        <asp:ListItem Value="addDateTime">Recently added</asp:ListItem>
                        <asp:ListItem Value="productName">Product Name</asp:ListItem>
                        <asp:ListItem Value="productBrand">Brand</asp:ListItem> 
                        <asp:ListItem Value="productModel">Product Model</asp:ListItem> 
                        <asp:ListItem Value="businessName">BusinessName</asp:ListItem>                     
                    </asp:RadioButtonList>
                </div>
                <div style="align-self:center;">
                    <asp:Button ID="btnUpdateRef" CssClass="btn-filter btn-medium-white" runat="server" Text="Update Crawled Info" OnClick="btnUpdateRef_Click" />
                </div>
            </div>
             </div>

            <div class="content-subcontainer" style="border:none;">
                <asp:GridView ID="userGrid" runat="server" class="role-display"  CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1000px" AllowSorting="True" AutoGenerateColumns="False" Height="147px" OnRowDataBound="userGrid_RowDataBound" OnRowDeleting="userGrid_RowDeleting" OnRowEditing="userGrid_RowEditing" OnRowUpdating="userGrid_RowUpdating" OnRowCancelingEdit="userGrid_RowCancelingEdit" OnSelectedIndexChanged="userGrid_SelectedIndexChanged">
                
                <Columns>
                    <asp:TemplateField Visible="False">
                        <HeaderTemplate>
                            <asp:CheckBox ID = "chkHeader" runat="server" OnCheckedChanged="chkHeader_CheckedChanged"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID = "chkAccAll" runat="server" OnCheckedChanged="chkAccAll_CheckedChanged"  />
                        </ItemTemplate>
                        <ControlStyle BorderStyle = "None" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblproductId" runat="server" Text="Product Id"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProductIdContent" runat="server" Text='<%# Eval("productId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate> 
                            <asp:Label ID = "lblProductPhoto" runat="server" Text="Product Photo"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div id="productPhoto">
                                <!--display image-->
                                <div class="flex-column small-top-gap" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value) || !Eval("productPhoto").Equals(DBNull.Value)?true:false %>' runat="server">
                                    <div id="imgCon1" class="border-black flex-column" runat="server" visible='<%# !Eval("productPhotoURL").Equals(DBNull.Value)?true:false %>'>
                                        <asp:Image ID="Image1" Width="200px" ImageUrl='<%# Eval("productPhotoURL") %>' runat="server" Height="100px" />
                                    </div>
                                    <div id="imgCon2" class="border-black flex-column" runat="server" visible='
                                    <%# !Eval("productPhoto").Equals(DBNull.Value) ?true:false %>'>
                                        <asp:Image ID="Image2" ImageUrl='<%# String.Concat("~/User/ProcessPhoto.ashx?prodId=",Eval("productId")) %>' Width="200px" runat="server" Height="100px" />
                                    </div>
                                    <asp:HiddenField ID="hfRowAccId" Value='<%# Eval("productId") %>' runat="server" />
                                 </div>

                                <asp:HiddenField ID="hfProductPhotoURL" Value='<%# Eval("productPhotoURL") %>' runat="server" />
                                <asp:HiddenField ID="hfProductPhoto" Value='<%# Eval("productPhoto") %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblProductName" runat="server" Text="Product Name"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblProductNameContent" runat="server" Text='<%# Eval("productName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblDateTime" runat="server" Text="Added Date Time"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblAddDateTimeContent" runat="server" Text='<%# Eval("addDateTime") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblProductBrand" runat="server" Text="Brand"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblBrandContent" runat="server" Text='<%# Eval("productBrand") %>'></asp:Label>
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblProductModel" runat="server" Text="Model"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblModelContent" runat="server" Text='<%# Eval("productModel") %>'></asp:Label>
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>
                        
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblBusinessName" runat="server" Text="Business Name"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblBusinessNameContent" runat="server" Text='<%# Eval("businessName") %>'></asp:Label>
                         </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblFlag" runat="server" Text="Flag"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div>
                                <div>
                                    <label class="switch" runat="server">
                                        <asp:CheckBox ID="CheckBox1" type="checkbox" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged1" Text="" AutoPostBack="True" Checked="False" ViewStateMode="Inherit" TextAlign="Right" ClientIDMode="Static" EnableViewState="True" />
                                        <span id="btnToggleRound" class="slider round" style="--transformValue:0px;" runat="server"></span>
                                    </label>
                                </div>
                                <div>
                                    <div>
                                        <asp:Label ID="lblStatusContent" runat="server" Text='<%#  Eval("productStatus") %>'></asp:Label>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hfRowNo" Value='<%# Container.DataItemIndex %>' runat="server" />
                            </div>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontAlalign="Right" FooterStyle-BackColor="Blue" FooterStyle-ForeColor="White">
                        <FooterTemplate>
                            <asp:Button ID="btnFlag" CssClass="btn-small-golden" runat="server" Text="Flag" />
                        </FooterTemplate>

<FooterStyle HorizontalAlign="Right" BackColor="Blue" ForeColor="White"></FooterStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
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

