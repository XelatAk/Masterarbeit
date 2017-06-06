﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Lot_sizing.Network;

namespace Lot_sizing
{
	internal class Program
	{
		#region -- Constants --
		private const int MaxEpochs = 5000;
		private const double MinimumError = 0.01;
		private const TrainingType TrainingType = Network.TrainingType.MinimumError;
		#endregion

		#region -- Variables --
		//private static int _numInputParameters;
		private static int _numHiddenLayerNeurons;
		private static int _numOutputParameters;
		private static Network.Network _network;
		private static List<DataSet> _dataSets;
		#endregion
		
		public static List<int> InputLayer;

		#region -- Main --
		private static void Main()
		{
			Greet();
			SetupNetwork();
			TrainNetwork();
			VerifyTraining();
		}
		#endregion

		#region -- Network Training --
		private static void TrainNetwork()
		{
			PrintNewLine();
			PrintUnderline(50);
			Console.WriteLine("Training...");

			Train();

			PrintNewLine();
			Console.WriteLine("Training Complete!");
			PrintNewLine();
		}

		private static void VerifyTraining()
		{
			Console.WriteLine("Let's test it!");
			PrintNewLine();

			while (true)
			{
				PrintUnderline(50);
				var values = GetInputData($"Type {InputNeuronen.period} inputs: ");
				var results = _network.Compute(values);
				PrintNewLine();

				foreach (var result in results)
				{
					Console.WriteLine($"Output: {result}");
				}

				PrintNewLine();

				var convertedResults = new double[results.Length];
				for (var i = 0; i < results.Length; i++) { convertedResults[i] = results[i] > 0.5 ? 1 : 0; }

				var message = $"Was the result supposed to be {string.Join(" ", convertedResults)}? (yes/no/exit)";
				if (!GetBool(message))
				{
					var offendingDataSet = _dataSets.FirstOrDefault(x => x.Values.SequenceEqual(values) && x.Targets.SequenceEqual(convertedResults));
					_dataSets.Remove(offendingDataSet);

					var expectedResults = GetExpectedResult("What were the expected results?");
					if (!_dataSets.Exists(x => x.Values.SequenceEqual(values) && x.Targets.SequenceEqual(expectedResults)))
						_dataSets.Add(new DataSet(values, expectedResults));

					PrintNewLine();
					Console.WriteLine("Retraining Network...");
					PrintNewLine();

					Train();
				}
				else
				{
					PrintNewLine();
					Console.WriteLine("Neat!");
					Console.WriteLine("Encouraging Network...");
					PrintNewLine();

					Train();
				}
			}
		}

		private static void Train()
		{
			_network.Train(_dataSets, TrainingType == TrainingType.Epoch ? MaxEpochs : MinimumError);
		}
		#endregion

		#region -- Network Setup --
		private static void Greet()
		{
			Console.WriteLine("We're going to create an artificial Neural Network!");
			Console.WriteLine("The network will use back propagation to train itself.");
			PrintUnderline(50);
			PrintNewLine();
		}

		private static void SetupNetwork()
		{
			if (GetBool("Do you want to read from the space delimited data.txt file? (yes/no/exit)"))
			{
				SetupFromFile();
			}
			else
			{
				SetInputParameters();
				SetNumNeuronsInHiddenLayer();
				SetNumOutputParameters();
				GetTrainingData();
			}

			Console.WriteLine("Creating Network...");
			_network = new Network.Network(InputLayer.Count, _numHiddenLayerNeurons, _numOutputParameters);
			PrintNewLine();
		}

		private static void SetInputParameters()
		{
			PrintNewLine();
			InputNeuronen.InputLotSize();
			PrintNewLine(2);
		}

		private static void SetNumNeuronsInHiddenLayer()
		{
			Console.WriteLine("How many neurons in the hidden layer? (2 or more)");
			_numHiddenLayerNeurons = GetInput("Neurons: ", 2);
			PrintNewLine(2);
		}

		private static void SetNumOutputParameters()
		{
			Console.WriteLine("How many output parameters will there be? (1 or more)");
			_numOutputParameters = InputNeuronen.period;
			PrintNewLine(2);
		}

		private static void GetTrainingData()
		{
			PrintUnderline(50);
			Console.WriteLine("Now, we need some input data.");
			PrintNewLine();

			_dataSets = new List<DataSet>();
			for (var i = 0; i < 4; i++)
			{
				var values = GetInputData($"Data Set {i + 1}");
				var expectedResult = GetExpectedResult($"Expected Result for Data Set {i + 1}:");
				_dataSets.Add(new DataSet(values, expectedResult));
			}
		}

		private static double[] GetInputData(string message)
		{
			Console.WriteLine(message);
			var line = GetLine();

			while (line == null || line.Split(' ').Length != InputNeuronen.period)
			{
				Console.WriteLine($"{InputNeuronen.period} inputs are required.");
				PrintNewLine();
				Console.WriteLine(message);
				line = GetLine();
			}

			var values = new double[InputNeuronen.period];
			var lineNums = line.Split(' ');
			for (var i = 0; i < lineNums.Length; i++)
			{
				double num;
				if (double.TryParse(lineNums[i], out num))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You entered an invalid number.  Try again");
					PrintNewLine(2);
					return GetInputData(message);
				}
			}

			return values;
		}

		private static double[] GetExpectedResult(string message)
		{
			Console.WriteLine(message);
			var line = GetLine();

			while (line == null || line.Split(' ').Length != _numOutputParameters)
			{
				Console.WriteLine($"{_numOutputParameters} outputs are required.");
				PrintNewLine();
				Console.WriteLine(message);
				line = GetLine();
			}

			var values = new double[_numOutputParameters];
			var lineNums = line.Split(' ');
			for (var i = 0; i < InputNeuronen.period; i++)
			{
				int num;
				if (int.TryParse(lineNums[i], out num) && (num == 0 || num == 1))
				{
					values[i] = num;
				}
				else
				{
					Console.WriteLine("You must enter 1s and 0s!");
					PrintNewLine(2);
					return GetExpectedResult(message);
				}
			}

			return values;
		}
		#endregion

		#region -- I/O Help --
		private static void SetupFromFile()
		{
			_dataSets = new List<DataSet>();
			var fileContent = File.ReadAllText("data.txt");
			var lines = fileContent.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

			if (lines.Length < 2)
			{
				WriteError("There aren't enough lines in the file.  The first line should have 3 integers representing the number of inputs, the number of hidden neurons and the number of outputs." +
						   "\r\nThere should also be at least one line of data.");
			}
			else
			{
				var setupParameters = lines[0].Split(' ');
				if (setupParameters.Length != 3)
					WriteError("There aren't enough setup parameters.");

				if (!int.TryParse(setupParameters[0], out InputNeuronen.period) || !int.TryParse(setupParameters[1], out _numHiddenLayerNeurons) || !int.TryParse(setupParameters[2], out _numOutputParameters))
					WriteError("The setup parameters are malformed.  There must be 3 integers.");

				//if (_numInputParameters < 2)
				//	WriteError("The number of input parameters must be greater than or equal to 2.");

				if (_numHiddenLayerNeurons < 2)
					WriteError("The number of hidden neurons must be greater than or equal to 2.");

				if (_numOutputParameters < 1)
					WriteError("The number of hidden neurons must be greater than or equal to 1.");
			}

			for (var lineIndex = 1; lineIndex < lines.Length; lineIndex++)
			{
				var items = lines[lineIndex].Split(' ');
				if (items.Length != InputNeuronen.period + _numOutputParameters)
					WriteError($"The data file is malformed.  There were {items.Length} elements on line {lineIndex + 1} instead of {InputNeuronen.period + _numOutputParameters}");

				var values = new double[InputNeuronen.period];
				for (var i = 0; i < InputNeuronen.period; i++)
				{
					double num;
					if (!double.TryParse(items[i], out num))
						WriteError($"The data file is malformed.  On line {lineIndex + 1}, input parameter {items[i]} is not a valid number.");
					else
						values[i] = num;
				}

				var expectedResults = new double[_numOutputParameters];
				for (var i = 0; i < _numOutputParameters; i++)
				{
					int num;
					if (!int.TryParse(items[InputNeuronen.period + i], out num))
						Console.WriteLine($"The data file is malformed.  On line {lineIndex}, output paramater {items[i]} is not a valid number.");
					else
						expectedResults[i] = num;
				}
				_dataSets.Add(new DataSet(values, expectedResults));
			}
		}
		#endregion

		#region -- Console Helpers --

		private static string GetLine()
		{
			var line = Console.ReadLine();
			return line?.Trim() ?? string.Empty;
		}

		private static int GetInput(string message, int min)
		{
			Console.Write(message);
			var num = GetNumber();

			while (num < min)
			{
				Console.Write(message);
				num = GetNumber();
			}

			return num;
		}

		private static int GetNumber()
		{
			int num;
			var line = GetLine();
			return line != null && int.TryParse(line, out num) ? num : 0;
		}

		private static bool GetBool(string message)
		{
			Console.WriteLine(message);
			Console.Write("Answer: ");
			var line = GetLine();

			bool answer;
			while (line == null || !TryGetBoolResponse(line.ToLower(), out answer))
			{
				if (line == "exit")
					Environment.Exit(0);

				Console.WriteLine(message);
				Console.Write("Answer: ");
				line = GetLine();
			}

			PrintNewLine();
			return answer;
		}

		private static bool TryGetBoolResponse(string line, out bool answer)
		{
			answer = false;
			if (string.IsNullOrEmpty(line)) return false;

			if (bool.TryParse(line, out answer)) return true;

			switch (line[0])
			{
				case 'y':
					answer = true;
					return true;
				case 'n':
					return true;
			}

			return false;
		}

		private static void PrintNewLine(int numNewLines = 1)
		{
			for (var i = 0; i < numNewLines; i++)
				Console.WriteLine();
		}

		private static void PrintUnderline(int numUnderlines)
		{
			for (var i = 0; i < numUnderlines; i++)
				Console.Write('-');
			PrintNewLine(2);
		}

		private static void WriteError(string error)
		{
			Console.WriteLine(error);
			Console.ReadLine();
			Environment.Exit(0);
		}
		#endregion
	}
}
