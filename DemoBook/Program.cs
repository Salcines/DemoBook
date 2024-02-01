// See https://aka.ms/new-console-template for more information

using DemoBook.Resources;
using DemoBook.services;
using DemoBook.Util;

while (true)
{
	//CultureInfo.CurrentUICulture = new CultureInfo("en-us");
	Console.Clear();

	var optionSelection = Options();

	var bookOptions = (BookOptions)optionSelection;

	if (bookOptions.Equals(BookOptions.Exit)) return;

	var message = bookOptions switch
	{
		BookOptions.Add => BookService.Create(),
		BookOptions.Update => BookService.Update(),
		BookOptions.GetAll => BookService.GetAll(),
		BookOptions.GetBook => BookService.GetBook(),
		BookOptions.Delete => BookService.Delete(),
		_ => "wrong option"
	};

	Console.WriteLine(message);

	Console.WriteLine($"\n{Globalization.Continue}");
	Console.ReadKey();

	static void Header()
	{
		Console.WriteLine(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "Resources", "TitleArt.txt")));
		Console.WriteLine($@"		{Globalization.Subtitle}");
	}

	static int Options()
	{
		while (true)
		{
			Console.Clear();
			Header();

			Console.WriteLine($@"1. {Globalization.Add}");
			Console.WriteLine(@"______________________");
			Console.WriteLine($@"2. {Globalization.Update}");
			Console.WriteLine(@"______________________");
			Console.WriteLine($@"3. {Globalization.Read}");
			Console.WriteLine(@"______________________");
			Console.WriteLine($@"4. {Globalization.ReadAll}");
			Console.WriteLine(@"______________________");
			Console.WriteLine($@"5. {Globalization.Delete}");
			Console.WriteLine(@"______________________");
			Console.WriteLine($@"6. {Globalization.Exit}");

			Console.WriteLine($@"----------{Globalization.Option}----------");

			var input = Console.ReadLine();
			if (int.TryParse(input, out var option)) return option;

			Console.WriteLine(Globalization.WrongOption);
			Console.ReadKey();
		}
	}
}