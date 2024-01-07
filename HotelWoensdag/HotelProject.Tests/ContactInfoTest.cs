using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class ContactInfoTest
    {
        private readonly ContactInfo sut;

        public ContactInfoTest() => sut = new("Email@Test", "PhoneTest", new Address("CityTest", "ZipCodeTest", "NumberTest", "StreetTest"));

        // Email

        [Fact]
        public void Email_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Email));
        }

        [Fact]
        public void Email_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<ContactInfoException>(() => sut.Email = null);
            Assert.Throws<ContactInfoException>(() => sut.Email = "");
            Assert.Throws<ContactInfoException>(() => sut.Email = " ");
        }

        [Fact]
        public void Email_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Email);
        }

        [Fact]
        public void Email_ThrowsExceptionWhenNotContainsAt()
        {
            Assert.Throws<ContactInfoException>(() => sut.Email = "EmailTest");
        }

        // Phone

        [Fact]
        public void Phone_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Phone));
        }

        [Fact]

        public void Phone_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<ContactInfoException>(() => sut.Phone = null);
            Assert.Throws<ContactInfoException>(() => sut.Phone = "");
            Assert.Throws<ContactInfoException>(() => sut.Phone = " ");
        }

        [Fact]
        public void Phone_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Phone);
        }

        // Address

        [Fact]
        public void Address_IsCorrect()
        {
            Assert.NotNull(sut.Address);
        }

        [Fact]

        public void Address_ThrowsExceptionWhenNull()
        {
            Assert.Throws<ContactInfoException>(() => sut.Address = null);
        }

    }
}
