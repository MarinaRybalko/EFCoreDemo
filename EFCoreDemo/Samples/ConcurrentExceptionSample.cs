using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo.Samples
{
    public class ConcurrentExceptionSample
    {
        private readonly ApplicationContext _context;

        public ConcurrentExceptionSample(ApplicationContext context)
        {
            _context = context;
        }

        // !!!!!!!!!!!!!DBContext НЕ ThreadSafe(потокобезопасный) !!!!!!!!!!!!!!!
        // Когда вы работаете с ним внимально следите, чтобы ДО момента пока началась 2 операция
        // первая ОБЯЗАТЕЛЬНО ЗАВЕРШИЛАСЬ!!
        // Иначе будет исключение started on this context before a previous operation completed. Any instance members are not guaranteed to be thread safe.
        // Не нужно пытаться делать как в этом примере!!!!!
        public async Task ThreadSafeTest()
        {
            var task1 = Task.Run(async () =>
           {
               var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == 8);
               product.Name = "FADED-EFFECT DRESS WITH POCKETS" + Guid.NewGuid();
               await Task.Delay(TimeSpan.FromSeconds(5));
               await _context.SaveChangesAsync();
           });
           var task2 = Task.Run(async () =>
           {
               var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == 1);
               company.Name = "Test Company " + Guid.NewGuid();
               await Task.Delay(TimeSpan.FromSeconds(2));
               await _context.SaveChangesAsync();
           });

           await Task.WhenAll(task2, task1);
        }

        /// <summary>
        /// Корректный пример работы с DbContext!!!
        /// Вы можете делать несколько операций с сущностями при этом ДОЖИДАЯСЬ завершения операции
        /// перед началом второй
        /// </summary>
        /// <returns></returns>
        public async Task ThreadSafeTestV2()
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == 8);
            product.Name = "FADED-EFFECT DRESS WITH POCKETS" + Guid.NewGuid();
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == 1);
            company.Name = "Test Company ";

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Это самое интересное.
        /// Например: У нас есть 2 пользователя, которые редактируют Название товара.
        /// Хорошо. Допустим 2 пользователя загрузили страницу с товарами.
        /// Первый пользователь отредактировал Название товара, а потом второй пользователь тоже
        /// отредактировал название этого же товара. И получается перетер изменения первого.
        /// Есть ситуации, когда ЭТО НЕЖЕЛАТЕЛЬНОЕ ПОВЕДЕНИЕ!!!!
        /// НУЖНО НЕ РАЗРЕШАТЬ ПЕРЕТИРАТЬ ИЗМЕНЕНИЯ, А ВЫБРАСЫВАТЬ ОШИБКУ!!!!
        /// Для этого используется IsConcurrencyToken
        /// Как протестировать этот пример???
        /// В файле ProductCategoryConfiguration раскоментируйте строку
        /// builder.Property(p => p.Name).HasMaxLength(255).IsConcurrencyToken(); и закоментируйте строку выше
        /// Поставьте дебаггер на строке 75 в этом файле и запустите
        /// Когда остановился дебаггер выполните команду в Management Studio и нажмите Continue
        /// UPDATE ProductCategory SET Name = 'Test 33333' WHERE ProductCategoryId = 3
        /// И вы увидите исключение! Почему? Потому что у вас в приложении не актуальное имя компании загружено
        /// </summary>
        /// <returns></returns>
        public async Task UpdateIsConcurrencyToken()
        {
            var category = await _context.ProductCategories.FirstOrDefaultAsync(x => x.Id == 3);
            category.Name = Guid.NewGuid().ToString();

            await _context.SaveChangesAsync();
        }
    }
}
