using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class RegistrationTest
    {
        private readonly Registration sut;

        public RegistrationTest() => sut = new(1, new(1, "Test Event", "Testen van Activity klasse", DateTime.Now.AddDays(1), 10, "Gent", 25, new(15.20m, 10.20m, 0.0m, 18)), new("Test Customer", 1, new("Email@Test", "PhoneTest", new Address("CityTest", "ZipCodeTest", "NumberTest", "StreetTest"))), 25.4m);



        // ID

        [Fact]
        public void Id_ShouldBePositive()
        {
            Assert.True(sut.Id > 0);
        }

        [Fact]
        public void Id_ThrowsExceptionWhenNegativeOrZero()
        {
            Assert.Throws<RegistrationException>(() => sut.Id = -1);
            Assert.Throws<RegistrationException>(() => sut.Id = 0);
        }

        [Fact]
        public void Id_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.Id);
        }


        // Activity

        [Fact]
        public void Activity_ShouldNotBeNull()
        {
            Assert.NotNull(sut.Activity);
        }

        [Fact]
        public void Activity_ShouldBeOfTypeActivity()
        {
            Assert.IsType<Activity>(sut.Activity);
        }

        [Fact]
        public void Activity_ThrowsExceptionWhenNull()
        {
            Assert.Throws<RegistrationException>(() => sut.Activity = null);
        }

        // Customer

        [Fact]
        public void Customer_ShouldNotBeNull()
        {
            Assert.NotNull(sut.Customer);
        }

        [Fact]
        public void Customer_ShouldBeOfTypeCustomer()
        {
            Assert.IsType<Customer>(sut.Customer);
        }

        [Fact]
        public void Customer_ThrowsExceptionWhenNull()
        {
            Assert.Throws<RegistrationException>(() => sut.Customer = null);
        }
    }
}
