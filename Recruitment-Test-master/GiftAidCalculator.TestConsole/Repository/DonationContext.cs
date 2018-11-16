using System.Data.Entity;
using GiftAidCalculator.TestConsole.Models;
using System.Collections.Generic;

namespace GiftAidCalculator.TestConsole.Repository
{
	public class DonationContext : DbContext
	{
		public DbSet<Tax> Tax { get; set; }
		public List<GiftAidEvent> Event { get; set; }
	}
}
