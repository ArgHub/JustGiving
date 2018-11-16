using GiftAidCalculator.TestConsole.Abstract;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class EventPromoterOther : IEventPromoterOther
	{
		private readonly IDonor _Donor;

		public EventPromoterOther(IDonor donor)
		{
			_Donor = donor;
		}

		public decimal CalculateOtherGiftAid(decimal amount)
		{
			return(_Donor.CalculateGiftAid(amount));
		}

	}
}
