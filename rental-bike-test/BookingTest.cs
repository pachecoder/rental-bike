using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using rental_bike;
using System.Linq;

namespace rental_bike_test
{
    [TestClass]
    public class BookingTest
    {

        Booking booking = new Booking();
        BookOrder bookOrder = InitializeBookOrder();
        const double DISCOUNT = 0.3;

        [TestMethod]
        public void BookNewOrder()
        {
            Assert.IsTrue(true);
        }
        
        [TestMethod]
        public void IsFamilyDiscount()
        {
            BookOrder bookOrder = InitializeBookOrder();

            bool actual;
            bool expected = true;
            
            if (booking.IsFamilyDiscount(bookOrder))
            {
                Assert.IsTrue(booking.IsFamilyDiscount(bookOrder));
            }
            else
            {
                Assert.IsFalse(booking.IsFamilyDiscount(bookOrder));
            };
            
        }
        
        [TestMethod]
        public void ApplyFamilyDiscount()
        {
            double bookOrderWithDiscount = 0;
            double expected = 14;

            if (booking.IsFamilyDiscount(bookOrder))
            {
                bookOrderWithDiscount = booking.ApplyFamilyDiscount(bookOrder, bookOrder.RentalType).Price;
                Assert.AreEqual(bookOrderWithDiscount, expected);
            }
            else
            {
                Assert.AreSame(bookOrder, booking.ApplyFamilyDiscount(bookOrder, bookOrder.RentalType));
            }
            
        }

        private static BookOrder InitializeBookOrder()
        {
            //validate quantities of bikes to know if is Family Discount
            List<Bike> bikesOrder = new List<Bike>();
            for (int i = 0; i < 4; i++)
            {
                bikesOrder.Add(new Bike { Color = "White", Frame = "Men", Rim = 26 });
            }

            //initialize RentalType
            RentType rentType = RentTypeCollection().First();

            //initialize Customer
            Customer customer = new Customer() { Ssn = "123456789", Name = "Cosme Fulanito" };

            //initialize BookOrder
            BookOrder bookOrder = new BookOrder()
            {
                Bikes = bikesOrder,
                Customer = customer,
                RentalType = rentType,
                Price = rentType.Price
            };

            return bookOrder;
        }

        private static List<RentType> RentTypeCollection()
        {
            return new List<RentType>
            {
                new RentType() {
                    RentTypeId = 1,
                    Description = "Hour",
                    Price = 5.0
                },

                new RentType() {
                    RentTypeId = 2,
                    Description = "Day",
                    Price = 20.0
                    },

                new RentType() {
                    RentTypeId = 3,
                    Description = "Week",
                    Price = 60.0
                }
            };
        }
    }
}
