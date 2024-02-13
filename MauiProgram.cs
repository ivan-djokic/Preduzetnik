//using CommunityToolkit.Maui;
using MauiApp5.UI.Pages;
using MauiApp5.UI.ViewModels;

namespace MauiApp5;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.UseMauiApp<App>()
			.ConfigureFonts(AddFonts);

		builder.Services.AddSingleton<HomePage>();
		builder.Services.AddSingleton<HomeViewModel>();

		builder.Services.AddTransient<ProjectDetailsPage>();
		builder.Services.AddTransient<ProjectDetailsViewModel>();

		builder.Services.AddTransient<WorkerDetailsPage>();
		builder.Services.AddTransient<WorkerDetailsViewModel>();

		builder.Services.AddTransient<ReportDetailsPage>();
		builder.Services.AddTransient<ReportDetailsViewModel>();

		return builder.Build();
	}

	private static void AddFonts(IFontCollection fonts)
	{
		fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
	}
}