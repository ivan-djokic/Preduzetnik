using Octokit;

namespace MauiApp5.Utils;

public class NetworkClient
{
	private readonly GitHubClient m_client;
	private static readonly Lazy<NetworkClient> s_instance = new(() => new NetworkClient());

	private NetworkClient()
	{
		m_client = new(new ProductHeaderValue(Constants.NETWORK_REPO))
		{
			Credentials = new Credentials(Constants.NETWORK_TOKEN)
		};
	}

	public static NetworkClient Instance
	{
		get => s_instance.Value;
	}

	public bool TryDownload(string fileName, out string content)
	{
		content = string.Empty;

		if (m_client.TryDownloadFile(fileName, out var file))
		{
			content = file.Content;
		}

		return !string.IsNullOrWhiteSpace(content);
	}

	public void Upload(string fileName, string content)
	{
		if (!m_client.TryDownloadFile(fileName, out var file))
		{
			return;
		}

		m_client.TryUploadFile(file, content);
	}
}
