using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        #region GetDetails
        public EditProductCategory GetDetails(long id)
        {
            //get productCategory with id of dataBase
            return _context.ProductCategories.Select(x => new EditProductCategory()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Keywords = x.Keywords,
                MetaDescription = x.MetaDescription,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug
            }).FirstOrDefault(x => x.Id == id);
        }

        #endregion
        #region GetProductCategories
        public List<ProductCategoryViewModel> GetProductCategories()
        {
            ////get list productCategories of dataBase
            return _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        #endregion
        #region GetSlugById
        public string GetSlugById(long id)
        {
            return _context.ProductCategories.Select(x => new { x.Id, x.Slug }).SingleOrDefault(x => x.Id == id).Slug;

        }
        #endregion
        #region Search
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            //Getting list ProductCategories from the ProductCategoryViewModel
            var query = _context.ProductCategories.Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                Name = x.Name,
                CreationDate = x.CreationDate.ToFarsi(),
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle
            });
            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }
            return query.OrderByDescending(x => x.Id).ToList();
        }

        #endregion

    }
}
