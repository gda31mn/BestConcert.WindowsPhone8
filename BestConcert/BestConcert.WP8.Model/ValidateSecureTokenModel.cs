using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestConcert.WP8.Model
{
    public class ValidateSecureTokenModel
    {
        public bool Validated { get; set; }
        public string Status { get; set; }
        public bool HasError { get; set; }
    }
}
