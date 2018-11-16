using System;
using Moq;
using NUnit.Framework;
using GiftAidCalculator.TestConsole.Abstract;

namespace GiftAid.Tests
{
	[TestFixture]
	public class UnitTest1
	{
		private Mock<IDecimalHelper> _DecimalHelper;

		[SetUp]
		public void Setup() => _DecimalHelper = new Mock<IDecimalHelper>();

		[Test]
		public void ShouldParseDecimal()
		{
			//	_DecimalHelper.Setup(a => a.IsDecimal(It.IsAny<string>())).Returns(true);
			Assert.IsTrue(_DecimalHelper.Object.IsDecimal("2.95"));
		}
	}
}
