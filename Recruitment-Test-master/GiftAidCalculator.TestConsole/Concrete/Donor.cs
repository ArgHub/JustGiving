using System;
using GiftAidCalculator.TestConsole.Abstract;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class Donor : IDonor
	{
		private readonly IRepository _Repository;

		public Donor(IRepository repository)
		{
			_Repository = repository;
		}

		public virtual decimal GiftAidTotal { get; set; }
		public virtual decimal DonationAmount { get; set; }

		public virtual decimal CalculateGiftAid(decimal amount)
		{
			decimal taxRate = _Repository.GetTax().TaxRate;			
			return (amount * (taxRate / (100 - taxRate)));
		}
	}
}
