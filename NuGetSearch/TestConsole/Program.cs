using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NuGetApiClientLib;

namespace TestConsole
{
    class Program
    {
        private static Task initTask;

        static void Main(string[] args)
        {
            var nugetOrgFeed = new NuGetOrgSearcher();
            initTask = nugetOrgFeed.SearchPackagesByTermAsync("Edi.", 1);
            Console.ReadKey();
        }
    }
}
