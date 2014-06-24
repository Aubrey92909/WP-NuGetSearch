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
    }
}
