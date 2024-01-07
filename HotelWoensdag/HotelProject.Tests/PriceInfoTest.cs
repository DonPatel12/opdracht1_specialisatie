using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class PriceInfoTest
    {
        private readonly PriceInfo sut;

        public PriceInfoTest() => sut = new(13.20m, 6.25m, 0.1m, 18);

        // AdultCost

        [Fact]
        public void AdultCost_IsCorrect()
        {
            Assert.True(sut.AdultCost > 0);
        }

        [Fact]
        public void AdultCost_ThrowsExceptionWhenLessThanZero()
        {
            Assert.Throws<PriceInfoException>(() => sut.AdultCost = -1.65m);
        }

        [Fact]
        public void AdultCost_ShouldBeOfTypeDecimal()
        {
            Assert.IsType<decimal>(sut.AdultCost);
        }

        // ChildCost

        [Fact]
        public void ChildCost_IsCorrect()
        {
            Assert.True(sut.ChildCost > 0);
        }

        [Fact]
        public void ChildCost_ThrowsExceptionWhenLessThanZero()
        {
            Assert.Throws<PriceInfoException>(() => sut.ChildCost = -1.24m);
        }

        [Fact]
        public void ChildCost_ShouldBeOfTypeDecimal()
        {
            Assert.IsType<decimal>(sut.ChildCost);
        }

        

        // Discount

        [Fact]

        public void Discount_IsCorrect()
        {
            Assert.True(sut.Discount >= 0 && sut.Discount <= 1);
        }
        [Fact]

        public void Discount_ThrowsExceptionWhenLessThanZeroOrGreaterThanOne()
        {
            Assert.Throws<PriceInfoException>(() => sut.Discount = -0.1m);
            Assert.Throws<PriceInfoException>(() => sut.Discount = 1.1m);
        }

        [Fact]
        public void Discount_ShouldBeOfTypeDecimal()
        {
            Assert.IsType<decimal>(sut.Discount);
        }

        // AdultAge

        [Fact]
        public void AdultAge_IsCorrect()
        {
            Assert.True(sut.AdultAge > 0);
        }

        [Fact]
        public void AdultAge_ThrowsExceptionWhenLessThanOrEqualToZero()
        {
            Assert.Throws<PriceInfoException>(() => sut.AdultAge = 0);
            Assert.Throws<PriceInfoException>(() => sut.AdultAge = -1);
        }

        [Fact]
        public void AdultAge_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.AdultAge);
        }

    }
}
