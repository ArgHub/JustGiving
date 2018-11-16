using NUnit.Framework;
using Moq;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Concrete;

namespace GiftAidCalculator.Tests
{
	[TestFixture]
	public class EventPromoterSwimmingTests
	{
		private Mock<IRepository> _Repository;
		private Mock<IDonor> _Donor;
		private IEventPromoterSwimming _EventPromoterSwimming;

		[SetUp]
		public void Setup()
		{
			_Repository = new Mock<IRepository>();
			_Donor = new Mock<IDonor>();
			_EventPromoterSwimming = new EventPromoterSwimming(_Repository.Object, _Donor.Object);
		}

		[Test]
		public void CalculateSwimmerGiftAidLoadsSpecifiedEventSupplementFromRepository()
		{
			string eventName = "Event";
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName = eventName, Supplement = 2 });
			Assert.AreEqual(_Repository.Object.GetEventByName(eventName).Supplement, 2);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
		}

		[Test]
		public void CalculateSwimmerGiftAidHandlesNullSupplement()
		{
			string eventName = "Swimming";
			decimal amount = 10;

			_Donor.Setup(a => a.CalculateGiftAid(amount)).Returns(2.5m);
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName = eventName });
			Assert.AreEqual(_EventPromoterSwimming.CalculateSwimmerGiftAid(amount), 2.5);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
			_Donor.Verify(a => a.CalculateGiftAid(It.IsAny<decimal>()), Times.Once, "IDonor - CalculateGiftAid not get called!");
		}

		[Test]
		public void CalculateSwimmerGiftAidReturnsTheAidAsExpected()
		{
			string eventName = "Swimming";
			decimal amount = 10;

			_Donor.Setup(a => a.CalculateGiftAid(amount)).Returns(2.5m);
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName = eventName, Supplement = 3 });
			Assert.AreEqual(_EventPromoterSwimming.CalculateSwimmerGiftAid(amount), 2.575);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
			_Donor.Verify(a => a.CalculateGiftAid(It.IsAny<decimal>()), Times.Once, "IDonor - CalculateGiftAid not get called!");
		}

		[TearDown]
		public void TearDown()
		{
			_Repository = null;
			_Donor = null;
			_EventPromoterSwimming = null;
		}
	}
}
