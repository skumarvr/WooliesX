using AutoMapper;
using AutoMapper.QueryableExtensions;
using Exercises.BusinessLayer.Entities;
using Exercises.BusinessLayer.RepositoryContracts;
using Exercises.BusinessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Exercises.BusinessLayer.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IConfigurationProvider _config;
        public ProductService(IProductRepository productRepository, IConfigurationProvider config)
        {
            _productRepository = productRepository;
            _config = config;
        }

        public async Task<List<ProductsResponse>> GetProducts(SortOptions sortOptions, CancellationToken ct = default(CancellationToken))
        {
            var prods = sortOptions switch
            {
                SortOptions.Low => await _productRepository.GetProductsSortedByPriceAsc(),
                SortOptions.High => await _productRepository.GetProductsSortedByPriceDesc(),
                SortOptions.Ascending => await _productRepository.GetProductsSortedByNameAsc(),
                SortOptions.Descending => await _productRepository.GetProductsSortedByNameDesc(),
                SortOptions.Recommended => await _productRepository.GetRecommendedProducts(),
                _ => null,
            };

            return prods.AsQueryable().ProjectTo<ProductsResponse>(_config).ToList();
        }
    }
}
