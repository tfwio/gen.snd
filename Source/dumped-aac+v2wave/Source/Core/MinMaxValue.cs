using System;

//425:12
namespace AvUtil.Core
{

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
            get { return _value; }
            set { value = _value; }
        } readonly int _value;
        
        public MinMaxValue(int min, int max, int val)
        {
            Minimum = min;
            Maximum = max;
            _value  = val;
        }
    }
}
