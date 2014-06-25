using System;
using System.Linq;
using FntSize = FirstFloor.ModernUI.Presentation.FontSize;
namespace genericwav
{
	public interface IInt32MinMaxVal
	{
	    int Minimum { get;set; }
	    int Maximum { get;set; }
	    int Value   { get;set; }
	}
	public class MinMaxValue : IInt32MinMaxVal
	{
		public int Minimum {
			get;
			set;
		}

		public int Maximum {
			get;
			set;
		}

		public int Value {
			get {
				return _value;
			}
			set {
				value = _value;
			}
		}

		readonly int _value;

		public MinMaxValue(int min, int max, int val)
		{
			Minimum = min;
			Maximum = max;
			_value = val;
		}
	}
}




