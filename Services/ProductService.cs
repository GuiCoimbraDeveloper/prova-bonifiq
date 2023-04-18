using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService
    {
        TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public ProductList ListProducts(int page)
        {
            return new ProductList()
            {
                HasNext = false,
                TotalCount = 10,
                Values = _ctx.Products
                .OrderBy(b => b.Id).Skip((page - 1) * 10).Take(page * 10).ToList()
            };
        }

    }
}
