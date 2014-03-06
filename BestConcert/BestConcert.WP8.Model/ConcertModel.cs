using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestConcert.WP8.Model
{
    public class ConcertModel
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
        public object Date { get; set; }
        public string Location { get; set; }
    }

    public class ConcertList
    {
        public List<ConcertModel> Concert { get; set; }
    }
}
