using Exercises.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Exercises.BusinessLayer.RepositoryContracts
{
    public interface IProductRepository: IDisposable
    {
        Task<List<Product>> GetProductsSortedByPriceAsc(CancellationToken ct = default(CancellationToken));
        Task<List<Product>> GetProductsSortedByPriceDesc(CancellationToken ct = default(CancellationToken));
        Task<List<Product>> GetProductsSortedByNameAsc(CancellationToken ct = default(CancellationToken));
        Task<List<Product>> GetProductsSortedByNameDesc(CancellationToken ct = default(CancellationToken));
        Task<List<Product>> GetRecommendedProducts(CancellationToken ct = default(CancellationToken));
    }
}
