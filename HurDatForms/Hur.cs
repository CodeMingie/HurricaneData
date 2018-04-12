using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurDatForms
{
    public class Hur
    {
        public int Id { get; set; }
        public string Basin { get; set; }
        public int CycloneNum { get; set; }
        public int Year { get; set; }
        public string Name { get; set; }
        public int NumEntries { get; set; }
        public List<HurDetail> Details { get; set; }
    }

    public class HurDetail
    {
        public int HurId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        //One character identifier for hurricane ex. L, C, G
        public string RecordId { get; set; }
        //Status of system ex TD, TS, HU
        public string Status { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longtitude { get; set; }
        public decimal MaxWind { get; set; }
        public string State { get; set; }
    }
}
