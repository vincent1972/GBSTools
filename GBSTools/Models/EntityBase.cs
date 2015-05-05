using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GBSTools.Models
{
    public class EntityBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }
        public bool Active { get; set; }
    }
}