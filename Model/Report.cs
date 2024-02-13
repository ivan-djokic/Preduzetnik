using System.ComponentModel;
using System.Runtime.Serialization;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp5.Utils;

namespace MauiApp5.Models;

[DataContract]
public partial class Report : TotalAmountModel
{
	[DataMember]
	[ObservableProperty]
	private DateOnly m_date = DateOnly.FromDateTime(DateTime.Now);

	[ObservableProperty]
	private float m_earned;

	[DataMember]
	[ObservableProperty]
	private float m_expenses;

	[DataMember]
	[ObservableProperty]
	private float m_hourlyRate;

	[DataMember]
	[ObservableProperty]
	public float m_workingHours = Constants.DEFAULT_WORKING_HOURS;

	public Report()
	{
		Global.Options.CurrencyChanged += OnCurrencyChanged;
		PropertyChanged += Subscribe;
	}

	public Report Clone()
	{
		return new()
		{
			Date = Date,
			Expenses = Expenses,
			HourlyRate = HourlyRate,
			WorkingHours= WorkingHours
		};
	}

	private void CalculateEarnings()
	{
		Earned = WorkingHours * HourlyRate;
	}

	private void CalculateTotalAmount()
	{
		TotalAmount = Earned - Expenses;
	}

	private void Subscribe(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
			case nameof(Earned):
			case nameof(Expenses):
				CalculateTotalAmount();
				break;

			case nameof(HourlyRate):
			case nameof(WorkingHours):
				CalculateEarnings();
				break;
		}
	}

	private void OnCurrencyChanged(object sender, CurrencyChangedEventArgs e)
	{
		Expenses = (Expenses * e.Factor).Round();
		HourlyRate = (HourlyRate * e.Factor).Round();
	}

	[OnDeserialized()]
	internal void OnDeserializedMethod(StreamingContext context)
	{
		CalculateEarnings();
		CalculateTotalAmount();
	}
}
