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
        
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Product { get; set; }
        public bool Normal { get; set; }
        public bool Acne { get; set; }
        public bool Eczema { get; set; }
        public string Text { get; set; }
        
        

    }
}
