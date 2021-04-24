using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    /// <summary>
    /// Перед тестированием этих прммеров убедитесь, что
    ///  //optionsBuilder.UseLazyLoadingProxies(); НЕ закоментирована в ApplicationContext
    /// </summary>
    public class LazyLoadingSamples
    {
        private readonly ApplicationContext _context;

        public LazyLoadingSamples(ApplicationContext context)
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
            // SELECT [p].[ProductId], [p].[CategoryId], [p].[Name]
            // FROM[Product] AS[p]
            var products = await _context.Products.ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in products)
            {
                // SELECT [p].[ProductCategoryId], [p].[CreatedDate], [p].[Name]
                // FROM[ProductCategory] AS[p]
                // WHERE[p].[ProductCategoryId] = @__p_0
                // SELECT[p].[ProductCategoryId], [p].[CreatedDate], [p].[Name]
                // FROM[ProductCategory] AS[p]
                // WHERE[p].[ProductCategoryId] = @__p_0
                // ОБРАТИТЕ ВНИМАНИЕ КОЛ-ВО ЗАПРОСОВ ТУТ БУДЕТ ЗАСИСЕТЬ ОТ ТОГО СКОЛЬКО РАЗЛИЧНЫХ КАТЕГОРИЙ У ПРОДУКТОВ
                // ПРИМЕР: У нас есть 10 продуктов, но 6 из них имеют категорию Платья, а 4 имеют категорию Пиджаки.
                // То будет сгенерировано 2 запроса на получение категорий!!
                Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category.Name}");
            }

            // Переписанная версия, которая ВСЕГДА генерирует 1 запрос
            #region Updated version

            //var products = await _context.Products
            //    .Select(x => new { Category = x.Category.Name, x.Name })
            //    .ToListAsync();

            //Console.WriteLine("Products with Categories");
            //foreach (var product in products)
            //{
            //    Console.WriteLine($"Product Name: {product.Name}. Product Category: {product.Category}");
            //}

            #endregion
        }

        /// <summary>
        /// Выводит название продукта и название компании, которая поставляет продукт.
        /// Выводит только продукты, которые поставляет хотя бы одна компания.
        /// Выводит только компании, которые поставляют хотя бы один продукт.
        /// </summary>
        /// <returns></returns>
        public async Task LoadCategoriesProductsAndSupply()
        {
            // Обратите внимание какой запрос генерируется!!!!!
            // Посмотрите на запрос и на тип джоина. 
            // Написанный код будет транслирован в LEFT JOIN и мы получим продукты, которые не поставляет ни одна компания
            // А ЭТО НЕ СООТВЕТСТВУЕТ ТРЕБОВАНИЯМ!! 
            var data = await _context.Products
                .Select(x => new { ProductName = x.Name, Companies = x.Companies.Select(v => v.Name) })
                .ToListAsync();

            Console.WriteLine("Products with Categories");
            foreach (var product in data)
            {
                Console.WriteLine($"Product Name: {product.ProductName}. Companies: {string.Join(",", product.Companies)}");
            }

            // Немного переписав запрос LINQ вы получите совсем другой запрос SQL
            #region Updated version

            //var data2 = await _context.Products
            //    .SelectMany(z => z.Companies.Select(v => new { CompanyName = v.Name, Product = z.Name }))
            //    .ToListAsync();

            //Console.WriteLine("Products with Categories");
            //foreach (var supply in data2)
            //{
            //    Console.WriteLine($"Company: {supply.CompanyName}. Product: {supply.Product}");
            //}

            #endregion
        }
    }
}
