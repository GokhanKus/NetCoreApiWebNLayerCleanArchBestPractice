﻿using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products;
public class ProductRepository(AppDbContext context) : GenericRepository<Product, int>(context), IProductRepository
{
	public Task<List<Product>> GetTopPriceProductAsync(int count)
	{
		return _context.Products.OrderByDescending(p => p.Price).Take(count).ToListAsync();
	}
}
