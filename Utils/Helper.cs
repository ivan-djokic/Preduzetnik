using MauiApp5.Models;

namespace MauiApp5.Utils;

public static partial class Helper
{
	public static Color GetColor(float amount)
	{
		return amount < 0 ? Constants.COLOR_RED : Constants.COLOR_GREEN;
	}

	public static string GetNonDuplicateName<T>(IEnumerable<T> items, string name)
		where T : INamedModel
	{
		name = name.Trim();

		if (!items.Any(item => item.Name == name))
		{
			return name;
		}

		var ordinal = GetDuplicateOrdinal(ref name);

		while (items.Any(item => item.Name == name))
		{
			ordinal++;
			name = $"{name} ({ordinal})";
		}

		return name;
	}

	private static int GetDuplicateOrdinal(ref string name)
	{
		if (!name.EndsWith(')'))
		{
			return Constants.FIRST_DUPLICATE;
		}

		var startIndex = name.LastIndexOf('(');

		if (startIndex < 1 || !int.TryParse(name.SubstringFromTo(startIndex + 1, name.Length - 1), out var ordinal))
		{
			return Constants.FIRST_DUPLICATE;
		}

		name = name.Remove(startIndex).TrimEnd();
		return ordinal;
	}
}
