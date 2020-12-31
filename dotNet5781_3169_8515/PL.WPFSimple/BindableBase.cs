using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
	/// <summary>
	/// forked  from Prism.Mvvm.BindableBase and Microsoft
	/// </summary>
	public abstract class BindableBase : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

			storage = value;
			RaisePropertyChanged(propertyName);

			return true;
		}

		protected virtual bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(storage, value)) return false;

			storage = value;
			onChanged?.Invoke();
			RaisePropertyChanged(propertyName);

			return true;
		}

		// This method is called by the Set accessor of each property.  
		// The CallerMemberName attribute that is applied to the optional propertyName  
		// parameter causes the property name of the caller to be substituted as an argument.  
		protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			PropertyChanged?.Invoke(this, args);
		}

		////// This method is called by the Set accessor of each property.  
		////// The CallerMemberName attribute that is applied to the optional propertyName  
		////// parameter causes the property name of the caller to be substituted as an argument.  
		////protected virtual  void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
		////{
		////	PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		////}

	}
}
