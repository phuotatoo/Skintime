using System;
using System.Collections.Generic;
using System.Text;

namespace Skintime.Models
{
    public class Diary
    {

        public int ID { get; set; }

        public DateTime Date { get; set; }

        // them 1 dong may cai bool cua may cai button nua 
        public List<bool> condition { get; set; }
        public string Text { get; set; }
    }
}
