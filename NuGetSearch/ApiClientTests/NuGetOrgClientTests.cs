using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGetApiClientLib;
using NuGetApiClientLib.NuGetService;

namespace ApiClientTests
{
    [TestClass]
    public class NuGetOrgClientTests
    {
        [TestMethod]
        public async Task SearchPackagesByTerm_Test()
        {
            var nugetOrgFeed = new NuGetOrgSearcher();
            var result = await nugetOrgFeed.SearchPackagesByTermAsync("Edi.", 1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response<IEnumerable<V2FeedPackage>>));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Item.Any());

            var package = result.Item.First();
            Assert.IsNotNull(package);
            Assert.IsTrue(package.Title.Contains("Edi"));
        }

        [TestMethod]
        public async Task GetMostPopularPackages_Test()
        {
            var nugetOrgFeed = new NuGetOrgSearcher();
            var result = await nugetOrgFeed.GetMostPopularPackagesAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response<IEnumerable<V2FeedPackage>>));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Item.Any());
            Assert.IsTrue(result.Item.Count() == nugetOrgFeed.PageSize);

            var package = result.Item.First();
            Assert.IsNotNull(package);
        }

        [TestMethod]
        public async Task GetTopMicrosoftDotNetPackages_Test()
        {
            var nugetOrgFeed = new NuGetOrgSearcher();
            var result = await nugetOrgFeed.GetTopMicrosoftDotNetPackagesAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response<IEnumerable<V2FeedPackage>>));
            Assert.IsTrue(result.IsSuccess);
            Assert.IsTrue(result.Item.Any());
            Assert.IsTrue(result.Item.Count() == nugetOrgFeed.PageSize);

            var package = result.Item.First();
            Assert.IsNotNull(package);
        }

        [TestMethod]
        public void PageSize_Test()
        {
            var nugetOrgFeed = new NuGetOrgSearcher();
            Assert.IsTrue(nugetOrgFeed.PageSize == 30);

            nugetOrgFeed.PageSize = 20;
            Assert.IsTrue(nugetOrgFeed.PageSize == 20);
        }

        [TestMethod]
        public async Task GetMostPopularPackagesPaging_Test()
        {
            var nugetOrgFeed = new NuGetOrgSearcher();

            var page1Task = nugetOrgFeed.GetMostPopularPackagesAsync(1);
            var page2Task = nugetOrgFeed.GetMostPopularPackagesAsync(2);

            var page1Result = await page1Task;
            var page2Result = await page2Task;

            Assert.IsTrue(page1Result.Item.Count() == nugetOrgFeed.PageSize);
            Assert.IsTrue(page2Result.Item.Count() == nugetOrgFeed.PageSize);

            bool dontHaveMatch = page1Result.Item.Any(p => page2Result.Item.All(p2 => p.Id != p2.Id));
            Assert.IsTrue(dontHaveMatch);
        }
    }
}
