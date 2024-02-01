using System.Text.Json;
using DemoBook.entities;

namespace DemoBook.Util;

public static class StorageBooks
{
	public static List<Book> LoadFromFile()
	{
		if (!File.Exists(Constants.BooksFile))
		{
			return []; //Collection expression --> Normal expression: return new List<Book>()
		}

		string json = File.ReadAllText(Constants.BooksFile);
		return (string.IsNullOrEmpty(json) ? [] : JsonSerializer.Deserialize<List<Book>>(json))!;
	}

	public static void SaveToFile(List<Book> book)
	{
		string json = JsonSerializer.Serialize(book);

		File.WriteAllText(Constants.BooksFile, json);
	}
}