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
		public static List<int> inputNeuronen;

		public static void Main()
		{
			inputNeuronen = new List<int>();
			Console.WriteLine("Gib die Werte der folgenden Parameter ein:");

			SetupCost();
		
			SetupTime();

			Console.WriteLine(inputNeuronen.Count());

			Console.ReadLine();

		}

		public static void SetupCost()
		{
			//int setupCost;
			
			Console.Write("Rüstkosten:");
			setupCost = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstkosten betragen {setupCost} Euro");

			inputNeuronen.Add(setupCost);
			
			//TODO: Eine Ja/Nein Abfrage, ob die Werte stimmen...
		}

		public static void SetupTime()
		{
			//int setupTime;
			
			Console.Write("Rüstzeit:");
			setupTime = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstzeit beträgt {setupTime} Zeiteinheiten");

			inputNeuronen.Add(setupTime);
		}

	}
}