using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp5.Models;
using MauiApp5.Resources;
using MauiApp5.UI.Pages;
using MauiApp5.Utils;

namespace MauiApp5.UI.ViewModels;

[QueryProperty(nameof(Project), Constants.ROUTE_PARAM_PRIMARY)]
public partial class ProjectDetailsViewModel : ObservableObject
{
	[ObservableProperty]
	private Project m_project;

	public async Task Add()
	{
		var name = await Dialog.ShowEntry(Strings.AddWorker);

		if (string.IsNullOrWhiteSpace(name))
		{
			return;
		}

		var worker = new Worker
		{
			Name = Helper.GetNonDuplicateName(Project.Workers, name)
		};

		var index = Project.Workers.GetInsertIndexOf(item => item.Name.CompareTo(worker.Name) > 0);
		Project.Workers.Insert(index, worker);
		Global.Data.Save();
	}

	[RelayCommand]
	private async Task Remove(Worker worker)
	{
		if (!await Dialog.ShowQuestion(string.Format(Strings.RemoveWorker, worker.Name),
			string.Format(Strings.RemoveWorkerDescription, Project.Name)))
		{
			return;
		}

		Project.Workers.Remove(worker);
		Global.Data.Save();
	}

	[RelayCommand]
	private static async Task ShowDetails(Worker worker)
	{
		await Route.Navigate<WorkerDetailsPage>(worker);
	}
}
