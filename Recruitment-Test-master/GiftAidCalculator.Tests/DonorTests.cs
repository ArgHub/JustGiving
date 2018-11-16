using NUnit.Framework;
using Moq;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Concrete;


namespace GiftAidCalculator.Tests
{
	[TestFixture]
	public class DonorTests
	{
		private Mock<IRepository> _Repository;
		private DecimalHelper test;
		private IDonor _Donor;

		[SetUp]
		public void Setup()
		{
			_Repository = new Mock<IRepository>();
			_Donor = new Donor(_Repository.Object);
		}

		[Test]
		public void CalculateGiftAidLoadsTaxRateFromRepository()
		{
			_Repository.Setup(a => a.GetTax()).Returns(new TestConsole.Models.Tax() { Id = "1", TaxRate = 17.5m });
			Assert.AreEqual(_Repository.Object.GetTax().TaxRate, 17.5m);

			_Repository.Verify(a => a.GetTax(), Times.Once, "IRepository - GetTax not get called!");
		}

		[TestCase(20, 5)]
		[TestCase(15.50, 3.875)]
		[Test]
		public void CalculateGiftAidReturnsTheAidAsExpected(decimal amount, decimal result)
		{
			_Repository.Setup(a => a.GetTax()).Returns(new TestConsole.Models.Tax() { Id = "1", TaxRate = 20.00m });
			var donationaid = _Donor.CalculateGiftAid(amount);
			Assert.AreEqual(donationaid, result);

			_Repository.Verify(a => a.GetTax(), Times.Once, "IRepository - GetTax not get called!");
		}

		[TearDown]
		public void TearDown()
		{
			_Repository = null;
			_Donor = null;
		}
	}
}
