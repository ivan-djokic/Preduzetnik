using MauiApp5.Utils;

namespace MauiApp5.UI.Controls;

public partial class Currency : ContentView
{
	private static readonly (Color, Color) s_currencyIsEUR = (Constants.COLOR_PURPLE, Constants.COLOR_WHITE);
	private static readonly (Color, Color) s_currencyIsRSD = (Constants.COLOR_WHITE, Constants.COLOR_PURPLE);

	public Currency()
	{
		InitializeComponent();

		Global.Options.CurrencyChanged += OnCurrencyChanged;
		OnCurrencyChanged(this, EventArgs.Empty);
	}

	private void ChangeCurrency(object sender, TappedEventArgs e)
	{
		Global.Options.IsCurrencyEUR = e.Parameter != null;
		Global.Options.Save();
		Global.Data.Save();
	}

	private void OnCurrencyChanged(object sender, EventArgs e)
	{
		var (colorEUR, colorRSD) = Global.Options.IsCurrencyEUR ? s_currencyIsEUR : s_currencyIsRSD;

		LabelEUR.TextColor = colorEUR;
		LabelRSD.TextColor = colorRSD;
	}
}
