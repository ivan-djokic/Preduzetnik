using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Utils;

namespace MauiApp5.Models;

public abstract partial class TotalAmountModel : ObservableObject
{
	[ObservableProperty]
	private float m_totalAmount;

	[ObservableProperty]
	private Color m_totalAmountColor;

	public TotalAmountModel()
	{
		PropertyChanged += Subscribe;
		TotalAmountChanged += ChangeTotalAmountColor;
	}

	public event EventHandler TotalAmountChanged;

	private void ChangeTotalAmountColor(object sender, EventArgs e)
	{
		TotalAmountColor = Helper.GetColor(TotalAmount);
	}

	private void Subscribe(object sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(TotalAmount))
		{
			return;
		}

		TotalAmountChanged.Invoke(sender, e);
	}
}
