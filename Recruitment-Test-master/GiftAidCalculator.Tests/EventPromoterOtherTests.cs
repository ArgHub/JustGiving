using NUnit.Framework;
using Moq;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Concrete;

namespace GiftAidCalculator.Tests
{
	[TestFixture]
	public class EventPromoterOtherTests
	{
		private Mock<IDonor> _Donor;
		private IEventPromoterOther _EventPromoterOther;

		[SetUp]
		public void Setup()
		{
			_Donor = new Mock<IDonor>();
			_EventPromoterOther = new EventPromoterOther( _Donor.Object);
		}

		[Test]
		public void CalculateOtherGiftAidReturnsTheAidAsExpected()
		{
			decimal amount = 10;

			_Donor.Setup(a => a.CalculateGiftAid(amount)).Returns(2.5m);
			 Assert.AreEqual(_EventPromoterOther.CalculateOtherGiftAid(amount), 2.5);

			_Donor.Verify(a => a.CalculateGiftAid(It.IsAny<decimal>()), Times.Once, "IDonor - CalculateGiftAid not get called!");
		}

		[TearDown]
		public void TearDown()
		{
			_Donor = null;
			_EventPromoterOther = null;
		}
	}
}
