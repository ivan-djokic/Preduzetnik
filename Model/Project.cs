using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp5.Models;

[DataContract]
public partial class Project : ObservableObject, INamedModel
{
	[DataMember]
	[ObservableProperty]
	private string m_name;

	[DataMember]
	[ObservableProperty]
	private ObservableCollection<Worker> m_workers = new();
}
