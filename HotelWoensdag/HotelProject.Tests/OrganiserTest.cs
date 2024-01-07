using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class OrganiserTest
    {
        private readonly Organiser sut;

        public OrganiserTest() => sut = new(1,"Test Customer",  new("Email@Test", "PhoneTest", new Address("CityTest", "ZipCodeTest", "NumberTest", "StreetTest")));

        // Name

        [Fact]
        public void Name_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void Name_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<OrganiserException>(() => sut.Name = null);
            Assert.Throws<OrganiserException>(() => sut.Name = "");
            Assert.Throws<OrganiserException>(() => sut.Name = " ");
        }

        [Fact]
        public void Name_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Name);
        }

        // Id

        [Fact]
        public void Id_IsCorrect()
        {
            Assert.True(sut.Id > 0);
        }

        [Fact]
        public void Id_ThrowsExceptionWhenLessThanOrEqualToZero()
        {
            Assert.Throws<OrganiserException>(() => sut.Id = 0);
            Assert.Throws<OrganiserException>(() => sut.Id = -1);
        }

        [Fact]
        public void Id_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.Id);
        }

        // ContactInfo

        [Fact]
        public void ContactInfo_IsCorrect()
        {
            Assert.NotNull(sut.ContactInfo);
        }

        [Fact]
        public void ContactInfo_ThrowsExceptionWhenNull()
        {
            Assert.Throws<OrganiserException>(() => sut.ContactInfo = null);
        }

        [Fact]
        public void ContactInfo_ShouldBeOfTypeContactInfo()
        {
            Assert.IsType<ContactInfo>(sut.ContactInfo);
        }
    }
}
