using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp5.Models;
using MauiApp5.Resources;
using MauiApp5.UI.Pages;
using MauiApp5.Utils;

namespace MauiApp5.UI.ViewModels;

[QueryProperty(nameof(Worker), Constants.ROUTE_PARAM_PRIMARY)]
public partial class WorkerDetailsViewModel : ObservableObject
{
	[ObservableProperty]
	private Worker m_worker;

	public async Task Add()
	{
		var report = new Report();

		if (Worker.Reports.Any())
		{
			var other = Worker.Reports.First();

			report.HourlyRate = other.HourlyRate;
			report.WorkingHours = other.WorkingHours;
		}

		await Route.Navigate<ReportDetailsPage>(report, Worker);
	}

	[RelayCommand]
	private async Task Remove(Report report)
	{
		if (!await Dialog.ShowQuestion(string.Format(Strings.RemoveReport, report.Date.ToString(Constants.DATE_FORMAT)),
			string.Format(Strings.RemoveReportDescription, Worker.Name)))
		{
			return;
		}

		Worker.Reports.Remove(report);
		Global.Data.Save();
	}

	[RelayCommand]
	private async Task ShowDetails(Report report)
	{
		await Route.Navigate<ReportDetailsPage>(report, Worker);
	}
}
