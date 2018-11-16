namespace GiftAidCalculator.TestConsole.Abstract
{
	public interface IDecimalHelper
	{
		bool IsDecimal(string value);
		decimal ConvertToDecimal(string value);
		decimal RoundTwoDecimalPlaces(decimal value);
	}
}
