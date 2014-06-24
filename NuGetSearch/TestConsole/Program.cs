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
            var nugetClient = new NuGetApiClient("https://www.nuget.org/api/v2/");
            initTask = nugetClient.SetPageSize(30).GetDataAsync("Edi.", 1);
            Console.ReadKey();
        }
    }
}
