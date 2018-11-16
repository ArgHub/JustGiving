using System.ComponentModel.DataAnnotations;

namespace GiftAidCalculator.TestConsole.Models
{
	public class Tax
	{
		public decimal TaxRate { get; set; }
		[Key]
		public string Id { get; set; }
	}
}
