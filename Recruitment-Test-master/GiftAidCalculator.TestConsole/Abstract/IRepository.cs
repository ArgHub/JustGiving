using System;
using System.Collections.Generic;
using GiftAidCalculator.TestConsole.Models;

namespace GiftAidCalculator.TestConsole.Abstract
{
	public interface IRepository : IDisposable
	{
		Tax GetTax();
		void UpdateTaxRecord(Tax tax);
		GiftAidEvent GetEventByName(string name);
		List<GiftAidEvent> GetAllEvents();
		bool Save();
	}
}
