using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdgeD3;
using OrderitTables;

namespace GBSTools.Models
{
    public class ProvinceRepository:BaseService
    {
        public List<Province> GetAll()
        {
            List<Province> provinces = new List<Province>();
            d3file_province_table ds = new d3file_province_table();
            var result = ds.GetProvince_Table().province_table;
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    Province province = new Province();
                    province.Name = item.name;
                    province.SeoName = item.seoname;
                    province.Active = item.active;
                    province.Id = item.province_table_id;
                    province.CountryId = item.countryid;
                    provinces.Add(province);

                }
            }
            return provinces.OrderBy(x => x.Name).ToList();

        }
        public List<Province> GetProvincesByCountryId(string Id)
        {
            List<Province> provinces = new List<Province>();
            d3file_province_table ds = new d3file_province_table();
            var result = ds.GetProvince_Table().province_table.Where(x => x.countryid == Id).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    Province province = new Province();
                    province.Name = item.name;
                    province.SeoName = item.seoname;
                    province.Active = item.active;
                    province.Id = item.province_table_id;
                    province.CountryId = item.countryid;
                    provinces.Add(province);

                }
            }
            return provinces.OrderBy(x => x.Name).ToList();
        }
        public bool Insert(Province province)
        {

            try
            {
                d3file_province_table ds = new d3file_province_table();
                d3file_province_table.province_tableRow dr = ds.province_table.Newprovince_tableRow();
                dr.province_table_id = province.Id;
                dr.active = province.Active;
                dr.name = province.Name;
                dr.seoname = province.SeoName;
                dr.countryid = province.CountryId;
                ds.province_table.Rows.Add(dr);
                ds.WriteProvince_Table(ds, dr.province_table_id);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }


        public bool Delete(string Id)
        {
            d3file_province_table ds = new d3file_province_table();
            try
            {
                var province = GetProvinceById(Id);
                if (province != null)
                {
                    ds.DeleteProvince_Table(Id);
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Province province)
        {
            d3file_province_table ds = new d3file_province_table();
            try
            {
                ds = ds.GetProvince_TableByProvince_Table_Id(province.Id);
                if (ds.province_table.Count > 0)
                {
                    ds.UpdateProvince_TableToD3(province.Name, province.SeoName, province.Active, province.CountryId, province.Id, ds);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public Province GetProvinceById(string Id)
        {
            Province province = new Province();
            d3file_province_table ds = new d3file_province_table();
            d3file_rest_file ds1 = new d3file_rest_file();

            ds = ds.GetProvince_TableByProvince_Table_Id(Id);
            if (ds.province_table.Count > 0)
            {
                var pro = ds.province_table[0];
                province.Name = pro.name;
                province.SeoName = pro.seoname;
                province.Active = pro.active;
                province.Id = pro.province_table_id;
                province.CountryId = pro.countryid;

                return province;
            }
            return null;
        }
        public bool IsDuplicateId(string Id)
        {
            var result = GetProvinceById(Id);
            return result == null ? true : false;
        }
        public bool IsDuplicateName(string name, string countryId, string provinceId)
        {
            d3file_province_table ds = new d3file_province_table();
            var result = ds.GetProvince_Table().province_table.Where(x => x.countryid == countryId).Where(y => y.name.ToLower() == name.ToLower());
            if (string.IsNullOrEmpty(provinceId))
            {
                return result.Count() > 0 ? true : false;
            }
            else
            {
                result = result.Where(x => x.province_table_id != provinceId);
                return result.Count() > 0 ? true : false;
            }

        }
    }
}