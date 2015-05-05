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
    public partial class GBSTools : System.Web.UI.Page
    {
        #region private variable
        private CountryRepository _countryRepository;
        private ProvinceRepository _provinceRepository;
        private NeighborhoodRepository _neighborhoodRepository;
        private CityRepository _cityRepository;
        private FSARepository _fsaRepository;
        public string SelectCountry
        { 
            get {

           if(DpListForCountry.SelectedItem!=null&&DpListForCountry.SelectedValue!="-1")
              {return DpListForCountry.SelectedItem.Text;}
         return "";
        }
       }

        public string SelectProvince 
        {
            get {

                if (DdlForProvince.SelectedItem != null && DdlForProvince.SelectedValue != "-1")
                {
                    return DdlForProvince.SelectedItem.Text;
                }
                return "";
            
            }
        
        }
        public string SelectCity
        {
            get { 
            
            if(DdlforCity.SelectedItem!=null&&DdlforCity.SelectedItem.Value!="-1")
            {
                return DdlforCity.SelectedItem.Text;
            }
            return "";
            
            }
        
        }

        public string SelectNeighborhood
        {
            get {

                if (DdlForNeighborhood.SelectedItem != null && DdlForNeighborhood.SelectedValue != "-1")
                {
                    return DdlForNeighborhood.SelectedItem.Text;
                }
                return "";
            }
        }
        #endregion
        public GBSTools()
        {

            _countryRepository = new CountryRepository();
            _provinceRepository = new ProvinceRepository();
            _neighborhoodRepository = new NeighborhoodRepository();
            _cityRepository = new CityRepository();
            _fsaRepository = new FSARepository();
        
        }
        protected void Page_Load(object sender, EventArgs e)
        {

         
            if (!IsPostBack)
            {


                BindingCountry();
                SetUpdateDisplay();

            }
        }
        #region country section eventhanlers

        /// <summary>
        /// Add new country
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CountryAddButton_click(object sender, EventArgs e)
        {
            var name = TextBoxForCountry.Text.Trim();
            var Id = TextBoxForCountryId.Text.Trim();
            bool IsduplicateName = _countryRepository.IsDuplicateName(name, string.Empty);
            bool IsDuplicatId = _countryRepository.IsDuplicateId(Id);
            if (IsduplicateName)
            {
                LiteralForAddCountryWarning.Text = "Duplicated Country Name";
            }
            if (IsDuplicatId)
            {

                LiteralForAddCountryWarning.Text = "Duplicated Country Id";

            }
            if (Page.IsValid && !IsduplicateName && !IsDuplicatId)
            {

                Country c = new Country();
                c.Name = name;
                c.SeoName = UrlSafe_SEOName(name);
                c.Id = Id;
                c.Active = true;
                bool result = _countryRepository.Insert(c);
                if (result)
                {
                    LiteralForAddCountryWarning.Text = "Add a new country " + c.Name + " successfully";
                    TextBoxForCountry.Text = "";
                    BindingCountry();
                }
                else
                {
                    TextBoxForCountry.Text = c.Name;
                    LiteralForAddCountryWarning.Text = "Failed to add a new country " + c.Name;
                }
            }
            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CountryDelButton_click(object sender, EventArgs e)
        {

            var id = LBForCountry.SelectedValue;
            var item = LBForCountry.SelectedItem;

            if (!string.IsNullOrEmpty(id))
            {
                var province = _provinceRepository.GetProvincesByCountryId(id);
                if (province.Count <= 0)
                {



                    bool result = _countryRepository.Delete(id);
                    if (result)
                    {
                        BindingCountry();
                        CountryDelEditErrorWarning.Text = "Succeed to delete " + item.Text;
                    }
                    else
                        CountryDelEditErrorWarning.Text = "Fail to delete " + item.Text;

                }
                else
                    CountryDelEditErrorWarning.Text = "There are provinces belong to it, you can not delete it.";

            }
            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CountryUpdateButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            string Id = countryId.Value;
            if (!string.IsNullOrEmpty(Id))
            {
                Country country = _countryRepository.GetCountryById(Id);
                if (country != null)
                {
                    country.Id = Id;
                    country.Name = CountryEditTextBox.Text.Trim();
                    country.SeoName = UrlSafe_SEOName(country.Name);
                    bool IsDuplicate = _countryRepository.IsDuplicateName(country.Name, Id);
                    if (!IsDuplicate)
                    {
                        bool result = _countryRepository.Update(country);
                        if (result)
                        {
                            BindingCountry();
                            LBForCountry.SelectedValue = Id;
                            CountryDelEditErrorWarning.Text = country.Name + " was updated successfully";

                        }
                        else
                        {
                            PanelForDelEditCountry.Visible = false;
                            PanelForUpdateCountry.Visible = true;
                            CountryDelEditErrorWarning.Text = "Failed to update " + country.Name;
                        }
                    }
                    else
                    {
                        PanelForDelEditCountry.Visible = false;
                        PanelForUpdateCountry.Visible = true;
                        CountryDelEditErrorWarning.Text = "Duplicate Country Name:" + country.Name;
                    }
                }


            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DpListForCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            Setup(DdlChange.Country_Change);
            SetUpdateDisplay();
            try
            {
                string i = DpListForCountry.SelectedValue;
                if (!string.IsNullOrEmpty(i) && i != "-1")
                {
                    BindingProvince(i);
                }

            }


            catch (Exception exc)
            {
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void CountryEditButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            if (LBForCountry.SelectedItem != null)
            {
                PanelForDelEditCountry.Visible = false;
                PanelForUpdateCountry.Visible = true;
                countryId.Value = LBForCountry.SelectedItem.Value;
                CountryEditTextBox.Text = LBForCountry.SelectedItem.Text;

            }


        }

        private void BindingCountry()
        {
            var countryList = _countryRepository.GetAll();
            DpListForCountry.DataSource = countryList;
            DpListForCountry.DataValueField = "Id";
            DpListForCountry.DataTextField = "Name";
            //ListItem item = new ListItem("-Select Country-", "-1");
            //DpListForCountry.Items.Insert(0, item);
            CountryCount.Text = countryList.Count().ToString();
            DpListForCountry.DataBind();
            LBForCountry.DataSource = countryList;
            LBForCountry.DataValueField = "Id";
            LBForCountry.DataTextField = "Name";
            LBForCountry.DataBind();

            if (countryList.Count > 0)
            {
                BindingProvince(countryList.First().Id);
            }
            

        }
        private void BindingCountry(string id)
        {
            BindingCountry();
            if (!string.IsNullOrEmpty(id))
            {
                DpListForCountry.SelectedValue = id.ToString();
            }

        }
        #endregion

        #region Province section eventhanlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlForProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setup(DdlChange.Province_Change);
            SetUpdateDisplay();
            try
            {
                string i = DdlForProvince.SelectedValue;
                if (!string.IsNullOrEmpty(i) && i != "-1")
                {
                    BindingCity(i);
                }
                else
                {

                    DdlforCity.Items.Clear();
                    LBForCity.Items.Clear();
                }

            }

            catch (Exception exc)
            { }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        private void BindingCity(string i)
        {
            var ci = _cityRepository.GetCitiesByProvinceId(i);
            //ci.AddRange(GetAllCities());
            DdlforCity.DataSource = ci;
            DdlforCity.DataValueField = "Id";
            DdlforCity.DataTextField = "Name";

            LBForCity.DataSource = ci;
            LBForCity.DataTextField = "Name";
            LBForCity.DataValueField = "Id";

            CityCount.Text = ci.Count().ToString();
            LBForCity.DataBind();
            DdlforCity.DataBind();
            if (ci.Count > 0)
            {
                ListItem item = new ListItem("-Select City-", "-1");
                DdlforCity.Items.Insert(0, item);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProvinceAddButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            if (Page.IsValid)
            {
                Province province = new Province();
                province.Name = TextBoxForProvince.Text.Trim();
                province.SeoName = UrlSafe_SEOName(province.Name);
                province.CountryId = DpListForCountry.SelectedValue;
                province.Id = TextBoxForProvinceId.Text.Trim();
                province.Active = true;
                bool IsduplicateName = _provinceRepository.IsDuplicateName(province.Name, province.CountryId, string.Empty);
                bool IsDuplicateId = _provinceRepository.IsDuplicateId(province.Id);
                if (IsduplicateName)
                {
                    LiteralForAddProvinceWarning.Text = "Duplicated Province Name";

                }

                else if (IsDuplicateId)
                {
                    LiteralForAddProvinceWarning.Text = "Duplicated Province Id";
                }
                else
                {
                    var result = _provinceRepository.Insert(province);
                    if (result)
                    {
                        BindingProvince(province.CountryId);
                        LiteralForAddProvinceWarning.Text = "Add a new province " + province.Name + " successfully";
                        TextBoxForProvince.Text = "";
                    }
                    else
                    {
                        LiteralForAddProvinceWarning.Text = "Failed to add a new province " + province.Name;
                        TextBoxForProvince.Text = province.Name;
                    }
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProviceDelButton_Click(object sender, EventArgs e)
        {
            var id = LBForProvince.SelectedValue;
            var item = LBForProvince.SelectedItem;
            var countryId = DpListForCountry.SelectedValue;
            if (!string.IsNullOrEmpty(id))
            {
                var cities = _cityRepository.GetCitiesByProvinceId(id);
                if (cities.Count <= 0)
                {
                    bool result = _provinceRepository.Delete(id);
                    if (result)
                    {
                        BindingProvince(countryId);
                        ProvinceDelEditErrorWarning.Text = "Succeed to delete " + item.Text;
                    }
                    else
                        ProvinceDelEditErrorWarning.Text = "Fail to delete " + item.Text;
                }
                else
                    ProvinceDelEditErrorWarning.Text = "There are cities belong to it, you can not delete it.";
            }
            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProvinceEditButton_Click(object sender, EventArgs e)
        {

            SetUpdateDisplay();
            if (LBForProvince.SelectedItem != null)
            {
                ProvinceEditTextBox.Text = LBForProvince.SelectedItem.Text;
                ProvinceId.Value = LBForProvince.SelectedItem.Value;
                PanelForUpdateProvince.Visible = true;
                PanelForDelEditProvince.Visible = false;



            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ProvinceUpdateButton_Command(object sender, CommandEventArgs e)
        {

            SetUpdateDisplay();
            var name = ProvinceEditTextBox.Text.Trim();
            var Id = ProvinceId.Value;
            var countryId = DpListForCountry.SelectedValue;
            if (!string.IsNullOrEmpty(Id))
            {
                Province province = _provinceRepository.GetProvinceById(Id);
                if (province != null)
                {
                    province.Name = name;
                    province.SeoName = UrlSafe_SEOName(name);
                    bool IsDuplicate = _provinceRepository.IsDuplicateName(province.Name, province.CountryId, province.Id);
                    if (!IsDuplicate)
                    {
                        bool result = _provinceRepository.Update(province);
                        if (result)
                        {

                            BindingProvince(countryId);
                            LBForProvince.SelectedValue = Id;
                            ProvinceDelEditErrorWarning.Text = name + " was updated successfully";

                        }
                        else
                        {

                            PanelForDelEditProvince.Visible = false;
                            PanelForUpdateProvince.Visible = true;
                            ProvinceDelEditErrorWarning.Text = "Failed to update " + name;
                        }

                    }
                    else
                    {
                        PanelForDelEditProvince.Visible = false;
                        PanelForUpdateProvince.Visible = true;
                        ProvinceDelEditErrorWarning.Text = "Duplicate Province Name:" + province.Name;

                    }

                }


            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        private void BindingProvince(string i)
        {
            var prov = _provinceRepository.GetProvincesByCountryId(i);

            LBForProvince.DataSource = prov;
            DdlForProvince.DataSource = prov;
            DdlForProvince.DataValueField = "Id";
            DdlForProvince.DataTextField = "Name";
            LBForProvince.DataValueField = "Id";
            LBForProvince.DataTextField = "Name";
            ProvinceCount.Text = prov.Count().ToString();
            LBForProvince.DataBind();
            DdlForProvince.DataBind();
            if (prov.Count > 0)
            {
                ListItem item = new ListItem("-Select Province-", "-1");
                DdlForProvince.Items.Insert(0, item);
            }

        }
        #endregion

        #region city section eventhandlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CityAddButton_click(object sender, EventArgs e)
        {

            if (Page.IsValid)
            {
                City city = new City();
                city.Name = TextBoxForCity.Text.Trim();
                city.SeoName = UrlSafe_SEOName(city.Name);
                city.ProvinceId = DdlForProvince.SelectedValue;
                city.Active = true;
                bool IsDuplicate = _cityRepository.IsDuplicate(city.Name, city.ProvinceId, string.Empty);
                if (IsDuplicate)
                {
                    LiteralForAddCityWarning.Text = "Duplicated City Name";
                }
                else
                {
                    var result = _cityRepository.Insert(city);
                    if (result)
                    {
                        BindingCity(city.ProvinceId);
                        LiteralForAddCityWarning.Text = "Add a new city " + city.Name + " successfully";
                        TextBoxForCity.Text = "";
                    }
                    else
                    {
                        LiteralForAddCityWarning.Text = "Failed to add new city " + city.Name;
                        TextBoxForCity.Text = city.Name;

                    }

                }

            }
            SetUpdateDisplay();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CityDelButton_Click(object sender, EventArgs e)
        {

            var item = LBForCity.SelectedItem;
            var provinceId = DdlForProvince.SelectedValue;
            if (!string.IsNullOrEmpty(item.Value))
            {
                var neighborhoods = _neighborhoodRepository.GetAllByCityId(item.Value);
                if (neighborhoods.Count <= 0)
                {
                    bool result = _cityRepository.Delete(item.Value);
                    if (result)
                    {
                        BindingCity(provinceId);
                        CityDelEditErrorWarning.Text = "Succeed to delete " + item.Text;
                    }
                    else
                    {
                        CityDelEditErrorWarning.Text = "Fail to delete " + item.Text;
                    }
                }
                else
                    CityDelEditErrorWarning.Text = "There are neighbourhood belong to it, you can not delete it.";

            }
            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CityEditButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            if (LBForCity.SelectedItem != null)
            {
                CityEditTextBox.Text = LBForCity.SelectedItem.Text;
                CityId.Value = LBForCity.SelectedItem.Value;
                PanelForDelEditCity.Visible = false;
                PanelForUpdateCity.Visible = true;

            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CityUpdateButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            var name = CityEditTextBox.Text.Trim();
            var Id = CityId.Value;
            var provinceId = DdlForProvince.SelectedValue;
            if (!string.IsNullOrEmpty(Id))
            {
                City city = _cityRepository.GetCityById(Id);
                if (city != null)
                {
                    city.Name = name;
                    city.SeoName = UrlSafe_SEOName(name);
                    bool IsDuplicate = _cityRepository.IsDuplicate(name, provinceId, Id);
                    if (!IsDuplicate)
                    {
                        bool result = _cityRepository.Update(city);
                        if (result)
                        {
                            BindingCity(provinceId);
                            LBForCity.SelectedValue = Id;
                            CityDelEditErrorWarning.Text = name + " was updated successfully";
                        }
                        else
                        {
                            PanelForDelEditCity.Visible = true;
                            PanelForUpdateCity.Visible = false;
                            CityDelEditErrorWarning.Text = "Failed to update " + name;
                        }
                    }
                    else
                    {
                        PanelForDelEditCity.Visible = false;
                        PanelForUpdateCity.Visible = true;
                        CityDelEditErrorWarning.Text = "Duplicate City Name:" + name;

                    }


                }

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlforCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setup(DdlChange.City_Change);
            SetUpdateDisplay();


            try
            {
                string i = DdlforCity.SelectedValue;
                if (i != "-1")
                {

                    BindingNeighborhood(i);
                }
                else
                {

                    LBForNeighborhood.Items.Clear();
                    DdlForNeighborhood.Items.Clear();
                }

            }
            catch (Exception exc)
            { }

        }


        #endregion
        #region neighborhood section eventhandlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlForNeighborhood_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setup(DdlChange.Neighhood_Change);
            SetUpdateDisplay();
            try
            {
                string i = DdlForNeighborhood.SelectedValue;
                if (!string.IsNullOrEmpty(i) && i != "-1")
                {
                    ListBox1.SelectedValue = i;
                    BindingFSA(i);
                }
                else
                {

                    LBForFSA.Items.Clear();

                }

            }
            catch { }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NeighborhoodAddButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Neighborhood neighborhood = new Neighborhood();
                neighborhood.Name = TextBoxForNeighborhood.Text.Trim();
                neighborhood.SeoName = UrlSafe_SEOName(neighborhood.Name);
                neighborhood.CityId = DdlforCity.SelectedValue;
                neighborhood.Active = true;
                bool IsDuplicate = _neighborhoodRepository.IsDuplicate(neighborhood.Name, neighborhood.CityId, string.Empty);
                if (IsDuplicate)
                {
                    LiteralForAddNeighborhoodWarning.Text = "Duplicated Neighborhood Name";
                }
                else
                {
                    var result = _neighborhoodRepository.Insert(neighborhood);
                    if (result)
                    {
                        BindingNeighborhood(neighborhood.CityId);
                        LiteralForAddNeighborhoodWarning.Text = "Add a new neighborhood " + neighborhood.Name + " successfully";
                        TextBoxForNeighborhood.Text = "";
                    }
                    else
                    {
                        LiteralForAddNeighborhoodWarning.Text = "Failed to add new neighborhood " + neighborhood.Name;
                        TextBoxForNeighborhood.Text = neighborhood.Name;

                    }

                }

            }
            SetUpdateDisplay();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NeighborhoodDelButton_Click(object sender, EventArgs e)
        {
            var item = LBForNeighborhood.SelectedItem;
            var cityId = DdlforCity.SelectedValue;
            if (!string.IsNullOrEmpty(item.Value))
            {
                var fsas = _fsaRepository.GetAllByNeighborhoodId(item.Value);
                if (fsas.Count <= 0)
                {
                    bool result = _neighborhoodRepository.Delete(item.Value);
                    if (result)
                    {
                        BindingNeighborhood(cityId);
                        NeighborhoodDelEditErrorWarning.Text = "Succeed to delete " + item.Text;
                    }
                    else
                    {
                        NeighborhoodDelEditErrorWarning.Text = "Fail to delete " + item.Text;
                    }
                }
                else
                    NeighborhoodDelEditErrorWarning.Text = "There are FSA belong to it, you can not delete it.";

            }

            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NeighborhoodEditButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            if (LBForNeighborhood.SelectedItem != null)
            {
                Neighborhood neigh = _neighborhoodRepository.GetNeighborhoodById(LBForNeighborhood.SelectedItem.Value);
                NeighborhoodEditTextBox.Text = LBForNeighborhood.SelectedItem.Text;
                NeighborhoodId.Value = LBForNeighborhood.SelectedItem.Value;
                CheckBoxForNeighborhoodActive.Checked = neigh.Active;
                PanelForDelEditNeighborhood.Visible = false;
                PanelForUpdateNeighborhood.Visible = true;


            }

        }
        protected void LBForNeighborhood_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            if (LBForNeighborhood.SelectedItem != null)
            {
                if (!string.IsNullOrEmpty(NeighborhoodEditTextBox.Text))
                {
                    Neighborhood neigh = _neighborhoodRepository.GetNeighborhoodById(LBForNeighborhood.SelectedItem.Value);
                    NeighborhoodEditTextBox.Text = LBForNeighborhood.SelectedItem.Text;
                    NeighborhoodId.Value = LBForNeighborhood.SelectedItem.Value;
                    CheckBoxForNeighborhoodActive.Checked = neigh.Active;
                    PanelForDelEditNeighborhood.Visible = false;
                    PanelForUpdateNeighborhood.Visible = true;
                }
                else
                {
                    PanelForDelEditNeighborhood.Visible = true;
                    PanelForUpdateNeighborhood.Visible = false;
                }


            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NeighborhoodUpdateButton_Click(object sender, EventArgs e)
        {

            SetUpdateDisplay();
            var name = NeighborhoodEditTextBox.Text.Trim();
            var Id = NeighborhoodId.Value;
            var cityId = DdlforCity.SelectedValue;
            if (!string.IsNullOrEmpty(Id))
            {
                Neighborhood neighborhood = _neighborhoodRepository.GetNeighborhoodById(Id);
                if (neighborhood != null)
                {
                    neighborhood.Name = name;
                    neighborhood.SeoName = UrlSafe_SEOName(name);
                    neighborhood.Active = CheckBoxForNeighborhoodActive.Checked;
                    bool IsDuplicate = _neighborhoodRepository.IsDuplicate(name, cityId, Id);
                    if (!IsDuplicate)
                    {
                        bool result = _neighborhoodRepository.Update(neighborhood);
                        if (result)
                        {
                            BindingNeighborhood(cityId);
                            LBForNeighborhood.SelectedValue = Id;
                            NeighborhoodDelEditErrorWarning.Text = name + " was updated successfully";
                        }
                        else
                        {
                            PanelForDelEditNeighborhood.Visible = false;
                            PanelForUpdateNeighborhood.Visible = true;
                            NeighborhoodDelEditErrorWarning.Text = "Failed to update " + name;
                        }
                    }
                    else
                    {
                        PanelForDelEditNeighborhood.Visible = false;
                        PanelForUpdateNeighborhood.Visible = true;
                        NeighborhoodDelEditErrorWarning.Text = "Duplicate neighborhood Name:" + name;

                    }


                }

            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        private void BindingFSA(string i)
        {
            var ci = _fsaRepository.GetAllByNeighborhoodId(i);
            LBForFSA.DataSource = ci;
            LBForFSA.DataValueField = "Id";
            LBForFSA.DataTextField = "Name";
            FSACount.Text = ci.Count().ToString();
            LBForFSA.DataBind();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        private void BindingNeighborhood(string i)
        {
            var neighborhoodList = _neighborhoodRepository.GetAllByCityId(i);
            DdlForNeighborhood.DataSource = neighborhoodList;
            LBForNeighborhood.DataSource = neighborhoodList;
            ListBox1.DataSource = neighborhoodList;
            ListBox1.DataTextField = "Name";
            ListBox1.DataValueField = "Id";
            ListBox1.DataBind();
            //CheckBoxList1.DataSource = neighborhoodList;
            //CheckBoxList1.DataTextField = "Name";
            //CheckBoxList1.DataValueField = "Id";
            //CheckBoxList1.DataBind();
            DdlForNeighborhood.DataValueField = "Id";
            DdlForNeighborhood.DataTextField = "Name";
            LBForNeighborhood.DataValueField = "Id";
            LBForNeighborhood.DataTextField = "Name";
            NeighborhoodCount.Text = neighborhoodList.Count().ToString();
            LBForNeighborhood.DataBind();
            DdlForNeighborhood.DataBind();

            ListItem item = new ListItem("-Select Neighbour -", "-1");
            DdlForNeighborhood.Items.Insert(0, item);
        }

        #endregion

        #region FSA section eventhandlers
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DdlForFSA_SelectedIndexChanged(object sender, EventArgs e)
        {
            Setup(DdlChange.FSA_Change);
            SetUpdateDisplay();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FSAEditButton_Click(object sender, EventArgs e)
        {

            SetUpdateDisplay();
            if (LBForFSA.SelectedItem != null)
            {
                FSAEditTextBox.Text = LBForFSA.SelectedItem.Text;
                FSAId.Value = LBForFSA.SelectedItem.Value;
                PanelForDelEditFSA.Visible = false;
                PanelForUpdateFSA.Visible = true;


            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FSAUpdateButton_Click(object sender, EventArgs e)
        {
            SetUpdateDisplay();
            var name = FSAEditTextBox.Text;
            var Id = FSAId.Value;
            var neighborhoodId = DdlForNeighborhood.SelectedValue;
            if (!string.IsNullOrEmpty(Id))
            {
                FSA Fsa = _fsaRepository.GetFSAById(Id);
                if (Fsa != null)
                {
                    Fsa.Name = name;
                    Fsa.SeoName = UrlSafe_SEOName(name);
                    bool IsDuplicate = _fsaRepository.IsDuplicate(name, Id);
                    if (!IsDuplicate)
                    {
                        bool result = _fsaRepository.Update(Fsa);
                        if (result)
                        {
                            BindingFSA(neighborhoodId);
                            LBForFSA.SelectedValue = Id;
                            FSADelEditErrorWarning.Text = name + " was updated successfully";
                        }
                        else
                        {
                            PanelForDelEditFSA.Visible = false;
                            PanelForUpdateFSA.Visible = true;
                            FSADelEditErrorWarning.Text = "Failed to update " + name;
                        }
                    }
                    else
                    {
                        PanelForDelEditFSA.Visible = false;
                        PanelForUpdateFSA.Visible = true;
                        FSADelEditErrorWarning.Text = "Duplicate FSA Name:" + name;

                    }


                }

            }



        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FSAAddButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                FSA Fsa = new FSA();
                Fsa.Name = TextBoxForFSA.Text.Trim();
                Fsa.SeoName = UrlSafe_SEOName(Fsa.Name);
               // Fsa.NeighborhoodId = DdlForNeighborhood.SelectedValue;
                Fsa.Active = true;
                bool IsDuplicate = _fsaRepository.IsDuplicate(Fsa.Name, string.Empty);
                if (IsDuplicate)
                {
                    LiteralForAddFsaWarning.Text = "Duplicated Fsa Name";
                }
                else
                {

                    foreach (ListItem listItem in ListBox1.Items)
                    {
                        if (listItem.Selected == true)
                        {
                            Fsa.NeighborhoodIds.Add(listItem.Value);
                        }
                    }
                    var result = _fsaRepository.Insert(Fsa);
                    if (result)
                    {
                        BindingFSA(DdlForNeighborhood.SelectedValue);
                        LiteralForAddFsaWarning.Text = "Add a new Fsa " + Fsa.Name + " successfully";
                        TextBoxForFSA.Text = "";
                    }
                    else
                    {
                        LiteralForAddFsaWarning.Text = "Failed to add new Fsa " + Fsa.Name;
                        TextBoxForFSA.Text = Fsa.Name;

                    }

                }

            }
            SetUpdateDisplay();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FSADelButton_Click(object sender, EventArgs e)
        {
            var item = LBForFSA.SelectedItem;
            var neighborhoodId = DdlForNeighborhood.SelectedValue;
            if (!string.IsNullOrEmpty(item.Value))

            {


              //  bool result = _fsaRepository.Delete(item.Value);
                bool result=_fsaRepository.RemoveFSAByNeighborhoodId(item.Value,neighborhoodId);
                if (result)
                {
                    BindingFSA(neighborhoodId);
                    FSADelEditErrorWarning.Text = "Succeed to delete " + item.Text;
                }
                else
                {
                    FSADelEditErrorWarning.Text = "Fail to delete " + item.Text;
                }
            }

            SetUpdateDisplay();
        }
        #endregion


        #region help

        private enum DdlChange
        {
            Country_Change,
            Province_Change,
            City_Change,
            Neighhood_Change,
            FSA_Change
        }
        private void Setup(DdlChange init)
        {
            string i;
            SetUpdateDisplay();
            switch (init)
            {
                case DdlChange.Country_Change:

                    DdlforCity.Items.Clear();
                    LBForCity.Items.Clear();
                    LBForNeighborhood.Items.Clear();
                    DdlForNeighborhood.Items.Clear();
                    LBForFSA.Items.Clear();
                    TextBoxForCountry.Text = "";
                    TextBoxForProvince.Text = "";
                    TextBoxForCity.Text = "";
                    TextBoxForFSA.Text = "";
                    TextBoxForCity.Enabled = false;
                    TextBoxForNeighborhood.Enabled = false;
                    TextBoxForFSA.Enabled = false;
                    //CityEditTextBox.Enabled = false;
                    CityCount.Text = "";
                    NeighborhoodCount.Text = "";
                    FSACount.Text = "";

                    break;
                case DdlChange.Province_Change:
                    i = DdlForProvince.SelectedValue;
                    if (i == "-1")
                    {
                        //ProvinceEditTextBox.Text = "";
                        //ProvinceEditTextBox.Enabled = false;
                        TextBoxForCity.Enabled = false;
                        CityCount.Text = "";
                    }
                    LBForNeighborhood.Items.Clear();
                    DdlForNeighborhood.Items.Clear();
                    LBForFSA.Items.Clear();
                    TextBoxForCountry.Text = "";
                    TextBoxForProvince.Text = "";
                    //CityEditTextBox.Text = "";
                    //CityId.Value = "";
                    TextBoxForCity.Text = "";
                    //NeighborhoodEditTextBox.Text = "";
                    //NeighborhoodId.Value = "";
                    TextBoxForNeighborhood.Text = "";
                    //FSAEditTextBox.Text = "";
                    //FSAId.Value = "";
                    TextBoxForFSA.Text = "";
                    TextBoxForNeighborhood.Enabled = false;
                    TextBoxForFSA.Enabled = false;
                    //CityEditTextBox.Enabled = false;
                    //NeighborhoodEditTextBox.Enabled = false;
                    //FSAEditTextBox.Enabled = false;
                    NeighborhoodCount.Text = "";
                    FSACount.Text = "";
                    break;
                case DdlChange.City_Change:
                    i = DdlforCity.SelectedValue;
                    if (i == "-1")
                    {
                        TextBoxForNeighborhood.Enabled = false;
                        NeighborhoodCount.Text = "";
                    }
                    LBForFSA.Items.Clear();
                    TextBoxForCountry.Text = "";
                    TextBoxForProvince.Text = "";
                    TextBoxForCity.Text = "";
                    TextBoxForNeighborhood.Text = "";
                    TextBoxForFSA.Text = "";
                   //TextBoxForFSA.Enabled = false;
                    FSACount.Text = "";
                    break;
                case DdlChange.Neighhood_Change:
                    i = DdlForNeighborhood.SelectedValue;
                    if (i == "-1")
                    {
                        //NeighborhoodEditTextBox.Text = "";
                        //NeighborhoodEditTextBox.Enabled = false;
                       // TextBoxForFSA.Enabled = false;
                        FSACount.Text = "";
                    }
                    TextBoxForCountry.Text = "";
                    TextBoxForProvince.Text = "";
                    TextBoxForCity.Text = "";
                    TextBoxForNeighborhood.Text = "";
                    FSAEditTextBox.Text = "";
                    FSAId.Value = "";
                    TextBoxForFSA.Text = "";
                    FSAEditTextBox.Enabled = false;
                    break;
                // case DdlChange.FSA_Change:
                // i = DdlForFSA.SelectedValue;

                //  TextBoxForCountry.Text = "";
                // TextBoxForProvince.Text = "";
                // TextBoxForCity.Text = "";
                // TextBoxForNeighborhood.Text = "";
                // TextBoxForFSA.Text = "";
                //break;

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

        private void SetUpdateDisplay()
        {
            PanelForDelEditCountry.Visible = true;
            PanelForUpdateCountry.Visible = false;
            PanelForDelEditProvince.Visible = true;
            PanelForUpdateProvince.Visible = false;
            PanelForDelEditCity.Visible = true;
            PanelForUpdateCity.Visible = false;
            PanelForDelEditNeighborhood.Visible = true;
            PanelForUpdateNeighborhood.Visible = false;
            PanelForDelEditFSA.Visible = true;
            PanelForUpdateFSA.Visible = false;
        }
        #endregion

     

     
    }
}