using MauiApp5.Utils;

namespace MauiApp5.UI.Controls;

public class AutoClosingSwipeView : SwipeView
{
	private static SwipeView s_lastOpennedItem;
	private static readonly Timer s_timer = new(OnTimerElapsed);

	public AutoClosingSwipeView()
	{
		SwipeStarted += OnSwipeStarted;
	}

	private void OnSwipeStarted(object sender, SwipeStartedEventArgs e)
	{
		s_lastOpennedItem?.Close();
		s_lastOpennedItem = this;

		s_timer.Change(Constants.SWIPE_OPPENED_LIFE_PERIOD, int.MaxValue);
	}

	private static void OnTimerElapsed(object state)
	{
		s_lastOpennedItem?.Close();
		s_lastOpennedItem = null;
	}
}
