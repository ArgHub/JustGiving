using GiftAidCalculator.TestConsole.Models;

namespace GiftAidCalculator.TestConsole.Abstract
{
	public interface ISiteAdministrator
	{
		Tax Tax { get; set; }
		void SetTaxRate(decimal taxRate);
	}
}
