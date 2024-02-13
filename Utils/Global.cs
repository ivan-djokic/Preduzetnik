using MauiApp5.Models;
using Newtonsoft.Json;

namespace MauiApp5.Utils;

public static class Global
{
	private static readonly Lazy<Data> s_data = new(Load<Data>);
	private static readonly Lazy<Options> s_options = new(Load<Options>);

	public static Data Data
	{
		get => s_data.Value;
	}

	public static Options Options
	{
		get => s_options.Value;
	}

	public static void Save(ModelBase model)
	{
		var fileName = model.GetType().Name.ToJsonFile();
		var content = JsonConvert.SerializeObject(model, Formatting.Indented);

		FileManager.Write(fileName, content);
		NetworkClient.Instance.Upload(fileName, content);
	}

	private static T GetLocalModel<T>(string fileName, out string content)
		where T : new()
	{
		if (!FileManager.Read(fileName, out content) || !TryDeserialize(content, out T model))
		{
			return new();
		}

		return model;
	}

	private static T GetNetworkModel<T>(string fileName, out string content)
		where T : new()
	{
		if (!NetworkClient.Instance.TryDownload(fileName, out content) || !TryDeserialize(content, out T model))
		{
			return new();
		}

		return model;
	}

	private static T Load<T>()
		where T : new()
	{
		var fileName = typeof(T).Name.ToJsonFile();
		var localModel = GetLocalModel<T>(fileName, out var localContent);
		var networkModel = GetNetworkModel<T>(fileName, out var networkContent);

		if (networkModel is null)
		{
			return localModel;
		}

		if ((networkModel as ModelBase).Version < (localModel as ModelBase).Version)
		{
			NetworkClient.Instance.Upload(fileName, localContent);
			return localModel;
		}

		FileManager.Write(fileName, networkContent);
		return networkModel;
	}

	private static bool TryDeserialize<T>(string content, out T model)
	{
		try
		{
			model = JsonConvert.DeserializeObject<T>(content);
			return true;
		}
		catch
		{
			model = default;
			return false;
		}
	}
}
