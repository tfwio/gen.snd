/* oio * 6/16/2014 * Time: 11:05 PM
 */
using System;
using FirstFloor.ModernUI.Presentation;

namespace genericwav.Models
{
	/// <summary>
	/// Description of MainViewModel.
	/// </summary>
	public class MainViewModel
        : NotifyPropertyChanged
	{
		public string MainContent { get { return _mainContent; } set { _mainContent = value; OnPropertyChanged("MainContent"); } }
		string _mainContent = "I'm the main string within the control!";
		
		public MainViewModel()
		{
		}
	}
}
