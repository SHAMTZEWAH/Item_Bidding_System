 <%@ Page Language="C#" MasterPageFile="~/ItemBidding.Master" AutoEventWireup="true" CodeBehind="ManageComplaint.aspx.cs" Inherits="Item_Bidding_System.Admin.ManageComplaint" %>
<%@ Register TagPrefix="Master" TagName="Side" Src="~/SideMenuAdmin.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.3/font/bootstrap-icons.css" />
    <link type="text/css" rel="stylesheet" href="../MasterCSS.css" />
    <link type="text/css" rel="stylesheet" href="../Content.css" />
    
    <div class="content-container">
        <div class="top-filter">
            <div class="title2-black-bold content-title">Manage Complaint Report</div> <!--Row 1-->
            <div class="displayLess">
                <asp:Label ID="lblNoData" runat="server" Text="" Visible="false"></asp:Label>
            </div>
            <div class="filter-option"> <!--Row 2-->
                <div class="btn-filter btn-medium-white">
                    <i class="bi bi-funnel-fill"></i>
                    <div class="filter-text">Filter</div>
                </div>
            </div>
            <div id="radioContainer" class="filter-content">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged1" AutoPostBack="True">
                    <asp:ListItem Value="complaintDateTime">Recently added</asp:ListItem>
                    <asp:ListItem Value="complaintTitle">Complaint Title</asp:ListItem>
                    <asp:ListItem Value="productName">Product Name</asp:ListItem> 
                    <asp:ListItem Value="username">Username</asp:ListItem>                     
                </asp:RadioButtonList>
            </div>
             </div>

            <div id="SubStoreCon" class="substore-container" runat="server">
             

            </div>
            <div class="content-subcontainer" style="border:none;">
                <asp:GridView ID="userGrid" runat="server" class="role-display"  CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="1000px" AllowSorting="True" AutoGenerateColumns="False" Height="147px" OnRowDataBound="userGrid_RowDataBound" OnRowDeleting="userGrid_RowDeleting" OnRowEditing="userGrid_RowEditing" OnRowUpdating="userGrid_RowUpdating" OnRowCancelingEdit="userGrid_RowCancelingEdit" OnSelectedIndexChanged="userGrid_SelectedIndexChanged">
                
                <Columns>
                    <asp:TemplateField Visible = "False">
                        <HeaderTemplate>
                            <asp:CheckBox ID = "chkHeader" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID = "chkAccAll" runat="server" OnCheckedChanged="chkAccAll_CheckedChanged"  />
                        </ItemTemplate>
                        <ControlStyle BorderStyle = "None" />
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblComplaintID" runat="server" Text="Complaint <br /> ID"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblComplaintIdContent" runat="server" Text='<%# Eval("complaintId") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblComplaintTitle" runat="server" Text="Title"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblComplaintTitleContent" CssClass="text-break" runat="server" Text='<%# Eval("complaintTitle") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>

                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Label ID = "lblDateTime" runat="server" Text="Complaint <br /> Date Time"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblDateTimeContent" CssClass="text-break" runat="server" Text='<%# Eval("complaintDateTime") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign = "Center" VerticalAlign="Middle" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" />
                    </asp:TemplateField>
                        
                    <asp:TemplateField>
                         
                        <HeaderTemplate>
                            <asp:Label ID = "lblDesc" runat="server" Text="Description"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblDescContent" CssClass="desc-container" runat="server" Text=' <%# Eval("description") %>'></asp:Label>
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
                            <asp:Label ID = "lblUsername" runat="server" Text="Report By"></asp:Label>
                        </HeaderTemplate>
                         <ItemTemplate>
                             <asp:Label ID="lblUsernameContent" runat="server" Text='<%# Eval("username") %>'></asp:Label>
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
                                    <asp:CheckBox ID="CheckBox1" type="checkbox" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="" AutoPostBack="True" Checked="False" ViewStateMode="Inherit" TextAlign="Right" ClientIDMode="Static" EnableViewState="True" OnDataBinding="CheckBox1_DataBinding" OnLoad="CheckBox1_Load" />
                                    <span id="btnToggleRound" class="slider round" style="--transformValue:0px;" runat="server"></span>
                                </label>
                                </div>
                                 <div>
                                     <asp:Label ID="lblStatusContent" runat="server" Text='<%#  Eval("reportStatus") %>'></asp:Label>
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

        /*var panel = document.getElementById("Panel1");
        var btnAddRoles = document.getElementById("btnAddRoles");
        btnAddRoles.addEventListener("click", function () {
            panel.classList.remove("displayLess");
            panel.style.display = "block";
        });
            
        

        btnCancel = document.getElementById("btnCancel");
        btnCancel.addEventListener("click", function () {
            panel.style.display = "none";
            
        });*/
    </script>
    </asp:Content>