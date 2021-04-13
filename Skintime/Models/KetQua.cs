using SQLite;

namespace Skintime.Models
{
    public class KetQua
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string key { get; set; }
    }
}
