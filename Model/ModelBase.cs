using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Utils;

namespace MauiApp5.Models;

[DataContract]
public abstract class ModelBase : ObservableObject
{
	[DataMember]
	public ulong Version { get; set; }

	public void Save()
	{
		Version++;
		Global.Save(this);
	}
}
