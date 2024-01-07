using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class AddressTest
    {
        private readonly Address sut;

        public AddressTest() => sut = new("Test Street", "9000", "1", "Gent");

        // Street

        [Fact]
        public void Street_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Street));
        }

        [Fact]
        public void Street_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<AddressException>(() => sut.Street = null);
            Assert.Throws<AddressException>(() => sut.Street = "");
            Assert.Throws<AddressException>(() => sut.Street = " ");
        }

        [Fact]
        public void Street_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Street);
        }

        // ZipCode

        [Fact]
        public void ZipCode_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.ZipCode));
        }

        [Fact]
        public void ZipCode_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<AddressException>(() => sut.ZipCode = null);
            Assert.Throws<AddressException>(() => sut.ZipCode = "");
            Assert.Throws<AddressException>(() => sut.ZipCode = " ");
        }

        [Fact]
        public void ZipCode_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.ZipCode);
        }

        // HouseNumber

        [Fact]
        public void HouseNumber_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.HouseNumber));
        }

        [Fact]
        public void HouseNumber_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<AddressException>(() => sut.HouseNumber = null);
            Assert.Throws<AddressException>(() => sut.HouseNumber = "");
            Assert.Throws<AddressException>(() => sut.HouseNumber = " ");
        }

        [Fact]
        public void HouseNumber_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.HouseNumber);
        }

        // Municipality

        [Fact]
        public void Municipality_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Municipality));
        }

        [Fact]
        public void Municipality_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<AddressException>(() => sut.Municipality = null);
            Assert.Throws<AddressException>(() => sut.Municipality = "");
            Assert.Throws<AddressException>(() => sut.Municipality = " ");
        }

        [Fact]
        public void Municipality_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Municipality);
        }

        
    }
}
