using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp5.Models;
using MauiApp5.Resources;
using MauiApp5.UI.Pages;
using MauiApp5.Utils;

namespace MauiApp5.UI.ViewModels;

public partial class HomeViewModel : ObservableObject
{
	[ObservableProperty]
	private ObservableCollection<Project> m_projects = Global.Data.Projects;

	public async Task Add()
	{
		var name = await Dialog.ShowEntry(Strings.AddProject);

		if (string.IsNullOrWhiteSpace(name))
		{
			return;
		}

		Projects.AddFirst(new Project
		{
			Name = Helper.GetNonDuplicateName(Projects, name)
		});

		Global.Data.Save();
	}

	[RelayCommand]
	private async Task Remove(Project project)
	{
		if (!await Dialog.ShowQuestion(string.Format(Strings.RemoveProject, project.Name), Strings.RemoveProjectDescription))
		{
			return;
		}

		Projects.Remove(project);
		Global.Data.Save();
	}

	[RelayCommand]
	private static async Task ShowDetails(Project project)
	{
		var str = Microsoft.Maui.Storage.FileSystem.AppDataDirectory;
		await Route.Navigate<ProjectDetailsPage>(project);
	}
}
