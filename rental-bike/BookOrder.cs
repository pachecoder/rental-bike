using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rental_bike
{
    public class BookOrder
    {
        public int id { get; set; }
        public Customer Customer { get; set; }
        public List<Bike> Bikes { get; set; }
        public RentType RentalType { get; set; }
        public double Price { get; set; }
        public bool ApplyFamilyDiscount { get; set; }

    }
}
