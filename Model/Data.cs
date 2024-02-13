using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp5.Models;

[DataContract]
public partial class Data : ModelBase
{
	[DataMember]
	[ObservableProperty]
	private ObservableCollection<Project> m_projects = new();
}
