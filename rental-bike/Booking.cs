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

        private const double DISCOUNT = 0.3;

        public enum Rent
        {
            Hour = 1,
            Day = 2,
            Week = 3
        }

        public string BookNewOrder(BookOrder bookOrder)
        {
            string recordSaved;

            try
            {
                if (IsFamilyDiscount(bookOrder))
                {
                    BookOrder bookOrderWithDiscount = ApplyFamilyDiscount(bookOrder, bookOrder.RentalType);
                    recordSaved = Save(bookOrderWithDiscount);
                }
                else
                {
                    recordSaved = Save(bookOrder);
                }

                return recordSaved;
            }
            catch (Exception exception)
            {
                throw new NotImplementedException();
            }
        }

        public string Save(BookOrder bookOrder)
        {
            return String.Format("Record saved, Customer {0} has rented the quantity of {1} bikes, for this price: {2}$. Apply Family Discount: {3}", bookOrder.Customer.Name, bookOrder.Bikes.Count(), bookOrder.Price, bookOrder.ApplyFamilyDiscount);
        }

        public double GetPrice(RentType rentType)
        {
            return rentType.Price;
        }

        public double GetPriceByRentType(List<RentType> rentType, Rent lapse)
        {
            int rentByLapse = (int) lapse;
            RentType getPriceByRentType = rentType.Where(r => r.RentTypeId == rentByLapse).First();
            double getPrice = GetPrice(getPriceByRentType);
            return getPrice;
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
                //we assign True to field ApplyFamilyDiscount 
                bookOrder.GetType().GetProperty("ApplyFamilyDiscount").SetValue(bookOrder, true, null);

                return bookOrder;
            }
            else
            {
                return bookOrder;
            }
        }
        
    }
}
