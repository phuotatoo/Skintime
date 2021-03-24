using System.Collections.Generic;

namespace Skintime.Models
{
    public class Cosmetics
    {
        public string id { get; set; }
        public string brand { get; set; }
        public string name { get; set; }
        public List<string> ingredient_list { get; set; }
    }
}
