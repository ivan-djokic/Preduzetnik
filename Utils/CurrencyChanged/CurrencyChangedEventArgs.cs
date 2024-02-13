namespace MauiApp5.Utils;

public class CurrencyChangedEventArgs : EventArgs
{
	public CurrencyChangedEventArgs(float factor)
	{
		Factor = factor;
	}

	public float Factor { get; }
}
