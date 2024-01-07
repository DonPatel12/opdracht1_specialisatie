using HotelProject.BL.Exceptions;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.Tests
{
    public class ActivityTest
    {
        private readonly Activity sut;

        public ActivityTest() => sut = new(1, "Test Event", "Testen van Activity klasse", DateTime.Now.AddDays(1), 10, "Gent", 25, new(15.20m, 10.20m, 0.05m, 18));

        // ID

        [Fact]
        public void Id_ShouldBePositive()
        {
            Assert.True(sut.Id > 0);
        }

        [Fact]
        public void Id_ThrowsExceptionWhenNegativeOrZero()
        {
            Assert.Throws<ActivityException>(() => sut.Id = -1);
            Assert.Throws<ActivityException>(() => sut.Id = 0);
        }

        [Fact]
        public void Id_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.Id);
        }

        // Name

        [Fact]
        public void Name_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Name));
        }

        [Fact]
        public void Name_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<ActivityException>(() => sut.Name = null);
            Assert.Throws<ActivityException>(() => sut.Name = "");
            Assert.Throws<ActivityException>(() => sut.Name = " ");
        }

        [Fact]
        public void Name_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Name);
        }

        // Description

        [Fact]
        public void Description_IsCorrect()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Description));
        }

        [Fact]
        public void Description_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<ActivityException>(() => sut.Description = null);
            Assert.Throws<ActivityException>(() => sut.Description = "");
            Assert.Throws<ActivityException>(() => sut.Description = " ");
        }

        [Fact]
        public void Description_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Description);
        }

        // EventDateTime

        [Fact]
        public void EventDateTime_ShouldBeInTheFuture()
        {
            Assert.True(sut.EventDateTime > DateTime.Now);
        }

        [Fact]
        public void EventDateTime_ThrowsExceptionWhenInThePast()
        {
            Assert.Throws<ActivityException>(() => sut.EventDateTime = DateTime.Now.AddDays(-10));
        }

        [Fact]
        public void EventDateTime_ShouldBeOfTypeDateTime()
        {
            Assert.IsType<DateTime>(sut.EventDateTime);
        }

        // Duration

        [Fact]
        public void Duration_ShouldBePositive()
        {
            Assert.True(sut.Duration > 0);
        }

        [Fact]
        public void Duration_ThrowsExceptionWhenNegativeOrZero()
        {
            Assert.Throws<ActivityException>(() => sut.Duration = -1);
            Assert.Throws<ActivityException>(() => sut.Duration = 0);
        }

        [Fact]
        public void Duration_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.Duration);
        }

        // Location

        [Fact]
        public void Location_ShouldNotBeNullOrWhiteSpace()
        {
            Assert.False(string.IsNullOrWhiteSpace(sut.Location));
        }

        [Fact]
        public void Location_ThrowsExceptionWhenNullOrWhiteSpace()
        {
            Assert.Throws<ActivityException>(() => sut.Location = null);
            Assert.Throws<ActivityException>(() => sut.Location = "");
            Assert.Throws<ActivityException>(() => sut.Location = " ");
        }

        [Fact]
        public void Location_ShouldBeOfTypeString()
        {
            Assert.IsType<string>(sut.Location);
        }

        // NumberOfSpots

        [Fact]
        public void NumberOfParticipants_ShouldBePositive()
        {
            Assert.True(sut.NumberOfSpots > 0);
        }

        [Fact]
        public void NumberOfParticipants_ThrowsExceptionWhenNegativeOrZero()
        {
            Assert.Throws<ActivityException>(() => sut.NumberOfSpots = -1);
            Assert.Throws<ActivityException>(() => sut.NumberOfSpots = 0);
        }

        [Fact]
        public void NumberOfParticipants_ShouldBeOfTypeInt()
        {
            Assert.IsType<int>(sut.NumberOfSpots);
        }

        // PriceInfo

        [Fact]
        public void PriceInfo_ShouldNotBeNull()
        {
            Assert.NotNull(sut.PriceInfo);
        }

        [Fact]
        public void PriceInfo_ThrowsExceptionWhenNull()
        {
            Assert.Throws<ActivityException>(() => sut.PriceInfo = null);
        }

        // Cost

        [Fact]
        public void Cost_IsCorrect()
        {
            List<Member> members = new();
            Member m = new("Test", new DateTime(2000, 1, 1));
            members.Add(m);

            Assert.Equal(14.44m, sut.Cost(members));
            Assert.IsType<decimal>(sut.Cost(members));
            
        }

        

    }
}
