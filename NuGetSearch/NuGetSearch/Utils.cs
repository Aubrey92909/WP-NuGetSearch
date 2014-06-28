using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Phone.Tasks;

namespace NuGetSearch
{
    public class Utils
    {
        public static void GoToReview()
        {
            var rev = new MarketplaceReviewTask();
            rev.Show();
        }
    }
}
