using NUnit.Framework;
using Moq;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Concrete;

namespace GiftAidCalculator.Tests
{
	[TestFixture]
	public class EventPromoterRunningTests
	{
		private Mock<IRepository> _Repository;
		private Mock<IDonor> _Donor;
		private IEventPromoterRunning _EventPromoterRunning;

		[SetUp]
		public void Setup()
		{
			_Repository = new Mock<IRepository>();
			_Donor = new Mock<IDonor>();
			_EventPromoterRunning = new EventPromoterRunning(_Repository.Object, _Donor.Object);
		}

		[Test]
		public void CalculateRunnerGiftAidLoadsSpecifiedEventSupplementFromRepository()
		{
			string eventName = "NewEvent";
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName= eventName, Supplement = 8 });
			Assert.AreEqual(_Repository.Object.GetEventByName(eventName).Supplement, 8);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
		}

		[Test]
		public void CalculateRunnerGiftAidHandlesNullSupplement()
		{
			string eventName = "Running";
			decimal amount = 10;

			_Donor.Setup(a => a.CalculateGiftAid(amount)).Returns(2.5m);
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName = eventName});
			Assert.AreEqual(_EventPromoterRunning.CalculateRunnerGiftAid(amount), 2.525);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
			_Donor.Verify(a => a.CalculateGiftAid(It.IsAny<decimal>()), Times.Once, "IDonor - CalculateGiftAid not get called!");
		}

		[Test]
		public void CalculateRunnerGiftAidReturnsTheAidAsExpected()
		{
			string eventName = "Running";
			decimal amount = 10;

			_Donor.Setup(a => a.CalculateGiftAid(amount)).Returns(2.5m);
			_Repository.Setup(a => a.GetEventByName(eventName)).Returns(new TestConsole.Models.GiftAidEvent() { Id = "1", EventName = eventName, Supplement = 5 });
			Assert.AreEqual(_EventPromoterRunning.CalculateRunnerGiftAid(amount), 2.625);

			_Repository.Verify(a => a.GetEventByName(It.IsAny<string>()), Times.Once, "IRepository - GetEventByName not get called!");
			_Donor.Verify(a => a.CalculateGiftAid(It.IsAny<decimal>()), Times.Once, "IDonor - CalculateGiftAid not get called!");
		}

		[TearDown]
		public void TearDown()
		{
			_Repository = null;
			_Donor = null;
			_EventPromoterRunning = null;
		}
	}
}
