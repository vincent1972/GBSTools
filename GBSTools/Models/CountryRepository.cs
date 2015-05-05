using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdgeD3;
using OrderitTables;
namespace GBSTools.Models
{
    public class CountryRepository:BaseService
    {
        public List<Country> GetAll()
        {
            d3file_country_table ds = new d3file_country_table();
            ds = ds.GetCountry_Table();
            List<Country> countries = new List<Country>();
            foreach (d3file_country_table.country_tableRow row in ds.country_table)
            {
                Country item = new Country();
                item.Name = row.name;
                item.SeoName = row.seoname;
                item.Active = row.active;
                item.Id = row.country_table_id;

                countries.Add(item);

            }
            return countries.OrderBy(x => x.Name).ToList();
        }
        public bool Insert(Country country)
        {

            try
            {
                d3file_country_table ds = new d3file_country_table();
                d3file_country_table.country_tableRow dr = ds.country_table.Newcountry_tableRow();
                dr.country_table_id = country.Id;
                dr.active = country.Active;
                dr.name = country.Name;
                dr.seoname = country.SeoName;
                ds.country_table.Rows.Add(dr);
                ds.WriteCountry_Table(ds, dr.country_table_id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string Id)
        {
            d3file_country_table ds = new d3file_country_table();

            try
            {

                ds.DeleteCountry_Table(Id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }


        public bool Update(Country country)
        {
            d3file_country_table ds = new d3file_country_table();
            try
            {
                ds = ds.GetCountry_TableByCountry_Table_Id(country.Id);
                if (ds.country_table.Count > 0)
                {
                    ds.UpdateCountry_TableToD3(country.Name, country.SeoName, country.Active, country.Id, ds);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public Country GetCountryById(string Id)
        {
            d3file_country_table ds = new d3file_country_table();
            ds = ds.GetCountry_TableByCountry_Table_Id(Id);
            Country country = new Country();
            if (ds.country_table.Count > 0)
            {
                country.Name = ds.country_table[0].name;
                country.SeoName = ds.country_table[0].seoname;
                country.Id = ds.country_table[0].country_table_id;
                country.Active = ds.country_table[0].active;
                return country;
            }

            return null;

        }

        public bool IsDuplicateId(string Id)
        {
            var result = GetCountryById(Id);
            return result == null ? true : false;

        }
        public bool IsDuplicateName(string name, string Id)
        {
            d3file_country_table ds = new d3file_country_table();
            var result = ds.GetCountry_Table().country_table.Where(x => x.name.ToLower() == name.ToLower()).ToList();
            if (string.IsNullOrEmpty(Id))
            {
                return result.Count > 0 ? true : false;
            }
            result = result.Where(x => x.country_table_id != Id).ToList();
            return result.Count > 0 ? true : false;

        }
    }
}