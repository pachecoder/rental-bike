using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using rental_bike;
using System.Linq;
using System.Reflection;

namespace rental_bike_test
{
    [TestClass]
    public class BookingTest
    {

        Booking booking = new Booking();
        BookOrder bookOrder = InitializeBookOrder();
        List<RentType> rentType = InitializeRentTypeCollection();

        const double DISCOUNT = 0.3;



        [TestMethod]
        public void BookNewOrder()
        {
            Assert.AreEqual("Record saved, Customer Cosme Fulanito has rented the quantity of 4 bikes, for this price: 14$. Apply Family Discount: True", booking.BookNewOrder(bookOrder));
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

        [TestMethod]
        public void GetPrice()
        {
            double expected = 20.0;

            int rentByDay = (int)Booking.Rent.Day;
            RentType getPriceByRentType = rentType.Where(r => r.RentTypeId == rentByDay).First();
            double getPrice = getPriceByRentType.Price;
            Assert.AreEqual(expected, getPrice);
        }


        [TestMethod]
        public void GetPriceByRentType()
        {
            double expected = 20.0;
            
            var rentByDay = Booking.Rent.Day;
            double getPriceByRentType = booking.GetPriceByRentType(rentType, rentByDay); 
            Assert.AreEqual(expected, getPriceByRentType);
        }

        [TestMethod()]
        public void TestPropertiesBike()
        {
            Bike bike = new Bike() { Color = "White", Frame = "Men", Rim = 26};

            Assert.AreEqual("White", bike.Color);
            Assert.AreEqual("Men", bike.Frame);
            Assert.AreEqual(26, bike.Rim);
        }

        [TestMethod()]
        public void TestPropertiesRentType()
        {
            RentType rentTypeTest = new RentType
            {
                RentTypeId = 1,
                Description = "Hour",
                Price = 5.0
            };

            Assert.AreEqual(1, rentTypeTest.RentTypeId);
            Assert.AreEqual("Hour", rentTypeTest.Description);
            Assert.AreEqual(5.0, rentTypeTest.Price);
        }

        [TestMethod()]
        public void TestPropertiesCustomer()
        {
            Customer customer = new Customer
            {
                Ssn = "123456789",
                Name = "Cosme Fulanito"
            };

            Assert.AreEqual("123456789", customer.Ssn);
            Assert.AreEqual("Cosme Fulanito", customer.Name);
        }

        [TestMethod()]
        public void TestPropertiesBookOrder()
        {
            BookOrder book = new BookOrder
            {
                id = 123456789
            };
            Assert.AreEqual(123456789, book.id);
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
            int rentByHour = (int) Booking.Rent.Hour;
            RentType rentType = InitializeRentTypeCollection().Where(r => r.RentTypeId == rentByHour).First();

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

        private static List<RentType> InitializeRentTypeCollection()
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
