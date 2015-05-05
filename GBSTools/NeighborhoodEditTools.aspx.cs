using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EdgeD3;
using OrderitTables;
using GBSTools.Models;

namespace GBSTools
{
    public partial class NeighborhoodEditTools : System.Web.UI.Page
    {
       
        private ProvinceRepository _provinceRepository;
        private NeighborhoodRepository _neighborhoodRepository;
        private CityRepository _cityRepository;
      
        public NeighborhoodEditTools()
        {
        
        
            _provinceRepository = new ProvinceRepository();
            _neighborhoodRepository = new NeighborhoodRepository();
            _cityRepository = new CityRepository();
       
          }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                var prov = _provinceRepository.GetProvincesByCountryId("CA");

                DropDownListForProvince.DataSource = prov;
                DropDownListForProvince.DataValueField = "Id";
                DropDownListForProvince.DataTextField = "Name";
                DropDownListForProvince.DataBind();
                if (prov.Count > 0)
                {
                    ListItem item = new ListItem("-Select Province-", "-1");
                    DropDownListForProvince.Items.Insert(0, item);
                }
            }

        }

        protected void DropDownListForProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            string i = DropDownListForProvince.SelectedValue;
            if (!string.IsNullOrEmpty(i) && i != "-1")
            {
                var ci = _cityRepository.GetCitiesByProvinceId(i);

                DropDownListForCity.DataSource = ci;
                DropDownListForCity.DataValueField = "Id";
                DropDownListForCity.DataTextField = "Name";

                DropDownListForCity.DataBind();
             
                if (ci.Count > 0)
                {
                    ListItem item = new ListItem("-Select City-", "-1");
                    DropDownListForCity.Items.Insert(0, item);
                }
            }
        }

        protected void DropDownListForCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            string i = DropDownListForCity.SelectedValue;
            if (i != "-1")
            {
                var neighborhoodList = _neighborhoodRepository.GetAllByCityId(i);

                ListViewForNeighborhood.DataSource = neighborhoodList;
                ListViewForNeighborhood.DataBind();
                
               
            }

        }

        protected void ListViewForNeighborhood_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected void ListViewForNeighborhood_ItemEditing(object sender, ListViewEditEventArgs e)
        {
           
            string i = DropDownListForCity.SelectedValue;
            if (i != "-1")
            {
                var neighborhoodList = _neighborhoodRepository.GetAllByCityId(i);

                ListViewForNeighborhood.DataSource = neighborhoodList;
                ListViewForNeighborhood.EditIndex = e.NewEditIndex;
                ListViewItem item = ListViewForNeighborhood.Items[e.NewEditIndex];
                ListViewForNeighborhood.DataBind();


            }

         
        }

        protected void ListViewForNeighborhood_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            string i = DropDownListForCity.SelectedValue;
            if (i != "-1")
            {
                var neighborhoodList = _neighborhoodRepository.GetAllByCityId(i);

                ListViewForNeighborhood.DataSource = neighborhoodList;
                ListViewForNeighborhood.EditIndex = -1;
                ListViewForNeighborhood.DataBind();


            }
          
        }

        protected void ListViewForNeighborhood_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            string Id = (ListViewForNeighborhood.DataKeys[e.ItemIndex].Value.ToString());

            ListViewItem item = ListViewForNeighborhood.Items[e.ItemIndex];

            TextBox tC = (TextBox)item.FindControl("cityTextBox");

            CheckBox cA = (CheckBox)item.FindControl("activeCheckBox");

            HiddenField cityId = (HiddenField)item.FindControl("HiddenFieldForCId");
            Neighborhood neigh = new Neighborhood();
            neigh.Id = Id;
            neigh.Active = cA.Checked;
            neigh.Name = tC.Text.Trim();
            neigh.SeoName = UrlSafe_SEOName(neigh.Name);
            neigh.CityId = cityId.Value;
            string i = DropDownListForCity.SelectedValue;
            if (_neighborhoodRepository.Update(neigh))
            {
                ListViewForNeighborhood.EditIndex = -1;
                var neighborhoodList = _neighborhoodRepository.GetAllByCityId(i);

                ListViewForNeighborhood.DataSource = neighborhoodList;
                ListViewForNeighborhood.EditIndex = -1;
                ListViewForNeighborhood.DataBind();
            }


        }
        public static string UrlSafe_SEOName(string textString)
        {
            string urlSafeString = textString.Replace(@"/", "-");
            ;
            urlSafeString = urlSafeString.Replace("//", "-");
            urlSafeString = urlSafeString.Replace("&", "-");
            urlSafeString = urlSafeString.Replace(" ", "-");
            urlSafeString = urlSafeString.Replace("?", "-");

            urlSafeString = urlSafeString.Replace("%", "-");
            urlSafeString = urlSafeString.Replace(" ", "-");
            urlSafeString = HttpUtility.UrlEncode(urlSafeString);
            return urlSafeString;
        }
       
    }
}