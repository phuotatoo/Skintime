using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Skintime.Models
{
    public class Diary
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
