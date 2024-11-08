namespace App.Services.Products;
public interface IProductService
{
	Task<ServiceResult<List<ProductDto>>> GetAllAsync();
	Task<ServiceResult<List<ProductDto>>> GetAllPagedAsync(int pageNumber, int pageSize);
	Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
	Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
	Task<ServiceResult<CreateProductResponse>> CreateAsync(CreateProductRequest request);
	Task<ServiceResult> UpdateAsync(int id, UpdateProductRequest request);
	Task<ServiceResult> DeleteAsync(int id);
}
