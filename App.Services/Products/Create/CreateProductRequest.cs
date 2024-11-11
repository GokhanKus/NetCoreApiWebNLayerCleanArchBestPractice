namespace App.Services.Products.Create;
public record CreateProductRequest(string Name, decimal Price, int Stock,int CategoryId);
//Request ve Responseler birer dtodur acıkca yazmaya artık gerek yok