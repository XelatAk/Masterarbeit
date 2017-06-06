using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lot_sizing.Network
{
	public class DataSet
	{
		#region -- Properties --
		public double[] Values { get; set; }
		public double[] Targets { get; set; }
		#endregion

		#region -- Constructor --
		public DataSet(double[] values, double[] targets)
		{
			Values = values;
			Targets = targets;
		}
		#endregion
	}
}