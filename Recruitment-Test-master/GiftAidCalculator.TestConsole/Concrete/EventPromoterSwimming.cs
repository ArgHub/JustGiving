using GiftAidCalculator.TestConsole.Abstract;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class EventPromoterSwimming : IEventPromoterSwimming
	{
		private readonly IRepository _Repository;
		private readonly IDonor _Donor;

		public EventPromoterSwimming(IRepository repository, IDonor donor)
		{
			_Repository = repository;
			_Donor = donor;
		}

		public decimal CalculateSwimmerGiftAid(decimal amount)
		{
			decimal d = _Donor.CalculateGiftAid(amount);
			int result = _Repository.GetEventByName("Swimming").Supplement;

			int supplement = result != null ? result : 1;

			return d += (d / 100 * supplement);
		}
	}
}
