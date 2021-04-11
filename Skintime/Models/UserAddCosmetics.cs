using System;
using System.Collections.Generic;
using System.Text;

namespace Skintime.Models
{
    public class UserAddCosmetics
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public List<string> ingredient_list;
    }
}
