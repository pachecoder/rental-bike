using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rental_bike
{
    public class Booking
    {
        //Instatiate book order to get the Orders
        BookOrder bookOrder = new BookOrder();

        const double DISCOUNT = 0.3;

        public bool BookNewOrder()
        {
            return false;
        }

        public double GetPrice(RentType rentType)
        {
            return rentType.Price;
        }

        public double GetPriceByRentType(List<RentType> rentType)
        {
            double price = 0;

            foreach (var itemToReturn in rentType)
            {
                price = itemToReturn.Price;
            }

            return price;
        }

        public bool IsFamilyDiscount(BookOrder bookOrder)
        {
            if (bookOrder.Bikes.Count >= 3 && bookOrder.Bikes.Count <= 5)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public BookOrder ApplyFamilyDiscount(BookOrder bookOrder, RentType rentType)
        {
            if (IsFamilyDiscount(bookOrder))
            {
                // we change the value of the object bookOrder using reflection to avoid create a new object
                double bookOrderWithDiscount = (bookOrder.Price * bookOrder.Bikes.Count()) - ((bookOrder.Price * bookOrder.Bikes.Count()) * DISCOUNT);
                bookOrder.GetType().GetProperty("Price").SetValue(bookOrder, bookOrderWithDiscount, null);
                return bookOrder;
            }
            else
            {
                return bookOrder;
            }
        }
        
    }
}
