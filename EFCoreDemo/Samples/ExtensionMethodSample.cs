using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    public class ExtensionMethodSample
    {
        private readonly ApplicationContext _context;

        public ExtensionMethodSample(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Этот метод выбросит исключение, потому что НЕЛЬЗЯ использовать методы написанные на С#
        /// в выражении Select!!!!!
        /// </summary>
        /// <returns></returns>
        public async Task SelectWithExtensionMethod()
        {
            var products = await _context.Products
                .OrderByDescending(x=> x.Name)
                .Select(product => new {  Name = FormatName(product.Name) })
                .ToListAsync();

            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {product.Name}.");
            }
        }

        /// <summary>
        /// Этот метод сработает корректно, потому что мы сначала получим данные с БД
        /// А потом уже в приложении вызовем FormatName метод
        /// </summary>
        /// <returns></returns>
        public async Task SelectWithExtensionMethodV2()
        {
           // SELECT [p].[Name]
           // FROM [Product] AS[p]
           // ORDER BY[p].[Name] DESC
           // Обратите внимание мы выбираем ТОЛЬКО имя, а не всю информацию  о продукте!!!!
           var products = await _context.Products
                .OrderByDescending(x => x.Name)
                .Select(product => new { product.Name })
                .ToListAsync();

            Console.WriteLine("Products:");
            foreach (var product in products)
            {
                Console.WriteLine($"Product Name: {FormatName(product.Name)}.");
            }
        }

        private string FormatName(string name)
        {
            name = name.ToLower();

            if (name.Contains("-"))
            {
                name = name.Replace('-', ' ');
            }

            return name;
        }
    }
}
