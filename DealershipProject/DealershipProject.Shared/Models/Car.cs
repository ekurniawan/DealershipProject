using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealershipProject.Shared.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public decimal BasePrice { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Stock { get; set; }
    }
}
