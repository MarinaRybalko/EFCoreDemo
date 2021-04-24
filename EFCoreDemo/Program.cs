using System;
using System.Threading.Tasks;
using EFCoreDemo.Samples;

namespace EFCoreDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            #region Eager loading

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new EagerLoadingSample(context).LoadOnlyProducts();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new EagerLoadingSample(context).LoadOnlyProductsAndTryToDisplayCategory();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new EagerLoadingSample(context).LoadProductsAndCategory();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new EagerLoadingSample(context).LoadProductsAndCategories();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new EagerLoadingSample(context).LoadCategoriesProductsAndSupply();
            //}

            #endregion

            #region Explicit loading

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ExplicitLoadingSample(context).LoadProductsAndCategory();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ExplicitLoadingSample(context).LoadWithReference();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ExplicitLoadingSample(context).LoadWithCollection();
            //}

            #endregion

            #region Lazy Loading

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new LazyLoadingSamples(context).LoadOnlyProducts();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new LazyLoadingSamples(context).LoadOnlyProductsAndTryToDisplayCategory();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new LazyLoadingSamples(context).LoadCategoriesProductsAndSupply();
            //}

            #endregion

            #region Extension method

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ExtensionMethodSample(context).SelectWithExtensionMethod();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ExtensionMethodSample(context).SelectWithExtensionMethodV2();
            //}

            #endregion

            #region AsNoTracking() method

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new AsNoTrackingSample(context).WithoutAsNoTracking();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new AsNoTrackingSample(context).AsNoTracking();
            //}

            #endregion

            #region Concurrent

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ConcurrentExceptionSample(context).ThreadSafeTest();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ConcurrentExceptionSample(context).ThreadSafeTestV2();
            //}

            //await using (var context = new SampleContextFactory().CreateDbContext(args))
            //{
            //    await new ConcurrentExceptionSample(context).UpdateIsConcurrencyToken();
            //}

            #endregion

            Console.ReadKey();
        }
    }
}
