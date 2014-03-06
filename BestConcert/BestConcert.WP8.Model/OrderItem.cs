using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestConcert.WP8.Model
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public ConcertModel Concert { get; set; }
    }
}
