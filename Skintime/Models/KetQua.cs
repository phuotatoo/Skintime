using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace Skintime.Models
{
    public class KetQua
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }
        public string key { get; set; }
    }
}
