using System.ComponentModel;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Utils;

namespace MauiApp5.Models;

[DataContract]
public partial class Options : ModelBase
{
	[DataMember]
	[ObservableProperty]
	public bool m_isCurrencyEUR = Constants.DEFAULT_CURRENCY_IS_EUR;

	[DataMember]
	[ObservableProperty]
	public bool m_restricted = Constants.DEFAULT_RESTRICTION;

	public Options()
	{
		PropertyChanged += Subscribe;
	}

	public event CurrencyChangedEventHandler CurrencyChanged;

	private void Subscribe(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(IsCurrencyEUR))
		{
			return;
		}

		CurrencyChanged?.Invoke(sender, new CurrencyChangedEventArgs(IsCurrencyEUR ? 1 / Constants.CURRENCY : Constants.CURRENCY));
	}
}
