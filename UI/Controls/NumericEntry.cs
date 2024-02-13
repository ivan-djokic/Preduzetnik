namespace MauiApp5.UI.Controls;

public class NumericEntry : Entry
{
	public NumericEntry()
	{
		Keyboard = Keyboard.Numeric;
	}

	protected override void OnTextChanged(string oldValue, string newValue)
	{
		if (newValue.StartsWith('-'))
		{
			Text = oldValue;
			return;
		}

		base.OnTextChanged(oldValue, newValue);
	}
}
