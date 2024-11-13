using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Categories;
public class CategoryRepository(AppDbContext context) : GenericRepository<Category, int>(context), ICategoryRepository
{
	public IQueryable<Category> GetCategoryWithProducts()
	{
		var categoryWithProducts = context.Categories.Include(c => c.Products).AsQueryable();
		return categoryWithProducts;
	}

	public async Task<Category?> GetCategoryWithProductsAsync(int id)
	{
		var categoryWithProducts = await context.Categories.Include(c => c.Products).FirstOrDefaultAsync(i => i.Id == id);
		return categoryWithProducts;
	}
}
