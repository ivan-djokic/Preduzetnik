using MauiApp5.UI.ViewModels;

namespace MauiApp5.UI.Pages;

public partial class ReportDetailsPage : ContentPage
{
	public ReportDetailsPage(ReportDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void SaveButton_Clicked(object sender, EventArgs e)
	{
		await (BindingContext as ReportDetailsViewModel).Save();
	}
}
