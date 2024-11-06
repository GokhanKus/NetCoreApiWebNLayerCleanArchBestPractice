namespace App.Repositories;
public class Product
{
	public int Id { get; set; }
	public string Name { get; set; } = default!; //default deger (null) olmayacak
	public decimal Price { get; set; }
	public int Stock { get; set; }
}