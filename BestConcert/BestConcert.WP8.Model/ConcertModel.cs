using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestConcert.WP8.Model
{
    public class ConcertModel
    {
        public string Name { get; set; }
        public string Artist { get; set; }
        public string Genre { get; set; }
        public int Price { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
    }
}
