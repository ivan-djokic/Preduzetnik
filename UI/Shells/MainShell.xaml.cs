using MauiApp5.UI.Pages;
using MauiApp5.Utils;

namespace MauiApp5.UI.Shells;

public partial class MainShell : Shell
{
	public MainShell()
	{
		InitializeComponent();

		Route.Register<HomePage>();
		Route.Register<ProjectDetailsPage>();
		Route.Register<WorkerDetailsPage>();
		Route.Register<ReportDetailsPage>();
	}
}
