using System.Text;
using DemoBook.entities;
using DemoBook.Resources;
using DemoBook.Util;

namespace DemoBook.services;

public static class BookService
{
	private static List<Book> _books = [];

	public static string Create()
	{
		Console.WriteLine(Globalization.ServiceTitle);
		Console.WriteLine(Globalization.BookTitleMessage);
		var name = Console.ReadLine();
		Console.WriteLine(Globalization.AuthorMessage);
		var author = Console.ReadLine();
		Console.WriteLine(Globalization.CategoryMessage);
		var category = Console.ReadLine();

		_books = StorageBooks.LoadFromFile();

		var book = new Book()
		{
			Id = ISBNCalculate.CalculateISBN(),
			Title = name,
			Author = author,
			Category = category,
			IsAvailable = true
		};

		_books.Add(book);
		StorageBooks.SaveToFile(_books);

		return string.Format(Globalization.ReturnCreate, name);
	}

	public static string Update()
	{
		Console.Clear();
		Console.WriteLine(Globalization.Update);
		Console.WriteLine(Globalization.UpdateTitleMessage);

		string isbn = Console.ReadLine() ?? Globalization.ISBNError;

		_books = StorageBooks.LoadFromFile();
		var book = _books.FirstOrDefault(code => code.Id == isbn);

		if (book == null)
		{
			return string.Format(Globalization.notFound, isbn);
		}

		Console.WriteLine(Globalization.newTitle);
		string? title = Console.ReadLine();

		Console.WriteLine(Globalization.newAuthor);
		string? name = Console.ReadLine();

		Console.WriteLine(Globalization.newCategory);
		string? category = Console.ReadLine();

		Console.WriteLine(Globalization.bookAvailable);
		string? isAvailable = Console.ReadLine()?.ToUpper();

		return UpdateBook(book, title, name, category, isAvailable);
	}

	private static string UpdateBook(Book book, string? title, string? name, string? category, string? isAvailable)
	{
		_books = StorageBooks.LoadFromFile();

		if (!string.IsNullOrWhiteSpace(title))
		{
			book.Title = title;
		}

		if (!string.IsNullOrWhiteSpace(name))
		{
			book.Author = name;
		}

		if (!string.IsNullOrWhiteSpace(category))
		{
			book.Category = category;
		}

		if (!string.IsNullOrWhiteSpace(isAvailable))
		{
			book.IsAvailable = isAvailable == Globalization.Positive;
		}

		StorageBooks.SaveToFile(_books);

		return string.Format(Globalization.UpdateMessage, book.Id);
	}

	public static string GetAll()
	{
		Console.Clear();

		_books = StorageBooks.LoadFromFile();

		if (_books.Count == 0)
		{
			return Globalization.emptyBooks;
		}

		var builder = new StringBuilder();

		// builder.AppendLine("| ISBN".PadRight(10) + "| " + Globalization.title.PadRight(20) + "| Author" + "| ");
		builder.AppendLine($"| {Globalization.ISBN.PadRight(23)}| {Globalization.title.PadRight(20)}" +
		                   $"|{Globalization.author.PadRight(20)}| {Globalization.Category.PadRight(20)}| " +
		                   $"{Globalization.isAvailable.PadRight(10)}");

		foreach (var book in _books)
		{
			builder.AppendLine($"| {book.Id.PadRight(22)}| {book?.Title?.PadRight(19)}| {book?.Author?.PadRight(19)}" +
			                   $"| {book?.Category?.PadRight(19)}| " +
			                   $"{(book!.IsAvailable ? Globalization.Positive : Globalization.Negative).PadRight(9)}");
		}


		return builder.ToString();
	}

	public static string GetBook()
	{
		Console.Clear();

		Console.WriteLine(Globalization.SearchBook);
		Console.WriteLine(Globalization.SearchBookMessage);

		string isbn = Console.ReadLine() ?? string.Format(Globalization.ISBNError);

		var message = SearchBook(isbn);

		return message;
	}

	private static string SearchBook(string isbn)
	{
		_books = StorageBooks.LoadFromFile();

		var book = _books.FirstOrDefault(code => code.Id == isbn);

		return book == null
			? string.Format(Globalization.notFound, isbn)
			: string.Format(Globalization.SearchBookResponse, isbn, book.Title, book.Author);
	}

	public static string Delete()
	{
		Console.Clear();

		Console.WriteLine(Globalization.DeleteBook);
		Console.WriteLine(Globalization.SearchBookMessage);

		string isbn = Console.ReadLine() ?? Globalization.ISBNError;

		return DeleteBook(isbn);
	}

	private static string DeleteBook(string isbn)
	{
		_books = StorageBooks.LoadFromFile();
		var book = _books.FirstOrDefault(code => code.Id == isbn);

		if (book == null)
		{
			return string.Format(Globalization.notFound, isbn);
		}

		_books.Remove(book);

		StorageBooks.SaveToFile(_books);

		return string.Format(Globalization.DeleteMessage, isbn);
	}
}