<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NeighborhoodEditTools.aspx.cs" Inherits="GBSTools.NeighborhoodEditTools" MaintainScrollPositionOnPostback="true"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row" style="margin-top:20px">
    <asp:DropDownList ID="DropDownListForProvince" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListForProvince_SelectedIndexChanged"></asp:DropDownList>
    <asp:DropDownList ID="DropDownListForCity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListForCity_SelectedIndexChanged"></asp:DropDownList>
        </div>
    <div class="row">
        <asp:Button ID="Button1" runat="server" Text="submit" />
    <asp:ListView ID="ListViewForNeighborhood" runat="server" DataKeyNames="Id" OnItemDataBound="ListViewForNeighborhood_ItemDataBound" OnItemEditing="ListViewForNeighborhood_ItemEditing" OnItemCanceling="ListViewForNeighborhood_ItemCanceling" OnItemUpdating="ListViewForNeighborhood_ItemUpdating">
   <LayoutTemplate> 
             
           <div class="container" <%--style="max-height:400px;overflow:auto;"--%>>
                            <table id="itemPlaceholderContainer" runat="server" class="table">
                                <tr runat="server" style="">
                                   
                                    <th runat="server">id</th>
                                    <th runat="server">Neighborhood</th>
                                   <%-- <th runat="server">seoname</th>--%>
                                    <th runat="server">active</th>
                                    <th runat="server"></th>
                                     <th runat="server"></th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                            </table>
               </div>
            </LayoutTemplate>
         
            <EditItemTemplate>
                <tr>
                   
                   <td>
                        <asp:Label ID="cityidLabel" runat="server" Text='<%# Bind("id") %>' />
                        <asp:HiddenField ID="HiddenFieldforcityid" runat="server" Value='<%# Bind("id") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="cityTextBox" runat="server" Text='<%# Bind("name") %>' />
                    </td>
                  
                    <td>
                        <asp:CheckBox ID="activeCheckBox" runat="server" Checked='<%# Bind("active") %>' />
                    </td>
                    <td>
                      <asp:HiddenField ID="HiddenFieldForCId" runat="server" Value='<%# Bind("cityId") %>' />
                    </td>
                     <td>
                        <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" CssClass="btn btn-success" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </EditItemTemplate>
            <EmptyDataTemplate>
                <table runat="server" style="">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <%--<InsertItemTemplate>
                <tr>
                   
                    <td>
                        <asp:Label ID="cityidLabel" runat="server" Text='<%# Bind("id") %>' />
                        <asp:HiddenField ID="HiddenFieldforcityid" runat="server" Value='<%# Bind("id") %>' />
                    </td>
                    
                    <td>
                        <asp:TextBox ID="cityTextBox" runat="server" Text='<%# Bind("name") %>' />
                    </td>
                 
                    <td>
                        <asp:TextBox ID="seonameTextBox" runat="server" Text='<%# Bind("seoname") %>' />
                    </td>
                    <td>
                        <asp:CheckBox ID="activeCheckBox" runat="server" Checked='<%# Bind("active") %>' />
                    </td>
                    <td>
                        <asp:TextBox ID="provinceidTextBox" runat="server" Text='<%# Bind("cityId") %>' />
                    </td>
                     <td>
                        <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" CssClass="btn btn-success" />
                        <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" CssClass="btn btn-primary" />
                    </td>
                </tr>
            </InsertItemTemplate>--%>
            <ItemTemplate>
                <tr>
                    
                    <td>
                        <asp:Label ID="cityidLabel" runat="server" Text='<%# Bind("id") %>' />
                    </td>
                    <td>
                        <asp:Label ID="cityLabel" runat="server" Text='<%# Eval("Name") %>' />
                    </td>
                 
                   
                    <%--<td>
                        <asp:Label ID="seonameLabel" runat="server" Text='<%# Eval("seoname") %>' />
                    </td>--%>
                    <td>
                        <asp:CheckBox ID="activeCheckBox" runat="server" Checked='<%# Eval("active") %>' Enabled="false" />
                    </td>
                    <td>
                        <%--<asp:Label ID="provinceidLabel" runat="server" Text='<%# Eval("provinceName") %>' />--%>
                        <asp:HiddenField ID="provinceid" runat="server" Value='<%# Bind("cityid") %>' />
                    </td>
                    <td>
                       <%-- <span onclick="return confirm('Are you sure to delete?')">
                        <asp:Button ID="DeleteButton" runat="server" CommandName="Delete" Text="Delete"  CssClass="btn btn-danger" />
                       </span>--%>
                        <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" CssClass="btn btn-success" />
                    </td>
                </tr>
            </ItemTemplate>












    </asp:ListView>
        </div>
</asp:Content>
