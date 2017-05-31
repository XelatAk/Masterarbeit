using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lot_sizing.Network
{
	public class Neuron
	{
		List<Synapse> InputSynapses { get; set; }
		List<Synapse> OutputSynapses { get; set; }
		double Value;
		double Bias;


		public Neuron()
		{
			InputSynapses = new List<Synapse>();
			OutputSynapses = new List<Synapse>();
			//Bias=Network.GetRandom();
		}

		//public Neuron(IEnumerable <Neuron> inputNeurons):this()
		//{

		//}

		double CalculateValue() //Propagierungsfunktion?
		{
			return Value = Sigmoid.Output(InputSynapses.Sum(a => a.Weight * a.InputNeuron.Value)); //+Bias
		}

	}

}