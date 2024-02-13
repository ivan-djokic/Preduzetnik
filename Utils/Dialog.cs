using MauiApp5.Resources;

namespace MauiApp5.Utils;

public static class Dialog
{
	private static Page Page
	{
		get => Shell.Current.CurrentPage;
	}

	public static Task<string> ShowEntry(string title)
	{
		return Page.DisplayPromptAsync(title, null, Strings.DialogOK, Strings.DialogCancel);
	}

	public static Task ShowInformation(string title, string description = null)
	{
		return Page.DisplayAlert(title, description, Strings.DialogOK);
	}

	public static Task<bool> ShowQuestion(string title, string description)
	{
		return Page.DisplayAlert(title, description, Strings.DialogYes, Strings.DialogNo);
	}
}
