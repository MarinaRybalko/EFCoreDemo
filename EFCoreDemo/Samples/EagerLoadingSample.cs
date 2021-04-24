using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    /// <summary>
    /// Перед тестированием этих прммеров убедитесь, что
    ///  //optionsBuilder.UseLazyLoadingProxies(); закоментирована в ApplicationContext
    /// </summary>
    public class EagerLoadingSample
    {
        private readonly ApplicationContext _context;

        public EagerLoadingSample(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Загрузить только данные о продуктах, связанные данные НЕ грузим
        /// </summary>
        /// <returns></returns>
        public async Task LoadOnlyProducts()
        {
            // Запрос, который сгенерируется
            // SELECT [p].[ProductId], [p].[CategoryId], [p].[Name]
            // FROM[Product] AS[p]
            var products = await _context.Products.ToListAsync();
            
            Console.WriteLine("Products");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Name}");
            }
        }

        public async Task LoadOnlyProductsAndTryToDisplayCategory()
        {
            var products = await _context.Products.ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in products)
            {
                //System.NullReferenceException: 'Object reference not set to an instance of an object.'
                //Когда пытаемся вывести данные с сущности Category, потому что в Eager loading нужно
                //ЯВНО ЗАГРУЖАТЬ СВЯЗАННЫЕ СУЩНОСТИ
                Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category.Name}");
            }
        }

        public async Task LoadProductsAndCategory()
        {
            //Запрос, который сгенерируется
            //SELECT [p].[ProductId], [p].[CategoryId], [p].[Name], [p0].[ProductCategoryId], [p0].[CreatedDate], [p0].[Name]
            //FROM[Product] AS[p]
            //INNER JOIN[ProductCategory] AS[p0] ON[p].[CategoryId] = [p0].[ProductCategoryId]
            var products = await _context.Products
                .Include(x => x.Category)
                .ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category.Name}");
            }
        }

        public async Task LoadProductsAndCategories()
        {
            // SELECT [p].[ProductCategoryId], [p].[CreatedDate], [p].[Name]
            // FROM [ProductCategory] AS[p]
            //Загружаем все категории в контекст
            var categories = await _context.ProductCategories.ToListAsync();
            // SELECT [p].[ProductId], [p].[CategoryId], [p].[Name]
            // FROM [Product] AS[p]
            //Загружаем все продукты в контекст
            var products = await _context.Products.ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in products)
            {
                //Тут уже не подгружаем данные, потому что они УЖЕ ЕСТЬ в контексте
                Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category.Name}");
            }
        }

        public async Task LoadCategoriesProductsAndSupply()
        {
            // SELECT [p].[ProductCategoryId], [p].[CreatedDate], [p].[Name], [t0].[ProductId], [t0].[CategoryId], [t0].[Name], [t0].[CompanyId], [t0].[ProductId0], [t0].[CompanyId0], [t0].[FoundationDate], [t0].[Name0], [t0].[Revenue]
            // FROM[ProductCategory] AS[p]
            // LEFT JOIN(
            //     SELECT[p0].[ProductId], [p0].[CategoryId], [p0].[Name], [t].[CompanyId], [t].[ProductId] AS[ProductId0], [t].[CompanyId0], [t].[FoundationDate], [t].[Name] AS[Name0], [t].[Revenue]
            // FROM [Product] AS [p0]
            // LEFT JOIN (
            //     SELECT[s].[CompanyId], [s].[ProductId], [c].[CompanyId] AS[CompanyId0], [c].[FoundationDate], [c].[Name], [c].[Revenue]
            // FROM [Supply] AS [s]
            // INNER JOIN [Company] AS[c] ON [s].[CompanyId] = [c].[CompanyId]
            // ) AS[t] ON[p0].[ProductId] = [t].[ProductId]
            // ) AS[t0] ON[p].[ProductCategoryId] = [t0].[CategoryId]
            // ORDER BY[p].[ProductCategoryId], [t0].[ProductId], [t0].[CompanyId], [t0].[ProductId0], [t0].[CompanyId0]
            var data = await _context.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Companies).ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in data)
            {
                Console.WriteLine($"Product Name: {product.Name}. Product Created Date: {product.CreatedDate}");
            }
        }
    }
}
