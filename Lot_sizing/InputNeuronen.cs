using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lot_sizing
{
	class InputNeuronen
	{
		public static int setupCost;
		public static int setupTime;
		public static int period;
		public static int demand;
		public static List<int> inputNeuronen;

		public static void Main()
		{
			inputNeuronen = new List<int>();
			Console.WriteLine("Gib die Werte der folgenden Parameter ein:");

			SetupCost();
		
			SetupTime();

			Period();

			Demand();

			Console.WriteLine($"Es sind {inputNeuronen.Count()} Inputs gespeichert");

			Console.ReadLine();

		}

		public static void SetupCost()
		{
			Console.Write("Rüstkosten:");
			setupCost = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstkosten betragen {setupCost} Euro");

			inputNeuronen.Add(setupCost);
			
			//TODO: Eine Ja/Nein Abfrage, ob die Werte stimmen...
		}

		public static void SetupTime()
		{
			Console.Write("Rüstzeit:");
			setupTime = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstzeit beträgt {setupTime} Zeiteinheiten");

			inputNeuronen.Add(setupTime);
		}

		public static void Period()
		{
			Console.Write("Anzahl der betrachteten Perioden:");
			period = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Es werden {period} Perioden betrachtet");

		}

		public static void Demand()
		{
			int num;
			Console.WriteLine($"Nachfrage der {period} Perioden:");

			num = 0;
			while(num<period)
			{
				demand = Convert.ToInt32(Console.ReadLine());
				inputNeuronen.Add(demand);

				num += 1;
			}
			

			
		}

	}
}