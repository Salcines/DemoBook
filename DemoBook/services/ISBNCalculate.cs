using Microsoft.VisualBasic.CompilerServices;

namespace DemoBook.services;

public static class ISBNCalculate
{
	const int Weightcontrol = 7;
	private const int Limitponderate = 12;

	private static readonly Random Rnd = new Random();
	public static string CalculateISBN()
	{
		string prefixEAN = Rnd.Next(978, 979).ToString();
		string registrationGroups = Rnd.Next(0, 99_995).ToString();
		string titular = Rnd.Next(0, 10_000_000).ToString();
		string publication = Rnd.Next(0, 1_000_000).ToString("D6");

		string partialISBN = $"{prefixEAN}{registrationGroups}{titular}{publication}";

		string[] splitISBN = partialISBN.Where(char.IsDigit).
		                                 Select(c => c.ToString()).
		                                 ToArray();

		int pondererProduct = 0;

		for (int i = 1; i < Limitponderate; i+=2)
		{
			pondererProduct += (Convert.ToInt32(splitISBN[i]) * Weightcontrol);
		}

		int controlDigit = 0;

		if (pondererProduct % 10 != 0)
		{
			controlDigit = 10 - pondererProduct % 10;
		}

		return $"{partialISBN}{controlDigit}";
	}
}