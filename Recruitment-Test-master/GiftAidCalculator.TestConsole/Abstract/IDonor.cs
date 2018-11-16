namespace GiftAidCalculator.TestConsole.Abstract
{
	public interface IDonor
	{
		decimal CalculateGiftAid(decimal amount);
		decimal DonationAmount { get; set; }
	}
}
