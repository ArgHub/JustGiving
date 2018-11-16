using System;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Models;

namespace GiftAidCalculator.TestConsole.Concrete
{
	public class SiteAdministrator : ISiteAdministrator
	{
		private readonly IRepository _Repository;

		public SiteAdministrator(IRepository repository)
		{
			_Repository = repository;
		}
		public Tax Tax { get; set; }

		public void SetTaxRate(decimal taxRate)
		{

			_Repository.UpdateTaxRecord(
				new Tax()
				{
					Id = (new Random(1)).ToString(),
					TaxRate = taxRate
				});
			_Repository.Save();
		}
	}
}
