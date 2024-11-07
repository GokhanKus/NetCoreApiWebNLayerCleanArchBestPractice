namespace App.Services.Products;
public record CreateProductRequest(string Name, decimal Price, int Stock);
//Request ve Responseler birer dtodur acıkca yazmaya artık gerek yok