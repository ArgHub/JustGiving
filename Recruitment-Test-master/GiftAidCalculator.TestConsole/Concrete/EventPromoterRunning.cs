using GiftAidCalculator.TestConsole.Abstract;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class EventPromoterRunning : IEventPromoterRunning
	{
		private readonly IRepository _Repository;
		private readonly IDonor _Donor;

		public EventPromoterRunning(IRepository repository, IDonor donor)
		{
			_Repository = repository;
			_Donor = donor;
		}

		public decimal CalculateRunnerGiftAid(decimal amount)
		{
			decimal d = _Donor.CalculateGiftAid(amount);
			int result = _Repository.GetEventByName("Running").Supplement;

			//this is just to make sure the result is handled! the supplement is set to a new value for unit test purpose!
			int supplement = result != 0 ? result : 1;

			return d += (d / 100 * supplement);
		}
	}
}
