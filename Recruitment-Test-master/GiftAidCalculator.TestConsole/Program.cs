using GiftAidCalculator.TestConsole.Concrete;
using GiftAidCalculator.TestConsole.Abstract;
using GiftAidCalculator.TestConsole.Repository;
using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using System.Data.Entity;

namespace GiftAidCalculator.TestConsole
{
	class Program
	{
		//The following dependecies would be defined as static due to having a console app as desired UI! 
		private static IRepository _Repository;
		private static IDonor _Doner;
		private static IEventPromoterRunning _EventPromoterRunning;
		private static IEventPromoterSwimming _EventPromoterSwimming;
		private static IEventPromoterOther _EventPromoterOther;
		private static ISiteAdministrator _SiteAdministrator;
		private static IDecimalHelper _DecimalHelper;

		static void Main(string[] args)
		{
			// **** Registering
			var container = new WindsorContainer();
			container.Register(Component.For<DbContext>().ImplementedBy<DonationContext>());
			container.Register(Component.For<IRepository>().ImplementedBy<GiftAidRepository>());
			container.Register(Component.For<IDecimalHelper>().ImplementedBy<DecimalHelper>());
			container.Register(Component.For<IDonor>().ImplementedBy<Donor>());
			container.Register(Component.For<ISiteAdministrator>().ImplementedBy<SiteAdministrator>());
			container.Register(Component.For<IEventPromoterSwimming>().ImplementedBy<EventPromoterSwimming>());
			container.Register(Component.For<IEventPromoterRunning>().ImplementedBy<EventPromoterRunning>());
			container.Register(Component.For<IEventPromoterOther>().ImplementedBy<EventPromoterOther>());

			// **** Resolving

			_Repository = container.Resolve<IRepository>();
			_DecimalHelper = container.Resolve<IDecimalHelper>();
			_Doner = container.Resolve<IDonor>();
			_SiteAdministrator = container.Resolve<ISiteAdministrator>();
			_EventPromoterRunning = container.Resolve<IEventPromoterRunning>();
			_EventPromoterSwimming = container.Resolve<IEventPromoterSwimming>();
			_EventPromoterOther = container.Resolve<IEventPromoterOther>();

			DefineRole();
		}

		enum Roles
		{
			siteadministrator,
			donor,
			eventpromoter
		}

		enum EventTypes
		{
			running,
			swimming,
			other
		}

		public static void DefineRole()
		{
			Console.WriteLine("Please enter your role from the followings "
							+ Environment.NewLine +
							"SiteAdministrator / Donor / EventPromoter.");
			try
			{
				Roles role = (Roles)Enum.Parse(typeof(Roles), Console.ReadLine().ToLower(), true);

				switch (role)
				{
					case Roles.donor:
						DonorAid();
						break;
					case Roles.siteadministrator:
						SiteAdminTaxUpdate();
						break;
					case Roles.eventpromoter:
						EventPromoterAid();
						break;

					default:
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				DefineRole();
			}
		}

		public static void DonorAid()
		{

			Console.WriteLine("Please Enter donation amount:");
			string s = Console.ReadLine();

			if (_DecimalHelper.IsDecimal(s))
			{
				_Doner.DonationAmount = _DecimalHelper.RoundTwoDecimalPlaces(_DecimalHelper.ConvertToDecimal(s));
				Console.WriteLine("Gift aid is: £" + _DecimalHelper.RoundTwoDecimalPlaces(_Doner.CalculateGiftAid(_Doner.DonationAmount)).ToString());
				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Number is not in a correct format. Please try again.");
				DonorAid();
			}
		}

		public static void SiteAdminTaxUpdate()
		{
			Console.WriteLine("Enter new tax rate:");
			string s = Console.ReadLine();

			if (_DecimalHelper.IsDecimal(s))
			{
				decimal rate = _DecimalHelper.RoundTwoDecimalPlaces(_DecimalHelper.ConvertToDecimal(s));
				if (rate < 100)
				{
					_SiteAdministrator.SetTaxRate(rate);

					Console.WriteLine("New tax rate is: " + rate + "%");
				}
				else
				{
					Console.WriteLine("Please enter a valid tax rate");
					SiteAdminTaxUpdate();
				}

			}
			else
			{
				Console.WriteLine("Number is not in a correct format. Please try again.");
				SiteAdminTaxUpdate();
			}
		}

		public static void EventPromoterAid()
		{
			Console.WriteLine("Please enter an event from the followings "
				+ Environment.NewLine +
				"Running / Swimming / Other.");
			EventTypes events = (EventTypes)Enum.Parse(typeof(EventTypes), Console.ReadLine().ToLower(), true);
			Console.WriteLine("Please Enter donation amount:");
			string s = Console.ReadLine();

			decimal aid = 0;

			if (_DecimalHelper.IsDecimal(s))
			{
				decimal amount = _DecimalHelper.RoundTwoDecimalPlaces(_DecimalHelper.ConvertToDecimal(s));

				switch (events)
				{
					case EventTypes.running:
						aid = _DecimalHelper.RoundTwoDecimalPlaces(_EventPromoterRunning.CalculateRunnerGiftAid(amount));
						break;
					case EventTypes.swimming:
						aid = _DecimalHelper.RoundTwoDecimalPlaces(_EventPromoterSwimming.CalculateSwimmerGiftAid(amount));
						break;

						// Other events promoter could be treated as a donor as there would be no supplement applied to their donation, 
						// though it has been implemented the same way as the other event promoters, in case there would be future similar concepts
					case EventTypes.other:
						aid = _DecimalHelper.RoundTwoDecimalPlaces(_EventPromoterOther.CalculateOtherGiftAid(amount));
						break;

					default:
						break;
				}
				Console.WriteLine("Gift aid is: £" + aid.ToString());
				Console.ReadLine();
			}
			else
			{
				Console.WriteLine("Number is not in a correct format. Please try again.");
				EventPromoterAid();
			}
		}
	}
}
