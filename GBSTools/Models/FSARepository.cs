using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdgeD3;
using OrderitTables;

namespace GBSTools.Models
{
    public class FSARepository:BaseService
    {
        public bool Insert(FSA fsa)
        {

            try
            {
                d3file_fsa_table ds = new d3file_fsa_table();
                d3file_fsa_table.fsa_tableRow dr = ds.fsa_table.Newfsa_tableRow();
                //if (fsa.NeighborhoodIds.Count > 0)
                //{

                //    foreach(var id in fsa.NeighborhoodIds)
                //    {

                //    d3file_fsa_table.mv_neighbourhoodidRow nerdr =  ds.mv_neighbourhoodid.Newmv_neighbourhoodidRow();
                //    nerdr.neighbourhoodid = id;
                //    nerdr.fsa_tableRow = dr;
                //    ds.mv_neighbourhoodid.Addmv_neighbourhoodidRow(nerdr);
                //    }  

                //} 


                dr.active = fsa.Active;
                dr.name = fsa.Name;
                dr.seoname = fsa.SeoName;
                //dr.neighbourhoodid = fsa.NeighborhoodId;
                ds.fsa_table.Rows.Add(dr);

                var Id = ds.InsertFsa_Table(ds).fsa_table.FirstOrDefault().fsa_table_id;

                if (fsa.NeighborhoodIds.Count > 0)
                {

                    foreach (var id in fsa.NeighborhoodIds)
                    {

                        d3file_fsa_table.mv_neighbourhoodidRow nerdr = ds.mv_neighbourhoodid.Newmv_neighbourhoodidRow();
                        nerdr.neighbourhoodid = id;
                        nerdr.fsa_tableRow = dr;
                        ds.mv_neighbourhoodid.Addmv_neighbourhoodidRow(nerdr);
                    }

                }
                ds.WriteFsa_Table(ds, Id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string Id)
        {
            d3file_fsa_table ds = new d3file_fsa_table();
            try
            {
                ds = ds.GetFsa_TableByFsa_Table_Id(Id);

                ds.DeleteFsa_Table(Id);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }
        }
        public bool RemoveFSAByNeighborhoodId(string FSAId, string neighborhoodId)
        {
            d3file_fsa_table ds = new d3file_fsa_table();
            try
            {
                ds = ds.GetFsa_TableByFsa_Table_Id(FSAId);

                d3file_fsa_table.mv_neighbourhoodidRow nerdr = ds.mv_neighbourhoodid.Where(x => x.neighbourhoodid == neighborhoodId).FirstOrDefault();
                ds.mv_neighbourhoodid.Removemv_neighbourhoodidRow(nerdr);
                ds.WriteFsa_Table(ds, FSAId);
                return true;
            }

            catch (Exception ex)
            {
                return false;
            }

        }
        public bool Update(FSA fsa)
        {
            d3file_fsa_table ds = new d3file_fsa_table();
            try
            {
                ds = ds.GetFsa_TableByFsa_Table_Id(fsa.Id);
                ds.UpdateFsa_TableToD3(fsa.Name, fsa.SeoName, fsa.Active, fsa.Id, ds);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public FSA GetFSAById(string Id)
        {
            d3file_fsa_table ds = new d3file_fsa_table();
            ds = ds.GetFsa_TableByFsa_Table_Id(Id);
            FSA fsa = new FSA();
            if (ds.fsa_table.Count > 0)
            {
                fsa.Name = ds.fsa_table[0].name;
                fsa.SeoName = ds.fsa_table[0].seoname;
                fsa.Id = ds.fsa_table[0].fsa_table_id;
                fsa.Active = ds.fsa_table[0].active;
                // fsa.NeighborhoodId = ds.fsa_table[0].neighbourhoodid;
                return fsa;
            }

            return null;
        }

        public List<FSA> GetAllByNeighborhoodId(string Id)
        {
            List<FSA> fsalist = new List<FSA>();
            d3file_fsa_table ds = new d3file_fsa_table();
            var result = ds.GetFsa_TableByNeighbourhoodId(Id).fsa_table;
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    FSA fsa = new FSA();
                    fsa.Name = item.name;
                    fsa.SeoName = item.seoname;
                    fsa.Active = item.active;
                    fsa.Id = item.fsa_table_id;
                    fsalist.Add(fsa);
                }

            }
            return fsalist;
        }

        public bool IsDuplicate(string name, string id)
        {
            d3file_fsa_table ds = new d3file_fsa_table();
            var result = ds.GetFsa_Table().fsa_table.Where(x => x.name.ToLower() == name.ToLower()).ToList();
            if (string.IsNullOrEmpty(id))
            {
                return result.Count() > 0 ? true : false;
            }
            else
            {
                result = result.Where(x => x.fsa_table_id != id).ToList();
                return result.Count() > 0 ? true : false;
            }
        }
    }
}