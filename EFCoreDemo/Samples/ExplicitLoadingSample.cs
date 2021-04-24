using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    /// <summary>
    /// Перед тестированием этих прммеров убедитесь, что
    ///  //optionsBuilder.UseLazyLoadingProxies(); закоментирована в ApplicationContext
    /// </summary>
    public class ExplicitLoadingSample
    {
        private readonly ApplicationContext _context;

        public ExplicitLoadingSample(ApplicationContext context)
        {
            _context = context;
        }

        public async Task LoadProductsAndCategory()
        {
            // ОБРАТИТЕ ВНИМАНИЕ!!
            // В этом методе мы реально делаем 2 запроса в БД. 
            // Лучше стараться свести это к 1 запросу!!!!
            // SELECT TOP(1) [p].[ProductCategoryId], [p].[CreatedDate], [p].[Name]
            // FROM[ProductCategory] AS[p]
            var category = await _context.ProductCategories.FirstOrDefaultAsync();
            // Метод Load подгружает продукты у которых категория == категории которую мы получили выше
            // SELECT [p].[ProductId], [p].[CategoryId], [p].[Name]
            // FROM[Product] AS[p]
            // WHERE[p].[CategoryId] = @__category_Id_0
            await _context.Products.Where(p => p.CategoryId == category.Id).LoadAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in category.Products)
            {
                Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category?.Name}");
            }
        }

        public async Task LoadWithReference()
        {
            // ОБРАТИТЕ ВНИМАНИЕ!!
            // В этом методе мы реально делаем 2 запроса в БД. 
            // Лучше стараться свести это к 1 запросу!!!!
            // SELECT TOP(1) [p].[ProductCategoryId], [p].[CreatedDate], [p].[Name]
            // FROM[ProductCategory] AS[p]
            var product = await _context.Products.FirstOrDefaultAsync();
            // Метод Load подгружает продукты у которых категория == категории которую мы получили выше
            // SELECT [p].[ProductId], [p].[CategoryId], [p].[Name]
            // FROM[Product] AS[p]
            // WHERE[p].[CategoryId] = @__category_Id_0
            await _context.Entry(product).Reference(x => x.Category).LoadAsync();
            Console.WriteLine($"{product.Name} - {product.Category?.Name}");
        }

        public async Task LoadWithCollection()
        {
            // ВСЕГДА ПРОВЕРЯЙТЕ КАКОЙ ЗАПРОС ГЕНЕРИРУЕТСЯ!!!
            // ОНИ НЕ ВСЕГДА ОПТИМАЛЬНЫ!!!!!!
            // SELECT[t].[CompanyId], [t].[FoundationDate], [t].[Name], [t].[Revenue], [p].[ProductId], [t].[CompanyId0], [t].[ProductId], [t0].[CompanyId], [t0].[ProductId], [t0].[ProductId0], [t0].[CategoryId], [t0].[Name]
            // FROM[Product] AS[p]
            // INNER JOIN(
            //     SELECT[c].[CompanyId], [c].[FoundationDate], [c].[Name], [c].[Revenue], [s].[CompanyId] AS[CompanyId0], [s].[ProductId]
            // FROM [Supply] AS [s]
            // INNER JOIN [Company] AS[c] ON [s].[CompanyId] = [c].[CompanyId]
            // ) AS[t] ON[p].[ProductId] = [t].[ProductId]
            // LEFT JOIN(
            //     SELECT[s0].[CompanyId], [s0].[ProductId], [p0].[ProductId] AS[ProductId0], [p0].[CategoryId], [p0].[Name]
            // FROM [Supply] AS [s0]
            // INNER JOIN [Product] AS[p0] ON [s0].[ProductId] = [p0].[ProductId]
            // WHERE[p0].[ProductId] = @__p_0
            //     ) AS[t0] ON[t].[CompanyId] = [t0].[CompanyId]
            // WHERE[p].[ProductId] = @__p_0
            //     ORDER BY[p].[ProductId], [t].[CompanyId0], [t].[ProductId], [t].[CompanyId], [t0].[CompanyId], [t0].[ProductId], [t0].[ProductId0]
            var product = await _context.Products.FirstOrDefaultAsync();
            await _context.Entry(product).Collection(t => t.Companies).LoadAsync();

            Console.WriteLine($"Product: {product.Name}");
            foreach (var company in product.Companies)
            {
                Console.WriteLine($"Company: {company.Name}");
            }
        }
    }
}
