using System;

namespace Skintime.Models
{
    public class Product
    {
        public int ID { get; set; }
        public Cosmetics Info { get; set; }

        // Han su dung
        public DateTime EXP { get; set; }
    }
}
