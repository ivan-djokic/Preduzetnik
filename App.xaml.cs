using MauiApp5.UI.Shells;
using MauiApp5.Utils;

namespace MauiApp5;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		MainPage = Global.Options.Restricted ? new RestrictShell() : new MainShell();
	}
}
