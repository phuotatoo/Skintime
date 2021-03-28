using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace Skintime.Models
{
    public class KetQua
    {

        [JsonProperty("DS_KetQua")]
        public Cosmetics cosmetics { get; set; }
    }
}
