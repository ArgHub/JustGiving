using System;
using System.Data.Entity;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Models;
using System.Globalization;
using System.Collections.Generic;

namespace GiftAidCalculator.TestConsole.Repository
{
	public class GiftAidRepository : IRepository
	{
		private DonationContext context;

		public GiftAidRepository()
		{
			this.context = new DonationContext();
		}

		public virtual void UpdateTaxRecord(Tax tax)
		{
			context.Entry(tax).State = EntityState.Modified;
		}

		public virtual GiftAidEvent GetEventByName(string name)
		{
			if (context.Event == null)
			{
				context.Event = new List<GiftAidEvent>(){
				new GiftAidEvent(){EventName = "Running", Supplement = 5, Id = "1"},
				new GiftAidEvent(){EventName = "Swimming", Supplement = 3, Id = "2"},
				new GiftAidEvent(){EventName = "Other", Id = "3"}
				};
				context.SaveChanges();
			}
			return context.Event.Find(a => a.EventName == name);
		}

		public virtual List<GiftAidEvent> GetAllEvents()
		{
			var events = context.Event.FindAll(a => a.Supplement > 0);
			if (events == null)
			{
				context.Event = new List<GiftAidEvent>(){
				new GiftAidEvent(){EventName = "Running", Supplement = 5},
				new GiftAidEvent(){EventName = "Swimming", Supplement = 3},
				new GiftAidEvent(){EventName = "Other"}
				};
				
				context.Event.AddRange(events);
				context.SaveChanges();
			}
			return events;
		}

		public bool Save()
		{
			try
			{
				context.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}

		}

		private bool disposed = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					context.Dispose();
				}
			}
			this.disposed = true;
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public virtual Tax GetTax()
		{
			var tax = context.Tax.Find("1");
			
			if (tax == null)
			{
				context.Tax.Add(new Tax()
				{
					TaxRate = Decimal.Parse(Properties.Resources.TaxRate,
					new CultureInfo("en-us")),
					Id = "1"
				});
				context.SaveChanges();
			}
			return tax;
		}
	}
}
