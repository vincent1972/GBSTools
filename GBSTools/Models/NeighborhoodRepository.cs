using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdgeD3;
using OrderitTables;


namespace GBSTools.Models
{
    public class NeighborhoodRepository:BaseService
    {
        public bool Insert(Neighborhood neighborhood)
        {

            try
            {
                d3file_neighbourhood_table ds = new d3file_neighbourhood_table();
                d3file_neighbourhood_table.neighbourhood_tableRow dr = ds.neighbourhood_table.Newneighbourhood_tableRow();

                dr.active = neighborhood.Active;
                dr.name = neighborhood.Name;
                dr.seoname = neighborhood.SeoName;
                dr.cityid = neighborhood.CityId.ToString();
                ds.neighbourhood_table.Rows.Add(dr);
                ds.InsertNeighbourhood_Table(ds);

                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string Id)
        {
            d3file_neighbourhood_table ds = new d3file_neighbourhood_table();
            try
            {

                ds.DeleteNeighbourhood_Table(Id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update(Neighborhood neighourhood)
        {
            d3file_neighbourhood_table ds = new d3file_neighbourhood_table();
            try
            {

                ds = ds.GetNeighbourhood_TableByNeighbourhood_Table_Id(neighourhood.Id);
                ds.UpdateNeighbourhood_TableToD3(neighourhood.Name, neighourhood.SeoName, neighourhood.Active, neighourhood.CityId.ToString(), neighourhood.Id, ds);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public Neighborhood GetNeighborhoodById(string Id)
        {
            d3file_neighbourhood_table ds = new d3file_neighbourhood_table();
            ds = ds.GetNeighbourhood_TableByNeighbourhood_Table_Id(Id);
            Neighborhood neighbourhood = new Neighborhood();
            if (ds.neighbourhood_table.Count > 0)
            {
                neighbourhood.Name = ds.neighbourhood_table[0].name;
                neighbourhood.SeoName = ds.neighbourhood_table[0].seoname;
                neighbourhood.Id = ds.neighbourhood_table[0].neighbourhood_table_id;
                neighbourhood.Active = ds.neighbourhood_table[0].active;
                neighbourhood.CityId = ds.neighbourhood_table[0].cityid;
                return neighbourhood;
            }

            return null;
        }

        public bool IsDuplicate(string name, string CityId, string id)
        {
            d3file_neighbourhood_table ds = new d3file_neighbourhood_table();
            var result = ds.GetNeighbourhood_Table().neighbourhood_table.Where(x => x.cityid == CityId).Where(y => y.name.ToLower().Trim() == name.ToLower().Trim()).ToList();
            if (string.IsNullOrEmpty(id))
            {
                return result.Count() > 0 ? true : false;
            }
            else
            {
                result = result.Where(x => x.neighbourhood_table_id != id).ToList();
                return result.Count() > 0 ? true : false;
            }
        }
        public List<Neighborhood> GetAllByCityId(string Id)
        {
            List<Neighborhood> neighbourhoods = new List<Neighborhood>();
            d3file_neighbourhood_table ds = new d3file_neighbourhood_table();

            // var result = ds.GetNeighbourhood_Table().neighbourhood_table.Where(x => x.cityid == Id).ToList();
            var result = ds.GetNeighbourhood_TableByCityId(Id).neighbourhood_table.OrderBy(x => x.name).ToList(); ;
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    Neighborhood neighbourhood = new Neighborhood();
                    neighbourhood.Name = item.name;
                    neighbourhood.SeoName = item.seoname;
                    neighbourhood.Active = item.active;
                    neighbourhood.Id = item.neighbourhood_table_id;
                    neighbourhood.CityId = item.cityid;
                    neighbourhoods.Add(neighbourhood);

                }
            }
            return neighbourhoods;


        }
    }
}