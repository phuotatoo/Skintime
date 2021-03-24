using SQLite;
using System;

namespace Skintime.Models
{
    public class Diary
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
    }
}
