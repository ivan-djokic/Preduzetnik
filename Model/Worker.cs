using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiApp5.Models;

[DataContract]
public partial class Worker : TotalAmountModel, INamedModel
{
	[DataMember]
	[ObservableProperty]
	private string m_name;

	[DataMember]
	[ObservableProperty]
	private ObservableCollection<Report> m_reports = new();

	public Worker()
	{
		Reports.CollectionChanged += ReportsChanged;
	}

	private void CalculateTotalAmount(object sender, EventArgs e)
	{
		TotalAmount = Reports.Sum(item => item.TotalAmount);
	}

	private void ReportsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		CalculateTotalAmount(sender, e);

		if (e.Action != NotifyCollectionChangedAction.Add)
		{
			return;
		}

		foreach (Report report in e.NewItems)
		{
			report.TotalAmountChanged += CalculateTotalAmount;
		}
	}
}
