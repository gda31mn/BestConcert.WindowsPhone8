using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestConcert.WP8.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string State { get; set; }
        public object ValidationDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
