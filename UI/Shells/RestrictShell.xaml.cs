using MauiApp5.UI.Pages;
using MauiApp5.Utils;

namespace MauiApp5.UI.Shells;

public partial class RestrictShell : Shell
{
	public RestrictShell()
	{
		InitializeComponent();
		Route.Register<RestrictPage>();
	}
}
