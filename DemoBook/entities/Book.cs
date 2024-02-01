namespace DemoBook.entities;

public class Book
{
	public required string Id { get; init; }
	public string? Title { get; set; }
	public string? Author { get; set; }
	public string? Category { get; set; }
	public bool IsAvailable { get; set; }       
}