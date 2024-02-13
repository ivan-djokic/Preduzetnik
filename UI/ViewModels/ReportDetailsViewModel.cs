using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Models;
using MauiApp5.Resources;
using MauiApp5.Utils;

namespace MauiApp5.UI.ViewModels;

[QueryProperty(nameof(Report), Constants.ROUTE_PARAM_PRIMARY)]
[QueryProperty(nameof(Worker), Constants.ROUTE_PARAM_SECONDARY)]
public partial class ReportDetailsViewModel : ObservableObject
{
	private Report m_oldReport;

	[ObservableProperty]
	private Report m_report;

	[ObservableProperty]
	private Worker m_worker;

	public ReportDetailsViewModel()
	{
		PropertyChanged += Initializing;
	}

	public async Task Save()
	{
		if (Worker.Reports.Where(report => report != m_oldReport)
			.Any(report => report.Date == Report.Date))
		{
			await Dialog.ShowInformation(Strings.DateReserved);
			return;
		}

		InsertOrReplace();
		Global.Data.Save();

		await Route.GoBack();
	}

	private void Initializing(object sender, PropertyChangedEventArgs e)
	{
		if (Report is null || Worker is null)
		{
			return;
		}

		Worker.PropertyChanged -= Initializing;

		if (!Worker.Reports.Contains(Report))
		{
			return;
		}

		m_oldReport = Report;
		Report = Report.Clone();
	}

	private void InsertOrReplace()
	{
		if (Worker.Reports.TryGetIndexOf(m_oldReport, out var index))
		{
			Worker.Reports[index] = Report;
			return;
		}

		index = Worker.Reports.GetInsertIndexOf(item => item.Date < Report.Date);
		Worker.Reports.Insert(index, Report);
	}
}
