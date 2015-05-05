<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GBSTools.aspx.cs" Inherits="GBSTools.GBSTools" MaintainScrollPositionOnPostback="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h3>GBS Management Tool</h3> 
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Country List &nbsp<span class="badge"><asp:Literal ID="CountryCount" runat="server"></asp:Literal></span></h3>
            </div>
            <div class=" panel-body">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:DropDownList ID="DpListForCountry" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DpListForCountry_SelectedIndexChanged" ViewStateMode="Enabled"></asp:DropDownList>
                          
                    </div>

                  
                        <div class="form-group">
                                <label for="TextBoxForCountry" >Name:</label>  
                            <asp:TextBox ID="TextBoxForCountry" runat="server" CssClass="form-control" CausesValidation="False" EnableViewState="False" ></asp:TextBox>
                            
                        </div> 
                    <div class="form-group">
                    <label for="TextBoxForCountryId">Abbreviation(ID):</label>
                   <asp:TextBox ID="TextBoxForCountryId" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>

                    </div>
                     <div>
                      
                    <asp:Button ID="CountryAddButton" runat="server" Text="Add" CssClass="btn btn-success" OnClick="CountryAddButton_click" ValidationGroup="CountryAddGroup" />
                                         
                </div>

                       <div>
                           <span class="infoWarning"><asp:Literal ID="LiteralForAddCountryWarning" runat="server" EnableViewState="False"></asp:Literal></span> 
                       </div>
                    <div>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidatorForCountry" runat="server" ErrorMessage="Country can not be empty" ValidationGroup="CountryAddGroup" ControlToValidate="TextBoxForCountry" CssClass="error-block"></asp:RequiredFieldValidator>
                    </div>
                    <div>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorForCountryId" runat="server" ErrorMessage="Country Id can not be empty" ValidationGroup="CountryAddGroup" ControlToValidate="TextBoxForCountryId" CssClass="error-block"></asp:RequiredFieldValidator>
                    </div> 
                         
                          
                                 
                    </div>
               
                <div class="col-md-offset-2  col-md-5">
                  <div class="form-group">
                      <asp:ListBox ID="LBForCountry" runat="server" SelectionMode="Multiple" CssClass="form-control"></asp:ListBox>
                  </div>
                    <asp:Panel ID="PanelForDelEditCountry" runat="server">
                    <div>
                     <asp:Button ID="CountryDelButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="CountryDelButton_click" ValidationGroup="DelEditCountryGroup" />
                    <asp:Button ID="CountryEditButton" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="CountryEditButton_Click" ValidationGroup="DelEditCountryGroup" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectCountry" runat="server" ErrorMessage="Please select a country" ControlToValidate="LBForCountry" ValidationGroup="DelEditCountryGroup"></asp:RequiredFieldValidator>
                     
                  </div>
                    </asp:Panel>
              <asp:Panel ID="PanelForUpdateCountry" runat="server">
                  <div class="form-group">
                   
                     <label for="CountryEditTextBox">Name:</label>
                     <asp:TextBox ID="CountryEditTextBox" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>
                     <asp:HiddenField ID="countryId" runat="server" />
              </div>
                 
                  <div  class="form-group">
                  <asp:Button ID="CountryUpdateButton" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="CountryEditGroup" OnClick="CountryUpdateButton_Click" />
               
                </div>
                 <div> 
                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorForCountryEdit" runat="server" ErrorMessage="Country can not be empty" ValidationGroup="CountryEditGroup" ControlToValidate="CountryEditTextBox" SetFocusOnError="True" Display="Dynamic"></asp:RequiredFieldValidator> 
                 </div>
               </asp:Panel>
                    <div>  <span class="infoWarning"><asp:Literal ID="CountryDelEditErrorWarning" runat="server" EnableViewState="False"></asp:Literal></span></div>
                </div>
             
              
              
            </div>
            </div>
    </div>
    <div class="row">
        <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Province/Region List&nbsp<span class="badge"><asp:Literal ID="ProvinceCount" runat="server"></asp:Literal></span></h3>
            </div>
            <div class=" panel-body">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:DropDownList ID="DdlForProvince" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlForProvince_SelectedIndexChanged"></asp:DropDownList>
                            
                    </div>
               
                    <div class="form-group">
                          <label for="TextBoxForProvince">Name:</label>
                            <asp:TextBox ID="TextBoxForProvince" runat="server" CssClass="form-control" CausesValidation="False" EnableViewState="False" ></asp:TextBox>
                        </div>
                 
                    <div class="form-group">
                         <label for="TextBoxForProvinceId" >Abbreviation(ID):</label>  
                        <asp:TextBox ID="TextBoxForProvinceId" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>
                    </div>
                        <div>
                        <asp:Button ID="ProvinceAddButton" runat="server" Text="Add" CssClass="btn btn-success" OnClick="ProvinceAddButton_Click" ValidationGroup="ProvinceAddGroup" />
                       
                    </div>
                    <div>
                        <span class="infoWarning"><asp:Literal ID="LiteralForAddProvinceWarning" runat="server" EnableViewState="False"></asp:Literal></span>
                     </div>
                    <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForAddPronvivce" runat="server" ErrorMessage="Province/region/state can not be empty" ValidationGroup="ProvinceAddGroup" ControlToValidate="TextBoxForProvince"></asp:RequiredFieldValidator>
                        </div>
                    <div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForProvinceId" runat="server" ErrorMessage="Province/region/state Id can not be empty"  ValidationGroup="ProvinceAddGroup" ControlToValidate="TextBoxForProvinceId"></asp:RequiredFieldValidator>
                  </div>
                    
                
                      
                    </div>
                <div class="col-md-offset-2  col-md-5">
                  <div class="form-group">
                        <label for="LBForProvince" >Province List For:<%=SelectCountry %></label> 
                    <asp:ListBox ID="LBForProvince" runat="server" CssClass="form-control" Rows="8"></asp:ListBox>
                  </div>
                    <asp:Panel ID="PanelForDelEditProvince" runat="server">
                    <div>
                    <asp:Button ID="ProviceDelButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="ProviceDelButton_Click" ValidationGroup="ProvinceDelEditGroup" />
                    <asp:Button ID="ProvinceEditButton" runat="server" Text="Edit" CssClass="btn btn-success" OnClick="ProvinceEditButton_Click" ValidationGroup="ProvinceDelEditGroup"/>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectProvince" runat="server" ErrorMessage="please select a province/region" ValidationGroup="ProvinceDelEditGroup" ControlToValidate="LBForProvince"></asp:RequiredFieldValidator>
                    </div> 
                  </asp:Panel> 
                    <asp:Panel ID="PanelForUpdateProvince" runat="server">
                     <div class="form-group">
                         <label for="ProvinceEditTextBox">Name:</label>
                      <asp:TextBox ID="ProvinceEditTextBox" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>
                      <asp:HiddenField ID="ProvinceId" runat="server" />
                  </div>
                       
                    <div>
                         <asp:Button ID="ProvinceUpdateButton" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="ProvinceEditGroup" OnCommand="ProvinceUpdateButton_Command"/>
                    </div>
                         <div><asp:RequiredFieldValidator ID="RequiredFieldValidatorForEditProvince" runat="server" ErrorMessage="Province/region can not be empty" ValidationGroup="ProvinceEditGroup" ControlToValidate="ProvinceEditTextBox"></asp:RequiredFieldValidator></div>
                    </asp:Panel>
                   <div>
                       
                        <span class="infoWarning"><asp:Literal ID="ProvinceDelEditErrorWarning" runat="server" EnableViewState="False"></asp:Literal></span>
                       </div>
                </div>
            </div>
       </div>
    </div>
     <div class="row">
     <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">City List&nbsp<span class="badge"><asp:Literal ID="CityCount" runat="server" ></asp:Literal></span></h3>
            </div>
         <div class=" panel-body">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:DropDownList ID="DdlforCity" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlforCity_SelectedIndexChanged"></asp:DropDownList>
                          
                    </div>
                        <div class="form-group">
                            <label for="TextBoxForCity">Name:</label>
                            <asp:TextBox ID="TextBoxForCity" runat="server" CssClass="form-control" CausesValidation="False" EnableViewState="False"></asp:TextBox>
                        </div>
                 
                        <div>
                             <asp:Button ID="CityAddButton" runat="server" Text="Add" CssClass="btn btn-success" OnClick="CityAddButton_click" ValidationGroup="CityAddGroup" />
                        </div>
                       <div>
                        <span class="infoWarning"><asp:Literal ID="LiteralForAddCityWarning" runat="server"></asp:Literal></span>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorForAddCity" runat="server" ErrorMessage="City can not be empty" ValidationGroup="CityAddGroup" ControlToValidate="TextBoxForCity"></asp:RequiredFieldValidator>

                    </div>
                    </div>
                <div class="col-md-offset-2  col-md-5">
                  <div class="form-group">
                       <label for="LBForCity" >City List For:<%=SelectProvince %></label>
                      <asp:ListBox ID="LBForCity" runat="server" CssClass="form-control" Rows="8"></asp:ListBox>
                  </div>
                    <asp:Panel ID="PanelForDelEditCity" runat="server">
                    <div>
                    <asp:Button ID="CityDelButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="CityDelButton_Click" ValidationGroup="CityDelEditGroup" />
                    <asp:Button ID="CityEditButton" runat="server" Text="Edit" CssClass="btn btn-success" ValidationGroup="CityDelEditGroup" OnClick="CityEditButton_Click"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectCity" runat="server" ErrorMessage="Please select a city" ValidationGroup="CityDelEditGroup" ControlToValidate="LBForCity"></asp:RequiredFieldValidator>
                    </div>
                    </asp:Panel>
           <asp:Panel ID="PanelForUpdateCity" runat="server">
               <div class="form-group">
                   <label for="CityEditTextBox">Name:</label>
                <asp:TextBox ID="CityEditTextBox" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>
                 <asp:HiddenField ID="CityId" runat="server" />
                </div>
                    
                    <div>   
                        <asp:Button ID="CityUpdateButton" runat="server" Text="Update" CssClass="btn btn-success"  ValidationGroup="CityEditGroup" OnClick="CityUpdateButton_Click"/>
                    </div>
                 <div>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditCity" runat="server" ErrorMessage="City can not be empty" ValidationGroup="CityEditGroup" ControlToValidate="CityEditTextBox"></asp:RequiredFieldValidator>
                     </div>
               </asp:Panel>
                    <div> <span class="infoWarning"><asp:Literal ID="CityDelEditErrorWarning" runat="server" EnableViewState="False"></asp:Literal></span></div>
                </div>
            </div>
         </div>
     </div>
     <div class="row">
           <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">Neighbourhood List&nbsp<span class="badge"><asp:Literal ID="NeighborhoodCount" runat="server"></asp:Literal></span></h3>
            </div>
               <div class=" panel-body">
                <div class="col-md-5">
                    <div class="form-group">
                        <asp:DropDownList ID="DdlForNeighborhood" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlForNeighborhood_SelectedIndexChanged" ></asp:DropDownList>
                         
                    </div>
                
                   
                        <div class="form-group">
                              <label for="TextBoxForNeighborhood">Name:</label>
                            <asp:TextBox ID="TextBoxForNeighborhood" runat="server" CssClass="form-control" CausesValidation="False" ViewStateMode="Disabled"></asp:TextBox>
                        </div>
                      <div>
                           <asp:Button ID="NeighborhoodAddButton" runat="server" Text="Add" CssClass="btn btn-success" OnClick="NeighborhoodAddButton_Click" ValidationGroup="NeighborhoodAddGroup"/>
                        </div>
                        <div><span class="infoWarning"><asp:Literal ID="LiteralForAddNeighborhoodWarning" runat="server" EnableViewState="False"></asp:Literal></span>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorForNeighborhoodAdd" runat="server" ErrorMessage="Neighborhood can not be empty." ValidationGroup="NeighborhoodAddGroup" ControlToValidate="TextBoxForNeighborhood"></asp:RequiredFieldValidator>
                      </div>
                      
                    </div>
                <div class="col-md-offset-2  col-md-5">
                  <div class="form-group">
                      <label for="LBForNeighborhood" >Neighborhood List For:<%=SelectCity %></label>
                    <asp:ListBox ID="LBForNeighborhood" runat="server" CssClass="form-control" Rows="8" OnSelectedIndexChanged="LBForNeighborhood_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                  </div>
                     <asp:Panel ID="PanelForDelEditNeighborhood" runat="server">
                    <div>
                    <asp:Button ID="NeighborhoodDelButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="NeighborhoodDelButton_Click" ValidationGroup="neighborhoodDelEditGroup" />
                    <asp:Button ID="NeighborhoodEditButton" runat="server" Text="Edit" CssClass="btn btn-success" ValidationGroup="neighborhoodDelEditGroup" OnClick="NeighborhoodEditButton_Click"/>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectNeighborhood" runat="server" ErrorMessage="Please select a neighborhood" ValidationGroup="neighborhoodDelEditGroup" ControlToValidate="LBForNeighborhood"></asp:RequiredFieldValidator>
                    </div>
                    </asp:Panel>
                     <asp:Panel ID="PanelForUpdateNeighborhood" runat="server">
                        <div class="form-group">
                          <label for="NeighborhoodEditTextBox">Name:</label> 
                       <asp:TextBox ID="NeighborhoodEditTextBox" runat="server" CssClass="form-control"  EnableViewState="False"></asp:TextBox>
                            
                       <asp:HiddenField ID="NeighborhoodId" runat="server" />
                  </div>
                         <div class="form-group">
                             <label for="CheckBoxForNeighborhoodActive">Is Active:</label>
                            <asp:CheckBox ID="CheckBoxForNeighborhoodActive"  EnableViewState="False" runat="server" />
                         </div>
                   
                          <div>
                             <asp:Button ID="NeighborhoodUpdateButton" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="NeighborhoodEditGroup" OnClick="NeighborhoodUpdateButton_Click"/>
                          </div>
                     <div>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidatorForNeighborhoodEdit" runat="server" ErrorMessage="Neighborhood can not be empty" ValidationGroup="NeighborhoodEditGroup" ControlToValidate="NeighborhoodEditTextBox"></asp:RequiredFieldValidator>
                      </div>
                       </asp:Panel>
                          <div>  <span class="infoWarning"><asp:Literal ID="NeighborhoodDelEditErrorWarning" runat="server" EnableViewState="False"></asp:Literal></span> </div>
                  
                </div>
             
              
                   
                  
            </div>
         </div>
     </div>
     <div class="row">
            <div class="panel panel-primary">
            <div class="panel-heading">
                <h3 class="panel-title">FSA List</h3>
            </div>
            <div class=" panel-body">
                <div class="col-md-5">
                   
                    <div class="form-group input-group">
                       <%-- <label for="ListBox1">Mutiple select list (hold shift to select more than one):</label>
                        <asp:ListBox ID="ListBox1" runat="server" Rows="8" SelectionMode="Multiple" CssClass="form-control" ></asp:ListBox>--%>
                       <%-- <asp:DropDownList ID="DdlForFSA" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="DdlForFSA_SelectedIndexChanged" Visible="False"></asp:DropDownList>--%>
                           <%-- <div class="input-group-addon"><span class="badge"><asp:Literal ID="FSACount" runat="server" Visible="False"></asp:Literal></span></div>--%>
                        <asp:Literal ID="FSACount" runat="server" Visible="False"></asp:Literal>
                    </div>
                  
                        <div class="form-group">
                            <label for="TextBoxForFSA">Name:</label>
                            <asp:TextBox ID="TextBoxForFSA" runat="server" CssClass="form-control" CausesValidation="False" EnableViewState="False"></asp:TextBox>
                        </div>

                   <%--  <div>
                        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Visible="False"></asp:CheckBoxList>
                    </div>--%>
                     <div class="form-group input-group">
                        <label for="ListBox1">Mutiple select Neighborhood list (hold control to select more than one):</label>
                        <asp:ListBox ID="ListBox1" runat="server" Rows="8" SelectionMode="Multiple" CssClass="form-control" ></asp:ListBox>
                         </div>
                    <div>
                       <asp:Button ID="FSAAddButton" runat="server" Text="Add" CssClass="btn btn-success" OnClick="FSAAddButton_Click" ValidationGroup="AddFSAGroup"/>
                    </div>
                     <div>
                         <span class="infoWarning"><asp:Literal ID="LiteralForAddFsaWarning" runat="server" EnableViewState="False"></asp:Literal></span>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddFSA" runat="server" ErrorMessage="FSA can not be empty" ValidationGroup="AddFSAGroup" ControlToValidate="TextBoxForFSA"></asp:RequiredFieldValidator>
                      </div>
                    </div>
                <div class="col-md-offset-2  col-md-5">
                  <div class="form-group">
                        <label for="LBForFSA" >FSA List For:<%=SelectNeighborhood %></label>
                      <asp:ListBox ID="LBForFSA" runat="server" CssClass="form-control" Rows="8"></asp:ListBox>
                  </div>
                   <asp:Panel ID="PanelForDelEditFSA" runat="server">
                    <div>
                    <asp:Button ID="FSADelButton" runat="server" Text="Delete" CssClass="btn btn-danger" OnClick="FSADelButton_Click" ValidationGroup="FSADelEditGroup" />
                   <asp:Button ID="FSAEditButton" runat="server" Text="Edit" ValidationGroup="FSADelEditGroup" CssClass="btn btn-success" OnClick="FSAEditButton_Click" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSelectFSA" runat="server" ErrorMessage="Please select a FSA" ValidationGroup="FSADelEditGroup" ControlToValidate="LBForFSA"></asp:RequiredFieldValidator>
                    </div>
                 </asp:Panel>
                    <asp:Panel ID="PanelForUpdateFSA" runat="server">
                     <div class="form-group">
                           <label for="FSAEditTextBox">Name:</label>
                       <asp:TextBox ID="FSAEditTextBox" runat="server" CssClass="form-control" EnableViewState="False"></asp:TextBox>
                        <asp:HiddenField ID="FSAId" runat="server" /> 
                  </div>
                   
                        <div>
                             <asp:Button ID="FSAUpdateButton" runat="server" Text="Update" CssClass="btn btn-success" ValidationGroup="FSAEditGroup" OnClick="FSAUpdateButton_Click"  />
                             
                        </div>
                         <div>
                       
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorFSAEdit" runat="server" ErrorMessage="FSA can not be empty" ControlToValidate="FSAEditTextBox" ValidationGroup="FSAEditGroup"></asp:RequiredFieldValidator>
                    </div>
                   </asp:Panel>
                    <div> <span class="infoWarning"><asp:Literal ID="FSADelEditErrorWarning" runat="server" EnableViewState="False"></asp:Literal></span></div>
                </div>
            </div>

           </div>
     </div>
   <script type="text/javascript">
       $(document).ready(function () {

           $(":submit").click(function () {

               $(".infoWarning").html("");
           }
           );
           var $cityTextBox = $("#<%=TextBoxForCity.ClientID%>");
           var $cityAddBn = $("#<%=CityAddButton.ClientID%>");
           var $cityEditBn = $("#<%=CityEditButton.ClientID%>");
           var $cityDelBn = $("#<%=CityDelButton.ClientID%>")
           var $neighborhoodTextBox = $("#<%=TextBoxForNeighborhood.ClientID%>");
           var $neighborhoodAddBn = $("#<%=NeighborhoodAddButton.ClientID%>");
           var $neighborhoodEditBn = $("#<%=NeighborhoodEditButton.ClientID%>");
           var $neighborhoodDelBn = $("#<%=NeighborhoodDelButton.ClientID%>");
           var $fsaTextBox = $("#<%=TextBoxForFSA.ClientID%>");
           var $fsaAddBn = $("#<%=FSAAddButton.ClientID%>");
           var $fsaEditBn = $("#<%=FSAEditButton.ClientID%>");
           var $fsaDelBn = $("#<%=FSADelButton.ClientID%>");
           
           var $dplForcity = $("#<%=DdlforCity.ClientID%>").val();
           var $dplForNeighborhood = $("#<%=DdlForNeighborhood.ClientID%>").val();
           <%--    var $dplForFsa = $("#<%=DdlForFSA.ClientID%>").val();--%>
           var $ListBox1 = $("#<%=ListBox1.ClientID%>").val();
           if ($ListBox1 === null) {
              

           }
 
           if ($dplForcity === null) {
               $cityTextBox.attr("disabled", "disabled");
               $cityAddBn.attr("disabled", "disabled");
               $cityEditBn.attr("disabled", "disabled");
               $cityDelBn.attr("disabled", "disabled");
           }
           if ($dplForNeighborhood === null) {
               $neighborhoodTextBox.attr("disabled", "disabled");
               $neighborhoodAddBn.attr("disabled", "disabled");
               $neighborhoodEditBn.attr("disabled", "disabled");
               $neighborhoodDelBn.attr("disabled", "disabled");
               $fsaTextBox.attr("disabled", "disabled");
               $fsaAddBn.attr("disabled", "disabled");
           }
           //if ($dplForFsa === null) {
           //    $fsaTextBox.attr("disabled", "disabled");
           //    $fsaAddBn.attr("disabled", "disabled");
           //    $fsaEditBn.attr("disabled", "disabled");
           //    $fsaDelBn.attr("disabled", "disabled");
           //}
         
          

       });

    <%if(CountryEditTextBox.Visible==true) {%>

       $(document).ready(function () {
           var $LBForCountry = $("#<%=LBForCountry.ClientID%>");
            var $CountryEditTextBox = $("#<%=CountryEditTextBox.ClientID%>");
            var $countryId = $("#<%=countryId.ClientID%>");


            $LBForCountry.change(function () {
                $CountryEditTextBox.val($LBForCountry.find('option:selected').text());
                $countryId.val($LBForCountry.find('option:selected').val());

            });
        });
       <%}%>
        <%if (ProvinceEditTextBox.Visible==true) {%>

       $(document).ready(function () {
           var $LBForProvince = $("#<%=LBForProvince.ClientID%>");
            var $ProvinceEditTextBox = $("#<%=ProvinceEditTextBox.ClientID%>");
            var $ProvinceId = $("#<%=ProvinceId.ClientID%>");
            $LBForProvince.change(function () {
                $ProvinceEditTextBox.val($LBForProvince.find('option:selected').text());
                $ProvinceId.val($LBForProvince.find('option:selected').val());

            });
        });


       <%}%>
           <%if (CityEditTextBox.Visible==true) {%>

       $(document).ready(function () {
           var $LBForCity = $("#<%=LBForCity.ClientID%>");
           var $CityEditTextBox = $("#<%=CityEditTextBox.ClientID%>");
           var $CityId = $("#<%=CityId.ClientID%>");
           $LBForCity.change(function () {
               $CityEditTextBox.val($LBForCity.find('option:selected').text());
               $CityId.val($LBForCity.find('option:selected').val());
           });
       });


       <%}%>
<%--              <%if (NeighborhoodEditTextBox.Visible==true) {%>

       $(document).ready(function () {
           var $LBForNeighborhood = $("#<%=LBForNeighborhood.ClientID%>");
           var $NeighborhoodEditTextBox = $("#<%=NeighborhoodEditTextBox.ClientID%>");
           var $NeighborhoodId = $("#<%=NeighborhoodId.ClientID%>");
           $LBForNeighborhood.change(function () {
               $NeighborhoodEditTextBox.val($LBForNeighborhood.find('option:selected').text());
               $NieghborhoodId.val($LBForNeighborhood.find('option:selected').val());
           });
       });


       <%}%>--%>
             <%if (FSAEditTextBox.Visible==true) {%>

       $(document).ready(function () {
           var $LBForFSA = $("#<%=LBForFSA.ClientID%>");
           var $FSAEditTextBox = $("#<%=FSAEditTextBox.ClientID%>");
           var $FSAId = $("#<%=FSAId.ClientID%>");
           $LBForFSA.change(function () {
               $FSAEditTextBox.val($LBForFSA.find('option:selected').text());
               $FSAId.val($LBForFSA.find('option:selected').val());
           });
       });


       <%}%>
    </script>
</asp:Content>
