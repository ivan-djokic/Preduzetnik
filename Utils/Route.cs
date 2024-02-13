namespace MauiApp5.Utils;

public static class Route
{
	public static Task GoBack()
	{
		return Shell.Current.GoToAsync(Constants.ROUTE_BACK);
	}

	public static Task Navigate<T>(params object[] argument)
		where T : Page
	{
		var type = typeof(T);
		return Shell.Current.GoToAsync(type.Name, true, GetParameters(argument));
	}

	public static void Register<T>()
		where T : Page
	{
		var type = typeof(T);
		Routing.RegisterRoute(type.Name, type);
	}

	private static Dictionary<string, object> GetParameters(params object[] arguments)
	{
		var parameters = new Dictionary<string, object>();

		if (arguments.Any())
		{
			parameters.Add(Constants.ROUTE_PARAM_PRIMARY, arguments.First());
		}

		if (arguments.HasMoreItems())
		{
			parameters.Add(Constants.ROUTE_PARAM_SECONDARY, arguments.Second());
		}

		return parameters;
	}
}
