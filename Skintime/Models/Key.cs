﻿using SQLite;

namespace Skintime.Models
{
    public class Key
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
    }
}
