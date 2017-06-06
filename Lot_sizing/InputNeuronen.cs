using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lot_sizing.Network
{
	class InputNeuronen
	{
		public static int setupCost;
		public static int setupTime;
		public static int period;
		public static int demand;
		public static List<int> InputLayer;


		public static void InputLotSize()
		{
			InputLayer = new List<int>();
			Console.WriteLine("Gib die Werte der folgenden Parameter ein:");
			Console.WriteLine();

			SetupCost();
			Console.WriteLine();

			SetupTime();
			Console.WriteLine();

			Period();
			Console.WriteLine();

			Demand();
			Console.WriteLine();

			Console.WriteLine($"Es sind {InputLayer.Count()} Inputs gespeichert");

			Console.ReadLine();

		}

		public static void SetupCost()
		{
			Console.Write("Rüstkosten in Euro:");
			setupCost = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstkosten betragen {setupCost} Euro");

			InputLayer.Add(setupCost);

			//TODO: Eine Ja/Nein Abfrage, ob die Werte stimmen
			//TODO: Exception einfügen, d.h. bei fehlerhafter Eingabe neu eingeben
		}

		public static void SetupTime()
		{
			Console.Write("Rüstzeit in Minuten:");
			setupTime = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstzeit beträgt {setupTime} Minuten");

			InputLayer.Add(setupTime);
		}

		public static void Period()
		{
			Console.Write("Anzahl der betrachteten Perioden:");
			period = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Es werden {period} Perioden betrachtet");

		}

		public static void Demand()
		{
			Console.WriteLine();
			Console.WriteLine($"Nachfrage der {period} Perioden:");
			Console.WriteLine();

			for (int num = 1; num < (period+1); num++)
			{
				Console.WriteLine();
				Console.WriteLine($"Geben Sie die Nachfrage für die Periode {num + 1} an:");
				Console.WriteLine($"Periode {num}={demand = Convert.ToInt32(Console.ReadLine())} Mengeneinheiten");
				InputLayer.Add(demand);
			}

		}

	}
}