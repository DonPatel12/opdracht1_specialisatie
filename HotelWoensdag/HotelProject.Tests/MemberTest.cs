using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class MemberTest
    {
        private readonly Member sut;

        public MemberTest() => sut = new("Test Member", new DateTime(2000, 1, 1));

        // Name

        [Fact]
        public void Name_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void Name_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<MemberException>(() => sut.Name = null);
            Assert.Throws<MemberException>(() => sut.Name = "");
            Assert.Throws<MemberException>(() => sut.Name = " ");
        }

        [Fact]
        public void Name_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Name);
        }

        // BirthDay

        [Fact]
        public void BirthDay_IsCorrect()
        {
            Assert.True(sut.BirthDay < DateTime.Now);
        }

        [Fact]
        public void BirthDay_ThrowsExceptionWhenGreaterThanNow()
        {
            Assert.Throws<MemberException>(() => sut.BirthDay = DateTime.Now.AddDays(1));
        }

        [Fact]
        public void BirthDay_ShouldBeOfTypeDateTime()
        {
            Assert.IsType<DateTime>(sut.BirthDay);
        }
    }
}
