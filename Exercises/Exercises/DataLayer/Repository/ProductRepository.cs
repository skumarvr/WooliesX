using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exercises.BusinessLayer.Entities;
using Exercises.BusinessLayer.RepositoryContracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exercises.DataLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ExercisesDbContext _context;
        private readonly IConfigurationProvider _config;
        public ProductRepository(ExercisesDbContext context, IConfigurationProvider config)
        {
            _context = context;
            _config = config;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<List<Product>> GetProductsSortedByNameAsc(CancellationToken ct = default(CancellationToken))
        {
            return await _context.products
                                .OrderBy(prod => prod.name)
                                .ProjectTo<Product>(_config)
                                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsSortedByNameDesc(CancellationToken ct = default(CancellationToken))
        {
            return await _context.products
                                .OrderByDescending(prod => prod.name)
                                .ProjectTo<Product>(_config)
                                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsSortedByPriceAsc(CancellationToken ct = default(CancellationToken))
        {
            return await _context.products
                                .OrderBy(prod => prod.price)
                                .ProjectTo<Product>(_config)
                                .ToListAsync();
        }

        public async Task<List<Product>> GetProductsSortedByPriceDesc(CancellationToken ct = default(CancellationToken))
        {
            return await _context.products
                                .OrderByDescending(prod => prod.price)
                                .ProjectTo<Product>(_config)
                                .ToListAsync();
        }

        public async Task<List<Product>> GetRecommendedProducts(CancellationToken ct = default(CancellationToken))
        {
            var custProd = await _context.shopperHistories
                                .GroupBy(sh => sh.productName)
                                .Select(cl => new Product
                                {
                                    name = cl.Key,
                                    price = 0,
                                    quantity = cl.Sum(c => c.quantity) 
                                })
                                .ToListAsync();

            var prod = await _context.products.ProjectTo<Product>(_config).ToListAsync();

            return custProd.Union(prod, new ProductComparer()).OrderByDescending(pr => pr.quantity).ToList();
        }
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product prod1, Product prod2)
        {
            return (prod1.name == prod2.name);
        }

        public int GetHashCode([DisallowNull] Product obj)
        {
            return obj.name.GetHashCode();
        }
    }
}
