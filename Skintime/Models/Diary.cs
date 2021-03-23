using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Skintime.Models
{
    public class Diary
    {
        [PrimaryKey,AutoIncrement]
        public int ID { get; set; }

        public DateTime Date { get; set; }

        // them 1 dong may cai bool cua may cai button nua 
        public bool nor { get; set; }
        public bool acn { get; set; }
        public bool ecz { get; set; }
        public string Text { get; set; }
    }
}
