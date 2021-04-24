using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    public class AsNoTrackingSample
    {
        private readonly ApplicationContext _context;

        public AsNoTrackingSample(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Этот метод показывает как работает механизм Change Detection
        /// По умолчанию, когда вы получаете сущность с БД она начинает отслеживатся DbContext-ом
        /// Все изменения, которые сделаны с этой сущностью будут применены к бд после вызова
        /// SaveChangesAsync/SaveChanges
        /// </summary>
        /// <returns></returns>
        public async Task WithoutAsNoTracking()
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == 3);
            Console.WriteLine($"Product Name: {product.Name}");
            
            product.Name = "Test" + Guid.NewGuid();
            await _context.SaveChangesAsync();

            var updated = await _context.Products.FirstOrDefaultAsync(x => x.Id == 3);
            Console.WriteLine($"Updated product Name: {updated.Name}");
        }

        /// <summary>
        /// По умолчанию, когда вы получаете сущность с БД она начинает отслеживатся DbContext-ом
        /// Все изменения, которые сделаны с этой сущностью будут применены к бд после вызова
        /// SaveChangesAsync/SaveChanges
        /// AsNoTracking позволяет НЕ отслеживать сущность и не тратить на это ресурсы 
        /// Когда использовать AsNoTracking?? Если вы просто выбираете данные с БД и НЕ изменяете их
        /// С AsNoTracking() немного будет быстрее
        /// ПРАВИЛО!!! Если данные просто отдать на фронт или использовать ТОЛЬКО для чтения используйте AsNoTracking
        /// </summary>
        /// <returns></returns>
        public async Task AsNoTracking()
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);
            Console.WriteLine($"Product Name: {product.Name}");

            product.Name = "Test" + Guid.NewGuid();
            await _context.SaveChangesAsync();

            var updated = await _context.Products.FirstOrDefaultAsync(x => x.Id == 2);
            Console.WriteLine($"Updated product Name: {updated.Name}");
        }
    }
}
