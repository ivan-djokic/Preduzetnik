namespace MauiApp5.Utils;

public static class FileManager
{
	private static string Directory
	{
		get => FileSystem.Current.AppDataDirectory;
	}

	public static bool Read(string fileName, out string content)
	{
		try
		{
			using var stream = File.OpenRead(Path.Combine(Directory, fileName));
			using var reader = new StreamReader(stream);

			content = reader.ReadToEnd();
		}
		catch
		{
			content = string.Empty;
		}

		return !string.IsNullOrEmpty(content);
	}

	public static void Write(string fileName, string content)
	{
		try
		{
			using var stream = File.OpenWrite(Path.Combine(Directory, fileName));
			using var writer = new StreamWriter(stream);

			writer.Write(content);
		}
		catch
		{
		}
	}
}
