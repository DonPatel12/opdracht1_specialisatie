using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class CustomerTest
    {
        private readonly Customer sut;

        public CustomerTest() => sut = new("Test Customer", 1, new("Email@Test", "PhoneTest", new Address("CityTest", "ZipCodeTest", "NumberTest", "StreetTest")));

        // Name

        [Fact]
        public void Name_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void Name_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<CustomerException>(() => sut.Name = null);
            Assert.Throws<CustomerException>(() => sut.Name = "");
            Assert.Throws<CustomerException>(() => sut.Name = " ");
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
            Assert.Throws<CustomerException>(() => sut.Id = 0);
            Assert.Throws<CustomerException>(() => sut.Id = -1);
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
            Assert.Throws<CustomerException>(() => sut.ContactInfo = null);
        }

        [Fact]
        public void ContactInfo_ShouldBeOfTypeContactInfo()
        {
            Assert.IsType<ContactInfo>(sut.ContactInfo);
        }

        // Members
        [Fact]
        public void Members_IsCorrect()
        {
            Assert.NotNull(sut.GetMembers());
        }

        [Fact]
        public void AddMember_IsCorrect()
        {
            Member member = new("Test Member", DateTime.Now);
            sut.AddMember(member);
            Assert.Contains(member, sut.GetMembers());
        }

        [Fact]
        public void AddMember_ThrowsExceptionWhenMemberAlreadyExists()
        {
            Member member = new("Test Member", DateTime.Now);
            sut.AddMember(member);
            Assert.Throws<CustomerException>(() => sut.AddMember(member));
        }

        [Fact]
        public void RemoveMember_IsCorrect()
        {
            Member member = new("Test Member", DateTime.Now);
            sut.AddMember(member);
            sut.RemoveMember(member);
            Assert.DoesNotContain(member, sut.GetMembers());
        }

        [Fact]
        public void RemoveMember_ThrowsExceptionWhenMemberDoesNotExist()
        {
            Member member = new("Test Member", DateTime.Now);
            Assert.Throws<CustomerException>(() => sut.RemoveMember(member));
        }

        [Fact]
        public void RemoveMember_ThrowsExceptionWhenMemberIsNull()
        {
            Assert.Throws<CustomerException>(() => sut.RemoveMember(null));
        }

        [Fact]
        public void RemoveMember_ThrowsExceptionWhenMemberIsNotInList()
        {
            Member member = new("Test Member", DateTime.Now);
            sut.AddMember(member);
            sut.RemoveMember(member);
            Assert.Throws<CustomerException>(() => sut.RemoveMember(member));
        }
    }
}
