using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSTools.Models
{
    public class FSA:EntityBase
    {

        public FSA()
        {
            NeighborhoodIds = new List<string>();
        }
        public List<string> NeighborhoodIds { get; set; }
    }
}