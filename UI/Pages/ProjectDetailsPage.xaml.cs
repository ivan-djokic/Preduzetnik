using MauiApp5.UI.ViewModels;

namespace MauiApp5.UI.Pages;

public partial class ProjectDetailsPage : ContentPage
{
	public ProjectDetailsPage(ProjectDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void AddButton_Clicked(object sender, EventArgs e)
	{
		await (BindingContext as ProjectDetailsViewModel).Add();
	}
}
