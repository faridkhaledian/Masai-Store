using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        #region GetDetails
        public EditProduct GetDetails(long id)
        {
            //Getting a product with id
            return _context.Products.Select(x => new EditProduct
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                Slug = x.Slug,
                CategoryId = x.CategoryId,
                Description = x.Description,
                KeyWords = x.KeyWords,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ShortDescription = x.ShortDescription,
            }).FirstOrDefault(x => x.Id == id);
        }

        #endregion

        #region GetProducts
        public List<ProductViewModel> GetProducts()
        {
            //get list of products
            return _context.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        #endregion

        #region GetProductWithCategory
        public Product GetProductWithCategory(long id)
        {
            //get productWithCategory with id
            return _context.Products.Include(x => x.Category).SingleOrDefault(x => x.Id == id);
        }

        #endregion

        #region Search
        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            //get product with category
            var query = _context.Products
                  .Include(x => x.Category)
                  .Select(x => new ProductViewModel
                  {
                      Id = x.Id,
                      Name = x.Name,
                      Category = x.Category.Name,
                      CategoryId = x.CategoryId,
                      Code = x.Code,
                      Picture = x.Picture,
                      CreationDate = x.CreationDate.ToFarsi()
                  });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(x => x.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Code))
                query = query.Where(x => x.Code.Contains(searchModel.Code));


            if (searchModel.CategoryId != 0)
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);

            return query.OrderByDescending(x => x.Id).ToList();
        }

        #endregion

    }
}
