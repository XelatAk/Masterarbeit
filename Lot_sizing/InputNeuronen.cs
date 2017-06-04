using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lot_sizing
{
	class InputNeuronen
	{
		//public int setupCost;
		public List<InputNeuronen> InputNeuron{ get; set; }
		public static void Main()
		{
			int setupCost;
			int setupTime;

			Console.WriteLine("Gib die Werte der folgenden Parameter ein:");

			Console.Write("Rüstkosten:");
			setupCost = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstkosten betragen {setupCost} Euro");
			
			Console.Write("Rüstzeit:");
			setupTime = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine($"Die Rüstzeit beträgt {setupTime} Zeiteinheiten");
			Console.ReadLine();



			List<int> inputNeuronen = new List<int>();
			inputNeuronen.Add(setupCost);
			inputNeuronen.Add(setupTime);

			Console.WriteLine(inputNeuronen.Count());

			Console.ReadLine();
			

		}
	}
}