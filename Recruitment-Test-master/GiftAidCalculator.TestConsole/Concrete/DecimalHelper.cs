using GiftAidCalculator.TestConsole.Abstract;
using System;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class DecimalHelper : IDecimalHelper
	{
		public decimal RoundTwoDecimalPlaces(decimal amount)
		{
			return Math.Round(amount, 2);
		}

		public bool IsDecimal(string amount)
		{
			decimal ParsedDecimal;
			return Decimal.TryParse(amount, out ParsedDecimal);
		}

		public decimal ConvertToDecimal(string value)
		{
			decimal ParsedDecimal;
			Decimal.TryParse(value, out ParsedDecimal);
			return ParsedDecimal;
		}
	}
}
