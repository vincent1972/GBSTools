using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdgeD3;
using OrderitTables;

namespace GBSTools.Models
{
    public class CityRepository:BaseService
    {
        public bool Insert(City city)
        {

            try
            {
                d3file_city_table ds = new d3file_city_table();
                d3file_city_table.city_tableRow dr = ds.city_table.Newcity_tableRow();
                // ds = ds.Getcity_Table();
                dr.active = city.Active;
                dr.city = city.Name;
                dr.seoname = city.SeoName;
                dr.provinceid = city.ProvinceId;
                ds.city_table.Rows.Add(dr);
                ds.InsertCity_Table(ds);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string Id)
        {

            d3file_city_table ds = new d3file_city_table();
            try
            {

                ds.DeleteCity_Table(Id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(City city)
        {
            d3file_city_table ds = new d3file_city_table();
            try
            {
                ds = ds.GetCity_TableByCity_Table_Id(city.Id);
                var row = ds.city_table.First();
                ds.UpdateCity_TableToD3(city.Name, row.vendorcount, row.territory, city.SeoName, city.Active, city.ProvinceId, city.Id, ds);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public City GetCityById(string Id)
        {
            d3file_city_table ds = new d3file_city_table();
            ds = ds.GetCity_TableByCity_Table_Id(Id);
            City city = new City();
            if (ds.city_table.Count > 0)
            {
                
                city.Name = ds.city_table[0].city;
                city.SeoName = ds.city_table[0].seoname;
                city.Id = ds.city_table[0].city_table_id;
                city.Active = ds.city_table[0].active;
                city.ProvinceId = ds.city_table[0].provinceid;
                return city;
            }

            return null;



        }

        public List<City> GetAll()
        {
            List<City> cities = new List<City>();
            d3file_city_table ds = new d3file_city_table();
            var result = ds.GetCity_Table().city_table;
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    City city = new City();
                    city.Name = item.city;
                    city.SeoName = item.seoname;
                    city.Active = item.active;
                    city.Id = item.city_table_id;
                    city.ProvinceId = item.provinceid;
                    cities.Add(city);
                }

            }
            return cities.OrderBy(x => x.Name).ToList();
        }

        public List<City> GetCitiesByProvinceId(string Id)
        {
            List<City> cities = new List<City>();
            d3file_city_table ds = new d3file_city_table();
            // var result = ds.GetCity_Table().city_table.Where(x => x.provinceid == Id).ToList();
            var result = ds.GetCity_TableByProvinceId(Id).city_table.OrderBy(x => x.city).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    City city = new City();
                    city.Name = item.city;
                    city.SeoName = item.seoname;
                    city.Active = item.active;
                    city.Id = item.city_table_id;
                    city.ProvinceId = item.provinceid;
                    cities.Add(city);
                }

            }
            return cities.OrderBy(x => x.Name).ToList();

        }

        public bool IsDuplicate(string name, string provinceId, string cityId)
        {
            d3file_city_table ds = new d3file_city_table();
            var result = ds.GetCity_Table().city_table.Where(x => x.provinceid == provinceId).Where(y => y.city.ToLower().Trim() == name.ToLower().Trim());
            if (string.IsNullOrEmpty(cityId))
            {

                return result.Count() > 0 ? true : false;
            }
            else
            {
                result = result.Where(x => x.city_table_id != cityId);
                return result.Count() > 0 ? true : false;

            }

        }

        public EntityBase GetEntityById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}