using Octokit;
using System.Collections.ObjectModel;

namespace MauiApp5.Utils;

public static class Extensions
{
	public static void AddFirst<T>(this Collection<T> items, T item)
	{
		items.Insert(0, item);
	}

	public static bool HasMoreItems<T>(this IList<T> items)
	{
		return items.Count > 1;
	}

	public static int GetInsertIndexOf<T>(this Collection<T> items, Predicate<T> predicate)
	{
		int index = 0;

		while (index < items.Count && !predicate(items[index]))
		{
			index++;
		}

		return index;
	}

	public static float Round(this float value)
	{
		var numberParts = value.ToString().Split('.');

		if (numberParts.Length == 1)
		{
			return value;
		}

		if (TryGetSequence(numberParts[1], '0', input => input, out var normalizer))
		{
			return value - float.Parse(normalizer);
		}

		if (TryGetSequence(numberParts[1], '9', ComputeLowerExcess, out normalizer))
		{
			return value + float.Parse(normalizer);
		}

		return value;
	}

	public static T Second<T>(this IList<T> items)
	{
		return items[1];
	}

	public static string SubstringFromTo(this string input, int startIndex, int endIndex)
	{
		return input[startIndex..endIndex];
	}

	public static string ToJsonFile(this string input)
	{
		return $"{input}.json";
	}

	public static bool TryDownloadFile(this GitHubClient client, string fileName, out RepositoryContent file)
	{
		file = null;

		if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
		{
			return false;
		}

		try
		{
			file = client.Repository.Content.GetAllContents(Constants.NETWORK_USER, Constants.NETWORK_REPO, fileName).Result.Single();
			return true;
		}
		catch
		{
			return false;
		}
	}

	public static bool TryGetIndexOf<T>(this Collection<T> items, T item, out int index)
	{
		index = items.IndexOf(item);
		return index >= 0;
	}

	public static async void TryUploadFile(this GitHubClient client, RepositoryContent file, string content)
	{
		if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
		{
			return;
		}

		try
		{
			await client.Repository.Content.UpdateFile(Constants.NETWORK_USER, Constants.NETWORK_REPO, file.Name, new UpdateFileRequest("Update", content, file.Sha));
		}
		catch
		{
		}
	}

	private static string ComputeLowerExcess(string input)
	{
		var result = string.Empty;

		while (input.StartsWith('0'))
		{
			input = input.Remove(1);
			result += '9';
		}

		return result + (input.Length * 10 - int.Parse(input)).ToString();
	}

	private static bool TryGetSequence(string number, char digit, Func<string, string> computeExcess, out string normalizer)
	{
		var sequence = Constants.MIN_SEQUENCE_CAPACITY - 1;
		normalizer = "0.";

		for (var i = 0; i < number.Length; i++)
		{
			if (number[i] == digit)
			{
				normalizer += '0';
				sequence--;
				continue;
			}

			if (sequence < 0)
			{
				normalizer += computeExcess(number.Substring(i));
				return true;
			}

			normalizer += '0';
		}

		return false;
	}
}
