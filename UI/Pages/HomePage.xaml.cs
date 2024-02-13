using MauiApp5.UI.ViewModels;

namespace MauiApp5.UI.Pages;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	private async void AddButton_Clicked(object sender, EventArgs e)
	{
		await (BindingContext as HomeViewModel).Add();
	}
}
