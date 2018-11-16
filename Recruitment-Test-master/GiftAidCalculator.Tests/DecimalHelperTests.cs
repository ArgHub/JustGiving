using System.Linq;
using NUnit.Framework;
using GiftAidCalculator.TestConsole.Concrete;

namespace GiftAidCalculator.Tests
{
	[TestFixture]
	public class DecimalHelperTests
	{
		private DecimalHelper _DecimalHelper;

		[SetUp]
		public void Setup() => _DecimalHelper = new DecimalHelper();

		[Test]
		public void ShouldParseDecimal()
		{
		   Assert.IsTrue(_DecimalHelper.IsDecimal("4.678"));
		}

		[Test]
		public void ShouldFailParseDecimal()
		{
			Assert.IsFalse(_DecimalHelper.IsDecimal("2.95kj"));
		}

		[Test]
		public void CheckRoundingOfDecimal()
		{
			var d = _DecimalHelper.ConvertToDecimal("584.799");
			d = _DecimalHelper.RoundTwoDecimalPlaces(d);
			string[] words = d.ToString().Split('.');
			Assert.AreEqual(2, words.Count());
		}
	}
}
